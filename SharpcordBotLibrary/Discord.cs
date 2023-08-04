using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public class Discord
    {
        public const string DC_API = "https://discord.com/api/v10/";
    }
    public abstract class Bot
    {
        public abstract void Shutdown();
    }
    public abstract class GatewayBot : Bot
    {
        GatewayConnection gateway;
        
        protected void InitBot()
        {
            gateway = new GatewayConnection(GetBotToken());
            gateway.INTERACTION_CREATE += INTERACTION_CREATE;
            Logger.Info(this, "Gateway bot initialization complete.");
            gateway.Connect();
        }
        private void INTERACTION_CREATE(object? sender, JToken e)
        {
            Interaction(new GatewayInteraction(e.ToObject<InteractionObject>()));
        }
        abstract public void Interaction(Interaction i);
        abstract public string GetBotToken();
        override abstract public void Shutdown();
    }

    public abstract class WebhookBot : Bot
    {
        internal static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
        public WebhookServer server;
        protected void InitBot(string endpoint,string publickey)
        {
            server = new WebhookServer(
                endpoint,
                NSec.Cryptography.PublicKey.Import(
                    NSec.Cryptography.SignatureAlgorithm.Ed25519,
                    StringToByteArray(publickey),
                    NSec.Cryptography.KeyBlobFormat.RawPublicKey));
            server.Interaction_create = c =>
            {
                Interaction(c);
            };
        }
        abstract public void Interaction(Interaction i);
        //abstract public void Interaction_followup(JObject o);
        abstract public string GetBotToken();
        override abstract public void Shutdown();
    }
    public abstract class HttpNonDiscordBot: Bot
    {
        public WebhookServer server;
        protected void InitBot(string endpoint)
        {
            server = new WebhookServer(endpoint);
            server.NonDiscordRequest = Request;
        }
        abstract public void Request(HttpListenerContext c);
        override abstract public void Shutdown();
    }
    
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class DiscordBotAttribute : Attribute
    {
        public enum BotType { gateway = 0, webhook = 1};
        public string DisplayName { get; set; }
        public BotType Type { get; set; }
        public DiscordBotAttribute(string displayName, BotType type)
        {
            DisplayName = displayName;
            Type=type;
        }

    }
}
