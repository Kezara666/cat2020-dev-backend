using CAT20.Core;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Repositories.Common;
using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.FinalAccount;
using CAT20.Core.Repositories.ShopRental;
using CAT20.Core.Repositories.Vote;
using CAT20.Core.Services.ShopRental;
using CAT20.Data.Repositories.Common;
using CAT20.Data.Repositories.Control;
using CAT20.Data.Repositories.FinalAccount;
using CAT20.Data.Repositories.ShopRental;
using CAT20.Data.Repositories.Vote;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data
{
    public class ShopRentalUnitOfWork : IShopRentalUnitOfWork
    {
        private readonly ShopRentalDbContext _shcontext;

        //linking
        private readonly ControlDbContext _ccontext;

        private readonly VoteAccDbContext _vtcontext;


        private RentalPlaceRepository _rentalPlaceRepository;
        private FloorRepository _floorRepository;
        private PropertyNatureRepository _propertyNatureRepository;
        private PropertyTypeRepository _propertyTypeRepository;
        private PropertyRepository _propertyRepository;
        private ShopRepository _shopRepository;

        private ShopRentalOpeningBalanceRepository _ShopRentalOpeningBalanceRepository;

        private ShopRentalBalanceRepository _shopRentalBalanceRepository;

        private ShopRentalVoteAssignRepository _shopRentalVoteAssignRepository;

        private ShopRentalVotePaymentTypeRepository _shopRentalVotePaymentTypeRepository;

        private ShopRentalProcessConfigarationRepository _shopRentalProcessConfigarationRepository;
        
        private FineRateTypeRepository _fineRateTypeRepository;

        private FineCalTypeRepository _fineCalTypeRepository;

        private RentalPaymentDateTypeRepository _rentalPaymentDateTypeRepository;

        private FineChargingMethodRepository _fineChargingMethodRepository;

        private ProcessConfigurationSettingAssignRepository _processConfigurationSettingAssignRepository;

        private ShopAgreementChangeRequestRepository _shopAgreementChangeRequestRepository;

        private ShopRentalProcessRepository _shopRentalProcessRepository;

        private ShopRentalReceivableIncomeVoteAssignRepository _shopRentalReceivableIncomeVoteAssignRepository;

        //linking
        private OfficeRepository _officeRepository;
        private SessionRepository _sessionRepository;

        private VoteBalanceRepository _voteBalanceRepository;
        private VoteBalanceLogRepository _voteBalanceLogRepository;
        private VoteBalanceActionLogRepository _voteBalanceActionLogRepository;

        private InternalJournalTransfersRepository _internalJournalTransfers;
        private SpecialLedgerAccountsRepository _specialLedgerAccountsRepository;
        private DailyFineProcessLogRepository _dailyFineProcessLogRepository;
        private ShopAgreementActivityLogRepository _shopAgreementActivityLogRepository;
        public ShopRentalUnitOfWork(ShopRentalDbContext context, ControlDbContext ccontext, VoteAccDbContext vtcontext)
        {
            _shcontext = context;
            _ccontext = ccontext;
            _vtcontext = vtcontext;
        }

        public IRentalPlaceRepository RentalPlaces => _rentalPlaceRepository = _rentalPlaceRepository ?? new RentalPlaceRepository(_shcontext);
        public IFloorRepository Floors => _floorRepository = _floorRepository ?? new FloorRepository(_shcontext);
        public IPropertyNatureRepository PropertyNatures => _propertyNatureRepository = _propertyNatureRepository ?? new PropertyNatureRepository(_shcontext);
        public IPropertyTypeRepository PropertyTypes => _propertyTypeRepository = _propertyTypeRepository ?? new PropertyTypeRepository(_shcontext);
        public IPropertyRepository Properties => _propertyRepository = _propertyRepository ?? new PropertyRepository(_shcontext);
        public IShopRepository Shops => _shopRepository = _shopRepository ?? new ShopRepository(_shcontext);


        public IShopRentalOpeningBalanceRepository ShopRentalOpeningBalance => _ShopRentalOpeningBalanceRepository = _ShopRentalOpeningBalanceRepository ?? new ShopRentalOpeningBalanceRepository(_shcontext);

        public IShopRentalBalanceRepository ShopRentalBalance => _shopRentalBalanceRepository = _shopRentalBalanceRepository ?? new ShopRentalBalanceRepository(_shcontext);

        public IShopRentalVoteAssignRepository ShopRentalVoteAssign => _shopRentalVoteAssignRepository = _shopRentalVoteAssignRepository ?? new ShopRentalVoteAssignRepository(_shcontext);

        public IShopRentalVotePaymentTypeRepository ShopRentalVotePaymentType => _shopRentalVotePaymentTypeRepository = _shopRentalVotePaymentTypeRepository ?? new ShopRentalVotePaymentTypeRepository(_shcontext);

        public IShopRentalProcessConfigarationRepository ShopRentalProcessConfigaration => _shopRentalProcessConfigarationRepository = _shopRentalProcessConfigarationRepository ?? new ShopRentalProcessConfigarationRepository(_shcontext);

        public IFineRateTypeRepository FineRateType => _fineRateTypeRepository = _fineRateTypeRepository ?? new FineRateTypeRepository(_shcontext);

        public IFineCalTypeRepository FineCalType => _fineCalTypeRepository = _fineCalTypeRepository ?? new FineCalTypeRepository(_shcontext);

        public IRentalPaymentDateTypeRepository RentalPaymentDateType => _rentalPaymentDateTypeRepository = _rentalPaymentDateTypeRepository ?? new RentalPaymentDateTypeRepository(_shcontext);

        public IFineChargingMethodRepository FineChargingMethod => _fineChargingMethodRepository = _fineChargingMethodRepository ?? new FineChargingMethodRepository(_shcontext);

        public IProcessConfigurationSettingAssignRepository ProcessConfigurationSettingAssign => _processConfigurationSettingAssignRepository = _processConfigurationSettingAssignRepository ?? new ProcessConfigurationSettingAssignRepository(_shcontext);

        public IShopAgreementChangeRequestRepository ShopAgreementChangeRequest => _shopAgreementChangeRequestRepository = _shopAgreementChangeRequestRepository ?? new ShopAgreementChangeRequestRepository(_shcontext);

        public IShopRentalProcessRepository ShopRentalProcess => _shopRentalProcessRepository = _shopRentalProcessRepository ?? new ShopRentalProcessRepository(_shcontext);

        public IShopRentalReceivableIncomeVoteAssignRepository ShopRentalReceivableIncomeVoteAssign => _shopRentalReceivableIncomeVoteAssignRepository = _shopRentalReceivableIncomeVoteAssignRepository ?? new ShopRentalReceivableIncomeVoteAssignRepository(_shcontext);


        //linking
        public IOfficeRepository Offices => _officeRepository = _officeRepository ?? new OfficeRepository(_ccontext);
        public ISessionRepository Sessions => _sessionRepository = _sessionRepository ?? new SessionRepository(_ccontext);

        public IVoteBalanceRepository VoteBalances => _voteBalanceRepository = _voteBalanceRepository ?? new VoteBalanceRepository(_vtcontext);
        public IVoteBalanceLogRepository VoteBalanceLogs => _voteBalanceLogRepository = _voteBalanceLogRepository ?? new VoteBalanceLogRepository(_vtcontext);
        public IVoteBalanceActionLogRepository VoteBalanceActionLogs => _voteBalanceActionLogRepository = _voteBalanceActionLogRepository ?? new VoteBalanceActionLogRepository(_vtcontext);

        public IInternalJournalTransfersRepository InternalJournalTransfers => _internalJournalTransfers = _internalJournalTransfers ?? new InternalJournalTransfersRepository(_vtcontext);
        public ISpecialLedgerAccountsRepository SpecialLedgerAccounts => _specialLedgerAccountsRepository = _specialLedgerAccountsRepository ?? new SpecialLedgerAccountsRepository(_vtcontext);

        public IDailyFineProcessLogRepository DailyFineProcessLog => _dailyFineProcessLogRepository = _dailyFineProcessLogRepository ?? new DailyFineProcessLogRepository(_shcontext);

       public  IShopAgreementActivityLogRepository ShopAgreementActivityLog => _shopAgreementActivityLogRepository = _shopAgreementActivityLogRepository ?? new ShopAgreementActivityLogRepository(_shcontext);
 

        //--------[Start- Transaction] ------
        public IDbTransaction BeginTransaction()
        {
            var transaction = _shcontext.Database.BeginTransaction();
            return transaction.GetDbTransaction();
        }
        //--------[End- Transaction] --------

        public async Task<int> CommitAsync()
        {
            int result = await _ccontext.SaveChangesAsync();
            result += await _shcontext.SaveChangesAsync();
            result += await _vtcontext.SaveChangesAsync();

            return result;
        }


        //---
        //public async Task<int> CommitAsync()
        //{
        //    // Use the execution strategy returned by DbContext.Database.CreateExecutionStrategy()
        //    var strategy = _shcontext.Database.CreateExecutionStrategy();

        //    // Execute the SaveChangesAsync calls within the execution strategy
        //    return await strategy.ExecuteAsync(async () =>
        //    {
        //        using (var transaction = _shcontext.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                int result = await _ccontext.SaveChangesAsync();
        //                result += await _shcontext.SaveChangesAsync();

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
        //----

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _shcontext.Dispose();
                    _ccontext.Dispose();
                    _vtcontext.Dispose();
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
        //public void Dispose()
        //{
        //    _context.Dispose();
        //}
    }
}