using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Constants
{
    public class GeneralConstants
    {
        public const string WEEKLY_EXPENSE_NOTE = "Your total expenses that you spent this week is {0}";
        public enum DependencyInjectionTypes
        {
            Transient,
            Scoped,
            Singleton
        }
    }
}
