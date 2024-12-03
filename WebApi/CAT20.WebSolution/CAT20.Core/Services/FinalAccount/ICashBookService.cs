using CAT20.Core.DTO.Final;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.FinalAccount
{
    public interface ICashBookService
    {
        Task<(int totalCount, IEnumerable<CashBookResource> list)> GetAllTransactionsForSabhaByAccount(int sabhaId, int accountId, int pageNo, int pageSize, string? filterKeyWord);

        Task<IEnumerable<CashBookDailyBalance>> GetDailyBalances(int officeId, int sessionId, DateTime date, int createdby);
    }
}
