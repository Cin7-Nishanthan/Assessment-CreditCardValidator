using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Data.CustomModels
{
    public class CardValidations
    {
        public int Id { get; set; }

        public string StartingNumber { get; set; } = null!;

        public int Length { get; set; }

        public int CardId { get; set; }
    }
}
