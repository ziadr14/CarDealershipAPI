using AutoMapper;
using CarDealershipBLL.DTOs;
using CarDealershipBLL.Interfaces;
using CarDealershipDAL.Interfaces;
using CarDealershipDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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

        public async Task<SaleDto> GetSaleByIdAsync(int id)
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

        public async Task<SaleDto> CreateSaleAsync(SaleDto saleDto)
        {

            var sale = _mapper.Map<Sale>(saleDto);

            await _repo.Sales.AddAsync(sale);
            await _repo.SaveAsync();
            return _mapper.Map<SaleDto>(sale);
        }

        public async Task<bool> UpdateSaleAsync(int id, SaleDto saleDto)
        {
            var existingSale = await _repo.Sales.GetByIdAsync(id);
            if (existingSale == null) return false;

            _mapper.Map(saleDto, existingSale);
            _repo.Sales.UpdateAsync(existingSale);
            await _repo.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            var existingSale = await _repo.Sales.GetByIdAsync(id);
            if (existingSale == null) return false;

            _repo.Sales.DeleteAsync(existingSale);
            await _repo.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<SaleDto>> GetSalesByCustomerIdAsync(int customerId)
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

        public async Task<IEnumerable<SaleDto>> GetSalesByDateRangeAsync(DateTime start, DateTime end)
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

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _repo.Sales.GetAllSales().SumAsync(s => s.SalePrice);
        }

        public async Task<IEnumerable<TopSellingCarDto>> GetTopSellingCarsAsync(int count)
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


    }
}
