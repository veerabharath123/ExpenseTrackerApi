using ExpenseTracker.SharedKernel.Models.Common.Class;
using ExpenseTracker.SharedKernel.Models.Response;

namespace ExpenseTracker.Application.Services.Category
{
    public interface ICategoryServices
    {
        Task<ApiResponseDto<List<BaseRefDto>>> GetPrimaryCategoryListAsync();
        Task<ApiResponseDto<List<BaseRefDto>>> GetSubCategoryListByParentIdAsync(Guid Id);
    }
}