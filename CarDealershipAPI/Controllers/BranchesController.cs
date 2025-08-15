using CarDealershipBLL.DTOs;
using CarDealershipBLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarDealershipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchService _branchServic;

        public BranchesController(IBranchService branchServic)
        {
            _branchServic = branchServic;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var branches = await _branchServic.GetAllBranchsAsync();
            return Ok(branches);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var branch = await _branchServic.GetBranchByIdAsync(id);
            if (branch == null)
                return NotFound();

            return Ok(branch);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create([FromBody] BranchDto branchDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdBranch = await _branchServic.CreateBranchAsync(branchDto);
            return CreatedAtAction(nameof(GetById), new { id = createdBranch.BranchId}, createdBranch);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update(int id, [FromBody] BranchDto branchDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _branchServic.UpdateBranchAsync(id, branchDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _branchServic.DeleteBranchAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
