using Newtonsoft.Json;
using UnityEngine;

namespace CoreLib.Complex_Types
{
    [System.Serializable]
    public struct Monitored<T>
    {
        [JsonIgnore]public CoreEvent<T> OnChange;
        [JsonProperty]private T value;

        public void SetValue(T _value)
        {
            value = _value;
            OnChange.Invoke(value);
        }
        
        public T GetValue() => value;

        public Monitored(T _value)
        {
            OnChange = new CoreEvent<T>();
            value = _value;
        }

        public override string ToString() => value.ToString();
    }

    public struct MonitoredInt
    {
        [JsonIgnore]public CoreEvent<int> OnChange;
        [JsonProperty]private int value;
        
        public void SetValue(int _value)
        {
            value = _value;
            OnChange.Invoke(value);
        }
        
        public void AddValue(int _value)
        {
            value += _value;
            OnChange.Invoke(value);
        }
        
        public void SubtractValue(int _value)
        {
            value -= _value;
            OnChange.Invoke(value);
        }
        
        public void ClampValue(int min, int max)
        {
            value = Mathf.Clamp(value, min, max);
            OnChange.Invoke(value);
        }
        
        public int GetValue() => value;

        public MonitoredInt(int _value)
        {
            OnChange = new CoreEvent<int>();
            value = _value;
        }

        public override string ToString() => value.ToString();
    }

    public struct MonitoredKeyInt<T>
    {
        [JsonIgnore]public readonly CoreEvent<T,int> OnChange;
        [JsonProperty]private int value;
        [JsonProperty]private T key;
        
        public void SetValue(int _value)
        {
            value = _value;
            OnChange.Invoke(key,value);
        }
        
        public void AddValue(int _value)
        {
            value += _value;
            OnChange.Invoke(key,value);
        }
        
        public void SubtractValue(int _value)
        {
            value -= _value;
            OnChange.Invoke(key,value);
        }
        
        public void ClampValue(int min, int max)
        {
            value = Mathf.Clamp(value, min, max);
            OnChange.Invoke(key,value);
        }
        
        public int GetValue() => value;

        public MonitoredKeyInt(int _value, T _key)
        {
            OnChange = new CoreEvent<T,int>();
            value = _value;
            key = _key;
        }

        public override string ToString() => value.ToString();
    }

    [System.Serializable]
    public struct Monitored<T1, T2>
    {
        
        [JsonIgnore]public CoreEvent<T1, T2> OnChange;
        [JsonProperty]private T1 key;
        [JsonProperty]private T2 value;
        

        public void SetValue(T2 _value)
        {
            value = _value;
            OnChange.Invoke(key,value);
        }

        public T2 GetValue() => value;

        public Monitored(T1 _key, T2 _value)
        {
            key = _key;
            value = _value;
            OnChange = new CoreEvent<T1, T2>();
        }

    }
}