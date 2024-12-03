using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Mixin;

namespace CAT20.Core.Repositories.Mixin
{
    public interface IMixinOrderLogRepository : IRepository<MixinOrderLog>
    {
        Task<MixinOrderLog> GetById(int id);
        Task<MixinOrderLog> GetForProcessPaymentById(int id);
        Task<MixinOrderLog> getByPaymentDetailId(int? id);
        Task<MixinOrderLog> GetByIdAndOffice(int id, int officeid);
        Task<MixinOrderLog> GetByCode(string code, int officeid);
        Task<MixinOrderLog> GetOrderByBarcodeOfficeSession(string code, int officeid, int sessionid);
        Task<IEnumerable<MixinOrderLog>> GetAll();
        Task<IEnumerable<MixinOrderLog>> GetAllForOffice(int officeid);
        Task<(int totalCount, IEnumerable<MixinOrderLog> list)> GetAllForOfficeAndState(int Id, OrderStatus state, int pageNumber, int pageSize, string keyword);
        Task<(int totalCount, IEnumerable<MixinOrderLog> list)> GetAllPlacedAssessmentOrders(int assessmentId, int pageNumber);
        Task<(int totalCount, IEnumerable<MixinOrderLog> list)> getAllPlacedWaterConnectionOrders(int wcId, int pageNumber);
        Task<IEnumerable<MixinOrderLog>> GetAllForOfficeAndState(int Id, OrderStatus state);
        Task<IEnumerable<MixinOrderLog>> GetAllForOfficeAndStateAndDate(int Id, OrderStatus state, DateTime date);
        Task<IEnumerable<MixinOrderLog>> GetAllForUserAndState(int Id, OrderStatus state);

        Task<IEnumerable<MixinOrderLog>> GetPlacedOrdersByUserByCategoryByState(int userid, int category, OrderStatus state);

        Task<IEnumerable<MixinOrderLog>> GetAllForSessionAndState(int SessionId, OrderStatus state);
        Task<IEnumerable<MixinOrderLog>> GetAllCashBookForOfficeId(int officeid);
        Task<IEnumerable<MixinOrderLog>> GetAllCashBookForOfficeIdBankAccountId(int officeid, int banaccid);
        Task<IEnumerable<MixinOrderLog>> GetAllPaidOrdersForOfficeIdBankAccountIdCurrentSession(int officeid, int banaccid, int sessionid);
        Task<IEnumerable<MixinOrderLog>> GetAllPaidOrdersForOfficeIdCurrentSession(int officeid, int sessionid);
        Task<IEnumerable<MixinOrderLog>> GetAllTradeTaxOrdersForUserAndState(int Id, OrderStatus state);
        Task<IEnumerable<MixinOrderLog>> GetAllReceiptCreatedUsersForOffice(int officeId);
        Task<IEnumerable<MixinOrderLog>> GetAllReceiptCreatedUsersForSabha(int sabhaId);

        Task<IEnumerable<MixinOrderLog>> GetAllTradeLicensesForOfficeAndState(int Id, TradeLicenseStatus state);
        Task<IEnumerable<MixinOrderLog>> GetAllTradeLicensesForOfficeAndStateAndTaxType(int Id, TradeLicenseStatus state, int taxtypeid);

        Task<IEnumerable<Object>> GetAllTotalAmountsByAppCategoryForSession(Session session);
        Task<IEnumerable<Object>> GetAllOnlinePaymentTotalAmountsByAppCategoryForSession(Session session);

        Task<IEnumerable<MixinOrderLog>> GetForReversePayment(int assessmentId);
        Task<IEnumerable<MixinOrderLog>> GetForReversePaymentWaterBill(int wcPrimaryId);

        //--------------[cancelShopRentalOrder]----------------------------------------------
        //Note : modified : 2024/04/09
        Task<IEnumerable<MixinOrderLog>> GetForReversePaymentShopRentalPayment(int shopId);
        //--------------[cancelShopRentalOrder]----------------------------------------------

        Task<(int totalCount, IEnumerable<MixinOrderLog> list)> SearchOrderByKeyword(List<int?> officeIds, OrderStatus state, int pageNo, int pageSize, string keyword);

        Task<MixinOrderLog> GetMixinOrderLogForRepaymentById(int mxId);




        Task<IEnumerable<MixinOrderLog>> GetForAssessmentReport(int assessment);
        Task<IEnumerable<MixinOrderLog>> GetForWaterBillReport(int assessment,int year,int month);





        /*for final account */

        Task<MixinOrderLog> GetCrossOrderById(int mxId);
    }
}
