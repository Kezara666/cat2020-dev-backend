using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.HRM.PersonalFile;

namespace CAT20.Core.Repositories.HRM.PersonalFile
{
    public interface IJobTitleDataRepository : IRepository<JobTitleData>
    {
        Task<JobTitleData> GetJobTitleDataById(int id);
        Task<IEnumerable<JobTitleData>> GetAllJobTitleDataByServiceTypeDataId(int id);
        Task<IEnumerable<JobTitleData>> GetAllJobTitleData();

    }
}
