using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.HRM.PersonalFile;

namespace CAT20.Core.Repositories.HRM.PersonalFile
{
    public interface ISalaryStructureDataRepository : IRepository<SalaryStructureData>
    {
        Task<SalaryStructureData> GetSalaryStructureDataById(int id);
        Task<IEnumerable<SalaryStructureData>> GetAllSalaryStructureData();

    }
}
