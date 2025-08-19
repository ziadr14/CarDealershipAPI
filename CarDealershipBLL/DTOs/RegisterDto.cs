using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipBLL.DTOs
{
    public class RegisterDto
    {
        public string FullName { get; set; }   // اسم المستخدم كامل
        public string Email { get; set; }      // الإيميل (Username)
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
