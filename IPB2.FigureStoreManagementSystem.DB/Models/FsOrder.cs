using System;
using System.Collections.Generic;

namespace IPB2.FigureStoreManagementSystem.DB.Models;

public partial class FsOrder
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Status { get; set; }

    public virtual FsCustomer? Customer { get; set; }

    public virtual ICollection<FsOrderDetail> FsOrderDetails { get; set; } = new List<FsOrderDetail>();

    public virtual ICollection<FsPayment> FsPayments { get; set; } = new List<FsPayment>();
}
