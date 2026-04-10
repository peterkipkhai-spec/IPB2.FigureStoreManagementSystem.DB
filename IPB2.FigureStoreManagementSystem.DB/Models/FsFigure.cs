using System;
using System.Collections.Generic;

namespace IPB2.FigureStoreManagementSystem.DB.Models;

public partial class FsFigure
{
    public int FigureId { get; set; }

    public string FigureName { get; set; } = null!;

    public int? CategoryId { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string? Description { get; set; }

    public virtual FsCategory? Category { get; set; }

    public virtual ICollection<FsOrderDetail> FsOrderDetails { get; set; } = new List<FsOrderDetail>();
}
