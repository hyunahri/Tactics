using System;
using System.Collections.Generic;
using UnityEngine;

namespace CoreLib.Complex_Types
{
    /// <summary> C# implementation of pythons defaultdict   </summary>
    public class DefaultDict<TKey, TVal> : Dictionary<TKey, TVal>, ISerializationCallbackReceiver
    {
        [SerializeField][SerializeReference]private List<KeyValuePair<TKey, TVal>> entries = new List<KeyValuePair<TKey, TVal>>();
        
        // Default constructor with optional equality comparer
        public DefaultDict(IEqualityComparer<TKey> comparer = null) : base(comparer ?? EqualityComparer<TKey>.Default)
        {
            if (typeof(TVal).IsClass)
                throw new ArgumentException("Missing default factory for ref type, use the other constructor");
        }
        
        //<summary> Constructor with default factory that accepts TKey and optional equality comparer
        public DefaultDict(Func<TKey, TVal> defaultFactory, IEqualityComparer<TKey> comparer = null) 
            : base(comparer ?? EqualityComparer<TKey>.Default)
        {
            this.defaultFactory = defaultFactory ?? throw new ArgumentNullException(nameof(defaultFactory));
        }
        
        //<summary> Constructor with default factory and optional equality comparer
        public DefaultDict(Func<TVal> defaultFactory, IEqualityComparer<TKey> comparer = null) 
            : this(_ => defaultFactory(), comparer)
        {
        }

        private readonly Func<TKey, TVal> defaultFactory;

        public new TVal this[TKey key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    this[key] = defaultFactory != null ? defaultFactory.Invoke(key) : default;
                }

                return base[key];
            }
            set => base[key] = value;
        }

        
        
        
        public void OnBeforeSerialize()
        {
            entries ??= new List<KeyValuePair<TKey, TVal>>();
            entries.Clear();
            foreach (var kvp in this)
            {
                entries.Add(new KeyValuePair<TKey, TVal>(kvp.Key, kvp.Value));
            }
        }

        public void OnAfterDeserialize()
        {
            entries ??= new List<KeyValuePair<TKey, TVal>>();
            Clear();
            foreach (var kvp in entries)
            {
                this[kvp.Key] = kvp.Value;
            }
            
        }
    }
}