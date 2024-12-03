using CAT20.Core.Models.Mixin;
using CAT20.Core.Repositories.Common;
using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.FinalAccount;
using CAT20.Core.Repositories.ShopRental;
using CAT20.Core.Repositories.Vote;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Threading.Tasks;

namespace CAT20.Core
{
    public interface IShopRentalUnitOfWork : IDisposable, IAsyncDisposable
    {
        IRentalPlaceRepository RentalPlaces { get; }
        IFloorRepository Floors { get; }
        IPropertyNatureRepository PropertyNatures { get; }
        IPropertyTypeRepository PropertyTypes { get; }
        IPropertyRepository Properties { get; }
        IShopRepository Shops { get; }

        IShopRentalOpeningBalanceRepository ShopRentalOpeningBalance { get; }

        IShopRentalBalanceRepository ShopRentalBalance { get; }

        IShopRentalVoteAssignRepository ShopRentalVoteAssign { get; }

        IShopRentalVotePaymentTypeRepository ShopRentalVotePaymentType { get; }

        IShopRentalProcessConfigarationRepository ShopRentalProcessConfigaration { get; }
        IFineRateTypeRepository FineRateType { get; }
        IFineCalTypeRepository FineCalType { get; }
        IRentalPaymentDateTypeRepository RentalPaymentDateType { get; }
        IFineChargingMethodRepository FineChargingMethod { get; }
        IProcessConfigurationSettingAssignRepository ProcessConfigurationSettingAssign { get; }
        IShopAgreementChangeRequestRepository ShopAgreementChangeRequest { get; }
        IShopRentalProcessRepository ShopRentalProcess { get; }
        IShopRentalReceivableIncomeVoteAssignRepository ShopRentalReceivableIncomeVoteAssign { get; }

        //linking repository
        IOfficeRepository Offices { get; }
        ISessionRepository Sessions { get; }

        IVoteBalanceRepository VoteBalances { get; }
        IVoteBalanceLogRepository VoteBalanceLogs { get; }
        IVoteBalanceActionLogRepository VoteBalanceActionLogs { get; }

        IInternalJournalTransfersRepository InternalJournalTransfers { get; }

        ISpecialLedgerAccountsRepository SpecialLedgerAccounts { get; }

        IDailyFineProcessLogRepository DailyFineProcessLog { get; }

        IShopAgreementActivityLogRepository ShopAgreementActivityLog { get; }
        //---
        IDbTransaction BeginTransaction();

        Task<int> CommitAsync();
    }
}
