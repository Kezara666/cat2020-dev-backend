using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.WaterBilling
{
    public class WaterProjectMainRoadService : IWaterProjectMainRoadService
    {
        private readonly IWaterBillingUnitOfWork _wb_unitOfWork;
        public WaterProjectMainRoadService(IWaterBillingUnitOfWork wb_unitOfWork)
        {
             _wb_unitOfWork = wb_unitOfWork;
        }

        public async Task<WaterProjectMainRoad> Create(WaterProjectMainRoad newMainRaod)
        {
            try
            {
                await _wb_unitOfWork.WaterProjectMainRoads.AddAsync(newMainRaod);
                await _wb_unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return newMainRaod;
        }

        public async Task<(bool,string)> Delete(int id)
        {
            try
            {

                var mainRoad=await _wb_unitOfWork.WaterProjectMainRoads.GetByIdAsync(id);
                if (mainRoad != null) {
                    if (await _wb_unitOfWork.WaterProjectMainRoads.IsRelationshipsExist(id))
                    {
                        _wb_unitOfWork.WaterProjectMainRoads.Remove(mainRoad);
                        await _wb_unitOfWork.CommitAsync();
                        return (true ,"Succussfull Delete");
                    }
                    else {
                        throw new GeneralException("Unable To Delete");
                    }
                }
                else
                {
                    throw new GeneralException("Unable To Found MainRoad");
                }
          


            }
            catch (Exception ex)
            {
                return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);

            }
        }

        public async Task<IEnumerable<WaterProjectMainRoad>> GetAllForSabha(int sabhaid)
        {
            return await _wb_unitOfWork.WaterProjectMainRoads.GetAllForSabha(sabhaid);
                
        }

        public async Task<IEnumerable<WaterProjectMainRoad>> GetAllMainRoads()
        {
            return await _wb_unitOfWork.WaterProjectMainRoads.GetAllAsync();
        }

        public async Task<IEnumerable<WaterProjectMainRoad>> GetAllMainRoadsForProject(int waterProjectId)
        {
            return await _wb_unitOfWork.WaterProjectMainRoads.GetAllMainRoadsForProject(waterProjectId);
        }

        public async Task<WaterProjectMainRoad> GetById(int id)
        {
            return await _wb_unitOfWork.WaterProjectMainRoads.GetByIdAsync(id);
        }

        public  async Task Update(WaterProjectMainRoad objToBeUpdated, WaterProjectMainRoad obj)
        {
            objToBeUpdated.Name = obj.Name; 
            objToBeUpdated.SabhaId = obj.SabhaId;
            objToBeUpdated.UpdatedBy = obj.UpdatedBy;
            objToBeUpdated.UpdatedAt = DateTime.Now;

            await _wb_unitOfWork.CommitAsync();

        }
    }
}
