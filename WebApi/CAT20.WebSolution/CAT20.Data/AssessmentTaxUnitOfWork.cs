using CAT20.Core;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Repositories.AssessmentAuditActivity;
using CAT20.Core.Repositories.AssessmentTax;
using CAT20.Core.Repositories.AssessmentTax.QuarterRepository;
using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.FinalAccount;
using CAT20.Core.Repositories.Mixin;
using CAT20.Core.Repositories.Vote;
using CAT20.Data.Repositories.AssessmentAuditActivity;
using CAT20.Data.Repositories.AssessmentTax;
using CAT20.Data.Repositories.AssessmentTax.QuarterRepository;
using CAT20.Data.Repositories.Control;
using CAT20.Data.Repositories.FinalAccount;
using CAT20.Data.Repositories.Mixin;
using CAT20.Data.Repositories.Vote;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace CAT20.Data
{
    public class AssessmentTaxUnitOfWork : IAssessmentTaxUnitOfWork
    {
        private readonly AssessmentTaxDbContext _assmtcontext;
        private readonly ControlDbContext _ccontext;
        private readonly AuditTrailDbContext _auditContext;
        private readonly VoteAccDbContext _vtcontext;

        private readonly MixinDbContext _mixincontext;


        private WardRepository _WardRepository;
        private StreetRepository _StreetRepository;
        private AssessmentPropertyTypeRepository _AssessmentPropertyTypeRepository;
        private DescriptionRepository _DescriptionRepository;

        private AssessmentRepository _AssessmentRepository;
        private AllocationRepository _AllocationRepository;
        private AssessmetTempPartnerRepository _AssessmetTempPartnerRepository;
        private AssessmentTempSubPartnerRepository _TempSubPartnerRepository;

        private AssessmentBalanceRepository _AssessmentBalanceRepository;
        private AssessmentVoteAssignRepository _AssmtVoteAssignRepository;
        private AssessmentVotePaymentTypeRepository _AssmtVoteTypeRepository;

        private AssessmentsBalancesHistoryRepository _AssessmentsBalancesHistoryRepository;
        private AssessmentRatesRepository _AssessmentRatesRepository;


        private Q1Repository _Q1Repository;
        private Q2Repository _Q2Repository;
        private Q3Repository _Q3Repository;
        private Q4Repository _Q4Repository;

        private AssessmentProcessRepository _AssessmentProcessLogRepository;

        private AssessmentTransactionRepository _AssessmentTransactionRepository;
        private AssessmentDescriptionLogRepository _AssessmentDescriptionLogs;
        private AssessmentPropertyTypeLogRepository _AssessmentPropertyTypeLogs;

        private AllocationLogRepository _AllocationLogs;

        private AssessmentJournalRepository _AssessmentJournals;
        private AssessmentAssetsChangeRepository _AssessmentAssetsChanges;

        private AssessmentAuditLogRepository _AssessmentAuditLogs;

        private NewAllocationRequestRepository _NewAllocationRequestRepository;

        private AssessmentQuarterReportRepository _AssessmentQuarterReportRepository;
        private AssessmentQuarterReportLogRepository _AssessmentQuarterReportLogRepository;


        private AssessmentBillAdjustmentRepository _AssessmentBillAdjustmentRepository;


        private NQ1Repository _NQ1Repository;
        private NQ2Repository _NQ2Repository;
        private NQ3Repository _NQ3Repository;
        private NQ4Repository _NQ4Repository;


        private AmalgamationRepository _AmalgamationRepository;
        private SubDivisionRepository _SubDivisionRepository;

        private AmalgamationSubDivisionRepository _AmalgamationSubDivisionRepository;
        private AmalgamationSubDivisionActionsRepository _AmalgamationSubDivisionActionsRepository;
        private AmalgamationAssessmentRepository _AmalgamationAssessmentRepositoryRepository;
        private AmalgamationSubDivisionDocumentsRepository _AmalgamationSubDivisionDocumentsRepository;
        private AssessmentATDRepository _AssessmentATDRepository;

        // linking repository 
        private PartnerRepository _partnerRepository;
        private AssessmentUserActivityRepository _assessmentUserActivityRepository;

        private OfficeRepository _officeRepository;
        private SessionRepository _sessionRepository;

        private MixinOrdersRepository _mixinOrderRepository;

        private VoteBalanceRepository _voteBalanceRepository;
        private VoteBalanceLogRepository _voteBalanceLogRepository;
        private VoteBalanceActionLogRepository _voteBalanceActionLogRepository;

        private InternalJournalTransfersRepository _internalJournalTransfers;
        private SpecialLedgerAccountsRepository _specialLedgerAccountsRepository;

        public AssessmentTaxUnitOfWork(ControlDbContext ccontext, AssessmentTaxDbContext context,AuditTrailDbContext aContext, MixinDbContext mixincontext, VoteAccDbContext vtcontext)
        {
            _ccontext = ccontext;
            _assmtcontext = context;
            _auditContext = aContext;
            _mixincontext = mixincontext;
            _vtcontext = vtcontext;
        }

        public IWardRepository Wards => _WardRepository = _WardRepository ?? new WardRepository(_assmtcontext);
        public IStreetRepository Streets => _StreetRepository = _StreetRepository ?? new StreetRepository(_assmtcontext);
        public IDescriptionRepository Descriptions => _DescriptionRepository = _DescriptionRepository ?? new DescriptionRepository(_assmtcontext);
        public IAssessmentPropertyTypeRepository AssessmentPropertyTypes => _AssessmentPropertyTypeRepository = _AssessmentPropertyTypeRepository ?? new AssessmentPropertyTypeRepository(_assmtcontext);
        public IAssessmentRepository Assessments => _AssessmentRepository = _AssessmentRepository ?? new AssessmentRepository(_assmtcontext);
        public IAllocationRepository Allocations => _AllocationRepository = _AllocationRepository ?? new AllocationRepository(_assmtcontext);
        public IAssessmetTempPartnerRepository TempPartners => _AssessmetTempPartnerRepository = _AssessmetTempPartnerRepository ?? new AssessmetTempPartnerRepository(_assmtcontext);
        public IAssessmentTempSubPartnerRepository TempSubPartners => _TempSubPartnerRepository = _TempSubPartnerRepository ?? new AssessmentTempSubPartnerRepository(_assmtcontext);
        public IAssessmentBalanceRepository AssessmentBalances => _AssessmentBalanceRepository = _AssessmentBalanceRepository ?? new AssessmentBalanceRepository(_assmtcontext);
        public IAssessmentVoteAssignRepository AssmtVoteAssigns => _AssmtVoteAssignRepository = _AssmtVoteAssignRepository ?? new AssessmentVoteAssignRepository(_assmtcontext);
        public IAssessmentVotePaymentTypeRepository AssmtVotePaymentTypes => _AssmtVoteTypeRepository = _AssmtVoteTypeRepository ?? new AssessmentVotePaymentTypeRepository(_assmtcontext);


        public IAssessmentsBalancesHistoryRepository AssessmentsBalancesHistories => _AssessmentsBalancesHistoryRepository = _AssessmentsBalancesHistoryRepository ?? new AssessmentsBalancesHistoryRepository(_assmtcontext);


        public IAssessmentRatesRepository AssessmentRates => _AssessmentRatesRepository = _AssessmentRatesRepository ?? new AssessmentRatesRepository(_assmtcontext);

        public IAssessmentTransactionRepository AssessmentTransactions => _AssessmentTransactionRepository = _AssessmentTransactionRepository ?? new AssessmentTransactionRepository(_assmtcontext);
        public IAssessmentDescriptionLogRepository AssessmentDescriptionLogs => _AssessmentDescriptionLogs = _AssessmentDescriptionLogs ?? new AssessmentDescriptionLogRepository(_assmtcontext);
        public IAssessmentPropertyTypeLogRepository AssessmentPropertyTypeLogs => _AssessmentPropertyTypeLogs = _AssessmentPropertyTypeLogs ?? new AssessmentPropertyTypeLogRepository(_assmtcontext);

        public IAllocationLogRepository AllocationLogs => _AllocationLogs = _AllocationLogs ?? new AllocationLogRepository(_assmtcontext);

        public IQ1Repository Q1s => _Q1Repository = _Q1Repository ?? new Q1Repository(_assmtcontext);
        public IQ2Repository Q2s => _Q2Repository = _Q2Repository ?? new Q2Repository(_assmtcontext);
        public IQ3Repository Q3s => _Q3Repository = _Q3Repository ?? new Q3Repository(_assmtcontext);
        public IQ4Repository Q4s => _Q4Repository = _Q4Repository ?? new Q4Repository(_assmtcontext);


        public IAssessmentProcessRepository AssessmentProcesses => _AssessmentProcessLogRepository = _AssessmentProcessLogRepository ?? new AssessmentProcessRepository(_assmtcontext);
        public IAssessmentJournalRepository AssessmentJournals => _AssessmentJournals = _AssessmentJournals ?? new AssessmentJournalRepository(_assmtcontext);
        public IAssessmentAssetsChangeRepository AssessmentAssetsChanges => _AssessmentAssetsChanges = _AssessmentAssetsChanges ?? new AssessmentAssetsChangeRepository(_assmtcontext);

        public IAssessmentAuditLogRepository AssessmentAuditLogs => _AssessmentAuditLogs = _AssessmentAuditLogs ?? new AssessmentAuditLogRepository(_assmtcontext);

        public INewAllocationRequestRepository NewAllocationRequests => _NewAllocationRequestRepository = _NewAllocationRequestRepository ?? new NewAllocationRequestRepository(_assmtcontext);

        public IAssessmentQuarterReportRepository AssessmentQuarterReports => _AssessmentQuarterReportRepository = _AssessmentQuarterReportRepository ?? new AssessmentQuarterReportRepository(_assmtcontext);
        public IAssessmentQuarterReportLogRepository AssessmentQuarterReportLogs => _AssessmentQuarterReportLogRepository = _AssessmentQuarterReportLogRepository ?? new AssessmentQuarterReportLogRepository(_assmtcontext);


        public IAssessmentBillAdjustmentRepository AssessmentBillAdjustments => _AssessmentBillAdjustmentRepository = _AssessmentBillAdjustmentRepository ?? new AssessmentBillAdjustmentRepository(_assmtcontext);

        public INQ1Repository NQ1s => _NQ1Repository = _NQ1Repository ?? new NQ1Repository(_assmtcontext);
        public INQ2Repository NQ2s => _NQ2Repository = _NQ2Repository ?? new NQ2Repository(_assmtcontext);
        public INQ3Repository NQ3s => _NQ3Repository = _NQ3Repository ?? new NQ3Repository(_assmtcontext);
        public INQ4Repository NQ4s => _NQ4Repository = _NQ4Repository ?? new NQ4Repository(_assmtcontext);



        public IAmalgamationRepository Amalgamations => _AmalgamationRepository = _AmalgamationRepository ?? new AmalgamationRepository(_assmtcontext);
        public ISubDivisionRepository SubDivisions => _SubDivisionRepository = _SubDivisionRepository ?? new SubDivisionRepository(_assmtcontext);

        public IAmalgamationSubDivisionRepository AmalgamationSubDivision  => _AmalgamationSubDivisionRepository = _AmalgamationSubDivisionRepository ?? new AmalgamationSubDivisionRepository(_assmtcontext);
        public IAmalgamationSubDivisionActionsRepository AmalgamationSubDivisionActions => _AmalgamationSubDivisionActionsRepository = _AmalgamationSubDivisionActionsRepository ?? new AmalgamationSubDivisionActionsRepository(_assmtcontext);
        public IAmalgamationAssessmentRepository AmalgamationAssessmentRepository => _AmalgamationAssessmentRepositoryRepository= _AmalgamationAssessmentRepositoryRepository ?? new AmalgamationAssessmentRepository(_assmtcontext);   
        public IAmalgamationSubDivisionDocumentsRepository AmalgamationSubDivisionDocuments => _AmalgamationSubDivisionDocumentsRepository = _AmalgamationSubDivisionDocumentsRepository ?? new AmalgamationSubDivisionDocumentsRepository(_assmtcontext);
        public IAssessmentATDRepository AssessmentATDs => _AssessmentATDRepository = _AssessmentATDRepository ?? new AssessmentATDRepository(_assmtcontext);


        //linking repository 

        public IPartnerRepository Partners => _partnerRepository = _partnerRepository ?? new PartnerRepository(_ccontext);

        public IAssessmentUserActivityRepository AssessmentUserActivity => _assessmentUserActivityRepository = _assessmentUserActivityRepository ?? new AssessmentUserActivityRepository(_auditContext);

        public IOfficeRepository Offices => _officeRepository = _officeRepository ?? new OfficeRepository(_ccontext);
        public ISessionRepository Sessions => _sessionRepository = _sessionRepository ?? new SessionRepository(_ccontext);

        public IMixinOrderRepository MixinOrders => _mixinOrderRepository = _mixinOrderRepository ?? new MixinOrdersRepository(_mixincontext);

        public IVoteBalanceRepository VoteBalances => _voteBalanceRepository = _voteBalanceRepository ?? new VoteBalanceRepository(_vtcontext);
        public IVoteBalanceLogRepository VoteBalanceLogs => _voteBalanceLogRepository = _voteBalanceLogRepository ?? new VoteBalanceLogRepository(_vtcontext);
        public IVoteBalanceActionLogRepository VoteBalanceActionLogs => _voteBalanceActionLogRepository = _voteBalanceActionLogRepository ?? new VoteBalanceActionLogRepository(_vtcontext);

        public IInternalJournalTransfersRepository InternalJournalTransfers => _internalJournalTransfers = _internalJournalTransfers ?? new InternalJournalTransfersRepository(_vtcontext);
        public ISpecialLedgerAccountsRepository SpecialLedgerAccounts => _specialLedgerAccountsRepository = _specialLedgerAccountsRepository ?? new SpecialLedgerAccountsRepository(_vtcontext);
        //public async Task<int> CommitAsync()
        //{
        //    return await _assmtcontext.SaveChangesAsync() +await _auditContext.SaveChangesAsync()+ await _ccontext.SaveChangesAsync();
        //}

        //private bool _disposed = false;
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!_disposed)
        //    {
        //        if (disposing)
        //        {
        //            _assmtcontext.Dispose();
        //            _ccontext.Dispose();
        //            _auditContext.Dispose();
        //        }
        //        _disposed = true;
        //    }
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //public async ValueTask DisposeAsync()
        //{
        //    Dispose();
        //    await Task.CompletedTask;
        //}



        public IDbTransaction BeginTransaction()
        {
            var transaction = _assmtcontext.Database.BeginTransaction();
            return transaction.GetDbTransaction();
        }

        public async Task<int> CommitAsync()
        {
            // Use the execution strategy returned by DbContext.Database.CreateExecutionStrategy()
            var strategy = _assmtcontext.Database.CreateExecutionStrategy();

            // Execute the SaveChangesAsync calls within the execution strategy
            return await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _assmtcontext.Database.BeginTransaction())
                {
                    try
                    {
                        int result = await _auditContext.SaveChangesAsync();
                        result += await _ccontext.SaveChangesAsync();
                        result += await _assmtcontext.SaveChangesAsync();
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

        public async Task<int> StepAsync()
        {
            // Use the execution strategy returned by DbContext.Database.CreateExecutionStrategy()
            var strategy = _assmtcontext.Database.CreateExecutionStrategy();

            // Execute the SaveChangesAsync calls within the execution strategy
            return await strategy.ExecuteAsync(async () =>
            {
               
                    try
                    {
                        int result = await _auditContext.SaveChangesAsync();
                        result += await _ccontext.SaveChangesAsync();
                        result += await _assmtcontext.SaveChangesAsync();
                        result += await _mixincontext.SaveChangesAsync();


                        return result;
                    }
                    catch (Exception ex)
                    {
                        // If an exception occurs, roll back the transaction
                        //return -1;
                        throw;
                    }
                
            });
        }

        public void Dispose()
        {
            _ccontext.Dispose();
            _auditContext.Dispose();
            _assmtcontext.Dispose();
        }

    }

    //public void Dispose()
    //{
    //    _context.Dispose();
    //}
}