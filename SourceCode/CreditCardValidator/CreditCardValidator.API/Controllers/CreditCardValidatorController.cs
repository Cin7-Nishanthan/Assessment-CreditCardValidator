using CreditCardValidator.API.Models;
using CreditCardValidator.Business;
using CreditCardValidator.Business.Rule;
using CreditCardValidator.Core;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardValidator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditCardValidatorController : ControllerBase
    {
        private readonly CoreService _coreService;

        public CreditCardValidatorController(CoreService coreService)
        {
            _coreService = coreService;
        }

        [HttpPost]
        public async Task<ResponseData> Post([FromBody]string CardNumber)
        {
            ResponseData ResponseData = new ResponseData();
            try
            {
                await _coreService.AddInfoLogs("Entered Card No is : " + CardNumber);

                CreditCard CreditCard = new CreditCard(new NotEmpty(), new OnlyNumbers(), new LuhnValidate(), new CardNumber());
                ResponseData.Data = CreditCard.Validate(CardNumber);
                ResponseData.Status = 1000;
            }
            catch (Exception Ex) 
            {
                ResponseData.Status = 1001;
                ResponseData.Data = Ex.Message;
                await _coreService.AddErrorLogs(Ex);
            }

            return ResponseData;
        }
    }
}
