using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExpenseTracker.Domain.Records
{
    public class ExpenseRecords
    {
        public record ExpenseAddOrUpdateRec(
                                   string Name,
                                   string Description,
                                   decimal Amount,
                                   DateTime Date,
                                   int UserId,
                                   bool IsActive,
                                   int CategoryId
                                   );
    }
}
