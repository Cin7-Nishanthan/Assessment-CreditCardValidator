using CreditCardValidator.API.Models;
using CreditCardValidator.Business;
using CreditCardValidator.Business.Rule;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardValidator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditCardValidatorController : ControllerBase
    {
        private readonly ILogger<CreditCardValidatorController> _logger;

        public CreditCardValidatorController(ILogger<CreditCardValidatorController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ResponseData Post([FromBody]string CardNumber)
        {
            ResponseData ResponseData = new ResponseData();
            try
            {
                _logger.LogInformation("Entered Card No is : " + CardNumber);

                CreditCard CreditCard = new CreditCard(new NotEmpty(), new OnlyNumbers(), new LuhnValidate(), new CardNumber());
                ResponseData.Data = CreditCard.Validate(CardNumber);
                ResponseData.Status = 1000;
            }
            catch (Exception Ex) 
            {
                ResponseData.Status = 1001;
                ResponseData.Data = Ex.Message;
                _logger.LogError(Ex, Ex.Message);
            }

            return ResponseData;
        }
    }
}
