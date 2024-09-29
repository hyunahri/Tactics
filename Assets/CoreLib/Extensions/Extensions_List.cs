using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CoreLib.Extensions
{
    /// <summary>
    /// Extension methods for generic lists.
    /// </summary>
    public static class Extensions_List
    {

        /// <summary>
        /// Adds an item to a list in sorted order. The list items must implement
        /// IComparable.
        /// </summary>
        /// <param name="this">This list.</param>
        /// <param name="item">Item to add.</param>
        /// <typeparam name="T">The list type.</typeparam>
        public static void AddSorted<T>(this List<T> @this, T item) where T : IComparable<T>
        {
            if (@this.Count == 0)
            {
                @this.Add(item);
            }
            else if (@this[@this.Count - 1].CompareTo(item) <= 0)
            {
                @this.Add(item);
            }
            else if (@this[0].CompareTo(item) >= 0)
            {
                @this.Insert(0, item);
            }
            else
            {
                int index = @this.BinarySearch(item);
                if (index < 0)
                {
                    index = ~index;
                }

                @this.Insert(index, item);
            }
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            return source.GroupBy(keySelector).Select(g => g.First());
        }

        public static List<Transform> GetChildren(this Transform transform)
        {
            List<Transform> children = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                children.Add(transform.GetChild(i));
            }

            return children;
        }

        public static void ForEachReverse<T>(this List<T> list, Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            for (int i = list.Count - 1; i >= 0; i--)
            {
                action(list[i]);
            }
        }

        public static bool ContainsAll<T>(this IEnumerable<T> containingList, IEnumerable<T> lookupList)
        {
            return !lookupList.Except(containingList).Any();
        }


        public static string ToDebugString<T>(this List<T> list)
        {
            string result = "";
            foreach (var entry in list)
                result += $"{entry.ToString()}, ";
            return result;
        }

        public static T GetRandom<T>(this List<T> list)
        {
            if (!list.Any())
            {
                throw new InvalidOperationException("Cannot get a random item from an empty list.");
            }

            return list[RNG.rng.Next(0, list.Count)];
        }

        public static T GetRandom<T>(this IEnumerable<T> list)
        {
            var enumerable = list as T[] ?? list.ToArray();
            if (!enumerable.Any())
                throw new InvalidOperationException("Cannot get a random item from an empty enumerable.");
            return enumerable.ElementAt(RNG.rng.Next(0, enumerable.Count()));
        }

        public static void RandomizeList<T>(this List<T> list) => Shuffle(list);

        public static void Shuffle<T>(this List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                var k = RNG.rng.Next(i + 1);
                (list[k], list[i]) = (list[i], list[k]);
            }
        }

        //addrange for hashset
        public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                hashSet.Add(item);
            }
        }
    }


}