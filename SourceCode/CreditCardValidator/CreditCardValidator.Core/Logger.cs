using Core;
using Core.Data;
using CreditCardValidator.Data.Repository;

namespace CreditCardValidator.Utils
{
    public class Logger: ILogger
    {
        #region " Vars "
        IRepositoryLogger _logger;
        int _requestId;
        #endregion

        #region " Constructor "
        public Logger() 
        {
            _logger = new RepositoryApplicationLog();
            _requestId = 0;   
        }
        #endregion

        #region " Methods "
        public void Log(int step, string data, string? requestData = null, string? exceptionData = null, string? responseData = null)
        {
            try
            {
                _requestId = _logger.Log(_requestId, step, data, requestData, exceptionData, responseData);
            }
            catch
            {

            }
        }
        #endregion
    }
}
