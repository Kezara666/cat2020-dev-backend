using CAT20.Core.Models.Mixin;
using CAT20.Core.Repositories.Common;
using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.FinalAccount;
using CAT20.Core.Repositories.HRM;
using CAT20.Core.Repositories.HRM.LoanManagement;
using CAT20.Core.Repositories.HRM.PersonalFile;
using CAT20.Core.Repositories.Mixin;
using CAT20.Core.Repositories.Vote;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Threading.Tasks;

namespace CAT20.Core
{
    public interface IHRMUnitOfWork : IDisposable
    {
        // PersonalFile
        IEmployeeRepository Employees { get; }
        IEmployeeTypeDataRepository EmployeeTypeDatas { get; }
        ICarderStatusDataRepository CarderStatusDatas { get; }
        ISalaryStructureDataRepository SalaryStructureDatas { get; }
        IServiceTypeDataRepository ServiceTypeDatas { get; }
        IJobTitleDataRepository JobTitleDatas { get; }
        IClassLevelDataRepository ClassLevelDatas { get; }
        IGradeLevelDataRepository GradeLevelDatas { get; }
        IAgraharaCategoryDataRepository AgraharaCategoryDatas { get; }
        IAppointmentTypeDataRepository AppointmentTypeDatas { get; }
        IFundingSourceDataRepository FundingSourceDatas { get; }
        ISupportingDocTypeDataRepository SupportingDocTypeDatas { get; }

        IHRMSequenceNumberRepository HRMSequenceNumbers { get; }


        /*linking repository*/
        ISessionRepository Sessions { get; }

        IVoteAssignmentRepository VoteAssignments { get; }
        IVoteAssignmentDetailsRepository VoteAssignmentDetails { get; }

        IVoteDetailRepository VoteDetails { get; }
        IVoteBalanceRepository VoteBalances { get; }
        IVoteBalanceLogRepository VoteBalanceLogs { get; }
        IVoteBalanceActionLogRepository VoteBalanceActionLogs { get; }

        IInternalJournalTransfersRepository InternalJournalTransfers { get; }

        ISpecialLedgerAccountsRepository SpecialLedgerAccounts { get; }

        // Loan Management
        IAdvanceBRepository AdvanceBs { get; }
        IAdvanceBTypeDataRepository AdvanceBTypeDatas { get; }

        IAdvancedBTypeLedgerMappingRepository AdvanceBTypeLedgerMapping { get; }
        Task<int> CommitAsync();

    }
}
