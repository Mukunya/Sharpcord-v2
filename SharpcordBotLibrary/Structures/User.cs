using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public struct User
    {
        public ulong id;
        public string username;
        public string discriminator;
        public string? global_name;
        public string? avatar;
        public bool? bot;
        public bool? system;
        public bool? mfa_enabled;
        public string? banner;
        public int? accent_color;
        public string? locale;
        public bool? verified;
        public string? email;
        public int? flags;
        public int? premium_type;
        public int? public_flags;
    }
}
