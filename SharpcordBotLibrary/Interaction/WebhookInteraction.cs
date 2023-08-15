using Sharpcord_bot_library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public sealed class WebhookInteraction : Interaction
    {
        public WebhookInteraction(InteractionObject interaction) 
        {
            token = interaction.token;
            application_id = interaction.application_id;
            Data = interaction;
        }
        internal InteractionResponse initial;
        private ManualResetEvent respond = new ManualResetEvent(false);
        internal bool GetResponse(int mstimeout,out InteractionResponse? response)
        {
            if(respond.WaitOne(mstimeout))
            {
                response = initial; return true;
            }
            else
            {
                timeout = true;
                response = null; return false;
            }
        }
        private bool timeout = false;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Task RespondAsync(InteractionResponse resp)
        {
            if (timeout)
            {
                throw new TimeoutException();
            }
            initial = resp;
            respond.Set();
        }
    }
}
