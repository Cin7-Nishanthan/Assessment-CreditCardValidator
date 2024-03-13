
using CreditCardValidator.Data.Models;

namespace CreditCardValidator.Data.Repository
{
    public class RepositoryCardValidation : IRepositoryCardValidation
    {
        #region " Vars "
        CreditCardValidatorContext _context;
        #endregion

        #region " Constructor "
        public RepositoryCardValidation()
        {
            _context = new CreditCardValidatorContext();
        }
        #endregion

        #region " Methods "
        public List<CardValidation> Get()
        {
            return _context.CardValidations.Where(cv => cv.Status == 1 && cv.IsDeleted == false).ToList();
        }

        public List<CardValidation> GetByCard(int cardId)
        {
            return _context.CardValidations.Where(cv => cv.CardId == cardId && cv.Status == 1 && cv.IsDeleted == false).ToList();
        }
        #endregion
    }
}
