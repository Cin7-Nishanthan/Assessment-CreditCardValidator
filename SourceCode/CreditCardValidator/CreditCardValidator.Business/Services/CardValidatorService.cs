
using CreditCardValidator.Data.Models;
using CreditCardValidator.Data.Repository;

namespace CreditCardValidator.Business.Services
{
    public class CardValidatorService
    {
        #region " Vars "
        IRepositoryCardValidation _repositoryCardValidation;
        #endregion

        #region " Constructor "
        public CardValidatorService(IRepositoryCardValidation repositoryCardValidation) 
        {
            _repositoryCardValidation = repositoryCardValidation;
        }
        #endregion

        #region " Methods "
        public List<CardValidation> Get()
        {
            return _repositoryCardValidation.Get();
        }

        public List<CardValidation> GetByCard(int cardId)
        {
            return _repositoryCardValidation.GetByCard(cardId);
        }
        #endregion
    }
}
