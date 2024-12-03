using CAT20.Core;
using CAT20.Core.Models.HRM;
using CAT20.Core.Repositories.Common;
using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.FinalAccount;
using CAT20.Core.Repositories.HRM;
using CAT20.Core.Repositories.HRM.LoanManagement;
using CAT20.Core.Repositories.HRM.PersonalFile;
using CAT20.Core.Repositories.Mixin;
using CAT20.Core.Repositories.Vote;
using CAT20.Core.Services.HRM;
using CAT20.Data.Repositories.Common;
using CAT20.Data.Repositories.Control;
using CAT20.Data.Repositories.FinalAccount;
using CAT20.Data.Repositories.HRM;
using CAT20.Data.Repositories.HRM.LoanManagement;
using CAT20.Data.Repositories.HRM.PersonalFile;
using CAT20.Data.Repositories.Mixin;
using CAT20.Data.Repositories.Vote;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CAT20.Data
{
    public class HRMUnitOfWork : IHRMUnitOfWork
    {
        private readonly HRMDbContext _hrmcontext;
        private readonly ControlDbContext _ccontext;
        private readonly MixinDbContext _mixincontext;
        private readonly VoteAccDbContext _vtcontext;

        // PersonalFile
        private EmployeeRepository _EmployeeRepository;
        private EmployeeTypeDataRepository _EmployeeTypeDataRepository;
        private CarderStatusDataRepository _CarderStatusDataRepository;
        private SalaryStructureDataRepository _SalaryStructureDataRepository;
        private ServiceTypeDataRepository _ServiceTypeDataRepository;
        private JobTitleDataRepository _JobTitleDataRepository;
        private ClassLevelDataRepository _ClassLevelDataRepository;
        private GradeLevelDataRepository _GradeLevelDataRepository;
        private AgraharaCategoryDataRepository _AgraharaCategoryDataRepository;
        private AppointmentTypeDataRepository _AppointmentTypeDataRepository;
        private FundingSourceDataRepository _FundingSourceDataRepository;
        private SupportingDocTypeDataRepository _SupportingDocTypeDataRepository;

        private HRMSequenceNumberRepository _HRMSequenceNumberRepository;

        // Loan Management
        private AdvanceBRepository _LoanRepository;
        private AdvanceBTypeDataRepository _LoanTypeDataRepository;

        /*linking repository*/

        private SessionRepository _sessionRepository;

        private VoteAssignmentRepository _voteAssignmentRepository;
        private VoteAssignmentDetailsRepository _voteAssignmentDetailsRepository;

        private VoteDetailRepository _voteDetailRepository;
        private VoteBalanceRepository _voteBalanceRepository;
        private VoteBalanceLogRepository _voteBalanceLogRepository;
        private VoteBalanceActionLogRepository _voteBalanceActionLogRepository;

        private InternalJournalTransfersRepository _internalJournalTransfers;
        private SpecialLedgerAccountsRepository _specialLedgerAccountsRepository;
        private AdvancedBtypeLedgerMappingRepository _advancedBtypeLedgerMappingRepository;
        public HRMUnitOfWork(HRMDbContext context,ControlDbContext ccontext,MixinDbContext mixinContext, VoteAccDbContext vtcontext)
        {
            _hrmcontext = context;
            _ccontext = ccontext;
            _mixincontext = mixinContext;
            _vtcontext = vtcontext;
        }

        // PersonalFile
        public IEmployeeRepository Employees => _EmployeeRepository = _EmployeeRepository ?? new EmployeeRepository(_hrmcontext);
        public IEmployeeTypeDataRepository EmployeeTypeDatas => _EmployeeTypeDataRepository = _EmployeeTypeDataRepository ?? new EmployeeTypeDataRepository(_hrmcontext);
        public ICarderStatusDataRepository CarderStatusDatas => _CarderStatusDataRepository = _CarderStatusDataRepository ?? new CarderStatusDataRepository(_hrmcontext);
        public ISalaryStructureDataRepository SalaryStructureDatas => _SalaryStructureDataRepository = _SalaryStructureDataRepository ?? new SalaryStructureDataRepository(_hrmcontext);
        public IServiceTypeDataRepository ServiceTypeDatas => _ServiceTypeDataRepository = _ServiceTypeDataRepository ?? new ServiceTypeDataRepository(_hrmcontext);
        public IJobTitleDataRepository JobTitleDatas => _JobTitleDataRepository = _JobTitleDataRepository ?? new JobTitleDataRepository(_hrmcontext);
        public IClassLevelDataRepository ClassLevelDatas => _ClassLevelDataRepository = _ClassLevelDataRepository ?? new ClassLevelDataRepository(_hrmcontext);
        public IGradeLevelDataRepository GradeLevelDatas => _GradeLevelDataRepository = _GradeLevelDataRepository ?? new GradeLevelDataRepository(_hrmcontext);
        public IAgraharaCategoryDataRepository AgraharaCategoryDatas => _AgraharaCategoryDataRepository = _AgraharaCategoryDataRepository ?? new AgraharaCategoryDataRepository(_hrmcontext);
        public IAppointmentTypeDataRepository AppointmentTypeDatas => _AppointmentTypeDataRepository = _AppointmentTypeDataRepository ?? new AppointmentTypeDataRepository(_hrmcontext);
        public IFundingSourceDataRepository FundingSourceDatas => _FundingSourceDataRepository = _FundingSourceDataRepository ?? new FundingSourceDataRepository(_hrmcontext);
        public ISupportingDocTypeDataRepository SupportingDocTypeDatas => _SupportingDocTypeDataRepository = _SupportingDocTypeDataRepository ?? new SupportingDocTypeDataRepository(_hrmcontext);

        public IHRMSequenceNumberRepository HRMSequenceNumbers => _HRMSequenceNumberRepository = _HRMSequenceNumberRepository ?? new HRMSequenceNumberRepository(_hrmcontext);

        // Loan Management
        public IAdvanceBRepository AdvanceBs => _LoanRepository = _LoanRepository ?? new AdvanceBRepository(_hrmcontext);
        public IAdvanceBTypeDataRepository AdvanceBTypeDatas => _LoanTypeDataRepository = _LoanTypeDataRepository ?? new AdvanceBTypeDataRepository(_hrmcontext);


            /*linking repository*/
        public ISessionRepository Sessions => _sessionRepository = _sessionRepository ?? new SessionRepository(_ccontext);



        public IVoteAssignmentRepository VoteAssignments => _voteAssignmentRepository = _voteAssignmentRepository ?? new VoteAssignmentRepository(_mixincontext);
        public IVoteAssignmentDetailsRepository VoteAssignmentDetails => _voteAssignmentDetailsRepository = _voteAssignmentDetailsRepository ?? new VoteAssignmentDetailsRepository(_mixincontext);

        public IVoteDetailRepository VoteDetails => _voteDetailRepository = _voteDetailRepository ?? new VoteDetailRepository(_vtcontext);
        public IVoteBalanceRepository VoteBalances => _voteBalanceRepository = _voteBalanceRepository ?? new VoteBalanceRepository(_vtcontext);
        public IVoteBalanceLogRepository VoteBalanceLogs => _voteBalanceLogRepository = _voteBalanceLogRepository ?? new VoteBalanceLogRepository(_vtcontext);
        public IVoteBalanceActionLogRepository VoteBalanceActionLogs => _voteBalanceActionLogRepository = _voteBalanceActionLogRepository ?? new VoteBalanceActionLogRepository(_vtcontext);

        public IInternalJournalTransfersRepository InternalJournalTransfers => _internalJournalTransfers = _internalJournalTransfers ?? new InternalJournalTransfersRepository(_vtcontext);
        public ISpecialLedgerAccountsRepository SpecialLedgerAccounts => _specialLedgerAccountsRepository = _specialLedgerAccountsRepository ?? new SpecialLedgerAccountsRepository(_vtcontext);

        public IAdvancedBTypeLedgerMappingRepository AdvanceBTypeLedgerMapping => _advancedBtypeLedgerMappingRepository = _advancedBtypeLedgerMappingRepository ?? new AdvancedBtypeLedgerMappingRepository(_hrmcontext);

        //public async Task<int> CommitAsync()
        //{
        //    return await _hrmcontext.SaveChangesAsync();
        //}

        //public void Dispose()
        //{
        //    _hrmcontext.Dispose();
        //}

        public async Task<int> CommitAsync()
        {
            // Use the execution strategy returned by DbContext.Database.CreateExecutionStrategy()
            var strategy = _vtcontext.Database.CreateExecutionStrategy();

            // Execute the SaveChangesAsync calls within the execution strategy
            return await strategy.ExecuteAsync(async () =>
            {
                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        int result = 0;
                        result += await _hrmcontext.SaveChangesAsync();
                        result += await _ccontext.SaveChangesAsync();
                        result += await _vtcontext.SaveChangesAsync();
                        result += await _mixincontext.SaveChangesAsync();

                        transactionScope.Complete();

                        return result;
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            });
        }

        public void Dispose()
        {
            _hrmcontext.Dispose();
            _ccontext.Dispose();
            _mixincontext.Dispose();
            _vtcontext.Dispose();
        }
    }
}