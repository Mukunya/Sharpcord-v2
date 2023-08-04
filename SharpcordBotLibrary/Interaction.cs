using Newtonsoft.Json;
using Sharpcord_bot_library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public abstract class Interaction
    {
        internal HttpClient client = new HttpClient() { BaseAddress = new Uri(Discord.DC_API) };
        internal string token;
        internal ulong application_id;
        #region Sync
        public void Respond(InteractionResponse resp)
        {
            RespondAsync(resp).GetAwaiter().GetResult();
        }
        public void DeleteOriginalResponse()
        {
            DeleteOriginalResponseAsync().GetAwaiter().GetResult();
        }
        public Message CreateFollowupMessage(MessageResponse message)
        {
            return CreateFollowupMessageAsync(message).GetAwaiter().GetResult();
        }
        public Message GetOriginalResponse()
        {
            return GetOriginalResponseAsync().GetAwaiter().GetResult();
        }
        public Message EditOriginalResponse(MessageResponse message)
        {
            return EditOriginalResponseAsync(message).GetAwaiter().GetResult();
        }
        public Message GetFollowupMessage(ulong id)
        {
            return GetFollowupMessageAsync(id).GetAwaiter().GetResult();
        }
        public void DeleteFollowupMessage(ulong id)
        {
            DeleteFollowupMessageAsync(id).GetAwaiter().GetResult();
        }
        public Message EditFollowupMessage(ulong id, MessageResponse message)
        {
            return EditFollowupMessageAsync(id, message).GetAwaiter().GetResult();
        }
        #endregion
        public abstract Task RespondAsync(InteractionResponse resp);
        public async Task<Message> GetOriginalResponseAsync()
        {
            return JsonConvert.DeserializeObject<Message>(await client.GetStringAsync($"/webhooks/{application_id}/{token}/messages/@original"));
        }
        public async Task<Message> EditOriginalResponseAsync(MessageResponse message)
        {
             return JsonConvert.DeserializeObject<Message>(await (await client.PatchAsync($"/webhooks/{application_id}/{token}/messages/@original", new StringContent(JsonConvert.SerializeObject(message, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore })).AsJson())).EnsureSuccessStatusCode().Content.ReadAsStringAsync());
        }
        public async Task DeleteOriginalResponseAsync()
        {
            await client.DeleteAsync($"/webhooks/{application_id}/{token}/messages/@original");
        }
        public async Task<Message> CreateFollowupMessageAsync(MessageResponse message)
        {
            return JsonConvert.DeserializeObject<Message>(await (await client.PostAsync($"/webhooks/{application_id}/{token}", new StringContent(JsonConvert.SerializeObject(message, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore })).AsJson())).EnsureSuccessStatusCode().Content.ReadAsStringAsync());
        }
        public async Task<Message> GetFollowupMessageAsync(ulong id)
        {
            return JsonConvert.DeserializeObject<Message>(await client.GetStringAsync($"/webhooks/{application_id}/{token}/messages/{id}"));
        }
        public async Task DeleteFollowupMessageAsync(ulong id)
        {
            await client.DeleteAsync($"/webhooks/{application_id}/{token}/messages/{id}");
        }
        public async Task<Message> EditFollowupMessageAsync(ulong id,MessageResponse message)
        {
            return JsonConvert.DeserializeObject<Message>(await (await client.PatchAsync($"/webhooks/{application_id}/{token}/messages/{id}", new StringContent(JsonConvert.SerializeObject(message, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore })).AsJson())).EnsureSuccessStatusCode().Content.ReadAsStringAsync());
        }
        public InteractionObject Data { get; internal set; }
    }
}
