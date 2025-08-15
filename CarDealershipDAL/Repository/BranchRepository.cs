using CarDealershipDAL.Interfaces;
using CarDealershipDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipDAL.Repository
{
    public class BranchRepository : BaseRepository<Branch>, IBranchRepository
    {
        public BranchRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Branch> GetAllBranchs()
        {
            return  _context.Branches.AsQueryable();
        }

        public IQueryable<Branch> GetBranchById(int id)
        {
            return _context.Branches.Where(b => b.BranchId == id).AsQueryable();
        }
    }
}
