using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.WaterBilling
{
    public interface IWaterMonthEndReportRepository:IRepository<WaterMonthEndReport>
    {
        Task<IEnumerable<WaterConnection>> GetConnectionsForInti(int officeId, int subRoadId);
        Task<IEnumerable<WaterConnection>> GetConnectionsForReport(int officeId, int subRoadId);
        Task<IEnumerable<WaterConnection>> GetConnections(int officeId,int year,int month);

        Task<WaterConnection> CalculatePaymentsForReport(int wcPrimaryId, int year, int month);
        Task<bool> HasReportForGivenMonth(int wcPrimaryId, int year, int month);
        Task<WaterMonthEndReport> GetLastReport(int wcPrimaryId);
        Task<WaterConnectionBalance> GetBalanceBillYearAndMonth(int wcPrimaryId, int year, int month);


        Task<IEnumerable<WaterConnection>> Validate(int officeId,int? subRoadId, int year, int month);




    }
}
