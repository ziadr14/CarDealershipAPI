using AutoMapper;
using CarDealershipBLL.DTOs;
using CarDealershipBLL.Interfaces;
using CarDealershipDAL.Interfaces;
using CarDealershipDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipBLL.Services
{
    public class CarService : ICarService
    {
        private readonly IMapper _mapper;
        private readonly IRepoWrapper _repo;

        public CarService(IMapper mapper, IRepoWrapper repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<IEnumerable<CarDto>> GetAllCarsAsync()
        {
            try
            {
                var cars = await _repo.Cars.GetAllCars()
                    .Include(c => c.Brand)
                    .Select(c => new CarDto
                    {
                        CarId = c.CarId,
                        Color = c.Color,
                        Model = c.Model,
                        Quantity = c.Quantity,
                        Price = c.Price,
                        Year = c.Year,
                        BrandId = c.BrandId,
                        BrandName = c.Brand.BrandName
                    })
                    .ToListAsync();

                return cars;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting all cars", ex);
            }
        }

        public async Task<CarDto> GetCarByIdAsync(int id)
        {
            try
            {
                var car = await _repo.Cars.GetCarsById(id)
                    .Select(c => new CarDto
                    {
                        CarId = c.CarId,
                        Color = c.Color,
                        Model = c.Model,
                        Quantity = c.Quantity,
                        Price = c.Price,
                        Year = c.Year,
                        BrandId = c.Brand.BrandId
                    })
                    .FirstOrDefaultAsync();

                return car;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while getting car with id {id}", ex);
            }
        }

        public async Task<CreatCarDto> CreateCarAsync(CreatCarDto createCar)
        {
            try
            {
                // التحقق من وجود البراند
                var brand = await _repo.Brands.GetByIdAsync(createCar.BrandId);
                if (brand == null)
                    throw new ArgumentException($"Brand with ID {createCar.BrandId} not found.");

                // إنشاء السيارة
                var newCar = _mapper.Map<Car>(createCar);
                newCar.Brand = null; // منع EF من محاولة إضافة Brand جديد

                await _repo.Cars.AddAsync(newCar);
                await _repo.SaveAsync();

                return _mapper.Map<CreatCarDto>(newCar);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating car", ex);
            }
        }

        public async Task<bool> UpdateCarAsync(int id, CarDto carDto)
        {
            try
            {
                var car = await _repo.Cars.GetByIdAsync(id);
                if (car == null) return false;

                _mapper.Map(carDto, car);
                _repo.Cars.UpdateAsync(car);
                await _repo.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while updating car with id {id}", ex);
            }
        }

        public async Task<bool> DeleteCarAsync(int id)
        {
            try
            {
                var car = await _repo.Cars.GetByIdAsync(id);
                if (car == null) return false;

                _repo.Cars.DeleteAsync(car);
                await _repo.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while deleting car with id {id}", ex);
            }
        }
    }
}
