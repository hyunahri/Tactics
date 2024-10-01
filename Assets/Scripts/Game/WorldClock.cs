using System;
using CoreLib.Events;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Game
{
    public class WorldClock : MonoBehaviour
    {
        public static WorldClock Instance;
        
        public WorldClock()
        {
            Instance = this;
        }
        
        //Settings
        public bool IsPaused = false;
        public float TimeScale = 1f;
        public float DayLengthInSeconds = 15f; // * 5f; //5 Minutes real time per day, should extend to 10 at least later on
        float TimePerSecond => DayLengthInSeconds / 24f;
        
        //State
        public int CurrentDay = 1;
        public float CurrentTime = 12f;

        private void Update()
        {
            CurrentTime += Time.deltaTime * TimeScale / TimePerSecond;
            if (CurrentTime >= 24f)
                OnDay();
            StaticEvents_Time.OnTime.Invoke(CurrentTime);
        }

        private void OnDay()
        {
            CurrentTime = 0f;
            CurrentDay++;
            StaticEvents_Time.OnDay.Invoke();
        }
    }
}