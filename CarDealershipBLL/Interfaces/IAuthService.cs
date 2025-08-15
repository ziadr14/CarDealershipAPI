using CarDealershipBLL.DTOs;
using CarDealershipDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipBLL.Interfaces
{
    public interface IAuthService
    {
        Task<Users?> Register(Users userDto);
        Task<string> Login(LoginDto userDto);
    }
}
