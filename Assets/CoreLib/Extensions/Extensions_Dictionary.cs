using System;
using System.Collections.Generic;
using System.Linq;
using CoreLib.Complex_Types;
using UnityEngine;

namespace CoreLib.Extensions
{

    /// <summary>
    /// Extension methods for generic dictionaries.
    /// </summary>
    public static class Extensions_Dictionary
    {

        /// <summary>
        /// Works like List.RemoveAll.
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="dictionary">Dictionary to remove entries from</param>
        /// <param name="match">Delegate to match keys</param>
        /// <returns>Number of entries removed</returns>
        public static int RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Predicate<TKey> match)
        {
            if (dictionary == null || match == null) return 0;
            var keysToRemove = dictionary.Keys.Where(k => match(k)).ToList();
            if (keysToRemove.Count > 0)
            {
                foreach (var key in keysToRemove)
                {
                    dictionary.Remove(key);
                }
            }
            return keysToRemove.Count;
        }
        
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<Pair<TKey, TValue>> collection)
        {
            if (dictionary == null || collection == null) return;
            foreach (var kvp in collection)
            {
                dictionary.Add(kvp.Item1, kvp.Item2);
            }
        }
        
        public static void Merge<T>(this Dictionary<T,int> dict, Dictionary<T,int> other)
        {
            foreach (var kvp in other)
            {
                if (dict.ContainsKey(kvp.Key))
                    dict[kvp.Key] += kvp.Value;
                else
                    dict.Add(kvp.Key, kvp.Value);
            }
        }
        
        public static void Deduct<T>(this Dictionary<T,int> dict, Dictionary<T,int> other)
        {
            foreach (var kvp in other)
            {
                if (dict.ContainsKey(kvp.Key))
                    dict[kvp.Key] -= kvp.Value;
            }
        }
        
        public static bool CanDeduct<T>(this Dictionary<T,int> dict, Dictionary<T,int> other)
        {
            foreach (var kvp in other)
            {
                if (dict.ContainsKey(kvp.Key) == false || dict[kvp.Key] < kvp.Value)
                    return false;
            }
            return true;
        }

        public static int MultiplesOf<T>(this Dictionary<T, int> dict, Dictionary<T, int> other)
        {
            int multiples = int.MaxValue;
            foreach (var kvp in other)
            {
                if (dict.ContainsKey(kvp.Key) == false)
                    return 0;
                multiples = Mathf.Min(multiples, dict[kvp.Key] / kvp.Value);
            }

            return multiples;
        }
        
        //Remove one from hashset using pattern match
        public static bool Remove<T>(this HashSet<T> set, Predicate<T> match)
        {
            if (set == null || match == null) return false;
            var itemToRemove = set.FirstOrDefault(i => match(i));
            if (itemToRemove != null)
            {
                set.Remove(itemToRemove);
                return true;
            }
            return false;
        }
    }

}
