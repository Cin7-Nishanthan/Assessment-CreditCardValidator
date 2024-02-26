using CreditCardValidator.Business.Rule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Business
{
    public class CreditCard
    {
        IRule[] Rules;
        public CreditCard(params IRule[] Rules)
        {
            this.Rules = Rules;
        }

        public bool Validate(string CardNumber)
        {
            foreach (var Rule in this.Rules)
            {
                if(!Rule.IsValid(CardNumber))
                    return false;
            }

            return true;
        }
    }
}
