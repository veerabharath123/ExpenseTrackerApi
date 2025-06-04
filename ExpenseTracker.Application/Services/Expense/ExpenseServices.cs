using ExpenseTracker.Application.Common.Interface;
using ExpenseTracker.Domain.Entites;
using ExpenseTracker.SharedKernel.Models.Response;
using Microsoft.EntityFrameworkCore;
using static ExpenseTracker.Domain.Records.ExpenseRecords;
using ExpenseTracker.SharedKernel.Models.Request;
using ExpenseTracker.Application.Services.Category;
using ExpenseTracker.Application.Common.Class;

namespace ExpenseTracker.Application.Services
{
    public class ExpenseServices :  IExpenseServices
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
                                  where userId == e.UserId && e.IsActive && !e.IsDeleted
                                  select new ExpenseResponseDto
                                  {
                                      Name = e.Name,
                                      Description = e.Description,
                                      Amount = e.Amount,
                                      Date = e.Date,
                                      CategoryName = c.Name
                                  }).ToListAsync();

            return ApiResponseDto<List<ExpenseResponseDto>>.SuccessStatus(expenses);
        }

        public async Task<ApiResponseDto<bool>> InsertUserExpenseAsync(ExpenseRequestDto request)
        {
            var userId = 1;
            var categoryId = await _unitOfWork.CategoryRepo.GetIdByGuid(request.CategoryId);

            if (categoryId == null)
                return ApiResponseDto<bool>.FailureStatus("Category not found.");

            var expense = new Expense();
            expense.AddUserExpense(new ExpenseAddOrUpdateRec(
                 request.Name,
                 request.Description,
                 request.Amount,
                 request.Date,
                 userId,
                 true,
                 categoryId.Value
            ));
            _unitOfWork.ExpenseRepo.Add(expense);

            var saved = await _unitOfWork.SaveAsync();

            return ApiResponseDto<bool>.SuccessStatus(saved);
        }
    }
}
