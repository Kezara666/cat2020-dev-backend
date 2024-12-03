using CAT20.Core.Models.WaterBilling;

namespace CAT20.Core.Services.WaterBilling
{
    public interface IWaterProjectSubRoadService
    {
        Task<IEnumerable<WaterProjectSubRoad>> GetAllSubRoads();
        Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForMainRoad(int mainRoadId);
        Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForMainRoadForWaterProject(int waterProjectId, int mainRoadId);
        Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForOffice(int officeId);
        Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForSabha(int SabhaId);
        Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsNotAssignedReader(int waterProjectId);
        Task<WaterProjectSubRoad> GetById(int id);
        Task<WaterProjectSubRoad> GetByIdWithMainRoad(int id);
        Task<WaterProjectSubRoad> Create(WaterProjectSubRoad obj);

        Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForListOfIds(List<int> ids);
        Task Update(WaterProjectSubRoad objToBeUpdated, WaterProjectSubRoad obj);

        Task<bool> Delete(WaterProjectSubRoad obj);

    }
}
