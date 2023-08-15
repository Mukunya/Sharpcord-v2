using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public struct Message
    {
        public ulong id;
        public ulong channel_id;
        public User author;
        public string content;
        public string timestamp;
        public string edited_timestamp;
        public bool tts;
        public bool mention_everyone;
        public User[] mentions;
        public ulong[] mention_roles;
        public ChannelMention[]? mention_channels;
        public Attachment[] attachments;
        public Embed[] embeds;
        public Reaction[]? reactions;
        public MessageComponent[]? components;
        public string? nonce;
        public bool pinned;
        public ulong? webhook_id;
        public int type;
        public ulong? application_id;
    }
}
