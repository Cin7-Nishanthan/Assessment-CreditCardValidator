
using Core;
using Core.API;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using CreditCardValidator.Utils;
using CreditCardValidator.Business.Rule;

namespace CreditCardValidator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditCardValidatorController : BaseController
    {
        #region " Constructor "
        public CreditCardValidatorController():base(new Logger())
        {
            
        }
        #endregion

        #region " EndPoints "
        [HttpPost]
        public async Task<ResponseData> Post([FromBody]string cardNumber)
        {
            try
            {
                _logger.Log(1, "Request starts", cardNumber);

                Business.CreditCardValidator creditCardValidation = new Business.CreditCardValidator(new RuleNotEmptyValidation(), new RuleOnlyNumbersValidation(), new RuleLuhnValidation(), new RuleCardNumberValidation());
                _responseData.Data = creditCardValidation.Validate(cardNumber);
                _responseData.Status = 1000;
            }
            catch (Exception Ex) 
            {
                _responseData.Status = 1001;
                _responseData.Data = Ex.Message;
                _logger.Log(3, "Exception in validation process", null,Ex.Message);

            }
            _logger.Log(3, "Response sent", null, null, JsonSerializer.Serialize<ResponseData>(_responseData));
            return _responseData;
        }
        #endregion
    }
}