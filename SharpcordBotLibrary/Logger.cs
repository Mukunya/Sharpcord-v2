using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public static class Logger
    {
        public static void Init(bool debug = false)
        {
            LoggerConfiguration cfg =
            new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + "/logs/log-" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt");
            if (debug)
            {
                cfg.MinimumLevel.Debug();
            }
            else
            {
                cfg.MinimumLevel.Information();
            }
            Log.Logger = cfg.CreateLogger();
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
        public static void Error(object? source, Exception ex)
        {
            Error(source, "Exception occured" + ex.ToString());
        }
    }

    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct|AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    sealed class LogContextAttribute : Attribute
    {
        readonly string context;

        public LogContextAttribute(string context)
        {
            this.context = context;
        }

        public string Context
        {
            get { return context; }
        }

    }
}
