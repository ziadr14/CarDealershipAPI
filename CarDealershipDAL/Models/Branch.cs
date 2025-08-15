using System;
using System.Collections.Generic;

namespace CarDealershipDAL.Models;

public partial class Branch
{
    public int BranchId { get; set; }

    public string BranchName { get; set; } = null!;

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
