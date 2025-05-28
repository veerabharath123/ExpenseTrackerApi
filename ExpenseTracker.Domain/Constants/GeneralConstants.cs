using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Constants
{
    public class GeneralConstants
    {
        public enum DependencyInjectionTypes
        {
            Transient,
            Scoped,
            Singleton
        }
    }
}
