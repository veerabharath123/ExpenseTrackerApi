﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Exception
{
    public abstract class DomainException:System.Exception
    {
        public DomainException(string message): base(message)
        {
            
        }
    }
}
