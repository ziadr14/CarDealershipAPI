using CarDealershipDAL.Interfaces;
using CarDealershipDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipDAL.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Customer> GetAllCustomer()
        {
            return _context.Customers.AsQueryable();
        }

        public IQueryable<Customer> GetCustomerById(int id)
        {
            return _context.Customers.Where(C => C.CustomerId ==id).AsQueryable();    
        }
    }
}
