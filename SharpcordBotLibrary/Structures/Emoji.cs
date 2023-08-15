using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public struct Emoji
    {
        public ulong? id;
        public string? name;
        public ulong[]? roles;
        public User? user;
        public bool? require_colons;
        public bool? managed;
        public bool? animated;
        public bool? available;
    }
}
