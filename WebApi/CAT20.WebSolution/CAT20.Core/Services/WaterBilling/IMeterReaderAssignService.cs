using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.WaterBilling
{
    public interface IMeterReaderAssignService
    {
        Task<IEnumerable<MeterReaderAssign>> GetAllAssigns();
        Task<IEnumerable<MeterReaderAssign>> GetAllForMeterReader(int readerId);
        //Task<IEnumerable<MeterReaderAssign>> AssignSubRoadsForMeterReader(int readerId, List<MeterReaderAssign> meterReaderAssigns);
        Task<IEnumerable<MeterReaderAssign>> AssignSubRoadsForMeterReader(int readerId, List<WaterProjectSubRoad> subRoadsToAssign, int createdBy,int updatedBy);

        Task<MeterReaderAssign> GetById(int id);
        Task<MeterReaderAssign> Create(MeterReaderAssign obj);

        Task Update(MeterReaderAssign objToBeUpdated, MeterReaderAssign obj);

        Task Delete(MeterReaderAssign obj);
    }
}
