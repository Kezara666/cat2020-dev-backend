using CAT20.Core.Models.HRM.PersonalFile;
using CAT20.Core.Repositories.HRM.PersonalFile;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.HRM.PersonalFile
{
    public class ServiceTypeDataRepository : Repository<ServiceTypeData>, IServiceTypeDataRepository
    {
        public ServiceTypeDataRepository(DbContext context) : base(context)
        {

        }

        public async Task<ServiceTypeData?> GetServiceTypeDataById(int id)
        {
            return await HRMDbContext.ServiceTypeData
                .Where(e => e.Id == id && e.Status == 1)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<ServiceTypeData?>> GetServiceTypeDataBySalaryStructureId(int id)
        {
            return await HRMDbContext.ServiceTypeData
                .Where(e => e.SalaryStructureDataId == id && e.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<ServiceTypeData>> GetAllServiceTypeData()
        {
            return await HRMDbContext.ServiceTypeData
                .Where(e => e.Status == 1)
                .ToListAsync();
        }

        private HRMDbContext HRMDbContext
        {
            get { return Context as HRMDbContext; }
        }
    }
}