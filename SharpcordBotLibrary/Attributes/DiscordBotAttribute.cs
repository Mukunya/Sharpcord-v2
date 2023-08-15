using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class DiscordBotAttribute : Attribute
    {
        public enum BotType { gateway = 0, webhook = 1 };
        public string DisplayName { get; set; }
        public BotType Type { get; set; }
        public DiscordBotAttribute(string displayName, BotType type)
        {
            DisplayName = displayName;
            Type=type;
        }

    }
}
