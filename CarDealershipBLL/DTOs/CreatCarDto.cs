using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipBLL.DTOs
{
    public class CreatCarDto
    {
        public int CarId { get; set; }

        public int BrandId { get; set; }

        public string Model { get; set; } = null!;

        public int Year { get; set; }

        public string? Color { get; set; }

        public decimal Price { get; set; }

        public int? Quantity { get; set; } = 1;
    }
}
