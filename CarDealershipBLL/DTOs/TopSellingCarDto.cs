using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipBLL.DTOs
{
    public class TopSellingCarDto
    {
        public int CarId { get; set; }
        public string Model { get; set; }
        public string BrandName { get; set; }
        public int TotalSold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
