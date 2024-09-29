using System;
using System.Text;
using UnityEngine;

namespace CoreLib.Extensions
{
    public static class Extensions_Int
    {
        private static readonly (int Value, string Numeral)[] RomanNumerals = new[]
        {
            (1000, "M"), (900, "CM"), (500, "D"), (400, "CD"),
            (100, "C"), (90, "XC"), (50, "L"), (40, "XL"),
            (10, "X"), (9, "IX"), (5, "V"), (4, "IV"),
            (1, "I")
        };

        
        public static string ToRoman(this int number)
        {
            if (number is < 1 or > 3999)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "Value must be in the range 1-3999.");
            }

            var result = new StringBuilder();
        
            foreach (var (value, numeral) in RomanNumerals)
            {
                while (number >= value)
                {
                    result.Append(numeral);
                    number -= value;
                }
            }

            return result.ToString();
        }
        
        
        public static string ToDirectionString(this Vector2 direction)
        {
            // Normalize the input direction
            Vector2 normalizedDir = direction.normalized;

            // Define the angle thresholds
            float angle = Mathf.Atan2(normalizedDir.y, normalizedDir.x) * Mathf.Rad2Deg;
            angle = (angle + 360) % 360; // Normalize the angle to 0-360 degrees

            // Determine the direction based on the angle
            if (angle >= 337.5f || angle < 22.5f)
                return "E"; // 0 degrees
            else if (angle >= 22.5f && angle < 67.5f)
                return "NE"; // 45 degrees
            else if (angle >= 67.5f && angle < 112.5f)
                return "N"; // 90 degrees
            else if (angle >= 112.5f && angle < 157.5f)
                return "NW"; // 135 degrees
            else if (angle >= 157.5f && angle < 202.5f)
                return "W"; // 180 degrees
            else if (angle >= 202.5f && angle < 247.5f)
                return "SW"; // 225 degrees
            else if (angle >= 247.5f && angle < 292.5f)
                return "S"; // 270 degrees
            else if (angle >= 292.5f && angle < 337.5f)
                return "SE"; // 315 degrees

            return "O";
        }
    }

}