using CAT20.Core.HelperModels;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Vote
{
    public interface IAccountBalanceDetailService
    {
        Task<IEnumerable<AccountBalanceDetail>> GetAllAccountBalanceDetails();
        Task<AccountBalanceDetail> GetAccountBalanceDetailById(int id);
        Task <(bool, string?)> CreateAccountBalanceDetail(AccountBalanceDetail newAccountBalanceDetail,HTokenClaim token);
        Task UpdateAccountBalanceDetail(AccountBalanceDetail accountBalanceDetailToBeUpdated, AccountBalanceDetail accountBalanceDetail);
        Task DeleteAccountBalanceDetail(AccountBalanceDetail accountBalanceDetail);

        Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailByAccountDetailId(int Id);
        Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailBySabhaId(int Id);
        Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailByAccountDetailIdSabhaId(int AccountDetailId, int SabhaId);

        Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailsBySabhaId(int Id);
        Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailByAccountId(int AccountId);
        
    }
}

