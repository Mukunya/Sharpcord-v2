using Serilog;
using Sharpcord_bot_library;
using System.Reflection;

List<Tuple<Assembly, Type>> LoadedBots = new List<Tuple<Assembly, Type>>();
List<Bot> BotInstances = new List<Bot>();

Logger.Init(args.Contains("-D"));
AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
Logger.Info("Starting, searching for bots");
if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/modules"))
{
    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/modules");
}
string[] botdlls = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "/modules");
foreach (string botdll in botdlls)
{
    if (botdll.EndsWith(".dll"))
    {
        Assembly dll = Assembly.LoadFile(botdll);
        foreach (var type in dll.GetExportedTypes())
        {
            if (type.IsDefined(typeof(DiscordBotAttribute), false))
            {
                LoadedBots.Add(new(dll, type));
                Logger.Info($"Loaded {type.GetCustomAttribute<DiscordBotAttribute>()?.DisplayName}, a bot with type {type.GetCustomAttribute<DiscordBotAttribute>()?.Type}");
            }
        }
    }
}
Logger.Info($"Loading process done, loaded {LoadedBots.Count} modules.");
Logger.Info("Starting bots...");
foreach (var item in LoadedBots)
{
    try
    {
        Logger.Info("Starting " + item.Item2.GetCustomAttribute<DiscordBotAttribute>()?.DisplayName + "...");
        BotInstances.Add(item.Item1.CreateInstance(item.Item2.FullName) as Bot);
        Logger.Info("Done.");
    }
    catch (Exception e)
    {
        Logger.Error(null, e);
    }
}
Logger.Info("All bots started");
new ManualResetEvent(false).WaitOne();

void CurrentDomain_ProcessExit(object? sender, EventArgs e)
{
    Logger.Info("Shutting down, ctrl+c pressed.");
    foreach (var item in BotInstances)
    {
        item.Shutdown();
    }
    Logger.Shutdown();
    throw new Exception("Exit normally");
    //Environment.Exit(-1);
}