using System;
using System.Collections.Generic;
using System.Linq;
using CoreLib.Utilities;

namespace CoreLib.Complex_Types
{
    public class StorageContainer_Dictionary<T>
    {
        /*
         * Wrapper for Dictionary, intended for storing game data
         */

        //Container
        protected readonly Dictionary<string, T> container = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);

        //Events
        public readonly CoreEvent<T> OnAdd = new CoreEvent<T>();

        //Accessors
        public virtual T Get(string key)
        {
            if (container.TryGetValue(key, out var result))
                return result;
            throw new Exception($"Invalid Key: '{key}', {typeof(T)}");
        }

        public virtual bool TryGet(string key, out T result)
        {
            return container.TryGetValue(key, out result);
        }

        public virtual void Register(string key, T obj)
        {
            if (obj is null)
            {
                FLog.LogError($"Null Object, key [{key}]");
                return;
            }

            if (!container.TryAdd(key, obj))
            {
                FLog.LogError($"Multiple entries with tag '{key}', {typeof(T)}");
            }
            else
                OnAdd.Invoke(obj);
        }

        //Utility
        public virtual List<T> GetAll() => container.Values.ToList();
        public virtual List<string> GetKeys() => container.Keys.ToList();
        public virtual void Clear() => container.Clear();
        public Type GetContainerType => typeof(T);
    }
}