using UnityEngine;

namespace CoreLib.Extensions
{


    public static class Color32Extensions
    {
        public static Color32 RandomColor32(this Color32 color)
        {
            return new Color32(
                (byte)RNG.rng.Next(0, 256),
                (byte)RNG.rng.Next(0, 256),
                (byte)RNG.rng.Next(0, 256),
                255
            );
        }

        public static Color32 ToAlpha(this Color32 color, byte alpha)
        {
            return new Color32(color.r, color.g, color.b, alpha);
        }

        public static Color ToAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }

        public static Color32 GetColorFromIndex(this float colorIndex)
        {
            // Clamp the color index to a typical range of -0.4 to 1.6
            colorIndex = Mathf.Clamp(colorIndex, -0.4f, 1.6f);

            // Map the color index to a range from blue to red
            byte r, g, b;

            if (colorIndex < 0.0f)
            {
                // Blue range
                r = 0;
                g = (byte)(255 * (0.4f + colorIndex) / 0.4f); // More green as index increases
                b = 255;
            }
            else if (colorIndex < 0.3f)
            {
                // Blue to white range
                r = (byte)(255 * colorIndex / 0.3f);
                g = (byte)(255 * colorIndex / 0.3f);
                b = 255;
            }
            else if (colorIndex < 0.6f)
            {
                // White range
                r = 255;
                g = 255;
                b = (byte)(255 * (0.6f - colorIndex) / 0.3f);
            }
            else if (colorIndex < 1.0f)
            {
                // Yellow range
                r = 255;
                g = (byte)(255 * (1.0f - colorIndex) / 0.4f + 0.5f * 255);
                b = 0;
            }
            else
            {
                // Red range
                r = 255;
                g = (byte)(255 * (1.6f - colorIndex) / 0.6f);
                b = 0;
            }

            return new Color32(r, g, b, 255);
        }
    }
}