using CarDealershipBLL.Interfaces;
using CarDealershipDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipDAL.Interfaces
{
    public interface ICustomerRepository:IBaseRepository<Customer>
    {
        IQueryable<Customer> GetAllCustomer();
        IQueryable<Customer> GetCustomerById(int id);

    }
}
