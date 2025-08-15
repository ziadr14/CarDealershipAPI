using CarDealershipBLL.DTOs;
using CarDealershipBLL.Interfaces;
using CarDealershipDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CarDealershipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthControllers : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthControllers(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<Users>>  Register(Users userDto)
        {
            var user = await _authService.Register(userDto);

            if (user == null)
            {
                return BadRequest("Username Already Exists");
            }

            return Ok(user);
        }

        [HttpPost("Login")]

        public async Task<ActionResult<string>> Login(LoginDto userDto)
        {
            var token = await _authService.Login(userDto);
            if (token == null)
            {
                return BadRequest("Invalid username or password");
            }

            return Ok(token);
        }


    }
}
