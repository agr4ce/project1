using System;
using System.Collections.Generic;

namespace GameStarts.Models;

public partial class BoardGame
{
    public int IdBoardGame { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public byte MinAge { get; set; }

    public byte MinCountPlayers { get; set; }

    public byte? MaxCountPlayer { get; set; }

    public string? Description { get; set; }

    public TimeSpan? AverageTime { get; set; }

    public byte[]? Picture { get; set; }

    public short Amount { get; set; }

    public virtual ICollection<Addition> Additions { get; set; } = new List<Addition>();

    public virtual ICollection<PurshasedBoardGame> PurshasedBoardGames { get; set; } = new List<PurshasedBoardGame>();

    public virtual ICollection<Category> IdCategories { get; set; } = new List<Category>();
}
