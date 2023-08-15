using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public enum InteractionType
    {
        PING = 1, APPLICATION_COMMAND = 2, MESSAGE_COMPONENT = 3, APPLICATION_COMMAND_AUTOCOMPLETE = 4, MODAL_SUBMIT = 5
    }
    public struct InteractionObject
    {
        public ulong id;
        public ulong application_id;
        public InteractionType type;
        public InteractionData? data;
        public ulong? guild_id;
        public ulong? channel_id;
        public GuildMember? member;
        public User? user;
        public string token;
        public int version;
        public string? app_permissions;
        public string? locale;
        public string? guild_locale;
    }
}
