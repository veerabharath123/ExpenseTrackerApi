using ExpenseTracker.SharedKernel.Models.Request;
using ExpenseTracker.SharedKernel.Models.Response;

namespace ExpenseTracker.Application.Services
{
    public interface IUserServices
    {
        Task<ApiResponseDto<bool>> InsertUserAsync(UserInsertRequestDto request);
        Task<List<string>> GetPermissionsByUserIdAsync(Guid Id);

        Task<ApiResponseDto<LoginResponseDto>> ValidateUserLoginAsync(LoginRequestDto request);
    }
}