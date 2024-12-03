using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.WaterBilling
{
    public interface IWaterConnectionService
    {

        
        Task<IEnumerable<WaterConnection>> GetAllWaterConnections();

        Task<IEnumerable<WaterConnection>> GetAllWaterConnectionsForOffice(int officeId);
        Task<(int totalCount, IEnumerable<WaterConnection> list)> GetAllWaterConnectionsForSabha(List<int> officeIdListOfSabha, int? waterProjectId, int? subRoadId, int? natureId, int? connectionStatus, int pageNo, int pageSize, string? filterValue);
        Task<(int totalCount, IEnumerable<WaterConnection> list)> getAllMeterReadingUpdatedWaterConnectionsForSabhaAndMonth(List<int> officeIdListOfSabha, int? waterProjectId, int? subRoadId, int? natureId, int? connectionStatus, int pageNo, int pageSize, string? filterValue,int year,int month);
        Task<(int totalCount, IEnumerable<WaterConnection> list)> getAllMeterReadingNotUpdatedWaterConnectionsForSabhaAndMonth(List<int> officeIdListOfSabha, int? waterProjectId, int? subRoadId, int? natureId, int? connectionStatus, int pageNo, int pageSize, string? filterValue,int? year, int? month);
        Task<IEnumerable<WaterConnection>> getAllConnNatureChangeRequestForSabha(List<int> officeIdListOfSabha);
        Task<IEnumerable<WaterConnection>> getAllConnStatusChangeRequestForSabha(List<int> officeIdListOfSabha);

        Task<WaterConnection> GetById(int id);
        Task<WaterConnection> GetInfoById(int id);

        Task<IEnumerable<WaterConnection>> SearchWaterConnectionsByConnectionId(string searchQuery);
        Task<IEnumerable<WaterConnection>> GetAllConnectionByPartner(int officeid, int partnerId);
        Task<IEnumerable<WaterConnection>> GetAllConnectionsWithZeroOpenBalanceForOffice(int officeId);

        Task<WaterConnection> Create(string applicationNo, string meterConnectionInfoId, int officeId, DateOnly InstallDate, int CreatedBy);

        Task Update(WaterConnection objToBeUpdated,WaterConnection obj);

        Task Delete(WaterConnection obj);

        //--------------- [Start - GetAllWaterConnectionForSubRoad(int subRoadId)] -----
        Task<IEnumerable<WaterConnection>> GetAllWaterConnectionForSubRoad(int subRoadId);
        //--------------- [End - GetAllWaterConnectionForSubRoad(int subRoadId)] -------


        //--------------- [Start - GetAllNotAddOpenBalanceWaterConnectionForSubRoad(int subRoadId)] -------
        Task<IEnumerable<WaterConnection>> GetAllNotAddOpenBalanceWaterConnectionForSubRoad(int subRoadId);
        //--------------- [End - GetAllNotAddOpenBalanceWaterConnectionForSubRoad(int subRoadId)] -------
        Task<IEnumerable<WaterConnection>> getWaterConnectionsBySubRoad(int subRoadId);


        Task<int> NumberOfConnectionForSubRoad(int subRoadId);

        Task<IEnumerable<WaterConnection>> GetWaterConnectionsForSabhaAndPartnerId(int sabhaId, int partnerId);
    }
}
