using CAT20.Core.Models.WaterBilling;

namespace CAT20.Core.Repositories.WaterBilling
{
    public interface IWaterProjectSubRoadRepository : IRepository<WaterProjectSubRoad>
    {
        Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForMainRoad(int mainRoadId);
        Task<WaterProjectSubRoad> GetByIdWithMainRoad(int id);
        Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForMainRoadForWaterProject(int waterProjectId, int mainRoadId);
        Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForOffice(int officeId);
        Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForSabha(int sabhaId);
        Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsNotAssignedReader(int waterProjectId);
        Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForListOfIds(List<int> ids);

        Task<bool> IsRelationshipsExist(int subRoadID);
    }
}
