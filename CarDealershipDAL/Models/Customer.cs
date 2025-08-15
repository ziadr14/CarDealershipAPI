﻿using System;
using System.Collections.Generic;

namespace CarDealershipDAL.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FullName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
