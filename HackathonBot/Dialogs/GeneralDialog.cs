using HackathonBot.Model;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackathonBot.Dialogs
{
    [LuisModel("2b357f92-681a-4d14-afeb-ed297b055404", "50c09327130842ce820e121e03ff1fe3")]
    [Serializable]
    public class GeneralDialog : LuisDialog<string>
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
            PromptDialog.Number(context, ConfirmBillDateHandler, "Please provide either your account number or phone number to confirm your identity?");

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

        [LuisIntent("digitalMeter")]
        public async Task DigitalMeters(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Digital meters, otherwise known as smart meters, are the next generation of electricity meters, replacing analogue ones. They measure your home's electricity use in 30-minute intervals, and can send this data automatically to your energy retailer every day using a secure wireless network.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("agl")]
        public async Task AGL(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("AGL Energy is a publicly–listed Australian company, provides energy products and services to the Australian economy. The company is involved in both the generation and retailing of electricity for residential and commercial use.");
            await context.PostAsync(@"Giving you more reasons to switch to AGL:
                                    Big range of competitive energy plans
                                    Great rewards for staying with us
                                    Useful tools and tips to help you save");
            context.Wait(MessageReceived);
        }

        [LuisIntent("aglEnergySources")]
        public async Task AglEnergySources(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("AGL’s diverse power generation portfolio is spread across traditional thermal generation as well as renewable sources including hydro, wind, landfill gas, biomass and solar.");
            await context.PostAsync("For more infromation, please visit: https://www.agl.com.au/about-agl/how-we-source-energy");
            context.Wait(MessageReceived);
        }

        private async Task ConfirmBillDateHandler(IDialogContext context, IAwaitable<long> result)
        {
            await context.PostAsync("Thanks Tama!");
            await context.PostAsync("Your current usage is $170.89");
            await context.PostAsync("Your next bill will be on 10/08/2017");
            await context.PostAsync("To make your payment process easier, please configure your payment method");

            var list = new List<string>()
            {
                "Bank",
                "Paypal",
                "Credit or Debit Card"
            };

            PromptDialog.Choice<string>(context, ConfirmPaymentMethod, list, "Please select your payment method:", "I didn't get that", 3, PromptStyle.Auto);
        }

        private async Task ConfirmPaymentMethod(IDialogContext context, IAwaitable<string> result)
        {
            PromptDialog.Confirm(context, ConfirmCreditCardHandler, "Do you want to pay by credit card?");
            await Task.CompletedTask;
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
    }
}