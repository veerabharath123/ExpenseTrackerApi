using ExpenseTracker.SharedKernel.Models.Common.Class;
using ExpenseTracker.SharedKernel.Models.Response;

namespace ExpenseTracker.Application.Services
{
    public interface ICategoryServices
    {
        Task<ApiResponseDto<List<BaseRefDto>>> GetPrimaryCategoryListAsync();
        Task<ApiResponseDto<List<BaseRefDto>>> GetSubCategoryListByParentIdAsync(Guid Id);
    }
}