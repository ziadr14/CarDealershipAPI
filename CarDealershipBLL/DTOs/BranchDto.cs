using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipBLL.DTOs
{
    public class BranchDto
    {
        public int BranchId { get; set; }

        public string BranchName { get; set; } = null!;

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
