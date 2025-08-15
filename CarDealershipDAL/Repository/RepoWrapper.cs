using CarDealershipDAL.Interfaces;
using CarDealershipDAL.Models;

namespace CarDealershipDAL.Repositories
{
    public class RepoWrapper : IRepoWrapper
    {
        private readonly AppDbContext _context;

        public ICarRepository Cars { get; private set; }
        public IBranchRepository Branches { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public ISaleRepository Sales { get; private set; }
        public IBrandRepository Brands { get; private set; }

        public RepoWrapper(AppDbContext context,
                           ICarRepository carRepo,
                           IBranchRepository branchRepo,
                           ICustomerRepository customerRepo,
                           ISaleRepository saleRepo,
                           IBrandRepository brandRepo)
        {
            _context = context;
            Cars = carRepo;
            Branches = branchRepo;
            Customers = customerRepo;
            Sales = saleRepo;
            Brands = brandRepo;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
