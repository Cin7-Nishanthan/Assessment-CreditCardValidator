using System;
using System.Collections.Generic;

namespace CreditCardValidator.Data.Models;

public partial class LogResponse
{
    public int Id { get; set; }

    public int RequestId { get; set; }

    public string? Data { get; set; }

    public DateTime DateCreated { get; set; }
}
