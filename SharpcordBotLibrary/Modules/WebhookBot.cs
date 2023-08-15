using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
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
        protected void InitBot(string endpoint, string publickey)
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
        abstract public string GetBotToken();
        override abstract public void Shutdown();
    }
}
