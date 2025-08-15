using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipBLL.DTOs
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }

        public string FullName { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }
    }
}
