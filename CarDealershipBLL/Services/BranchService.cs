using AutoMapper;
using CarDealershipBLL.DTOs;
using CarDealershipBLL.Interfaces;
using CarDealershipDAL.Interfaces;
using CarDealershipDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            try
            {
                var branches = await _repo.Branches.GetAllBranchs()
                    .Select(R => new BranchDto
                    {
                        BranchId = R.BranchId,
                        BranchName = R.BranchName,
                        Address = R.Address,
                        PhoneNumber = R.PhoneNumber,
                    }).ToListAsync();

                return branches;
            }
            catch (Exception ex)
            {
                // ممكن تعمل logging هنا
                throw new Exception("Error while getting all branches", ex);
            }
        }

        public async Task<BranchDto> GetBranchByIdAsync(int id)
        {
            try
            {
                var branch = await _repo.Branches.GetBranchById(id)
                    .Select(R => new BranchDto
                    {
                        BranchId = R.BranchId,
                        BranchName = R.BranchName,
                        Address = R.Address,
                        PhoneNumber = R.PhoneNumber,
                    }).FirstOrDefaultAsync();

                return branch;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while getting branch with id {id}", ex);
            }
        }

        public async Task<BranchDto> CreateBranchAsync(BranchDto branchDto)
        {
            try
            {
                var newBranch = _mapper.Map<Branch>(branchDto);

                await _repo.Branches.AddAsync(newBranch);
                await _repo.SaveAsync();

                return _mapper.Map<BranchDto>(newBranch);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating branch", ex);
            }
        }

        public async Task<bool> DeleteBranchAsync(int id)
        {
            try
            {
                var branch = await _repo.Branches.GetByIdAsync(id);
                if (branch == null) return false;

                _repo.Branches.DeleteAsync(branch);
                await _repo.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while deleting branch with id {id}", ex);
            }
        }

        public async Task<bool> UpdateBranchAsync(int id, BranchDto branchDto)
        {
            try
            {
                var branch = await _repo.Branches.GetByIdAsync(id);
                if (branch == null) return false;

                _mapper.Map(branchDto, branch);
                _repo.Branches.UpdateAsync(branch);
                await _repo.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while updating branch with id {id}", ex);
            }
        }
    }
}
