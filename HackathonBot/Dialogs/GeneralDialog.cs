using HackathonBot.Model;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace HackathonBot.Dialogs
{
    [LuisModel("2b357f92-681a-4d14-afeb-ed297b055404", "50c09327130842ce820e121e03ff1fe3")]
    [Serializable]
    public class GeneralDialog : LuisDialog<string>
    {
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            var rand = new Random();
            int messageIndex = rand.Next() % _notUnderstoodMessages.Length;

            var giffy = new GiffyService();
            var gif = giffy.GetRandomGif("lol");

            var reply = context.MakeMessage();
            reply.Text = _notUnderstoodMessages[messageIndex];

            reply.Attachments.Add(new Attachment
            {
                ContentUrl = gif,
                ContentType = "image/gif",
                Name = "giffy.png"
            });

            await context.PostAsync(reply);

            context.Wait(MessageReceived);
        }

        [LuisIntent("nextBill")]
        public async Task NextBill(IDialogContext context, LuisResult result)
        {
            PromptDialog.Confirm(context, ConfirmBillDateHandler, "Do you want the date for the next bill?");

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


        [LuisIntent("greetings")]
        public async Task Greetings(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Hey, please feel free to ask me anything about AGL, your Bills, or our packages.");
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
            await context.PostAsync("Your credit card information has been saved, your next bill will be deducted from your card, no more paper bills!");
            context.Done("RestartConversation");
            await Task.CompletedTask;
        }

        private readonly string[] _notUnderstoodMessages =
        {
            "I didn't understand your message, please try again",
            "Thank you very much, Mr. Ducksworth! Quack Quack Quack Quack Quack, Mr. Ducksworth!",
            "Um. Okay. - Ducks fly together!"
        };
    }
}