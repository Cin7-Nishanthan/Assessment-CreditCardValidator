using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public interface IRepositoryLogger
    {
        int Log(int requestId, int step, string data, string? requestData = null, string? exceptionData = null, string? responseData = null);
    }
}
