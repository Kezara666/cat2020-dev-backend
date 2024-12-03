using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Vote
{
    public interface IAccountDetailService
    {
        Task<IEnumerable<AccountDetail>> GetAllAccountDetails();
        Task<AccountDetail> GetAccountDetailById(int id);
        Task<AccountDetail> CreateAccountDetail(AccountDetail newAccountDetail);
        Task UpdateAccountDetail(AccountDetail accountDetailToBeUpdated, AccountDetail accountDetail);
        Task DeleteAccountDetail(AccountDetail accountDetail);

        Task<IEnumerable<AccountDetail>> GetAllWithAccountDetailByBankId(int Id);
        Task<IEnumerable<AccountDetail>> GetAllAccountDetailByOfficeId(int OfficeId);
        Task<IEnumerable<AccountDetail>> GetAllWithAccountDetailByBankIdandOfficeId(int BankId, int OfficeId);
    }
}

