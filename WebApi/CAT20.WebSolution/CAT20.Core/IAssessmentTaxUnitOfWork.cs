using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Repositories.AssessmentAuditActivity;
using CAT20.Core.Repositories.AssessmentTax;
using CAT20.Core.Repositories.AssessmentTax.QuarterRepository;
using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.FinalAccount;
using CAT20.Core.Repositories.Mixin;
using CAT20.Core.Repositories.Vote;
using System.Data;

namespace CAT20.Core
{
    public interface IAssessmentTaxUnitOfWork : IDisposable
    {
        IWardRepository Wards { get; }
        IStreetRepository Streets { get; }
        IDescriptionRepository Descriptions { get; }
        IAssessmentPropertyTypeRepository AssessmentPropertyTypes { get; }

        IAssessmentRepository Assessments { get; }
        IAllocationRepository Allocations { get; }
        IAssessmetTempPartnerRepository TempPartners { get; }
        IAssessmentTempSubPartnerRepository TempSubPartners { get; }

        IAssessmentVoteAssignRepository AssmtVoteAssigns { get; }
        IAssessmentBalanceRepository AssessmentBalances { get; }
        IAssessmentVotePaymentTypeRepository AssmtVotePaymentTypes { get; }

        IAssessmentsBalancesHistoryRepository AssessmentsBalancesHistories { get; }
        IAssessmentRatesRepository AssessmentRates { get; }
        IQ1Repository Q1s { get; }
        IQ2Repository Q2s { get; }
        IQ3Repository Q3s { get; }
        IQ4Repository Q4s { get; }


        IAssessmentProcessRepository AssessmentProcesses { get; }

        IAssessmentTransactionRepository AssessmentTransactions { get; }
        IAssessmentDescriptionLogRepository AssessmentDescriptionLogs { get; }
        IAssessmentPropertyTypeLogRepository AssessmentPropertyTypeLogs { get; }

        IAllocationLogRepository AllocationLogs { get; }
        IAssessmentJournalRepository AssessmentJournals { get; }


        IAssessmentAssetsChangeRepository AssessmentAssetsChanges { get; }
        IAssessmentAuditLogRepository AssessmentAuditLogs { get; }

        INewAllocationRequestRepository  NewAllocationRequests { get; }

        IAssessmentQuarterReportRepository AssessmentQuarterReports { get; }
        IAssessmentQuarterReportLogRepository AssessmentQuarterReportLogs { get; }

        IAssessmentBillAdjustmentRepository AssessmentBillAdjustments { get; }

        INQ1Repository NQ1s { get; }
        INQ2Repository NQ2s { get; }
        INQ3Repository NQ3s { get; }
        INQ4Repository NQ4s { get; }

        IAmalgamationRepository Amalgamations { get; }
        ISubDivisionRepository SubDivisions { get; }

        IAmalgamationSubDivisionRepository AmalgamationSubDivision {  get; }
        IAmalgamationSubDivisionActionsRepository AmalgamationSubDivisionActions { get; }
        IAmalgamationAssessmentRepository AmalgamationAssessmentRepository { get; }
        IAmalgamationSubDivisionDocumentsRepository AmalgamationSubDivisionDocuments { get; }
        IAssessmentATDRepository AssessmentATDs { get; }

        //linking repository

        IPartnerRepository Partners { get; }

        IAssessmentUserActivityRepository AssessmentUserActivity { get; }

        IOfficeRepository Offices { get; }
        ISessionRepository Sessions { get; }

        IMixinOrderRepository MixinOrders { get; }


        IVoteBalanceRepository VoteBalances { get; }
        IVoteBalanceLogRepository VoteBalanceLogs { get; }
        IVoteBalanceActionLogRepository VoteBalanceActionLogs { get; }

        IInternalJournalTransfersRepository InternalJournalTransfers { get; }

        ISpecialLedgerAccountsRepository SpecialLedgerAccounts { get; }

        Task<int> CommitAsync();
        Task<int> StepAsync();

        IDbTransaction BeginTransaction();
    }
}
