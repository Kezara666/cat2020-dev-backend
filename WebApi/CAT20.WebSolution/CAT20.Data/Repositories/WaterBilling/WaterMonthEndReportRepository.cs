using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Repositories.WaterBilling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.WaterBilling
{
    internal class WaterMonthEndReportRepository : Repository<WaterMonthEndReport>, IWaterMonthEndReportRepository
    {
        public WaterMonthEndReportRepository(DbContext context) : base(context)
        {
        }

        private WaterBillingDbContext waterBillingDbContext
        {
            get { return Context as WaterBillingDbContext; }
        }


        public async Task<IEnumerable<WaterConnection>> GetConnectionsForInti(int officeId,int subRoadId)
        {
            return await waterBillingDbContext.WaterConnections
              .AsNoTracking()
              .Include(i => i.OpeningBalanceInformation)
              //.Include(r=>r.WaterMonthEndReports)
              .Where(wc => wc.OfficeId == officeId && !wc.WaterMonthEndReports!.Any()).ToListAsync();
        }
        public async Task<IEnumerable<WaterConnection>> GetConnectionsForReport(int officeId,int subRoadId)
        {
            return await waterBillingDbContext.WaterConnections
              .AsNoTracking()
              .Include(i => i.OpeningBalanceInformation)
              .Where(wc => wc.OfficeId == officeId ).ToListAsync();
        }


        public async Task<IEnumerable<WaterConnection>> GetConnections(int officeId, int year, int month)
        {
            return await waterBillingDbContext.WaterConnections
                .AsNoTracking()
                .Include(i => i.Balances!.Where(b => b.Year == year && b.Month == month))
                .Where(wc => wc.OfficeId == officeId).ToListAsync();


        }

        public async Task<bool> HasReportForGivenMonth(int id, int year, int month)
        {
            return await waterBillingDbContext.WaterMonthEndReport
                .AsNoTracking()
                .Where(wc => wc.WcPrimaryId == id && wc.Year == year && wc.Month == month).AnyAsync();
        }

        public async Task<WaterMonthEndReport> GetLastReport(int id)
        {
            return await waterBillingDbContext.WaterMonthEndReport
                .AsNoTracking()
                .Where(wc => wc.WcPrimaryId == id).OrderByDescending(wc=>wc.Id) .FirstOrDefaultAsync();
        }

        public async Task<WaterConnectionBalance> GetBalanceBillYearAndMonth(int id, int year, int month)
        {
            return await waterBillingDbContext.Balances
                .AsNoTracking()
                .Where(wc => wc.WcPrimaryId == id && wc.Year == year && wc.Month == month).FirstOrDefaultAsync();
        }




        public async Task<WaterConnection> CalculatePaymentsForReport(int wcPrimaryId,int year, int month)
        {
         
            try
            {
                var x = await waterBillingDbContext.WaterConnections
                    .AsNoTracking()
                    .Where(wc => wc.Id == wcPrimaryId && wc.Status == 1)
                    .Include(wc => wc.Balances!.Where(b => (b.Year < year) || (b.Year == year && b.Month < month)))
                    .FirstOrDefaultAsync();

                //if (x.Balances!.Count() == 0)
                //{
                //    return await waterBillingDbContext.WaterConnections
                //     .AsNoTracking()
                //     .Where(wc => wc.Id == wcPrimaryId && wc.Status == 1)
                //     .Include(wc => wc.Balances!.Where(b => b.IsFilled == true).OrderByDescending(b => b.Id).Take(1))
                //    .FirstOrDefaultAsync();
                //}
                //else
                //{
                //    return x;
                //}

                return x;
            }
            catch (Exception ex)
            {

                return new WaterConnection();
            }

        }

        public async Task<IEnumerable<WaterConnection>> Validate(int officeId, int? subRoadId,int year, int month)
        {
            return await waterBillingDbContext.WaterConnections
             .AsNoTracking()
             .Include(i => i.WaterMonthEndReports)
             .Where(wc => wc.OfficeId == officeId )
             .Where(wc => subRoadId.HasValue? wc.SubRoadId == subRoadId:true )
             .ToListAsync();
        }
    }
}
