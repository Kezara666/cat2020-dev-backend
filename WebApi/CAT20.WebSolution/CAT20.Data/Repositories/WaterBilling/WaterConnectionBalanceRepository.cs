using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Repositories.WaterBilling;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CAT20.Data.Repositories.WaterBilling
{
    public class WaterConnectionBalanceRepository : Repository<WaterConnectionBalance>, IWaterConnectionBalanceRepository
    {
        public WaterConnectionBalanceRepository(DbContext context) : base(context)
        {
        }


        private WaterBillingDbContext waterBillingDbContext
        {
            get { return Context as WaterBillingDbContext; }
        }

        public async Task<WaterConnection> GetForAddReadingById(int wcPrimaryId)
        {
            try
            {
                var x = await waterBillingDbContext.WaterConnections.Where(wc => wc.Id == wcPrimaryId && wc.Status == 1)
                     .AsNoTracking()
                     .Include(wc => wc.SubRoad)
                     .ThenInclude(s => s.MainRoad)
                      .Include(wc => wc.ActiveNature)
                                       .ThenInclude(n => n.WaterTariffs!.Where(t => t.Status == 1))
                     .Include(wc => wc.ActiveNature!.NonMeterFixCharges)
                      .Include(wc => wc.Balances)
                     .FirstOrDefaultAsync();

                x.ActiveNature!.WaterTariffs = x.ActiveNature!.WaterTariffs!
                  .Where(t => t.WaterProjectId == x.SubRoad!.WaterProjectId)
                  .ToList();

                if (x != null && x.Balances != null)
                {
                    x.Balances = x.Balances!
                       .Where(b => b.IsCompleted == false)
                       .ToList();
                }
                return x;
            }
            catch (Exception ex)
            {
                return new WaterConnection();
            }
        }

        public async Task<WaterConnection> GetForAddReadingByConnectionId(int officeId, string connectionId)
        {
            try
            {
                var x = await waterBillingDbContext.WaterConnections.Where(wc => wc.OfficeId == officeId && wc.ConnectionId == connectionId && wc.Status == 1)
                     .AsNoTracking()
                     .Include(wc => wc.MeterConnectInfo)
                     .Include(wc => wc.SubRoad)
                     .ThenInclude(s => s.MainRoad)
                     .Include(wc=>wc.SubRoad!.WaterProject)
                      .Include(wc => wc.ActiveNature)
                                       .ThenInclude(n => n.WaterTariffs!.Where(t => t.Status == 1))
                     .Include(wc => wc.ActiveNature!.NonMeterFixCharges)
                      .Include(wc => wc.Balances!.OrderBy(wc=>wc.Id))
                     .FirstOrDefaultAsync();

                x.ActiveNature!.WaterTariffs = x.ActiveNature!.WaterTariffs!
                  .Where(t => t.WaterProjectId == x.SubRoad!.WaterProjectId)
                  .ToList();

                if (x != null && x.Balances != null)
                {
                    x.Balances = x.Balances!
                       .Where(b => b.IsCompleted == false)
                       .ToList();
                }
                return x;
            }
            catch (Exception ex)
            {
                return new WaterConnection();
            }

        }

        public async Task<WaterConnection> GetForAddReadingByConnectionNo(int officeId, string connectionNo)
        {
            try
            {

                var x = await waterBillingDbContext.WaterConnections.Where(wc => wc.OfficeId == officeId && wc.MeterConnectInfo!.ConnectionNo == connectionNo && wc.Status == 1)
                     .AsNoTracking()
                     .Include(wc => wc.MeterConnectInfo)
                     .Include(wc => wc.SubRoad)
                     .ThenInclude(s => s.MainRoad)
                      .Include(wc => wc.ActiveNature)
                                      .ThenInclude(n => n.WaterTariffs!.Where(t=> t.Status==1))
                     .Include(wc => wc.ActiveNature!.NonMeterFixCharges)
                     .Include(wc => wc.Balances!.OrderBy(wc => wc.Id))
                     .FirstOrDefaultAsync();

                x.ActiveNature!.WaterTariffs = x.ActiveNature!.WaterTariffs!
                    .Where(t => t.WaterProjectId == x.SubRoad!.WaterProjectId)
                    .ToList();

                if (x != null && x.Balances != null)
                {
                    x.Balances = x.Balances!
                           .Where(b => b.IsCompleted == false)
                           .ToList();
                }
                return x;
            }
            catch (Exception ex)
            {
                return new WaterConnection();
            }
        }

        public async Task<WaterConnection> GetForAddReadingByBarCode(int officeId, string barCode)
        {
            try
            {

                var wcPrimaryId = await waterBillingDbContext.Balances
                  .Where(b => b.BarCode == barCode)
              .Select(b => b.WcPrimaryId).FirstOrDefaultAsync();

                var x = await waterBillingDbContext.WaterConnections.Where(wc => wc.OfficeId == officeId && wc.Id == wcPrimaryId && wc.Status == 1)
                    .Include(wc => wc.ActiveNature)
                    .ThenInclude(n => n.WaterTariffs)
                    .Include(wc => wc.ActiveNature!.NonMeterFixCharges)
                    .Include(wc => wc.Balances!.OrderBy(wc => wc.Id))
                    .FirstOrDefaultAsync();

                x.Balances = x.Balances!
                       .Where(b => b.IsCompleted == false)
                       .ToList();

                return x;
            }
            catch (Exception ex)
            {
                return new WaterConnection();
            }
        }


        public async Task<IEnumerable<WaterConnection>> getWaterBill(int partnerId, int sabhaId)
        {
            var x = await waterBillingDbContext.WaterConnections
                .Include(wc => wc.Balances)
                .Where(wc => (wc.PartnerId == partnerId) && (wc.SubRoad.MainRoad.SabhaId == sabhaId) && wc.Status == 1)
                .Where(wc => wc.Balances!.All(b => b.IsCompleted == false && b.IsFilled == true))
                .ToListAsync();
            return x;
        }
        public async Task<IEnumerable<WaterConnection>> getWaterConnection(int partnerId)
        {
            var waterConnections = await waterBillingDbContext.WaterConnections
                .Include(m => m.SubRoad.MainRoad)
                .Where(wc => wc.PartnerId == partnerId)
                .ToListAsync();

            return waterConnections;
        }

        public async Task<IEnumerable<WaterConnection>> GetToBillProcess(int year, int month, List<int> subroadIds)
        {


            //var wcs = await waterBillingDbContext.WaterConnections
            //    .Include(wc => wc.Balances)
            //    .Include(wc => wc.MeterConnectInfo)
            //    .Where(wc => subroadIds.Contains(wc.SubRoadId!.Value) && wc.Status == 1)
            //    .Where(wc => wc.Balances!.All(b => b.IsFilled == true && String.Concat(b.Year, b.Month) != String.Concat(year, month)))
            //    .ToListAsync();

            //foreach (var wc in wcs)
            //{
            //    wc.Balances = wc.Balances!
            //        .Where(b => (b.IsCompleted == false && b.IsFilled == true) || b.IsProcessed == false)
            //        .ToList();
            //}

            //wcs = wcs.Where(wc => wc.Balances!.Count() > 0)
            //        .ToList();





            //var wcs = await waterBillingDbContext.WaterConnections
            //   .Include(wc => wc.Balances)
            //   .Include(wc => wc.MeterConnectInfo)
            //   .Where(wc => subroadIds.Contains(wc.SubRoadId!.Value) && wc.Status == 1)
            //   .Include(wc => wc.Balances!.Where(b => String.Concat(b.Year, b.Month) != String.Concat(year, month) && (b.IsCompleted == false || b.IsProcessed == false)))
            //   .Where(wc => wc.Balances!.All(b => b.IsFilled == true))
            //   .Where(wc => wc.Balances!.Count() > 0)
            //   .ToListAsync();


            var wcs = await waterBillingDbContext.WaterConnections
              .Include(wc => wc.Balances)
              .Include(wc => wc.MeterConnectInfo)
              .Where(wc => subroadIds.Contains(wc.SubRoadId!.Value) && wc.Status == 1 && wc.ActiveStatus==1)
              .Where(wc => wc.Balances!.All(b => b.IsFilled == true && b.ToDate!=null && String.Concat(b.Year, b.Month) != String.Concat(year, month)))
              .Include(wc => wc.Balances!.Where(b => (b.IsCompleted == false && b.IsFilled == true) || b.IsProcessed == false))
              .Where(wc => wc.Balances!.Count() > 0)
              .ToListAsync();

            return wcs;


        }

        public async Task<(int totalCount, IEnumerable<WaterConnectionBalance> list)> GetPreviousBills(int wcId, int pageNo)
        {
            try
            {

                var query = waterBillingDbContext.Balances
                    .Where(a => a.WcPrimaryId == wcId && a.IsFilled == true)
                    .OrderByDescending(a => a.Id); ;

                // Implement your logic to calculate totalCount

                int totalCount = await query.CountAsync();

                var pageSize = 5;
                int skipAmount = (pageNo - 1) * pageSize;

                var balances = await query.Skip(skipAmount).Take(pageSize).ToListAsync();

                return (totalCount: totalCount, list: balances);

            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // You might want to return an appropriate error response.
                return (totalCount: 0, list: null);
            }
        }

        public async Task<IEnumerable<WaterConnection>> GetProcessedBills(int year, int month, List<int> subroadIds)
        {
            //var wcs = await waterBillingDbContext.WaterConnections
            //    .Include(wc => wc.Balances)
            //    .Where(wc => subroadIds.Contains(wc.SubRoadId!.Value))
            //    .Where(wc => wc.Balances!.All(b => b.IsFilled == true && String.Concat(b.Year, b.Month) != String.Concat(year, month)))
            //    .ToListAsync();

            //foreach (var wc in wcs)
            //{
            //    wc.Balances = wc.Balances!
            //        .Where(b => b.IsCompleted == false && b.IsFilled == true)
            //        .ToList();
            //}

            //wcs = wcs.Where(wc => wc.Balances!.Count() > 0)
            //        .ToList();

            //return wcs;

            /*
            var wcs = await waterBillingDbContext.WaterConnections
               .Include(wc => wc.Balances)
               .Include(wc => wc.MeterConnectInfo)
               .Where(wc => subroadIds.Contains(wc.SubRoadId!.Value) && wc.Status == 1)
               .Where(wc => wc.Balances!.Any(b => String.Concat(b.Year, b.Month) == String.Concat(year, month)))
               .ToListAsync();

            foreach (var wc in wcs)
            {
                wc.Balances = wc.Balances!
                    .Where(b => b.IsCompleted == false && b.IsFilled == false)
                    .ToList();
            }

            wcs = wcs.Where(wc => wc.Balances!.Count() > 0)
                    .ToList();

            return wcs;

            */
            var y = await waterBillingDbContext.WaterConnections
                   .Include(wc => wc.Balances)
                   .Include(wc => wc.MeterConnectInfo)
                   .Include(wc => wc.SubRoad)
                   .ThenInclude(wc=>wc.WaterProject)
                   .Include(wc=>wc.ActiveNature)
                   .Include(wc=>wc.ActiveNature!.WaterTariffs)
                   .Include(wc=>wc.ActiveNature!.NonMeterFixCharges)
                   .Where(wc => subroadIds.Contains(wc.SubRoadId!.Value) && wc.Status == 1 && wc.ActiveStatus == 1)
                   .Include(wc => wc.Balances!.Where(b => String.Concat(b.Year, b.Month) == String.Concat(year, month) && b.IsFilled == false))
                   .Where(wc => wc.Balances!.Any(b => String.Concat(b.Year, b.Month) == String.Concat(year, month) && b.IsFilled == false))
                   .Where(wc => wc.Balances!.Count() > 0)
                   .OrderBy(wc=>wc.MeterConnectInfo!.OrderNo)
                   .ToListAsync();

            return y;

        }

        public async Task<IEnumerable<WaterConnectionBalance>> GetForPrintBills(int year, int month, List<int> subroadIds)
        {
            var y = await waterBillingDbContext.WaterConnections
              .Include(wc => wc.Balances)
              .Include(wc => wc.MeterConnectInfo)
              .Where(wc => subroadIds.Contains(wc.SubRoadId!.Value) && wc.Status == 1)
              .Include(wc => wc.Balances!.Where(b => String.Concat(b.Year, b.Month) == String.Concat(year, month) && b.IsFilled == false))
              .Where(wc => wc.Balances!.Any(b => String.Concat(b.Year, b.Month) == String.Concat(year, month) && b.IsFilled == false))
              .Where(wc => wc.Balances!.Count() > 0)
              .SelectMany(wc=>wc.Balances!.ToList())
              .ToListAsync();

            return y;
        }

        public async Task<IEnumerable<WaterConnection>> GetForPayments(List<int> partnerId)
        {
            var x = await waterBillingDbContext.WaterConnections.Where(wc => partnerId.Contains(wc.PartnerId!.Value) && wc.Status == 1)
              .Where(wc => wc.Balances!.All(b => b.IsCompleted == false && b.IsFilled == true))
              .ToListAsync();
            return x;
        }

        public async Task<WaterConnection> GetForPaymentsByBarCode(int officeId, string barcode)
        {
            var wcPrimaryId = await waterBillingDbContext.Balances
              .Where(b => b.BarCode == barcode)
          .Select(b => b.WcPrimaryId).FirstOrDefaultAsync();



            return await waterBillingDbContext.WaterConnections.Where(wc => wc.Id == wcPrimaryId && wc.OfficeId == officeId && wc.Status == 1)
                        .Include(wc => wc.ActiveNature)
                        .Include(wc => wc.MeterConnectInfo)
                        .Include(wc => wc.SubRoad)
                        .Include(wc => wc.SubRoad!.MainRoad)
                        .Include(wc => wc.SubRoad!.WaterProject)
                        .Include(wc => wc.Balances!.OrderByDescending(b => b.Id).Take(1))
                        .FirstOrDefaultAsync();





        }

        public async Task<WaterConnection> GetForPaymentsByConnectionId(int officeId, string connectionId)
        {

            return await waterBillingDbContext.WaterConnections.Where(wc => wc.ConnectionId == connectionId && wc.Status == 1 && wc.OfficeId == officeId)
                 .Include(wc => wc.ActiveNature)
                 .Include(wc => wc.MeterConnectInfo)
                 .Include(wc => wc.SubRoad)
                 .Include(wc => wc.SubRoad!.MainRoad)
                 .Include(wc => wc.SubRoad!.WaterProject)
                 .Include(wc => wc.Balances!.OrderByDescending(b => b.Id).Take(1))
                 .FirstOrDefaultAsync();
        }

        public async Task<WaterConnection> GetForPaymentsByConnectionNo(int officeId, string connectionNo)
        {

            var x = await waterBillingDbContext.WaterConnections.Where(wc => wc.MeterConnectInfo!.ConnectionNo == connectionNo && wc.Status == 1 && wc.OfficeId == officeId)
                .Include(wc => wc.ActiveNature)
                .Include(wc => wc.MeterConnectInfo)
                .Include(wc => wc.SubRoad)
                .Include(wc => wc.SubRoad!.MainRoad)
                .Include(wc => wc.SubRoad!.WaterProject)
                //.Include(wc => wc.Balances)
                .Include(wc => wc.Balances!.OrderByDescending(b => b.Id).Take(1))
                .FirstOrDefaultAsync();


            //if (x != null && x.Balances != null)
            //{
            //    //x.Balances = x.Balances!
            //    //   .Where(b => b.IsCompleted == false && b.IsFilled == true)
            //    //   .ToList();

            //    x.Balances = x.Balances!
            //       .OrderByDescending(b => b.Id)
            //       .Take(1)
            //       .ToList();
            //}
            return x;
        }

        public Task<WaterConnection> GetForPaymentsById(int wcPrimaryId)
        {
            throw new NotImplementedException();
        }

        public async Task<WaterConnection> CalculatePayments(int wcPrimaryId)
        {
            //var x = await waterBillingDbContext.WaterConnections.Where(wc => wc.Id == wcPrimaryId && wc.Status == 1)
            //    //.Include(wc => wc.ActiveNature)
            //    //.Include(wc => wc.MeterConnectInfo)
            //    //.Include(wc => wc.SubRoad)
            //    //.Include(wc => wc.SubRoad!.MainRoad)
            //    .Include(wc => wc.Balances!.Where(b => b.IsCompleted == false && b.IsFilled == true))
            //    .FirstOrDefaultAsync();


            //if (x != null && x.Balances != null)
            //{
            //    x.Balances = x.Balances!
            //       .Where(b => b.IsCompleted == false && b.IsFilled == true)
            //       .ToList();
            //}

            try
            {
                var x = await waterBillingDbContext.WaterConnections.Where(wc => wc.Id == wcPrimaryId && wc.Status == 1)
                    .Include(wc => wc.Balances!.Where(b => b.IsCompleted == false && b.IsFilled == true))
                    .FirstOrDefaultAsync();

                if (x.Balances!.Count() == 0)
                {
                    return await waterBillingDbContext.WaterConnections
                     .Where(wc => wc.Id == wcPrimaryId && wc.Status == 1)
                     .Include(wc => wc.Balances!.Where(b=>b.IsFilled == true).OrderByDescending(b => b.Id).Take(1))
                    .FirstOrDefaultAsync();
                }
                else
                {
                    return x;
                }
            }
            catch (Exception ex)
            {

                return new WaterConnection();
            }

        }

        public async Task<IEnumerable<WaterConnectionBalance>> ReversePayments(int wcPrimaryId,decimal? amount)
        {
            return  await waterBillingDbContext.Balances.Where(b => b.WcPrimaryId == wcPrimaryId && b.IsFilled ==true)
                .OrderByDescending(b => b.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<WaterConnection>> GetPreviousBills(int year, int month, List<int> subroadIds)
        {
            var y = await waterBillingDbContext.WaterConnections
       .Include(wc => wc.Balances)
       .Include(wc => wc.MeterConnectInfo)
       .Include(wc => wc.SubRoad)
       .ThenInclude(wc => wc.WaterProject)
       .Include(wc => wc.ActiveNature)
       .Include(wc => wc.ActiveNature!.WaterTariffs)
       .Include(wc => wc.ActiveNature!.NonMeterFixCharges)
       .Where(wc => subroadIds.Contains(wc.SubRoadId!.Value) && wc.Status == 1 && wc.ActiveStatus == 1)
       .Include(wc => wc.Balances!.Where(b => String.Concat(b.Year, b.Month) == String.Concat(year, month) && b.IsFilled == false))
       .Where(wc => wc.Balances!.Any(b => String.Concat(b.Year, b.Month) == String.Concat(year, month) && b.IsFilled == true))
       .Where(wc => wc.Balances!.Count() > 0)
       .OrderBy(wc => wc.MeterConnectInfo!.OrderNo)
       .ToListAsync();

            return y;
        }

        public async Task<WaterConnection> getPreviousBillForWaterConnection(int year, int month, int wcId)
        {
            var y = await waterBillingDbContext.WaterConnections
                .Include(wc => wc.Balances)
                .Include(wc => wc.MeterConnectInfo)
                .Include(wc => wc.SubRoad)
                .ThenInclude(wc => wc.WaterProject)
                .Include(wc => wc.ActiveNature)
                .Where(wc => wc.Id == wcId)
                .Include(wc => wc.Balances!.Where(b => String.Concat(b.Year, b.Month) == String.Concat(year, month) && b.IsFilled == true))
                .Where(wc => wc.Balances!.Count() > 0)
                .FirstOrDefaultAsync();

            return y;
        }
    }
}
