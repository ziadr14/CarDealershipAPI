using CarDealershipBLL.DTOs;
using CarDealershipBLL.Interfaces;
using CarDealershipBLL.Services;
using CarDealershipDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAll()
        {
            var brands =  await _brandService.GetAllBrandsAsync();

            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandDto>> GetById(int id)
        {
            var brand = await _brandService.GetBrandByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]


        public async Task<ActionResult<BrandDto>> CreateBrand(BrandDto brandDto)
        {
            var newBrand = await _brandService.CreateBrandAsync(brandDto);
            return CreatedAtAction(nameof(GetById), new { id = newBrand.Id }, newBrand);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBrand(int id , BrandDto brandDto)
        {
            var brand = await _brandService.UpdateBrandAsync(id, brandDto);
            if (!brand)
                return NotFound();

            return NoContent();

        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]


        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _brandService.DeleteBrandAsync(id);
            if (!brand)
                return NotFound();

            return NoContent();
        }

    }
}
