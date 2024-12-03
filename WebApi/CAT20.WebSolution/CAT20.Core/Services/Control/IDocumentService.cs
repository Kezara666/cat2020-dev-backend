using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface IDocumentService
    {
        byte[] GeneratePdfFromString();

        byte[] GenerateWaterBillPdfFromWaterConnectionBalancesList(IEnumerable<WaterConnectionBalance> WaterConnectionBalances);

        //byte[] GeneratePdfFromRazorView();
    }
}
