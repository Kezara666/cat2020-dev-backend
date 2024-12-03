using CAT20.Core.HelperModels;
using CAT20.Core.Models.WaterBilling;

namespace CAT20.Core.Services.WaterBilling
{
    public interface IOpeningBalanceInformationService
    {
        Task<OpeningBalanceInformation> GetById(int id);


        Task<OpeningBalanceInformation> Create(OpeningBalanceInformation obj,HTokenClaim token);

        Task<IEnumerable<OpeningBalanceInformation>> CreateMultiple(List<OpeningBalanceInformation> objs);

        Task<IEnumerable<OpeningBalanceInformation>> GetOpeningBalancesForConnectionIds(List<int?> wcKeyIds);

        //--------------- [Start - Define new api to filter openBalance data w.r.t. isProcessed] --------
        Task<OpeningBalanceInformation> GetNotProcessedOpenBalancesByWaterConnectionId(int waterConnectionId);

        Task<IEnumerable<OpeningBalanceInformation>> GetAllNotProcessedOpenBalancesForConnectionIds(List<int?> wcKeyIds);

        Task<IEnumerable<OpeningBalanceInformation>> GetAllProcessedOpenBalancesForConnectionIds(List<int?> wcKeyIds);

        Task<(bool, string?)> UpdateNotProcessedOpenBalance(OpeningBalanceInformation obj, HTokenClaim token);
        //--------------- [End - Define new api to filter openBalance data w.r.t. isProcessed] ----------


        Task<IEnumerable<WaterConnection>> GetAllWaterConnectionsNotProcessedOpenBalanceForOfficeId(int officeId);
        Task<WaterConnection> GetWaterConnectionsNotProcessedOpenBalance(int wcId);

    }
}
