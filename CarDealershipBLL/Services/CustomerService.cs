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
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IRepoWrapper _repo;

        public CustomerService(IMapper mapper, IRepoWrapper repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomerAsync()
        {
            try
            {
                var customers = await _repo.Customers.GetAllCustomer().Select(x => new CustomerDto
                {
                    CustomerId = x.CustomerId,
                    FullName = x.FullName,
                    PhoneNumber = x.PhoneNumber,
                    Address = x.Address,
                    Email = x.Email,

                }).ToListAsync();

                return customers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllCustomerAsync: {ex.Message}");
                return new List<CustomerDto>();
            }
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await _repo.Customers.GetCustomerById(id).Select(x => new CustomerDto
                {
                    CustomerId = x.CustomerId,
                    FullName = x.FullName,
                    PhoneNumber = x.PhoneNumber,
                    Address = x.Address,
                    Email = x.Email,

                }).FirstOrDefaultAsync();

                return customer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetCustomerByIdAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<CustomerDto> CreateCustomerAsync(CustomerDto customerDto)
        {
            try
            {
                var newCustomer = _mapper.Map<Customer>(customerDto);

                await _repo.Customers.AddAsync(newCustomer);
                await _repo.SaveAsync();
                return _mapper.Map<CustomerDto>(newCustomer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateCustomerAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateCustomerAsync(int id, CustomerDto customerDto)
        {
            try
            {
                var customer = await _repo.Customers.GetByIdAsync(id);
                if (customer == null) return false;
                _mapper.Map(customerDto, customer);
                _repo.Customers.UpdateAsync(customer);
                await _repo.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateCustomerAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            try
            {
                var customer = await _repo.Customers.GetByIdAsync(id);
                if (customer == null) return false;

                _repo.Customers.DeleteAsync(customer);
                await _repo.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteCustomerAsync: {ex.Message}");
                return false;
            }
        }
    }
}
