using System;
using System.Collections.Generic;
using UnityEngine;

namespace Illumate
{
    internal class ReporterData
    {
        public readonly string Version = "0.2";
        public readonly ApplicationData applicationData = new();
        public readonly SystemData systemData = new();
        public readonly List<LogLine> logs = new();
        public readonly Dictionary<string, string> stats = new();



        /// <summary>
        /// Logs a message to the stack
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        internal void Log(LogType logType, string message, string stackTrace = null)
        {
            logs.Add(new LogLine { Type = logType, Message = message});
        }

        /// <summary>
        /// Set a stat
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        internal void SetStat(string key, string value)
        {
            if (stats.ContainsKey(key))
                stats[key] = value;
            else
                stats.Add(key, value);
        }

        public class LogLine
        {
            public LogType Type;
            public string Message;
            //public string StackTrace;
            public DateTime Time = DateTime.Now;
        }

        public class ApplicationData
        {
            public string productName = Application.productName;
            public string companyName = Application.companyName;
            public string version = Application.version;
            public string unityVersion = Application.unityVersion;
            public string buildGUID = Application.buildGUID;
            public string identifier = Application.identifier;
            public string absoluteURL = Application.absoluteURL;
            public string targetFrameRate = Application.targetFrameRate.ToString();
            public string genuine = Application.genuine.ToString();
            public string genuineCheckAvailable = Application.genuineCheckAvailable.ToString();
            public string installMode = Application.installMode.ToString();
            public string sandboxType = Application.sandboxType.ToString();
            public string internetReachability = Application.internetReachability.ToString();
            public string isMobilePlatform = Application.isMobilePlatform.ToString();
            public string isConsolePlatform = Application.isConsolePlatform.ToString();
            public string isEditor = Application.isEditor.ToString();
            public string isBatchMode = Application.isBatchMode.ToString();
            public string backgroundLoadingPriority = Application.backgroundLoadingPriority.ToString();
        }

        public class SystemData
        {
            public string systemLanguage = Application.systemLanguage.ToString();
            public string platform = Application.platform.ToString();
            public string dataPath = Application.dataPath;
            public string persistentDataPath = Application.persistentDataPath;
            public string streamingAssetsPath = Application.streamingAssetsPath;
            public string temporaryCachePath = Application.temporaryCachePath;
            public string consoleLogPath = Application.consoleLogPath;
            public string systemMemorySize = SystemInfo.systemMemorySize.ToString();
            public string processorType = SystemInfo.processorType;
            public string processorCount = SystemInfo.processorCount.ToString();
            public string operatingSystem = SystemInfo.operatingSystem;
            public string graphicsDeviceName = SystemInfo.graphicsDeviceName;
            public string graphicsDeviceType = SystemInfo.graphicsDeviceType.ToString();
            public string graphicsDeviceVendor = SystemInfo.graphicsDeviceVendor;
            public string graphicsDeviceVendorID = SystemInfo.graphicsDeviceVendorID.ToString();
            public string graphicsDeviceVersion = SystemInfo.graphicsDeviceVersion;
            public string graphicsMemorySize = SystemInfo.graphicsMemorySize.ToString();
            public string graphicsShaderLevel = SystemInfo.graphicsShaderLevel.ToString();
            public string deviceModel = SystemInfo.deviceModel;
            public string deviceName = SystemInfo.deviceName;
            public string deviceType = SystemInfo.deviceType.ToString();
            public string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
        }
    }

}