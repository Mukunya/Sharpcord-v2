using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    //Confusing naming
    public struct ApplicationCommandInteractionDataOption
    {
        public string name;
        public int type;
        public object? value;
        public ApplicationCommandInteractionDataOption[]? options;
        public bool? focused;
    }
}
