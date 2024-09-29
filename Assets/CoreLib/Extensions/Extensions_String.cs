using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CoreLib.Utilities;
using Unity.Mathematics;
using UnityEngine;

namespace CoreLib.Extensions
{

    public static class Extensions_String
    {

        public static string ToEmpty(this string str) => "";
        public static string ToEmpty(this int i) => "";
        public static string ToEmpty(this decimal d) => "";
        public static string ToEmpty(this float f) => "";
        public static string ToEmpty(this double d) => "";
        public static string ToEmpty(this bool b) => "";
        public static string ToEmpty(this object o) => "";

        //Fetchers



        public static string ToPopulationString(this int i)
        {
            if (i < 10000)
                return i.ToString("#,0");
            if (i < 1000000) // 1 million
                return $"{(i / 1000f):#,0.#}K";
            if (i < 1000000000) // 1 billion
                return $"{(i / 1000000f):#,0.##}M";
            // default to returning in billions
            return $"{(i / 1000000000f):#,0.##}B";
        }





        public static string ToShieldString(this int width, int depth)
        {
            if (width == 0 || depth == 0)
                return "";

            StringBuilder stringBuilder = new StringBuilder();
            for (int y = depth - 1; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                {
                    string color = "#006FFF";
                    stringBuilder.AppendFormat("<color={0}>{1}</color>", color, "–");
                }

                stringBuilder.Append("\n");
            }

            return stringBuilder.ToString();
        }



        /// <summary>
        /// Gets points both on the border and in the interior of a rectangle.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static IEnumerable<int2> GetAllPoints(this Rectangle r)
        {
            for (var x = r.Left; x < r.Right; x++)
            {
                for (var y = r.Top; y < r.Bottom; y++)
                    yield return new int2(x, y);
            }
        }

        /// <summary>
        /// Gets the points on the border of a rectangle.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static IEnumerable<int2> GetBorderPoints(this Rectangle r)
        {
            for (var x = r.Left; x <= r.Right; x++)
            {
                if (x == r.Left || x == r.Right)
                {
                    // get left and right sides
                    for (var y = r.Top; y <= r.Bottom; y++)
                        yield return new int2(x, y);
                }
                else
                {
                    // just get top and bottom
                    yield return new int2(x, r.Top);
                    if (r.Top != r.Bottom)
                        yield return new int2(x, r.Bottom);
                }
            }
        }

        public static string Repeat(this string str, int count) => string.Concat(Enumerable.Repeat(str, count));

        public static TProp MinOrDefault<TItem, TProp>(this IEnumerable<TItem> stuff, Func<TItem, TProp> selector) => stuff.Select(selector).MinOrDefault();

        public static T MinOrDefault<T>(this IEnumerable<T> stuff)
        {
            if (!stuff.Any())
                return default(T);
            return stuff.Min();
        }

        /// <summary>
        /// Computes the Manhattan (4-way grid) distance between two points.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int ManhattanDistance(this int2 p, int2 target)
        {
            return Math.Abs(target.x - p.x) + Math.Abs(target.y - p.y);
        }

        public static T PickWeighted<T>(this IDictionary<T, double> src, PRNG prng = null)
        {
            var total = src.Sum(kvp => kvp.Value);
            double num;
            if (prng == null)
                num = RNG.rng.NextFloat(0.0, total);
            else
                num = prng.Next(total);
            double sofar = 0;
            foreach (var kvp in src)
            {
                sofar += kvp.Value;
                if (num < sofar)
                    return kvp.Key;
            }

            return default(T); // nothing to pick...
        }

        /// <summary>
        /// Removes points within a certain eight way distance of a certain point.
        /// </summary>
        /// <param name="points">The points to start with.</param>
        /// <param name="center">The point to block out.</param>
        /// <param name="distance">The distance to block out from the center.</param>
        /// <returns>The points that are left.</returns>
        public static IEnumerable<int2> BlockOut(this IEnumerable<int2> points, int2 center, int distance)
        {
            foreach (var p in points)
            {
                if (center.EightWayDistance(p) > distance)
                    yield return p;
            }
        }

        public static int EightWayDistance(this int2 p, int2 target)
        {
            var dx = Math.Abs(target.x - p.x);
            var dy = Math.Abs(target.y - p.y);
            return Math.Max(dx, dy);
        }

        public static Color32 ToColor(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return new Color32();
            string[] split = str.Split(DataPaths.PAIR_DELIMITER);
            byte r = byte.Parse(split[0]);
            byte g = byte.Parse(split[1]);
            byte b = byte.Parse(split[2]);
            return new Color32(r, g, b, 255);
        }

        public static T ToEnum<T>(this string str) where T : Enum
        {
            if (string.IsNullOrEmpty(str))
                return default;
            return (T)Enum.Parse(typeof(T), str, true);
        }

        public static string RemoveFirstAndLast(this string str)
        {
            try
            {
                return str.Substring(1, str.Length - 2);
            }
            catch
            {
                FLog.LogError($"Failed to trim {str}");
                return "";
            }
        }

        public static string Cleanup(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Remove all spaces
            string noSpaces = input.Replace(" ", "");

            // Set string to lower-invariant
            string lowerInvariant = noSpaces.ToLowerInvariant();

            // Remove any non-alphanumeric characters using a regex
            string alphanumericOnly = Regex.Replace(lowerInvariant, "[^a-zA-Z0-9]", "");

            return alphanumericOnly;
        }

        public static string AddSpaceBeforeCapitals(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            StringBuilder stringBuilder = new StringBuilder(input.Length + 10); // Rough estimate of additional space
            char[] characters = input.ToCharArray();

            stringBuilder.Append(characters[0]); // Append the first character

            for (int i = 1; i < characters.Length; i++)
            {
                if (char.IsUpper(characters[i]))
                {
                    stringBuilder.Append(' ');
                }

                stringBuilder.Append(characters[i]);
            }

            return stringBuilder.ToString();
        }

        public static string GetResourcesDescription(this Dictionary<string, int> resources)
        {
            var descriptions = new List<string>();

            foreach (var pair in resources)
            {
                descriptions.Add($"{pair.Value} {pair.Key}");
            }

            return string.Join(", ", descriptions);
        }

        public static string OnlyFirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1).ToLower();
            }
        }

    }
}