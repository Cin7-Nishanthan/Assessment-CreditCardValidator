

using CreditCardValidator.Data.Models;
using Microsoft.Extensions.Caching.Memory;
using CreditCardValidator.Business.Support;

namespace CreditCardValidator.Business.Cache
{
    public static class CreditCardValidationCache
    {
        #region " Vars "
        private static readonly IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        static List<CardValidation> _cardValidations;
        #endregion

        #region " Methods "
        public static List<CardValidation> GetCardValidations()
        {
            _cardValidations = new List<CardValidation>();
            bool IsAvaiable = _cache.TryGetValue(Constants.CACHEKEY, out _cardValidations);
            return _cardValidations;
        }

        public static void SetCardValidations(List<CardValidation> cardValidations)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions();

            _cache.Set(Constants.CACHEKEY, cardValidations, cacheEntryOptions);
        }
        #endregion
    }
}
