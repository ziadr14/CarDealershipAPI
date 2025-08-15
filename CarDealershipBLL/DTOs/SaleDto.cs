using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipBLL.DTOs
{
    public class SaleDto
    {

        public int SaleId { get; set; }

        public int CarId { get; set; }

        public string CarModel { get; set; }

        public int BranchId { get; set; }
        public string BranchName { get; set; }

        public int CustomerId { get; set; }
        public string CoustomerName { get; set; }

        public decimal SalePrice { get; set; }
        public DateTime? SaleDate { get; set; }




        //public int Id { get; set; }
        //public int CarId { get; set; }
        //public string CarModel { get; set; }
        //public int CustomerId { get; set; }
        //public string CustomerName { get; set; }
        //public DateTime SaleDate { get; set; }
        //public decimal SalePrice { get; set; }
    }
}
