﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Common.Interface
{
    public interface IBarcodeServices
    {
        byte[] GetBarcodeBytes(string text);
        string GetBarcodeBase64(string text);

    }
}
