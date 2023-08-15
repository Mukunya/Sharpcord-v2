using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public struct AutoCompleteResult : IInteractionResponseData
    {
        public struct Choice
        {
            public string name;
            public object value;
        }
        public Choice[] choices;
    }
}
