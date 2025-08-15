using CarDealershipBLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipBLL.Interfaces
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchDto>> GetAllBranchsAsync();
        Task<BranchDto> GetBranchByIdAsync(int id);

        Task<BranchDto> CreateBranchAsync(BranchDto branchDto);
        Task<bool> UpdateBranchAsync(int id, BranchDto branchDto);
        Task<bool> DeleteBranchAsync(int id);
    }
}
