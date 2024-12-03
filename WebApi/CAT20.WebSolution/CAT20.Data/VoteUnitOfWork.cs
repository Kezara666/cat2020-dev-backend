using CAT20.Core;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.Common;
using CAT20.Core.Repositories.FinalAccount;
using CAT20.Core.Repositories.TradeTax;
using CAT20.Core.Repositories.Vote;
using CAT20.Core.Services.FinalAccount;
using CAT20.Data.Repositories.Common;
using CAT20.Data.Repositories.FinalAccount;
using CAT20.Data.Repositories.TradeTax;
using CAT20.Data.Repositories.Vote;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.Mixin;
using CAT20.Data.Repositories.Control;
using CAT20.Data.Repositories.Mixin;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using CAT20.Data.Repositories.AssessmentTax;
using CAT20.Core.Repositories.AssessmentTax;
using CAT20.Core.Repositories.WaterBilling;
using CAT20.Data.Repositories.WaterBilling;
using CAT20.Data.Repositories.ShopRental;
using CAT20.Core.Repositories.ShopRental;
using CAT20.Data.Repositories.HRM.LoanManagement;
using CAT20.Core.Repositories.HRM.LoanManagement;

namespace CAT20.Data
{
    public class VoteUnitOfWork : IVoteUnitOfWork
    {
        private readonly AssessmentTaxDbContext _assmtcontext;
        private readonly VoteAccDbContext _vcontext;
        public readonly MixinDbContext _mixincontext;
        public readonly ControlDbContext _ccontext;
        public readonly WaterBillingDbContext _wbContext;
        public readonly ShopRentalDbContext _shContext;
        private readonly HRMDbContext _hrmcontext;


        private MixinOrderLineRepository _mixinOrderLineRepository;
        private OfficeRepository _officeRepository;
        private SessionRepository _sessionRepository;
        private BusinessTaxRepository _businessTaxRepository;
        private DocumentSequenceNumberRepository _documentSequenceNumberRepository;


        private ProgrammeRepository _programmeRepository;
        private AccountBalanceDetailRepository _accountBalanceRepository;
        private AccountDetailRepository _accountDetailRepository;
        private BalancesheetBalanceRepository _balancesheetBalanceRepository;
        private BalancesheetSubtitleRepository _balancesheetSubtitleRepository;
        private BalancesheetTitleRepository _balancesheetTitleRepository;
        private ProjectRepository _projectRepository;
        private SubProjectRepository _subProjectRepository;
        private IncomeSubtitleRepository _incomeSubtitleRepository;
        private IncomeTitleRepository _incomeTitleRepository;
        private VoteBalanceRepository _voteBalanceRepository;
        private VoteBalanceLogRepository _voteBalanceLogRepository;
        private VoteBalanceActionLogRepository _voteBalanceActionLogRepository;

        private VoteJournalAdjustmentRepository _voteJournalAdjustments;
        private CutProvisionRepository _cutProvisionRepository;
        private SupplementaryRepository _supplementaryRepository;
        private FR66TransferRepository _fr66TransferRepository;
        private AccountTransferRepository _accountTransferRepository;
        private AccountTransferRefundingRepository _accountTransferRefundingRepository;

        private VoteDetailRepository _voteDetailRepository;
        private AuditLogRepository _auditLogRepository;
        private BusinessNatureRepository _businessNatureRepository;
        private BusinessSubNatureRepository _businessSubNatureRepository;
        private TaxValueRepository _taxValueRepository;
        private TradeTaxVoteAssignmentRepository _tradeTaxVoteAssignmentRepository;
        private CommitmentRepository _commitmentRepository;
        private CommitmentLineRepository _commitmentLineRepository;
        private CommitmentLogRepository _commitmentLogRepository;
        private CommitmentApprovedLogRepository _commitmentApprovedLogRepository;
        private CommitmentLineVoteRepository _commitmentLineVoteRepository;

        private VoucherRepository _voucherRepository;
        private VoucherLineRepository _voucherLineRepository;
        private VoucherLogRepository _voucherLogRepository;
        private VoucherApprovedLogRepository _voucherApprovedLogRepository;
        private VoucherCrossOrderRepository _voucherCrossOrderRepository;

