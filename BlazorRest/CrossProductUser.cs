using System;
using System.Collections.Generic;

namespace BlazorRest;

public partial class CrossProductUser
{
    public int IdProduct { get; set; }

    public int? IdUser { get; set; }

    public int CountProduct { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual User? IdUserNavigation { get; set; }
}
