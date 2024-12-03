using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.Common;
using CAT20.Core.Repositories.FinalAccount;
using CAT20.Core.Repositories.TradeTax;
using CAT20.Core.Repositories.Vote;
using CAT20.Core.Services.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.Mixin;
using System.Data;
using CAT20.Core.Repositories.AssessmentTax;
using CAT20.Core.Repositories.WaterBilling;
using CAT20.Core.Repositories.ShopRental;
using CAT20.Core.Repositories.HRM.LoanManagement;

namespace CAT20.Core
{
    public interface IVoteUnitOfWork : IDisposable
    {
        IOfficeRepository Offices { get; }
        IDocumentSequenceNumberRepository DocumentSequenceNumbers { get; }
        ISessionRepository Sessions { get; }
        IBusinessTaxRepository BusinessTaxes { get; }
        IMixinOrderRepository MixinOrders { get; }
        IMixinOrderLineRepository MixinOrderLines { get; }

        IAccountBalanceDetailRepository AccountBalanceDetails { get; }
        IAccountDetailRepository AccountDetails { get; }
        IBalancesheetBalanceRepository BalancesheetBalances { get; }
        IBalancesheetSubtitleRepository BalancesheetSubtitles { get; }
        IBalancesheetTitleRepository BalancesheetTitles { get; }
        IProjectRepository Projects { get; }
        ISubProjectRepository SubProjects { get; }
        IIncomeSubtitleRepository IncomeSubtitles { get; }
        IIncomeTitleRepository IncomeTitles { get; }
        IVoteBalanceRepository VoteBalances { get; }
        IVoteBalanceLogRepository VoteBalanceLogs { get; }
        IVoteBalanceActionLogRepository VoteBalanceActionLogs { get; }
        IVoteDetailRepository VoteDetails { get; }
        
        //15.10.2024
        IBudgetRepository Budgets { get; }
        //End

        IProgrammeRepository Programmes { get; }
        IAuditLogRepository AuditLogs { get; }
        IBusinessNatureRepository BusinessNatures { get; }
        IBusinessSubNatureRepository BusinessSubNatures { get; }
        ITaxValueRepository TaxValues { get; }
        ITradeTaxVoteAssignmentRepository TradeTaxVoteAssignments { get; }

        ICommitmentRepository Commitment { get; }
        ICommitmentLineRepository CommitmentLine { get; }
        ICommitmentLogRepository CommitmentLog { get; }
        ICommitmentActionLogRepository CommitmentActionLog { get; }

        IVoucherRepository Voucher { get; }
        IVoucherLineRepository VoucherLine { get; }
        IVoucherLogRepository VoucherLog { get; }
        IVoucherActionLogRepository VoucherActionLog { get; }
        IVoucherCrossOrderRepository VoucherCrossOrder { get; }
        IVoucherDocumentRepository VoucherDocument { get; }
        IVoucherInvoiceRepository VoucherInvoice { get; }

        IVoucherChequeRepository VoucherCheque { get; }

        ICommitmentLineVoteRepository CommitmentLineVote { get; }
        IAccountBalanceDetailRepository AccountBalanceDetail { get; }
        IDepositSubInfoRepository DepositSubInfo { get; }

        IFinalAccountSequenceNumberRepository FinalAccountSequenceNumber { get; }


        IVoteJournalAdjustmentRepository VoteJournalAdjustments { get; }

        ISupplementaryRepository Supplementary { get; }
        ICutProvisionRepository CutProvision { get; }
        IFR66TransferRepository FR66Transfer { get; }

        IAccountTransferRepository AccountTransfer { get; }
        IAccountTransferRefundingRepository AccountTransferRefunding { get; }



        IVoucherVoteAllocationBalanceRepository VoucherVoteAllocationBalance { get; }


        IVoteLedgerBookRepository VoteLedgerBook { get; }
        IVoteLedgerBookDailyBalanceRepository VoteLedgerBookDailyBalance { get; }

        ICashBookRepository CashBook { get; }
        ICashBookDailyBalanceRepository CashBookDailyBalance { get; }

        IDepositRepository Deposits { get; }
        ISubImprestRepository SubImprests { get; }
        ISubImprestSettlementRepository SubImprestSettlements { get; }


        IInternalJournalTransfersRepository InternalJournalTransfers { get; }

        ISpecialLedgerAccountsRepository SpecialLedgerAccounts { get; }

        ISabhaFundSourceRepository SabhaFundSources { get; }

        IIndustrialCreditorsDebtorsTypesRepository IndustrialCreditorsDebtorsTypes { get; }

        //.....................................//
        ICreditorsBillingRepository CreditorsBilling { get; }

        IFixedDepositRepository FixedDeposits { get; }

        IIndustrialCreditorsRepository IndustrialCreditors { get; }
        IIndustrialDebtorsRepository IndustrialDebtors { get; }

        ILABAnkLoanRepository LABankLoans { get; }
        IPrepaidPaymentRepository PrepaidPayments { get; }

        IReceivableExchangeNonExchangeRepository ReceivableExchangeNonExchanges { get; }

        IStoreCreditorsRepository StoreCreditors { get; }

        ISingleOpenBalanceRepository SingleOpenBalances { get; }

        IFixedAssetsRepository FixedAssets { get; }

        IDepreciationRatesRepository DepreciationRates { get; }

        ICustomVoteEntryRepository CustomVoteEntries { get; }
        ICustomVoteBalanceRepository CustomVoteBalances { get; }
        ICustomVoteBalanceLogRepository CustomVoteBalanceLogs { get; }
        ICustomVoteBalanceActionLogRepository CustomVoteBalanceActionLogs { get; }

        //....................................//
        /*linking module mixing vote*/

        IVoteAssignmentRepository VoteAssignments { get; }
        IVoteAssignmentDetailsRepository CustomVoteDetails { get; }
        //IVoteAssignmentDetailsRepository VoteAssignmentDetails { get; }

        IAssessmentRepository Assessments { get; }
        IAssessmentVoteAssignRepository AssmtVoteAssigns { get; }

        IWaterConnectionRepository WaterConnections { get; }

        IVoteAssignRepository WbVoteAssign { get; }

        IShopRepository Shops { get; }

        IClassificationRepository Classifications { get; }


        IAdvanceBRepository AdvanceBs { get; }
        IBudgetRepository Budget { get; }

        Task<int> CommitAsync();

        IDbTransaction BeginTransaction();

    }
}
