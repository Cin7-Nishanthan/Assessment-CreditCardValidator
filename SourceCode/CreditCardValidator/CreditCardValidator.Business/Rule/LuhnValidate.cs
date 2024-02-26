using CreditCardValidator.Business.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Business.Rule
{
    public class LuhnValidate : IRule
    {
        Luhn Luhn;
        
        public bool IsValid(string CardNumber)
        {
            Luhn = new Luhn(CardNumber);
            return Luhn.IsValid();
        }
    }
}
