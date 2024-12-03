using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;

namespace CAT20.Core.Repositories.Vote
{
    public interface IAccountBalanceDetailRepository : IRepository<AccountBalanceDetail>
    {
        Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailAsync();
        Task<AccountBalanceDetail> GetWithAccountBalanceDetailByIdAsync(int id);
        Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailByAccountBalanceDetailIdAsync(int Id);
        Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailByAccountDetailIdAsync(int Id);
        Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailBySabhaIdAsync(int Id);
        Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailByAccountDetailIdSabhaIdAsync(int AccountDetailId, int SabhaId);
        Task<IEnumerable<AccountBalanceDetail>> GetAllAccountBalanceDetailsForSabhaIdAsync(int SabhaId);
        Task<IEnumerable<AccountBalanceDetail>> GetAllWithAccountBalanceDetailByAccountIdAsync(int AccountDetailId);
        Task<AccountBalanceDetail> GetAllWithAccountBalanceDetailByAccountIdAsync1(int AccountDetailId);

        
    }
}
