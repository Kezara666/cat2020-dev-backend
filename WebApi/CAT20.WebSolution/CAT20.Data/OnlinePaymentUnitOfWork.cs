using System.Data;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using CAT20.Core;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Repositories.AssessmentTax;
using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.Mixin;
using CAT20.Core.Repositories.OnlinePayment;
using CAT20.Core.Repositories.ShopRental;
using CAT20.Core.Repositories.WaterBilling;
using CAT20.Core.Services.OnlinePayment;
using CAT20.Data.Repositories.AssessmentTax;
using CAT20.Data.Repositories.Control;
using CAT20.Data.Repositories.Mixin;
using CAT20.Data.Repositories.OnlinePayment;
using CAT20.Data.Repositories.ShopRental;
using CAT20.Data.Repositories.WaterBilling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CAT20.Data;

public class OnlinePaymentUnitOfWork : IOnlinePaymentUnitOfWork
{
    public readonly ControlDbContext _ccontext;
    public readonly AssessmentTaxDbContext _assmtcontext;
    public readonly MixinDbContext _mixincontext;
    public readonly OnlinePaymentDbContext _onineContext;
    private readonly WaterBillingDbContext _wbcontext;
    private readonly ShopRentalDbContext _shpcontext;


    //Controller Database
    private SabhaRepository _sabhaRepository;
    private ProvinceRepository _provinceRepository;

    private DistrictRepository _districtRepository;

    private PartnerRepository _partnerRepository;

    //Water billing Database
    private WaterConnectionRepository _waterConnectionRepository;

    //Assessment Database
    private AssessmentRepository _assessmentRepository;

    //Mixin 
    private MixinOrdersRepository _mixinOrderRepository;

    private MixinOrderLineRepository _mixinOrderLineRepository;
    private VoteAssignmentRepository _voteAssignmentRepository;
    private VoteAssignmentDetailsRepository _voteAssignmentDetailsRepository;
    private BankingRepository _bankingRepository;

    //ShopRental
    private ShopRepository _shopRepository;

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

    private PaymentDetailsRepository _paymentDetailsRepository;

    private LogInRepository _logInRepository;

    private DisputeRepository _disputeRepository;

    private PaymentGatewayRepository _paymentGatewayRepository;

    private PaymentDetailBackUpRepository _paymentDetailBackUpRepository;

    private WaterConnectionBalanceRepository _WaterConnectionBalanceRepository;
    private BookingPropertyRepository _bookingPropertyRepository;
    private BookingSubPropertyRepository _bookingSubPropertyRepository;
    private BookingTimeSlotsRepository _bookingTimeSlotsRepository;
    private BookingChargingSchemeRepository _bookingChargingSchemeRepository;
    private OnlineBookingRepository _onlineBookingRepository;
    private BookingDateRepository _bookingDateRepository;

    public OnlinePaymentUnitOfWork(WaterBillingDbContext wbcontext, ControlDbContext ccontext,
        AssessmentTaxDbContext assessment, MixinDbContext mixinContext, ShopRentalDbContext shopRentalDbContext,
        OnlinePaymentDbContext online)
    {
        _ccontext = ccontext;
        _assmtcontext = assessment;
        _mixincontext = mixinContext;
        _onineContext = online;
        _wbcontext = wbcontext;
        _shpcontext = shopRentalDbContext;
    }

    public ISabhaRepository Sabhas => _sabhaRepository = _sabhaRepository ?? new SabhaRepository(_ccontext);

    public IProvinceRepository Provinces =>
        _provinceRepository = _provinceRepository ?? new ProvinceRepository(_ccontext);

    public IDistrictRepository Districts =>
        _districtRepository = _districtRepository ?? new DistrictRepository(_ccontext);

    public IPartnerRepository Partners => _partnerRepository = _partnerRepository ?? new PartnerRepository(_ccontext);

    public IWaterConnectionRepository WaterConnections =>
        _waterConnectionRepository ?? new WaterConnectionRepository(_wbcontext);

    public IAssessmentRepository Assessments => _assessmentRepository ?? new AssessmentRepository(_assmtcontext);

    public IMixinOrderRepository MixinOrders => _mixinOrderRepository ?? new MixinOrdersRepository(_mixincontext);

    public IMixinOrderLineRepository MixinOrderLines => _mixinOrderLineRepository =
        _mixinOrderLineRepository ?? new MixinOrderLineRepository(_mixincontext);

    public IVoteAssignmentRepository VoteAssignments => _voteAssignmentRepository =
        _voteAssignmentRepository ?? new VoteAssignmentRepository(_mixincontext);

    public IVoteAssignmentDetailsRepository VoteAssignmentDetails => _voteAssignmentDetailsRepository =
        _voteAssignmentDetailsRepository ?? new VoteAssignmentDetailsRepository(_mixincontext);

    public IBankingRepository Bankings =>
        _bankingRepository = _bankingRepository ?? new BankingRepository(_mixincontext);


    // linking repository

    public IOfficeRepository Offices => _officeRepository = _officeRepository ?? new OfficeRepository(_ccontext);

    public IDocumentSequenceNumberRepository DocumentSequenceNumbers => _documentSequenceNumberRepository =
        _documentSequenceNumberRepository ?? new DocumentSequenceNumberRepository(_ccontext);

    public ISessionRepository Sessions => _sessionRepository = _sessionRepository ?? new SessionRepository(_ccontext);

