using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CoreLib.Extensions
{
    public static class Extensions_Miscellaneous
    {
        

        public static float GetLongest(this Renderer ren)
        {
            float length = ren.bounds.size.z; //Length along z, used for normalizing size in window
            float lengthY = ren.bounds.size.y; //Length along z, used for normalizing size in window
            float lengthx = ren.bounds.size.x; //Length along z, used for normalizing size in window
            float comparator = new List<float>() {length, lengthY, lengthx}.OrderByDescending(x => x).First();
            return comparator;
        }
        
        public static string ToOrdinal(this int num)
        {
            if( num <= 0 ) return num.ToString();

            switch(num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }
    
            switch(num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }
        }
        
       
        
        public static Enum GetRandomEnumValue(this Type t)
        {
            return Enum.GetValues(t)          // get values from Type provided
                .OfType<Enum>()               // casts to Enum
                .OrderBy(e => Guid.NewGuid()) // mess with order of results
                .FirstOrDefault();            // take first item in result
        }
        

        

        
        public static void AddExclusive<T>(this List<T> list, T item)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
            }
        }

        
    }
}