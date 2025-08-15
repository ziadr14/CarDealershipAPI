using CarDealershipDAL.Interfaces;
using CarDealershipDAL.Models;
using CarDealershipDAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealershipDAL.Repositories
{
    public class SaleRepository : BaseRepository<Sale>, ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Sale> GetAllSales()
        {
            return _context.Sales
                .Include(s => s.Car)
                    .ThenInclude(c => c.Brand)
                .Include(s => s.Customer);
        }



        public IQueryable<Sale> GetSalesById(int id)
        {
            return _context.Sales.Where(b => b.SaleId == id).AsQueryable();

        }

        public IQueryable<Sale> GetSalesByCustomerId(int customerId)
        {
            return _context.Sales
                .Where(s => s.CustomerId == customerId)
                .Include(s => s.Car)
                    .ThenInclude(c => c.Brand)
                .Include(s => s.Customer);
        }

        public IQueryable<Sale> GetSalesByDateRange(DateTime start, DateTime end)
        {
            return _context.Sales
                .Where(s => s.SaleDate >= start && s.SaleDate <= end)
                .Include(s => s.Car)
                    .ThenInclude(c => c.Brand)
                .Include(s => s.Customer);
        }



        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Sales.SumAsync(s => s.SalePrice);
        }


    }
}
