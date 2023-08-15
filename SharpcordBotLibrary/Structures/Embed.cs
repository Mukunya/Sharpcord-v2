using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public struct Embed
    {
        public string? title;
        public string? type;
        public string? description;
        public string? url;
        public string? timestamp;
        public int? color;
        public struct Footer
        {
            public string text;
            public string? icon_url;
            public string? proxy_icon_url;
        }
        public Footer? footer;
        public struct Image
        {
            public string url;
            public string? proxy_url;
            public int? height;
            public int? width;
        }
        public Image? image;
        public struct Thumbnail
        {
            public string url;
            public string? proxy_url;
            public int? height;
            public int? width;
        }
        public Thumbnail? thumbnail;
        public struct Video
        {
            public string? url;
            public string? proxy_url;
            public int? height;
            public int? width;
        }
        public Video? video;
        public struct Provider
        {
            public string? name;
            public string? url;
        }
        public Provider? provider;
        public struct Author
        {
            public string name;
            public string? url;
            public string? icon_url;
            public string? proxy_icon_url;
        }
        public Author? author;
        public struct Field
        {
            public string name;
            public string value;
            public bool? inline;
        }
        public Field[]? fields;
    }
}
