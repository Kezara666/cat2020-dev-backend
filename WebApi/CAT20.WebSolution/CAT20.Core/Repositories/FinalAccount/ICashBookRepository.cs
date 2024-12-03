using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.FinalAccount
{
    public interface ICashBookRepository: IRepository<CashBook>
    {
        Task<(int totalCount, IEnumerable<CashBook> list)> GetAllTransactionsForSabhaByAccount(int sabhaId,int accountId, int pageNo, int pageSize, string? filterKeyWord);
        Task<IEnumerable<CashBookDailyBalance>> GetDailyBalancesAsync(int officeId, int sessionId, DateTime date, int createdby);
    }
}
