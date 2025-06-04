using ExpenseTracker.SharedKernel.Models.Request;
using ExpenseTracker.SharedKernel.Models.Response;

namespace ExpenseTracker.Application.Services
{
    public interface IExpenseServices
    {
        Task<ApiResponseDto<List<ExpenseResponseDto>>> GetUserExpensesAsync();
        Task<ApiResponseDto<bool>> InsertUserExpenseAsync(ExpenseRequestDto request);
    }
}