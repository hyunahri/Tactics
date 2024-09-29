using System;
using System.Linq;

namespace CoreLib.Extensions
{
    public static class Extensions_Enum
    {
        public static TEnum RandomEnum<TEnum>(this TEnum enumType) where TEnum : Enum
        {
            Type type = typeof(TEnum);
            Array values = Enum.GetValues(type);
            lock (RNG.rng)
            {
                object value = values.GetValue(RNG.rng.Next(values.Length));
                return (TEnum)Enum.ToObject(type, value);
            }
        }
        
        public static int GetMaxValue<TEnum>(this TEnum enumType) where TEnum : Enum => Enum.GetValues(enumType.GetType()).Cast<int>().Max();
    }
}