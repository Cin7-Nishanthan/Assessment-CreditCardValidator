using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreditCardValidator.Business.Support;

namespace CreditCardValidator.Business.Rule
{
    public class OnlyNumbers : IRule
    {
        public bool IsValid(string CardNumber)
        {
            return CardNumber.IsDigitsOnly();
        }
    }
}
