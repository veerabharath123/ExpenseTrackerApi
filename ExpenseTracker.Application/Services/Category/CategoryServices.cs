using ExpenseTracker.Application.Common.Interface;
using ExpenseTracker.SharedKernel.Models.Common.Class;
using ExpenseTracker.SharedKernel.Models.Response;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ExpenseTracker.Domain.Entites;

namespace ExpenseTracker.Application.Services.Category
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private IQueryable<BaseRefDto> GetCategoryQuery(Expression<Func<Domain.Entites.Category, bool>> predicate)
        {
            var query = _unitOfWork.CategoryRepo.TableNoTracking
                            .Where(predicate)
                            .Select(x => new BaseRefDto
                            {
                                Id = x.GuidId,
                                Name = x.Name,
                            });

            return query;
        }

        public async Task<ApiResponseDto<List<BaseRefDto>>> GetPrimaryCategoryListAsync()
        {
            var list = await GetCategoryQuery(x => !x.IsDeleted && x.IsActive && !x.IsSubCategory)
                            .ToListAsync();

            return ApiResponseDto<List<BaseRefDto>>.SuccessStatus(list);
        }

        public async Task<ApiResponseDto<List<BaseRefDto>>> GetSubCategoryListByParentIdAsync(Guid parentId)
        {
            var parentCategoryId = await _unitOfWork.CategoryRepo.GetIdByGuid(parentId);

            if(parentCategoryId is null)
            {
                return ApiResponseDto<List<BaseRefDto>>.FailureStatus("Unable to find Sub catergories");
            }

            var list = await GetCategoryQuery(x => 
                        !x.IsDeleted 
                        && x.IsActive 
                        && x.IsSubCategory 
                        && x.ParentCategory == parentCategoryId)
                        .ToListAsync();

            return ApiResponseDto<List<BaseRefDto>>.SuccessStatus(list);
        }

    }
}
