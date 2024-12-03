using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;

namespace CAT20.Core.Repositories.Vote
{
    public interface IAccountDetailRepository : IRepository<AccountDetail>
    {
        Task<IEnumerable<AccountDetail>> GetAllWithAccountDetailAsync();
        Task<AccountDetail> GetWithAccountDetailByIdAsync(int id);
        Task<IEnumerable<AccountDetail>> GetAllWithAccountDetailByBankIdAsync(int Id);
        Task<AccountDetail> GetAllWithAccountDetailByBankIdAsync1(int Id);
        Task<IEnumerable<AccountDetail>> GetAllWithAccountDetailByOfficeIdAsync(int OfficeId);
        Task<IEnumerable<AccountDetail>> GetAllWithAccountDetailByBankIdandOfficeIdAsync(int BankId, int OfficeId);
    }
}
