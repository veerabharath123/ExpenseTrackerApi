using ExpenseTracker.Application.Services;
using ExpenseTracker.SharedKernel.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        public RefController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetPrimaryCategoryList()
        {
            var response = await _categoryServices.GetPrimaryCategoryListAsync();
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetSubCategoryListByParentId(GuidIdRequestDto request)
        {
            var response = await _categoryServices.GetSubCategoryListByParentIdAsync(request.Id);
            return Ok(response);
        }
    }
}
