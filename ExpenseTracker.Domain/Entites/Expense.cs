using ExpenseTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
    }
}
