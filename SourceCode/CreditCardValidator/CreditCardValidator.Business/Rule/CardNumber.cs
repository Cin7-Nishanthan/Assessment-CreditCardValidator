using CreditCardValidator.Business.Cache;
using CreditCardValidator.Data;
using CreditCardValidator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Business.Rule
{
    public class CardNumber : IRule
    {
        UnitOfWork UnitOfWork;
        List<CardValidationSPData> CardValidations;

        public CardNumber()
        {
            UnitOfWork = new UnitOfWork(new CreditCardValidatorContext());
        }

        public bool IsValid(string CardNumber)
        {
            CardValidations = CreditCardValidationCache.GetCardValidations();
            if (CardValidations == null || CardValidations.Count == 0)
            {
                CardValidations = UnitOfWork.GetCardValidationStoredProcedure("GetCardValidation", new Microsoft.Data.SqlClient.SqlParameter("@CardId", DBNull.Value)).ToList();
                CreditCardValidationCache.SetCardValidations(CardValidations);
            }

            foreach (CardValidationSPData CardValidation in CardValidations) 
            {
                if (CardNumber.StartsWith(CardValidation.StartingNumber) && CardNumber.Length == CardValidation.Length)
                    return true;
            }
            return false;
        }
    }
}
