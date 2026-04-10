using System;
using System.Collections.Generic;

namespace IPB2.FigureStoreManagementSystem.DB.Models;

public partial class FsCustomer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<FsOrder> FsOrders { get; set; } = new List<FsOrder>();
}
