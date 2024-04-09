using System;
using System.Collections.Generic;
using UnityEngine;

namespace Illumate
{
    internal class ReporterData
    {
        internal readonly List<string> ignoredLogs = new();
        private readonly List<LogData> logs = new();
        private readonly Dictionary<string, string> stats = new();

        /// <summary>
        /// Logs a message to the stack
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        internal void Log(LogType logType, string message, string stackTrace = null)
        {
            if (ignoredLogs.Contains(message)) return;
            logs.Add(new LogData { Type = logType, Message = message, StackTrace = stackTrace });
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

        /// <summary>
        /// Generate text from the logs, stats and system info
        /// </summary>
        /// <returns></returns>
        internal string GenerateText()
        {
            string text = $"LOG STACK (v0.1)        Date: {DateTime.Now:dd/MM/yyyy HH:mm}\n" +
                $"Application: {Application.productName} by {Application.companyName} (v{Application.version})\n" +
                $"Unity Version: {Application.unityVersion}, {Application.systemLanguage}, Build GUID: {Application.buildGUID}," +
                $"Platform: {Application.platform}, Url: {Application.absoluteURL}";

            text += "\nSystem:\n";
            text += $"{SystemInfo.deviceModel}, {SystemInfo.deviceName}, {SystemInfo.deviceType}, {SystemInfo.deviceUniqueIdentifier} \n";
            text += $"{SystemInfo.graphicsDeviceName}, {SystemInfo.graphicsDeviceType}, {SystemInfo.graphicsDeviceVendor}, {SystemInfo.graphicsDeviceVendorID}, {SystemInfo.graphicsDeviceVersion}, {SystemInfo.graphicsMemorySize}, {SystemInfo.graphicsShaderLevel} \n";
            text += $"{SystemInfo.operatingSystem}, {SystemInfo.processorCount}, {SystemInfo.processorType}, {SystemInfo.systemMemorySize} \n";

            if (stats.Count > 0)
            {
                text += "----------------------------------------\n";
                text += "\nStats\n";
                foreach (var stat in stats)
                    text += $"{stat.Key}: {stat.Value}\n";
            }
            else
            {
                text += "No stats\n";
            }

            if (logs.Count > 0)
            {
                text += "----------------------------------------\n";
                text += "\nLogs\n";
                foreach (var log in logs)
                {
                    text += $"{log.Type}: {log.Message}\n";
                    string stackTrace = log.StackTrace != null ? log.StackTrace.Trim() : "";
                    if (!string.IsNullOrEmpty(stackTrace))
                    {
                        // TODO: Add stack trace
                    }
                }
            }
            else
            {
                text += "No logs\n";
            }
            return text;
        }



        private class LogData
        {
            public LogType Type;
            public string Message;
            public string StackTrace;
        }
    }

}