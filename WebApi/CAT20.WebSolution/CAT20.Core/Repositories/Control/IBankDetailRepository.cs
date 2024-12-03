using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.Control
{
    public interface IBankDetailRepository : IRepository<BankDetail>
    {
        Task<IEnumerable<BankDetail>> GetAllWithBankDetailAsync();
        Task<BankDetail> GetWithBankDetailByIdAsync(int id);
        Task<IEnumerable<BankDetail>> GetAllWithBankDetailByBankDetailIdAsync(int Id);
    }
}
