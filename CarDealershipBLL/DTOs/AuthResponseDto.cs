using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipBLL.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; }      // JWT Token
        public DateTime Expiration { get; set; } // وقت انتهاء التوكين

        public string Role { get; set; }
    }
}
