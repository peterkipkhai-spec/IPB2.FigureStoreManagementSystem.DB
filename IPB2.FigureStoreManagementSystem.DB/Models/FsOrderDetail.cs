using System;
using System.Collections.Generic;

namespace IPB2.FigureStoreManagementSystem.DB.Models;

public partial class FsOrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int FigureId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Subtotal { get; set; }

    public virtual FsFigure Figure { get; set; } = null!;

    public virtual FsOrder Order { get; set; } = null!;
}
