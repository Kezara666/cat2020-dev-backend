using CAT20.Core;
using CAT20.Core.Repositories.AssessmentTax;
using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.FinalAccount;
using CAT20.Core.Repositories.Mixin;
using CAT20.Core.Repositories.ShopRental;
using CAT20.Core.Repositories.Vote;
using CAT20.Core.Repositories.WaterBilling;
using CAT20.Data.Repositories.AssessmentTax;
using CAT20.Data.Repositories.Control;
using CAT20.Data.Repositories.FinalAccount;
using CAT20.Data.Repositories.Mixin;
using CAT20.Data.Repositories.ShopRental;
using CAT20.Data.Repositories.Vote;
using CAT20.Data.Repositories.WaterBilling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Transactions;

namespace CAT20.Data
{
    public class MixinUnitOfWork : IMixinUnitOfWork
    {
        private readonly ControlDbContext _ccontext;

        private readonly MixinDbContext _mixincontext;

        private readonly AssessmentTaxDbContext _assmtcontext;

        private readonly WaterBillingDbContext _wbcontext;

        private readonly VoteAccDbContext _vtcontext;

        private VoteAssignmentRepository _voteAssignmentRepository;
        private VoteAssignmentDetailsRepository _voteAssignmentDetailsRepository;
        private CustomVoteSubLevel1Repository _customVoteSubLevel1Repository;
        private CustomVoteSubLevel2Repository _customVoteSubLevel2Repository;
        private MixinCancelOrdersRepository _mixinCancelOrderRepository;
        private MixinOrdersRepository _mixinOrderRepository;
        private MixinOrderLineRepository _mixinOrderLineRepository;
        private BankingRepository _bankingRepository;

        private MixinOrderLineLogRepository _mixOrderLogs;
        private MixinOrderLineLogRepository _mixOrderLineLogs;


        // linking repository

        //from control module
        private OfficeRepository _officeRepository;
        private DocumentSequenceNumberRepository _documentSequenceNumberRepository;
        private SessionRepository _sessionRepository;
        private BusinessTaxRepository _businessTaxRepository;


        //from assessment module
        private AssessmentBalanceRepository _AssessmentBalanceRepository;
        private AssessmentRatesRepository _AssessmentRatesRepository;
        private AssessmentTransactionRepository _AssessmentTransactionRepository;

        //from waterBill module
        private WaterConnectionRepository _WaterConnections;
        private WaterConnectionBalanceRepository _WaterConnectionBalanceRepository;

        //from vote module

        private VoteDetailRepository _voteDetailRepository;
        private DepositRepository _depositRepository;
        private VoteBalanceRepository _voteBalanceRepository;
        private VoteBalanceLogRepository _voteBalanceLogRepository;
        private VoteBalanceActionLogRepository _voteBalanceActionLogRepository;

        private AccountDetailRepository _accountDetailRepository;
        private VoteLedgerBookRepository _voteLedgerBookRepository;
        private CashBookRepository _cashBookRepository;
        private SpecialLedgerAccountsRepository _specialLedgerAccountsRepository;

        //--------------[Start - shop renral balance repository]----------------------------------
        //Note : modified : 2024/04/09
        private readonly ShopRentalDbContext _shcontext;
        private ShopRentalBalanceRepository _shopRentalBalanceRepository;
        private ProcessConfigurationSettingAssignRepository _processConfigurationSettingAssignRepository;
        //--------------[End - shop renral balance repository]------------------------------------





        public MixinUnitOfWork(ControlDbContext ccontext, MixinDbContext context, AssessmentTaxDbContext assmtcontext, WaterBillingDbContext wbcontext,VoteAccDbContext vtcontext, ShopRentalDbContext shcontext)
        {
            _ccontext = ccontext;
            _mixincontext = context;
            _assmtcontext = assmtcontext;
            _wbcontext = wbcontext;
            _vtcontext = vtcontext;

            _shcontext = shcontext; //modified 2024/04/09
        }

        public IVoteAssignmentRepository VoteAssignments => _voteAssignmentRepository = _voteAssignmentRepository ?? new VoteAssignmentRepository(_mixincontext);
        public IVoteAssignmentDetailsRepository VoteAssignmentDetails => _voteAssignmentDetailsRepository = _voteAssignmentDetailsRepository ?? new VoteAssignmentDetailsRepository(_mixincontext);
        public IMixinCancelOrderRepository MixinCancelOrders => _mixinCancelOrderRepository = _mixinCancelOrderRepository ?? new MixinCancelOrdersRepository(_mixincontext);
        public IMixinOrderRepository MixinOrders => _mixinOrderRepository = _mixinOrderRepository ?? new MixinOrdersRepository(_mixincontext);
        public IMixinOrderLineRepository MixinOrderLines => _mixinOrderLineRepository = _mixinOrderLineRepository ?? new MixinOrderLineRepository(_mixincontext);
        public IBankingRepository Bankings => _bankingRepository = _bankingRepository ?? new BankingRepository(_mixincontext);
        public ICustomVoteSubLevel1Repository CustomVoteSubLevel1s => _customVoteSubLevel1Repository = _customVoteSubLevel1Repository ?? new CustomVoteSubLevel1Repository(_mixincontext);
        public ICustomVoteSubLevel2Repository CustomVoteSubLevel2s => _customVoteSubLevel2Repository = _customVoteSubLevel2Repository ?? new CustomVoteSubLevel2Repository(_mixincontext);

