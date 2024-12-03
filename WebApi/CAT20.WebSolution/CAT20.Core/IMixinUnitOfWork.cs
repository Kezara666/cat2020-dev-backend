using CAT20.Core.Repositories.AssessmentTax;
using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.FinalAccount;
using CAT20.Core.Repositories.Mixin;
using CAT20.Core.Repositories.ShopRental;
using CAT20.Core.Repositories.Vote;
using CAT20.Core.Repositories.WaterBilling;
using System.Data;

namespace CAT20.Core
{
    public interface IMixinUnitOfWork : IDisposable
    {
        IVoteAssignmentRepository VoteAssignments { get; }
        IVoteAssignmentDetailsRepository VoteAssignmentDetails { get; }
        ICustomVoteSubLevel1Repository CustomVoteSubLevel1s { get; }
        ICustomVoteSubLevel2Repository CustomVoteSubLevel2s { get; }
        IMixinOrderRepository MixinOrders { get; }
        IMixinOrderLineRepository MixinOrderLines { get; }
        IMixinCancelOrderRepository MixinCancelOrders { get; }
        IBankingRepository Bankings { get; }
        IMixinOrderLineLogRepository MixOrderLogs { get; }
        IMixinOrderLineLogRepository MixOrderLineLogs { get; }


        //linking repository 
        IOfficeRepository Offices { get; }
        IDocumentSequenceNumberRepository DocumentSequenceNumbers { get; }
        ISessionRepository Sessions { get; }

        IBusinessTaxRepository BusinessTaxes { get; }

        IAssessmentBalanceRepository AssessmentBalances { get; }
        IAssessmentRatesRepository AssessmentRates { get; }

        IAssessmentTransactionRepository AssessmentTransactions { get; }


        IWaterConnectionRepository WaterConnections { get; }
        IWaterConnectionBalanceRepository Balances { get; }

        IAccountDetailRepository AccountDetails { get; }
        ICashBookRepository CashBook { get; }

        IVoteDetailRepository VoteDetails { get; }
        IVoteBalanceRepository VoteBalances { get; }
        IVoteBalanceLogRepository VoteBalanceLogs { get; }
        IVoteBalanceActionLogRepository VoteBalanceActionLogs { get; }
        IVoteLedgerBookRepository VoteLedgerBook { get; }

        ISpecialLedgerAccountsRepository SpecialLedgerAccounts { get; }

        IDepositRepository Deposits { get; }


        //Task<int> CommitAsync();

        Task<int> CommitAsync();

        //IDbTransaction BeginTransaction();


        //--------------[Start - shop renral balance repository]----------------------------------
        //Note : modified : 2024/04/09
        IShopRentalBalanceRepository ShopRentalBalances { get; }

        IProcessConfigurationSettingAssignRepository ProcessConfigurationSettingAssign { get; }
        //--------------[End - shop renral balance repository]------------------------------------

        IDbTransaction BeginTransaction();
    }
}
