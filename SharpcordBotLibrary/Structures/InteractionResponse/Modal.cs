using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public struct Modal : IInteractionResponseData
    {
        public string custom_id;
        public string title;
        public MessageComponent[] components;
    }
}
