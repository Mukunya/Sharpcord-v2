using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public struct GuildMember
    {
        public User? user;
        public string? nick;
        public string? avatar;
        public ulong[] roles;
        public string joined_at;
        public string? premium_since;
        public bool deaf;
        public bool mute;
        public int flags;
        public bool? pending;
        public string? permissions;
        public string? communication_disabled_until;
    }
}
