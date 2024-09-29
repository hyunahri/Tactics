using System.IO;
using UnityEngine;
using Color = System.Drawing.Color;

namespace CoreLib
{

/*
 * For constants and other statics used by infrastructure classes.
 */
    public static class Standards
    {
        public static Color Log_Color_Base = Color.Beige;
        public static Color Log_Color_Event = Color.Aquamarine;
        public static Color Log_Color_Warning = Color.Yellow;
        public static Color Log_Color_Error = Color.Red;

        //Base Paths, extend these in a separate Standards class for your specific project.
        private static string streamingAssetsPath = Application.streamingAssetsPath;
        public static string Log_BasePath => Path.Combine(streamingAssetsPath, "Logs");
        public static string Log_Path => Path.Combine(Log_BasePath, "Debug_Log.txt");

    }
}