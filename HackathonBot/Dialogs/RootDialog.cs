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
            
            await context.Forward(new Dialogs.GeneralDialog(), ResumeAfter, activity);
        }

        private async Task ResumeAfter(IDialogContext context, IAwaitable<string> result)
        {
            var endMessage = await result;

            await context.PostAsync("How can I help you again?");
        }
    }
}