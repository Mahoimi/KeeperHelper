using UnityEngine;
using System.Collections.Generic;

namespace KeeperHelper.Utils
{
	public class LogHandler
    {
        public static void RegisterLogHandler()
        {
#if DEBUG
            Application.logMessageReceived += LogHandle;
#endif
        }

        public static void UnregisterLogHandler()
        {
#if DEBUG
            Application.logMessageReceived -= LogHandle;
#endif
        }

        private static void LogHandle(string logString, string stackTrace, LogType type)
        {
            switch (type)
            {
                case LogType.Error:
                case LogType.Assert:
                case LogType.Exception:
                    #if UNITY_EDITOR
                        Debug.Break();
                    #endif

                    #if !UNITY_EDITOR && UNITY_ANDROID
                        Application.Quit();
                    #endif
                    break;
                default:
                    break;
            }
        }
	}
}