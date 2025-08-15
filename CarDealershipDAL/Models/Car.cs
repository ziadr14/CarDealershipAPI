using System;
using System.Collections.Generic;

namespace CarDealershipDAL.Models;

public partial class Car
{
    public int CarId { get; set; }

    public int BrandId { get; set; }

    public string Model { get; set; } = null!;

    public int Year { get; set; }

    public string? Color { get; set; }

    public decimal Price { get; set; }

    public int? Quantity { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
