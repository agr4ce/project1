using System;
using System.Collections.Generic;

namespace GameStarts.Models;

public partial class PurshasedAddition
{
    public int IdAddition { get; set; }

    public int IdPurshases { get; set; }

    public short Amount { get; set; }

    public virtual Addition IdAdditionNavigation { get; set; } = null!;

    public virtual Purshase IdPurshasesNavigation { get; set; } = null!;
}
