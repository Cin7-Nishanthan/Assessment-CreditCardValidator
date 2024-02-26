using CreditCardValidator.Data;
using CreditCardValidator.Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.API.Logger
{
    public class DbLoggerProvider : ILoggerProvider
    {
        private readonly UnitOfWork _unitOfWork;

        public DbLoggerProvider()
        {
            _unitOfWork = new UnitOfWork(new CreditCardValidatorContext());
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger(_unitOfWork);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
