using CarDealershipBLL.DTOs;
using CarDealershipDAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipBLL.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto model);
        Task<string> RegisterAsync(RegisterDto model);
    }
}
