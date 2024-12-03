using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Mixin;

namespace CAT20.Core.Services.Mixin
{
    public interface IMixinOrderService
    {
        Task<MixinOrder> GetById(int id);
        Task<MixinOrder> getByPaymentDetailId(int id);
        Task<MixinOrder> GetByIdAndOffice(int id, int officeid);
        Task<MixinOrder> GetByCode(string code, int officeid);
        Task<MixinOrder> GetOrderByBarcodeOfficeSession(string code, int officeid, int sessionid);
        Task<MixinOrder> Create(MixinOrder newMixinOrder);
        Task<IEnumerable<MixinOrder>> PlaceAssessmentOrder(List<MixinOrder> newOrders);
        Task<MixinOrder> PlaceWaterBillOrdersAndProcessPayments(MixinOrder mxOrder,HTokenClaim token);
        Task Update(MixinOrder mixinOrderToBeUpdated, MixinOrder mixinOrder);
        //Task Cancel(MixinOrder mixinOrder);
        Task Paid(MixinOrder mixinOrder, int cashierid, string documentCode);
        //Task<bool> ApproveCancelOrder(MixinCancelOrder mixinOrder,HTokenClaim token);
        Task<(bool, string?)> ApproveCancelOrder(MixinCancelOrder mxCancelOrder, HTokenClaim token);
        Task DisapproveCancelOrder(MixinOrder mixinOrder, int officerid);
        Task ApproveTradeLicense(MixinOrder mixinOrder, int officerid);
        Task DiapproveTradeLicense(MixinOrder mixinOrder, int officerid);
      //  Task<bool> CancelOrder(MixinOrder mixinOrder, int officerid);
        Task<(bool, string?)> CancelOrder(MixinOrder mixinOrder, int cashierid);
        Task<bool> DeleteOrder(MixinOrder mixinOrder, int officerid);
        Task PostOrder(MixinOrder mixinOrder, int officerid);

        Task updateState(MixinOrder mixinOrder, OrderStatus state);
        Task UpdateTradeLicenseStatus(MixinOrder mixinOrder, TradeLicenseStatus state);

        Task<IEnumerable<MixinOrder>> GetAll();
        Task<IEnumerable<MixinOrder>> GetAllForOffice(int officeid);
        Task<(int totalCount, IEnumerable<MixinOrder> list)> GetAllForOfficeAndState(int officeid, OrderStatus state, int pageNumber, int PageSize, string filterKeyWord);
        Task<(int totalCount, IEnumerable<MixinOrder> list)> GetAllPlacedAssessmentOrders(int assessmentId, int pageNumber);
        Task<(int totalCount, IEnumerable<MixinOrder> list)> getAllPlacedWaterConnectionOrders(int wcId, int pageNumber);
        Task<IEnumerable<MixinOrder>> GetAllForOfficeAndState(int officeid, OrderStatus state);
        Task<IEnumerable<MixinOrder>> GetAllForOfficeAndStateAndDate(int officeid, OrderStatus state, DateTime fordate);
        Task<IEnumerable<MixinOrder>> GetAllForUserAndState(int userid, OrderStatus state);
        Task<IEnumerable<MixinOrder>> GetPlacedOrdersByUserByCategoryByState(int userid, int category, OrderStatus state);
        Task<IEnumerable<MixinOrder>> GetAllForSessionAndState(int sessionid, OrderStatus state);
        Task<IEnumerable<MixinOrder>> GetAllCashBookForOfficeId(int officeid);
        Task<IEnumerable<MixinOrder>> GetAllCashBookForOfficeIdBankAccountId(int officeid, int bankaccid);
        Task<IEnumerable<MixinOrder>> GetAllPaidOrdersForOfficeIdBankAccountIdCurrentSession(int officeid, int bankaccid, int sessionid);
        Task<IEnumerable<MixinOrder>> GetAllPaidOrdersForOfficeIdCurrentSession(int officeid, int sessionid);
        Task<IEnumerable<MixinOrder>> GetAllTradeTaxOrdersForUserAndState(int sessionid, OrderStatus state);
        Task<IEnumerable<MixinOrder>> GetAllReceiptCreatedUsersForOffice(int officeId);
        Task<IEnumerable<MixinOrder>> GetAllReceiptCreatedUsersForSabha(int sabhaId);

        //not checked
        Task<IEnumerable<MixinOrder>> GetAllTradeLicensesForOfficeAndState(int officeid, TradeLicenseStatus state);
        Task<IEnumerable<MixinOrder>> GetAllTradeLicensesForOfficeAndStateAndTaxType(int officeid, TradeLicenseStatus state, int taxtypeid);

        Task<IEnumerable<Object>> GetAllTotalAmountsByAppCategoryForSession(Session session);
        Task<IEnumerable<Object>> GetAllOnlinePaymentTotalAmountsByAppCategoryForSession(Session session);

        Task<(bool,string?)> ProcessPayment(int id, int cashierid, HTokenClaim token);

        //--------------[Start - placeShopRentalOrder]--------------------------------------------
        //Note : modified : 2024/04/03
        Task<MixinOrder> PlaceShopRentalOrder(MixinOrder newMixinShopRentalOrder);
        //--------------[End - placeShopRentalOrder]----------------------------------------------


        Task<(int totalCount, IEnumerable<MixinOrder> list)> SearchOrderForAdjesment(List<int?> officeIds, OrderStatus state, int pageNo, int pageSize, string keyword);
        Task<(int totalCount, IEnumerable<MixinOrder> list)> SearchOrderByKeyword(List<int?> officeIds, OrderStatus state, int pageNo, int pageSize, string keyword);

        Task<MixinOrder> GetMixinOrderForRepaymentById(int mxId);

        Task<(bool, string?,MixinOrder)> PlaceVoteSurchargeOrderAndProcessPayments(MixinOrder mxOrder, HTokenClaim token);

        //shop payment history
        Task<IEnumerable<MixinOrder>> GetPaidPostedOrdersByShopId(OrderStatus orderstate, int shopId);

        Task<MixinOrder> CreateMixinOrderJournalEntry(MixinOrder newMixinOrder);

        Task<IEnumerable<MixinOrder>> GetAllMixinOrderJournalEntryOrdersByOrderStatusAndOfficeId(OrderStatus orderstate, int officeId);

        Task<MixinOrder> GetMixinOrderJournalEntryOrderId(int mxId);

        Task<bool> RejecttMixinOrderJournalEntryOrder(MixinOrder mixinOrder, HTokenClaim token);

        Task<bool> ApproveMixinOrderJournalEntryOrder(MixinOrder mixinOrder, HTokenClaim token);
        Task<IEnumerable<MixinOrder>> GetAllForEmployeeId(int empId);
    }
}

