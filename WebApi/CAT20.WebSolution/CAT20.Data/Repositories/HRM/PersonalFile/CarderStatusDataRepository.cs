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
    public class CarderStatusDataRepository : Repository<CarderStatusData>, ICarderStatusDataRepository
    {
        public CarderStatusDataRepository(DbContext context) : base(context)
        {

        }

        public async Task<CarderStatusData?> GetCarderStatusDataById(int id)
        {
            return await HRMDbContext.CarderStatusData
                .Where(e => e.Id == id && e.Status == 1)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CarderStatusData>> GetAllCarderStatusData()
        {
            return await HRMDbContext.CarderStatusData
                .Where(e => e.Status == 1)
                .ToListAsync();
        }

        private HRMDbContext HRMDbContext
        {
            get { return Context as HRMDbContext; }
        }
    }
}