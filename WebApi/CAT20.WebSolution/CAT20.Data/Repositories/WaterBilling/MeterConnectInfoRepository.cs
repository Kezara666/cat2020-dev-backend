using CAT20.Core.HelperModels;
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
    public class MeterConnectInfoRepository : Repository<MeterConnectInfo>, IMeterConnectInfoRepository
    {
        public MeterConnectInfoRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MeterConnectInfo>> GetAllByOrderUnderSubRoad(int subRoadId)
        {
           //return await  waterBillingDbContext.MeterConnectInfos.OrderBy(mci=>mci.OrderNo).ToListAsync();
           return await  waterBillingDbContext.MeterConnectInfos.Where(mci=>mci.SubRoadId==subRoadId).OrderBy(mci=>mci.OrderNo).ToListAsync();
        }

        public async ValueTask<MeterConnectInfo> GetById(string id)
        {
            return await waterBillingDbContext.MeterConnectInfos.FirstOrDefaultAsync(mci => mci.ConnectionId == id);
        }

        public async Task UpdateOrderNo(string meterConnectionId, int orderNo)
        {
            var meterConnectInfoToUpdate = await waterBillingDbContext.MeterConnectInfos.FirstOrDefaultAsync(e => e.ConnectionId == meterConnectionId);

            if (meterConnectInfoToUpdate != null)
            {
                meterConnectInfoToUpdate.OrderNo = orderNo;
               await waterBillingDbContext.SaveChangesAsync();

            }
        }

        public async Task<IEnumerable<MeterConnectInfo>> GetAllAvailableByOrderUnderSubRoad(int subRoadId)
        {
            return await waterBillingDbContext.MeterConnectInfos.Where(mci => mci.SubRoadId == subRoadId && mci.IsAssigned == false).OrderBy(mci => mci.OrderNo).ToListAsync();
        }

        public async Task<IEnumerable<MeterConnectInfo>> GetAllByOrderUnderSubRoadList(List<int?> subRoadIds)
        {
            //return await waterBillingDbContext.MeterConnectInfos.Where(mci => subRoadIds.Contains(mci.SubRoadId.Value)).Include(mci=>mci.WaterProjectSubRoad)
            //     .OrderBy(mci => mci.WaterProjectSubRoad.Name).ThenBy(mci => mci.OrderNo).ToListAsync();

            return await waterBillingDbContext.MeterConnectInfos.Where(mci => subRoadIds.Contains(mci.SubRoadId.Value)).Include(mci => mci.WaterProjectSubRoad).ToListAsync();
               
        }

        public async Task<MeterConnectInfo> GetInfoById(string id)
        {
            return await waterBillingDbContext.MeterConnectInfos.Include(mci=>mci.WaterProjectSubRoad).Include(mci=>mci.WaterProjectSubRoad.MainRoad).Include(mci => mci.WaterProjectSubRoad.WaterProject).FirstOrDefaultAsync(mci => mci.ConnectionId == id);
        }

        public async Task<bool> IsExist(string generatedKey)
        {
            return  waterBillingDbContext.MeterConnectInfos.Any(mci => mci.ConnectionId == generatedKey);
        }

        public async Task<IEnumerable<MeterConnectInfo>> GetAllAssignedByOrderUnderSubRoad(int subRoadId)
        {
            return await waterBillingDbContext.MeterConnectInfos.Where(mci => mci.SubRoadId == subRoadId && mci.IsAssigned == true).OrderBy(mci => mci.OrderNo).ToListAsync();
        }

        public async Task<bool> AlreadyExist(string meterConnectionNo, List<int?> officeIds)
        {
            var waterProjectIds = waterBillingDbContext.WaterProjects.Where(wp => officeIds.Contains(wp.OfficeId)).Select(wp => wp.Id).ToList();
            var subroadsIds = waterBillingDbContext.WaterProjectSubRoads.Where(wpsr => waterProjectIds.Contains( wpsr.WaterProjectId)).Select(wpsr => wpsr.Id).ToList();
            return await waterBillingDbContext.MeterConnectInfos.AnyAsync(mci => mci.ConnectionNo == meterConnectionNo && subroadsIds.Contains(mci.SubRoadId));
        }

        private WaterBillingDbContext waterBillingDbContext
        {
            get { return Context as WaterBillingDbContext; }
        }
    }
}
