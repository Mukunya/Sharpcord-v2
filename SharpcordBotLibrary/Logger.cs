using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public static class Logger
    {
        public static void Init(bool debug = false, LoggerConfiguration cfg = null!)
        {
            LoggerConfiguration cfg2 =
            new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + "/logs/log-" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt");
            if (debug)
            {
                cfg2.MinimumLevel.Debug();
            }
            else
            {
                cfg2.MinimumLevel.Information();
            }
            Log.Logger = (cfg??cfg2).CreateLogger();
            Log.Information($"ENVIRONMENT: {Environment.OSVersion.VersionString}, {Environment.ProcessorCount} core CPU, {Environment.MachineName}; Running as {Environment.UserName}.");
            Log.Debug("Debug messages enabled.");
            if (Environment.UserName == "root")
            {
                Log.Warning("Running as root! This poses several security risks. Any vulnerability in any module can be fatal to the entire system. To avoid these risks, run Sharpcord as a regular user with specific permissions.");
            }
            Log.Information($"Running Sharpcord version 2.0 by Mukunya");
        }

        public static void Shutdown() 
        {
            Log.Information("Shutting down logger");
            Log.CloseAndFlush();
        }
        [Obsolete]
        public static void Info(object? source, string message)
        {
            string prefix = "";
            if (source != null)
            {
                var attr = source.GetType().GetCustomAttribute<LogContextAttribute>();
                if (attr != null)
                {
                    prefix = $"[{attr.Context}] ";
                }
            }
            Log.Information(prefix + message);
        }
        public static void Info(string message)
        {
            StackTrace stackTrace = new StackTrace();
            Type t = stackTrace.GetFrame(1).GetMethod().DeclaringType;
            string prefix = "";
            if (t != null)
            {
                var attr = t.GetCustomAttribute<LogContextAttribute>();
                if (attr != null)
                {
                    prefix = $"[{attr.Context}] ";
                }
            }
            Log.Information(prefix + message);
        }
        [Obsolete]
        public static void Warning(object? source, string message)
        {
            string prefix = "";
            if (source != null)
            {
                var attr = source.GetType().GetCustomAttribute<LogContextAttribute>();
                if (attr != null)
                {
                    prefix = $"[{attr.Context}] ";
                }
            }
            Log.Warning(prefix + message);
        }
        public static void Warning(string message)
        {
            StackTrace stackTrace = new StackTrace();
            Type t = stackTrace.GetFrame(1).GetMethod().DeclaringType;
            string prefix = "";
            if (t != null)
            {
                var attr = t.GetCustomAttribute<LogContextAttribute>();
                if (attr != null)
                {
                    prefix = $"[{attr.Context}] ";
                }
            }
            Log.Warning(prefix + message);
        }
        [Obsolete]
        public static void Debug(object? source, string message)
        {
            string prefix = "";
            if (source != null)
            {
                var attr = source.GetType().GetCustomAttribute<LogContextAttribute>();
                if (attr != null)
                {
                    prefix = $"[{attr.Context}] ";
                }
            }
            Log.Debug(prefix + message);
        }
        public static void Debug(string message)
        {
            StackTrace stackTrace = new StackTrace();
            Type t = stackTrace.GetFrame(1).GetMethod().DeclaringType;
            string prefix = "";
            if (t != null)
            {
                var attr = t.GetCustomAttribute<LogContextAttribute>();
                if (attr != null)
                {
                    prefix = $"[{attr.Context}] ";
                }
            }
            Log.Debug(prefix + message);
        }
        [Obsolete]
        public static void Error(object? source, string message)
        {
            string prefix = "";
            if (source != null)
            {
                var attr = source.GetType().GetCustomAttribute<LogContextAttribute>();
                if (attr != null)
                {
                    prefix = $"[{attr.Context}] ";
                }
            }
            Log.Error(prefix + message);
        }
        private static void error(string message)
        {
            StackTrace stackTrace = new StackTrace();
            Type t = stackTrace.GetFrame(2).GetMethod().DeclaringType;
            string prefix = "";
            if (t != null)
            {
                var attr = t.GetCustomAttribute<LogContextAttribute>();
                if (attr != null)
                {
                    prefix = $"[{attr.Context}] ";
                }
            }
            Log.Error(prefix + message);
        }
        public static void Error(string message)
        {
            error(message);
        }
        [Obsolete]
        public static void Error(object? source, Exception ex)
        {
            Error(source, "Exception occured: " + ex.ToString());
        }
        public static void Error(string message,Exception ex)
        {
            error(message + ex.ToString());
        }
        public static void Error(Exception ex)
        {
            error("Exception occured: " + ex);
        }
    }
}
