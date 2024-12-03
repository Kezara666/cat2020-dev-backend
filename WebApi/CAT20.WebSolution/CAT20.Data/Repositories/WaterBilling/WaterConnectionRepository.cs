using CAT20.Core.Models.Control;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Repositories.WaterBilling;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.WaterBilling
{
    internal class WaterConnectionRepository : Repository<WaterConnection>, IWaterConnectionRepository
    {
        public WaterConnectionRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<WaterConnection>> GetAllWaterConnectionsForOffice(int officeId)
        {
            var waterProjects = waterBillingDbContext.WaterProjects.Where(wp => wp.OfficeId == officeId).Include(sr => sr.SubRoads).ToList();
            var subroadIds = waterProjects.SelectMany(wp => wp.SubRoads).Select(sr => sr.Id).Distinct().ToList();

            return await waterBillingDbContext.WaterConnections
                .Where(wc => subroadIds.Contains(wc.SubRoadId.Value))
                 .Where(wc => wc.Status == 1)
                .Include(wc => wc.ActiveNature)
                .Include(wc => wc.StatusInfos)
                .Include(wc => wc.NatureInfos)
                .ThenInclude(nat => nat.Nature).ToListAsync();
        }

        public async Task<(int totalCount, IEnumerable<WaterConnection> list)> GetAllWaterConnectionsForSabha(List<int> officeIdListOfSabha, int? waterProjectId, int? subRoadId,int? natureId,int? connectionStatus,int pageNo,int pageSize,string? filterValue)
        {
            if (filterValue != "undefined")
            {
                //filterValue = "%" + filterValue + "%";
            }
            else if (filterValue == "undefined")
            {
                filterValue = null;
            }


            var waterProjects = waterBillingDbContext.WaterProjects.Where(wp => officeIdListOfSabha.Contains(wp.OfficeId))
                                    .Where(wp => waterProjectId.HasValue ? wp.Id == waterProjectId : true)
                                    .Include(sr => sr.SubRoads).ToList();

            var subroadIds = waterProjects.SelectMany(wp => wp.SubRoads).Select(sr => sr.Id).Distinct().ToList();

            var result = waterBillingDbContext.WaterConnections
                .Where(wc => subroadIds.Contains(wc.SubRoadId.Value) && wc.ConnectionId != null && wc.Status == 1)
                .Where(wc => subRoadId.HasValue ? wc.SubRoadId == subRoadId : true)
                .Where(wc => natureId.HasValue ? wc.ActiveNatureId == natureId : true)
                .Where(wc => connectionStatus.HasValue ? wc.ActiveStatus == connectionStatus : true)
                .Where(wc => EF.Functions.Like(wc.MeterConnectInfo!.ConnectionNo!, "%" + filterValue + "%"))
                .Include(wc => wc.ActiveNature)
                .Include(wc => wc.StatusInfos)
                //.Include(wc => wc.NatureInfos)
                //.ThenInclude(nat => nat.Nature)
                .Include(wc => wc.MeterConnectInfo);

            int totalCount = await result.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
        }




        public async Task<(int totalCount, IEnumerable<WaterConnection> list)> getAllMeterReadingUpdatedWaterConnectionsForSabhaAndMonth(List<int> officeIdListOfSabha, int? waterProjectId, int? subRoadId, int? natureId, int? connectionStatus, int pageNo, int pageSize, string? filterValue,int year,int month)
        {
            if (filterValue != "undefined")
            {
                //filterValue = "%" + filterValue + "%";
            }
            else if (filterValue == "undefined")
            {
                filterValue = null;
            }


            var waterProjects = waterBillingDbContext.WaterProjects.Where(wp => officeIdListOfSabha.Contains(wp.OfficeId))
                                    .Where(wp => waterProjectId.HasValue ? wp.Id == waterProjectId : true)
                                    .Include(sr => sr.SubRoads).ToList();

            var subroadIds = waterProjects.SelectMany(wp => wp.SubRoads).Select(sr => sr.Id).Distinct().ToList();

            var result = waterBillingDbContext.WaterConnections
                .Where(wc => subroadIds.Contains(wc.SubRoadId.Value) && wc.ConnectionId != null && wc.Status == 1)
                .Where(wc => subRoadId.HasValue ? wc.SubRoadId == subRoadId : true)
                .Where(wc => natureId.HasValue ? wc.ActiveNatureId == natureId : true)
                .Where(wc => connectionStatus.HasValue ? wc.ActiveStatus == connectionStatus : true)
                .Where(wc => EF.Functions.Like(wc.MeterConnectInfo!.ConnectionNo!, "%" + filterValue + "%"))
                .Include(wc => wc.ActiveNature)
                .Include(wc => wc.StatusInfos)
                //.Include(wc => wc.NatureInfos)
                //.ThenInclude(nat => nat.Nature)
                .Include(wc => wc.MeterConnectInfo)
                //.Include(wc => wc.Balances)
                .Where(wc => wc.Balances.Any(b => b.Year == year && b.Month == month && b.ThisMonthMeterReading >= b.PreviousMeterReading ));
            
            int totalCount = await result.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
        }

        public async Task<(int totalCount, IEnumerable<WaterConnection> list)> getAllMeterReadingNotUpdatedWaterConnectionsForSabhaAndMonth(List<int> officeIdListOfSabha, int? waterProjectId, int? subRoadId, int? natureId, int? connectionStatus, int pageNo, int pageSize, string? filterValue, int? year, int? month)
        {
            if (filterValue != "undefined")
            {
                //filterValue = "%" + filterValue + "%";
            }
            else if (filterValue == "undefined")
            {
                filterValue = null;
            }


            var waterProjects = waterBillingDbContext.WaterProjects.Where(wp => officeIdListOfSabha.Contains(wp.OfficeId))
                                    .Where(wp => waterProjectId.HasValue ? wp.Id == waterProjectId : true)
                                    .Include(sr => sr.SubRoads).ToList();

            var subroadIds = waterProjects.SelectMany(wp => wp.SubRoads).Select(sr => sr.Id).Distinct().ToList();

            var result = waterBillingDbContext.WaterConnections
                .Where(wc => subroadIds.Contains(wc.SubRoadId.Value) && wc.ConnectionId != null && wc.Status == 1)
                .Where(wc => subRoadId.HasValue ? wc.SubRoadId == subRoadId : true)
                .Where(wc => natureId.HasValue ? wc.ActiveNatureId == natureId : true)
                .Where(wc => connectionStatus.HasValue ? wc.ActiveStatus == connectionStatus : true)
                .Where(wc => EF.Functions.Like(wc.MeterConnectInfo!.ConnectionNo!, "%" + filterValue + "%"))
                .Include(wc => wc.ActiveNature)
                .Include(wc => wc.StatusInfos)
                //.Include(wc => wc.NatureInfos)
                //.ThenInclude(nat => nat.Nature)
                .Include(wc => wc.MeterConnectInfo)
                //.Include(wc => wc.Balances)
                //.Where(wc => wc.Balances.Any(b =>  year.HasValue? b.Year == year && month.HasValue? b.Month == month && b.ThisMonthMeterReading == null));
                .Where(wc => wc.Balances.Any(b =>
                (!year.HasValue || b.Year == year.Value) &&
                (!month.HasValue || b.Month == month.Value) &&
                b.ThisMonthMeterReading == null));

            int totalCount = await result.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);
        }
        public async Task<WaterConnection> GetInfoById(int id)
        {
            return await waterBillingDbContext.WaterConnections
                .Include(wc => wc.SubRoad).ThenInclude(sr => sr.WaterProject)
                .Include(wc => wc.ActiveNature).Include(wc => wc.SubRoad.MainRoad)
                .Include(wc => wc.NatureInfos).ThenInclude(nat => nat.Nature)
                .Include(wc => wc.StatusInfos).Include(wc => wc.Documents)
                .Include(wc=>wc.MeterConnectInfo)
                .FirstOrDefaultAsync(wc => wc.Id == id && wc.Status==1);

        }


        public async Task<WaterConnection> GetByIdWithSubRoad(int id)
        {
            return await waterBillingDbContext.WaterConnections.Include(wc => wc.SubRoad)
                 .Where(wc => wc.Status == 1)
                .FirstOrDefaultAsync(wc => wc.Id == id);

        }

        public async Task<IEnumerable<WaterConnection>> GetAllConnectionByPartner(int officeid, int partnerId)
        {
            var wcs = await waterBillingDbContext.WaterConnections
                .Where(wc => wc.OfficeId == officeid && wc.PartnerId == partnerId)
                .Where(wc=> wc.Status==1)
                .Include(wc => wc.ActiveNature)
                .Include(wc => wc.SubRoad)
                .ThenInclude(sr => sr.WaterProject)
                .Include(wc => wc.SubRoad.MainRoad)
                .Include(wc => wc.StatusInfos)
                .Include(wc => wc.NatureInfos)
                .ThenInclude(n => n.Nature)
                .OrderBy(wc => wc.ConnectionId).ToListAsync();
            foreach (var wc in wcs)
            {
                // Project the StatusInfos into NatureInfos with reversed order
                wc.NatureInfos = wc.NatureInfos
                    .OrderByDescending(ntf => ntf.Id)
                    .Select(ntf => new WaterConnectionNatureLog
                    {
                        Id = ntf.Id,
                        ConnectionId = ntf.ConnectionId,
                        NatureId = ntf.NatureId,
                        Nature = ntf.Nature,
                        Status = ntf.Status,
                        ActivatedDate = ntf.ActivatedDate,
                        ActionBy = ntf.ActionBy,
                        CreatedAt = ntf.CreatedAt,
                        CreatedBy = ntf.CreatedBy,
                        UpdatedBy = ntf.UpdatedBy,

                    })
                    .ToList();

                wc.StatusInfos = wc.StatusInfos
                  .OrderByDescending(stf => stf.Id)
                  .Select(stf => new WaterConnectionStatusLog
                  {
                      Id = stf.Id,
                      ConnectionId = stf.ConnectionId,
                      ConnectionStatus = stf.ConnectionStatus,
                      Comment = stf.Comment,
                      Status = stf.Status,
                      ActivatedDate = stf.ActivatedDate,
                      ActionBy = stf.ActionBy,
                      CreatedAt = stf.CreatedAt,
                      CreatedBy = stf.CreatedBy,
                      UpdatedBy = stf.UpdatedBy,

                  })
                  .ToList();

            }

            return wcs;

        }

        public async Task<IEnumerable<WaterConnection>> GetAllConnectionsWithZeroOpenBalanceForOffice(int officeId)
        {
            var waterProjects = waterBillingDbContext.WaterProjects.Where(wp => wp.OfficeId == officeId).Include(sr => sr.SubRoads).ToList();
            var subroadIds = waterProjects.SelectMany(wp => wp.SubRoads).Select(sr => sr.Id).Distinct().ToList();

            //return await waterBillingDbContext.WaterConnections.Where(wc => subroadIds.Contains(wc.SubRoadId.Value)).Include(wc => wc.StatusInfos).Include(wc => wc.NatureInfos).ThenInclude(nat => nat.Nature).ToListAsync();

            return await waterBillingDbContext.WaterConnections
                .Where(wc => subroadIds.Contains(wc.SubRoadId.Value) && wc.OpeningBalanceInformation == null && wc.ConnectionId != null)
                .Where(wc => wc.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<WaterConnection>> SearchWaterConnectionsByConnectionId(string searchQuery)
        {
            return await waterBillingDbContext.WaterConnections.Where(wc => wc.ConnectionId.Contains(searchQuery))
                 .Where(wc => wc.Status == 1)
                .ToListAsync();
        }



        public async Task<IEnumerable<WaterConnection>> getAllConnNatureChangeRequestForSabha(List<int> officeIdListOfSabha)
        {

            var waterProjects = waterBillingDbContext.WaterProjects.Where(wp => officeIdListOfSabha.Contains(wp.OfficeId)).Include(sr => sr.SubRoads).ToList();
            var subroadIds = waterProjects.SelectMany(wp => wp.SubRoads).Select(sr => sr.Id).Distinct().ToList();

            return await waterBillingDbContext.WaterConnections
                .Where(wc => subroadIds.Contains(wc.SubRoadId.Value) && wc.NatureChangeRequest == true)
                .Include(wc => wc.ActiveNature).Include(wc => wc.StatusInfos)
                .Include(wc => wc.NatureInfos)
                .ThenInclude(nat => nat.Nature)
                 .Where(wc => wc.Status == 1).
                 ToListAsync();
        }

        public async Task<IEnumerable<WaterConnection>> getAllConnStatusChangeRequestForSabha(List<int> officeIdListOfSabha)
        {

            var waterProjects = waterBillingDbContext.WaterProjects.Where(wp => officeIdListOfSabha.Contains(wp.OfficeId)).Include(sr => sr.SubRoads).ToList();
            var subroadIds = waterProjects.SelectMany(wp => wp.SubRoads).Select(sr => sr.Id).Distinct().ToList();

            return await waterBillingDbContext.WaterConnections
                .Where(wc => subroadIds.Contains(wc.SubRoadId.Value) && wc.StatusChangeRequest == true)
                 .Where(wc => wc.Status == 1)
                .Include(wc => wc.ActiveNature)
                .Include(wc => wc.StatusInfos)
                .Include(wc => wc.NatureInfos)
                .ThenInclude(nat => nat.Nature).ToListAsync();
        }

        private WaterBillingDbContext waterBillingDbContext
        {
            get { return Context as WaterBillingDbContext; }
        }

        // private async  Task<>> getAllSaba(int partnerId)
        // {
        //     
        //     var result = from wc in waterBillingDbContext.WaterConnections
        //         join sr in waterBillingDbContext.WaterProjectSubRoads on wc.SubRoadId equals sr.Id
        //         join mr in waterBillingDbContext.WaterProjectMainRoads on sr.MainRoadId equals mr.Id
        //         where wc.PartnerId == partnerId
        //         select new
        //         {
        //             SabhaId = mr.SabhaId,
        //             // Include other properties from WaterConnections, WaterProjectSubRoads, or WaterProjectMainRoads as needed
        //         };
        // }


        //--------------- [Start - GetAllWaterConnectionForSubRoad(int subRoadId)] -----
        public async Task<IEnumerable<WaterConnection>> GetAllWaterConnectionForSubRoad(int subRoadId)
        {
            //get all connections in subroad
            return await waterBillingDbContext.WaterConnections.Include(wc => wc.SubRoad)
                 .Where(wc => wc.Status == 1)
                .Where(sr => sr.SubRoadId == subRoadId)
                .OrderBy(wc => wc.ConnectionId).ToListAsync();
        }
        //--------------- [End - GetAllWaterConnectionForSubRoad(int subRoadId)] -------


        public async Task<IEnumerable<WaterConnection>> GetAllNotAddOpenBalanceWaterConnectionForSubRoad(int subRoadId)
        {
            return await waterBillingDbContext.WaterConnections
                 .Where(wc => wc.Status == 1)
                .Include(wc=>wc.MeterConnectInfo)
                .Include(wc => wc.SubRoad)
                .Where(sr => sr.SubRoadId == subRoadId && sr.OpeningBalanceInformation == null && sr.ConnectionId != null).OrderBy(wc => wc.ConnectionId).ToListAsync();
        }

        public async Task<int> NumberOfConnectionForSubRoad(int subRoadId)
        {
           return await waterBillingDbContext.WaterConnections.Where(wc => wc.SubRoadId == subRoadId).CountAsync();
        }

        public async Task<List<int?>> HasOverPayments(int subRoadId)
        {
            return await  waterBillingDbContext.WaterConnections
                 .Where(wc => wc.Status == 1)
                .Where(wc => wc.SubRoadId == subRoadId && wc.RunningOverPay>0)
                .Where(wc=>wc.Balances!.Any(b=>b.IsCompleted==false))
                .Select(wc=>wc.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<WaterConnection>> getWaterConnectionsBySubRoad(int subRoadId)
        {
            return await waterBillingDbContext.WaterConnections
                 .Where(wc => wc.Status == 1)
                .Include(wc=>wc.MeterConnectInfo)
                .Where(wc => wc.SubRoadId == subRoadId).ToListAsync();    
        }

        public async Task<IEnumerable<WaterConnection>> GetWaterConnectionsForSabhaAndPartnerId(int sabhaId, int partnerId)
        {
            var wcs = await waterBillingDbContext.WaterConnections
                .Where(wc => wc.SubRoad.MainRoad.SabhaId == sabhaId && wc.PartnerId == partnerId)
                 .Where(wc => wc.Status == 1)
                .Include(wc => wc.ActiveNature).Include(wc => wc.SubRoad)
                .ThenInclude(sr => sr.WaterProject)
                .Include(wc => wc.SubRoad.MainRoad)
                .Include(wc => wc.StatusInfos).Include(wc => wc.NatureInfos)
                .ThenInclude(n => n.Nature).OrderBy(wc => wc.ConnectionId).ToListAsync();
            foreach (var wc in wcs)
            {
                // Project the StatusInfos into NatureInfos with reversed order
                wc.NatureInfos = wc.NatureInfos
                    .OrderByDescending(ntf => ntf.Id)
                    .Select(ntf => new WaterConnectionNatureLog
                    {
                        Id = ntf.Id,
                        ConnectionId = ntf.ConnectionId,
                        NatureId = ntf.NatureId,
                        Nature = ntf.Nature,
                        Status = ntf.Status,
                        ActivatedDate = ntf.ActivatedDate,
                        ActionBy = ntf.ActionBy,
                        CreatedAt = ntf.CreatedAt,
                        CreatedBy = ntf.CreatedBy,
                        UpdatedBy = ntf.UpdatedBy,

                    })
                    .ToList();

                wc.StatusInfos = wc.StatusInfos
                  .OrderByDescending(stf => stf.Id)
                  .Select(stf => new WaterConnectionStatusLog
                  {
                      Id = stf.Id,
                      ConnectionId = stf.ConnectionId,
                      ConnectionStatus = stf.ConnectionStatus,
                      Comment = stf.Comment,
                      Status = stf.Status,
                      ActivatedDate = stf.ActivatedDate,
                      ActionBy = stf.ActionBy,
                      CreatedAt = stf.CreatedAt,
                      CreatedBy = stf.CreatedBy,
                      UpdatedBy = stf.UpdatedBy,

                  })
                  .ToList();

            }

            return wcs;

        }

        public async Task<IEnumerable<WaterConnection>> GetYearEndProcessForFinalAccount(List<int?> officeIdListOfSabha)
        {
          

            return await waterBillingDbContext.WaterConnections
                .Where(wc => officeIdListOfSabha.Contains(wc.OfficeId!.Value) && wc.ConnectionId != null)
                .Where(wc => wc.Status == 1)
                .Include(wc=>wc.SubRoad!.WaterProject!)
                .Include(wc=>wc.MeterConnectInfo)
                .Include(wc=>wc.OpeningBalanceInformation)
                .ToListAsync();
        }

        public async Task<WaterConnection> GetYearEndProcessForFinalAccount(int wcId)
        {


            return await waterBillingDbContext.WaterConnections
                .Where(wc => wc.Id == wcId && wc.Status ==1)
                 .Where(wc => wc.Status == 1)
                .AsNoTracking()
                .Include(wc => wc.SubRoad!.WaterProject!)
                .Include(wc => wc.MeterConnectInfo)
                .Include(wc => wc.OpeningBalanceInformation)
                .FirstOrDefaultAsync();
        }
    }
}
