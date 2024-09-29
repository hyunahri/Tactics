using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Characters;
using NCalc;
using UnityEngine;
using Int32 = System.Int32;

namespace Game
{
    public static class FormulaEvaluator
    {
        private static bool DebugExpressions = false;

        public static T EvaluateExpression<T>(string e, ICharacter user, ICharacter target, Dictionary<string, float> customVariables)
        {
            e = e.Replace(" ", "");
            Dictionary<string, float> solvedVariables = new Dictionary<string, float>();
            HashSet<string> variables = ExtractVariables(e);

            //Replace with custom variables if any
            foreach (var set in customVariables)
            {
                if (variables.Remove(set.Key))
                {
                    if (DebugExpressions)
                        Debug.Log($"Replacing custom variable: '{set.Key}' with value: {set.Value}");
                    solvedVariables.Add(set.Key, set.Value);
                }
            }

            var scopedVarResults = ParseStats(variables, user, target);
            foreach (var set in scopedVarResults)
            {
                if (!variables.Remove(set.Key)) continue;
                if (DebugExpressions)
                    Debug.Log($"Replacing custom variable: '{set.Key}' with value: {set.Value}");
                solvedVariables.Add(set.Key, set.Value);
            }

            //Check if we got them all, if not time to error out
            if (variables.Count > 0)
                throw new Exception($"Could not solve all variables in expression: {string.Join(", ", variables)}");

            // Replace variables with their values
            string parsedFormula = e;
            foreach (var set in solvedVariables)
                parsedFormula = parsedFormula.Replace($"[{set.Key}]", set.Value.ToString(), StringComparison.OrdinalIgnoreCase);

            T result = default;
            var expression = new NCalc.Expression(parsedFormula);

            //check if the parsed formula contains any operators or numbers using regex
            if (!Regex.IsMatch(parsedFormula, @"[+\-*/]"))
            {
                if (typeof(T) == typeof(string))
                    return (T)(object)parsedFormula;
            }


            //first if T is int and the string can be parsed as an int, return it
            if (typeof(T) == typeof(int) && int.TryParse(parsedFormula, out int intResult))
                return (T)(object)intResult;
            //try
            {
                //if(GameRules.DebugExpressions)
                //   Debug.Log($"Evaluating expression: {expression.OriginalExpression}, compare to {parsedFormula}");

                var evaluatedExpression = expression.Evaluate();
                if (evaluatedExpression is int i && typeof(T) == typeof(float))
                {
                    result = (T)(object)(float)i;
                }
                else
                {
                    switch (evaluatedExpression)
                    {
                        case Int32 ik when typeof(T) == typeof(float):
                            result = (T)(object)(float)ik;
                            break;
                        case Int32 ik when typeof(T) == typeof(int):
                            result = (T)(object)(int)ik;
                            break;
                        case double d when typeof(T) == typeof(float):
                            result = (T)(object)(float)d;
                            break;
                        case double d when typeof(T) == typeof(int):
                            result = (T)(object)(int)d;
                            break;
                        default:
                        {
                            //Debug.Log($" {evaluatedExpression} is not a float or int, but a {evaluatedExpression.GetType()}. Trying to cast to {typeof(T)}");
                            if (typeof(T) == typeof(string))
                                return (T)(object)(evaluatedExpression.ToString());
                            result = (T)evaluatedExpression;
                            break;
                        }
                    }
                }
            }
            /*
            catch(Exception exc)
            {
                Debug.LogError($"Failed to evaluate expression: {e} to type {typeof(T)}");
                Debug.LogError(exc);
            }*/
            return result;
        }

        private static Dictionary<string, float> ParseStats(HashSet<string> varSet, ICharacter user, ICharacter targ)
        {
            //Sample input:  [name.stat.key]            [Tech.TechName.Level] [Tech.TechName.Unlocked]
            Dictionary<string, float> solved = new Dictionary<string, float>();


            foreach (var variable in varSet)
            {
                var (scope, targetType, targetKey) = variable.Split('.').Select(x => x.ToLowerInvariant()).ToArray() switch { var arr => (arr[0], arr[1], arr[2]) };

                switch (scope)
                {
                    case "user":
                    {
                        EvaluateInScope(user);
                        break;
                    }
                    case "target":
                    {
                        EvaluateInScope(targ);
                        break;
                    }
                }

                continue;

                void EvaluateInScope(ICharacter character)
                {
                    switch (targetType)
                    {
                        case "level":
                        {
                            solved.Add(variable, character.Level);
                            break;
                        }
                        case "stats": //yeah yeah keep laughing
                        case "stat":
                        {
                            solved.Add(variable, user.GetStat(targetKey));
                            break;
                        }
                    }
                }
            }

            return solved;
        }

        private static HashSet<string> ExtractVariables(string input)
        {
            // Pattern to find substrings within square brackets
            string pattern = @"\[([^\]]+)\]";
            var matches = Regex.Matches(input, pattern);

            // Use a HashSet to avoid duplicates, with case-insensitive comparison
            HashSet<string> variables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (Match match in matches)
            {
                // Group 1 contains the text inside the brackets
                variables.Add(match.Groups[1].Value);
                if (DebugExpressions)
                    Debug.Log($"Found variable: {match.Groups[1].Value}");
            }

            return variables;
        }
    }
}