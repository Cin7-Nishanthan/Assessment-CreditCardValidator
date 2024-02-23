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

        private GenericRepository<Card> _cardRepository;
        private GenericRepository<CardValidation> _cardValidationRepository;

        public GenericRepository<Card> CardRepository
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

        public GenericRepository<CardValidation> CardValidationRepository
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


        public IEnumerable<CardValidation> GetCardValidationStoredProcedure(string procedureName, SqlParameter parameters)
        {
            return _context.Database.SqlQueryRaw<CardValidation>(procedureName, parameters);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
