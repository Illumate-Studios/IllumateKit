using Illumate.Kit;
using System;
using UnityEngine;

namespace Illumate
{
    public class Reporter : MonoBehaviour
    {
        #region Private Singleton
        private static Reporter _instance;
        private static Reporter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<Reporter>();
                    if (_instance == null)
                        _instance = new GameObject(nameof(Reporter)).AddComponent<Reporter>();
                }
                return _instance;
            }
        }
        #endregion

        private readonly ReporterData logStackData = new();

        public static void Log(string message, UnityEngine.Object context = null) => Log(LogType.Log, message, context);
        public static void LogWarning(string message, UnityEngine.Object context = null) => Log(LogType.Warning, message, context);
        public static void LogError(string message, UnityEngine.Object context = null) => Log(LogType.Error, message, context);
        public static void SetStat(string key, string value) => Instance.logStackData.SetStat(key, value);
        public static string GenerateReport() => Instance.logStackData.GenerateText();


        private void Awake()
        {
            //Application.lowMemory += Event_LowMemory;
            //Application.logMessageReceived += Event_LogMessageReceived;
            //// Application.logMessageReceivedThreaded += Application_logMessageReceivedThreaded; // https://docs.unity3d.com/ScriptReference/Application-logMessageReceivedThreaded.html
            //UnityEngine.ResourceManagement.ResourceManager.ExceptionHandler = OnAddressableExceptionHandle;
        }

        private static void Log(LogType type, string message, string stack = null)
        {
            if (Instance.IsIgnoredMessage(type, message)) return;

            message ??= "";
            message.Trim();

            stack ??= "";
            stack = stack.Trim();

            if (type != LogType.Log)
                if (stack == null || stack == "")
                    stack = new System.Diagnostics.StackTrace().ToString().Trim();

#if UNITY_EDITOR
            SendConsoleInfo(type, message);
#endif

            Instance.logStackData.Log(type, message, type == LogType.Log ? null : stack);
        }

        private static void Log(LogType type, string message, UnityEngine.Object context, string stack = null)
        {
            if (Instance.IsIgnoredMessage(type, message)) return;

            message ??= "";
            message.Trim();

            stack ??= "";
            stack = stack.Trim();

            if (type != LogType.Log)
                if (stack == null || stack == "")
                    stack = new System.Diagnostics.StackTrace().ToString().Trim();

#if UNITY_EDITOR
            SendConsoleInfo(type, message);
#endif

            Instance.logStackData.Log(type, message, type == LogType.Log ? null : stack);
        }

        private void Event_LogMessageReceived(string condition, string stackTrace, LogType type)
        {
            throw new NotImplementedException();
        }

        private void Event_LowMemory()
        {
            throw new NotImplementedException();
        }



        private void OnDestroy()
        {
            if(!Application.isPlaying)
                Debug.Log("LogStack destroyed. LogStack: " + logStackData.GenerateText());
        }

        private static void SendConsoleInfo(LogType type, string message)
        {
            string text = ColorizeHex("REPORTER: ", "7fd6fd");
            text += Colorize(type.ToString(), type switch
            {
                LogType.Error => Color.red,
                LogType.Exception => Color.red,
                LogType.Assert => Color.red,
                LogType.Warning => Color.yellow,
                LogType.Log => Color.white,
                _ => Color.red
            });
            text += " => " + message;
            Debug.Log(text);

            string Colorize(string text, Color color) => ColorizeHex(text, ColorUtility.ToHtmlStringRGB(color));
            string ColorizeHex(string text, string colorHex) => $"<color={colorHex}>{text}</color>";
        }

        private bool IsIgnoredMessage(LogType logType, string message)
        {
            foreach (var ignoredMessage in IllumateConfigSC.Instance.reporterSettings.ignoredMessages)
                if (ignoredMessage.IsMatch(logType, message))
                    return true;
            return false;
        }



        [Serializable]
        public struct LogMatchData
        {
            public LogType logType;
            public StringCheckType checkType;
            public string message;

            public readonly bool IsMatch(LogType logType, string message)
            {
                if (this.logType != logType) return false;
                if (checkType == StringCheckType.StartsWith && message.StartsWith(this.message))
                    return true;
                if (checkType == StringCheckType.Contains && message.Contains(this.message))
                    return true;
                if (checkType == StringCheckType.EndsWith && message.EndsWith(this.message))
                    return true;
                return false;
            }
        }

        public enum StringCheckType { StartsWith, Contains, EndsWith }
    }

}