using ExpenseTracker.SharedKernel.Models.Common.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.SharedKernel.Models.Response
{
    public class LoginResponseDto: JwtUserDto
    {
        public List<string> Permissions { get; set; } = [];
        public string Token { get; set; } = string.Empty;
        public DateTime? TokenExpiry { get; set; }
    }
}
