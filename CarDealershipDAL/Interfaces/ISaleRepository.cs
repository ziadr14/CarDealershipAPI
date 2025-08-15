using CarDealershipBLL.Interfaces;
using CarDealershipDAL.Models;

namespace CarDealershipDAL.Interfaces
{
    public interface ISaleRepository:IBaseRepository<Sale>
    {
        IQueryable<Sale> GetAllSales();
        IQueryable<Sale> GetSalesById(int id);
        IQueryable<Sale> GetSalesByCustomerId(int customerId);
        IQueryable<Sale> GetSalesByDateRange(DateTime start, DateTime end);
        Task<decimal> GetTotalRevenueAsync();
    }
}
