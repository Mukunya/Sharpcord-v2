using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using NSec.Cryptography;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Sharpcord_bot_library
{
    [LogContext("Webhook server")]
    public class WebhookServer
    {
        static HttpListener Listener;
        static IAsyncResult listenerAsync;
        public static bool enabled = false;
        public static Dictionary<string,WebhookServer> Webhooks = new Dictionary<string, WebhookServer>();
        public string endpoint = "/default";
        public bool nondiscord = false;
        PublicKey BotKey;
        public Action<Interaction> Interaction_create;
        public Action<HttpListenerContext> NonDiscordRequest;
        public static void Setup()
        {
            string port = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/config.txt");
            Logger.Info(null, "Webhook initializing. Port is " + port);
            Listener = new HttpListener();
            Listener.Prefixes.Add("http://*:"+port + "/");
            Listener.Start();
            Logger.Info(null, "Webhook open and listening.");
            listenerAsync = Listener.BeginGetContext((_) => { Request(); }, null);
            //Request();
        }
        private static void Request()
        {
            Logger.Debug(null, "================================================================");
            Logger.Debug(null, "");
            Logger.Debug(null, "================================================================");
            HttpListenerContext context = Listener.EndGetContext(listenerAsync);
            listenerAsync = Listener.BeginGetContext((_) => { Request(); }, null);
            
            if (context.Request.Headers["CF-RAY"] == null)
            {
                Logger.Warning(null, "Direct IP request received. Denying. RawUrl: " + context.Request.RawUrl);
                context.Response.StatusCode = 403;
                context.Response.OutputStream.Write(Encoding.UTF8.GetBytes("403 - Forbidden"));
                context.Response.Close();
                return;
            }
            
            if (Webhooks.ContainsKey(context.Request.RawUrl.Split('?')[0]) && context.Request.Headers["User-Agent"] == "Discord-Interactions/1.0 (+https://discord.com)")
            {
                Logger.Info(null, "Cloudflare request received. RawUrl: " + context.Request.RawUrl);
                Webhooks[context.Request.RawUrl].Interaction_request(context);
            }
            else if (Webhooks.ContainsKey(context.Request.RawUrl.Split('?')[0]))
            {
                if (Webhooks[context.Request.RawUrl.Split('?')[0]].nondiscord)
                {
                    Logger.Info(null, "Cloudflare request received. RawUrl: " + context.Request.RawUrl.Split('?')[0]);
                    Webhooks[context.Request.RawUrl.Split('?')[0]].Interaction_request(context);
                }
            }
            else
            {
                Logger.Warning(null, "Cloudflare request received. Denying. RawUrl: " + context.Request.RawUrl.Split('?')[0]);
                context.Response.StatusCode = 404;
                context.Response.OutputStream.Write(Encoding.UTF8.GetBytes("Not found."));
                context.Response.Close();
            }
        }
        private void Interaction_request(HttpListenerContext c)
        {
            if (nondiscord)
            {
                Logger.Debug(this, "Non Discord request received.");
                NonDiscordRequest?.Invoke(c);
            }
            else
            {
                try
                {
                    Logger.Debug(this, "Discord request received.");
                    string data = new StreamReader(c.Request.InputStream, Encoding.UTF8).ReadToEnd();
                    Logger.Debug(this, data);
                    Logger.Debug(this, c.Request.Headers.ToString());
                    bool b = SignatureAlgorithm.Ed25519.Verify(BotKey, Encoding.UTF8.GetBytes(c.Request.Headers.Get("X-Signature-Timestamp") + data), StringToByteArrayFastest(c.Request.Headers.Get("X-Signature-Ed25519")?? "00"));
                    Logger.Debug(this, "Signature verification: "+ b.ToString());
                    if (!b)
                    {
                        Logger.Debug(this, "Response:  401; Wrong signature");
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        c.Response.OutputStream.Write(Encoding.UTF8.GetBytes("{\"error\":\"Bad network signature\"}"));
                        c.Response.Close();
                    }
                    else
                    {
                        Logger.Debug(this, "Response:  200");
                        c.Response.StatusCode = 200;

                        if (JObject.Parse(data)["type"].ToString() == "1")
                        {
                            Logger.Debug(this, "Returned: {\"type\":1}");
                            c.Response.ContentType = "application/json";
                            c.Response.OutputStream.Write(Encoding.UTF8.GetBytes("{\"type\":1}"));
                            c.Response.Close();
                        }
                        else
                        {
                            WebhookInteraction interaction = new WebhookInteraction(JsonConvert.DeserializeObject<InteractionObject>(data));
                            Interaction_create?.Invoke(interaction);
                            if (interaction.GetResponse(1000,out InteractionResponse? resp))
                            {
                                Logger.Debug(this, "Response got, " + JsonConvert.SerializeObject(resp,new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore}));
                                c.Response.ContentType = "application/json";
                                c.Response.OutputStream.Write(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(resp, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore })));
                                c.Response.Close();
                            }
                            else
                            {
                                Logger.Warning(this, "Response wait timed out, droppping interaction");
                                c.Response.Abort();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(this, e);
                }
            }
        }
        public static byte[] StringToByteArrayFastest(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        public static int GetHexVal(char hex)
        {
            int val = (int)hex;
            //For uppercase A-F letters:
            //return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }
        public WebhookServer(string endpoint, PublicKey key)
        {
            if (!enabled)
            {
                enabled = true;
                Setup();
            }
            this.endpoint = endpoint;
            BotKey = key;
            Webhooks.Add(endpoint, this);
        }
        public WebhookServer(string endpoint)
        {
            if (!enabled)
            {
                enabled = true;
                Setup();
            }
            this.endpoint = endpoint;
            nondiscord = true;
            Webhooks.Add(endpoint, this);
        }
    }
}