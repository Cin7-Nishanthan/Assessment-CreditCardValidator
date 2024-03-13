using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Business.Rule
{
    public interface IRule
    {
        bool IsValid(string cardNumber);
    }
}
