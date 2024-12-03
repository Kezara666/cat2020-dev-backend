﻿using System;
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
    public interface ILABankLoanService
    {
        Task<(int totalCount, IEnumerable<LALoanResource> list)> GetAllLABankLoanForSabha(int sabhaId, int ledgerAccountId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim _token);
        Task<(bool, string?)> CreateUpdateLABankLoan(SaveLALoanResource newLABankLoan, HTokenClaim token);

        Task<(bool, string?)> DeleteLABankLoan(int laBankLoanId, HTokenClaim token);
    }
}