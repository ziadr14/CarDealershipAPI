using CarDealershipBLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipBLL.Interfaces
{
    public interface ISaleService
    {
            Task<IEnumerable<SaleDto>> GetAllSalesAsync();
            Task<SaleDto> GetSaleByIdAsync(int id);
            Task<SaleDto> CreateSaleAsync(SaleDto saleDto);
            Task<bool> UpdateSaleAsync(int id, SaleDto saleDto);
            Task<bool> DeleteSaleAsync(int id);
            Task<IEnumerable<SaleDto>> GetSalesByCustomerIdAsync(int customerId);
            Task<IEnumerable<SaleDto>> GetSalesByDateRangeAsync(DateTime start, DateTime end);
            Task<decimal> GetTotalRevenueAsync();
            Task<IEnumerable<TopSellingCarDto>> GetTopSellingCarsAsync(int count);
        }

    }

