using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.FinalAccount;

namespace CAT20.Core.Repositories.FinalAccount
{
    public interface IReceivableExchangeNonExchangeRepository :IRepository<ReceivableExchangeNonExchange>
    {

        Task<(int totalCount, IEnumerable<ReceivableExchangeNonExchange> list)> GetAllReceivableExchangrNonExchangeeForSabha(int sabhaId, Nullable<int> ledgerAccountId, int pageNo, int pageSize, string? filterKeyword);

    }
}
