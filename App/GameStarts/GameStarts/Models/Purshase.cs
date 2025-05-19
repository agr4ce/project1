using System;
using System.Collections.Generic;

namespace GameStarts.Models;

public partial class Purshase
{
    public int IdPurshases { get; set; }

    public DateTime DateTime { get; set; }

    public string BuyersSurname { get; set; } = null!;

    public string BuyersName { get; set; } = null!;

    public string? BuyersPatronymic { get; set; }

    public string DeliveryAddress { get; set; } = null!;

    public string? Email { get; set; }

    public string Phone { get; set; } = null!;

    public virtual ICollection<PurshasedAddition> PurshasedAdditions { get; set; } = new List<PurshasedAddition>();

    public virtual ICollection<PurshasedBoardGame> PurshasedBoardGames { get; set; } = new List<PurshasedBoardGame>();
}
