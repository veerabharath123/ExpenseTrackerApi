using ExpenseTracker.Application.Services;
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
    }
}
