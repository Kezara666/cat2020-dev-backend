using CAT20.Core.HelperModels;
using CAT20.Core.Models.WaterBilling;

namespace CAT20.Core.Repositories.WaterBilling
{
    public interface IMeterConnectInfoRepository : IRepository<MeterConnectInfo>
    {

        Task<bool> AlreadyExist(string meterConnectionNo, List<int?> officeIds);
        ValueTask<MeterConnectInfo> GetById(string id);

        Task<bool> IsExist(string generatedKey);

        Task<MeterConnectInfo> GetInfoById(string id);
        Task<IEnumerable<MeterConnectInfo>> GetAllByOrderUnderSubRoad(int subRoadId);
        Task<IEnumerable<MeterConnectInfo>> GetAllByOrderUnderSubRoadList(List<int?> subRoadIds);
        Task<IEnumerable<MeterConnectInfo>> GetAllAvailableByOrderUnderSubRoad(int subRoadId);

        Task<IEnumerable<MeterConnectInfo>> GetAllAssignedByOrderUnderSubRoad(int subRoadId);

        Task UpdateOrderNo(string meterConnectionId, int orderNo);
    }
}
