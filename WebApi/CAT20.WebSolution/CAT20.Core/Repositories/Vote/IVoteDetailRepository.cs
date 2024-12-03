using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;

namespace CAT20.Core.Repositories.Vote
{
    public interface IVoteDetailRepository : IRepository<VoteDetail>
    {
        Task<bool> HasVoteDetailExist(string code,HTokenClaim token);
        Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailAsync();
        Task<VoteDetail> GetWithVoteDetailByIdAsync(int id);
        Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailByVoteDetailIdAsync(int Id);
        Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailBySabhaIdAsync(int SabhaId);
        Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailByProgrammeIdAsync(int ProgrammeId);
        Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailByIncomeSubTitleIdAsync(int IncomeSubTitleId);
        Task<IEnumerable<VoteDetail>> GetAllWithVoteDetailByProgrammeIdandSabhaIdAsync(int ProgrammeId, int SabhaId);

        //new route final account
        Task<IEnumerable<VoteDetail>> GetLAbankLoanLeggerAccountForSabhaId( int SabhaId);
        Task<IEnumerable<VoteDetail>> GetOtherAccountsOpeningBalancessLeggerAccountForSabhaId(int SabhaId);

        Task<IEnumerable<VoteDetail>> GetIndustrialCreditorsLeggerAccountForSabhaId(int SabhaId);

        Task<IEnumerable<VoteDetail>> GetIndustrialDebitorsLeggerAccountForSabhaId(int SabhaId);

        Task<IEnumerable<VoteDetail>> GetStoresAdvanceAccountsLeggerAccountForSabhaId(int SabhaId);

        Task<IEnumerable<VoteDetail>> GetPrepayableLeggerAccountForSabhaId(int SabhaId);
        Task<IEnumerable<VoteDetail>> GetFixedDepositLeggerAccountForSabhaId(int SabhaId);

        Task<IEnumerable<VoteDetail>> GetReceivableExchangeLeggerAccountForSabhaId(int SabhaId);

        Task<IEnumerable<VoteDetail>> GetReceivableNonExchangeLeggerAccountForSabhaId(int SabhaId);
        Task<IEnumerable<VoteDetail>> GetSingleOpeningBalanceLedgerAccount(int SabhaId);

        Task<IEnumerable<VoteDetail>> GetFixAssestsForSabhaId(int SabhaId);

        Task<IEnumerable<VoteDetail>> GetFixAssests2ForSabhaId(int SabhaId);



        Task<IEnumerable<VoteDetail>> GetAllVoteDetailsForSabhaIdAsync(int Id);

        Task<IEnumerable<VoteDetail>> GetAllVoteDetailsProgammesByClassificationIdAsync(int ClassificationId);
        Task<IEnumerable<VoteDetail>> GetAllVoteDetailsByProgrammeIdClassificationIdMainLedgerAccIdLedgerCategoryIdLedgerAccountId(int ProgrammeId, int ClassificationId, int MainLedgerAccId, int LedgerCategoryId, int LedgerAccountId, int SabhaId);

        Task<IEnumerable<VoteDetail>> GetAllLedgerAccountsForDepositSubCategoryId(int depositSubCategoryId, int sabhaid);
        Task<IEnumerable<VoteDetail>> GetAllImprestLedgerAccountsForSabhaId(int sabhaid);
        Task<IEnumerable<VoteDetail>> GetAllAccountTransferLedgerAccountsForSabhaId(int sabhaid);
        Task<bool> IsAccountTransferLedgerAccount(int Id);

        Task<int?> GetVoteClassification(int Id);
        Task<int?> IsDepositVote(int Id);

        Task<IEnumerable<VoteDetail>> GetAllShopRentalExpectedIncomeAccountsForSabhaId(int sabhaid);
        Task<IEnumerable<VoteDetail>> GetPayRollAccountsForSabha(int SabhaId);
        Task<IEnumerable<VoteDetail>> GetStoreAdvanceAssetsAccountsForSabha(int SabhaId);

        Task<VoteDetail> GetAssetsDepreciationAccounts(int sabhaid, int voteDetailId);
        Task<VoteDetail> GetAssetsDisposalAccounts(int sabhaid, int voteDetailId);
        Task<VoteDetail> GetAssetsGrantRealizationAccounts(int sabhaid, int voteDetailId);
        Task<VoteDetail> GetAssetsSaleAccounts(int sabhaid);
        Task<VoteDetail> GetAssetsProfitAccounts(int sabhaId, int voteDetailId);
        Task<VoteDetail> GetAssetsLossAccounts(int sabhaId, int voteDetailId);

        Task<IEnumerable<VoteDetail>> GetSabhaFundingSourceForIndustrialCreditorsDebtorsTypes(int sabhaId, bool isCreditor, bool isDebtor, int creditorsDebtorsType);
        Task<IEnumerable<VoteDetail>> GetSabhaFundingSourceForIndustrialCreditorsDebtorsTypesofOLDAccSystem(int sabhaId, bool isCreditor, bool isDebtor, int creditorsDebtorsType);

        Task<IEnumerable<VoteDetail>> getVoteDetalByFilterValues(int? programmeId, int? classificationId, int? mainLedgerAccId, int? ledgerCategoryId, int? ledgerAccountId, int? subLedgerId, int? projectId, int? subProject, int SabhaId);
        Task<IEnumerable<VoteDetail>> getVoteDetailsByFilterValuesWithBalanceForComparativeFigure(int? programmeId, int? classificationId, int? mainLedgerAccId, int? ledgerCategoryId, int? ledgerAccountId, int? subLedgerId, int? projectId, int? subProject, int SabhaId, int year, int balanacestatus);

    }
}
