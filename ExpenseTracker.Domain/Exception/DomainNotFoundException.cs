﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Exception
{
    public class DomainNotFoundException: DomainException
    {
        public DomainNotFoundException(string message) : base(message)
        {
            
        }
    }
}
