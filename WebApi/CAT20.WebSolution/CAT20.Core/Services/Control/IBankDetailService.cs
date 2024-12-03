using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface IBankDetailService
    {
        Task<IEnumerable<BankDetail>> GetAllBankDetails();
        Task<BankDetail> GetBankDetailById(int id);
        Task<BankDetail> CreateBankDetail(BankDetail newBankDetail);
        Task UpdateBankDetail(BankDetail bankDetailToBeUpdated, BankDetail bankDetail);
        Task DeleteBankDetail(BankDetail bankDetail);
    }
}

