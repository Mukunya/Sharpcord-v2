using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sharpcord_bot_library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public sealed class GatewayInteraction : Interaction
    {
        private ulong id; 
        internal GatewayInteraction(InteractionObject interaction) 
        {
            token = interaction.token;
            application_id = interaction.application_id;
            id = interaction.id;
            Data = interaction;
        }
        public override async Task RespondAsync(InteractionResponse resp)
        {
            Logger.Debug(token);
            Logger.Debug(application_id.ToString());
            Logger.Debug(id.ToString());
            Logger.Debug(JObject.FromObject(Data).ToString());
            (await client.PostAsync($"interactions/{id}/{token}/callback", new StringContent(JsonConvert.SerializeObject(resp, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore })).AsJson())).EnsureSuccessStatusCode();
        }
    }
}
