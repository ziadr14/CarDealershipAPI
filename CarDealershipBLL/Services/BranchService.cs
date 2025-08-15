using AutoMapper;
using CarDealershipBLL.DTOs;
using CarDealershipBLL.Interfaces;
using CarDealershipDAL.Interfaces;
using CarDealershipDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipBLL.Services
{
    public class BranchService : IBranchService
    {

        private readonly IMapper _mapper;
        private readonly IRepoWrapper _repo;

        public BranchService(IMapper mapper, IRepoWrapper repo)
        {
            _mapper = mapper;
            _repo = repo;
        }



        public async Task<IEnumerable<BranchDto>> GetAllBranchsAsync()
        {
            var branches = await _repo.Branches.GetAllBranchs().Select(R => new BranchDto
            {
                BranchId = R.BranchId,
                BranchName = R.BranchName,
                Address = R.Address,
                PhoneNumber = R.PhoneNumber,
                
                
            }).ToListAsync();

            return branches;
            
        }

        public async Task<BranchDto> GetBranchByIdAsync(int id)
        {
            var branch = await _repo.Branches.GetBranchById(id).Select(R => new BranchDto
            {
                BranchId = R.BranchId,
                BranchName = R.BranchName,
                Address = R.Address,
                PhoneNumber = R.PhoneNumber,
            }).FirstOrDefaultAsync();

            return branch;
        }



        public async Task<BranchDto> CreateBranchAsync(BranchDto branchDto)
        {
            var newBranch = _mapper.Map<Branch>(branchDto);

            await _repo.Branches.AddAsync(newBranch);
            await _repo.SaveAsync();
            return _mapper.Map<BranchDto>(newBranch);
        }

        public async Task<bool> DeleteBranchAsync(int id)
        {
            var branch = await _repo.Branches.GetByIdAsync(id);
            if (branch == null) return false;

            _repo.Branches.DeleteAsync(branch);
            await _repo.SaveAsync();
            return true;
        }




        public async Task<bool> UpdateBranchAsync(int id, BranchDto branchDto)
        {
            var branch = await _repo.Branches.GetByIdAsync(id);
            if (branch == null) return false;
            _mapper.Map(branchDto, branch);
            _repo.Branches.UpdateAsync(branch);
            await _repo.SaveAsync();
            return true;
        }
    }
}
