using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharpcord_bot_library;

namespace ExampleBot
{
    //This is required for the host to launch your bot.
    [DiscordBot("Example bot", DiscordBotAttribute.BotType.webhook)]
    public class ExampleBot : WebhookBot
    {
        public ExampleBot()
        {
            //You have add this to your constructor

            // /test is the endpoint for this bot. Enter https://your.domain/test as the interactions endpoint url. The webhook server will listen on the port specified in the config.txt
            InitBot("/test", "<Your public key>");
        }

        public override string GetBotToken()
        {
            //Return your bot token here

            //If possible, use a more secure way of obtaining it
            return "<Your token>";
        }

        public override async void Interaction(Interaction i)
        {
            //Handle interactions here
            //Info about the interaction is in i.Data
            switch (i.Data.type)
            {
                case InteractionType.PING:
                    //You should never get a ping, as it's handled internally
                    break;
                case InteractionType.APPLICATION_COMMAND:
                    //Get the command id like this
                    switch (i.Data.data?.id)
                    {
                        case 1136969193544224830: //id of an example command
                            //Create the initial response message
                            await i.RespondAsync(new InteractionResponse()
                            {
                                type = InteractionResponse.InteractionCallbackType.CHANNEL_MESSAGE_WITH_SOURCE,
                                data = new MessageResponse()
                                {
                                    content = "This is an example",
                                    embeds = new[]
                                    {
                                        new Embed()
                                        {
                                            title = "Pong",
                                            description = i.Data.data?.options?[0].value?.ToString()
                                        }
                                    }
                                }
                            });
                            break;
                        default:
                            break;
                    }
                    break;
                case InteractionType.MESSAGE_COMPONENT:
                    break;
                case InteractionType.APPLICATION_COMMAND_AUTOCOMPLETE:
                    break;
                case InteractionType.MODAL_SUBMIT:
                    break;
            }
        }

        public override void Shutdown()
        {
            //Optional, free up resources or save data here.
        }
    }
}
