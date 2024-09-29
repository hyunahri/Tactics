using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CoreLib.Complex_Types
{
    public class GoPool
    {
        private Transform PoolParent;
        
        protected GameObject Prefab;
        protected ObjectPoolBacking<GameObject> Pool;

        public virtual GameObject Get() => Pool.Get();
        public virtual void Release(GameObject go)
        {
            if (go == null)
            {
                Debug.LogError("Releasing null object");
                return;
            }

            Pool.Release(go);
        }

        public int GetCountInPool() => Pool.CountInactive;
        
        protected virtual GameObject Create()
        {
            var go = GameObject.Instantiate(Prefab, PoolParent);
            foreach(IPoolable poolable in GetPoolables(go))
                poolable.OnPoolCreate();
            go.SetActive(false);
            return go;
        }

        protected virtual void OnDeploy(GameObject go)
        {
            go.transform.SetParent(PoolParent);
            go.SetActive(true);
            go.transform.lossyScale.Set(1,1,1);
            foreach(IPoolable poolable in GetPoolables(go))
                poolable.OnPoolDeploy();
        }

        protected virtual void OnReturn(GameObject go)
        {
            go.transform.SetParent(PoolParent);
            go.SetActive(false);
            foreach(IPoolable poolable in GetPoolables(go))
                poolable.OnPoolReturn();
        }

        protected virtual void OnDestroy(GameObject go)
        {
            foreach(IPoolable poolable in GetPoolables(go))
                poolable.OnPoolDestroy();
            GameObject.Destroy(go);
        }

        public void Dispose()
        {
            GameObject.Destroy(PoolParent.gameObject);
        }

        List<IPoolable> GetPoolables(GameObject go) => go.GetComponentsInChildren<IPoolable>().ToList();
        
        public GoPool(GameObject prefab, int defaultCount, int maxCount = 5000)
        {
            var go = new GameObject("Pool: " + prefab.name);
            GameObject.DontDestroyOnLoad(go);
            PoolParent = go.transform;
            Prefab = prefab;
            Pool = new ObjectPoolBacking<GameObject>(Create, OnDeploy, OnReturn, OnDestroy, true, defaultCount, maxCount);
            var buffer = new List<GameObject>();
            for (int i = 0; i < defaultCount; i++)
            {
                buffer.Add(Pool.Get());
            }
            
            foreach (var gameObject in buffer)
            {
                Pool.Release(gameObject);
            }
        }
        
    }
}