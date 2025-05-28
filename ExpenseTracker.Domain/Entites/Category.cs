using ExpenseTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entites
{
    public class Category : AuditableWithBaseEntity<int>
    {

        public string Name { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        public bool IsSubCategory { get; private set; }

        public int? ParentCategory { get; private set; } 

        public bool IsActive { get; private set; }

    }
}
