using CoreLib.Events;
using CoreLib.Utilities;
using UnityEngine;

namespace CoreLib.Time
{
    public class Updater : MonoBehaviour
    {
        public static Updater Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                FLog.LogError("Multiple Updaters: " + Instance.gameObject.name + " and " + gameObject.name + "");
                Destroy(this);
            }
            if(transform.parent is null)
                DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        private void Update() => StaticEvents_Time.OnUpdate.Invoke();
        private void FixedUpdate() => StaticEvents_Time.OnFixedUpdate.Invoke();
        private void LateUpdate() => StaticEvents_Time.OnLateUpdate.Invoke();
    }
}