        private VoucherDocumentRepository _voucherDocumentRepository;
        private VoucherInvoiceRepository _voucherInvoiceRepository;

        private VoucherChequeRepository _voucherChequeRepository;
        private AccountBalanceDetailRepository _accountBalanceDetailRepository;
        private DepositSubInfoRepository _depositSubInfoRepository;
        private FinalAccountSequenceNumberRepository _finalAccountSequenceNumberRepository;



        private VoucherVoteAllocationBalanceRepository _voucherVoteAllocationBalanceRepository;


        private VoteLedgerBookRepository _voteLedgerBookRepository;
        private VoteLedgerBookDailyBalanceRepository _voteLedgerBookDailyBalanceRepository;
        private CashBookRepository _cashBookRepository;
        private CashBookDailyBalanceRepository _cashBookDailyBalanceRepository;

        private DepositRepository _depositRepository;
        private SubImprestRepository _subImprestRepository;
        private SubImprestSettlementRepository _subImprestSettlementRepository;

        private InternalJournalTransfersRepository _internalJournalTransfers;
        private SpecialLedgerAccountsRepository _specialLedgerAccountsRepository;

        private SabhaFundSourceRepository _sabhaFundSourceRepository;
        private IndustrialCreditorsDebtorsTypesRepository _industrialCreditorsDebtorsTypesRepository;


        //..........................//
        private FixedDepositRepository _fixedDepositRepository;
        private CreditorsBillingRepository _creditorsBllingRepository;
        private IndustrialCreditorsRepository _industrialCreditorsRepository;
        private IndustrialDebtorsRepository _industrialDebtorsRepository;
        private LABankLoanRepository _lABankLoanRepository;
        private PrepaidPaymentRepository _prepaidPaymentRepository;
        private ReceivableExchangeNonExchangesRepository _receivableExchangeNonExchangesRepository;
        private StoreCreditorsRepository _storeCreditorsRepository;
        private FixedAssetsRepository _fixedAssetsRepository;
        private SingleOpenBalanceRepository _singleOpenBalanceRepository;
        private DepreciationRatesRepository _depreciationRatesRepository;

        private CustomVoteEntryRepository _CustomVoteEntryRepository;

        private CustomVoteBalanceRepository _CustomVoteBalanceRepository;
        private CustomVoteBalanceLogRepository _CustomVoteBalanceLogRepository;
        private CustomVoteBalanceActionLogRepository _CustomVoteBalanceActionLogRepository;

        //.............//

        /*linking module*/

        private VoteAssignmentRepository _voteAssignmentRepository;
        private VoteAssignmentDetailsRepository _voteAssignmentDetailsRepository;

        private MixinOrdersRepository _mixinOrderRepository;


        /*  assessment repository*/
        private AssessmentRepository _AssessmentRepository;
        private AssessmentVoteAssignRepository _AssmtVoteAssignRepository;

        private VoteAssignRepository _wbVoteAssignRepository;
        private WaterConnectionRepository _waterConnectionRepository;

        private ShopRepository _shopRepository;


        private ClassificationRepository _classificationRepository;
        private BudgetRepository _budgetRepository;




        /*hrm*/

        private AdvanceBRepository _advanceBRepository;


        // private DocumentSequenceNumberRepository _documentSequenceNumberRepository;

        public VoteUnitOfWork(VoteAccDbContext context, MixinDbContext mixinContext, ControlDbContext contrlontext, AssessmentTaxDbContext assmtcontext, WaterBillingDbContext wbContext,ShopRentalDbContext shContext, HRMDbContext hrmcontext)
        {
            _vcontext = context;
            _mixincontext = mixinContext;
            _ccontext = contrlontext;
            _assmtcontext = assmtcontext;
            _wbContext = wbContext;
            _shContext = shContext;
            _hrmcontext = hrmcontext;
        }

