using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.FinalAccount;
using CAT20.Core.Repositories.Mixin;
using CAT20.Core.Repositories.Vote;
using CAT20.Core.Repositories.WaterBilling;
using System.Data;

namespace CAT20.Core
{
    public interface IWaterBillingUnitOfWork : IDisposable
    {
        IWaterProjectRepository WaterProjects { get; }
        IWaterTariffRepository WaterTariffs { get; }
        INonMeterFixChargeRepository NonMeterFixCharges { get; }
        IWaterProjectNatureRepository WaterProjectNatures { get; }
        IWaterProjectMainRoadRepository WaterProjectMainRoads { get; }
        IWaterProjectSubRoadRepository WaterProjectSubRoads { get; }

        IWaterProjectGnDivisionRepository WaterProjectGnDivisions { get; }
        IMeterReaderAssignRepository MeterReaderAssigns { get; }
        IMeterConnectInfoRepository MeterConnectInfos { get; }
        INumberSequenceRepository NumberSequences { get; }

        IVoteAssignRepository VoteAssigns { get; }
        IWaterBillPaymentCategoryRepository PaymentCategories { get; }

        IApplicationForConnectionRepository ApplicationForConnections { get; }
        IApplicationForConnectionDocumentsRepository ApplicationForConnectionDocuments { get; }

        IWaterConnectionRepository WaterConnections { get; }

        IWaterBillDocumentRepository WaterBillDocuments { get; }
        IWaterConnectionNatureLogRepository WaterConnectionNatureLogs { get; }

        IWaterConnectionStatusLogRepository WaterConnectionStatusLogs { get; }

        IOpeningBalanceInformationRepository OpeningBalanceInformations { get; }

        IWaterConnectionAuditLogRepository WaterConnectionAuditLogs { get; }

        IWaterConnectionBalanceRepository Balances { get; }
        IWaterConnectionBalanceHistoryRepository BalanceHistory { get; }

        IWaterMonthEndReportRepository WaterMonthEndReport { get; }


        //linking repository

        IGnDivisionsRepository GnDivisions { get; }
        IPartnerRepository Partners { get; }

        ISessionRepository Sessions { get; }

        IMixinOrderRepository MixinOrders { get; }

        IVoteBalanceRepository VoteBalances { get; }
        IVoteBalanceLogRepository VoteBalanceLogs { get; }
        IVoteBalanceActionLogRepository VoteBalanceActionLogs { get; }

        IInternalJournalTransfersRepository InternalJournalTransfers { get; }

        ISpecialLedgerAccountsRepository SpecialLedgerAccounts { get; }


        Task<int> CommitAsync();

        IDbTransaction BeginTransaction();




    }
}
