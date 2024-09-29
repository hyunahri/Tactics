using System;
using CoreLib.Complex_Types;
using CoreLib.Events;
using UnityEngine;

namespace CoreLib.Time
{
    
    //IF NOT TICKING, MAKE SURE YOU HAVE AN UPDATER IN SCENE

    public interface ITimer
    {
        public bool IHasExpired();
        public float IGetTimeRemaining();
        public float IGetTotalTime();
        public float IGetPercentTimeRemaining();
        public void IRefreshTimer();
        public void IAbortTimer();
        public CoreEvent IOnExecute { get; set; }
    }
    
    public class Timer : ITimer
    {
        //Helper class, calls a method after a set time.
        
        protected Action act;
        private float time { get; set; }
        private readonly float initTime;

        public void IAbortTimer()
        {
            StaticEvents_Time.OnUpdate.RemoveListener(OnUpdate);
        }

        public CoreEvent IOnExecute { get; set; } = new CoreEvent();

        private void OnUpdate()
        {
            time -= UnityEngine.Time.deltaTime;
//            Debug.Log(time);
            if(time <= 0f)
                Execute();
        }

        private void Execute()
        {
            StaticEvents_Time.OnUpdate.RemoveListener(OnUpdate);
            act?.Invoke();
            IOnExecute.Invoke();
        }
        
        public Timer(float _time, Action _act)
        {
            time = _time;
            initTime = _time;
            act = _act;
            StaticEvents_Time.OnUpdate.AddListener(OnUpdate);
            if(Updater.Instance is null)
                Debug.LogError("No Updater present, timer will not tick.");
        }

        public bool IHasExpired() => time <= 0f;

        public float IGetTimeRemaining() => time;

        public float IGetTotalTime() => initTime;

        public float IGetPercentTimeRemaining() => time / initTime;
        public void IRefreshTimer() => time = initTime;

    }
    
    public class Timer<T> : ITimer
    {
        //Helper class, calls a method after a set time.
        private Action<T> act;
        private T entity;
        public T GetEntity() => entity;
        
        private float time { get; set; }
        private readonly float initTime;
        
        public CoreEvent IOnExecute { get; set; } = new CoreEvent();

        public void IRefreshTimer() => time = initTime;
        public void IAbortTimer()
        {
            StaticEvents_Time.OnUpdate.RemoveListener(OnUpdate);
        }

        private void OnUpdate()
        {
            time -= UnityEngine.Time.deltaTime;
            if(time <= 0f)
                Execute();
        }

        private void Execute()
        {
            StaticEvents_Time.OnUpdate.RemoveListener(OnUpdate);
            act?.Invoke(entity);
            IOnExecute.Invoke();
        }   
        public bool IHasExpired() => time <= 0f;

        public float IGetTimeRemaining() => time;

        public float IGetTotalTime() => initTime;

        public float IGetPercentTimeRemaining() => time / initTime;
        
        public Timer (float _time, Action<T> _act, T _entity)
        {
            time = _time;
            initTime = _time;
            act = _act;
            entity = _entity;
            StaticEvents_Time.OnUpdate.AddListener(OnUpdate);
        }
    }
    
    public class Timer<T1, T2> : ITimer
    {
        //Helper class, calls a method after a set time.
        private Action<T1, T2> act;
        private T1 entity;
        public T1 GetEntity() => entity;
        private T2 entity2;
        
        private float time { get; set; }
        private readonly float initTime;
        
        public CoreEvent IOnExecute { get; set; } = new CoreEvent();

        public void IRefreshTimer() => time = initTime;
        public void IAbortTimer()
        {
            StaticEvents_Time.OnUpdate.RemoveListener(OnUpdate);
        }

        private void OnUpdate()
        {
            time -= UnityEngine.Time.deltaTime;
            if(time <= 0f)
                Execute();
        }

        private void Execute()
        {
            StaticEvents_Time.OnUpdate.RemoveListener(OnUpdate);
            act?.Invoke(entity, entity2);
            IOnExecute.Invoke();
        }   
        public bool IHasExpired() => time <= 0f;

        public float IGetTimeRemaining() => time;

        public float IGetTotalTime() => initTime;

        public float IGetPercentTimeRemaining() => time / initTime;
        
        public Timer (float _time, Action<T1,T2> _act, T1 _entity, T2 _entity2)
        {
            time = _time;
            initTime = _time;
            act = _act;
            entity = _entity;
            entity2 = _entity2;
            StaticEvents_Time.OnUpdate.AddListener(OnUpdate);
        }
    }
}