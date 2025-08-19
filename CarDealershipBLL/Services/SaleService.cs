using AutoMapper;
using CarDealershipBLL.DTOs;
using CarDealershipBLL.Interfaces;
using CarDealershipDAL.Interfaces;
using CarDealershipDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealershipBLL.Services
{
    public class SaleService : ISaleService
    {
        private readonly IMapper _mapper;
        private readonly IRepoWrapper _repo;

        public SaleService(IMapper mapper, IRepoWrapper repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<IEnumerable<SaleDto>> GetAllSalesAsync()
        {
            try
            {
                var sales = await _repo.Sales.GetAllSales()
                    .Include(s => s.Car)
                    .Include(s => s.Customer)
                    .Select(s => new SaleDto
                    {
                        SaleId = s.SaleId,
                        BranchId = s.BranchId,
                        BranchName = s.Branch.BranchName,
                        CarId = s.CarId,
                        CarModel = s.Car.Model,
                        CustomerId = s.CustomerId,
                        CoustomerName = s.Customer.FullName,
                        SalePrice = s.SalePrice,
                        SaleDate = s.SaleDate,
                    }).ToListAsync();

                return sales;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllSalesAsync: {ex.Message}");
                return new List<SaleDto>();
            }
        }

        public async Task<SaleDto> GetSaleByIdAsync(int id)
        {
            try
            {
                var sale = await _repo.Sales.GetSalesById(id).Select(s => new SaleDto
                {
                    SaleId = s.SaleId,
                    BranchId = s.BranchId,
                    BranchName = s.Branch.BranchName,
                    CarId = s.CarId,
                    CarModel = s.Car.Model,
                    CustomerId = s.CustomerId,
                    CoustomerName = s.Customer.FullName,
                    SalePrice = s.SalePrice,
                    SaleDate = s.SaleDate,
                }).FirstOrDefaultAsync();

                return sale;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetSaleByIdAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<SaleDto> CreateSaleAsync(SaleDto saleDto)
        {
            try
            {
                var sale = _mapper.Map<Sale>(saleDto);

                await _repo.Sales.AddAsync(sale);
                await _repo.SaveAsync();
                return _mapper.Map<SaleDto>(sale);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateSaleAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateSaleAsync(int id, SaleDto saleDto)
        {
            try
            {
                var existingSale = await _repo.Sales.GetByIdAsync(id);
                if (existingSale == null) return false;

                _mapper.Map(saleDto, existingSale);
                _repo.Sales.UpdateAsync(existingSale);
                await _repo.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateSaleAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            try
            {
                var existingSale = await _repo.Sales.GetByIdAsync(id);
                if (existingSale == null) return false;

                _repo.Sales.DeleteAsync(existingSale);
                await _repo.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteSaleAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<SaleDto>> GetSalesByCustomerIdAsync(int customerId)
        {
            try
            {
                var sales = await _repo.Sales.GetAllSales()
                    .Where(s => s.CustomerId == customerId)
                    .Include(s => s.Car)
                    .Include(s => s.Customer)
                    .Select(s => new SaleDto
                    {
                        SaleId = s.SaleId,
                        BranchId = s.BranchId,
                        BranchName = s.Branch.BranchName,
                        CarId = s.CarId,
                        CarModel = s.Car.Model,
                        CustomerId = s.CustomerId,
                        CoustomerName = s.Customer.FullName,
                        SalePrice = s.SalePrice,
                        SaleDate = s.SaleDate,
                    }).ToListAsync();

                return sales;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetSalesByCustomerIdAsync: {ex.Message}");
                return new List<SaleDto>();
            }
        }

        public async Task<IEnumerable<SaleDto>> GetSalesByDateRangeAsync(DateTime start, DateTime end)
        {
            try
            {
                var sales = await _repo.Sales.GetAllSales()
                    .Where(s => s.SaleDate >= start && s.SaleDate <= end)
                    .Include(s => s.Car)
                    .Include(s => s.Customer)
                    .Select(s => new SaleDto
                    {
                        SaleId = s.SaleId,
                        BranchId = s.BranchId,
                        BranchName = s.Branch.BranchName,
                        CarId = s.CarId,
                        CarModel = s.Car.Model,
                        CustomerId = s.CustomerId,
                        CoustomerName = s.Customer.FullName,
                        SalePrice = s.SalePrice,
                        SaleDate = s.SaleDate,
                    }).ToListAsync();

                return sales;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetSalesByDateRangeAsync: {ex.Message}");
                return new List<SaleDto>();
            }
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            try
            {
                return await _repo.Sales.GetAllSales().SumAsync(s => s.SalePrice);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetTotalRevenueAsync: {ex.Message}");
                return 0;
            }
        }

        public async Task<IEnumerable<TopSellingCarDto>> GetTopSellingCarsAsync(int count)
        {
            try
            {
                var topCars = await _repo.Sales.GetAllSales()
                    .Include(s => s.Car)
                    .ThenInclude(c => c.Brand)
                    .GroupBy(s => new { s.CarId, s.Car.Model, s.Car.Brand.BrandName })
                    .Select(g => new TopSellingCarDto
                    {
                        CarId = g.Key.CarId,
                        Model = g.Key.Model,
                        BrandName = g.Key.BrandName,
                        TotalSold = g.Count(),
                        TotalRevenue = g.Sum(s => s.SalePrice)
                    })
                    .OrderByDescending(tc => tc.TotalSold)
                    .Take(count)
                    .ToListAsync();

                return topCars;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetTopSellingCarsAsync: {ex.Message}");
                return new List<TopSellingCarDto>();
            }
        }
    }
}
