using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface ISabhaFundSourceService
    {
        Task<IEnumerable<SabhaFundSource>> GetAllActive();
    }
}
