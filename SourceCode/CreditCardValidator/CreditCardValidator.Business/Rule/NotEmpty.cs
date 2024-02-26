using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Business.Rule
{
    public class NotEmpty : IRule
    {
        public bool IsValid(string CardNumber)
        {
            return !string.IsNullOrWhiteSpace(CardNumber);
        }
    }
}
