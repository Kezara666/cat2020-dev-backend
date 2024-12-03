using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.Final.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;

namespace CAT20.Core.Services.FinalAccount
{
    public interface IIndustrialDebtorsService
    {
        Task<(int totalCount, IEnumerable<IndustrialDebtorsResource> list)> GetAllIndustrialDebtorsForSabha(int sabhaId, int ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim _token);

        Task<(bool, string?)> CreateUpdateIndustrialDebtors(SaveIndustrialDebtorsResource newIndustrialDebitor, HTokenClaim token);

        Task<(bool, string?)> DeleteIndustrialDebtor(int industrialDebtorId, HTokenClaim token);
    }
}
