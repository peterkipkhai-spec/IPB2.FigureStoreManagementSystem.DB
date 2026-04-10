using System;
using System.Collections.Generic;

namespace IPB2.FigureStoreManagementSystem.DB.Models;

public partial class FsCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<FsFigure> FsFigures { get; set; } = new List<FsFigure>();
}
