using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipDAL.Interfaces
{
    public interface IRepoWrapper
    {
        ICarRepository Cars { get; }
        IBranchRepository Branches { get; }
        ICustomerRepository Customers { get; }
        ISaleRepository Sales { get; }
        IBrandRepository Brands { get; }

        Task SaveAsync();
    }
}
