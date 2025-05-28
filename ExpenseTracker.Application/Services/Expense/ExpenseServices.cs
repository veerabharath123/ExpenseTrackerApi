using ExpenseTracker.Application.Common.Interface;
using ExpenseTracker.SharedKernel.Models.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Services
{
    public class ExpenseServices : IExpenseServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExpenseServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseDto<List<ExpenseResponseDto>>> GetUserExpensesAsync()
        {
            var userId = 1;
            var expenses = await (from e in _unitOfWork.ExpenseRepo.TableNoTracking
                                  join c in _unitOfWork.CategoryRepo.TableNoTracking on e.CategoryId equals c.Id
                                  where userId == e.UserId && e.IsActive && e.IsDeleted
                                  select new ExpenseResponseDto
                                  {
                                      Name = e.Name,

                                  }).ToListAsync();

            return ApiResponseDto<List<ExpenseResponseDto>>.SuccessStatus(expenses);
        }
    }
}
