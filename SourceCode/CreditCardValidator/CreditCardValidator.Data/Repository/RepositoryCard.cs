using CreditCardValidator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Data.Repository
{
    public class RepositoryCard : IRepositoryCard
    {
        CreditCardValidatorContext _context;

        public RepositoryCard()
        {
            _context = new CreditCardValidatorContext();
        }

        public List<Card> Get()
        {
            return _context.Cards.Where(c => c.Status == 1 && c.IsDeleted == false).ToList();
        }

        public Card Get(int id)
        {
            return _context.Cards.FirstOrDefault(c => c.Id == id && c.Status == 1 && c.IsDeleted == false);
        }
    }
}
