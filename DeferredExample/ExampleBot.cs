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
    //This attribute specifies the name of the logging context. (Log lines will appear as "[Timestamp] [Deferred example] This is a log message")
    [LogContext("Deferred example")]
    public class ExampleBot : WebhookBot
    {
        public ExampleBot()
        {
            //You have add this to your constructor

            // /test is the endpoint for this bot. Enter https://your.domain/test as the interactions endpoint url. The webhook server will listen on the port specified in the config.txt
            InitBot("/test", "<Your public key>");
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
                                //With webhook interactions, you have 1 second to send an initial reply

                                //For interactions that take time to execute, it is recommended to ack an interaction and send a response later to avoid timing out.

                                //Send a loading state
                                await i.RespondAsync(new InteractionResponse()
                                {
                                    type = InteractionResponse.InteractionCallbackType.DEFERRED_CHANNEL_MESSAGE_WITH_SOURCE,
                                    data = new MessageResponse() { }
                                });
                                Logger.Info(this, "Ping command invoked");
                                //Do some work
                                await Task.Delay(5000);

                                //Edit the initial response message
                                await i.EditOriginalResponseAsync(
                                    new MessageResponse()
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
                                );
                                Logger.Info(this, "Sent final response to command");
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
