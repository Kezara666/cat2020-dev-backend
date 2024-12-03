using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.FinalAccount
{
    public interface IFinalAccountSequenceNumberRepository:IRepository<FinalAccountSequenceNumber>
    {
        Task<bool> HasSequenceNumberForCurrentYearAndModule(int year, int? sabhaId, FinalAccountModuleType moduleType);

        Task<FinalAccountSequenceNumber> GetNextSequenceNumberForYearSabhaModuleType(int year, int? sabhaId, FinalAccountModuleType moduleType);
    }
}
