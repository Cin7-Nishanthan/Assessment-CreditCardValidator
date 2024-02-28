using CreditCardValidator.Data;
using CreditCardValidator.Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Core.Logger
{
    public class DbLogger : ILogger
    {
        private readonly UnitOfWork _unitOfWork;

        public DbLogger(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var logEntry = new ApplicationLog
            {
                TimeStamp = DateTime.Now,
                Level = logLevel.ToString(),
                Message = formatter(state, exception),
                Exception = exception?.ToString()
            };

            _unitOfWork.ApplicationLogRepository.Insert(logEntry);
            _unitOfWork.Save();
        }
    }
}
