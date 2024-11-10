using System;
using System.Collections.Generic;

namespace BlazorRest;

public partial class User
{
    public int Id { get; set; }

    public string? NameUser { get; set; }

    public virtual ICollection<CrossProductUser> CrossProductUsers { get; set; } = new List<CrossProductUser>();
}
