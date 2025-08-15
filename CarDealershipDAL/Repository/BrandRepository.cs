
using CarDealershipDAL.Interfaces;
using CarDealershipDAL.Models;
using CarDealershipDAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace CarDealership.DAL.Repositories
{
    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext context) : base(context) { }

        public IQueryable<Brand> GetAllBrands()
        {
            return _context.Brands.AsQueryable();


        }

        public IQueryable<Brand> GetBrandById(int id)
        {
            return _context.Brands.Where(b => b.BrandId == id).AsQueryable();
        }
    }
}
