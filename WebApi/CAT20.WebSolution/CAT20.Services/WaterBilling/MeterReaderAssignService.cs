using CAT20.Core;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.WaterBilling
{
    public class MeterReaderAssignService : IMeterReaderAssignService
    {
        private readonly IWaterBillingUnitOfWork _wb_unitOfWork;

        public MeterReaderAssignService(IWaterBillingUnitOfWork wb_unitOfWork)
        {
            _wb_unitOfWork = wb_unitOfWork;
        }

        public async Task<IEnumerable<MeterReaderAssign>> AssignSubRoadsForMeterReader(int readerId, List<WaterProjectSubRoad> subRoadsToAssign, int createdBy, int updatedBy)
        {
            try
            {
                var existingEntities = await GetAllForMeterReader(readerId);

                var existingsubRoadsIds = existingEntities.Select(mras => mras.SubRoadId);
                // Remove entities not present in the modified list
                var entitiesToRemove = existingEntities.Where(e => !subRoadsToAssign.Any(sr => sr.Id == e.SubRoadId)).ToList();

                _wb_unitOfWork.MeterReaderAssigns.RemoveRange(entitiesToRemove);

                // filter entities to insert

                var subroadsToAdd = subRoadsToAssign.Where(sr => !existingsubRoadsIds.Contains(sr.Id)).ToList();
               

                var entitiesToAdd = subroadsToAdd.Select(subroad => new MeterReaderAssign
                {
                    MeterReaderId = readerId,
                    SubRoadId = subroad.Id,
                    Status = 1,
                    CreatedBy = createdBy,
                    UpdatedBy = updatedBy
                }).ToList();

                await _wb_unitOfWork.MeterReaderAssigns.AddRangeAsync(entitiesToAdd);

                await _wb_unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            //return meterReaderAssigns;
            return await GetAllForMeterReader(readerId);

        }

        public async Task<MeterReaderAssign> Create(MeterReaderAssign meterReaderAssign)
        {
            try
            {
                await _wb_unitOfWork.MeterReaderAssigns.AddAsync(meterReaderAssign);
                await _wb_unitOfWork.CommitAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return meterReaderAssign;
        }

        public async Task Delete(MeterReaderAssign obj)
        {
            _wb_unitOfWork.MeterReaderAssigns.Remove(obj);
            await _wb_unitOfWork.CommitAsync();
        }

        public Task<IEnumerable<MeterReaderAssign>> GetAllAssigns()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MeterReaderAssign>> GetAllForMeterReader(int readerId)
        {
           return await _wb_unitOfWork.MeterReaderAssigns.GetAllForMeterReader(readerId);
        }

        public Task<MeterReaderAssign> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(MeterReaderAssign objToBeUpdated, MeterReaderAssign obj)
        {
            throw new NotImplementedException();
        }
    }
}
