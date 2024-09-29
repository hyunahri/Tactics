using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Range = CoreLib.Complex_Types.Range;

namespace CoreLib.Extensions
{
    public static class Extensions_Game
    {
        public static void DeleteChildren(this Transform t)
        {
            foreach(Transform child in t)
                GameObject.Destroy(child.gameObject);
        }
        
        public static int RollDice(this string diceString)
        {
            var range = new Range(diceString.ToLowerInvariant());
            return range.Roll();
        }
        
        public static Vector3 ToZ(this Vector3 vec, float z) => new Vector3(vec.x, vec.y, z);
        public static Vector3 ToX(this Vector3 vec, float x) => new Vector3(x, vec.y, vec.z);
        public static Vector3 ToY(this Vector3 vec, float y) => new Vector3(vec.x, y, vec.z);
        
        
        public static T GetRandomByWeight<T>(this IEnumerable<T> itemsEnumerable, Func<T, int> weightKey)
        {
            var items = itemsEnumerable.ToList();

            var totalWeight = items.Sum(x => weightKey(x));
            var randomWeightedIndex = RNG.rng.Next(totalWeight);
            var itemWeightedIndex = 0;
            foreach(var item in items)
            {
                itemWeightedIndex += weightKey(item);
                if(randomWeightedIndex < itemWeightedIndex)
                    return item;
            }
            throw new ArgumentException("Collection count and weights must be greater than 0");
        }

        public static TKey GetRandomByWeight<TKey>(this Dictionary<TKey, float> dict)
        {
            float totalWeight = dict.Values.Sum();
            var randomWeightedIndex = RNG.rng.NextFloat(0, totalWeight);
            var itemWeightedIndex = 0f;
            foreach(var kvp in dict)
            {
                itemWeightedIndex += kvp.Value;
                if(randomWeightedIndex < itemWeightedIndex)
                    return kvp.Key;
            }
            throw new ArgumentException("Collection count and weights must be greater than 0");
        }
        
        public static TKey GetRandomByWeight<TKey>(this Dictionary<TKey, int> dict)
        {
            var totalWeight = dict.Values.Sum();
            var randomWeightedIndex = RNG.rng.Next((int)totalWeight);
            var itemWeightedIndex = 0f;
            foreach(var kvp in dict)
            {
                itemWeightedIndex += kvp.Value;
                if(randomWeightedIndex < itemWeightedIndex)
                    return kvp.Key;
            }
            throw new ArgumentException("Collection count and weights must be greater than 0");
        }

        public static void AddValuesFrom(this Dictionary<string, int> target, Dictionary<string, int> source)
        {
            foreach (var kvp in source)
            {
                if (target.ContainsKey(kvp.Key))
                {
                    target[kvp.Key] += kvp.Value;
                }
                else
                {
                    target[kvp.Key] = kvp.Value;
                }
            }
        }
        
        
        public static List<T> CloneList<T>(this List<T> list)
        {
            var newList = new List<T>();
            foreach (var entry in list)
                newList.Add(entry);
            return newList;
        }
        
        public static string ColorString(this string str, string color) => $"<color={color}>{str}</color>";

        public static KeyCode ToKeycode(this int i)
        {
            {
                switch (i)
                {
                    case 1:
                        return KeyCode.Alpha1;
                    case 2:
                        return KeyCode.Alpha2;
                    case 3:
                        return KeyCode.Alpha3;
                    case 4:
                        return KeyCode.Alpha4;
                    case 5:
                        return KeyCode.Alpha5;
                    case 6:
                        return KeyCode.Alpha6;
                    case 7:
                        return KeyCode.Alpha7;
                    case 8:
                        return KeyCode.Alpha8;
                    case 9:
                        return KeyCode.Alpha9;
                    case 10:
                        return KeyCode.Alpha0;
                }
                return KeyCode.Ampersand;
            }
        }

        public static int ToLayermask(this int i)
        {
            return 1 << i;
        }


        
        public static T ToType<T>(this T i) => (T) i;

    

        public static Vector3 GetClosest(this List<Vector3> points, Vector3 source) => points.OrderBy(x => Vector3.Distance(x, source)).First();
        public static Transform GetClosest(this List<Transform> points, Vector3 source) => points.OrderBy(x => Vector3.Distance(x.position, source)).First();

    }

    
}