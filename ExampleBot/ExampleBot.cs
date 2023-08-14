using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharpcord_bot_library;

namespace ExampleBot
{
    //This is required for the host to launch your bot.
    [DiscordBot("Example bot", DiscordBotAttribute.BotType.gateway)]
    //This attribute specifies the name of the logging context. (Log lines will appear as "[Timestamp] [Example] This is a log message")
    [LogContext("Example")]
    public class ExampleBot : GatewayBot
    {
        public ExampleBot() 
        {
            //You have add this to your constructor
            InitBot();
            Logger.Info(this, "Bot started successfully");
        }

        public override string GetBotToken()
        {
            //Return your bot token here

            //If possible, use a more secure way of obtaining it
            return "<Your token>";
        }

        public override async void Interaction(Interaction i)
        {
            try
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
                                Logger.Info(this, "Ping command invoked");
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
            catch (Exception e)
            {
                Logger.Error(this, e);
            }
            
        }

        public override void Shutdown()
        {
            //Optional, free up resources or save data here.
        }
    }
}
