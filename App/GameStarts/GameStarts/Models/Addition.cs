using System;
using System.Collections.Generic;

namespace GameStarts.Models;

public partial class Addition
{
    public int IdAddition { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public byte MinAge { get; set; }

    public string? Description { get; set; }

    public byte[]? Picture { get; set; }

    public short Amount { get; set; }

    public int IdBoardGame { get; set; }

    public virtual BoardGame IdBoardGameNavigation { get; set; } = null!;

    public virtual ICollection<PurshasedAddition> PurshasedAdditions { get; set; } = new List<PurshasedAddition>();
}
