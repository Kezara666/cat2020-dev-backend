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
    public class AppointmentTypeDataRepository : Repository<AppointmentTypeData>, IAppointmentTypeDataRepository
    {
        public AppointmentTypeDataRepository(DbContext context) : base(context)
        {

        }

        public async Task<AppointmentTypeData?> GetAppointmentTypeDataById(int id)
        {
            return await HRMDbContext.AppointmentTypeData
                .Where(e => e.Id == id && e.Status == 1)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AppointmentTypeData>> GetAllAppointmentTypeData()
        {
            return await HRMDbContext.AppointmentTypeData
                .Where(e => e.Status == 1)
                .ToListAsync();
        }

        private HRMDbContext HRMDbContext
        {
            get { return Context as HRMDbContext; }
        }
    }
}