using System.Collections.Generic;
using System.Linq;

namespace CoreLib.Complex_Types
{
    public class Map<T1, T2>
    {
        protected readonly Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();
        protected readonly Dictionary<T2, T1> _reverse = new Dictionary<T2, T1>();

        public List<T2> GetValues() => _forward.Values.ToList();
        public List<T1> GetKeys() => _forward.Keys.ToList();

        public Map()
        {
            this.Forward = new Indexer<T1, T2>(_forward);
            this.Reverse = new Indexer<T2, T1>(_reverse);
        }

        public class Indexer<T3, T4>
        {
            private Dictionary<T3, T4> _dictionary;
            public Indexer(Dictionary<T3, T4> dictionary)
            {
                _dictionary = dictionary;
            }
            public T4 this[T3 index]
            {
                get { return _dictionary[index]; }
                set { _dictionary[index] = value; }
            }
        }

        public void Add(T1 t1, T2 t2)
        {
            _forward.Add(t1, t2);
            _reverse.Add(t2, t1);
        }

        public void Clear()
        {
            _forward.Clear();
            _reverse.Clear();
        }

        public T1 Get(T2 key) => _reverse[key];
        public T2 Get(T1 key) => _forward[key];

        public Indexer<T1, T2> Forward { get; protected set; }
        public Indexer<T2, T1> Reverse { get; protected set; }
    }
}