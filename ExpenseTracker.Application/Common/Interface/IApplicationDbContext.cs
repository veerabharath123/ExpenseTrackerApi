using ExpenseTracker.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Common.Interface
{
    public interface IApplicationDbContext
    {
        DbSet<User> User { get; set; }
        DbSet<Category> Category { get; set; }
        DbSet<Expense> Expense { get; set; }
    }
}
