using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public struct Attachment
    {
        public ulong id;
        public string filename;
        public string? description;
        public string? content_type;
        public int size;
        public string url;
        public string proxy_url;
        public int? height;
        public int? width;
        public bool? ephemeral;
        public float? duration_secs;
        public string? waveform;
    }
}
