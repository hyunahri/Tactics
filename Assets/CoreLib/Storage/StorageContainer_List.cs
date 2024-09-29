using System;
using System.Collections.Generic;

namespace CoreLib.Complex_Types
{

    public class StorageContainer_List<T>
    {
        /*
        * Wrapper for List, intended for storing game data
        */

        //Container
        protected readonly List<T> container = new List<T>();

        //Events

        public readonly CoreEvent<T> OnAdd = new CoreEvent<T>();
        public readonly CoreEvent<T> OnRemove = new CoreEvent<T>();

        //Accessors

        public virtual void Register(T obj)
        {
            container.Add(obj);
            OnAdd.Invoke(obj);
        }

        public virtual void Deregister(T obj)
        {
            container.Remove(obj);
            OnRemove.Invoke(obj);
        }

        //Utility

        public virtual List<T> GetAll() => container;
        public virtual void Clear() => container.Clear();
        public Type GetContainerType => typeof(T);
    }
}