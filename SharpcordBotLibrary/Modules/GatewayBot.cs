using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public abstract class GatewayBot : Bot
    {
        GatewayConnection gateway;

        protected void InitBot()
        {
            gateway = new GatewayConnection(GetBotToken());
            gateway.INTERACTION_CREATE += INTERACTION_CREATE;
            Logger.Info("Gateway bot initialization complete.");
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
}
