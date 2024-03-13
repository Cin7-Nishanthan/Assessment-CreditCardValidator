using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Data.Repository
{
    public interface IRepositoryApplicationLog
    {
        void Log(string Level, string Message, string Exception);
    }
}
