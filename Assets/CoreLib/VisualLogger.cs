using System;
using System.Collections;
using UnityEngine;

namespace CoreLib
{
    public class VisualLogger : MonoBehaviour
    {
        private int qsize = 8;  // number of messages to keep
        private Queue logQueue = new Queue();

        void Start() {
            DontDestroyOnLoad(gameObject);
        }

        void OnEnable() {
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable() {
            Application.logMessageReceived -= HandleLog;
        }

        void HandleLog(string logString, string stackTrace, LogType logType) {
        

            switch (logType)
            {
                case LogType.Error:
                    logQueue.Enqueue("[" + logType + "] : " + "<color=red>" + logString + "</color>");               
                    break;
                case LogType.Assert:
                    break;
                case LogType.Warning:
                    break;
                case LogType.Log:
                    logQueue.Enqueue("[" + logType + "] : " + logString);
                    break;
                case LogType.Exception:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logType), logType, null);
            }
        
            if (logType == LogType.Exception)
                logQueue.Enqueue(stackTrace);
            while (logQueue.Count > qsize)
                logQueue.Dequeue();
        }

        void OnGUI() {
            GUILayout.BeginArea(new Rect(Screen.width - 460, 0, 450, Screen.height));
            GUILayout.Label("\n" + string.Join("\n", logQueue.ToArray()));
            GUILayout.EndArea();
        }
    }
}