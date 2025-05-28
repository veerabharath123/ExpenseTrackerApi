using ExpenseTracker.SharedKernel.Models.Response;

namespace ExpenseTracker.Application.Services
{
    public interface IExpenseServices
    {
        Task<ApiResponseDto<List<ExpenseResponseDto>>> GetUserExpensesAsync();
    }
}