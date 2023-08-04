using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Sharpcord_bot_library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public class WebHook : IDisposable
    {
        public string _url;
        public ulong _channel_id;
        HttpClient _client = new HttpClient();
        public List<Message> messages = new List<Message>();
        HttpClient dcclient;

        public WebHook(ulong channel_id, HttpClient client)
        {
            Init(channel_id).Wait();
            this.dcclient=client;
        }
        public WebHook(string url)
        {
            _url = url;
            _client.BaseAddress = new Uri(_url);
        }
        private WebHook() { }
        private async Task Init(ulong channel_id)
        {
            string resp = await (await dcclient.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"channels/{channel_id}/webhooks").WithContent(new StringContent("{\"name\":\"Music player\",\"type\":3}").AsJson()))).EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            //Console.WriteLine(resp);
            JObject respjson = JObject.Parse(resp);
            _url = respjson["url"].ToString();
            _client.BaseAddress = new Uri(_url);
            _channel_id = channel_id;
        }
        public static async Task<WebHook[]> LoadFromChannel(ulong channel_id,HttpClient client)
        {
            string resp = await (await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"channels/{channel_id}/webhooks"))).EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            //await Console.Out.WriteLineAsync(resp);
            List<WebHook> hooks = new List<WebHook>();
            JObject respjson = new JObject();
            foreach (var item in respjson.Values().Where(o => o["application_id"]?.Value<ulong>() == 1120694366902698024))
            {
                hooks.Add(new WebHook(item["url"].ToString()) { _channel_id = channel_id });
            }
            return hooks.ToArray();
        }
        public async Task SendMessage(Message msg)
        {
            try
            {
                string resp = await (await _client.PostAsync(_url+"?wait=true", new StringContent(JsonConvert.SerializeObject(msg, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore })).AsJson())).EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                messages.Add(JsonConvert.DeserializeObject<Message>(resp));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public async Task EditMessage(Message msg)
        {
            try
            {
                (await _client.PatchAsync(_url+"/messages/"+msg.id, new StringContent(JsonConvert.SerializeObject(msg)).AsJson())).EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public async Task DeleteMessage(Message msg)
        {
            try
            {
                (await _client.DeleteAsync(_url+"/messages/"+msg.id)).EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                lock (messages)
                {
                    messages.Remove(messages.First(o => o.id == msg.id));
                }
            }

        }
        public async void Dispose()
        {
            try
            {
                (await _client.DeleteAsync(_url)).EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.ToString());
            }

            _client.Dispose();
        }
    }
}

