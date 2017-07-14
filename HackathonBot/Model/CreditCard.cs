using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonBot.Model
{
    [Serializable]
    public class CreditCard
    {
        public string CreditCardNumber { get; set; }

        public string CreditCardHolder { get; set; }

        public int CVV { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }
    }
}