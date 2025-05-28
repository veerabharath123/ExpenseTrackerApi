using ExpenseTracker.Application.Common.Interface;
using ExpenseTracker.Application.Common.Utitlities;
using ExpenseTracker.Domain.Constants;
using ExpenseTracker.Domain.Entites;
using ExpenseTracker.SharedKernel.Models.Request;
using ExpenseTracker.SharedKernel.Models.Response;
using Microsoft.EntityFrameworkCore;
using static ExpenseTracker.Domain.Records.UserRecords;

namespace ExpenseTracker.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private async Task<bool> CheckUserExistAsync(string username)
        {
            return await _unitOfWork.UserRepo.TableNoTracking.AnyAsync(x => x.UserName == username && !x.IsDeleted);
        }
        public async Task<ApiResponseDto<bool>> InsertUserAsync(UserInsertRequestDto request)
        {
            var userExist = await CheckUserExistAsync(request.UserName);

            if (userExist) return ApiResponseDto<bool>.FailureStatus("User with the name already exists");

            var user = new User();

            user.Add(new UserAddOrUpdateRec(
                request.FirstName,
                request.LastName,
                request.Email,
                request.UserName,
                true
            ));

            SetPassword(user, request.Password);

            _unitOfWork.UserRepo.Add(user);
            var saved = await _unitOfWork.SaveAsync();

            return ApiResponseDto<bool>.SuccessStatus(saved);
        }
        private static void SetPassword(User user, string password, bool isUpdate = default)
        {
            if (isUpdate && PasswordHasher.VerifyPasswordHash(password, user.Password, user.HashSalt))
                return;

            PasswordHasher.CreatePasswordHash(password, out byte[] hashedPassword, out byte[] salt);
            user.SetPassword(hashedPassword, salt);
        }
        private async Task<User?> GetUnqiueUserByUserNameAsync(string username)
        {
            return await _unitOfWork.UserRepo.TableNoTracking
                .SingleOrDefaultAsync(x => !x.IsDeleted && x.UserName.ToLower() == username.ToLower());
        }

        public async Task<ApiResponseDto<object>> ValidateUserLogin(string username, string password)
        {
            var user = await GetUnqiueUserByUserNameAsync(username);

            if (user is null)
                return ApiResponseDto<object>.FailureStatus("Username already exists");

            var isValid = PasswordHasher.VerifyPasswordHash(password,user.Password,user.HashSalt);

            if(isValid)
                return ApiResponseDto<object>.FailureStatus("Incorrect password/username");

            return ApiResponseDto<object>.FailureStatus("Incorrect password/username");
        }
    }
}
