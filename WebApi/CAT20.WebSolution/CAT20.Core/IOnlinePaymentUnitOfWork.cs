using System.Data;
using CAT20.Core.Repositories.AssessmentTax;
using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.Mixin;
using CAT20.Core.Repositories.OnlinePayment;
using CAT20.Core.Repositories.ShopRental;
using CAT20.Core.Repositories.WaterBilling;

namespace CAT20.Core;

public interface IOnlinePaymentUnitOfWork : IDisposable

{
    //likinig repository 
    IOfficeRepository Offices { get; }
    IDocumentSequenceNumberRepository DocumentSequenceNumbers { get; }

    ISessionRepository Sessions { get; }

    //
    ISabhaRepository Sabhas { get; }

    IPartnerRepository Partners { get; }

    //
    IBusinessTaxRepository BusinessTaxes { get; }

    IAssessmentBalanceRepository AssessmentBalances { get; }
    IAssessmentRatesRepository AssessmentRates { get; }
    IWaterConnectionRepository WaterConnections { get; }

    IAssessmentRepository Assessments { get; }
    IProvinceRepository Provinces { get; }
    IDistrictRepository Districts { get; }

    IMixinOrderRepository MixinOrders { get; }
    IVoteAssignmentRepository VoteAssignments { get; }
    IVoteAssignmentDetailsRepository VoteAssignmentDetails { get; }
    IMixinOrderLineRepository MixinOrderLines { get; }

    IAssessmentTransactionRepository AssessmentTransactions { get; }

    IPaymentDetailsRepository Payments { get; }

    ILogInRepository OnlineLogIns { get; }
    IPaymentGatewayRepository PaymentGateways { get; }

    IPaymentDetailBackUpRepository PaymentsBackUps { get; }

    IWaterConnectionBalanceRepository Balances { get; }
    IShopRepository Shops { get; }

    IDisputeRepository Dispute { get; }

    IBookingPropertyRepository BookingProperty { get; }

    IBookingSubPropertyRepository BookingSubProperty { get; }

    IBookingTimeSlotsRepository BookingTimeSlots { get; }

    IChargingSchemeRepository ChargingScheme { get; }

    IOnlineBookingRepository OnlineBooking { get; }

    IBookingDateRepository BookingDate { get; }

    //Task<int> CommitAsync();
    Task<int> CommitAsync();
    Task<int> CommitAsync1();
    Task<int> CommitAsync2();

    void Commit();


    // IDbTransaction BeginTransaction();
}