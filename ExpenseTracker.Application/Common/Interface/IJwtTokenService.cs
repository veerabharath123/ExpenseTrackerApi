using ExpenseTracker.SharedKernel.Models.Common.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Common.Interface
{
    public interface IJwtTokenServices
    {
        string GenerateToken(JwtUserDto user);
    }
}
