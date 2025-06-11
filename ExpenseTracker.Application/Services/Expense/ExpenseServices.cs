using ExpenseTracker.Application.Common.Interface;
using ExpenseTracker.Domain.Entites;
using ExpenseTracker.SharedKernel.Models.Response;
using Microsoft.EntityFrameworkCore;
using static ExpenseTracker.Domain.Records.ExpenseRecords;
using ExpenseTracker.SharedKernel.Models.Request;
using ExpenseTracker.Application.Services.Category;
using ExpenseTracker.Application.Common.Class;
using System.Linq;
using ExpenseTracker.Domain.Constants;

namespace ExpenseTracker.Application.Services
{
    public class ExpenseServices :  IExpenseServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExpenseServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseDto<List<ExpenseResponseDto>>> GetUserExpensesAsync()
        {
            var userId = 1;
            var expenses = await (from e in _unitOfWork.ExpenseRepo.TableNoTracking
                                  join c in _unitOfWork.CategoryRepo.TableNoTracking on e.CategoryId equals c.Id
                                  where userId == e.UserId && e.IsActive && !e.IsDeleted
                                  select new ExpenseResponseDto
                                  {
                                      Name = e.Name,
                                      Description = e.Description,
                                      Amount = e.Amount,
                                      Date = e.Date,
                                      CategoryName = c.Name
                                  }).ToListAsync();

            return ApiResponseDto<List<ExpenseResponseDto>>.SuccessStatus(expenses);
        }
        public async Task<ApiResponseDto<List<ExpenseResponseDto>>> GetUserExpensesOfTodayAsync()
        {
            var userId = 1;
            var expenses = await (from e in _unitOfWork.ExpenseRepo.TableNoTracking
                                  join c in _unitOfWork.CategoryRepo.TableNoTracking on e.CategoryId equals c.Id
                                  where userId == e.UserId && e.IsActive && !e.IsDeleted
                                  select new ExpenseResponseDto
                                  {
                                      Name = e.Name,
                                      Description = e.Description,
                                      Amount = e.Amount,
                                      Date = e.Date,
                                      CategoryName = c.Name
                                  }).ToListAsync();

            return ApiResponseDto<List<ExpenseResponseDto>>.SuccessStatus(expenses);
        }
        public async Task<ApiResponseDto<BarChartRequestDto>> GetExpenseFromDateRangeAsync(DateRangeRequestDto request)
        {
            var userId = 1;
            var expenses = await _unitOfWork.ExpenseRepo.TableNoTracking
                            .Where(e => e.UserId == userId && e.IsActive && !e.IsDeleted &&
                                    e.Date.Date >= request.Start.Date && e.Date.Date <= request.End.Date)
                            .GroupBy(x => x.Date.Date)
                            .Select(g => new
                            {
                                Date = g.Key,
                                Total = g.Sum(x => x.Amount)
                            })
                            .ToListAsync();

            var dateRange = Enumerable.Range(0, (request.End.Date - request.Start.Date).Days + 1)
                              .Select(offset => request.Start.Date.AddDays(offset))
                              .ToList();

            var merged = dateRange.Select(date =>
            {
                var expense = expenses.FirstOrDefault(e => e.Date == date);
                return new
                {
                    Date = date.ToString("yyyy-MM-dd"),
                    Total = expense?.Total ?? 0
                };
            }).ToList();

            var chartData = new BarCharBasicData
            {
                Labels = merged.Select(x => x.Date).ToList(),
                Datasets =
                [
                    new() {
                        Label = "Weeky expenses",
                        Data = merged.Select(e => (object)e.Total).ToList(),
                        BackgroundColor = merged.Select(e => GetBackgroundColor(e.Total)).ToList(),
                        BorderColor = merged.Select(e => GetBorderColor(e.Total)).ToList(),
                        BorderWidth = 1
                    }
                ]
            };

            return ApiResponseDto<BarChartRequestDto>.SuccessStatus(new BarChartRequestDto { BasicData = chartData, Message = string.Format(GeneralConstants.WEEKLY_EXPENSE_NOTE, merged.Sum(x => x.Total)) });
        }
        public async Task<ApiResponseDto<BarChartRequestDto>> GetWeeklyExpenseAsync()
        {
            DateTime today = DateTime.Today, sevenDaysAgo = today.AddDays(-6);

            return await GetExpenseFromDateRangeAsync(new DateRangeRequestDto { End = today, Start = sevenDaysAgo});
        }

        private string GetBackgroundColor(decimal amount) => amount switch
        {
            < 100 => "rgba(255, 99, 132, 0.2)",    
            < 500 => "rgba(255, 206, 86, 0.2)",   
            < 1000 => "rgba(75, 192, 192, 0.2)",  
            _ => "rgba(54, 162, 235, 0.2)" 
        };

        private string GetBorderColor(decimal amount) => amount switch
        {
            < 100 => "rgb(255, 99, 132)",
            < 500 => "rgb(255, 206, 86)",
            < 1000 => "rgb(75, 192, 192)",
            _ => "rgb(54, 162, 235)"
        };

        public async Task<ApiResponseDto<bool>> InsertUserExpenseAsync(ExpenseRequestDto request)
        {
            var userId = 1;
            var categoryId = await _unitOfWork.CategoryRepo.GetIdByGuid(request.CategoryId);

            if (categoryId == null)
                return ApiResponseDto<bool>.FailureStatus("Category not found.");

            var expense = new Expense();
            expense.AddUserExpense(new ExpenseAddOrUpdateRec(
                 request.Name,
                 request.Description,
                 request.Amount,
                 request.Date,
                 userId,
                 true,
                 categoryId.Value
            ));
            _unitOfWork.ExpenseRepo.Add(expense);

            var saved = await _unitOfWork.SaveAsync();

            return ApiResponseDto<bool>.SuccessStatus(saved);
        }
    }
}