        public IMixinOrderRepository MixinOrders => _mixinOrderRepository ?? new MixinOrdersRepository(_mixincontext);
        public IMixinOrderLineRepository MixinOrderLines => _mixinOrderLineRepository =
            _mixinOrderLineRepository ?? new MixinOrderLineRepository(_mixincontext);

        public IOfficeRepository Offices => _officeRepository = _officeRepository ?? new OfficeRepository(_ccontext);

        // public IDocumentSequenceNumberRepository DocumentSequenceNumbers => _documentSequenceNumberRepository =
            // _documentSequenceNumberRepository ?? new DocumentSequenceNumberRepository(_contrlontext);

        public ISessionRepository Sessions => _sessionRepository = _sessionRepository ?? new SessionRepository(_ccontext);

        public IBusinessTaxRepository BusinessTaxes =>
            _businessTaxRepository = _businessTaxRepository ?? new BusinessTaxRepository(_ccontext);

        public IProgrammeRepository Programmes => _programmeRepository = _programmeRepository ?? new ProgrammeRepository(_vcontext);
        public IAccountBalanceDetailRepository AccountBalanceDetails => _accountBalanceRepository = _accountBalanceRepository ?? new AccountBalanceDetailRepository(_vcontext);
        public IBalancesheetBalanceRepository BalancesheetBalances => _balancesheetBalanceRepository = _balancesheetBalanceRepository ?? new BalancesheetBalanceRepository(_vcontext);
        public IAccountDetailRepository AccountDetails => _accountDetailRepository = _accountDetailRepository ?? new AccountDetailRepository(_vcontext);
        public IBalancesheetSubtitleRepository BalancesheetSubtitles => _balancesheetSubtitleRepository = _balancesheetSubtitleRepository ?? new BalancesheetSubtitleRepository(_vcontext);
        public IBalancesheetTitleRepository BalancesheetTitles => _balancesheetTitleRepository = _balancesheetTitleRepository ?? new BalancesheetTitleRepository(_vcontext);
        public IProjectRepository Projects => _projectRepository = _projectRepository ?? new ProjectRepository(_vcontext);
        public IIncomeSubtitleRepository IncomeSubtitles => _incomeSubtitleRepository = _incomeSubtitleRepository ?? new IncomeSubtitleRepository(_vcontext);
        public ISubProjectRepository SubProjects => _subProjectRepository = _subProjectRepository ?? new SubProjectRepository(_vcontext);
        public IIncomeTitleRepository IncomeTitles => _incomeTitleRepository = _incomeTitleRepository ?? new IncomeTitleRepository(_vcontext);
        public IVoteBalanceRepository VoteBalances => _voteBalanceRepository = _voteBalanceRepository ?? new VoteBalanceRepository(_vcontext);

        public IVoteBalanceLogRepository VoteBalanceLogs => _voteBalanceLogRepository = _voteBalanceLogRepository ??   new VoteBalanceLogRepository(_vcontext);
        public IVoteBalanceActionLogRepository VoteBalanceActionLogs => _voteBalanceActionLogRepository = _voteBalanceActionLogRepository ?? new VoteBalanceActionLogRepository(_vcontext);

        public IVoteJournalAdjustmentRepository VoteJournalAdjustments => _voteJournalAdjustments = _voteJournalAdjustments ?? new VoteJournalAdjustmentRepository(_vcontext);

        public ISupplementaryRepository Supplementary => _supplementaryRepository = _supplementaryRepository ?? new SupplementaryRepository(_vcontext);
        public ICutProvisionRepository CutProvision => _cutProvisionRepository = _cutProvisionRepository ?? new CutProvisionRepository(_vcontext);
        public IFR66TransferRepository FR66Transfer => _fr66TransferRepository = _fr66TransferRepository ?? new FR66TransferRepository(_vcontext);

        public IAccountTransferRepository AccountTransfer => _accountTransferRepository = _accountTransferRepository ?? new AccountTransferRepository(_vcontext);
        public IAccountTransferRefundingRepository AccountTransferRefunding => _accountTransferRefundingRepository = _accountTransferRefundingRepository ?? new AccountTransferRefundingRepository(_vcontext);

