using System;
using System.Collections.Generic;

namespace IPB2.FigureStoreManagementSystem.DB.Models;

public partial class FsPayment
{
    public int PaymentId { get; set; }

    public int? OrderId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public string? PaymentMethod { get; set; }

    public virtual FsOrder? Order { get; set; }
}
