using CAT20.Core.HelperModels;
using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.WaterBilling
{
    public interface IMeterConnectInfoService
    {
        Task<IEnumerable<MeterConnectInfo>> GetAllConnection();

        Task<MeterConnectInfo> GetById(string id);
        Task<MeterConnectInfo> GetInfoById(string id);
        Task<(bool, string, MeterConnectInfo)> Create(MeterConnectInfo obj, HTokenClaim token, int keyPattern);


        Task<IEnumerable<MeterConnectInfo>> SaveMultipleMeterConnections(List<MeterConnectInfo> meterConnectInfos);
        Task<(bool, string?, MeterConnectInfo)> Update(MeterConnectInfo newobj, HTokenClaim token);

        Task UpdateOrderNo(string ConnectionId, int orderNo);
        Task<bool> Delete(string meterconnectionInfoId);


        //additional 

        Task<IEnumerable<MeterConnectInfo>> GetAllByOrderUnderSubRoad(int subRoadId);
        Task<IEnumerable<MeterConnectInfo>> GetAllByOrderUnderSubRoadList(List<int?> subRoadIds);
        Task<IEnumerable<MeterConnectInfo>> GetAllAvailableByOrderUnderSubRoad(int subRoadId);
        Task<IEnumerable<MeterConnectInfo>> GetAllAssignedByOrderUnderSubRoad(int subRoadId);



    }
}
