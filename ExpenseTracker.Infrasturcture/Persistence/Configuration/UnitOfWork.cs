using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Application.Common.Interface;
using ExpenseTracker.Domain.Entites;
using Microsoft.EntityFrameworkCore.Storage;

namespace ExpenseTracker.Infrasturcture.Persistence.Configuration
{
    internal class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        IDbContextTransaction dbContextTransaction;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context; 
        }
        #region private repositories

        private IRepository<User> _userRepo;
        private IRepository<Category> _categoryRepo;
        private IRepository<Expense> _expenseRepo;

        #endregion private repositories

        #region public repositories
        public IRepository<User> UserRepo
        {
            get 
            {
                _userRepo ??= new EFRepository<User>(_context);
                return _userRepo;
            }
        }
        public IRepository<Category> CategoryRepo
        {
            get
            {
                _categoryRepo ??= new EFRepository<Category>(_context);
                return _categoryRepo;
            }
        }
        public IRepository<Expense> ExpenseRepo
        {
            get
            {
                _expenseRepo ??= new EFRepository<Expense>(_context);
                return _expenseRepo;
            }
        }
        #endregion public repositories

        #region transaction methods
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public void BeginTransaction()
        {
            dbContextTransaction = _context.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            dbContextTransaction?.Commit();
        }
        public void RollBackTransaction()
        {
            dbContextTransaction?.Rollback();
        }

        #endregion transaction methods

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                _context.Dispose();
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
