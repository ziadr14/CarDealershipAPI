using CarDealershipBLL.DTOs;
using CarDealershipDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipBLL.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<CarDto>> GetAllCarsAsync();
        Task<CarDto> GetCarByIdAsync(int id);
        Task<CreatCarDto> CreateCarAsync(CreatCarDto creatCar);
        Task<bool> UpdateCarAsync(int id, CarDto carDto);
        Task<bool> DeleteCarAsync(int id);
    }
}
