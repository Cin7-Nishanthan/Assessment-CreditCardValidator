using System;
using System.Collections.Generic;

namespace CreditCardValidator.Data.Models;

public partial class Card
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public byte Status { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<CardValidation> CardValidations { get; set; } = new List<CardValidation>();
}
