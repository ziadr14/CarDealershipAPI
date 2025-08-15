using CarDealershipBLL.Interfaces;
using CarDealershipDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipDAL.Interfaces
{
    public interface IBranchRepository:IBaseRepository<Branch>
    {
        IQueryable<Branch> GetAllBranchs();
        IQueryable<Branch> GetBranchById(int id);


    }
}
