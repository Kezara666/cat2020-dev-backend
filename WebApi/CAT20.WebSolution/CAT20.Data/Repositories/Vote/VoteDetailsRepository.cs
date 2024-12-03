using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.Vote;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Vote
{
    public class VoteDetailRepository : Repository<VoteDetail>, IVoteDetailRepository
    {
        public VoteDetailRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailAsync()
        {
            return await voteAccDbContext.VoteDetails
                .Where(m => m.Status == 1)
                .ToListAsync();
        }

        public async Task<VoteDetail> GetWithVoteDetailByIdAsync(int id)
        {
            return await voteAccDbContext.VoteDetails
                .Where(m => m.ID == id && m.Status == 1)
                .SingleAsync(); ;
        }

        public async Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailByVoteDetailIdAsync(int Id)
        {
            return await voteAccDbContext.VoteDetails
                .Where(m => m.ID == Id && m.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailBySabhaIdAsync(int SabhaId)
        {
            return await voteAccDbContext.VoteDetails.Where(m => m.SabhaID == SabhaId && m.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailByProgrammeIdAsync(int ProgrammeId)
        {
            return await voteAccDbContext.VoteDetails.Where(m => m.ProgrammeID == ProgrammeId && m.Status == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailByIncomeSubTitleIdAsync(int IncomeSubtitleId)
        {
            return await voteAccDbContext.VoteDetails.Where(m => m.IncomeSubtitleID == IncomeSubtitleId && m.Status == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailByProgrammeIdandSabhaIdAsync(int ProgrammeId, int SabhaId)
        {
            return await voteAccDbContext.VoteDetails.Where(m => m.ProgrammeID == ProgrammeId && m.SabhaID == SabhaId && m.Status == 1)
                .OrderBy(m => m.VoteOrder.Value)
                .ToListAsync();
        }

        public async Task<IEnumerable<VoteDetail>> GetAllVoteDetailsForSabhaIdAsync(int Id)
        {
            return await voteAccDbContext.VoteDetails.Where(m => m.SabhaID == Id && m.Status == 1).ToListAsync();
        }

        public async Task<IEnumerable<VoteDetail>> GetAllVoteDetailsProgammesByClassificationIdAsync(int ClassificationId)
        {
            return await voteAccDbContext.VoteDetails.Where(m => m.ClassificationID == ClassificationId && m.Status == 1).ToListAsync();
        }

        public async Task<IEnumerable<VoteDetail>> GetAllVoteDetailsByProgrammeIdClassificationIdMainLedgerAccIdLedgerCategoryIdLedgerAccountId(int ProgrammeId, int ClassificationId, int MainLedgerAccId, int LedgerCategoryId, int LedgerAccountId, int SabhaId)
        {
            if (ProgrammeId != 0 && ClassificationId == 0 && MainLedgerAccId == 0 && LedgerCategoryId == 0 && LedgerAccountId == 0 && SabhaId != 0)
            {
                var x = await voteAccDbContext.VoteDetails.Where(m => m.ProgrammeID == ProgrammeId && m.SabhaID == SabhaId && m.Status == 1).OrderBy(m => m.VoteOrder).ToListAsync();
                return x;
            }
            else if (ProgrammeId != 0 && ClassificationId != 0 && MainLedgerAccId == 0 && LedgerCategoryId == 0 && LedgerAccountId == 0 && SabhaId != 0)
            {
                var x = await voteAccDbContext.VoteDetails.Where(m => m.ProgrammeID == ProgrammeId && m.ClassificationID == ClassificationId && m.SabhaID == SabhaId && m.Status == 1).OrderBy(m => m.VoteOrder).ToListAsync();
                return x;
            }
            else if (ProgrammeId != 0 && ClassificationId != 0 && MainLedgerAccId != 0 && LedgerCategoryId == 0 && LedgerAccountId == 0 && SabhaId != 0)
            {
                var x = await voteAccDbContext.VoteDetails.Where(m => m.ProgrammeID == ProgrammeId && m.ClassificationID == ClassificationId && m.MainLedgerAccountID == MainLedgerAccId && m.SabhaID == SabhaId && m.Status == 1).OrderBy(m => m.VoteOrder).ToListAsync();
                return x;
            }
            else if (ProgrammeId != 0 && ClassificationId != 0 && MainLedgerAccId != 0 && LedgerCategoryId != 0 && LedgerAccountId != 0 && SabhaId != 0)
            {
                var x = await voteAccDbContext.VoteDetails.Where(m => m.ProgrammeID == ProgrammeId && m.ClassificationID == ClassificationId && m.MainLedgerAccountID == MainLedgerAccId && m.IncomeTitleID == LedgerCategoryId && m.IncomeSubtitleID == LedgerAccountId && m.SabhaID == SabhaId && m.Status == 1).OrderBy(m => m.VoteOrder).ToListAsync();
                return x;
            }
            else if (ProgrammeId == 0 && ClassificationId != 0 && MainLedgerAccId == 0 && LedgerCategoryId == 0 && LedgerAccountId == 0 && SabhaId != 0)
            {
                var x = await voteAccDbContext.VoteDetails.Where(m => m.ClassificationID == ClassificationId && m.SabhaID == SabhaId && m.Status == 1).OrderBy(m => m.VoteOrder).ToListAsync();
                return x;
            }
            else if (ProgrammeId == 0 && ClassificationId != 0 && MainLedgerAccId != 0 && LedgerCategoryId == 0 && LedgerAccountId == 0 && SabhaId != 0)
            {
                var x = await voteAccDbContext.VoteDetails.Where(m => m.ClassificationID == ClassificationId && m.MainLedgerAccountID == MainLedgerAccId && m.SabhaID == SabhaId && m.Status == 1).OrderBy(m => m.VoteOrder).ToListAsync();
                return x;
            }
            else if (ProgrammeId == 0 && ClassificationId != 0 && MainLedgerAccId != 0 && LedgerCategoryId != 0 && LedgerAccountId == 0 && SabhaId != 0)
            {
                var x = await voteAccDbContext.VoteDetails.Where(m => m.ClassificationID == ClassificationId && m.MainLedgerAccountID == MainLedgerAccId && m.IncomeTitleID == LedgerCategoryId && m.SabhaID == SabhaId && m.Status == 1).OrderBy(m => m.VoteOrder).ToListAsync();
                return x;
            }
            else if (ProgrammeId == 0 && ClassificationId != 0 && MainLedgerAccId != 0 && LedgerCategoryId != 0 && LedgerAccountId != 0 && SabhaId != 0)
            {
                var x = await voteAccDbContext.VoteDetails.Where(m => m.ClassificationID == ClassificationId && m.MainLedgerAccountID == MainLedgerAccId && m.IncomeTitleID == LedgerCategoryId && m.IncomeSubtitleID == LedgerAccountId && m.SabhaID == SabhaId && m.Status == 1).OrderBy(m => m.VoteOrder).ToListAsync();
                return x;
            }
            else
            {
                return null;
            }

        }

        public async Task<IEnumerable<VoteDetail>> GetAllLedgerAccountsForDepositSubCategoryId(int depositSubCategoryId, int sabhaId)
        {
            var x = await voteAccDbContext.VoteDetails.Where(m => m.IncomeSubtitleID == depositSubCategoryId && m.Status == 1 && m.SabhaID == sabhaId).OrderBy(m => m.VoteOrder).ToListAsync();
            return x;
        }

        //ImprestAccounts
        public async Task<IEnumerable<VoteDetail>> GetAllImprestLedgerAccountsForSabhaId(int sabhaid)
        {
            var x = await voteAccDbContext.VoteDetails.Where(m => (m.IncomeSubtitleCode == "193" || m.IncomeSubtitleCode == "3551" || m.IncomeSubtitleCode == "3553") && m.Status == 1 && m.SabhaID == sabhaid).OrderBy(m => m.VoteOrder).ToListAsync();
            return x;
        }

        //LoanOfLAAccounts
        public async Task<IEnumerable<VoteDetail>> GetLAbankLoanLeggerAccountForSabhaId(int sabhaid)
        {
            var x = await voteAccDbContext.VoteDetails.Where(m => (m.IncomeSubtitleCode == "7101" || m.IncomeSubtitleCode == "7102" || m.IncomeSubtitleCode == "7109"
            || m.IncomeSubtitleCode == "221" || m.IncomeSubtitleCode == "222" || m.IncomeSubtitleCode == "223"
            ) && m.Status == 1 && m.SabhaID == sabhaid).OrderBy(m => m.VoteOrder).ToListAsync();
            return x;
        }

        //OtherAccountOpeningBalance 
        public async Task<IEnumerable<VoteDetail>> GetOtherAccountsOpeningBalancessLeggerAccountForSabhaId(int sabhaid)
        {
            var x = await voteAccDbContext.VoteDetails.Where(m => (m.IncomeSubtitleCode == "7351" || m.IncomeSubtitleCode == "7352" || m.IncomeSubtitleCode == "7353" || 
            m.IncomeSubtitleCode == "7359"
            || m.IncomeSubtitleCode == "237"
            ) && m.Status == 1 && m.SabhaID == sabhaid).OrderBy(m => m.VoteOrder).ToListAsync();
            return x;
        }

        //IndustrialCreditorAccounts 
        public async Task<IEnumerable<VoteDetail>> GetIndustrialCreditorsLeggerAccountForSabhaId(int sabhaid)
        {
            var x = await voteAccDbContext.VoteDetails.Where(m => (m.IncomeSubtitleCode == "7311" || m.IncomeSubtitleCode == "7312" || m.IncomeSubtitleCode == "7313" || m.IncomeSubtitleCode == "7314" || m.IncomeSubtitleCode == "231") && m.Status == 1 && m.SabhaID == sabhaid).OrderBy(m => m.VoteOrder).ToListAsync();
            return x;
        }

        //IndustrialDebtorAccounts
        public async Task<IEnumerable<VoteDetail>> GetIndustrialDebitorsLeggerAccountForSabhaId(int sabhaid)
        {
            var x = await voteAccDbContext.VoteDetails.Where(m => (m.IncomeSubtitleCode == "2101" || m.IncomeSubtitleCode == "2102" || m.IncomeSubtitleCode == "2103" || m.IncomeSubtitleCode == "2104" || m.IncomeSubtitleCode == "2105" || m.IncomeSubtitleCode == "2106" || m.IncomeSubtitleCode == "2107" || m.IncomeSubtitleCode == "2108" || m.IncomeSubtitleCode == "2109" || m.IncomeSubtitleCode == "2110" || m.IncomeSubtitleCode == "2111" || m.IncomeSubtitleCode == "2112" || m.IncomeSubtitleCode == "2113" || m.IncomeSubtitleCode == "2114" || m.IncomeSubtitleCode == "168") && m.Status == 1 && m.SabhaID == sabhaid).OrderBy(m => m.VoteOrder).ToListAsync();
            //var x = await voteAccDbContext.VoteDetails.Where(m => (m.IncomeSubtitleCode == "3225" || m.IncomeSubtitleCode == "3230") && m.Status == 1 && m.SabhaID == sabhaid).OrderBy(m => m.VoteOrder).ToListAsync();
            return x;
        }

        //StoresAdvanceAccounts
        public async Task<IEnumerable<VoteDetail>> GetStoresAdvanceAccountsLeggerAccountForSabhaId(int sabhaid)
        {
            //var x = await voteAccDbContext.VoteDetails.Where(m => (m.IncomeSubtitleCode == "7303" || m.IncomeSubtitleCode == "7304" || m.IncomeSubtitleCode == "7305" || m.IncomeSubtitleCode == "7306" || m.IncomeSubtitleCode == "7307" || m.IncomeSubtitleCode == "7308" || m.IncomeSubtitleCode == "7309" || m.IncomeSubtitleCode == "7310") && m.Status == 1 && m.SabhaID == sabhaid).OrderBy(m => m.VoteOrder).ToListAsync();
            var x = await voteAccDbContext.VoteDetails.Where(m => (m.IncomeSubtitleCode == "7301" || m.IncomeSubtitleCode == "7302" || m.IncomeSubtitleCode == "7303" || 
            m.IncomeSubtitleCode == "7304" || m.IncomeSubtitleCode == "7305" ||
            m.IncomeSubtitleCode == "232" || m.IncomeSubtitleCode == "233" ||
            m.IncomeSubtitleCode == "234" || m.IncomeSubtitleCode == "236"
            ) && m.Status == 1 && m.SabhaID == sabhaid).OrderBy(m => m.VoteOrder).ToListAsync();
            return x;
        }

        //PrepayableAccounts
        public async Task<IEnumerable<VoteDetail>> GetPrepayableLeggerAccountForSabhaId(int sabhaid)
        {
            var x = await voteAccDbContext.VoteDetails.Where(m => (m.IncomeSubtitleCode == "3301" || m.IncomeSubtitleCode == "3302" || m.IncomeSubtitleCode == "3303" || m.IncomeSubtitleCode == "3304" || m.IncomeSubtitleCode == "3305" || m.IncomeSubtitleCode == "3306"
            || m.IncomeSubtitleCode == "171" || m.IncomeSubtitleCode == "172") && m.Status == 1 && m.SabhaID == sabhaid).OrderBy(m => m.VoteOrder).ToListAsync();
            return x;
        }

        //FixedDepositAccounts
        public async Task<IEnumerable<VoteDetail>> GetFixedDepositLeggerAccountForSabhaId(int sabhaid)
        {
            var x = await voteAccDbContext.VoteDetails.Where(m => (m.IncomeSubtitleCode == "3501" || m.IncomeSubtitleCode == "181") && m.Status == 1 && m.SabhaID == sabhaid).OrderBy(m => m.VoteOrder).ToListAsync();
            return x;
        }

        //ReceivableExchangeAccounts
        public async Task<IEnumerable<VoteDetail>> GetReceivableExchangeLeggerAccountForSabhaId(int sabhaid)
        {
            var x = await voteAccDbContext.VoteDetails.Where(m => (m.IncomeSubtitleCode == "3253" || m.IncomeSubtitleCode == "3254" || m.IncomeSubtitleCode == "3255" || m.IncomeSubtitleCode == "3259" || 
            m.IncomeSubtitleCode == "151" || m.IncomeSubtitleCode == "152" || m.IncomeSubtitleCode == "153" ||
            m.IncomeSubtitleCode == "162" || m.IncomeSubtitleCode == "163" || m.IncomeSubtitleCode == "164" || m.IncomeSubtitleCode == "165" ||
            m.IncomeSubtitleCode == "166" || m.IncomeSubtitleCode == "167" || m.IncomeSubtitleCode == "169" ) && m.Status == 1 && m.SabhaID == sabhaid).OrderBy(m => m.VoteOrder).ToListAsync();
            return x;

            //var codes = Enumerable.Range(3202, 3212 - 3202 + 1).Select(c => c.ToString()).ToArray();

            //return await voteAccDbContext.VoteDetails
            //    .Where(m => codes.Contains(m.IncomeSubtitleCode) && m.Status == 1 && m.SabhaID == sabhaid)
            //    .OrderBy(m => m.VoteOrder)
            //    .ToListAsync();

        }

        //ReceivableNonExchangeAccounts
        public async Task<IEnumerable<VoteDetail>> GetReceivableNonExchangeLeggerAccountForSabhaId(int sabhaid)
        {
            var x = await voteAccDbContext.VoteDetails.Where(m => (m.IncomeSubtitleCode == "3202" || m.IncomeSubtitleCode == "3203" || m.IncomeSubtitleCode == "3204" || m.IncomeSubtitleCode == "3205" || m.IncomeSubtitleCode == "3206" || m.IncomeSubtitleCode == "3207" || m.IncomeSubtitleCode == "3208" || m.IncomeSubtitleCode == "3209" || m.IncomeSubtitleCode == "3210" || m.IncomeSubtitleCode == "3211" || m.IncomeSubtitleCode == "3212") && m.Status == 1 && m.SabhaID == sabhaid).OrderBy(m => m.VoteOrder).ToListAsync();
            return x;

        }

        //SingleOpeningBalanceAccounts
        public async Task<IEnumerable<VoteDetail>> GetSingleOpeningBalanceLedgerAccount(int sabhaid)
        {

            List<string> codes = new List<string>
                {
                    "3111", "3112", "3113", "3114", "3115",
                    "3131",
                    "3213", "3214", "3215",
                    "2201", "2202", "2203", "2204", "2249", "3101",
                    "3552", "3560",
                    "3601", "3602", "3603", "3604", "3605", "3606", "3607", "3608", "3609",
                    "3610", "3611", "3612", "3613", "3614",
                    "7501", "7502", "7503", "7504", "7505", "7506",
                    "6204",
                    "235", "242", 
                    "212", "213", "214", "215", "216", "217", "218", "219", "221"
                };

            return await voteAccDbContext.VoteDetails.Where(m => codes.Contains( m.IncomeSubtitleCode) && m.Status == 1 && m.SabhaID == sabhaid).OrderBy(m => m.VoteOrder).ToListAsync();
           
        }

        //FixAssestAccounts
        public async Task<IEnumerable<VoteDetail>> GetFixAssestsForSabhaId(int sabhaid)
        {
            var x = await voteAccDbContext.VoteDetails.Where(m => (m.IncomeSubtitleCode == "2101" || m.IncomeSubtitleCode == "2102" || m.IncomeSubtitleCode == "2103" || m.IncomeSubtitleCode == "2104" || m.IncomeSubtitleCode == "2105" || m.IncomeSubtitleCode == "2106" || m.IncomeSubtitleCode == "2107" || m.IncomeSubtitleCode == "2108" || m.IncomeSubtitleCode == "2109" || m.IncomeSubtitleCode == "2110" || m.IncomeSubtitleCode == "2111" || m.IncomeSubtitleCode == "2112" || m.IncomeSubtitleCode == "2113" || 
            m.IncomeSubtitleCode == "2114"
            || m.IncomeSubtitleCode == "111"
            || m.IncomeSubtitleCode == "112"
            || m.IncomeSubtitleCode == "113"
            || m.IncomeSubtitleCode == "114"
            || m.IncomeSubtitleCode == "115"
            ) && m.Status == 1 && m.SabhaID == sabhaid).OrderBy(m => m.VoteOrder).ToListAsync();
            return x;
        }

        //FixAssest2Accounts
        public async Task<IEnumerable<VoteDetail>> GetFixAssests2ForSabhaId(int sabhaid)
        {
            var x = await voteAccDbContext.VoteDetails.Where(m => (m.IncomeSubtitleCode == "6201" || m.IncomeSubtitleCode == "6202" || m.IncomeSubtitleCode == "6203") && m.Status == 1 && m.SabhaID == sabhaid).OrderBy(m => m.VoteOrder).ToListAsync();
            return x;
        }

        //............................//

        //AccountTransferAccounts
        public async Task<IEnumerable<VoteDetail>> GetAllAccountTransferLedgerAccountsForSabhaId(int sabhaid)
        {
            var x = await voteAccDbContext.VoteDetails.Where(m => (m.IncomeSubtitleCode == "191" || m.IncomeSubtitleCode == "192" || m.IncomeSubtitleCode == "3510" || m.IncomeSubtitleCode == "3511") && m.Status == 1 && m.SabhaID == sabhaid).OrderBy(m => m.VoteOrder).ToListAsync();
            return x;  //3510, 3511   // 191,192
        }

        //AccountTransferAccounts
        public async Task<bool> IsAccountTransferLedgerAccount(int Id)
        {
            var x = await voteAccDbContext.VoteDetails.Where(m => (m.IncomeSubtitleCode == "191" || m.IncomeSubtitleCode == "192" || m.IncomeSubtitleCode == "3510" || m.IncomeSubtitleCode == "3511") && m.Status == 1 && m.ID == Id).OrderBy(m => m.VoteOrder).AnyAsync();
            return x;  //3510, 3511   // 191,192
        }

        public async Task<int?> GetVoteClassification(int Id)
        {
            return await voteAccDbContext.VoteDetails.Where(m => m.ID == Id && m.Status == 1).Select(m => m.ClassificationID).FirstOrDefaultAsync();
        }

        //DepositAccounts
        public async Task<int?> IsDepositVote(int Id)
        {
            return await voteAccDbContext.VoteDetails.Where(m => m.ID == Id && m.Status == 1 && (m.IncomeTitleCode == "250" || m.IncomeTitleCode == "7200")).Select(m => m.IncomeSubtitleID).FirstOrDefaultAsync();
        }

        //public bool IsDeletable(int programmeId, int incometitleId, int incomesubtitileId, int projectId, int subprojectId)
        //{
        //    var resultcount = 0; 

        //    if (programmeId != null && programmeId > 0 && incometitleId != null && incometitleId > 0 && incomesubtitileId != null && incomesubtitileId > 0 && projectId != null && projectId > 0 && subprojectId != null && subprojectId > 0)
        //    {
        //        resultcount = voteAccDbContext.VoteDetails
        //        .Where(m =>
        //            m.ProgrammeID == programmeId &&
        //            m.IncomeTitleID == incometitleId &&
        //            m.IncomeSubtitleID == incomesubtitileId &&
        //            m.ProjectID == projectId &&
        //            m.SubprojectID == subprojectId &&
        //            m.Status == 1)
        //        .ToListAsync().Result.Count();
        //    }
        //    else if (programmeId != null && programmeId > 0 && incometitleId != null && incometitleId > 0 && incomesubtitileId != null && incomesubtitileId > 0 && projectId != null && projectId > 0 && subprojectId != null && subprojectId > 0)
        //    {
        //        resultcount = voteAccDbContext.VoteDetails
        //        .Where(m =>
        //            m.ProgrammeID == programmeId &&
        //            m.IncomeTitleID == incometitleId &&
        //            m.IncomeSubtitleID == incomesubtitileId &&
        //            m.ProjectID == projectId &&
        //            m.SubprojectID == subprojectId &&
        //            m.Status == 1)
        //        .ToListAsync().Result.Count();
        //    }
        //    else
        //    {
        //        return true;
        //    }

        //    if (resultcount>0)
        //        return false;
        //    else
        //        return true;
        //}

        //ShopRentalExpectedIncomeAccounts
        public async Task<IEnumerable<VoteDetail>> GetAllShopRentalExpectedIncomeAccountsForSabhaId(int sabhaid)
        {
            var x = await voteAccDbContext.VoteDetails.Where(m => (m.IncomeSubtitleCode == "4202" || m.IncomeSubtitleCode == "4502" || m.IncomeSubtitleCode == "4499" || m.IncomeSubtitleCode == "322" || m.IncomeSubtitleCode == "352" || m.IncomeSubtitleCode == "343" || m.IncomeSubtitleCode == "22" || m.IncomeSubtitleCode == "43" || m.IncomeSubtitleCode == "52") && m.Status == 1 && m.SabhaID == sabhaid).OrderBy(m => m.VoteOrder).ToListAsync();
            return x;
        }

        public Task<bool> HasVoteDetailExist(string code, HTokenClaim token)
        {
            return voteAccDbContext.VoteDetails.AnyAsync(m => m.Code == code && m.Status == 1 && m.SabhaID == token.sabhaId);
        }

        //PayrollAccounts
        public async Task<IEnumerable<VoteDetail>> GetPayRollAccountsForSabha(int sabhaId)
        {
            var x = await voteAccDbContext.VoteDetails.Where(m => (m.IncomeSubtitleCode == "1001" || m.IncomeSubtitleCode == "1002" || m.IncomeSubtitleCode == "1003" || m.IncomeSubtitleCode == "01-01" || m.IncomeSubtitleCode == "01-02" || m.IncomeSubtitleCode == "01-03" || m.IncomeSubtitleCode == "01-04" || m.IncomeSubtitleCode == "01-05" || m.IncomeSubtitleCode == "01-06" || m.IncomeSubtitleCode == "01-08") && m.Status == 1 && m.SabhaID == sabhaId).OrderBy(m => m.VoteOrder).ToListAsync();
            return x;
        }

        //StoreAdvanceAssetsAccounts
        public async Task<IEnumerable<VoteDetail>> GetStoreAdvanceAssetsAccountsForSabha(int sabhaId)
        {
                     var codes = new List<string>
                     {
                        "3101",
                        "3111",
                        "3112",
                        "3113",
                        "3114",
                        "3115",
                        "3131"
                    };

            return await voteAccDbContext.VoteDetails
                .Where(m => codes.Contains(m.IncomeSubtitleCode) && m.Status == 1 && m.SabhaID == sabhaId)
                .OrderBy(m => m.VoteOrder)
                .ToListAsync();
        }

        //IndustrialCreditorsDebtorFundingSources
        public async Task<IEnumerable<VoteDetail>> GetSabhaFundingSourceForIndustrialCreditorsDebtorsTypes(int sabhaId, bool isCreditor,bool isDebtor,int creditorsDebtorsType)
        {

            var codes = creditorsDebtorsType switch
            {
                1 => new List<string> { isCreditor ? "7325" : null },
                2 => new List<string> { isCreditor ? "7311" : null,},
                3 => new List<string> { isCreditor ? "7312" : null,},
                4 => new List<string> { isCreditor ? "7313" : null },
                //5 => new List<string> { isCreditor ? "7319" : null },

                6 => new List<string> { isDebtor ? "3225" : null },
                7 => new List<string> { isDebtor ? "3230" : null },
                8 => new List<string> { isDebtor ? "3226" : null },
                9 => new List<string> { isDebtor ? "3231" : null },
                _ => new List<string>()
            };

            codes = codes.Where(code => code != null).ToList();

            return await voteAccDbContext.VoteDetails
                .Where(m => codes.Contains(m.IncomeSubtitleCode) && m.Status == 1 && m.SabhaID == sabhaId)
                .OrderBy(m => m.VoteOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<VoteDetail>> GetSabhaFundingSourceForIndustrialCreditorsDebtorsTypesofOLDAccSystem(int sabhaId, bool isCreditor, bool isDebtor, int creditorsDebtorsType)
        {
            //Hve to update codes for Old Acc system LAs. 
            var codes = creditorsDebtorsType switch
            {
                11 => new List<string> { isCreditor ? "" : null },
                //11 => new List<string> { isCreditor ? "231" : null },
                //12 => new List<string> { isCreditor ? "7311" : null, },
                //13 => new List<string> { isCreditor ? "7312" : null, },
                //14 => new List<string> { isCreditor ? "7313" : null },
                //15 => new List<string> { isCreditor ? "7319" : null },

                //16 => new List<string> { isDebtor ? "168" : null },
                //17 => new List<string> { isDebtor ? "3230" : null },
                //18 => new List<string> { isDebtor ? "3226" : null },
                //19 => new List<string> { isDebtor ? "3231" : null },
                _ => new List<string>()
            };

            codes = codes.Where(code => code != null).ToList();

            return await voteAccDbContext.VoteDetails
                .Where(m => codes.Contains(m.IncomeSubtitleCode) && m.Status == 1 && m.SabhaID == sabhaId)
                .OrderBy(m => m.VoteOrder)
                .ToListAsync();
        }

        public async Task<VoteDetail> GetAssetsDepreciationAccounts(int sabhaId, int voteDetailId)
        {
            

            var vt = await voteAccDbContext.VoteDetails.Where(m => m.ID == voteDetailId && m.Status == 1 && m.SabhaID == sabhaId).FirstOrDefaultAsync();

            var subTitleCode = "";

            if (vt != null)
            {
                /*Follow the Account System TO find Ledger Account For Fix Assets*/

                if (vt.IncomeSubtitleCode.StartsWith("21"))
                {
                    subTitleCode = "74" + vt.IncomeSubtitleCode.Substring(2);
                }

                return  await voteAccDbContext.VoteDetails.Where(m => m.IncomeSubtitleCode == subTitleCode && m.ProgrammeID == vt.ProgrammeID && m.ProjectID == vt.ProjectID && m.SubprojectID == vt.SubprojectID  && m.Status == 1 && m.SabhaID == sabhaId).OrderBy(m => m.VoteOrder).FirstOrDefaultAsync();

            }
            else
            {
                throw new Exception("Vote Detail not found For Depreciate Account");
            }



        
        }


        public async Task<VoteDetail> GetAssetsDisposalAccounts(int sabhaId, int voteDetailId)
        {


            var vt = await voteAccDbContext.VoteDetails.Where(m => m.ID == voteDetailId && m.Status == 1 && m.SabhaID == sabhaId).FirstOrDefaultAsync();

            var subTitleCode = "";

            if (vt != null)
            {
                /*Follow the Account System TO find Ledger Account For Fix Assets*/

                if (vt.IncomeSubtitleCode!.StartsWith("21"))
                {
                    subTitleCode = "36" + vt.IncomeSubtitleCode.Substring(2);
                }

                return await voteAccDbContext.VoteDetails.Where(m => m.IncomeSubtitleCode == subTitleCode && m.ProgrammeID == vt.ProgrammeID && m.ProjectID == vt.ProjectID && m.SubprojectID == vt.SubprojectID && m.Status == 1 && m.SabhaID == sabhaId).OrderBy(m => m.VoteOrder).FirstOrDefaultAsync();

            }
            else
            {
                throw new Exception("Vote Detail not found For Depreciate Account");
            }
        }


        public async Task<VoteDetail> GetAssetsGrantRealizationAccounts(int sabhaId, int voteDetailId)
        {


            var vt = await voteAccDbContext.VoteDetails.Where(m => m.ID == voteDetailId && m.Status == 1 && m.SabhaID == sabhaId).FirstOrDefaultAsync();

            var subTitleCode = "";


            if (vt != null)
            {
                /*Follow the Account System TO find Ledger Account For Fix Assets*/

                if (vt.IncomeSubtitleCode.StartsWith("62"))
                {
                    subTitleCode = "52" + vt.IncomeSubtitleCode.Substring(2);

                    if (vt.IncomeSubtitleCode.Equals("6204"))
                    {
                        subTitleCode = "5299";
                    }
                }

                return await voteAccDbContext.VoteDetails.Where(m => m.IncomeSubtitleCode == subTitleCode && m.ProgrammeID == vt.ProgrammeID && m.ProjectID == vt.ProjectID && m.SubprojectID == vt.SubprojectID  && m.Status == 1 && m.SabhaID == sabhaId).OrderBy(m => m.VoteOrder).FirstOrDefaultAsync();

            }
            else
            {
                throw new Exception("Vote Detail not found For Depreciate Account");
            }
        }

        //AssetsSaleAccounts
        public async Task<VoteDetail> GetAssetsSaleAccounts(int sabhaId)
        {
            return await voteAccDbContext.VoteDetails.Where(m => m.IncomeSubtitleCode == "7610"  && m.Status == 1 && m.SabhaID == sabhaId).OrderBy(m => m.VoteOrder).FirstOrDefaultAsync();
        }

        public async Task<VoteDetail> GetAssetsProfitAccounts(int sabhaId, int voteDetailId)
        {
            var vt = await voteAccDbContext.VoteDetails.Where(m => m.ID == voteDetailId && m.Status == 1 && m.SabhaID == sabhaId).FirstOrDefaultAsync();

            var subTitleCode = "";


            if (vt != null)
            {
                /*Follow the Account System TO find Ledger Account For Fix Assets*/

                if (vt.IncomeSubtitleCode.StartsWith("360"))
                {
                    subTitleCode = "185" + vt.IncomeSubtitleCode.Substring(1);


                }

                return await voteAccDbContext.VoteDetails.Where(m => m.IncomeSubtitleCode == subTitleCode && m.ProgrammeID == vt.ProgrammeID && m.ProjectID == vt.ProjectID && m.SubprojectID == vt.SubprojectID && m.Status == 1 && m.SabhaID == sabhaId).OrderBy(m => m.VoteOrder).FirstOrDefaultAsync();

            }
            else
            {
                throw new Exception("Vote Detail not found For Depreciate Account");
            }
        }

        public async Task<VoteDetail> GetAssetsLossAccounts(int sabhaId, int voteDetailId)
        {
            var vt = await voteAccDbContext.VoteDetails.Where(m => m.ID == voteDetailId && m.Status == 1 && m.SabhaID == sabhaId).FirstOrDefaultAsync();

            var subTitleCode = "";


            if (vt != null)
            {
                /*Follow the Account System TO find Ledger Account For Fix Assets*/

                if (vt.IncomeSubtitleCode.StartsWith("360"))
                {
                    subTitleCode = "185" + vt.IncomeSubtitleCode.Substring(1);


                }

                return await voteAccDbContext.VoteDetails.Where(m => m.IncomeSubtitleCode == subTitleCode && m.ProgrammeID == vt.ProgrammeID && m.ProjectID == vt.ProjectID && m.SubprojectID == vt.SubprojectID && m.Status == 1 && m.SabhaID == sabhaId).OrderBy(m => m.VoteOrder).FirstOrDefaultAsync();

            }
            else
            {
                throw new Exception("Vote Detail not found For Depreciate Account");
            }

        }

        public async Task<IEnumerable<VoteDetail>> getVoteDetalByFilterValues(int? programmeId, int? classificationId, int? mainLedgerAccId, int? ledgerCategoryId, int? ledgerAccountId,int? subLedgerId, int? projectId, int? subProject, int SabhaId)
        {
            return await voteAccDbContext.VoteDetails
                 .Where(v => programmeId.HasValue ? v.ProgrammeID == programmeId : true)
                 .Where(v => classificationId.HasValue ? v.ClassificationID == classificationId : true)
                 .Where(v => mainLedgerAccId.HasValue ? v.MainLedgerAccountID == mainLedgerAccId : true)
                 .Where(v => ledgerCategoryId.HasValue ? v.IncomeTitleID == ledgerCategoryId : true)
                 .Where(v => ledgerAccountId.HasValue ? v.IncomeSubtitleID == ledgerAccountId : true)
                 .Where(v => subLedgerId.HasValue ? v.SubLedgerId == subLedgerId : true)
                 .Where(v => projectId.HasValue ? v.ProjectID == projectId : true)
                 .Where(v => subProject.HasValue ? v.SubprojectID == subProject : true)
                 .Where(v => v.SabhaID == SabhaId)
                 .ToListAsync();
        }

        public async Task<IEnumerable<VoteDetail>> getVoteDetailsByFilterValuesWithBalanceForComparativeFigure(int? programmeId, int? classificationId, int? mainLedgerAccId, int? ledgerCategoryId, int? ledgerAccountId, int? subLedgerId, int? projectId, int? subProject, int SabhaId, int year, int balanacestatus)
        {
            return await voteAccDbContext.VoteDetails
                  .Where(v => programmeId.HasValue ? v.ProgrammeID == programmeId : true)
                  .Where(v => classificationId.HasValue ? v.ClassificationID == classificationId : true)
                  .Where(v => mainLedgerAccId.HasValue ? v.MainLedgerAccountID == mainLedgerAccId : true)
                  .Where(v => ledgerCategoryId.HasValue ? v.IncomeTitleID == ledgerCategoryId : true)
                  .Where(v => ledgerAccountId.HasValue ? v.IncomeSubtitleID == ledgerAccountId : true)
                  .Where(v => subLedgerId.HasValue ? v.SubLedgerId == subLedgerId : true)
                  .Where(v => projectId.HasValue ? v.ProjectID == projectId : true)
                  .Where(v => subProject.HasValue ? v.SubprojectID == subProject : true)
                  .Include(v=>v.VoteBalances!.Where(b=>b.Year== (year-1) && b.Status == VoteBalanceStatus.Expired))
                  .Where(v => v.SabhaID == SabhaId)
      .ToListAsync();
        }

        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
    }
}