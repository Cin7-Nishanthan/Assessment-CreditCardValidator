using CreditCardValidator.Business.Rule;
using CreditCardValidator.Utils;


namespace CreditCardValidator.Business
{
    public class CreditCardValidator
    {
        IRule[] _rules;

        public CreditCardValidator(params IRule[] rules)
        {
            this._rules = rules;
        }

        public bool Validate(string cardNumber)
        {
            foreach (var rule in this._rules)
            {
                if (!rule.IsValid(cardNumber))
                    return false;
            }
            
            return true;
        }
    }
}
