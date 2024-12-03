using CAT20.Core.Models.HRM.PersonalFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.HRM.PersonalFile
{
    public interface ICarderStatusDataService
    {
        Task<CarderStatusData> GetCarderStatusDataById(int id);
        Task<IEnumerable<CarderStatusData>> GetAllCarderStatusData();
    }
}
