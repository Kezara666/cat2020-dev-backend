using CAT20.Core;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.WaterBilling;

namespace CAT20.Services.WaterBilling
{
    public class WaterProjectGnDivisionService : IWaterProjectGnDivisionService
    {
        IWaterBillingUnitOfWork _wb_unitOfWork;
        public WaterProjectGnDivisionService(IWaterBillingUnitOfWork wb_unitOfWork)
        {
            _wb_unitOfWork = wb_unitOfWork;
        }

        public async Task<WaterProjectGnDivision> Create(WaterProjectGnDivision addGnDivision)
        {
            try
            {
                if (_wb_unitOfWork.WaterProjectGnDivisions.IsExistGnDivision(addGnDivision.WaterProjectId.Value, addGnDivision.ExternalGnDivisionId.Value))
                {

                    return new WaterProjectGnDivision { Id = -1, };
                }
                else
                {
                    await _wb_unitOfWork.WaterProjectGnDivisions.AddAsync(addGnDivision);
                    await _wb_unitOfWork.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return addGnDivision;
        }

        public async Task Delete(WaterProjectGnDivision obj)
        {
            _wb_unitOfWork.WaterProjectGnDivisions.Remove(obj);
            await _wb_unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<WaterProjectGnDivision>> GetAllWaterProjectGnDivisions()
        {
            return await _wb_unitOfWork.WaterProjectGnDivisions.GetAllAsync();
        }


        public async Task<IEnumerable<WaterProjectGnDivision>> GetAllWaterProjectGnDivisionForProject(int waterProjectId)
        {
            var divisionsUnderProject = await _wb_unitOfWork.WaterProjectGnDivisions.GetAllWaterProjectGnDivisionForProject(waterProjectId);
            return divisionsUnderProject;
        }

        public async Task<IEnumerable<WaterProjectGnDivision>> GetAllWaterProjectGnDivisionForOffice(int officeid)
        {
            var divisionsUnderProject = await _wb_unitOfWork.WaterProjectGnDivisions.GetAllWaterProjectGnDivisionForOffice(officeid);
            return divisionsUnderProject;
        }

        public async Task<IEnumerable<GnDivisions>> GetAllGnDivisionForProject(int waterProjectId)
        {
            var gnDivisionIds = await _wb_unitOfWork.WaterProjectGnDivisions.GetAllGnDivisionIdsForProject(waterProjectId);

            return await _wb_unitOfWork.GnDivisions.GetAllForListAsync(gnDivisionIds.ToList());
        }


        public async Task<IEnumerable<WaterProjectGnDivision>> GetAllWaterProjectsUnderGnDivision(int gnDivisionId)
        {
            return await _wb_unitOfWork.WaterProjectGnDivisions.GetAllWaterProjectsUnderGnDivision(gnDivisionId);
        }

        public Task Update(WaterProjectGnDivision objToBeUpdated, WaterProjectGnDivision obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveGnDivision(int waterProjectId, int externalGnDivisionId)
        {
            return await _wb_unitOfWork.WaterProjectGnDivisions.RemoveGnDivision(waterProjectId, externalGnDivisionId);
        }
    }
}
