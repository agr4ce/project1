using System;
using System.Collections.Generic;

namespace GameStarts.Models;

public partial class User
{
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int IdRoleUser { get; set; }

    public virtual RoleUser IdRoleUserNavigation { get; set; } = null!;
}
