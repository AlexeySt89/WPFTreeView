using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WPFTreeView.Model;

public partial class Object
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Product { get; set; } = null!;

    public virtual ObservableCollection<Attribute> Attributes { get; set; } = new ObservableCollection<Attribute>();
}
