using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.HRM.PersonalFile;
using CAT20.Core.Models.Interfaces;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Repositories.Mixin;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.Mixin
{
    public class MixinOrdersRepository : Repository<MixinOrder>, IMixinOrderRepository
    {
        public MixinOrdersRepository(DbContext context) : base(context)
        {
        }

        public async Task<MixinOrder> GetById(int id)
        {
            try
            {
                var result = await mixinDbContext.MixinOrders
                     .Where(m => m.Id == id && m.State != OrderStatus.Deleted)
                    .FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<MixinOrder> GetForProcessPaymentById(int id)
        {
            try
            {
                var result = await mixinDbContext.MixinOrders
                    .Include(m => m.MixinOrderLine)
                     .Where(m => m.Id == id && m.State != OrderStatus.Deleted)
                    .FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<MixinOrder> getByPaymentDetailId(int? id)
        {
            
                var result = await mixinDbContext.MixinOrders
                     .Where(m => m.PaymentDetailId == id && m.PaymentDetailId != null)
                    .FirstOrDefaultAsync();

                return result;
            }
            
        

        public async Task<MixinOrder> GetByIdAndOffice(int id, int officeid)
        {
            return await mixinDbContext.MixinOrders
                .Where(m => m.Id == id && m.State != OrderStatus.Deleted && m.OfficeId == officeid)
                .FirstOrDefaultAsync();
        }


        public async Task<MixinOrder> GetByCode(string code, int officeid)
        {
            return await mixinDbContext.MixinOrders
            .Where(model => model.State == OrderStatus.Draft && model.OfficeId == officeid &&
        (EF.Functions.Like(model.Code, code)
        || EF.Functions.Like(code.Trim(), "%" + model.Code + "%")))
        .FirstOrDefaultAsync();
        }

        public async Task<MixinOrder> GetOrderByBarcodeOfficeSession(string code, int officeid, int sessionid)
        {
            return await mixinDbContext.MixinOrders
            .Where(model =>
        EF.Functions.Like(model.Code, code)
        || EF.Functions.Like(code.Trim(), "%" + model.Code + "%") && model.State == OrderStatus.Draft && model.OfficeId == officeid && model.SessionId == sessionid)
        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MixinOrder>> GetAll()
        {
            return
            await mixinDbContext.MixinOrders
                .Where(m => m.State != OrderStatus.Deleted)
                .OrderByDescending(m => m.Code)
                .ToListAsync();
        }

        public async Task<IEnumerable<MixinOrder>> GetAllForOffice(int officeId)
        {
            return
                await mixinDbContext.MixinOrders
                .Where(m => m.OfficeId == officeId && m.State != OrderStatus.Deleted)
                .ToListAsync();
        }

        public async Task<(int totalCount, IEnumerable<MixinOrder> list)> GetAllForOfficeAndState(int officeId, OrderStatus state, int pageNumber, int pageSize, string filterKeyword)
        {
            var keyword = "";
            if (filterKeyword != null)
            {
                keyword = "%" + filterKeyword + "%";
            }

            var result = await mixinDbContext.MixinOrders
                .Where(m => m.OfficeId == officeId && m.State == state && (string.IsNullOrEmpty(keyword) || (EF.Functions.Like(m.Code, keyword) || EF.Functions.Like(m.CustomerName, keyword)
                    || (EF.Functions.Like(m.CustomerNicNumber, keyword)) || (EF.Functions.Like(m.CustomerMobileNumber, keyword)) || (EF.Functions.Like(m.CreatedAt, keyword)))))
                .OrderByDescending(m => m.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalCount = 0;
            totalCount = await mixinDbContext.MixinOrders
                .Where(m => m.OfficeId == officeId && m.State == state && (string.IsNullOrEmpty(keyword) || (EF.Functions.Like(m.Code, keyword) || EF.Functions.Like(m.CustomerName, keyword))))
                .CountAsync();


            return (totalCount, result);
        }
        public async Task<IEnumerable<MixinOrder>> GetAllForOfficeAndState(int officeId, OrderStatus state)
        {
            var result = await mixinDbContext.MixinOrders
               .Where(m => m.OfficeId == officeId && m.State == state)
               .OrderByDescending(m => m.Id)
               .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<MixinOrder>> GetAllForOfficeAndStateAndDate(int officeId, OrderStatus state, DateTime fordate)
        {
            if (state == OrderStatus.Posted)
            {
                var result = await mixinDbContext.MixinOrders
                   .Where(m => m.OfficeId == officeId && (m.State == OrderStatus.Posted || m.State == OrderStatus.Cancel_Disapproved)
                   &&
                                m.CreatedAt.Year == fordate.Date.Year
                                && m.CreatedAt.Month == fordate.Date.Month
                                && m.CreatedAt.Day == fordate.Date.Day
                                )
                   .OrderByDescending(m => m.Id)
                   .ToListAsync();
                return result;
            }
            else
            {
                var result = await mixinDbContext.MixinOrders
                   .Where(m => m.OfficeId == officeId && m.State == state
                   &&
                                m.CreatedAt.Year == fordate.Date.Year
                                && m.CreatedAt.Month == fordate.Date.Month
                                && m.CreatedAt.Day == fordate.Date.Day
                                )
                   .OrderByDescending(m => m.Id)
                   .ToListAsync();
                return result;
            }
        }

        public async Task<IEnumerable<MixinOrder>> GetAllForUserAndState(int userid, OrderStatus state)
        {
            var result = await mixinDbContext.MixinOrders
               .Where(m => m.CreatedBy == userid && m.State == state && m.BusinessId == 0)
               .OrderByDescending(m => m.Id)
               .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<MixinOrder>> GetAllForSessionAndState(int sessionId, OrderStatus state)
        {
            return
                await mixinDbContext.MixinOrders
                .Where(m => m.SessionId == sessionId && m.State == state)
                .ToListAsync();
        }

        public async Task<IEnumerable<MixinOrder>> GetAllCashBookForOfficeId(int officeId)
        {
            var mixinOrders = await mixinDbContext.MixinOrders
            .Where(m => !mixinDbContext.Bankings
            .Select(b => b.OrderId)
            .Contains(m.Id)
             && (m.PaymentMethodId == 1 || m.PaymentMethodId == 2)
                && m.OfficeId == officeId && m.AccountDetailId != 0 && (m.State == OrderStatus.Paid || m.State == OrderStatus.Posted || m.State == OrderStatus.Cancel_Disapproved))
            .OrderByDescending(m => m.Id)
            .ToListAsync();

            return mixinOrders;
        }

        public async Task<IEnumerable<MixinOrder>> GetAllCashBookForOfficeIdBankAccountId(int officeId, int bankaccid)
        {
            var mixinOrders = await mixinDbContext.MixinOrders
            .Where(m => !mixinDbContext.Bankings
            .Select(b => b.OrderId)
            .Contains(m.Id)
             && (m.PaymentMethodId == 1 || m.PaymentMethodId == 2)
                && m.OfficeId == officeId && m.AccountDetailId == bankaccid && (m.State == OrderStatus.Paid || m.State == OrderStatus.Posted || m.State == OrderStatus.Cancel_Disapproved))
            .OrderByDescending(m => m.Id)
            .ToListAsync();

            return mixinOrders;
        }


        public async Task<IEnumerable<MixinOrder>> GetAllPaidOrdersForOfficeIdBankAccountIdCurrentSession(int officeId, int bankaccid, int sessionid)
        {
            var result = await mixinDbContext.MixinOrders
                .Where(m => m.OfficeId == officeId && m.AccountDetailId == bankaccid && m.SessionId == sessionid && (m.State == OrderStatus.Paid || m.State == OrderStatus.Posted || m.State == OrderStatus.Cancel_Disapproved))
                .OrderByDescending(m => m.Id)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<MixinOrder>> GetAllPaidOrdersForOfficeIdCurrentSession(int officeId, int sessionid)
        {
            var result = await mixinDbContext.MixinOrders
                .Where(m => m.OfficeId == officeId && m.SessionId == sessionid && (m.State == OrderStatus.Paid || m.State == OrderStatus.Posted || m.State == OrderStatus.Cancel_Disapproved))
                .OrderByDescending(m => m.Id)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<MixinOrder>> GetAllTradeTaxOrdersForUserAndState(int userid, OrderStatus state)
        {
            var result = await mixinDbContext.MixinOrders
               .Where(m => m.CreatedBy == userid && m.State == state && m.BusinessId != 0)
               .OrderByDescending(m => m.Id)
               .ToListAsync();
            return result;
        }


        public async Task<IEnumerable<MixinOrder>> GetAllTradeLicensesForOfficeAndState(int officeId, TradeLicenseStatus state)
        {
            var result = await mixinDbContext.MixinOrders
               .Where(m => m.OfficeId == officeId && m.TradeLicenseStatus == state && m.TaxTypeId == 3 && (m.State == OrderStatus.Paid || m.State == OrderStatus.Posted || m.State == OrderStatus.Cancel_Disapproved))
               .OrderByDescending(m => m.Id)
               .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<MixinOrder>> GetAllTradeLicensesForOfficeAndStateAndTaxType(int officeId, TradeLicenseStatus state, int taxtypeid)
        {
            var result = await mixinDbContext.MixinOrders
               .Where(m => m.OfficeId == officeId && m.TradeLicenseStatus == state && m.TaxTypeId == taxtypeid && (m.State == OrderStatus.Paid || m.State == OrderStatus.Posted || m.State == OrderStatus.Cancel_Disapproved))
               .OrderByDescending(m => m.Id)
               .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<MixinOrder>> GetAllReceiptCreatedUsersForOffice(int officeId)
        {
            try
            {
                var result =
                    await mixinDbContext.MixinOrders
                    .Where(m => m.OfficeId == officeId)
                    .ToListAsync();

                return result.DistinctBy(m => m.CreatedBy).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<MixinOrder>> GetAllReceiptCreatedUsersForSabha(int sabhaid)
        {
            try
            {
                var result =
                    await mixinDbContext.MixinOrders
                    .Include(m => m.Office)
                    .Where(m => m.Office.SabhaID == sabhaid)
                    .ToListAsync();

                return result.DistinctBy(m => m.CreatedBy).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<MixinOrder>> GetPlacedOrdersByUserByCategoryByState(int userid, int category, OrderStatus state)
        {
            var result = await mixinDbContext.MixinOrders
              .Where(m => m.CreatedBy == userid && m.AppCategoryId == category && m.State == state)
              .OrderByDescending(m => m.Id)
              .ToListAsync();
            return result;
        }

        public async Task<(int totalCount, IEnumerable<MixinOrder> list)> GetAllPlacedAssessmentOrders(int assessmentId, int pageNumber)
        {
            try
            {

                var query = mixinDbContext.MixinOrders
                    .Where(a => a.AppCategoryId == 5 && a.AssessmentId == assessmentId)
                    .OrderByDescending(a => a.Id); 

                // Implement your logic to calculate totalCount

                int totalCount = await query.CountAsync();

                var pageSize = 10;
                int skipAmount = (pageNumber - 1) * pageSize;

                var assessmentList = await query.Skip(skipAmount).Take(pageSize).ToListAsync();

                return (totalCount: totalCount, list: assessmentList);

            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // You might want to return an appropriate error response.
                return (totalCount: 0, list: null);
            }
        }

        public async Task<(int totalCount, IEnumerable<MixinOrder> list)> getAllPlacedWaterConnectionOrders(int wcId, int pageNumber)
        {
            try
            {

                var query = mixinDbContext.MixinOrders
                    .Where(a => a.AppCategoryId == 4 && a.WaterConnectionId == wcId)
                    .OrderByDescending(a => a.Id); ;

                // Implement your logic to calculate totalCount

                int totalCount = await query.CountAsync();

                var pageSize = 10;
                int skipAmount = (pageNumber - 1) * pageSize;

                var assessmentList = await query.Skip(skipAmount).Take(pageSize).ToListAsync();

                return (totalCount: totalCount, list: assessmentList);

            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // You might want to return an appropriate error response.
                return (totalCount: 0, list: null);
            }
        }


        public async Task<IEnumerable<MixinOrder>> GetForReversePayment(int assessmentId)
        {
            return await mixinDbContext.MixinOrders
               .Include(m => m.MixinOrderLine)
               .Where(m => m.AssessmentId == assessmentId).OrderByDescending(m => m.Id).Take(2).ToListAsync();
        }

        public async Task<IEnumerable<MixinOrder>> GetForReversePaymentWithPendingBill(int assessmentId)
        {
            return await mixinDbContext.MixinOrders
               .Include(m => m.MixinOrderLine)
               .Where(m => m.AssessmentId == assessmentId && m.State== OrderStatus.Cancel_Pending).OrderByDescending(m => m.Id).ToListAsync();
        }

        public async Task<IEnumerable<MixinOrder>> GetForReversePaymentWaterBill(int wcPrimaryId)
        {

            return await mixinDbContext.MixinOrders
               //.Include(m => m.MixinOrderLine)
               .Where(m => m.WaterConnectionId == wcPrimaryId).OrderByDescending(m => m.Id).Take(2).ToListAsync();
        }


        public async Task<IEnumerable<object>> GetAllTotalAmountsByAppCategoryForSession(Session session)
        {
            var result = await mixinDbContext.MixinOrders
            .Where(m => m.SessionId == session.Id &&
                        (m.State == OrderStatus.Paid || m.State == OrderStatus.Posted || m.State == OrderStatus.Cancel_Disapproved))
            .GroupBy(m => new { m.AppCategoryId, m.CashierId, m.CreatedBy }) // Group by both AppCategoryId and CashierId
            .Select(g => new
            {
                AppCategoryId = g.Key.AppCategoryId,
                AppCategoryName = ((CAT20.Core.Models.Enums.AppCategory)g.Key.AppCategoryId).ToString(),
                TotalAmount = g.Sum(m => m.TotalAmount),
                SessionStartAt = session.StartAt.ToString("yyyy-MM-dd"),// Include the StartAt property from the Session object
                SessionId = session.Id,
                CashierId = g.Key.CashierId,
                CreatedBy = g.Key.CreatedBy,
                CashAmount = g.Where(m => m.PaymentMethodId == 1).Sum(m => m.TotalAmount),
                ChequeAmount = g.Where(m => m.PaymentMethodId == 2).Sum(m => m.TotalAmount),
                CrossAmount = g.Where(m => m.PaymentMethodId == 3).Sum(m => m.TotalAmount),
                DirectAmount = g.Where(m => m.PaymentMethodId == 4).Sum(m => m.TotalAmount)
            })
            .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<object>> GetAllOnlinePaymentTotalAmountsByAppCategoryForSession(Session session)
        {
            var result = await mixinDbContext.MixinOrders
            .Where(m => m.SessionId == session.Id &&
                        (m.State == OrderStatus.Paid || m.State == OrderStatus.Posted || m.State == OrderStatus.Cancel_Disapproved) && m.PaymentDetailId>0)
            .GroupBy(m => new { m.AppCategoryId, m.CashierId, m.CreatedBy }) // Group by both AppCategoryId and CashierId
            .Select(g => new
            {
                AppCategoryId = g.Key.AppCategoryId,
                AppCategoryName = ((CAT20.Core.Models.Enums.AppCategory)g.Key.AppCategoryId).ToString(),
                TotalAmount = g.Sum(m => m.TotalAmount),
                SessionStartAt = session.StartAt.ToString("yyyy-MM-dd"),// Include the StartAt property from the Session object
                SessionId= session.Id,
                CashierId = g.Key.CashierId,
                CreatedBy = g.Key.CreatedBy,
                CashAmount = g.Where(m => m.PaymentMethodId == 1).Sum(m => m.TotalAmount),
                ChequeAmount = g.Where(m => m.PaymentMethodId == 2).Sum(m => m.TotalAmount),
                CrossAmount = g.Where(m => m.PaymentMethodId == 3).Sum(m => m.TotalAmount),
                DirectAmount = g.Where(m => m.PaymentMethodId == 4).Sum(m => m.TotalAmount)
            })
            .ToListAsync();

            return result;
        }


        //--------------[cancelShopRentalOrder]----------------------------------------------
        //Note : modified : 2024/04/09

        public async Task<IEnumerable<MixinOrder>> GetForReversePaymentShopRentalPayment(int shopId)
        {
            return await mixinDbContext.MixinOrders
              .Where(m => m.ShopId == shopId && m.State !=0  ).OrderByDescending(m => m.Id).Take(2).ToListAsync();
        }
        //--------------[cancelShopRentalOrder]----------------------------------------------

        public async Task<(int totalCount, IEnumerable<MixinOrder> list)> SearchOrderForAdjesment(List<int?> officeIds, OrderStatus state, int pageNo, int pageSize, string keyword)
        {
            if (keyword != "undefined")
            {
                keyword = "%" + keyword + "%";
            }
            else if (keyword == "undefined")
            {
                keyword = null;
            }


            var result = mixinDbContext.MixinOrders
                         .Where(m => officeIds.Contains(m.OfficeId))
                        .Where(m => (m.State == OrderStatus.Posted || m.State == OrderStatus.Cancel_Disapproved) && (string.IsNullOrEmpty(keyword) || (EF.Functions.Like(m.Code, keyword) || EF.Functions.Like(m.CustomerName, keyword)
                    || (EF.Functions.Like(m.CustomerNicNumber, keyword)) || (EF.Functions.Like(m.CustomerMobileNumber, keyword)) || (EF.Functions.Like(m.CreatedAt, keyword)))))
                     .OrderByDescending(m => m.Id);


            int totalCount = await result.CountAsync();

            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);


        }

        public async Task<(int totalCount, IEnumerable<MixinOrder> list)> SearchOrderByKeyword(List<int?> officeIds, OrderStatus state, int pageNo, int pageSize, string keyword)
        {
            if (keyword != "undefined")
            {
                keyword = "%" + keyword + "%";
            }
            else if (keyword == "undefined")
            {
                keyword = null;
            }


            var result = mixinDbContext.MixinOrders
                         .Where(m => officeIds.Contains( m.OfficeId))
                        .Where(m=> (m.State == OrderStatus.Posted || m.State == OrderStatus.Cancel_Disapproved) && (string.IsNullOrEmpty(keyword) || (EF.Functions.Like(m.Code, keyword) || EF.Functions.Like(m.CustomerName, keyword)
                    || (EF.Functions.Like(m.CustomerNicNumber, keyword)) || (EF.Functions.Like(m.CustomerMobileNumber, keyword)) || (EF.Functions.Like(m.CreatedAt, keyword)))))
                     .OrderByDescending(m => m.Id);


            int totalCount = await result.CountAsync();

            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);


        }

        public async Task<MixinOrder> GetMixinOrderForRepaymentById(int mxId)
        {
                    return await mixinDbContext.MixinOrders
              .Include(m => m.MixinOrderLine)
              .Where(m => m.Id == mxId && (m.State == OrderStatus.Posted || m.State == OrderStatus.Cancel_Disapproved))
              .FirstOrDefaultAsync();

        }




        public async Task<IEnumerable<MixinOrder>> GetForAssessmentReport(int assessmentId)
        {
            return await mixinDbContext.MixinOrders
                 .AsNoTracking()
                .Where(m => m.AssessmentId == assessmentId && (m.State == OrderStatus.Posted || m.State == OrderStatus.Paid || m.State == OrderStatus.Cancel_Disapproved))
                //.OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<MixinOrder>> GetForWaterBillReport(int wcPrimaryId,int year, int month)
        {
            return await mixinDbContext.MixinOrders
                .AsNoTracking()
                .Include(m => m.MixinOrderLine)
                .Where(m => m.WaterConnectionId == wcPrimaryId && (m.State == OrderStatus.Posted || m.State == OrderStatus.Cancel_Disapproved) && m.CreatedAt.Year == year &&  m.CreatedAt.Month == month)
                .ToListAsync();
        }

        public async Task<bool> HasPaidPostedOrdersForAssessment(int assessmentId)
        {
            return await mixinDbContext.MixinOrders
                 .AsNoTracking()
                .AnyAsync(m => m.AssessmentId == assessmentId && (m.State == OrderStatus.Posted || m.State == OrderStatus.Paid || m.State == OrderStatus.Cancel_Disapproved));
                //.OrderByDescending(m => m.Id)
        }
        public async Task<IEnumerable<MixinOrder>> GetPaidPostedOrdersForAssessmentJournal(int assessmentId,int year)
        {
            return await mixinDbContext.MixinOrders
                 .AsNoTracking()
                .Where(m => m.AssessmentId == assessmentId && m.CreatedAt.Year == year &&  (m.State == OrderStatus.Posted || m.State == OrderStatus.Paid || m.State == OrderStatus.Cancel_Disapproved))
                .ToListAsync();
        }


        public async Task<MixinOrder> GetCrossOrderById(int mxId)
        {
            return await mixinDbContext.MixinOrders
                .Include(mx=>mx.MixinOrderLine)
                 .Where(m => m.Id == mxId)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<MixinOrder>> GetForLegerAccountUpdate(int month, List<int?> officeIds)
        {
            return await mixinDbContext.MixinOrders
                .AsNoTracking()
                .Include(m => m.MixinOrderLine)
                .Where(m =>  officeIds.Contains(m.OfficeId!.Value)  && (m.State == OrderStatus.Posted || m.State == OrderStatus.Cancel_Disapproved) && m.CreatedAt.Year == 2024 && m.CreatedAt.Month == month)
                .ToListAsync();
        }

        public async Task<IEnumerable<MixinOrder>> GetPaidPostedOrdersByShopId(OrderStatus orderstate, int shopId)
        {
            var result = await mixinDbContext.MixinOrders
              .Where(m => m.ShopId == shopId && m.AppCategoryId == 3 && m.State == orderstate)
              .OrderByDescending(m => m.Id)
              .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<MixinOrder>> GetAllMixinOrderJournalEntryOrdersByOrderStatusAndOfficeId(OrderStatus orderstate, int officeId)
        {
            var result = await mixinDbContext.MixinOrders
              .Include(m => m.MixinOrderLine)
              .Where(m => m.OfficeId == officeId && m.AppCategoryId == 9 && m.State == orderstate)
              .OrderByDescending(m => m.Id)
              .ToListAsync();
            return result;
        }

        public async Task<MixinOrder> GetMixinOrderJournalEntryOrderId(int mxId)
        {
            return await mixinDbContext.MixinOrders
                .Include(mx => mx.MixinOrderLine)
                .Where(m => m.Id == mxId)
                .FirstOrDefaultAsync();
        }

        private MixinDbContext mixinDbContext
        {
            get { return Context as MixinDbContext; }
        }

        public async Task<IEnumerable<MixinOrder>> GetAllForEmployeeId(int empId)
        {
            return
            await mixinDbContext.MixinOrders
                .Where(m => m.EmployeeId == empId && m.State != OrderStatus.Deleted)
                .ToListAsync();
        }

        //public async Task<IEnumerable<MixinOrder>> GetAllForOfficeId(int officeId)
        //{
        //    return await mixinDbContext.MixinOrders.Where(m => m.OfficeId == officeId && m.IsActive == 1).ToListAsync();
        //}

        //public async Task<IEnumerable<MixinOrder>> GetAllForVoteId(int Id)
        //{
        //    return await mixinDbContext.MixinOrders.Where(m => m.MixinVoteAssignmentId == Id).ToListAsync();
        //}
    }
}