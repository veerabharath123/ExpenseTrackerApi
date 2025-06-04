using ExpenseTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static ExpenseTracker.Domain.Records.ExpenseRecords;

namespace ExpenseTracker.Domain.Entites
{
    public class Expense:AuditableWithBaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }

        public void AddUserExpense(ExpenseAddOrUpdateRec record)
        {
            Name = record.Name;
            Description = record.Description;
            Amount = record.Amount;
            Date = record.Date;
            Time = TimeSpan.Zero;
            UserId = record.UserId;
            IsActive = record.IsActive;
            CategoryId = record.CategoryId;
            IsDeleted = false;
        }
    }
}