        public IVoteDetailRepository VoteDetails => _voteDetailRepository = _voteDetailRepository ?? new VoteDetailRepository(_vcontext);
        public IAuditLogRepository AuditLogs => _auditLogRepository = _auditLogRepository ?? new AuditLogRepository(_vcontext);
        public IBusinessNatureRepository BusinessNatures => _businessNatureRepository = _businessNatureRepository ?? new BusinessNatureRepository(_vcontext);
        public IBusinessSubNatureRepository BusinessSubNatures => _businessSubNatureRepository = _businessSubNatureRepository ?? new BusinessSubNatureRepository(_vcontext);
        public ITaxValueRepository TaxValues => _taxValueRepository = _taxValueRepository ?? new TaxValueRepository(_vcontext);
        public ITradeTaxVoteAssignmentRepository TradeTaxVoteAssignments => _tradeTaxVoteAssignmentRepository = _tradeTaxVoteAssignmentRepository ?? new TradeTaxVoteAssignmentRepository(_vcontext);

        public ICommitmentRepository Commitment => _commitmentRepository = _commitmentRepository ?? new CommitmentRepository(_vcontext);

        public ICommitmentLogRepository CommitmentLog => _commitmentLogRepository = _commitmentLogRepository ?? new CommitmentLogRepository(_vcontext);

        public ICommitmentActionLogRepository CommitmentActionLog => _commitmentApprovedLogRepository = _commitmentApprovedLogRepository ?? new CommitmentApprovedLogRepository(_vcontext);

        public ICommitmentLineRepository CommitmentLine => _commitmentLineRepository = _commitmentLineRepository ?? new CommitmentLineRepository(_vcontext);

        public ICommitmentLineVoteRepository CommitmentLineVote => _commitmentLineVoteRepository = _commitmentLineVoteRepository ?? new CommitmentLineVoteRepository(_vcontext);

        public IVoucherRepository Voucher => _voucherRepository = _voucherRepository ?? new VoucherRepository(_vcontext);
        public IVoucherLineRepository VoucherLine => _voucherLineRepository = _voucherLineRepository ?? new VoucherLineRepository(_vcontext);
        public IVoucherLogRepository VoucherLog => _voucherLogRepository = _voucherLogRepository ?? new VoucherLogRepository(_vcontext);
        public IVoucherActionLogRepository VoucherActionLog => _voucherApprovedLogRepository = _voucherApprovedLogRepository ?? new VoucherApprovedLogRepository(_vcontext);

        public IVoucherCrossOrderRepository VoucherCrossOrder => _voucherCrossOrderRepository = _voucherCrossOrderRepository ?? new VoucherCrossOrderRepository(_vcontext);
        public IVoucherDocumentRepository VoucherDocument => _voucherDocumentRepository = _voucherDocumentRepository ?? new VoucherDocumentRepository(_vcontext);
        public IVoucherInvoiceRepository VoucherInvoice => _voucherInvoiceRepository = _voucherInvoiceRepository ?? new VoucherInvoiceRepository(_vcontext);

        public IVoucherChequeRepository VoucherCheque => _voucherChequeRepository = _voucherChequeRepository ?? new VoucherChequeRepository(_vcontext);

        public IAccountBalanceDetailRepository AccountBalanceDetail => _accountBalanceDetailRepository =
            _accountBalanceDetailRepository ?? new AccountBalanceDetailRepository(_vcontext);

        public IDocumentSequenceNumberRepository DocumentSequenceNumbers => _documentSequenceNumberRepository =
            _documentSequenceNumberRepository ?? new DocumentSequenceNumberRepository(_ccontext);

        public IDepositRepository Deposits => _depositRepository = _depositRepository ?? new DepositRepository(_vcontext);
        public IDepositSubInfoRepository DepositSubInfo => _depositSubInfoRepository =  _depositSubInfoRepository ?? new DepositSubInfoRepository(_vcontext);

        public ISubImprestRepository SubImprests => _subImprestRepository = _subImprestRepository ?? new SubImprestRepository(_vcontext);
        public ISubImprestSettlementRepository SubImprestSettlements => _subImprestSettlementRepository = _subImprestSettlementRepository ?? new SubImprestSettlementRepository(_vcontext);


