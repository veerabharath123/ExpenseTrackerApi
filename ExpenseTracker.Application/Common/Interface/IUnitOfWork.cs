using ExpenseTracker.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Common.Interface
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepo { get; }
        IRepository<Category> CategoryRepo { get; }
        IRepository<Expense> ExpenseRepo { get; }
        IRepository<Roles> RolesRepo { get; }
        IRepository<Permissions> PermissionsRepo { get; }
        IRepository<RolePermissions> RolePermissionsRepo { get; }
        IRepository<UserRoles> UserRolesRepo { get; }

        Task<bool> SaveAsync();
        Task<int> SaveChangesAsync();
        int Save();
        void BeginTransaction();
        void CommitTransaction();
        void RollBackTransaction();

    }
}
