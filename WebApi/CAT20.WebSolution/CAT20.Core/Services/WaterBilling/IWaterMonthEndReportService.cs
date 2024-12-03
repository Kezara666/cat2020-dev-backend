using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.WaterBilling
{
    public interface IWaterMonthEndReportService
    {
        Task<bool> CreateInitilReport(int officeId);
        Task<bool> CreateMonthlyReport(int officeId);
        Task<bool> validate(int officeId);
    }
}
