using Serilog;
using Sharpcord_bot_library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    [LogContext("test")]
    public class LoggerTest
    {
        [TestMethod]
        public void Context()
        {
            Logger.Init(true, new LoggerConfiguration().WriteTo.Console().MinimumLevel.Debug());
            Logger.Info("Info");
            Logger.Warning("Warning");
            Logger.Error("Message error");
            Logger.Error("Message: ", new Exception("dingus"));
            Logger.Error(new Exception("dingus"));
            Logger.Debug("Debug");
        }
    }
}
