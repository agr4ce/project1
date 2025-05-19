using System;
using System.Collections.Generic;

namespace GameStarts.Models;

public partial class MonthOrder
{
    public int IdPurshases { get; set; }

    public DateTime DateTime { get; set; }

    public string Buyer { get; set; } = null!;

    public decimal BoardGamesPrice { get; set; }

    public decimal AdditionGamesPrice { get; set; }

    public decimal? OrderPrice { get; set; }
}
