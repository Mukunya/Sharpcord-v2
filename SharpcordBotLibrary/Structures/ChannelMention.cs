using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public struct ChannelMention
    {
        public ulong id;
        public ulong guild_id;
        public int type;
        public string name;
    }
}
