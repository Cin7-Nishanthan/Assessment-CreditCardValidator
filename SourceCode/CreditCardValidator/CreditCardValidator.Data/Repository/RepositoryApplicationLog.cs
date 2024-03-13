using Core.Data;
using Microsoft.EntityFrameworkCore;
using CreditCardValidator.Data.Models;

namespace CreditCardValidator.Data.Repository
{
    public class RepositoryApplicationLog : IRepositoryLogger
    {
        #region " Vars "
        CreditCardValidatorContext _context;
        #endregion

        #region " Constructor "
        public RepositoryApplicationLog()
        {
            _context = new CreditCardValidatorContext();
        }
        #endregion

        #region " Methods "
        public int Log(int requestId, int step, string data, string requestData, string exceptionData, string responseData)
        {
            return _context.Database.SqlQuery<int>($"EXEC LogSave @requestId = {requestId}, @Step = {step}, @Data = {data}, @RequestData = {requestData}, @ExceptionData = {exceptionData}, @ResponseData = {responseData}"
                ).AsEnumerable().Select(cv => cv).First();
        }
        #endregion
    }
}