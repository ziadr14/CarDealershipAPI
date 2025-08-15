using CarDealershipBLL.DTOs;
using CarDealershipDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipBLL.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        Task<BrandDto> GetBrandByIdAsync(int id);
        Task<BrandDto> CreateBrandAsync(BrandDto creatCar);
        Task<bool> UpdateBrandAsync(int id, BrandDto carDto);
        Task<bool> DeleteBrandAsync(int id);
    }
}
