using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Common.Interface
{
    public interface IQrCodeServices
    {
        string GetQRBase64(string qrData);
        byte[] GetQRBytes(string qrData);
    }
}
