
using CreditCardValidator.Data.Models;
using CreditCardValidator.Business.Cache;
using CreditCardValidator.Data.Repository;

namespace CreditCardValidator.Business.Rule
{
    public class RuleCardNumberValidation : IRule
    {
        #region " Vars "
        IRepositoryCardValidation _repositoryCardValidation;
        List<CardValidation> _cardValidations;
        #endregion

        #region " Constructor "
        public RuleCardNumberValidation()
        {
            _repositoryCardValidation = new RepositoryCardValidationSP();
        }
        #endregion

        #region " Methods "
        public bool IsValid(string cardNumber)
        {
            _cardValidations = CreditCardValidationCache.GetCardValidations();
            if (_cardValidations == null || _cardValidations.Count == 0)
            {
                _cardValidations = _repositoryCardValidation.Get();
                CreditCardValidationCache.SetCardValidations(_cardValidations);
            }

            foreach (CardValidation cardValidation in _cardValidations)
            {
                if (cardNumber.StartsWith(cardValidation.StartingNumber) && cardNumber.Length == cardValidation.Length)
                    return true;
            }
            return false;
        }
        #endregion
    }
}