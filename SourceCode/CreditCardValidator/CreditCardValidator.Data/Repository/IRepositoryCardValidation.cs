
using CreditCardValidator.Data.Models;

namespace CreditCardValidator.Data.Repository
{
    public interface IRepositoryCardValidation
    {
        List<CardValidation> Get();
        List<CardValidation> GetByCard(int cardId);
        
    }
}
