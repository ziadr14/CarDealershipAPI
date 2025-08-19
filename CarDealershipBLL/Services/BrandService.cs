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
    public class BrandService : IBrandService
    {
        private readonly IMapper _mapper;
        private readonly IRepoWrapper _repo;

        public BrandService(IMapper mapper, IRepoWrapper repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            try
            {
                var brands = await _repo.Brands.GetAllBrands()
                    .Select(B => new BrandDto
                    {
                        Id = B.BrandId,
                        BrandName = B.BrandName,
                    }).ToListAsync();

                return brands;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting all brands", ex);
            }
        }

        public async Task<BrandDto> GetBrandByIdAsync(int id)
        {
            try
            {
                var brand = await _repo.Brands.GetBrandById(id)
                    .Select(B => new BrandDto
                    {
                        Id = B.BrandId,
                        BrandName = B.BrandName,
                    }).FirstOrDefaultAsync();

                return brand;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while getting brand with id {id}", ex);
            }
        }

        public async Task<BrandDto> CreateBrandAsync(BrandDto brandDto)
        {
            try
            {
                var newBrand = _mapper.Map<Brand>(brandDto);

                await _repo.Brands.AddAsync(newBrand);
                await _repo.SaveAsync();

                return _mapper.Map<BrandDto>(newBrand);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating brand", ex);
            }
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            try
            {
                var brand = await _repo.Brands.GetByIdAsync(id);
                if (brand == null) return false;

                _repo.Brands.DeleteAsync(brand);
                await _repo.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while deleting brand with id {id}", ex);
            }
        }

        public async Task<bool> UpdateBrandAsync(int id, BrandDto brandDto)
        {
            try
            {
                var brand = await _repo.Brands.GetByIdAsync(id);
                if (brand == null) return false;

                _mapper.Map(brandDto, brand);
                _repo.Brands.UpdateAsync(brand);
                await _repo.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while updating brand with id {id}", ex);
            }
        }
    }
}
