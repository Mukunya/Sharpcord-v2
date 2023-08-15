using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public struct MessageResponse : IInteractionResponseData
    {
        public bool? tts;
        public string? content;
        public Embed[]? embeds;
        [Flags]
        public enum Flags
        {
            CROSSPOSTED = 1<<0,
            IS_CROSSPOST = 1 << 1,
            SUPPRESS_EMBEDS = 1 << 2,
            SOURCE_MESSAGE_DELETED = 1 << 3,
            URGENT = 1 << 4,
            HAS_THREAD = 1 << 5,
            EPHEMERAL = 1 << 6,
            LOADING = 1 << 7,
            FAILED_TO_MENTION_SOME_ROLES_IN_THREAD = 1 << 8,
            SUPPRESS_NOTIFICATIONS = 1 << 12,
            IS_VOICE_MESSAGE = 1 << 13
        }
        public Flags? flags;
        public MessageComponent[]? components;
        public Attachment[]? attachments;
    }
}
