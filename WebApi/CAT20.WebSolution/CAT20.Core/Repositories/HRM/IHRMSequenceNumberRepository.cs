using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.HRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.HRM
{
    public interface IHRMSequenceNumberRepository:IRepository<HRMSequenceNumber>
    {

        Task<HRMSequenceNumber> GetNextSequenceNumberForYearSabhaModuleType(int year, int? sabhaId,int moduleType);

    }
}
