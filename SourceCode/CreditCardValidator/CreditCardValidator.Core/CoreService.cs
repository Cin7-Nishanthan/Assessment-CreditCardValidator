using Microsoft.Extensions.Logging;

namespace CreditCardValidator.Core
{
    public class CoreService
    {
        private readonly ILogger<CoreService> _logger;

        public CoreService(ILogger<CoreService> logger)
        {
            _logger = logger;
        }

        public async Task AddInfoLogs(string message)
        {
           _logger.LogInformation(message);
            await Task.Delay(1000);
        }

        public async Task AddErrorLogs(Exception exception)
        {
            _logger.LogInformation(exception, exception.Message);
            await Task.Delay(1000);
        }
    }
}
