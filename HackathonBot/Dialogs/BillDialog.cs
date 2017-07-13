using HackathonBot.Model;
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
    public class BillDialog : LuisDialog<string>
    {
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Sorry, please rephrase your question");
            context.Wait(MessageReceived);
        }

        [LuisIntent("nextBill")]
        public async Task NextBill(IDialogContext context, LuisResult result)
        {
            PromptDialog.Confirm(context, ConfirmBillDateHandler, "Do you want to the date for the next bill?");

            await Task.CompletedTask;
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

        private async Task ConfirmBillDateHandler(IDialogContext context, IAwaitable<bool> result)
        {
            var confirmResult = await result;

            if (confirmResult)
            {
                await context.PostAsync("Your next bill will be on 10/08/2017");
                PromptDialog.Confirm(context, ConfirmCreditCardHandler, "Do you want to pay by credit card?");
            }
            else
            {
                context.Done("RestartConversation");
            }
        }

        private async Task ConfirmCreditCardHandler(IDialogContext context, IAwaitable<bool> result)
        {
            var confirmResult = await result;

            if (confirmResult)
            {
                var creditCardForm = new FormDialog<CreditCard>(new CreditCard(), BuildCreditCardForm, FormOptions.PromptInStart);
                context.Call(creditCardForm, ResumeAfterCreditCardFormDialog);
            }
            else
            {
                context.Done("RestartConversation");
            }
        }

        private IForm<CreditCard> BuildCreditCardForm()
        {
            return new FormBuilder<CreditCard>().Build();
        }

        private async Task ResumeAfterCreditCardFormDialog(IDialogContext context, IAwaitable<CreditCard> result)
        {
            await Task.CompletedTask;
        }
    }
}