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
    public class EmployeeTypeDataRepository : Repository<EmployeeTypeData>, IEmployeeTypeDataRepository
    {
        public EmployeeTypeDataRepository(DbContext context) : base(context)
        {

        }

        public async Task<EmployeeTypeData?> GetEmployeeTypeDataById(int id)
        {
            return await HRMDbContext.EmployeeTypeData
                .Where(e => e.Id == id && e.Status == 1)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EmployeeTypeData>> GetAllEmployeeTypeData()
        {
            return await HRMDbContext.EmployeeTypeData
                .Where(e => e.Status == 1)
                .ToListAsync();
        }

        private HRMDbContext HRMDbContext
        {
            get { return Context as HRMDbContext; }
        }
    }
}