using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonBot.Model
{
    [Serializable]
    public class CreditCard
    {
        [Prompt("Please enter your Credit Card Number (16 digitis)")]
        [Pattern(@"^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$")]
        public string CreditCardNumber { get; set; }

        public string CreditCardHolder { get; set; }

        public MonthEnum ExpiryMonth { get; set; }
        
        [Prompt("Enter the expiry year (ex: 2018)")]
        public int ExpiryYear { get; set; }

        [Prompt("Please enter CVV (a 3 digit number on the back of your card)")]
        public string CVV { get; set; }

    }
}