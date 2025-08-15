using AutoMapper;
using CarDealershipBLL.DTOs;
using CarDealershipBLL.Interfaces;
using CarDealershipDAL.Interfaces;
using CarDealershipDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipBLL.Services
{
    public class BrandService : IBrandService
    {
        private readonly IMapper _mapper;
        private readonly IRepoWrapper _repo;

        public BrandService(IMapper mapper , IRepoWrapper repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var brands = await _repo.Brands.GetAllBrands().Select(B => new BrandDto
            {
                Id = B.BrandId,
                BrandName = B.BrandName,    
            }).ToListAsync();

            return brands;

        }



        public async Task<BrandDto> GetBrandByIdAsync(int id)
        {
            var brands = await _repo.Brands.GetBrandById(id).Select(B => new BrandDto
            {
                Id = B.BrandId,
                BrandName = B.BrandName,

            }).FirstOrDefaultAsync();
            
            return brands;
        }




        public async Task<BrandDto> CreateBrandAsync(BrandDto brandDto)
        {
            var newBrand = _mapper.Map<Brand>(brandDto);

            await _repo.Brands.AddAsync(newBrand);
            await _repo.SaveAsync();
            return _mapper.Map<BrandDto>(newBrand);
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            var brand = await _repo.Brands.GetByIdAsync(id);
            if (brand == null) return false;

            _repo.Brands.DeleteAsync(brand);
            await _repo.SaveAsync();
            return true;
        }





        public async Task<bool> UpdateBrandAsync(int id, BrandDto brandDto)
        {
            var brand = await _repo.Brands.GetByIdAsync(id);
            if (brand == null) return false;
            _mapper.Map(brandDto, brand);
            _repo.Brands.UpdateAsync(brand);
            await _repo.SaveAsync();
            return true;
        }
    }
}
