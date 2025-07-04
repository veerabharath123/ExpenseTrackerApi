using ExpenseTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entites
{
    public class RolePermissions:AuditableWithBaseEntity<int>
    {
        public int RoleId { get; private set; }
        public int PermissionId { get; private set; }
        public bool IsActive { get; private set; }
    }
}
