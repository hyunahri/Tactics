using System;
using System.Collections.Generic;

namespace CoreLib.Complex_Types
{
    public class BiDict<T1, T2>
    {
        private readonly Dictionary<T1, T2> forward = new Dictionary<T1, T2>();
        private readonly Dictionary<T2, T1> reverse = new Dictionary<T2, T1>();

        // Add a new key-value pair to the dictionary
        public void Add(T1 key, T2 value)
        {
            if (forward.ContainsKey(key) || reverse.ContainsKey(value))
            {
                throw new ArgumentException("Duplicate key or value.");
            }

            forward[key] = value;
            reverse[value] = key;
        }

        // Remove a key-value pair by key
        public bool RemoveByKey(T1 key)
        {
            if (forward.TryGetValue(key, out T2 value))
            {
                forward.Remove(key);
                reverse.Remove(value);
                return true;
            }
            return false;
        }

        // Remove a key-value pair by value
        public bool RemoveByValue(T2 value)
        {
            if (reverse.TryGetValue(value, out T1 key))
            {
                reverse.Remove(value);
                forward.Remove(key);
                return true;
            }
            return false;
        }

        // Try to get the value associated with the specified key
        public bool TryGetValueByKey(T1 key, out T2 value)
        {
            return forward.TryGetValue(key, out value);
        }

        // Try to get the key associated with the specified value
        public bool TryGetKeyByValue(T2 value, out T1 key)
        {
            return reverse.TryGetValue(value, out key);
        }

        // Indexer to get the value by key
        public T2 this[T1 key]
        {
            get => forward[key];
            set => Add(key, value);
        }

        // Indexer to get the key by value
        public T1 this[T2 val]
        {
            get => reverse[val];
            set => Add(value, forward[value]); //double check
        }

        // Clear the dictionary
        public void Clear()
        {
            forward.Clear();
            reverse.Clear();
        }

        // Get all keys
        public IEnumerable<T1> Keys => forward.Keys;

        // Get all values
        public IEnumerable<T2> Values => reverse.Keys;
    }
}
