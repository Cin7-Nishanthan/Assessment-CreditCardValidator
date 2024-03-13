

namespace CreditCardValidator.Business.Rule
{
    public class RuleNotEmptyValidation : IRule
    {
        #region " Methods "
        public bool IsValid(string cardNumber)
        {
            return !string.IsNullOrWhiteSpace(cardNumber);
        }
        #endregion
    }
}
