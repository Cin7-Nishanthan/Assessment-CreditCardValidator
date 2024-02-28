using CreditCardValidator.Core.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Contracts;

namespace CreditCardValidator.Core
{
    public static class CoreServiceExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<CoreService>();
            services.AddSingleton<ILoggerProvider, DbLoggerProvider>();
            services.AddLogging();
        }
    }
}
