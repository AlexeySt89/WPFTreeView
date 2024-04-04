using System;
using System.Collections.Generic;

namespace WPFTreeView.Model;

public partial class Attribute
{
    public int ObjectId { get; set; }

    public string Name { get; set; } = null!;

    public string Value { get; set; } = null!;

    public virtual Object Object { get; set; } = null!;
}
