using CarDealershipBLL.DTOs;
using CarDealershipBLL.Interfaces;
using CarDealershipDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipBLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }


        public async Task<Users?> Register(Users userDto)
        {

            if(await _context.Users.AnyAsync(u => u.UserName == userDto.UserName))
            {
                return null;
            }



            var user = new Users();
            var hashedPassword = new PasswordHasher<Users>().HashPassword(user, userDto.Password);

            user.UserName = userDto.UserName;
            user.Password = hashedPassword;
            user.Role = string.IsNullOrWhiteSpace(userDto.Role) ? "Buyer" : userDto.Role;


            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }


        public async Task<string> Login(LoginDto userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userDto.UserName);

            if (user is null)
            {
                return null;
            }
            if (new PasswordHasher<Users>().VerifyHashedPassword(user, user.Password, userDto.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return CreateToken(user);
        }

        private string CreateToken(Users user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role , user.Role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
