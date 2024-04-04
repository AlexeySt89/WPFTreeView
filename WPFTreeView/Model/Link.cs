using System;
using System.Collections.Generic;

namespace WPFTreeView.Model;

public partial class Link
{
    public int ParentId { get; set; }

    public int ChildId { get; set; }

    public virtual Object Child { get; set; } = null!;

    public virtual Object Parent { get; set; } = null!;
}
