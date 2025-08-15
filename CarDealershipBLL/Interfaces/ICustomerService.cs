using CarDealershipBLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipBLL.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomerAsync();
        Task<CustomerDto> GetCustomerByIdAsync(int id);
        Task<CustomerDto> CreateCustomerAsync(CustomerDto customerDto);
        Task<bool> UpdateCustomerAsync(int id, CustomerDto customerDto);
        Task<bool> DeleteCustomerAsync(int id);
    }
}
