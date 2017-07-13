using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;

namespace HackathonBot.Dialogs
{
    [LuisModel("2b357f92-681a-4d14-afeb-ed297b055404", "50c09327130842ce820e121e03ff1fe3")]
    [Serializable]
    public class BillDialog : LuisDialog<object>
    {
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Sorry, I don't know what you mean");
            context.Wait(MessageReceived);
        }

        [LuisIntent("nextBill")]
        public async Task NextBill(IDialogContext context, LuisResult result)
        {
            EntityRecommendation contractType;
            if(result.TryFindEntity("contactType",out contractType))
            {
                PromptDialog.Confirm(context, ResumeAfterHandler, "Do you want to the date for the next bill?", attempts: 3, promptStyle: PromptStyle.AutoText);
            }
            else
            {
                PromptDialog.Confirm(context, ResumeAfterHandler, "Do you want to the date for the next bill?", attempts: 3, promptStyle: PromptStyle.AutoText);
            }

            context.Wait(MessageReceived);
        }

        [LuisIntent("currentBill")]
        public async Task currentBill(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("We don't have your current meter reading, please upload a photo?");
            context.Wait(MessageReceived);
        }

        [LuisIntent("sendBill")]
        public async Task sendBill(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Do you want your bill to be sent over email?");
            context.Wait(MessageReceived);
        }

        private async Task ResumeAfterHandler(IDialogContext context, IAwaitable<bool> result)
        {
            await Task.FromResult(10);
        }
    }
}