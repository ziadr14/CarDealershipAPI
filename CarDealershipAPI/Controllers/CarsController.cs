using CarDealershipBLL.DTOs;
using CarDealershipBLL.Interfaces;
using CarDealershipDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<CarDto>>> GetAllCars()
        {
            var cars = await _carService.GetAllCarsAsync();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> getCarsById(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<CarDto>> CreateCar(CreatCarDto createCar)
        {
            var createdCar = await _carService.CreateCarAsync(createCar);
            return CreatedAtAction(nameof(getCarsById), new { id = createdCar.CarId }, createdCar);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateCar(int id, CarDto carDto)
        {
            var car = await _carService.UpdateCarAsync(id, carDto);
            if (!car)
                return NotFound();

            return NoContent();
        }



        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _carService.DeleteCarAsync(id);
            if (!car)
                return NotFound();

            return NoContent();
        }
    }
}