using System;
using UnityEngine;

namespace Mono
{
    /// <summary>
    /// Singleton to access player location
    /// </summary>
    public class PlayerMarker : MonoBehaviour
    {
        public static PlayerMarker Instance { get; private set; }

        private void OnEnable()
        {
            Instance = this;
        }

        private void OnDisable()
        {
            if (Instance == this)
                Instance = null;
        }
    }
}