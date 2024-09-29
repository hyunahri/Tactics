using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CoreLib.Complex_Types
{
    [Serializable]
    public struct Pair<T1,T2>
    {
        public T1 Item1;
        public T2 Item2;

        public Pair(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }
    
    [Serializable]
    public struct ReversiblePair<T>
    {
        [JsonProperty]public T Item1;
        [JsonProperty]public T Item2;

        public ReversiblePair(T _key, T _value)
        {
            Item1 = _key;
            Item2 = _value;
        }

        public override bool Equals(object obj)
        {
            if (obj is ReversiblePair<T> other)
            {
                return (EqualityComparer<T>.Default.Equals(Item1, other.Item1) && EqualityComparer<T>.Default.Equals(Item2, other.Item2)) ||
                       (EqualityComparer<T>.Default.Equals(Item1, other.Item2) && EqualityComparer<T>.Default.Equals(Item2, other.Item1));
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash1 = Item1?.GetHashCode() ?? 0;
            int hash2 = Item2?.GetHashCode() ?? 0;

            // Use XOR to combine hash codes, ensuring that the order doesn't matter
            return hash1 ^ hash2;
        }

        public static bool operator ==(ReversiblePair<T> pair1, ReversiblePair<T> pair2)
        {
            return pair1.Equals(pair2);
        }

        public static bool operator !=(ReversiblePair<T> pair1, ReversiblePair<T> pair2)
        {
            return !pair1.Equals(pair2);
        }
    }

}