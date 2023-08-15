using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public abstract class HttpNonDiscordBot : Bot
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
}
