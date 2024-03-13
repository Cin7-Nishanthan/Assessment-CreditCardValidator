
using Microsoft.EntityFrameworkCore;
using CreditCardValidator.Data.Models;
using CreditCardValidator.Data.Support;
using CreditCardValidator.Data.CustomModels;

namespace CreditCardValidator.Data.Repository
{
    public class RepositoryCardValidationSP : IRepositoryCardValidation
    {
        #region " Vars "
        CreditCardValidatorContext _context;
        #endregion

        #region " Constructor "
        public RepositoryCardValidationSP()
        {
            _context = new CreditCardValidatorContext();
        }
        #endregion

        #region " Methods "
        public List<CardValidation> Get()
        {
            return _context.Database.SqlQueryRaw<CardValidations>(Constants.SP_GET_CARD_VALIDATION, new Microsoft.Data.SqlClient.SqlParameter(Constants.PARAM_CARDID, DBNull.Value)).AsEnumerable().Select(cv => new CardValidation()
            {
                CardId = cv.CardId,
                Id = cv.Id,
                Length = cv.Length,
                StartingNumber = cv.StartingNumber,
            }).ToList();
        }

        public List<CardValidation> GetByCard(int cardId)
        {
            return _context.Database.SqlQueryRaw<CardValidations>(Constants.SP_GET_CARD_VALIDATION, new Microsoft.Data.SqlClient.SqlParameter(Constants.PARAM_CARDID, cardId)).AsEnumerable().Select(cv => new CardValidation()
            {
                CardId = cv.CardId,
                Id = cv.Id,
                Length = cv.Length,
                StartingNumber = cv.StartingNumber,
            }).ToList();
        }
        #endregion
    }
}
