using CarDealershipDAL.Interfaces;
using CarDealershipDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipDAL.Repository
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Car> GetAllCars()
        {
            return _context.Cars.AsQueryable(); 
        }

        public IQueryable<Car> GetCarsById(int id)
        {
            return _context.Cars
                           .Include(c => c.Brand) // يجيب البراند
                           .Where(c => c.CarId == id)
                           .AsQueryable();
        }
    }
}
