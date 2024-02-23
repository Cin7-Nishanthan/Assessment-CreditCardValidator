using System;
using System.Collections.Generic;

namespace CreditCardValidator.Data.Models;

public partial class CardValidation
{
    public int Id { get; set; }

    public string StartingNumber { get; set; } = null!;

    public int Length { get; set; }

    public int CardId { get; set; }

    public byte Status { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Card Card { get; set; } = null!;
}
