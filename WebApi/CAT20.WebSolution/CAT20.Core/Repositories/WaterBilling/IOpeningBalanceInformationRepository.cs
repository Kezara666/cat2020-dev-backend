using CAT20.Core.Models.WaterBilling;

namespace CAT20.Core.Repositories.WaterBilling
{
    public interface IOpeningBalanceInformationRepository : IRepository<OpeningBalanceInformation>
    {
        Task<IEnumerable<OpeningBalanceInformation>> GetOpeningBalancesForConnectionIds(List<int?> wcKeyIds);


        Task<OpeningBalanceInformation> GetNotProcessedOpenBalancesByWaterConnectionId(int waterConnectionId);

        Task<IEnumerable<OpeningBalanceInformation>> GetAllNotProcessedOpenBalancesForConnectionIds(List<int?> wcKeyIds);

        Task<IEnumerable<OpeningBalanceInformation>> GetAllProcessedOpenBalancesForConnectionIds(List<int?> wcKeyIds);


        Task<IEnumerable<WaterConnection>> GetAllWaterConnectionsNotProcessedOpenBalanceForOfficeId(int officeId);
        Task<WaterConnection> GetWaterConnectionsNotProcessedOpenBalance(int wcId);
    }
}
