using CreditCardValidator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Data.Repository
{
    public interface IRepositoryCard
    {
        List<Card> Get();
        Card Get(int id);
    }
}
