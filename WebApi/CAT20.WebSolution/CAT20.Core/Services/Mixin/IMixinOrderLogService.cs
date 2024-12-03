using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Mixin;

namespace CAT20.Core.Services.Mixin
{
    public interface IMixinOrderLogService
    {
        Task<MixinOrderLog> GetById(int id);
        Task<MixinOrderLog> getByPaymentDetailId(int id);
        Task<MixinOrderLog> GetByIdAndOffice(int id, int officeid);
        Task<MixinOrderLog> GetByCode(string code, int officeid);
        Task<MixinOrderLog> GetOrderByBarcodeOfficeSession(string code, int officeid, int sessionid);
        Task<MixinOrderLog> Create(MixinOrderLog newMixinOrderLog);
        Task<IEnumerable<MixinOrderLog>> PlaceAssessmentOrder(List<MixinOrderLog> newOrders);
        Task<MixinOrderLog> PlaceWaterBillOrdersAndProcessPayments(MixinOrderLog mxOrder);
        Task Update(MixinOrderLog MixinOrderLogToBeUpdated, MixinOrderLog MixinOrderLog);
        //Task Cancel(MixinOrderLog MixinOrderLog);
        Task Paid(MixinOrderLog MixinOrderLog, int cashierid, string documentCode);
        Task<bool> ApproveCancelOrder(MixinCancelOrder MixinOrderLog,HTokenClaim token);
        Task DisapproveCancelOrder(MixinOrderLog MixinOrderLog, int officerid);
        Task ApproveTradeLicense(MixinOrderLog MixinOrderLog, int officerid);
        Task DiapproveTradeLicense(MixinOrderLog MixinOrderLog, int officerid);
        Task<bool> CancelOrder(MixinOrderLog MixinOrderLog, int officerid);
        Task<bool> DeleteOrder(MixinOrderLog MixinOrderLog, int officerid);
        Task PostOrder(MixinOrderLog MixinOrderLog, int officerid);

        Task updateState(MixinOrderLog MixinOrderLog, OrderStatus state);
        Task UpdateTradeLicenseStatus(MixinOrderLog MixinOrderLog, TradeLicenseStatus state);

        Task<IEnumerable<MixinOrderLog>> GetAll();
        Task<IEnumerable<MixinOrderLog>> GetAllForOffice(int officeid);
        Task<(int totalCount, IEnumerable<MixinOrderLog> list)> GetAllForOfficeAndState(int officeid, OrderStatus state, int pageNumber, int PageSize, string filterKeyWord);
        Task<(int totalCount, IEnumerable<MixinOrderLog> list)> GetAllPlacedAssessmentOrders(int assessmentId, int pageNumber);
        Task<(int totalCount, IEnumerable<MixinOrderLog> list)> getAllPlacedWaterConnectionOrders(int wcId, int pageNumber);
        Task<IEnumerable<MixinOrderLog>> GetAllForOfficeAndState(int officeid, OrderStatus state);
        Task<IEnumerable<MixinOrderLog>> GetAllForOfficeAndStateAndDate(int officeid, OrderStatus state, DateTime fordate);
        Task<IEnumerable<MixinOrderLog>> GetAllForUserAndState(int userid, OrderStatus state);
        Task<IEnumerable<MixinOrderLog>> GetPlacedOrdersByUserByCategoryByState(int userid, int category, OrderStatus state);
        Task<IEnumerable<MixinOrderLog>> GetAllForSessionAndState(int sessionid, OrderStatus state);
        Task<IEnumerable<MixinOrderLog>> GetAllCashBookForOfficeId(int officeid);
        Task<IEnumerable<MixinOrderLog>> GetAllCashBookForOfficeIdBankAccountId(int officeid, int bankaccid);
        Task<IEnumerable<MixinOrderLog>> GetAllPaidOrdersForOfficeIdBankAccountIdCurrentSession(int officeid, int bankaccid, int sessionid);
        Task<IEnumerable<MixinOrderLog>> GetAllPaidOrdersForOfficeIdCurrentSession(int officeid, int sessionid);
        Task<IEnumerable<MixinOrderLog>> GetAllTradeTaxOrdersForUserAndState(int sessionid, OrderStatus state);
        Task<IEnumerable<MixinOrderLog>> GetAllReceiptCreatedUsersForOffice(int officeId);
        Task<IEnumerable<MixinOrderLog>> GetAllReceiptCreatedUsersForSabha(int sabhaId);

        //not checked
        Task<IEnumerable<MixinOrderLog>> GetAllTradeLicensesForOfficeAndState(int officeid, TradeLicenseStatus state);
        Task<IEnumerable<MixinOrderLog>> GetAllTradeLicensesForOfficeAndStateAndTaxType(int officeid, TradeLicenseStatus state, int taxtypeid);

        Task<IEnumerable<Object>> GetAllTotalAmountsByAppCategoryForSession(Session session);
        Task<IEnumerable<Object>> GetAllOnlinePaymentTotalAmountsByAppCategoryForSession(Session session);

        Task<(bool,string?)> ProcessPayment(int id, int cashierid, HTokenClaim token);

        //--------------[Start - placeShopRentalOrder]--------------------------------------------
        //Note : modified : 2024/04/03
        Task<MixinOrderLog> PlaceShopRentalOrder(MixinOrderLog newMixinShopRentalOrder);
        //--------------[End - placeShopRentalOrder]----------------------------------------------


        Task<(int totalCount, IEnumerable<MixinOrderLog> list)> SearchOrderByKeyword(List<int?> officeIds, OrderStatus state, int pageNo, int pageSize, string keyword);

        Task<MixinOrderLog> GetMixinOrderLogForRepaymentById(int mxId);

        Task<(bool, string?,MixinOrderLog)> PlaceVoteSurchargeOrderAndProcessPayments(MixinOrderLog mxOrder, HTokenClaim token);

    }
}

