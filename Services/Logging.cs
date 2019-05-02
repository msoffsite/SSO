using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web;

namespace SSOService.Services
{

    /// <REMARKS>
    /// Logging utility: Use when Trace Listeners are not in the current context; message format is the responsibility of the caller
    /// Logging utility: May not have access to the Service App.config file; if logging from Http Modules current context is the IIS Process
    /// Documentation: External documentation should reflect the default key values for file path key, file name key, and file name
    /// Warning: If "logFilePath" is empty, Stream Writer defaults to System.AppDomain.CurrentDomain.BaseDirectory, this may create multiple log files.
    /// </REMARKS>
    internal class Log
    {
        private static bool AppendIfFileExists { get; } = true;
        private static string DefaultAppKeyFilePath { get; } = "LogFilePath";
        private static string DefaultAppKeyFileName { get; } = "LogFileName";
        private static string DefaultAppKeyFileSize { get; } = "LogFileSizeKiloBytes";
        private static string DefaultLogFileName { get; } = "PTSSSOService.log";
        private static object FileLock { get; } = new object();
        private static long DefaultLogSize { get; } = 128;
        private static short KiloFactor { get; } = 1000;

        private static string _exception = string.Empty;
        private static long _streamLength;

        internal static void WriteLine(string message, string pathName = null, string fileName = null) {

            string appSettingFileName = Config.GetAppSettingsValue(DefaultAppKeyFileName);
            string appSettingFilePath = Config.GetAppSettingsValue(DefaultAppKeyFilePath) ?? AppDomain.CurrentDomain.BaseDirectory;

            string logFileName = (string.IsNullOrEmpty(fileName)) ? ((string.IsNullOrEmpty(appSettingFileName)) ? DefaultLogFileName : appSettingFileName) : fileName;
            string logFilePath = (string.IsNullOrEmpty(pathName)) ? ((string.IsNullOrEmpty(appSettingFilePath)) ? string.Empty : appSettingFilePath) : pathName;
            long logFileSize = (long.TryParse(Config.GetAppSettingsValue(DefaultAppKeyFileSize), out logFileSize)) ? logFileSize : DefaultLogSize;
            string logName = $"{logFilePath}{logFileName}";

            lock (FileLock) {
                StreamWriter stream = null;
                try {
                    stream = new StreamWriter(logName, AppendIfFileExists) { AutoFlush = true };
                    stream.WriteLine(message); if (!string.IsNullOrEmpty(_exception)) { stream.WriteLine(_exception); _exception = string.Empty; }
                    _streamLength = stream.BaseStream.Length;
                }
                catch (IOException ex) {
                    _exception = ex.Message;
                } finally {
                    stream?.Dispose();
                    if (_streamLength > (logFileSize * KiloFactor)) File.Move(logName, $"{logFilePath}PTSSSO.{DateTime.Now.Ticks.ToString()}.log");
                }
            }
        }

        internal static bool Verbose {
            get
            {
                bool logVerbose = bool.TryParse(Config.GetAppSettingsValue("LogVerbose"), out bool tryParse);
                return (tryParse) && logVerbose;
            }
        }

        internal static string HttpLogHeader(MethodBase method = null)
        {
            return method?.DeclaringType != null ? $"{DateTime.Now.ToString(CultureInfo.InvariantCulture)}, {method?.DeclaringType.FullName}.{method?.Name}, {HttpContext.Current.Request.HttpMethod}, {HttpContext.Current.Request.Url}" : null;
        }

    }

    internal class Config
    {
        internal static string ServiceRequestToken => GetAppSettingsValue("ServiceRequestToken");

        internal static string GetAppSettingsValue(string key) {
            //Application settings from the host Web.Config; not the service App.config
            return System.Configuration.ConfigurationManager.AppSettings?.Get(key) ?? string.Empty;
        }
    }
}
