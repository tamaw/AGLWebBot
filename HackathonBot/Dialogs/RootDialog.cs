using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.FormFlow;

namespace HackathonBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            var messageText = (activity.Text ?? string.Empty);
            
            //await context.PostAsync("We are processing your message ....");

            if (messageText.ToLower().Contains("bill"))
            {
                await context.Forward(new Dialogs.BillDialog(), ResumeAfter, activity);
            }
            else if (messageText.ToLower().Contains("what are we"))
            {
                await context.PostAsync("Ducks!");
            }
            else if (messageText.ToLower().Contains("what are you"))
            {
                await context.PostAsync("Thank you, Dwayne, but I'm no lady. I'M A DUCK!");
            }
            else if (messageText.ToLower().Contains("what do ducks do"))
            {
                await context.PostAsync("Ducks fly together!");
            }
            else if (messageText.ToLower().Contains("ducks fly together"))
            {
                await context.PostAsync("And ducks fly together. - That's right,Jan.");
            }
            else if (messageText.ToLower().Contains("why ducks"))
            {
                await context.PostAsync("I'll have you know, Peter, that the Duck is one of the most noble, agile and intelligent creatures in the animal kingdom.");
            }
            else
            {
                var rand = new Random();
                int messageIndex = rand.Next() % _notUnderstoodMessages.Length;

                var giffy = new GiffyService();
                var gif = giffy.GetRandomGif("lol");

                var reply = activity.CreateReply(_notUnderstoodMessages[messageIndex]);

                reply.Attachments.Add(new Attachment
                {
                    ContentUrl = gif,
                    ContentType = "image/gif",
                    Name = "giffy.png"
                });

                await context.PostAsync(reply);

                context.Wait(MessageReceivedAsync);
            }
        }

        private readonly string[] _notUnderstoodMessages =
        {
            "I didn't understand your message, please try again",
            "Thank you very much, Mr. Ducksworth! Quack Quack Quack Quack Quack, Mr. Ducksworth!",
            "Um. Okay. - Ducks fly together!"
            //"And just when you think they're about to break apart- - Ducks fly together!",
            //"And when the wind blows hard and the sky is black- - Ducks fly together!",
            //"When the roosters are crowing and the cows are spinning circles in the pasture-",
            //"And when the wind blows hard and the sky is black - Ducks fly together!",
        };

        private async Task ResumeAfter(IDialogContext context, IAwaitable<string> result)
        {
            var endMessage = await result;

            await context.PostAsync("How can I help you again?");
        }
    }
}