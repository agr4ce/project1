using System;
using System.Collections.Generic;

namespace GameStarts.Models;

public partial class RoleUser
{
    public int IdRoleUser { get; set; }

    public string NameRoleUser { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
