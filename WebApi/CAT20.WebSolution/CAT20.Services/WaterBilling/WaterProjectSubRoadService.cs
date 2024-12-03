using CAT20.Core;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.WaterBilling;

namespace CAT20.Services.WaterBilling
{
    public class WaterProjectSubRoadService : IWaterProjectSubRoadService
    {
        private readonly IWaterBillingUnitOfWork _wb_unitOfWork;

        public WaterProjectSubRoadService(IWaterBillingUnitOfWork wb_unitOfWork)
        {
            _wb_unitOfWork = wb_unitOfWork;
        }

        public async Task<IEnumerable<WaterProjectSubRoad>> GetAllSubRoads()
        {
            return await _wb_unitOfWork.WaterProjectSubRoads.GetAllAsync();

        }

        public async Task<WaterProjectSubRoad> Create(WaterProjectSubRoad newWPSubRoad)
        {
            try
            {
                await _wb_unitOfWork.WaterProjectSubRoads.AddAsync(newWPSubRoad);
                await _wb_unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new WaterProjectSubRoad();
            }

            return newWPSubRoad;
        }


        public async Task Update(WaterProjectSubRoad objToBeUpdated, WaterProjectSubRoad obj)
        {
            objToBeUpdated.Name = obj.Name;
            objToBeUpdated.WaterProjectId = obj.WaterProjectId;
            objToBeUpdated.MainRoadId = obj.MainRoadId;
            objToBeUpdated.UpdatedBy = obj.UpdatedBy;
            objToBeUpdated.UpdatedAt = DateTime.Now;

            await _wb_unitOfWork.CommitAsync();
        }
        public async Task<bool> Delete(WaterProjectSubRoad obj)
        {
            if (await _wb_unitOfWork.WaterProjectSubRoads.IsRelationshipsExist(obj.Id.Value))
            {
                _wb_unitOfWork.WaterProjectSubRoads.Remove(obj);
                await _wb_unitOfWork.CommitAsync();
                return true;
            }
            return false;
        }

        public async Task<WaterProjectSubRoad> GetById(int id)
        {
            return await _wb_unitOfWork.WaterProjectSubRoads.GetByIdAsync(id);
        }

        public async Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForMainRoad(int mainRoadId)
        {
            return await _wb_unitOfWork.WaterProjectSubRoads.GetSubRoadsForMainRoad(mainRoadId);
        }

        public async Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForOffice(int officeId)
        {
            return await _wb_unitOfWork.WaterProjectSubRoads.GetSubRoadsForOffice(officeId);
        }

        public async Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForSabha(int sabhaId)
        {
            return await _wb_unitOfWork.WaterProjectSubRoads.GetSubRoadsForSabha(sabhaId);
        }

        public async Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsNotAssignedReader(int waterProjectId)
        {
            return await _wb_unitOfWork.WaterProjectSubRoads.GetSubRoadsNotAssignedReader(waterProjectId);
        }

        public async Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForListOfIds(List<int> ids)
        {

            return await _wb_unitOfWork.WaterProjectSubRoads.GetSubRoadsForListOfIds(ids);
        }

        public async Task<IEnumerable<WaterProjectSubRoad>> GetSubRoadsForMainRoadForWaterProject(int waterProjectId, int mainRoadId)
        {
            return await _wb_unitOfWork.WaterProjectSubRoads.GetSubRoadsForMainRoadForWaterProject(waterProjectId, mainRoadId);
        }

        public Task<WaterProjectSubRoad> GetByIdWithMainRoad(int id)
        {
            throw new NotImplementedException();
        }

    }
}