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
    public class JobTitleDataRepository : Repository<JobTitleData>, IJobTitleDataRepository
    {
        public JobTitleDataRepository(DbContext context) : base(context)
        {

        }

        public async Task<JobTitleData?> GetJobTitleDataById(int id)
        {
            return await HRMDbContext.JobTitleData
                .Where(e => e.Id == id && e.Status == 1)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<JobTitleData>> GetAllJobTitleDataByServiceTypeDataId(int id)
        {
            return await HRMDbContext.JobTitleData
                .Where(e => e.ServiceTypeDataId == id && e.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<JobTitleData>> GetAllJobTitleData()
        {
            return await HRMDbContext.JobTitleData
                .Where(e => e.Status == 1)
                .ToListAsync();
        }

        private HRMDbContext HRMDbContext
        {
            get { return Context as HRMDbContext; }
        }
    }
}