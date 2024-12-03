using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Vote
{
    public interface IVoteDetailService
    {
        Task<IEnumerable<VoteDetail>> GetAllVoteDetails();
        Task<VoteDetail> GetVoteDetailById(int id);
        Task<(bool,string?,VoteDetail)> CreateVoteDetail(VoteDetail newVoteDetail,HTokenClaim token);
        Task UpdateVoteDetail(VoteDetail voteDetailToBeUpdated, VoteDetail voteDetail);
        Task DeleteVoteDetail(VoteDetail voteDetail);

        Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailBySabhaId(int SabhaId);
        Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailByProgrammeId(int ProgrammeId);
        Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailByIncomeSubTitleId(int IncomeSubTitleId);
        Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailByProgrammeIdandSabhaId(int ProgrammeId, int SabhaId);
        Task<IEnumerable<VoteDetail>> GetAllVoteDetailsForSabhaId(int SabhaId);
        Task<IEnumerable<VoteDetail>> GetAllVoteDetailsProgammesByClassificationId(int ClassificationId);
        Task<IEnumerable<VoteDetail>> GetAllVoteDetailsByProgrammeIdClassificationIdMainLedgerAccIdLedgerCategoryIdLedgerAccountId(int ProgrammeId, int ClassificationId, int MainLedgerAccId, int LedgerCategoryId, int LedgerAccountId, int SabhaId);
        Task<IEnumerable<VoteDetail>> GetAllLedgerAccountsForDepositSubCategoryId(int depositSubCategoryId, int sabhaid);
        Task<IEnumerable<VoteDetail>> GetAllImprestLedgerAccountsForSabhaId(int sabhaid);


        //new route final account
        Task<IEnumerable<VoteDetail>> GetLAbankLoanLeggerAccountForSabhaId(int sabhaid);

        Task<IEnumerable<VoteDetail>> GetOtherAccountsOpeningBalancessLeggerAccountForSabhaId(int sabhaid);

        Task<IEnumerable<VoteDetail>> GetIndustrialCreditorsLeggerAccountForSabhaId(int sabhaid);

        Task<IEnumerable<VoteDetail>> GetFixAssestsForSabhaId(int sabhaid);

        Task<IEnumerable<VoteDetail>> GetFixAssests2ForSabhaId(int sabhaid);

        Task<IEnumerable<VoteDetail>> GetIndustrialDebitorsLeggerAccountForSabhaId(int sabhaid);

        Task<IEnumerable<VoteDetail>> GetStoresAdvanceAccountsLeggerAccountForSabhaId(int sabhaid);

        Task<IEnumerable<VoteDetail>> GetPrepayableLeggerAccountForSabhaId(int sabhaid);

        Task<IEnumerable<VoteDetail>> GetFixedDepositLeggerAccountForSabhaId(int sabhaid);
        Task<IEnumerable<VoteDetail>> GetReceivableExchangeLeggerAccountForSabhaId(int sabhaid);

        Task<IEnumerable<VoteDetail>> GetReceivableNonExchangeLeggerAccountForSabhaId(int sabhaid);




        Task<IEnumerable<VoteDetail>> GetAllAccountTransferLedgerAccountsForSabhaId(int sabhaid);

        Task<IEnumerable<VoteDetail>> GetAllShopRentalExpectedIncomeAccountsForSabhaId(int SabhaId);
        Task<IEnumerable<VoteDetail>> GetPayRollAccountsForSabha(int SabhaId);
        Task<IEnumerable<VoteDetail>> GetStoreAdvanceAssetsAccountsForSabha(int SabhaId);

        Task<IEnumerable<VoteDetail>> GetSabhaFundingSourceForIndustrialCreditorsDebtorsTypes(int sabhaId, bool isCreditor, bool isDebtor, int creditorsDebtorsType);

        Task<IEnumerable<VoteDetail>> GetSabhaFundingSourceForIndustrialCreditorsDebtorsTypesofOLDAccSystem(int sabhaId, bool isCreditor, bool isDebtor, int creditorsDebtorsType);

        Task<IEnumerable<VoteDetail>> GetSingleOpeningBalanceLedgerAccount(int sabhaid);

        Task<IEnumerable<VoteDetail>> getVoteDetalByFilterValues(int? programmeId, int? classificationId, int? mainLedgerAccId, int? ledgerCategoryId, int? ledgerAccountId, int? subLedgerId, int? projectId, int? subProject, int SabhaId);
        Task<IEnumerable<VoteDetail>> getVoteDetailsByFilterValuesWithBalanceForComparativeFigure(int? programmeId, int? classificationId, int? mainLedgerAccId, int? ledgerCategoryId, int? ledgerAccountId, int? subLedgerId, int? projectId, int? subProject, int SabhaId,HTokenClaim token);


    }
}

