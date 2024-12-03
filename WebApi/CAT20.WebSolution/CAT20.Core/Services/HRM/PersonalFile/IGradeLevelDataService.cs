using CAT20.Core.Models.HRM.PersonalFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.HRM.PersonalFile
{
    public interface IGradeLevelDataService
    {
        Task<GradeLevelData> GetGradeLevelDataById(int id);
        Task<IEnumerable<GradeLevelData>> GetAllGradeLevelData();
    }
}
