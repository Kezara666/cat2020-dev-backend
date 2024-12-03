using CAT20.Core;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.WaterBilling;

namespace CAT20.Services.WaterBilling
{
    public class WaterProjectService : IWaterProjectService
    {
        private readonly IWaterBillingUnitOfWork _wb_unitOfWork;
        private readonly IControlUnitOfWork _controlUnitOfWork;
        private readonly IWaterProjectGnDivisionService wpGnDivisionService;

        public WaterProjectService(IWaterBillingUnitOfWork wb_unitOfWork, IControlUnitOfWork controlUnitOfWork, IWaterProjectGnDivisionService wpGnDivisionService)
        {
            _wb_unitOfWork = wb_unitOfWork;
            _controlUnitOfWork = controlUnitOfWork;
            this.wpGnDivisionService = wpGnDivisionService;
        }


        public async Task<IEnumerable<WaterProject>> GetAllProjects()
        {
            return await _wb_unitOfWork.WaterProjects.GetAllAsync();
        }

        public async Task<WaterProject> GetById(int id)
        {
            return await _wb_unitOfWork.WaterProjects.GetByIdAsync(id);
        }

        public async Task<WaterProject> Create(WaterProject newWaterProject)
        {
            try
            {
                await _wb_unitOfWork.WaterProjects.AddAsync(newWaterProject);
                await _wb_unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return newWaterProject;
        }

        public async Task<bool> Delete(WaterProject obj)
        {
            if (await _wb_unitOfWork.WaterProjects.IsRelationshipsExist(obj.Id))
            {
                _wb_unitOfWork.WaterProjects.Remove(obj);
                await _wb_unitOfWork.CommitAsync();
                return true;
            }

            return false;
        }
        public async Task Update(WaterProject objToBeUpdated, WaterProject obj)
        {
            objToBeUpdated.Name = obj.Name;
            objToBeUpdated.OfficeId = obj.OfficeId;
            objToBeUpdated.UpdatedBy = obj.UpdatedBy;
            objToBeUpdated.UpdatedAt = DateTime.Now;

            await _wb_unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<WaterProject>> GetAllForOffice(int officeid)
        {
            return await _wb_unitOfWork.WaterProjects.GetAllForOffice(officeid);
        }

        public async Task<IEnumerable<WaterProject>> GetAllForSabha(int sabhaid)
        {
            var offices = await _controlUnitOfWork.Offices.GetAllWithOfficeBySabhaIdAsync(sabhaid);

            return await _wb_unitOfWork.WaterProjects.GetAllByOfficeIds(offices.Select(of => of.ID.Value).ToList());

        }

        public async Task<WaterProject> GetWaterProjectById(int id)
        {
            return await _wb_unitOfWork.WaterProjects.GetWaterProjectById(id);
        }

        public async Task<WaterProject> AddNature(int waterProjectId, int natureId)
        {
            return await _wb_unitOfWork.WaterProjects.AddNature(waterProjectId, natureId);
        }



        public async Task<WaterProject> GetAllNaturesForProject(int id)
        {
            return await _wb_unitOfWork.WaterProjects.GetAllNaturesForProject(id);
        }

        public async Task<WaterProject> RemoveNature(int waterProjectId, int natureId)
        {
            return await _wb_unitOfWork.WaterProjects.RemoveNature(waterProjectId, natureId);
        }


    }
}
