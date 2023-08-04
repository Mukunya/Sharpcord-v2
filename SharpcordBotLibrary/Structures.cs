using Newtonsoft.Json;
using System.Threading;

namespace Sharpcord_bot_library
{
    public struct InteractionObject
    {
        public ulong id;
        public ulong application_id;
        public InteractionType type;
        public InteractionData? data;
        public ulong? guild_id;
        public ulong? channel_id;
        public GuildMember? member;
        public User? user;
        public string token;
        public int version;
        public string? app_permissions;
        public string? locale;
        public string? guild_locale;
    }
    public enum InteractionType
    {
        PING = 1, APPLICATION_COMMAND = 2, MESSAGE_COMPONENT = 3, APPLICATION_COMMAND_AUTOCOMPLETE = 4, MODAL_SUBMIT = 5
    }
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
    public struct MessageComponent
    {
        public enum Type
        {
            actionrow=1,button=2,stringselect=3,textinput=4,userselect=5,roleselect=6,mentionableselect7,channelselect=8
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
    public struct ApplicationCommandInteractionDataOption
    {
        public string name;
        public int type;
        public object? value;
        public ApplicationCommandInteractionDataOption[]? options;
        public bool? focused;
    }
    public struct GuildMember
    {
        public User? user;
        public string? nick;
        public string? avatar;
        public ulong[] roles;
        public string joined_at;
        public string? premium_since;
        public bool deaf;
        public bool mute;
        public int flags;
        public bool? pending;
        public string? permissions;
        public string? communication_disabled_until;
    }
    public struct User
    {
        public ulong id;
        public string username;
        public string discriminator;
        public string? global_name;
        public string? avatar;
        public bool? bot;
        public bool? system;
        public bool? mfa_enabled;
        public string? banner;
        public int? accent_color;
        public string? locale;
        public bool? verified;
        public string? email;
        public int? flags;
        public int? premium_type;
        public int? public_flags;
    }
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
    public struct ChannelMention
    {
        public ulong id;
        public ulong guild_id;
        public int type;
        public string name;
    }
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
    public struct Reaction
    {
        public int count;
        public bool me;
        public Emoji emoji;
    }
    public struct Emoji
    {
        public ulong? id;
        public string? name;
        public ulong[]? roles;
        public User? user;
        public bool? require_colons;
        public bool? managed;
        public bool? animated;
        public bool? available;
    }

    public struct InteractionResponse
    {
        public enum InteractionCallbackType
        {
            PONG = 1, CHANNEL_MESSAGE_WITH_SOURCE = 4, DEFERRED_CHANNEL_MESSAGE_WITH_SOURCE = 5, DEFERRED_UPDATE_MESSAGE = 6, UPDATE_MESSAGE = 7, APPLICATION_COMMAND_AUTOCOMPLETE_RESULT = 8, MODAL = 9
        }
        public InteractionCallbackType type;
        public IInteractionData data;
    }
    public interface IInteractionData { }
    public struct MessageResponse : IInteractionData 
    {
        public bool? tts;
        public string? content;
        public Embed[]? embeds;
        [Flags]
        public enum Flags
        {
            CROSSPOSTED = 1<<0, IS_CROSSPOST=1 << 1,SUPPRESS_EMBEDS=1 << 2,SOURCE_MESSAGE_DELETED=1 << 3,URGENT=1 << 4,HAS_THREAD=1 << 5,EPHEMERAL=1 << 6,LOADING=1 << 7,FAILED_TO_MENTION_SOME_ROLES_IN_THREAD=1 << 8,SUPPRESS_NOTIFICATIONS=1 << 12,IS_VOICE_MESSAGE=1 << 13
    }
        public Flags? flags;
        public MessageComponent[]? components;
        public Attachment[]? attachments;
    }
    public struct AutoCompleteResult : IInteractionData 
    {
        public struct Choice
        {
            public string name;
            public object value;
        }
        public Choice[] choices;
    }
    public struct Modal : IInteractionData
    {
        public string custom_id;
        public string title;
        public MessageComponent[] components;
    }
}

