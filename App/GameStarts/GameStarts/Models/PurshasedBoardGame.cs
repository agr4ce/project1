using System;
using System.Collections.Generic;

namespace GameStarts.Models;

public partial class PurshasedBoardGame
{
    public int IdBoardGame { get; set; }

    public int IdPurshases { get; set; }

    public short Amount { get; set; }

    public virtual BoardGame IdBoardGameNavigation { get; set; } = null!;

    public virtual Purshase IdPurshasesNavigation { get; set; } = null!;
}
