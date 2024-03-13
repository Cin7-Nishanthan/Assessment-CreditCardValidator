using Microsoft.AspNetCore.Mvc;


namespace Core.API
{
    public class BaseController : ControllerBase
    {
        #region " Vars "
        protected ResponseData _responseData;
        protected ILogger _logger;
        #endregion

        #region " Constructor "
        public BaseController(ILogger logger) 
        {
            _logger = logger;
            _responseData = new ResponseData();
        }
        #endregion
    }
}
