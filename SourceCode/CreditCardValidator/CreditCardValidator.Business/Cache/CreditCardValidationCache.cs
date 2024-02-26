using CreditCardValidator.Business.Support;
using CreditCardValidator.Data.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Business.Cache
{
    public static class CreditCardValidationCache
    {
        private static readonly IMemoryCache Cache = new MemoryCache(new MemoryCacheOptions());
        static List<CardValidationSPData> CardValidations;

        public static List<CardValidationSPData> GetCardValidations()
        {
            CardValidations = new List<CardValidationSPData>();
            bool IsAvaiable = Cache.TryGetValue(Constants.CACHEKEY, out  CardValidations);
            return CardValidations;
        }

        public static void SetCardValidations(List<CardValidationSPData> CardValidations)
        {
            var CacheEntryOptions = new MemoryCacheEntryOptions();

            Cache.Set(Constants.CACHEKEY, CardValidations, CacheEntryOptions);
        }
    }
}