        public IMixinOrderLineLogRepository MixOrderLogs => _mixOrderLogs = _mixOrderLogs ?? new MixinOrderLineLogRepository(_mixincontext);
        public IMixinOrderLineLogRepository MixOrderLineLogs => _mixOrderLineLogs = _mixOrderLineLogs ?? new MixinOrderLineLogRepository(_mixincontext);


        // linking repository

        public IOfficeRepository Offices => _officeRepository = _officeRepository ?? new OfficeRepository(_ccontext);
        public IDocumentSequenceNumberRepository DocumentSequenceNumbers => _documentSequenceNumberRepository = _documentSequenceNumberRepository ?? new DocumentSequenceNumberRepository(_ccontext);
        public ISessionRepository Sessions => _sessionRepository = _sessionRepository ?? new SessionRepository(_ccontext);

        public IBusinessTaxRepository BusinessTaxes => _businessTaxRepository = _businessTaxRepository ?? new BusinessTaxRepository(_ccontext);
        public IAssessmentBalanceRepository AssessmentBalances => _AssessmentBalanceRepository = _AssessmentBalanceRepository ?? new AssessmentBalanceRepository(_assmtcontext);
        public IAssessmentRatesRepository AssessmentRates => _AssessmentRatesRepository = _AssessmentRatesRepository ?? new AssessmentRatesRepository(_assmtcontext);

        public IAssessmentTransactionRepository AssessmentTransactions => _AssessmentTransactionRepository = _AssessmentTransactionRepository ?? new AssessmentTransactionRepository(_assmtcontext);

        //WaterBilling
        public IWaterConnectionRepository WaterConnections => _WaterConnections = _WaterConnections ?? new WaterConnectionRepository(_wbcontext);

        public IWaterConnectionBalanceRepository Balances => _WaterConnectionBalanceRepository = _WaterConnectionBalanceRepository ?? new WaterConnectionBalanceRepository(_wbcontext);


        // Vote
        public IVoteDetailRepository VoteDetails => _voteDetailRepository = _voteDetailRepository ?? new VoteDetailRepository(_vtcontext);

        public IDepositRepository Deposits => _depositRepository = _depositRepository ?? new DepositRepository(_vtcontext);
        public IAccountDetailRepository AccountDetails => _accountDetailRepository = _accountDetailRepository ?? new AccountDetailRepository(_vtcontext);

        public IVoteBalanceRepository VoteBalances => _voteBalanceRepository = _voteBalanceRepository ?? new VoteBalanceRepository(_vtcontext);

        public IVoteBalanceLogRepository VoteBalanceLogs => _voteBalanceLogRepository = _voteBalanceLogRepository ?? new VoteBalanceLogRepository(_vtcontext);

        public IVoteBalanceActionLogRepository VoteBalanceActionLogs => _voteBalanceActionLogRepository = _voteBalanceActionLogRepository ?? new VoteBalanceActionLogRepository(_vtcontext);
        public IVoteLedgerBookRepository VoteLedgerBook => _voteLedgerBookRepository = _voteLedgerBookRepository ?? new VoteLedgerBookRepository(_vtcontext);



        public ICashBookRepository CashBook => _cashBookRepository = _cashBookRepository ?? new CashBookRepository(_vtcontext);

        public ISpecialLedgerAccountsRepository SpecialLedgerAccounts => _specialLedgerAccountsRepository = _specialLedgerAccountsRepository ?? new SpecialLedgerAccountsRepository(_vtcontext);



        //--------------[Start - shop renral balance repository]----------------------------------
        //Note : modified : 2024/04/09
        public IShopRentalBalanceRepository ShopRentalBalances => _shopRentalBalanceRepository = _shopRentalBalanceRepository ?? new ShopRentalBalanceRepository(_shcontext);
        public IProcessConfigurationSettingAssignRepository ProcessConfigurationSettingAssign => _processConfigurationSettingAssignRepository = _processConfigurationSettingAssignRepository ?? new ProcessConfigurationSettingAssignRepository(_shcontext);
        //--------------[End - shop renral balance repository]------------------------------------




        //public IDbTransaction BeginTransaction()
        //{
        //    var transaction = _mixincontext.Database.BeginTransaction();
        //    return transaction.GetDbTransaction();
        //}

