using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Mixin;

namespace CAT20.Core.Repositories.Mixin
{
    public interface IMixinOrderRepository : IRepository<MixinOrder>
    {
        Task<MixinOrder> GetById(int id);
        Task<MixinOrder> GetForProcessPaymentById(int id);
        Task<MixinOrder> getByPaymentDetailId(int? id);
        Task<MixinOrder> GetByIdAndOffice(int id, int officeid);
        Task<MixinOrder> GetByCode(string code, int officeid);
        Task<MixinOrder> GetOrderByBarcodeOfficeSession(string code, int officeid, int sessionid);
        Task<IEnumerable<MixinOrder>> GetAll();
        Task<IEnumerable<MixinOrder>> GetAllForOffice(int officeid);
        Task<(int totalCount, IEnumerable<MixinOrder> list)> GetAllForOfficeAndState(int Id, OrderStatus state, int pageNumber, int pageSize, string keyword);
        Task<(int totalCount, IEnumerable<MixinOrder> list)> GetAllPlacedAssessmentOrders(int assessmentId, int pageNumber);
        Task<(int totalCount, IEnumerable<MixinOrder> list)> getAllPlacedWaterConnectionOrders(int wcId, int pageNumber);
        Task<IEnumerable<MixinOrder>> GetAllForOfficeAndState(int Id, OrderStatus state);
        Task<IEnumerable<MixinOrder>> GetAllForOfficeAndStateAndDate(int Id, OrderStatus state, DateTime date);
        Task<IEnumerable<MixinOrder>> GetAllForUserAndState(int Id, OrderStatus state);

        Task<IEnumerable<MixinOrder>> GetPlacedOrdersByUserByCategoryByState(int userid, int category, OrderStatus state);

        Task<IEnumerable<MixinOrder>> GetAllForSessionAndState(int SessionId, OrderStatus state);
        Task<IEnumerable<MixinOrder>> GetAllCashBookForOfficeId(int officeid);
        Task<IEnumerable<MixinOrder>> GetAllCashBookForOfficeIdBankAccountId(int officeid, int banaccid);
        Task<IEnumerable<MixinOrder>> GetAllPaidOrdersForOfficeIdBankAccountIdCurrentSession(int officeid, int banaccid, int sessionid);
        Task<IEnumerable<MixinOrder>> GetAllPaidOrdersForOfficeIdCurrentSession(int officeid, int sessionid);
        Task<IEnumerable<MixinOrder>> GetAllTradeTaxOrdersForUserAndState(int Id, OrderStatus state);
        Task<IEnumerable<MixinOrder>> GetAllReceiptCreatedUsersForOffice(int officeId);
        Task<IEnumerable<MixinOrder>> GetAllReceiptCreatedUsersForSabha(int sabhaId);

        Task<IEnumerable<MixinOrder>> GetAllTradeLicensesForOfficeAndState(int Id, TradeLicenseStatus state);
        Task<IEnumerable<MixinOrder>> GetAllTradeLicensesForOfficeAndStateAndTaxType(int Id, TradeLicenseStatus state, int taxtypeid);

        Task<IEnumerable<Object>> GetAllTotalAmountsByAppCategoryForSession(Session session);
        Task<IEnumerable<Object>> GetAllOnlinePaymentTotalAmountsByAppCategoryForSession(Session session);

        Task<IEnumerable<MixinOrder>> GetForReversePayment(int assessmentId);
        Task<IEnumerable<MixinOrder>> GetForReversePaymentWithPendingBill(int assessmentId);
        Task<IEnumerable<MixinOrder>> GetForReversePaymentWaterBill(int wcPrimaryId);

        //--------------[cancelShopRentalOrder]----------------------------------------------
        //Note : modified : 2024/04/09
        Task<IEnumerable<MixinOrder>> GetForReversePaymentShopRentalPayment(int shopId);
        //--------------[cancelShopRentalOrder]----------------------------------------------
        Task<(int totalCount, IEnumerable<MixinOrder> list)> SearchOrderForAdjesment(List<int?> officeIds, OrderStatus state, int pageNo, int pageSize, string keyword);
        Task<(int totalCount, IEnumerable<MixinOrder> list)> SearchOrderByKeyword(List<int?> officeIds, OrderStatus state, int pageNo, int pageSize, string keyword);

        Task<MixinOrder> GetMixinOrderForRepaymentById(int mxId);




        Task<IEnumerable<MixinOrder>> GetForAssessmentReport(int assessment);
        Task<IEnumerable<MixinOrder>> GetForWaterBillReport(int assessment,int year,int month);

        Task<bool> HasPaidPostedOrdersForAssessment(int assessment);
        Task<IEnumerable<MixinOrder>> GetPaidPostedOrdersForAssessmentJournal(int assessment,int year);



        /*for final account */

        Task<MixinOrder> GetCrossOrderById(int mxId);
        Task<IEnumerable<MixinOrder>> GetForLegerAccountUpdate(int month,List<int?> officeIds);


        //shop -payment history
        Task<IEnumerable<MixinOrder>> GetPaidPostedOrdersByShopId(OrderStatus orderstate, int shopId);

        Task<IEnumerable<MixinOrder>> GetAllMixinOrderJournalEntryOrdersByOrderStatusAndOfficeId(OrderStatus orderstate, int officeId);

        Task<MixinOrder> GetMixinOrderJournalEntryOrderId(int mxId);

        Task<IEnumerable<MixinOrder>> GetAllForEmployeeId(int empId);
    }
}
