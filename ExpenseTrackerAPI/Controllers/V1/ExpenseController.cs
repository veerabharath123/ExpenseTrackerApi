using ExpenseTracker.Application.Services;
using ExpenseTracker.SharedKernel.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseServices _expenseServices;
        public ExpenseController(IExpenseServices expenseServices)
        {
            _expenseServices = expenseServices;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetUserExpenses()
        {
            var respones = await _expenseServices.GetUserExpensesAsync();
            return Ok(respones);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> InsertUserExpense([FromBody] ExpenseRequestDto request)
        {
            var respones = await _expenseServices.InsertUserExpenseAsync(request);
            return Ok(respones);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetWeeklyExpense()
        {
            var respones = await _expenseServices.GetWeeklyExpenseAsync();
            return Ok(respones);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetExpenseFromRangeAsync([FromBody] DateRangeRequestDto request)
        {
            var respones = await _expenseServices.GetExpenseFromRangeAsync(request);
            return Ok(respones);
        }
    }
}
