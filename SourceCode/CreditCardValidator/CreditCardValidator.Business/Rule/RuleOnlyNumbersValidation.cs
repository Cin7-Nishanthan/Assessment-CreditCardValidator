using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreditCardValidator.Business.Support;

namespace CreditCardValidator.Business.Rule
{
    public class RuleOnlyNumbersValidation : IRule
    {
        public bool IsValid(string cardNumber)
        {
            return cardNumber.IsDigitsOnly();
        }
    }
}
