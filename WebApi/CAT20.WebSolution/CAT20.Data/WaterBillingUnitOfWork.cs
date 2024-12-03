using CAT20.Core;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.FinalAccount;
using CAT20.Core.Repositories.Mixin;
using CAT20.Core.Repositories.Vote;
using CAT20.Core.Repositories.WaterBilling;
using CAT20.Data.Repositories.Control;
using CAT20.Data.Repositories.FinalAccount;
using CAT20.Data.Repositories.Mixin;
using CAT20.Data.Repositories.Vote;
using CAT20.Data.Repositories.WaterBilling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace CAT20.Data
{
    public class WaterBillingUnitOfWork : IWaterBillingUnitOfWork
    {
        private readonly WaterBillingDbContext _wbcontext;
        private readonly ControlDbContext _ccontext;

        private readonly MixinDbContext _mixincontext;
        private readonly VoteAccDbContext _vtcontext;

        private WaterProjectRepository _ProjectRepository;
        private WaterTariffRepository _WaterTariffRepository;
        private NonMeterFixChargeRepository _NonMeterFixChargeRepository;
        private WaterProjectNatureRepository _NatureRepository;
        private WaterProjectMainRoadRepository _MainRoadRepository;
        private WaterProjectSubRoadRepository _SubRoadRepository;

        private WaterProjectGnDivisionRepository _WaterProjectGnDivisionRepository;

        private MeterReaderAssignRepository _MeterReaderAssignRepository;
        private MeterConnectInfoRepository _MeterConnectInfoRepository;
        private NumberSequenceRepository _NumberSequenceRepository;
        private VoteAssignRepository _VoteAssignRepository;
        private WaterBillPaymentCategoryRepository _PaymentCategoryRepository;

        private ApplicationForConnectionRepository _ApplicationForConnections;
        private ApplicationForConnectionDocumentsRepository _ApplicationForConnectionDocuments;

        private WaterConnectionRepository _WaterConnections;
        private WaterBillDocumentRepository _WaterBillDocuments;
        private WaterConnectionNatureLogRepository _WaterConnectionNatureLogs;
        private WaterConnectionStatusLogRepository _WaterConnectionStatusLogs;

        private OpeningBalanceInformationRepository _OpeningBalanceInformations;

        private WaterConnectionAuditLogRepository _WaterConnectionAuditLogRepository;

        private WaterConnectionBalanceRepository _WaterConnectionBalanceRepository;
        private WaterConnectionBalanceHistoryRepository _WaterConnectionBalanceHistoryRepository;

        private IWaterMonthEndReportRepository _waterMonthEndReportRepository;

        //linking repository

        private GnDivisionsRepository _GnDivisionsRepository;
        private PartnerRepository _PartnerRepository;
        private SessionRepository _sessionRepository;

        private MixinOrdersRepository _mixinOrderRepository;

        private VoteBalanceRepository _voteBalanceRepository;
        private VoteBalanceLogRepository _voteBalanceLogRepository;
        private VoteBalanceActionLogRepository _voteBalanceActionLogRepository;

        private InternalJournalTransfersRepository _internalJournalTransfers;
        private SpecialLedgerAccountsRepository _specialLedgerAccountsRepository;


        public WaterBillingUnitOfWork(WaterBillingDbContext wbcontext, ControlDbContext ccontext, MixinDbContext mixincontext, VoteAccDbContext vtcontext)
        {
            _wbcontext = wbcontext;
            _ccontext = ccontext;
            _mixincontext = mixincontext;
            _vtcontext = vtcontext;
        }


        public IWaterProjectRepository WaterProjects => _ProjectRepository = _ProjectRepository ?? new WaterProjectRepository(_wbcontext);
        public IWaterTariffRepository WaterTariffs => _WaterTariffRepository = _WaterTariffRepository ?? new WaterTariffRepository(_wbcontext);
        public INonMeterFixChargeRepository NonMeterFixCharges => _NonMeterFixChargeRepository = _NonMeterFixChargeRepository ?? new NonMeterFixChargeRepository(_wbcontext);
        public IWaterProjectNatureRepository WaterProjectNatures => _NatureRepository = _NatureRepository ?? new WaterProjectNatureRepository(_wbcontext);
        public IWaterProjectSubRoadRepository WaterProjectSubRoads => _SubRoadRepository = _SubRoadRepository ?? new WaterProjectSubRoadRepository(_wbcontext);
        public IWaterProjectMainRoadRepository WaterProjectMainRoads => _MainRoadRepository = _MainRoadRepository ?? new WaterProjectMainRoadRepository(_wbcontext);
        public IWaterProjectGnDivisionRepository WaterProjectGnDivisions => _WaterProjectGnDivisionRepository = _WaterProjectGnDivisionRepository ?? new WaterProjectGnDivisionRepository(_wbcontext);



        public IMeterReaderAssignRepository MeterReaderAssigns => _MeterReaderAssignRepository = _MeterReaderAssignRepository ?? new MeterReaderAssignRepository(_wbcontext);
        public IMeterConnectInfoRepository MeterConnectInfos => _MeterConnectInfoRepository = _MeterConnectInfoRepository ?? new MeterConnectInfoRepository(_wbcontext);

        public INumberSequenceRepository NumberSequences => _NumberSequenceRepository = _NumberSequenceRepository ?? new NumberSequenceRepository(_wbcontext);

        public IVoteAssignRepository VoteAssigns => _VoteAssignRepository = _VoteAssignRepository ?? new VoteAssignRepository(_wbcontext);
        public IWaterBillPaymentCategoryRepository PaymentCategories => _PaymentCategoryRepository = _PaymentCategoryRepository ?? new WaterBillPaymentCategoryRepository(_wbcontext);


        public IApplicationForConnectionRepository ApplicationForConnections => _ApplicationForConnections = _ApplicationForConnections ?? new ApplicationForConnectionRepository(_wbcontext);
        public IApplicationForConnectionDocumentsRepository ApplicationForConnectionDocuments => _ApplicationForConnectionDocuments = _ApplicationForConnectionDocuments ?? new ApplicationForConnectionDocumentsRepository(_wbcontext);
        public IWaterConnectionRepository WaterConnections => _WaterConnections = _WaterConnections ?? new WaterConnectionRepository(_wbcontext);

        public IWaterBillDocumentRepository WaterBillDocuments => _WaterBillDocuments = _WaterBillDocuments ?? new WaterBillDocumentRepository(_wbcontext);
        public IWaterConnectionNatureLogRepository WaterConnectionNatureLogs => _WaterConnectionNatureLogs = _WaterConnectionNatureLogs ?? new WaterConnectionNatureLogRepository(_wbcontext);
        public IWaterConnectionStatusLogRepository WaterConnectionStatusLogs => _WaterConnectionStatusLogs = _WaterConnectionStatusLogs ?? new WaterConnectionStatusLogRepository(_wbcontext);



        public IOpeningBalanceInformationRepository OpeningBalanceInformations => _OpeningBalanceInformations = _OpeningBalanceInformations ?? new OpeningBalanceInformationRepository(_wbcontext);
        public IWaterConnectionAuditLogRepository WaterConnectionAuditLogs => _WaterConnectionAuditLogRepository = _WaterConnectionAuditLogRepository ?? new WaterConnectionAuditLogRepository(_wbcontext);


        public IWaterConnectionBalanceRepository Balances => _WaterConnectionBalanceRepository = _WaterConnectionBalanceRepository ?? new WaterConnectionBalanceRepository(_wbcontext);

        public IWaterConnectionBalanceHistoryRepository BalanceHistory => _WaterConnectionBalanceHistoryRepository = _WaterConnectionBalanceHistoryRepository ?? new WaterConnectionBalanceHistoryRepository(_wbcontext);

        public IWaterMonthEndReportRepository WaterMonthEndReport => _waterMonthEndReportRepository = _waterMonthEndReportRepository ?? new WaterMonthEndReportRepository(_wbcontext);


        //linking repository
        public IGnDivisionsRepository GnDivisions => _GnDivisionsRepository = _GnDivisionsRepository ?? new GnDivisionsRepository(_ccontext);
        public IPartnerRepository Partners => _PartnerRepository = _PartnerRepository = _PartnerRepository ?? new PartnerRepository(_ccontext);

        public ISessionRepository Sessions => _sessionRepository = _sessionRepository ?? new SessionRepository(_ccontext);


        public IMixinOrderRepository MixinOrders => _mixinOrderRepository = _mixinOrderRepository ?? new MixinOrdersRepository(_mixincontext);


        public IVoteBalanceRepository VoteBalances => _voteBalanceRepository = _voteBalanceRepository ?? new VoteBalanceRepository(_vtcontext);
        public IVoteBalanceLogRepository VoteBalanceLogs => _voteBalanceLogRepository = _voteBalanceLogRepository ?? new VoteBalanceLogRepository(_vtcontext);
        public IVoteBalanceActionLogRepository VoteBalanceActionLogs => _voteBalanceActionLogRepository = _voteBalanceActionLogRepository ?? new VoteBalanceActionLogRepository(_vtcontext);

        public IInternalJournalTransfersRepository InternalJournalTransfers => _internalJournalTransfers = _internalJournalTransfers ?? new InternalJournalTransfersRepository(_vtcontext);
        public ISpecialLedgerAccountsRepository SpecialLedgerAccounts => _specialLedgerAccountsRepository = _specialLedgerAccountsRepository ?? new SpecialLedgerAccountsRepository(_vtcontext);
        //public async Task<int> CommitAsync()


        //public IDbTransaction BeginTransaction()
        //{
        //    var transaction = _wbcontext.Database.BeginTransaction();
        //    return transaction.GetDbTransaction();
        //}

        //public async Task<int> CommitAsync()
        //{
        //    return await _wbcontext.SaveChangesAsync() + await _ccontext.SaveChangesAsync();
        //}

        //public void Dispose()
        //{

        //    _wbcontext.Dispose();
        //    _ccontext.Dispose();
        //}


        public IDbTransaction BeginTransaction()
        {
            var transaction = _wbcontext.Database.BeginTransaction();
            return transaction.GetDbTransaction();
        }

        public async Task<int> CommitAsync()
        {
            // Use the execution strategy returned by DbContext.Database.CreateExecutionStrategy()
            var strategy = _wbcontext.Database.CreateExecutionStrategy();

            // Execute the SaveChangesAsync calls within the execution strategy
            return await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _wbcontext.Database.BeginTransaction())
                {
                    try
                    {
                        int result = await _wbcontext.SaveChangesAsync();
                        result += await _ccontext.SaveChangesAsync();
                        result += await _mixincontext.SaveChangesAsync();
                        // If all SaveChangesAsync calls were successful, commit the transaction
                        transaction.Commit();

                        return result;
                    }
                    catch (Exception ex)
                    {
                        // If an exception occurs, roll back the transaction
                        transaction.Rollback();
                        //return -1;
                        throw;
                    }
                }
            });
        }

        public void Dispose()
        {
            _ccontext.Dispose();
            _mixincontext.Dispose();
            _wbcontext.Dispose();
        }

    }
}
