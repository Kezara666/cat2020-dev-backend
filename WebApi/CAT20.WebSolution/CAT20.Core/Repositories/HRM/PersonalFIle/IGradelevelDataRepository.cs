using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.HRM.PersonalFile;

namespace CAT20.Core.Repositories.HRM.PersonalFile
{
    public interface IGradeLevelDataRepository : IRepository<GradeLevelData>
    {
        Task<GradeLevelData> GetGradeLevelDataById(int id);
        Task<IEnumerable<GradeLevelData>> GetAllGradeLevelData();

    }
}
