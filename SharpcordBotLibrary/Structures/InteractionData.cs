using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public struct InteractionData
    {
        public ulong? id;
        public string? name;
        public int? InteractionType;
        public object? resolved;
        public ApplicationCommandInteractionDataOption[]? options;
        public ulong? guild_id;
        public ulong? target_id;
        public string? custom_id;
        public int? component_type;
        public string[]? values;
        public MessageComponent[]? components;

    }
}