    public IBusinessTaxRepository BusinessTaxes =>
        _businessTaxRepository = _businessTaxRepository ?? new BusinessTaxRepository(_ccontext);

    public IAssessmentBalanceRepository AssessmentBalances => _AssessmentBalanceRepository =
        _AssessmentBalanceRepository ?? new AssessmentBalanceRepository(_assmtcontext);

    public IAssessmentRatesRepository AssessmentRates => _AssessmentRatesRepository =
        _AssessmentRatesRepository ?? new AssessmentRatesRepository(_assmtcontext);

    public IAssessmentTransactionRepository AssessmentTransactions => _AssessmentTransactionRepository =
        _AssessmentTransactionRepository ?? new AssessmentTransactionRepository(_assmtcontext);

    public IPaymentDetailsRepository Payments => _paymentDetailsRepository =
        _paymentDetailsRepository ?? new PaymentDetailsRepository(_onineContext);

    public ILogInRepository OnlineLogIns => _logInRepository = _logInRepository ?? new LogInRepository(_onineContext);

    public IPaymentGatewayRepository PaymentGateways => _paymentGatewayRepository =
        _paymentGatewayRepository ?? new PaymentGatewayRepository(_onineContext);

    public IPaymentDetailBackUpRepository PaymentsBackUps => _paymentDetailBackUpRepository =
        _paymentDetailBackUpRepository ?? new PaymentDetailBackUpRepository(_onineContext);

    // Water
    public IWaterConnectionBalanceRepository Balances => _WaterConnectionBalanceRepository =
        _WaterConnectionBalanceRepository ?? new WaterConnectionBalanceRepository(_wbcontext);

    // ShopRental
    public IShopRepository Shops => _shopRepository =
        _shopRepository ?? new ShopRepository(_shpcontext);

    public IDisputeRepository Dispute =>
        _disputeRepository = _disputeRepository ?? new DisputeRepository(_onineContext);

    public IBookingPropertyRepository BookingProperty =>
       _bookingPropertyRepository = _bookingPropertyRepository ?? new BookingPropertyRepository(_onineContext);

    public IBookingSubPropertyRepository BookingSubProperty =>
      _bookingSubPropertyRepository = _bookingSubPropertyRepository ?? new BookingSubPropertyRepository(_onineContext);

    public IBookingTimeSlotsRepository BookingTimeSlots =>
    _bookingTimeSlotsRepository = _bookingTimeSlotsRepository ?? new BookingTimeSlotsRepository(_onineContext);

    public IChargingSchemeRepository ChargingScheme =>
    _bookingChargingSchemeRepository = _bookingChargingSchemeRepository ?? new BookingChargingSchemeRepository(_onineContext);
    public IOnlineBookingRepository OnlineBooking =>
    _onlineBookingRepository = _onlineBookingRepository ?? new OnlineBookingRepository(_onineContext);

    public IBookingDateRepository BookingDate =>
    _bookingDateRepository = _bookingDateRepository ?? new BookingDateRepository(_onineContext);

    public IDbTransaction BeginTransaction()
    {
        var transaction = _assmtcontext.Database.BeginTransaction();
        return transaction.GetDbTransaction();
    }

    public async Task<int> CommitAsync1()
    {
        var strategy = _onineContext.Database.CreateExecutionStrategy();

        // Execute the SaveChangesAsync calls within the execution strategy
        return await strategy.ExecuteAsync(async () =>
        {
            using (var transaction = _onineContext.Database.BeginTransaction())
            {
                try
                {
                    int result = await _onineContext.SaveChangesAsync();

                    // If all SaveChangesAsync calls were successful, commit the transaction
                    transaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    // If an exception occurs, roll back the transaction
                    transaction.Rollback();
                    return -1;
                }
            }
        });
    }

    public async Task<int> CommitAsync2()
    {
        
            var strategy = _mixincontext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _mixincontext.Database.BeginTransaction())
                {
                    try
                    {
                        int result = await _mixincontext.SaveChangesAsync();
                        result += await _onineContext.SaveChangesAsync();
                        result += await _ccontext.SaveChangesAsync();
                        result += await _wbcontext.SaveChangesAsync();

                        // If all SaveChangesAsync calls were successful, commit the transaction
                        transaction.Commit();

                        return result;
                    }
                    catch (Exception ex)
                    {
                        // If an exception occurs, roll back the transaction
                        transaction.Rollback();
                        return -1;
                    }
                }
            });
        }
    




    public void Commit()
    {
        try
        {
            _onineContext.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
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
                    int result = await _assmtcontext.SaveChangesAsync();
                    result += await _ccontext.SaveChangesAsync();
                    result += await _wbcontext.SaveChangesAsync();
                    result += await _shpcontext.SaveChangesAsync();
                    result += await _mixincontext.SaveChangesAsync();
                    result += await _onineContext.SaveChangesAsync();

                    // If all SaveChangesAsync calls were successful, commit the transaction
                    transaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                  
                    // If an exception occurs, roll back the transaction
                    transaction.Rollback();
                    throw;
                  /*  return -1;*/
                }
            }
        });
    }


    public void Dispose()
    {
        _ccontext.Dispose();
        _wbcontext.Dispose();
        _shpcontext.Dispose();
        _assmtcontext.Dispose();
        _onineContext.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }
}