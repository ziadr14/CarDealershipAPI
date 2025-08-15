using System;
using System.Collections.Generic;

namespace CarDealershipDAL.Models;

public partial class Sale
{
    public int SaleId { get; set; }

    public int CarId { get; set; }

    public int BranchId { get; set; }

    public int CustomerId { get; set; }

    public DateTime? SaleDate { get; set; }

    public decimal SalePrice { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Car Car { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
