using System;
using System.Collections.Generic;

namespace CreditCardValidator.Data.Models;

public partial class ApplicationLog
{
    public int Id { get; set; }

    public DateTime TimeStamp { get; set; }

    public string Level { get; set; } = null!;

    public string? Message { get; set; }

    public string? Exception { get; set; }
}
