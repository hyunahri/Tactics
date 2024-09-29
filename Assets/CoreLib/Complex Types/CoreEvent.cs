using System;
using CoreLib.Utilities;

namespace CoreLib.Complex_Types
{
    public class CoreEvent
    {
        protected string eventName;
        public bool isVerbose;

        public CoreEvent(string _name = "Unnamed Event", bool _isVerbose = false)
        {
            eventName = _name;
            isVerbose = _isVerbose;
        }

        public void Invoke()
        {
            if (Event is null)
                FLog.LogError($"Null Event: {eventName}");
            if (isVerbose)
                Vocalize();
            Event?.Invoke();
        }

        public void AddListener(Action del) => Event += del;
        public void RemoveListener(Action del) => Event -= del;
        public void ClearListeners() => Event = delegate { };

        protected event Action Event = delegate { };
        private void Vocalize() => FLog.Log($"{eventName} invoked.");

    }

    public class CoreEvent<T1>
    {
        protected string eventName;
        public bool isVerbose = true;

        public CoreEvent(string name = "Unnamed Event", bool _isVerbose = false)
        {
            eventName = name;
            isVerbose = _isVerbose;
        }

        public void Invoke(T1 input)
        {
            if (Event is null)
                FLog.LogError($"Null Event: {eventName}");
            if (isVerbose)
                Vocalize();
            Event?.Invoke(input);
        }

        public void AddListener(Action<T1> del) => Event += del;
        public void RemoveListener(Action<T1> del) => Event -= del;
        public void ClearListeners() => Event = delegate { };

        protected event Action<T1> Event = delegate(T1 obj) { };
        private void Vocalize() => FLog.Log($"{eventName} invoked.");
    }

    public class CoreEvent<T1, T2>
    {
        protected string eventName;
        public bool isVerbose = true;

        public CoreEvent(string name = "Unnamed Event", bool _isVerbose = false)
        {
            eventName = name;
            isVerbose = _isVerbose;
        }

        public void Invoke(T1 input1, T2 input2)
        {
            if (Event is null)
                FLog.LogError($"Null Event: {eventName}");
            if (isVerbose)
                Vocalize();
            Event?.Invoke(input1, input2);
        }

        public void AddListener(Action<T1, T2> del) => Event += del;
        public void RemoveListener(Action<T1, T2> del) => Event -= del;
        public void ClearListeners() => Event = delegate { };

        protected event Action<T1, T2> Event = delegate(T1 arg1, T2 arg2) { };
        private void Vocalize() => FLog.Log($"{eventName} invoked.");
    }

    public class CoreEvent<T1, T2, T3>
    {
        protected string eventName;
        public bool isVerbose = true;

        public CoreEvent(string name = "Unnamed Event", bool _isVerbose = false)
        {
            eventName = name;
            isVerbose = _isVerbose;
        }

        public void Invoke(T1 input1, T2 input2, T3 input3)
        {
            if (Event is null)
                FLog.LogError($"Null Event: {eventName}");
            if (isVerbose)
                Vocalize();
            Event?.Invoke(input1, input2, input3);
        }

        public void AddListener(Action<T1, T2, T3> del) => Event += del;
        public void RemoveListener(Action<T1, T2, T3> del) => Event -= del;
        public void ClearListeners() => Event = delegate { };

        protected event Action<T1, T2, T3> Event = delegate(T1 arg1, T2 arg2, T3 arg3) { };
        private void Vocalize() => FLog.Log($"{eventName} invoked.");
    }
}