        //public async Task<int> CommitAsync()
        //{
        //    // Use the execution strategy returned by DbContext.Database.CreateExecutionStrategy()
        //    var strategy = _assmtcontext.Database.CreateExecutionStrategy();

        //    // Execute the SaveChangesAsync calls within the execution strategy
        //    return await strategy.ExecuteAsync(async () =>
        //    {
        //        using (var transaction = _assmtcontext.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                int result = await _assmtcontext.SaveChangesAsync();
        //                result += await _ccontext.SaveChangesAsync();
        //                result += await _wbcontext.SaveChangesAsync();
        //                result += await _vtcontext.SaveChangesAsync();



        //                //--------------[Start - placeShopRentalOrder]--------------------------------------------
        //                //Note : modified : 2024/04/03
        //                result += await _shcontext.SaveChangesAsync();
        //                //--------------[End - placeShopRentalOrder]----------------------------------------------


        //                result += await _mixincontext.SaveChangesAsync();

        //                // If all SaveChangesAsync calls were successful, commit the transaction
        //                transaction.Commit();

        //                return result;
        //            }
        //            catch (Exception ex)
        //            {
        //                // If an exception occurs, roll back the transaction
        //                transaction.Rollback();
        //                throw;
        //            }
        //        }
        //    });
        //}


        public IDbTransaction BeginTransaction()
        {
            var transaction = _mixincontext.Database.BeginTransaction();
            return transaction.GetDbTransaction();
        }

        public async Task<IDbTransaction> BeginTransactionAsync()
        {
            var transaction = await _mixincontext.Database.BeginTransactionAsync();
            return transaction.GetDbTransaction();
        }


        public async Task<int> CommitAsync()
        {
            // Use the execution strategy returned by DbContext.Database.CreateExecutionStrategy()
            var strategy = _assmtcontext.Database.CreateExecutionStrategy();

            // Execute the SaveChangesAsync calls within the execution strategy
            //return await strategy.ExecuteAsync(async () =>
            //{
            //    using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            //    {
            //        try
            //        {
            //            int result = 0;

            //            result += await _assmtcontext.SaveChangesAsync();
            //            result += await _ccontext.SaveChangesAsync();
            //            result += await _wbcontext.SaveChangesAsync();
            //            result += await _vtcontext.SaveChangesAsync();

            //            result += await _shcontext.SaveChangesAsync();

            //            result += await _mixincontext.SaveChangesAsync();

            //            transactionScope.Complete();

            //            return result;
            //        }
            //        catch (Exception ex)
            //        {

            //            throw;
            //        }
            //    }
            //});

            return await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _assmtcontext.Database.BeginTransaction())
                {
                    try
                    {
                        int result = 0;


                        result += await _assmtcontext.SaveChangesAsync();
                        result += await _ccontext.SaveChangesAsync();
                        result += await _wbcontext.SaveChangesAsync();
                        result += await _vtcontext.SaveChangesAsync();

                        result += await _shcontext.SaveChangesAsync();

                        result += await _mixincontext.SaveChangesAsync();

                        // If all SaveChangesAsync calls were successful, commit the transaction
                        transaction.Commit();

                        return result;
                    }
                    catch (Exception ex)
                    {
                        // If an exception occurs, roll back the transaction
                        transaction.Rollback();
                        throw;
                    }
                }
            });
        }

        public void Dispose()
        {
            _ccontext.Dispose();
            _mixincontext.Dispose();
            _assmtcontext.Dispose();
            _wbcontext.Dispose();
            _vtcontext.Dispose();
            _shcontext.Dispose(); 
        }


        /*
        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
        public async ValueTask DisposeAsync()
{
          // Asynchronous cleanup code
}
        */







        /*

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            Dispose();
            await Task.CompletedTask;
        }


        */




        //public void Dispose()
        //{
        //    _context.DisposeAsync();
        //}


        //#region Implements IDisposable

        //#region Private Dispose Fields

        //private bool _disposed;

        //#endregion

        ///// <summary>
        ///// Cleans up any resources being used.
        ///// </summary>
        ///// <returns><see cref="ValueTask"/></returns>
        //public async ValueTask DisposeAsync()
        //{
        //    await DisposeAsync(true);

        //    // Take this object off the finalization queue to prevent 
        //    // finalization code for this object from executing a second time.
        //    GC.SuppressFinalize(this);
        //}

        ///// <summary>
        ///// Cleans up any resources being used.
        ///// </summary>
        ///// <param name="disposing">Whether or not we are disposing</param>
        ///// <returns><see cref="ValueTask"/></returns>
        //protected virtual async ValueTask DisposeAsync(bool disposing)
        //{
        //    if (!_disposed)
        //    {
        //        if (disposing)
        //        {
        //            // Dispose managed resources.
        //            await _context.DisposeAsync();
        //        }

        //        // Dispose any unmanaged resources here...

        //        _disposed = true;
        //    }
        //}

        //#endregion
    }
}