using System.Collections.Generic;
using UnityEngine;

namespace CoreLib.Complex_Types
{
    public class GoPoolBudget : GoPool
    {
        private GameObject harbor;
        
        protected override void OnDeploy(GameObject go)
        {
            go.SetActive(true);
        }
        
        protected override void OnReturn(GameObject go)
        {
            go.transform.SetParent(harbor.transform);
            go.SetActive(false);
        }

        protected override void OnDestroy(GameObject go)
        {
            GameObject.Destroy(go);
        }

        protected override GameObject Create()
        {
            harbor ??= GameObject.Instantiate(new GameObject("Harbor: " + Prefab.name));
            var go = GameObject.Instantiate(Prefab, harbor.transform);
            go.SetActive(false);
            Debug.Log("created object");
            return go;        
        }


        public GoPoolBudget(GameObject prefab, int defaultCount, int maxCount = 5000) : base(prefab, defaultCount, maxCount)
        {
            
            harbor ??= GameObject.Instantiate(new GameObject("Harbor: " + prefab.name));
            Object.DontDestroyOnLoad(harbor);
            
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