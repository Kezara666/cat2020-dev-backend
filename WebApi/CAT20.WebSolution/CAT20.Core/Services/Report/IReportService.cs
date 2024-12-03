using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Report
{
    public interface IReportService
    {
        Task<byte[]> MixinOrdersReportForOfficeAsync(string reportName, string reportType, int officeId);
        Task<byte[]> MixinSarapReceiptsDailyReportForOfficeAsync(string reportName, string reportType, int officeId, DateTime fordate);
        Task<String> getfilelocation(string reportName, string reportType, int officeId, DateTime fordate);
        
    }
}

