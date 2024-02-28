using CreditCardValidator.Data.CustomModels;
using CreditCardValidator.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly CreditCardValidatorContext _context;

        public UnitOfWork(CreditCardValidatorContext context)
        {
            _context = context;
        }

        private IRepository<Card> _cardRepository;
        private IRepository<CardValidation> _cardValidationRepository;
        private IRepository<ApplicationLog> _applicationLogRepository;

        public IRepository<Card> CardRepository
        {
            get
            {
                try
                {
                    if (_cardRepository == null)
                    {
                        _cardRepository = new GenericRepository<Card>(_context);
                    }
                    return _cardRepository;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    throw;
                }
            }
        }

        public IRepository<CardValidation> CardValidationRepository
        {
            get
            {
                try
                {
                    if (_cardValidationRepository == null)
                    {
                        _cardValidationRepository = new GenericRepository<CardValidation>(_context);
                    }
                    return _cardValidationRepository;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    throw;
                }
            }
        }

        public IRepository<ApplicationLog> ApplicationLogRepository
        {
            get
            {
                try
                {
                    if (_applicationLogRepository == null)
                    {
                        _applicationLogRepository = new GenericRepository<ApplicationLog>(_context);
                    }
                    return _applicationLogRepository;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    throw;
                }
            }
        }

        public IEnumerable<CardValidations> GetCardValidationStoredProcedure(string procedureName, SqlParameter parameters)
        {
                return _context.Database.SqlQueryRaw<CardValidations>(procedureName, parameters);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
