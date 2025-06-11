using ExpenseTracker.SharedKernel.Models.Request;
using ExpenseTracker.SharedKernel.Models.Response;

namespace ExpenseTracker.Application.Services
{
    public interface IExpenseServices
    {
        Task<ApiResponseDto<List<ExpenseResponseDto>>> GetUserExpensesAsync();
        Task<ApiResponseDto<bool>> InsertUserExpenseAsync(ExpenseRequestDto request);
        Task<ApiResponseDto<BarChartRequestDto>> GetWeeklyExpenseAsync();
        Task<ApiResponseDto<BarChartRequestDto>> GetExpenseFromDateRangeAsync(DateRangeRequestDto request);
    }
}