        public  IFinalAccountSequenceNumberRepository FinalAccountSequenceNumber => _finalAccountSequenceNumberRepository = _finalAccountSequenceNumberRepository ?? new FinalAccountSequenceNumberRepository(_vcontext);


        public IVoucherVoteAllocationBalanceRepository VoucherVoteAllocationBalance => _voucherVoteAllocationBalanceRepository = _voucherVoteAllocationBalanceRepository ?? new VoucherVoteAllocationBalanceRepository(_vcontext);   

        public IVoteLedgerBookRepository VoteLedgerBook =>  _voteLedgerBookRepository = _voteLedgerBookRepository ?? new VoteLedgerBookRepository(_vcontext);

        public IVoteLedgerBookDailyBalanceRepository VoteLedgerBookDailyBalance => _voteLedgerBookDailyBalanceRepository = _voteLedgerBookDailyBalanceRepository ?? new VoteLedgerBookDailyBalanceRepository(_vcontext);

        public ICashBookRepository CashBook => _cashBookRepository = _cashBookRepository ?? new CashBookRepository(_vcontext);
        public ICashBookDailyBalanceRepository CashBookDailyBalance => _cashBookDailyBalanceRepository = new CashBookDailyBalanceRepository(_vcontext);


        public IInternalJournalTransfersRepository InternalJournalTransfers => _internalJournalTransfers = _internalJournalTransfers ?? new InternalJournalTransfersRepository(_vcontext);

        public ISpecialLedgerAccountsRepository SpecialLedgerAccounts => _specialLedgerAccountsRepository = _specialLedgerAccountsRepository ?? new SpecialLedgerAccountsRepository(_vcontext);
     
        public ISabhaFundSourceRepository SabhaFundSources => _sabhaFundSourceRepository = _sabhaFundSourceRepository ?? new SabhaFundSourceRepository(_vcontext);
        public IIndustrialCreditorsDebtorsTypesRepository IndustrialCreditorsDebtorsTypes => _industrialCreditorsDebtorsTypesRepository = _industrialCreditorsDebtorsTypesRepository ?? new IndustrialCreditorsDebtorsTypesRepository(_vcontext);
        public IFixedAssetsRepository FixedAssets => _fixedAssetsRepository = _fixedAssetsRepository ?? new FixedAssetsRepository(_vcontext);
        public IDepreciationRatesRepository DepreciationRates => _depreciationRatesRepository = _depreciationRatesRepository ?? new DepreciationRatesRepository(_vcontext);

        //..................................//
        public IFixedDepositRepository FixedDeposits => _fixedDepositRepository = _fixedDepositRepository ?? new FixedDepositRepository(_vcontext);

        public ICreditorsBillingRepository CreditorsBilling => _creditorsBllingRepository = _creditorsBllingRepository ?? new CreditorsBillingRepository(_vcontext);
        public IIndustrialCreditorsRepository IndustrialCreditors => _industrialCreditorsRepository = _industrialCreditorsRepository ?? new IndustrialCreditorsRepository(_vcontext);
        public IIndustrialDebtorsRepository IndustrialDebtors => _industrialDebtorsRepository = _industrialDebtorsRepository ?? new IndustrialDebtorsRepository(_vcontext);

        public ILABAnkLoanRepository LABankLoans => _lABankLoanRepository = _lABankLoanRepository ?? new LABankLoanRepository(_vcontext);

        public IPrepaidPaymentRepository PrepaidPayments => _prepaidPaymentRepository = _prepaidPaymentRepository ?? new PrepaidPaymentRepository(_vcontext);
        public IReceivableExchangeNonExchangeRepository ReceivableExchangeNonExchanges => _receivableExchangeNonExchangesRepository  = _receivableExchangeNonExchangesRepository ?? new ReceivableExchangeNonExchangesRepository(_vcontext);
        public IStoreCreditorsRepository StoreCreditors => _storeCreditorsRepository = _storeCreditorsRepository ?? new StoreCreditorsRepository(_vcontext);

