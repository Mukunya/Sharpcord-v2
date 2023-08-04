using Serilog;
using Sharpcord_bot_library;
using System.Reflection;

List<Tuple<Assembly, Type>> LoadedBots = new List<Tuple<Assembly, Type>>();
List<Bot> BotInstances = new List<Bot>();

Logger.Init(args.Contains("-D"));
AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
Logger.Info(null, "Starting, searching for bots");
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
                Logger.Info(null,$"Loaded {type.GetCustomAttribute<DiscordBotAttribute>()?.DisplayName}, a bot with type {type.GetCustomAttribute<DiscordBotAttribute>()?.Type}");
            }
        }
    }
}
Logger.Info(null,$"Loading process done, loaded {LoadedBots.Count} modules.");
Logger.Info(null,"Starting bots...");
foreach (var item in LoadedBots)
{
    try
    {
        Logger.Info(null, "Starting " + item.Item2.GetCustomAttribute<DiscordBotAttribute>()?.DisplayName + "...");
        BotInstances.Add(item.Item1.CreateInstance(item.Item2.FullName) as Bot);
        Logger.Info(null, "Done.");
    }
    catch (Exception e)
    {
        Logger.Error(null, e);
    }
}
new ManualResetEvent(false).WaitOne();

void CurrentDomain_ProcessExit(object? sender, EventArgs e)
{
    Logger.Info(null, "Shutting down, ctrl+c pressed.");
    foreach (var item in BotInstances)
    {
        item.Shutdown();
    }
    Logger.Shutdown();
    Environment.Exit(0);
}