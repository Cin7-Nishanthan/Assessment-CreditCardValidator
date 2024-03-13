using CreditCardValidator.Business.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Business.Rule
{
    public class RuleLuhnValidation : IRule
    {
        Luhn _luhn;
        
        public bool IsValid(string cardNumber)
        {
            _luhn = new Luhn(cardNumber);
            return _luhn.IsValid();
        }
    }
}
