using System.Collections.Generic;
using System.Linq;

namespace CoreLib.Utilities
{
    public class ListSet<T>
    {
        private List<T> internalList = new List<T>();
        private HashSet<T> internalSet = new HashSet<T>();

        public void Add(T item)
        {
            if (!internalSet.Contains(item))
            {
                internalList.Add(item);
                internalSet.Add(item);
            }
        }
        
        public bool Remove(T item)
        {
            if (!internalSet.Contains(item)) return false;
            internalList.Remove(item);
            internalSet.Remove(item);
            return true;
        }

        public int Count => internalList.Count;
        public int GetIndexOf(T item) => internalList.IndexOf(item);
        public List<T> GetAllHigherThan(T item) => internalList.GetRange(GetIndexOf(item) + 1, internalList.Count - GetIndexOf(item) - 1);
        public T GetLast() => internalList[^1];
        public bool Any() => internalList.Any();
        public bool Contains(T item) => internalSet.Contains(item);

        public IEnumerable<T> Items => internalList;
        
        //Get readonly list and set
        public IReadOnlyList<T> List => internalList;
        public IReadOnlyCollection<T> Set => internalSet;
    }
}

