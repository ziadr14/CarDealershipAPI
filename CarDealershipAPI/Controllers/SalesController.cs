using CarDealershipBLL.DTOs;
using CarDealershipBLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CarDealershipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _saleService.GetAllSalesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null) return NotFound();
            return Ok(sale);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaleDto saleDto)
        {
            var created = await _saleService.CreateSaleAsync(saleDto);
            return CreatedAtAction(nameof(GetById), new { id = created.SaleId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SaleDto saleDto)
        {
            var updated = await _saleService.UpdateSaleAsync(id, saleDto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _saleService.DeleteSaleAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetByCustomer(int customerId)
        {
            return Ok(await _saleService.GetSalesByCustomerIdAsync(customerId));
        }

        [HttpGet("daterange")]
        public async Task<IActionResult> GetByDateRange(DateTime start, DateTime end)
        {
            return Ok(await _saleService.GetSalesByDateRangeAsync(start, end));
        }

        [HttpGet("revenue/total")]
        public async Task<IActionResult> GetTotalRevenue()
        {
            return Ok(await _saleService.GetTotalRevenueAsync());
        }

        [HttpGet("topcars/{count}")]
        public async Task<IActionResult> GetTopSellingCars(int count)
        {
            return Ok(await _saleService.GetTopSellingCarsAsync(count));
        }
    }
}
