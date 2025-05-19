using System;
using System.Collections.Generic;

namespace GameStarts.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Genre { get; set; }

    public virtual ICollection<BoardGame> IdBoardGames { get; set; } = new List<BoardGame>();
}
