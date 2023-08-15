using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public struct MessageComponent
    {
        public enum Type
        {
            actionrow = 1, button = 2, stringselect = 3, textinput = 4, userselect = 5, roleselect = 6, mentionableselect7, channelselect = 8
        }
        public Type type;
        public MessageComponent[]? components;
        public string? custom_id;
        public enum ButtonStyle
        {
            Primary = 1, Secondary = 2, Success = 3, Danger = 3, Link = 5
        }
        public enum TextInputStyle
        {
            Short = 1, Paragraph = 2
        }
        public int? style;
        public string? label;
        public Emoji? emoji;
        public string? url;
        public bool? disabled;
        public SelectOption[]? options;
        public string? placeholder;
        public int? min_values;
        public int? max_values;
        public struct SelectOption
        {
            public string label;
            public string value;
            public string? description;
            public Emoji? emoji;
            [JsonProperty("default")]
            public bool? _default;
        }
        public int? min_lenght;
        public int? max_lenght;
        public string? value;
    }
}
