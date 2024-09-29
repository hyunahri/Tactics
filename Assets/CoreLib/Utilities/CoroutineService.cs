using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CoreLib.Utilities
{
    public static class CoroutineService
    {   /// <summary>
        /// Manages coroutines and provides opportunity for callbacks.
        /// Allows calling coroutines from static methods.
        /// </summary>
    
        private class CoroutineRunner : MonoBehaviour
        {
            public static CoroutineRunner Instance;
            private void Awake() => Instance = this;
        }
    
        public static Coroutine RunCoroutine(IEnumerator coroutine, Action callback = null)
        {
            if(CoroutineRunner.Instance == null)
            {
                GameObject go = new GameObject("CoroutineRunner");
                go.tag = "CoroutineRunner";
                Object.DontDestroyOnLoad(go);
                CoroutineRunner.Instance = go.AddComponent<CoroutineRunner>();
            }
            if(CoroutineRunner.Instance == null)
                Debug.LogError($"CoroutineRunner.Instance is null.");
       
            if(coroutine == null)
                Debug.LogError($"coroutine is null.");
            var wrapper = Wrapper(coroutine, callback);
            if(wrapper == null)
                Debug.LogError($"wrapper is null.");
       
            return CoroutineRunner.Instance.StartCoroutine(wrapper);
        }

        private static IEnumerator Wrapper(IEnumerator coroutine, Action callback)
        {
            yield return CoroutineRunner.Instance.StartCoroutine(coroutine);
            callback?.Invoke();
        }
    
        public static void StopCoroutine(Coroutine coroutine)
        {
            if (CoroutineRunner.Instance == null)
            {
                Debug.LogError($"CoroutineRunner.Instance is null.");
                return;
            }

            if(coroutine == null)
                Debug.LogError($"coroutine is null.");
            CoroutineRunner.Instance?.StopCoroutine(coroutine);
        }
    }
}