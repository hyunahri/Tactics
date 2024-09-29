using System;
using System.IO;
using UnityEngine;
using Color = System.Drawing.Color;

namespace CoreLib.Utilities
{

/*
 * Simplified Debugging system.
 */
    public static class FLog
    {
        private static bool init = false;
        private static int lineCount = 0;

        private static string path => Standards.Log_Path;

        public static void ModLog(string mod, string log) //Use with Modules
        {
            Debug.Log(log);
            LogToFile(log, mod);
        }

        public static void Log(string log, bool writeToFile = true, bool suppressUnity = false)
        {
            if(!suppressUnity)
                Debug.Log(log);
            if(writeToFile)
                LogToFile(log);
        }
        
        public static void LogWarning(string log, bool suppressUnity = false)
        {
            if(!suppressUnity)
                Debug.LogWarning(log);
            LogToFile(log);
        }

        public static void LogError(string log, bool suppressUnity = false)
        {
            if(!suppressUnity)
                Debug.LogError(log);
            LogToFile(log);
        }
        
        public static void LogException(Exception exception)
        {
            Debug.LogError("<color=red>EXCEPTION:</color> " + exception.Message);
            LogToFile(exception.Message);
        }

        #region TODO
        
        [Obsolete]
        public static void Log(string log, Color color)
        {
            Debug.Log(log);
            LogToFile(log);
        }

        [Obsolete]
        public static void LogEvent(string log)
        {
            Debug.Log(log);
            LogToFile(log);
        }

        #endregion
        

        private static readonly object lockObj = new object();
        private static void LogToFile(string log, string mod = "")
        {
            lock (lockObj)
            {
                if (!init)
                    SetupLog();
                try
                {
                    using StreamWriter writer = new StreamWriter(path, true);
                    writer.WriteLine($"{lineCount}. [{DateTime.Now.ToShortTimeString()}]\t{log}");
                    lineCount++;
                }
                catch
                {
                    Debug.LogError("Logger Write Fail");
                }
            }
        }


        private static void SetupLog()
        {
            init = true;
            Directory.CreateDirectory(Standards.Log_BasePath);
            using StreamWriter writer = new StreamWriter(path, false); //Clears existing file
            writer.WriteLine($"");
            writer.Close();
            
            LogToFile($"{DateTime.Now.ToShortTimeString()}\n------ S T A R T ------");
        }
    }
}