        public ISingleOpenBalanceRepository SingleOpenBalances => _singleOpenBalanceRepository = _singleOpenBalanceRepository ?? new SingleOpenBalanceRepository(_vcontext);

        public ICustomVoteEntryRepository CustomVoteEntries => _CustomVoteEntryRepository = _CustomVoteEntryRepository ?? new CustomVoteEntryRepository(_vcontext);
        public ICustomVoteBalanceRepository CustomVoteBalances => _CustomVoteBalanceRepository = _CustomVoteBalanceRepository ?? new CustomVoteBalanceRepository(_vcontext);
        public ICustomVoteBalanceLogRepository CustomVoteBalanceLogs => _CustomVoteBalanceLogRepository = _CustomVoteBalanceLogRepository ?? new CustomVoteBalanceLogRepository(_vcontext);
        public ICustomVoteBalanceActionLogRepository CustomVoteBalanceActionLogs => _CustomVoteBalanceActionLogRepository = _CustomVoteBalanceActionLogRepository ?? new CustomVoteBalanceActionLogRepository(_vcontext);


        /*linking module */

        public IVoteAssignmentRepository VoteAssignments => _voteAssignmentRepository = _voteAssignmentRepository ?? new VoteAssignmentRepository(_mixincontext);
        public IVoteAssignmentDetailsRepository CustomVoteDetails => _voteAssignmentDetailsRepository = _voteAssignmentDetailsRepository ?? new VoteAssignmentDetailsRepository(_mixincontext);
      
        public IAssessmentRepository Assessments => _AssessmentRepository = _AssessmentRepository ?? new AssessmentRepository(_assmtcontext);
        public IAssessmentVoteAssignRepository AssmtVoteAssigns => _AssmtVoteAssignRepository = _AssmtVoteAssignRepository ?? new AssessmentVoteAssignRepository(_assmtcontext);

        public IVoteAssignRepository WbVoteAssign => _wbVoteAssignRepository = _wbVoteAssignRepository ?? new VoteAssignRepository(_wbContext);
        public IWaterConnectionRepository WaterConnections => _waterConnectionRepository = _waterConnectionRepository ?? new WaterConnectionRepository(_wbContext);

        public IShopRepository Shops => _shopRepository = _shopRepository ?? new ShopRepository(_shContext);
        public IClassificationRepository Classifications => _classificationRepository = _classificationRepository ?? new ClassificationRepository(_vcontext);

        public IBudgetRepository Budget => _budgetRepository = _budgetRepository ?? new BudgetRepository(_vcontext);


        public IAdvanceBRepository AdvanceBs => _advanceBRepository = _advanceBRepository ?? new AdvanceBRepository(_hrmcontext);

        public IBudgetRepository Budgets => throw new NotImplementedException();

        //public async Task<int> CommitAsync()
        //{
        //    try
        //    {
        //        return await _vcontext.SaveChangesAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //}


        public IDbTransaction BeginTransaction()
        {
            var transaction = _vcontext.Database.BeginTransaction();
            return transaction.GetDbTransaction();
        }



        public async Task<int> CommitAsync()
        {
            var strategy = _mixincontext.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _mixincontext.Database.BeginTransaction())

                    try
                    {
                        int result = await _mixincontext.SaveChangesAsync();
                        result += await _ccontext.SaveChangesAsync();
                        result += await _assmtcontext.SaveChangesAsync();
                        result += await _vcontext.SaveChangesAsync();
                        result += await _wbContext.SaveChangesAsync();
                        result += await _shContext.SaveChangesAsync();

                        transaction.Commit();

                        return result;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
            });
        }

        //public void Dispose()
        //{
        //    _context.Dispose();
        //}

        //private bool _disposed = false;
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!_disposed)
        //    {
        //        if (disposing)
        //        {
        //            _vcontext.Dispose();
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

        public void Dispose()
        {
            _vcontext.Dispose();
            _assmtcontext.Dispose();
            _mixincontext.Dispose();
            _ccontext.Dispose();
            _wbContext.Dispose();
            _shContext.Dispose();
        }

    }
}