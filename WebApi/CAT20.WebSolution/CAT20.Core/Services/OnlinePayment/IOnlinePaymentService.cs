using System.Collections;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Models.OnlinePayment;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Models.WaterBilling;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.Core.Services.OnlinePayment;

public interface IOnlinePaymentService
{

    Task<IEnumerable<Assessment>> GetAllForCustomerIdAndSabhaId(int customerid, int sabhaId);
    Task<IEnumerable<Assessment>> GetAllForIds(List<int> assessmentIds);
    Task<IEnumerable<Assessment>> GetAllForIdsAndSabha(List<int> assessmentIds, int sabhaid);
    Task<IEnumerable<WaterConnection>> getWaterBill(int partnerId, int sabhaId);
    Task<IEnumerable<Shop>> getShopRental(int partnerId, int sabhaId);
    // Task<IEnumerable<Sabha>> getSabhaProviceDistricForPartner(int partnerId);
    Task<List<IEnumerable<Sabha>>> getSabhaProviceDistricForPartner(int partnerId);

    Task<Verified> isAvailable(string NIC, string mobileNo);
    Task<Verified> isMobileAvailable(string mobileNo);
    Task<Verified> isEmailAvailable(string email);
    Task<Verified> isPartnerAvailable(string NIC, string mobileNo, int? sabhaId);

    Task<PaymentDetails> PlaceAssessmentOrder(List<MixinOrder> newOrders, int status , int transactionId , int? cId);

    Task<PaymentDetails> PlaceWaterBillOrder(List<MixinOrder> newOrders, int status, int transactionId, int? cId );
    Task<PaymentDetails> PlaceOtherPaymentOrder( int status , int transactionId);

    Task<PaymentDetails> PlaceBookingPaymentOrder(int status, int id , int orderId);

    Task<bool> PlaceOrderBackUp(int status, int paymentDetailId);

    Task<Dispute> CreateDispute(Dispute dispute);
    
    Task<int?> SavePaymentDetail(PaymentDetails paymentDetails);
    Task<int?> SaveGateway(PaymentGateway paymentGateway);

    Task<PaymentGateway> GetGateway(int? sabhsId);
    

    Task<int?> SaveLogInInfo(LogInDetails logInDetails);

    Task<PaymentDetails> GetPaymentDetailById(int? id);
    Task<(int totalCount, IEnumerable<PaymentDetails> list)> GetAllPaymentDetails(int officeId, int? pageNumber, int? pageSize,
        string? filterKeyword);
    Task<(int totalCount, IEnumerable<PaymentDetails> list)> getPaymentHistory(int officeId, int? pageNumber, int? pageSize,
        string? filterKeyword);
    Task<(int totalCount, IEnumerable<PaymentDetails> list)> GetOtherPaymentDetails(int officeId, int? pageNumber, int? pageSize, string? filterKeyword);
    Task<(int totalCount, IEnumerable<PaymentDetails> list)> getDisputes(int officeId, int? pageNumber, int? pageSize, string? filterKeyword);
    Task<(int totalCount, IEnumerable<PaymentDetails> list)> getPaymentHistoryForOffice(int officeId, int? pageNumber, int? pageSize, string? filterKeyword);
    Task<IEnumerable<PaymentDetails>> getPartnerPaymentHistory(int partnerId,int? pageNumber,int? pageSize);
    Task<IEnumerable<PaymentDetails>> getPartnerDisputes(int partnerId,int? pageNumber,int? pageSize);

    Task<PaymentDetails> CheckByCashier(int id, int cID, int flag);

    Task<int?> UpdatePaymentDetail(PaymentDetails paymentDetails);

    Task<bool> SaveError(int PaymentDetailsId);

     string Encrypt(string data, string key);
     string Decrypt(string cipherText, string key);
     Task<Object> Create(MixinOrder newMixinOrder, int? cId, bool? dispute);

     Task<IEnumerable<Province>> GetAllProvince();
     Task<IEnumerable<Sabha>> getAllSabha(int provinceId);
     Task<bool> paymentDetailSheduler();
}