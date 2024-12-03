using AutoMapper;
using AutoMapper.Configuration.Annotations;
using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.AssessmentTax;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Services.ShopRental;
using CAT20.Core.Services.Vote;
using CAT20.Core.Services.WaterBilling;
using CAT20.Services.WaterBilling;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Irony.Parsing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace CAT20.Services.Mixin
{

    public class MixinOrderService : IMixinOrderService
    {

        /* App category 
         * 
         * 3 - Shop Rental
         * 4 - Water 
         * 5 - Assessment
         * 6 - Cash Settlement
         * 7 - Account Transfer
         * 8 - Surcharge
         *
         *
         *
         */


        private readonly ILogger<MixinOrderService> _logger;
        private readonly IMapper _mapper;
        private readonly IMixinUnitOfWork _unitOfWork;
        private readonly IMixinVoteBalanceService _mixVoteBalanceService;
        private readonly IAssessmentBalanceService _assessmentBalanceService;
        private readonly IAssessmentCancelOrderService _assessmentCancelOrderService;
        private readonly IVoteAssignmentService _voteAssignmentService;
        private readonly IWaterConnectionBalanceService _waterConnectionBalanceService;
        private readonly IWaterBillCancelOrderService _waterBillCancelOrderService;

        private readonly IShopRentalProcessPaymentService _shopRentalProcessPaymentService; //Note : modified : 2024/04/03
        private readonly IShopRentalCancelOrderService _shopRentalCancelOrderService; //Note : modified : 2024/04/03

        public MixinOrderService(ILogger<MixinOrderService> logger, IMapper mapper, IMixinUnitOfWork unitOfWork, IMixinVoteBalanceService voteBalanceService, IAssessmentBalanceService assessmentBalanceService, IAssessmentCancelOrderService assessmentCancelOrderService, IVoteAssignmentService voteAssignmentService, IWaterConnectionBalanceService waterConnectionBalanceService, IWaterBillCancelOrderService waterBillCancelOrderService, IShopRentalProcessPaymentService shopRentalProcessPaymentService, IShopRentalCancelOrderService shopRentalCancelOrderService)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mixVoteBalanceService = voteBalanceService;
            _assessmentBalanceService = assessmentBalanceService;
            _assessmentCancelOrderService = assessmentCancelOrderService;
            _voteAssignmentService = voteAssignmentService;
            _waterConnectionBalanceService = waterConnectionBalanceService;
            _waterBillCancelOrderService = waterBillCancelOrderService;

            _shopRentalProcessPaymentService = shopRentalProcessPaymentService;
            _shopRentalCancelOrderService = shopRentalCancelOrderService;
        }

        public async Task<MixinOrder> GetById(int id)
        {
            return await _unitOfWork.MixinOrders.GetById(id);

        }
        public async Task<MixinOrder> getByPaymentDetailId(int id)
        {
            return await _unitOfWork.MixinOrders.getByPaymentDetailId(id);

        }

        public async Task<MixinOrder> GetByIdAndOffice(int id, int officeid)
        {
            return await _unitOfWork.MixinOrders.GetByIdAndOffice(id, officeid);
        }

        public async Task<MixinOrder> GetByCode(string code, int officeId)
        {
            return await _unitOfWork.MixinOrders.GetByCode(code, officeId);
        }
        public async Task<MixinOrder> GetOrderByBarcodeOfficeSession(string code, int officeId, int sessionid)
        {
            return await _unitOfWork.MixinOrders.GetOrderByBarcodeOfficeSession(code, officeId, sessionid);
        }

        public async Task<MixinOrder> Create(MixinOrder newMixinOrder)
        {
            try
            {
                //    var session = await _unitOfWork.Sessions.GetByIdAsync(newMixinOrder.SessionId);

                //var sessionDate = await _unitOfWork.Sessions.IsRescueSessionThenDate(newMixinOrder.SessionId);

                //if (sessionDate.HasValue)
                //{
                //    newMixinOrder.CreatedAt = (DateTime)sessionDate;

                //    foreach (var item in newMixinOrder!.MixinOrderLine)
                //    {
                //        item.CreatedAt = (DateTime)sessionDate;
                //    }
                //}
                //else
                //{
                //    newMixinOrder.CreatedAt = DateTime.Now;

                //    foreach (var item in newMixinOrder!.MixinOrderLine)
                //    {
                //        item.CreatedAt = DateTime.Now;
                //    }
                //}
                var activesession = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(newMixinOrder.OfficeId);

                if (activesession != null)
                {
                    newMixinOrder.SessionId = activesession.Id;

                    if (activesession.Rescue == 1)
                    {
                        newMixinOrder.CreatedAt = activesession.StartAt;
                    }
                    else
                    {
                        newMixinOrder.CreatedAt = DateTime.Now;
                    }

                    newMixinOrder.Code = DateTime.Now.ToString("HHmmssfff") + new Random().Next(10);
                    await _unitOfWork.MixinOrders.AddAsync(newMixinOrder);

                    if (newMixinOrder.AppCategoryId == 2)
                    {
                        if (newMixinOrder.BusinessId > 0)
                        {
                            var businesstaxline = await _unitOfWork.BusinessTaxes.GetBusinessTaxForBusinessIdAsync(newMixinOrder.BusinessId.Value);
                            var firstBusinessTax = businesstaxline.FirstOrDefault();

                            firstBusinessTax.TaxState = TaxStatus.SentToCashier;
                            firstBusinessTax.UpdatedAt = DateTime.Now;
                            firstBusinessTax.UpdatedBy = newMixinOrder.CreatedBy;
                        }
                        else
                        {
                            return null;
                        }
                    }

                    #region Audit Log
                    //var note = new StringBuilder();
                    //if (newMixinOrder.ID == 0)
                    //    note.Append("Created on ");
                    //else
                    //    note.Append("Edited on ");
                    //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                    //note.Append(" by ");
                    //note.Append("System");


                    //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                    //{
                    //    dateTime = DateTime.Now,
                    //    TransactionID = newMixinOrder.ID,
                    //    TransactionName = "MixinOrder",
                    //    User = 1,
                    //    Note = note.ToString()
                    //});

                    #endregion

                    await _unitOfWork.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                return new MixinOrder();
            }

            return newMixinOrder;
        }

        //----
        public async Task<MixinOrder> CreateMixinOrderJournalEntry(MixinOrder newMixinOrder)
        {
            try
            {
                if (newMixinOrder.SessionId != 0)
                {
                    //var activesession = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(newMixinOrder.OfficeId);
                    var targetSession = await _unitOfWork.Sessions.GetByIdAsync(newMixinOrder.SessionId);

                    if (targetSession != null)
                    {
                        newMixinOrder.CreatedAt = targetSession.StartAt;
                    }

                    newMixinOrder.Code = "JNL" + DateTime.Now.ToString("HHmmssfff") + new Random().Next(10);
                    await _unitOfWork.MixinOrders.AddAsync(newMixinOrder);

                    await _unitOfWork.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                return new MixinOrder();
            }

            return newMixinOrder;
        }
        //----
        public Task<MixinOrder> GetMixinOrderJournalEntryOrderId(int mxId)
        {
            return _unitOfWork.MixinOrders.GetMixinOrderJournalEntryOrderId(mxId);
        }
        //----
        public async Task<IEnumerable<MixinOrder>> GetAllMixinOrderJournalEntryOrdersByOrderStatusAndOfficeId(OrderStatus orderstate, int officeId)
        {
            return await _unitOfWork.MixinOrders.GetAllMixinOrderJournalEntryOrdersByOrderStatusAndOfficeId(orderstate, officeId);
        }
        //----
        public async Task<bool> RejecttMixinOrderJournalEntryOrder(MixinOrder mixinOrder, HTokenClaim token)
        {
            try
            {
                mixinOrder.State = OrderStatus.Rejected_Journal_Entry;
                mixinOrder.UpdatedAt = DateTime.Now;

                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during rejecting mixin order journal entry order .{EventType}", "MixIn Order Journal Entry Rejection");
                return false;
            }
        }
        //----
        public async Task<bool> ApproveMixinOrderJournalEntryOrder(MixinOrder mixinOrder, HTokenClaim token)
        {
            try
            {
                string prefix = "JNL";

                var docSeqNums = await _unitOfWork.DocumentSequenceNumbers
                                        .GetNextSequenceNumberForYearOfficePrefixAsync(mixinOrder.CreatedAt.Year, mixinOrder.OfficeId.Value, "MIX");

                if (docSeqNums != null)
                {
                    // Update mxOrder properties
                    mixinOrder.State = OrderStatus.Posted; //approved
                    mixinOrder.CashierId = token.userId;
                    mixinOrder.UpdatedAt = DateTime.Now;
                    mixinOrder.Code = $"{token.officeCode}/{DateTime.Now.Year}/{prefix}/{++docSeqNums.LastIndex}";
                }

                await _unitOfWork.CommitAsync();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred during approving mixin order journal entry order .{EventType}", "MixIn Order Journal Entry Approval");
                return false;
            }
        }
        //----

        public async Task Paid(MixinOrder mixinOrder, int cashierid, string documentCode)
        {
            try
            {
                if (mixinOrder.SessionId != 0)
                {
                    #region Audit Log

                    //var note = new StringBuilder();
                    //note.Append("Deleted on ");
                    //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                    //note.Append(" by ");
                    //note.Append("System");

                    //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                    //{
                    //    dateTime = DateTime.Now,
                    //    TransactionID = mixinOrder.ID,
                    //    TransactionName = "MixinOrder",
                    //    User = 1,
                    //    Note = note.ToString()
                    //});
                    //mixinOrder.Active = 0;

                    #endregion
                    mixinOrder.State = OrderStatus.Paid;
                    mixinOrder.Code = documentCode;
                    mixinOrder.CashierId = cashierid;

                    if (mixinOrder.TaxTypeId == 3)
                    {
                        mixinOrder.TradeLicenseStatus = TradeLicenseStatus.Pending_Approval;
                    }
                    mixinOrder.UpdatedAt = DateTime.Now;

                    await _unitOfWork.CommitAsync();
                }
            }
            catch (Exception)
            {
            }
            //_unitOfWork.MixinOrders.Remove(mixinOrder);
        }

        public async Task<(bool,string?)> CancelOrder(MixinOrder mixinOrder, int cashierid)
        {
            try
            {

                if (mixinOrder.AppCategoryId == 5 && mixinOrder.AssessmentId.HasValue && mixinOrder.AssessmentId != 0)
                {
                    var asmtBals = await _unitOfWork.AssessmentBalances.GetForOrderTransaction(mixinOrder.AssessmentId.Value);
                    if (asmtBals.HasTransaction == false)
                    {
                        asmtBals.HasTransaction = true;
                    }
                    else
                    {
                        throw new Exception("Already Have Transaction.");
                    }
                }


                if (mixinOrder.AppCategoryId == 3 && mixinOrder.ShopId.HasValue && mixinOrder.ShopId != 0)
                {
                    var asmtBals = await _unitOfWork.ShopRentalBalances.GetForOrderTransaction(mixinOrder.ShopId.Value);

                    foreach (var asmtbal in asmtBals) {
                        if (asmtbal.HasTransaction == false)
                        {
                            asmtbal.HasTransaction = true;
                        }
                        else
                        {
                            throw new Exception("Already Have Transaction.");
                        }
                    }

                   
                }

                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Deleted on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = mixinOrder.ID,
                //    TransactionName = "MixinOrder",
                //    User = 1,
                //    Note = note.ToString()
                //});
                //mixinOrder.Active = 0;

                #endregion
                mixinOrder.State = OrderStatus.Cancel_Pending;
                //mixinOrder.CashierId = cashierid;
                mixinOrder.UpdatedAt = DateTime.Now;
                await _unitOfWork.CommitAsync();

                return (true,"Order Cancelled successfully !");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during cancel order processing.{EventType}", "MixIn Order");
                return (false,ex.Message);
            }
            //_unitOfWork.MixinOrders.Remove(mixinOrder);
        }
        public async Task<bool> DeleteOrder(MixinOrder mixinOrder, int cashierid)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Deleted on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = mixinOrder.ID,
                //    TransactionName = "MixinOrder",
                //    User = 1,
                //    Note = note.ToString()
                //});
                //mixinOrder.Active = 0;

                #endregion


                //if (mixinOrder.BusinessId != 0)
                //{
                //    try
                //    {
                //        var businessTaxToBeUpdate = await _businessTaxService.GetById(mixinOrder.BusinessTaxId.Value);
                //        var businestaxline = businessTaxToBeUpdate;
                //        businestaxline.TaxState = TaxStatus.Draft;

                //        await _businessTaxService.Update(businessTaxToBeUpdate, businestaxline);
                //    }
                //    catch (Exception ex)
                //    {
                //    }
                //}

                if (mixinOrder.BusinessId.HasValue && mixinOrder.BusinessId != 0)
                {
                    var businessTaxes = await _unitOfWork.BusinessTaxes.GetByIdAsync(mixinOrder.BusinessId.Value);

                    if (businessTaxes != null)
                    {
                        businessTaxes.TaxState = TaxStatus.Draft;
                        businessTaxes.UpdatedBy = mixinOrder.CreatedBy;
                        businessTaxes.UpdatedAt = System.DateTime.Now;
                    }
                    else
                    {
                        throw new Exception("Business taxes not found.");
                    }

                }

                if (mixinOrder.AssessmentId.HasValue && mixinOrder.AssessmentId != 0)
                {
                    var asmtBal = await _unitOfWork.AssessmentBalances.GetForOrderTransaction(mixinOrder.AssessmentId.Value);
                    asmtBal.HasTransaction = false;
                }

                //--------------[Start: set HasTransaction Status - ShopRentalMixinOrderDelete]---------------
                //Note : modified : 2024/04/09
                if (mixinOrder.ShopId.HasValue && mixinOrder.ShopId != 0)
                {
                    var shBalannces = await _unitOfWork.ShopRentalBalances.GetAllBalanceByShopId(mixinOrder.ShopId.Value);

                    foreach (var sh in shBalannces)
                    {
                        sh.HasTransaction = false; //disable lock
                    }
                }
                //--------------[End: set HasTransaction Status - ShopRentalMixinOrderDelete]-------------------

                mixinOrder.State = OrderStatus.Deleted;
                mixinOrder.UpdatedAt = DateTime.Now;
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task PostOrder(MixinOrder mixinOrder, int cashierid)
        {
            try
            {
                if (mixinOrder.SessionId != 0)
                {
                    #region Audit Log

                    //var note = new StringBuilder();
                    //note.Append("Deleted on ");
                    //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                    //note.Append(" by ");
                    //note.Append("System");

                    //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                    //{
                    //    dateTime = DateTime.Now,
                    //    TransactionID = mixinOrder.ID,
                    //    TransactionName = "MixinOrder",
                    //    User = 1,
                    //    Note = note.ToString()
                    //});
                    //mixinOrder.Active = 0;

                    #endregion
                    mixinOrder.State = OrderStatus.Posted;
                    //mixinOrder.CashierId = cashierid;
                    mixinOrder.UpdatedAt = DateTime.Now;
                    await _unitOfWork.CommitAsync();
                }
            }
            catch (Exception)
            {
            }
            //_unitOfWork.MixinOrders.Remove(mixinOrder);
        }

        public async Task<(bool,string?)> ApproveCancelOrder(MixinCancelOrder mxCancelOrder, HTokenClaim token)
        {
            //using (var dbtransaction = _unitOfWork.BeginTransaction())
            //{
                var cashBookIncomeCategory = CashBookIncomeCategory.Mix;

                try
                {
                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {

                        var mxOrder = await _unitOfWork.MixinOrders.GetForProcessPaymentById(mxCancelOrder.MixinOrderId);

                        var mxCancelOrderToUpdate = await _unitOfWork.MixinCancelOrders.GetByIdAsync(mxCancelOrder.Id);
                        mxCancelOrder.ApprovedBy = token.userId;


                        if (mxOrder != null && mxOrder.State == OrderStatus.Cancel_Pending && mxCancelOrderToUpdate != null)
                        {
                            if (mxOrder.AppCategoryId == 5 && mxOrder.AssessmentId.HasValue && mxOrder.AssessmentId != 0)
                            {

                              var pendingOrdersSameAssessment = await _unitOfWork.MixinOrders.GetForReversePaymentWithPendingBill(mxOrder.AssessmentId.Value);


                            if (pendingOrdersSameAssessment.Count()==1 || (pendingOrdersSameAssessment.Count() > 1 && pendingOrdersSameAssessment.First().Id == mxOrder.Id))
                            {


                                if (!await _assessmentCancelOrderService.ReversePayment(mxOrder.Id, mxOrder.AssessmentId.Value))
                                {
                                    throw new Exception("Unable to Cancel Assessment Order.");

                                }
                            }
                            else
                            {
                                throw new GeneralException("Please Approve Last order First");
                            }
                            }

                            if (mxOrder.BusinessId.HasValue && mxOrder.BusinessId != 0)
                            {
                                var businessTaxes = await _unitOfWork.BusinessTaxes.GetByIdAsync(mxOrder.BusinessId.Value);

                                if (businessTaxes != null)
                                {
                                    businessTaxes.TaxState = TaxStatus.Draft;
                                    businessTaxes.UpdatedBy = mxCancelOrder.ApprovedBy;
                                    businessTaxes.UpdatedAt = System.DateTime.Now;
                                }
                                else
                                {
                                    throw new Exception("Business taxes not found.");
                                }
                            }

                            if (mxOrder.AppCategoryId == 4 && mxOrder.WaterConnectionId.HasValue && mxOrder.WaterConnectionId != 0)
                            {
                                if (!await _waterBillCancelOrderService.ReversePayment(mxOrder.Id, mxOrder.WaterConnectionId.Value, mxCancelOrder.ApprovedBy!.Value))
                                {
                                    throw new Exception("Unable to Cancel WaterBill Order.");
                                }
                            }

                            /*
                            *
                            * update mixin order entity
                            * update mixincancel order entity
                            */

                            //--------------[Start :shoprentalMixOrderApproveCancel]---------------
                            //Note : modified : 2024/04/09
                            if (mxOrder.AppCategoryId == 3 && mxOrder.ShopId.HasValue && mxOrder.ShopId != 0)
                            {
                                cashBookIncomeCategory = CashBookIncomeCategory.ShopRental;
                                if (!await _shopRentalCancelOrderService.ReverseShopRentalPayment(mxOrder.Id, mxOrder.ShopId.Value, mxCancelOrder.ApprovedBy!.Value))
                                {
                                    throw new Exception("Unable to Cancel Shop Rental Order.");

                                }
                            }
                            //--------------[End :shoprentalMixOrderApproveCancel]---------------

                            mxOrder.State = OrderStatus.Cancel_Approved;
                            mxOrder.UpdatedAt = DateTime.Now;

                            mxCancelOrderToUpdate.ApprovedBy = mxCancelOrder.ApprovedBy;
                            mxCancelOrderToUpdate.UpdatedAt = mxCancelOrder.UpdatedAt;
                            mxCancelOrderToUpdate.ApprovalComment = mxCancelOrder.ApprovalComment;

                            await _unitOfWork.CommitAsync();

                            if (token.IsFinalAccountsEnabled == 1)
                            {

                                if (await UpdateVoteBalance(mxOrder, session, CashBookTransactionType.CREDIT, cashBookIncomeCategory, token))
                                {
                                    await _unitOfWork.CommitAsync();
                                }
                                else
                                {
                                    throw new Exception("Vote balance not updated.");
                                }



                                if (await CreateCashBookEntry(mxOrder, CashBookTransactionType.CREDIT, cashBookIncomeCategory, session, token))
                                {
                                    await _unitOfWork.CommitAsync();
                                }
                                else
                                {
                                    throw new Exception("Cashbook entry not created.");
                                }
                            }

                            //dbtransaction.Commit();


                        }
                        return (true,"Cancel approval process successfull");
                    }
                    else
                    {
                        throw new Exception("No Active Session Found.");
                    }
                }
                catch (Exception ex)
                {
                    //dbtransaction.Rollback();
                    return (false, ex.Message);
            }
            //}
            }


        public async Task DisapproveCancelOrder(MixinOrder mixinOrder, int officerid)
        {
            try
            {
                if (mixinOrder.AppCategoryId == 5 && mixinOrder.AssessmentId.HasValue && mixinOrder.AssessmentId != 0)
                {
                    var asmtBals = await _unitOfWork.AssessmentBalances.GetForOrderTransaction(mixinOrder.AssessmentId.Value);

                    asmtBals.HasTransaction = false;

                }

                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Deleted on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = mixinOrder.ID,
                //    TransactionName = "MixinOrder",
                //    User = 1,
                //    Note = note.ToString()
                //});
                //mixinOrder.Active = 0;

                #endregion
                mixinOrder.State = OrderStatus.Cancel_Disapproved;
                //mixinOrder.CashierId = officerid;
                mixinOrder.UpdatedAt = DateTime.Now;
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
            //_unitOfWork.MixinOrders.Remove(mixinOrder);
        }


        public async Task ApproveTradeLicense(MixinOrder mixinOrder, int officerid)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Deleted on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = mixinOrder.ID,
                //    TransactionName = "MixinOrder",
                //    User = 1,
                //    Note = note.ToString()
                //});
                //mixinOrder.Active = 0;

                #endregion
                mixinOrder.TradeLicenseStatus = TradeLicenseStatus.Approved;
                //mixinOrder.CashierId = officerid;
                mixinOrder.UpdatedAt = DateTime.Now;
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
            //_unitOfWork.MixinOrders.Remove(mixinOrder);
        }

        public async Task DiapproveTradeLicense(MixinOrder mixinOrder, int officerid)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Deleted on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = mixinOrder.ID,
                //    TransactionName = "MixinOrder",
                //    User = 1,
                //    Note = note.ToString()
                //});
                //mixinOrder.Active = 0;

                #endregion
                mixinOrder.TradeLicenseStatus = TradeLicenseStatus.Disapproved;
                //mixinOrder.CashierId = officerid;
                mixinOrder.UpdatedAt = DateTime.Now;
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
            //_unitOfWork.MixinOrders.Remove(mixinOrder);
        }

        public async Task updateState(MixinOrder mixinOrder, OrderStatus state)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Deleted on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = mixinOrder.ID,
                //    TransactionName = "MixinOrder",
                //    User = 1,
                //    Note = note.ToString()
                //});
                //mixinOrder.Active = 0;

                #endregion

                mixinOrder.State = state;
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
            //_unitOfWork.MixinOrders.Remove(mixinOrder);
        }



        public async Task UpdateTradeLicenseStatus(MixinOrder mixinOrder, TradeLicenseStatus state)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Deleted on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = mixinOrder.ID,
                //    TransactionName = "MixinOrder",
                //    User = 1,
                //    Note = note.ToString()
                //});
                //mixinOrder.Active = 0;

                #endregion

                mixinOrder.TradeLicenseStatus = state;
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
            //_unitOfWork.MixinOrders.Remove(mixinOrder);
        }


        public async Task<IEnumerable<MixinOrder>> GetAll()
        {
            return await _unitOfWork.MixinOrders.GetAll();
        }

        public async Task<IEnumerable<MixinOrder>> GetAllForOffice(int officeid)
        {
            return await _unitOfWork.MixinOrders.GetAllForOffice(officeid);
        }

        public async Task<(int totalCount, IEnumerable<MixinOrder> list)> GetAllForOfficeAndState(int officeid, OrderStatus state, int pageNumber, int pageSize, string filterKeyWord)
        {


            return await _unitOfWork.MixinOrders.GetAllForOfficeAndState(officeid, state, pageNumber, pageSize, filterKeyWord);
        }
        public async Task<IEnumerable<MixinOrder>> GetAllForOfficeAndState(int officeid, OrderStatus state)
        {
            return await _unitOfWork.MixinOrders.GetAllForOfficeAndState(officeid, state);
        }


        public async Task<IEnumerable<MixinOrder>> GetAllForOfficeAndStateAndDate(int officeid, OrderStatus state, DateTime fordate)
        {
            return await _unitOfWork.MixinOrders.GetAllForOfficeAndStateAndDate(officeid, state, fordate);
        }

        public async Task<IEnumerable<MixinOrder>> GetAllForUserAndState(int userid, OrderStatus state)
        {
            return await _unitOfWork.MixinOrders.GetAllForUserAndState(userid, state);
        }

        public async Task<IEnumerable<MixinOrder>> GetAllForSessionAndState(int sessionid, OrderStatus state)
        {
            return await _unitOfWork.MixinOrders.GetAllForSessionAndState(sessionid, state);
        }


        public async Task<IEnumerable<MixinOrder>> GetAllCashBookForOfficeId(int officeid)
        {
            return await _unitOfWork.MixinOrders.GetAllCashBookForOfficeId(officeid);
        }
        public async Task<IEnumerable<MixinOrder>> GetAllCashBookForOfficeIdBankAccountId(int officeid, int bankaccid)
        {
            return await _unitOfWork.MixinOrders.GetAllCashBookForOfficeIdBankAccountId(officeid, bankaccid);
        }

        public async Task<IEnumerable<MixinOrder>> GetAllTradeTaxOrdersForUserAndState(int userid, OrderStatus state)
        {
            return await _unitOfWork.MixinOrders.GetAllTradeTaxOrdersForUserAndState(userid, state);
        }

        public async Task<IEnumerable<MixinOrder>> GetAllPaidOrdersForOfficeIdBankAccountIdCurrentSession(int officeid, int bankaccid, int sessionid)
        {
            return await _unitOfWork.MixinOrders.GetAllPaidOrdersForOfficeIdBankAccountIdCurrentSession(officeid, bankaccid, sessionid);
        }

        public async Task<IEnumerable<MixinOrder>> GetAllPaidOrdersForOfficeIdCurrentSession(int officeid, int sessionid)
        {
            try
            {
                return await _unitOfWork.MixinOrders.GetAllPaidOrdersForOfficeIdCurrentSession(officeid, sessionid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<MixinOrder>> GetAllReceiptCreatedUsersForOffice(int officeid)
        {
            return await _unitOfWork.MixinOrders.GetAllReceiptCreatedUsersForOffice(officeid);
        }

        public async Task<IEnumerable<MixinOrder>> GetAllReceiptCreatedUsersForSabha(int sabhaid)
        {
            return await _unitOfWork.MixinOrders.GetAllReceiptCreatedUsersForSabha(sabhaid);
        }

        public Task<(int totalCount, IEnumerable<MixinOrder> list)> SearchOrderForAdjesment(List<int?> officeIds, OrderStatus state, int pageNo, int pageSize, string keyword)
        {
            return _unitOfWork.MixinOrders.SearchOrderForAdjesment(officeIds, state, pageNo, pageSize, keyword);
        }
        public Task<(int totalCount, IEnumerable<MixinOrder> list)> SearchOrderByKeyword(List<int?> officeIds, OrderStatus state, int pageNo, int pageSize, string keyword)
        {
            return _unitOfWork.MixinOrders.SearchOrderByKeyword(officeIds, state, pageNo, pageSize, keyword);
        }
        public Task<MixinOrder> GetMixinOrderForRepaymentById(int mxId)
        {
            return _unitOfWork.MixinOrders.GetMixinOrderForRepaymentById(mxId);
        }

        public async Task Update(MixinOrder mixinOrderToBeUpdated, MixinOrder mixinOrder)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (mixinOrderToBeUpdated.NameEnglish != mixinOrder.NameEnglish)
                //    note.Append(" English Name :" + mixinOrderToBeUpdated.NameEnglish + " Change to " + mixinOrder.NameEnglish);
                //if (mixinOrderToBeUpdated.NameSinhala != mixinOrder.NameSinhala)
                //    note.Append(" Sinhala Name :" + mixinOrderToBeUpdated.NameSinhala + " Change to " + mixinOrder.NameSinhala);
                //if (mixinOrderToBeUpdated.NameTamil != mixinOrder.NameTamil)
                //    note.Append(" Tamil Name :" + mixinOrderToBeUpdated.NameTamil + " Change to " + mixinOrder.NameTamil);
                //if (mixinOrderToBeUpdated.Code != mixinOrder.Code)
                //    note.Append(" Code :" + mixinOrderToBeUpdated.Code + " Change to " + mixinOrder.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = mixinOrderToBeUpdated.ID,
                //    TransactionName = "MixinOrder",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                //mixinOrderToBeUpdated.IsActive = 1;
                //mixinOrderToBeUpdated.DateModified = DateTime.Now;
                //mixinOrderToBeUpdated.BankAccountId = mixinOrder.BankAccountId;
                //mixinOrderToBeUpdated.OfficeId = mixinOrder.OfficeId;
                //mixinOrderToBeUpdated.VoteId = mixinOrder.VoteId;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }

        public async Task<IEnumerable<MixinOrder>> GetAllTradeLicensesForOfficeAndState(int officeid, TradeLicenseStatus state)
        {
            return await _unitOfWork.MixinOrders.GetAllTradeLicensesForOfficeAndState(officeid, state);
        }

        public async Task<IEnumerable<MixinOrder>> GetAllTradeLicensesForOfficeAndStateAndTaxType(int officeid, TradeLicenseStatus state, int taxtypeid)
        {
            return await _unitOfWork.MixinOrders.GetAllTradeLicensesForOfficeAndStateAndTaxType(officeid, state, taxtypeid);


        }

        public async Task<IEnumerable<MixinOrder>> PlaceAssessmentOrder(List<MixinOrder> newOrders)
        {
            try
            {

                //newOrders = newOrders.Select((order, index) =>
                //{
                //    order.Code = DateTime.Now.ToString("HHmmssfff") + index.ToString();
                //    return order;
                //}).ToList();

                //var session = await _unitOfWork.Sessions.GetByIdAsync(newOrders.First().SessionId);


                var sessionDate = await _unitOfWork.Sessions.IsRescueSessionThenDate(newOrders.First().SessionId);

                if (sessionDate.HasValue)
                {
                    foreach (var newMixinOrder in newOrders)
                    {

                        newMixinOrder.CreatedAt = (DateTime)sessionDate;

                        foreach (var item in newMixinOrder!.MixinOrderLine)
                        {
                            item.CreatedAt = (DateTime)sessionDate;
                        }
                    }
                }
                else
                {
                    foreach (var newMixinOrder in newOrders)
                    {
                        newMixinOrder.CreatedAt = DateTime.Now;

                        foreach (var item in newMixinOrder!.MixinOrderLine)
                        {
                            item.CreatedAt = DateTime.Now;
                        }
                    }
                }

                List<int?> asseessmntIds = newOrders.Select(mx => mx.AssessmentId).ToList();
                var asmtBals = await _unitOfWork.AssessmentBalances.GetForOrderTransaction(asseessmntIds);

                foreach (var item in asmtBals)
                {
                    item.HasTransaction = true;
                }

                newOrders.ForEach(order => order.Code = DateTime.Now.ToString("HHmmssfff") + newOrders.IndexOf(order));

                await _unitOfWork.MixinOrders.AddRangeAsync(newOrders);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                return new List<MixinOrder>();
            }
            return newOrders;
        }

        public async Task<(bool, string?)> ProcessPayment(int id, int cashierid, HTokenClaim token)
        {
            using (var dbtransaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {
                        //var mxOrder = await GetById(id);

                        var mxOrder = await _unitOfWork.MixinOrders.GetForProcessPaymentById(id);

                        if(mxOrder == null)
                        {
                            throw new Exception("Order not found.");
                        }

                        if((mxOrder.AppCategoryId!=9 && mxOrder.SessionId == session.Id) || (mxOrder.AppCategoryId == 9)) { //Session not check for Journal Entries


                        if ((mxOrder?.State == OrderStatus.Draft || mxOrder?.State == OrderStatus.Journal_Entry_Pending_Approval) && mxOrder.OfficeId.HasValue)
                        {
                            //var office = await _unitOfWork.Offices.GetOfficeByIdWithSabhaDetails(mxOrder.OfficeId.Value);

                            if (!string.IsNullOrEmpty(token.officeCode))
                            {
                                int bankaccountid = mxOrder.AccountDetailId.Value;
                                string prefix = "MIX";
                                if (mxOrder.AppCategoryId == 5)
                                {
                                    prefix = "ASM";
                                    if (token.officeTypeID == 2)
                                    {
                                        bankaccountid = await _voteAssignmentService.GetAssignedBankAccountForSubOffice(token.officeId);
                                        if (bankaccountid != 0 && bankaccountid != null)
                                        {
                                            mxOrder.AccountDetailId = bankaccountid;
                                        }
                                    }
                                }
                                else if (mxOrder.AppCategoryId == 3)
                                {
                                    prefix = "SHP";
                                    if (token.officeTypeID == 2)
                                    {
                                        bankaccountid = await _voteAssignmentService.GetAssignedBankAccountForSubOffice(token.officeId);
                                        if (bankaccountid != 0 && bankaccountid != null)
                                        {
                                            mxOrder.AccountDetailId = bankaccountid;
                                        }
                                    }
                                }
                                else if (mxOrder.AppCategoryId == 2)
                                {
                                    prefix = "TRD";
                                    if (token.officeTypeID == 2)
                                    {
                                        bankaccountid = await _voteAssignmentService.GetAssignedBankAccountForSubOffice(token.officeId);
                                        if (bankaccountid != 0 && bankaccountid != null)
                                        {
                                            mxOrder.AccountDetailId = bankaccountid;
                                        }
                                    }
                                }
                                else if (mxOrder.AppCategoryId == 4)
                                {
                                    prefix = "WTR";
                                    if (token.officeTypeID == 2)
                                    {
                                        bankaccountid = await _voteAssignmentService.GetAssignedBankAccountForSubOffice(token.officeId);
                                        if (bankaccountid != 0 && bankaccountid != null)
                                        {
                                            mxOrder.AccountDetailId = bankaccountid;
                                        }
                                    }
                                }
                                else if (mxOrder.AppCategoryId == 9)
                                {
                                    prefix = "JNL";
                                }
                                else
                                {
                                    prefix = "MIX";
                                }
                                var docSeqNums = await _unitOfWork.DocumentSequenceNumbers
                                    .GetNextSequenceNumberForYearOfficePrefixAsync(mxOrder.CreatedAt.Year, mxOrder.OfficeId.Value, "MIX");

                                if (docSeqNums != null)
                                {
                                    // Update mxOrder properties
                                    mxOrder.State = OrderStatus.Paid;
                                    mxOrder.CashierId = cashierid;
                                    mxOrder.UpdatedAt = DateTime.Now;
                                    mxOrder.Code = $"{token.officeCode}/{DateTime.Now.Year}/{prefix}/{++docSeqNums.LastIndex}";
                                }

                                if(mxOrder.AppCategoryId == 9)
                                {
                                    mxOrder.State = OrderStatus.Posted;
                                }
                            }



                            if (mxOrder.BusinessId.HasValue && mxOrder.BusinessId != 0)
                            {

                                var businessTaxes = await _unitOfWork.BusinessTaxes.GetByIdAsync(mxOrder.BusinessId.Value);

                                if (businessTaxes != null)
                                {
                                    businessTaxes.TaxState = TaxStatus.Paid;
                                    businessTaxes.LicenseNo = mxOrder.Code;
                                    businessTaxes.UpdatedAt = System.DateTime.Now;
                                    businessTaxes.UpdatedBy = cashierid;
                                }
                                else
                                {
                                    throw new Exception("Business taxes not found.");
                                }


                            }

                            if (mxOrder.AssessmentId.HasValue && mxOrder.AssessmentId != 0)
                            {
                                var assessmentBalance = await _unitOfWork.AssessmentBalances.GetByIdToProcessPayment(mxOrder.AssessmentId.Value);
                                int? month = await _unitOfWork.Sessions.GetCurrentSessionMonthForProcess(mxOrder.SessionId);
                                var rates = await _unitOfWork.AssessmentRates.GetByIdAsync(1);

                                if (assessmentBalance != null && assessmentBalance.Q1 != null && assessmentBalance.Q2 != null && assessmentBalance.Q3 != null && assessmentBalance.Q4 != null && month.HasValue && rates != null && assessmentBalance.HasTransaction == true)
                                {
                                    /*****
                                     op => outstandingPayable
                                     payable => payable amount for year
                                     deduction => deduction amount from last year overpayment
                                     paying => paying quantities    
                                     nextbal => next time balance after paying
                                     discount => applied discount
                                     dctRate => discount Rates   
                                     */

                                    (var op, var payable, var deduction, var paying, var nextbal, var discount, var dctRate) = _assessmentBalanceService.CalculatePaymentBalance(assessmentBalance, rates, mxOrder.TotalAmount, month.Value, true);

                                    assessmentBalance.ByExcessDeduction += deduction.Total;
                                    assessmentBalance.Paid += mxOrder.TotalAmount;
                                    assessmentBalance.ExcessPayment = 0;
                                    assessmentBalance.DiscountRate = discount.Total > 0 ? dctRate : assessmentBalance.DiscountRate;
                                    assessmentBalance.Discount += discount.Total;
                                    assessmentBalance.OverPayment += paying.OverPayment += deduction.OverPayment != 0 ? deduction.OverPayment : 0;

                                    assessmentBalance.LYWarrant = nextbal.LYWarrant;
                                    assessmentBalance.LYArrears = nextbal.LYArrears;

                                    assessmentBalance.TYWarrant = nextbal.TYWarrant;
                                    assessmentBalance.TYArrears = nextbal.TYArrears;




                                    if (!assessmentBalance.Q1.IsOver && !assessmentBalance.Q1.IsCompleted && (paying.Q1 != 0 || (paying.Q1 == 0 && discount.Q1 != 0)))

                                    {
                                        assessmentBalance.Q1.ByExcessDeduction += deduction.Q1;
                                        assessmentBalance.Q1.Paid += paying.Q1;
                                        assessmentBalance.Q1.Discount += discount.Q1;
                                        assessmentBalance.Q1.IsCompleted = assessmentBalance.Q1.Amount - assessmentBalance.Q1.ByExcessDeduction - assessmentBalance.Q1.Paid - assessmentBalance.Q1.Discount == 0 ? true : false;
                                    }

                                    if (!assessmentBalance.Q2.IsOver && !assessmentBalance.Q2.IsCompleted && (paying.Q2 != 0 || (paying.Q2 == 0 && discount.Q2 != 0)))
                                    {
                                        assessmentBalance.Q2.ByExcessDeduction += deduction.Q2;
                                        assessmentBalance.Q2.Paid += paying.Q2;
                                        assessmentBalance.Q2.Discount += discount.Q2;
                                        assessmentBalance.Q2.IsCompleted = assessmentBalance.Q2.Amount - assessmentBalance.Q2.ByExcessDeduction - assessmentBalance.Q2.Paid - assessmentBalance.Q2.Discount == 0 ? true : false;
                                    }

                                    if (!assessmentBalance.Q3.IsOver && !assessmentBalance.Q3.IsCompleted && (paying.Q3 != 0 || (paying.Q3 == 0 && discount.Q3 != 0)))
                                    {
                                        assessmentBalance.Q3.ByExcessDeduction += deduction.Q3;
                                        assessmentBalance.Q3.Paid += paying.Q3;
                                        assessmentBalance.Q3.Discount += discount.Q3;
                                        assessmentBalance.Q3.IsCompleted = assessmentBalance.Q3.Amount - assessmentBalance.Q3.ByExcessDeduction - assessmentBalance.Q3.Paid - assessmentBalance.Q3.Discount == 0 ? true : false;
                                    }

                                    if (!assessmentBalance.Q4.IsOver && !assessmentBalance.Q4.IsCompleted && (paying.Q4 != 0 || (paying.Q4 == 0 && discount.Q4 != 0)))
                                    {
                                        assessmentBalance.Q4.ByExcessDeduction += deduction.Q4;
                                        assessmentBalance.Q4.Paid += paying.Q4;
                                        assessmentBalance.Q4.Discount += discount.Q4;
                                        assessmentBalance.Q4.IsCompleted = assessmentBalance.Q4.Amount - assessmentBalance.Q4.ByExcessDeduction - assessmentBalance.Q4.Paid - assessmentBalance.Q4.Discount == 0 ? true : false;
                                    }

                                    if (assessmentBalance.Q1.IsCompleted && assessmentBalance.Q2.IsCompleted && assessmentBalance.Q3.IsCompleted && assessmentBalance.Q4.IsCompleted)
                                    {
                                        assessmentBalance.IsCompleted = true;
                                    }

                                    assessmentBalance.NumberOfPayments += 1;
                                    assessmentBalance.UpdatedBy = cashierid;
                                    assessmentBalance.UpdatedAt = DateTime.Now;
                                    assessmentBalance.HasTransaction = false;




                                    var q1 = (!assessmentBalance.Q1.IsCompleted && !assessmentBalance.Q1.IsOver) ? assessmentBalance.Q1.Amount - assessmentBalance.Q1.ByExcessDeduction - assessmentBalance.Q1.Paid - assessmentBalance.Q1.Discount : 0;
                                    var q2 = (!assessmentBalance.Q2.IsCompleted && !assessmentBalance.Q2.IsOver) ? assessmentBalance.Q2.Amount - assessmentBalance.Q2.ByExcessDeduction - assessmentBalance.Q2.Paid - assessmentBalance.Q2.Discount : 0;
                                    var q3 = (!assessmentBalance.Q3.IsCompleted && !assessmentBalance.Q3.IsOver) ? assessmentBalance.Q3.Amount - assessmentBalance.Q3.ByExcessDeduction - assessmentBalance.Q3.Paid - assessmentBalance.Q3.Discount : 0;
                                    var q4 = (!assessmentBalance.Q4.IsCompleted && !assessmentBalance.Q4.IsOver) ? assessmentBalance.Q4.Amount - assessmentBalance.Q4.ByExcessDeduction - assessmentBalance.Q4.Paid - assessmentBalance.Q4.Discount : 0;


                                    var transaction = new AssessmentTransaction
                                    {
                                        AssessmentId = assessmentBalance.AssessmentId,
                                        //DateTime = DateTime.Now,
                                        Type = AssessmentTransactionsType.Payment,
                                        LYArrears = assessmentBalance.LYArrears,
                                        LYWarrant = assessmentBalance.LYWarrant,

                                        TYArrears = assessmentBalance.TYArrears,
                                        TYWarrant = assessmentBalance.TYWarrant,
                                        RunningOverPay = assessmentBalance.OverPayment,

                                        Q1 = q1,
                                        Q2 = q2,
                                        Q3 = q3,
                                        Q4 = q4,

                                        RunningDiscount = assessmentBalance.Discount,
                                        RunningTotal =
                                        assessmentBalance.LYArrears
                                        + assessmentBalance.LYWarrant
                                        + assessmentBalance.TYArrears
                                        + assessmentBalance.TYWarrant
                                              + q1
                                              + q2
                                              + q3
                                              + q4
                                        - assessmentBalance.OverPayment,
                                        DiscountRate = assessmentBalance.DiscountRate,
                                    };

                                    await _unitOfWork.AssessmentTransactions.AddAsync(transaction);

                                }
                                else
                                {
                                    throw new Exception("unable to update assessment data.");
                                }

                            }

                            //--------------[Start :ProcessShopRentalPayment]---------------
                            //Note : modified : 2024/04/03
                            if (mxOrder.ShopId.HasValue && mxOrder.ShopId != 0)
                            {
                                if (!await _shopRentalProcessPaymentService.ProcessPayment(mxOrder.Id, cashierid, mxOrder.ShopId.Value))
                                {
                                    throw new GeneralException("Unable To Process Shop Rental Payment");
                                }
                            }
                            //--------------[End: ProcessShopRentalPayment]-----------------


                            //await _unitOfWork.CommitAsync();

                            if (token.IsFinalAccountsEnabled == 11 )
                            {
                                if (await UpdateVoteBalance(mxOrder, session, CashBookTransactionType.DEBIT, CashBookIncomeCategory.Mix, token))
                                {
                                    //await _unitOfWork.CommitAsync();
                                }
                                else
                                {
                                    throw new FinalAccountException("Vote Balance Entry Not updated.");
                                }



                                if (await CreateCashBookEntry(mxOrder, CashBookTransactionType.DEBIT, CashBookIncomeCategory.Mix, session, token))
                                {
                                    //await _unitOfWork.CommitAsync();
                                }
                                else
                                {
                                        //           dbtransaction.Rollback();
                                        //  throw new FinalAccountException("Cashbook Entry Not Created.");
                                        return (false, "Cash Book Entry is not created");
                                }
                            }

                                await _unitOfWork.CommitAsync();
                                dbtransaction.Commit();
                                return (true, "Payment Confirmed Successfully");
                        }
                        else
                        {
                           //     dbtransaction.Rollback();
                                throw new FinalAccountException("Order not found.");
                        }
                        }
                        else
                        {
                        //    dbtransaction.Rollback();
                            throw new FinalAccountException("Session Matching Issue");
                            }
                    }
                    else
                    {
                      //  dbtransaction.Rollback();
                        throw new FinalAccountException("Active Session not found.");
                    }
                }
                catch (Exception ex)
                {
                    dbtransaction.Rollback();
                    _logger.LogError(ex, "An error occurred during mixIn payment processing.{EventType}", "MixIn Order");

                return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
                //if (ex.GetType() == typeof(FinalAccountException) || )
                //{
                //    return (false, ex.Message);
                //}
                //else
                //{
                //    return (false, null);
                //}

            }

            }
        }

        public async Task<IEnumerable<MixinOrder>> GetPlacedOrdersByUserByCategoryByState(int userid, int category, OrderStatus state)
        {
            try
            {
                return await _unitOfWork.MixinOrders.GetPlacedOrdersByUserByCategoryByState(userid, category, state);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Casher.{EventType}", "MixIn Order");
                return new List<MixinOrder>();
            }
        }

        public async Task<(int totalCount, IEnumerable<MixinOrder> list)> GetAllPlacedAssessmentOrders(int assessmentId, int pageNumber)
        {
            return await _unitOfWork.MixinOrders.GetAllPlacedAssessmentOrders(assessmentId, pageNumber);
        }

        public async Task<(int totalCount, IEnumerable<MixinOrder> list)> getAllPlacedWaterConnectionOrders(int assessmentId, int pageNumber)
        {
            return await _unitOfWork.MixinOrders.getAllPlacedWaterConnectionOrders(assessmentId, pageNumber);
        }

        public async Task<IEnumerable<Object>> GetAllTotalAmountsByAppCategoryForSession(Session session)
        {
            return await _unitOfWork.MixinOrders.GetAllTotalAmountsByAppCategoryForSession(session);
        }

        public async Task<IEnumerable<Object>> GetAllOnlinePaymentTotalAmountsByAppCategoryForSession(Session session)
        {
            return await _unitOfWork.MixinOrders.GetAllOnlinePaymentTotalAmountsByAppCategoryForSession(session);
        }

        public async Task<MixinOrder> PlaceWaterBillOrdersAndProcessPayments(MixinOrder wbOrder,HTokenClaim token)
        {
            try
            {

                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                if (session != null)
                {

                    var sessionDate = await _unitOfWork.Sessions.IsRescueSessionThenDate(wbOrder.SessionId);

                    if (sessionDate.HasValue)
                    {

                        wbOrder.CreatedAt = (DateTime)sessionDate;

                        foreach (var item in wbOrder!.MixinOrderLine)
                        {
                            item.CreatedAt = (DateTime)sessionDate;
                        }
                    }
                    else
                    {

                        wbOrder.CreatedAt = DateTime.Now;
                        sessionDate = DateTime.Now;
                        foreach (var item in wbOrder!.MixinOrderLine)
                        {
                            item.CreatedAt = DateTime.Now;
                        }

                    }





                    var office = await _unitOfWork.Offices.GetByIdAsync(wbOrder.OfficeId.Value);


                    if (!string.IsNullOrEmpty(office?.Code))
                    {
                        int bankaccountid = wbOrder.AccountDetailId.Value;
                        string prefix = "MIX";
                        if (wbOrder.AppCategoryId == 5)
                        {
                            prefix = "ASM";
                            if (office.OfficeTypeID == 2)
                            {
                                bankaccountid = await _voteAssignmentService.GetAssignedBankAccountForSubOffice(office.ID.Value);
                                if (bankaccountid != 0 && bankaccountid != null)
                                {
                                    wbOrder.AccountDetailId = bankaccountid;
                                }
                            }
                        }
                        else if (wbOrder.AppCategoryId == 3)
                        {
                            prefix = "SHP";
                        }
                        else if (wbOrder.AppCategoryId == 4)
                        {
                            prefix = "WTR";
                        }
                        else
                        {
                            prefix = "MIX";
                        }
                        var docSeqNums = await _unitOfWork.DocumentSequenceNumbers
                            .GetNextSequenceNumberForYearOfficePrefixAsync(wbOrder.CreatedAt.Year, wbOrder.OfficeId.Value, "MIX");

                        if (docSeqNums != null)
                        {

                            // Update mxOrder properties
                            wbOrder.State = OrderStatus.Paid;
                            //mxOrder.CashierId = cashierid;
                            wbOrder.UpdatedAt = DateTime.Now;
                            wbOrder.Code = $"{office.Code}/{DateTime.Now.Year}/{prefix}/{++docSeqNums.LastIndex}";
                        }
                    }



                    await _unitOfWork.MixinOrders.AddAsync(wbOrder);
                    var x = await _waterConnectionBalanceService.CalculatePayments(wbOrder.WaterConnectionId!.Value, sessionDate.Value, wbOrder.TotalAmount, true, false, wbOrder.CreatedBy);


                    if (token.IsFinalAccountsEnabled == 1 )
                    {
                        if (await UpdateVoteBalance(wbOrder, session, CashBookTransactionType.DEBIT, CashBookIncomeCategory.Mix, token))
                        {
                            //await _unitOfWork.CommitAsync();
                        }
                        else
                        {
                            throw new FinalAccountException("Vote Balance Entry Not updated.");
                        }



                        if (await CreateCashBookEntry(wbOrder, CashBookTransactionType.DEBIT, CashBookIncomeCategory.Mix, session, token))
                        {
                            //await _unitOfWork.CommitAsync();
                        }
                        else
                        {
                            throw new FinalAccountException("Cashbook Entry Not Created.");
                        }
                    }

                    await _unitOfWork.CommitAsync();
                    return wbOrder;
                }
                else
                {
                    throw new FinalAccountException("Active Session not found.");
                }
            }
            catch (Exception ex)
            {
                return new MixinOrder();

            }

        }
        //public async Task<IEnumerable<MixinOrder>> GetAllForVoteId(int id)
        //    {
        //        return await _unitOfWork.MixinOrders.GetAllForVoteId(id);
        //    }


        //--------------[Start - placeShopRentalOrder]--------------------------------------------
        //Note : modified : 2024/04/03
        public async Task<MixinOrder> PlaceShopRentalOrder(MixinOrder newMixinShopRentalOrder)
        {
            try
            {
                if (newMixinShopRentalOrder.SessionId != 0)
                {
                    var activesession = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(newMixinShopRentalOrder.OfficeId);

                    if (activesession.Rescue == 1)
                    {
                        newMixinShopRentalOrder.CreatedAt = activesession.StartAt;
                    }
                    else
                    {
                        newMixinShopRentalOrder.CreatedAt = DateTime.Now;
                    }

                    //--- [Start: set HasTransaction Status - ShopRentalBalance]----------------------------
                    //Note : modified : 2024/04/09
                    var shBalannces = await _unitOfWork.ShopRentalBalances.GetAllBalanceByShopId(newMixinShopRentalOrder.ShopId.Value);

                    foreach (var sh in shBalannces)
                    {
                        sh.HasTransaction = true; //enable lock
                    }
                    //--- [End: set HasTransaction Status - ShopRentalBalance]------------------------------

                    newMixinShopRentalOrder.Code = DateTime.Now.ToString("HHmmssfff") + new Random().Next(10);
                    await _unitOfWork.MixinOrders.AddAsync(newMixinShopRentalOrder);

                    await _unitOfWork.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                return new MixinOrder();
            }

            return newMixinShopRentalOrder;
        }
        //--------------[End - placeShopRentalOrder]----------------------------------------------


        //shop payment history
        public async Task<IEnumerable<MixinOrder>> GetPaidPostedOrdersByShopId(OrderStatus orderstate, int shopId)
        {
            return await _unitOfWork.MixinOrders.GetPaidPostedOrdersByShopId(orderstate, shopId);
        }
        //----

        private async Task<bool> UpdateVoteBalance(MixinOrder mx, Session session, CashBookTransactionType transactionType, CashBookIncomeCategory incomeCategory, HTokenClaim token)

        {
            try
            {
                if (mx.MixinOrderLine.Count == 0)
                {
                    throw new FinalAccountException("No Order Line Found.");
                }


                foreach (var item in mx.MixinOrderLine)
                {


                    if (item.StampAmount > 0)
                    {
                        var stampLedgerAccount = await _unitOfWork.SpecialLedgerAccounts.GetStampLedgerAccount(token.sabhaId);

                        var stampVoteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(stampLedgerAccount.VoteId!.Value, token.sabhaId, session.StartAt.Year);

                        if (stampVoteBalance == null)
                        {
                            stampVoteBalance = await _mixVoteBalanceService.CreateNewVoteBalance(stampLedgerAccount.VoteId!.Value, session, token);

                            if (stampVoteBalance == null)
                            {
                                throw new FinalAccountException("Unable to Create Stamp Ledger Account Entry");
                            }


                        }


                        if (stampVoteBalance != null && stampVoteBalance.Id.HasValue)
                        {
                            stampVoteBalance.UpdatedAt = session.StartAt;
                            stampVoteBalance.UpdatedBy = token.userId;
                            stampVoteBalance.SystemActionAt = DateTime.Now;
                            stampVoteBalance.OfficeId = token.officeId;
                            stampVoteBalance.ExchangedAmount = (decimal)item.StampAmount!;
                            stampVoteBalance.SessionIdByOffice = session.Id;


                            if (transactionType == CashBookTransactionType.DEBIT)
                            {
                                stampVoteBalance.TransactionType = VoteBalanceTransactionTypes.Income;
                                stampVoteBalance.SubCode = mx.Code;


                                if (stampVoteBalance.ClassificationId == 1)
                                {
                                    stampVoteBalance.Debit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else if (stampVoteBalance.ClassificationId == 2)
                                {
                                    stampVoteBalance.Credit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else if (stampVoteBalance.ClassificationId == 3)
                                {
                                    stampVoteBalance.Credit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else if (stampVoteBalance.ClassificationId == 4)
                                {
                                    stampVoteBalance.Credit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Credit - stampVoteBalance.Debit;

                                }
                                else if (stampVoteBalance.ClassificationId == 5)
                                {
                                    stampVoteBalance.Debit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else
                                {
                                    throw new FinalAccountException("Ledger Account Classification Not Found.");
                                }

                            }



                            else if (transactionType == CashBookTransactionType.CREDIT)
                            {
                                stampVoteBalance.TransactionType = VoteBalanceTransactionTypes.ReverseIncome;

                                if (stampVoteBalance.ClassificationId == 1)
                                {
                                    stampVoteBalance.Credit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else if (stampVoteBalance.ClassificationId == 2)
                                {
                                    stampVoteBalance.Debit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else if (stampVoteBalance.ClassificationId == 3)
                                {
                                    stampVoteBalance.Debit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else if (stampVoteBalance.ClassificationId == 4)
                                {
                                    stampVoteBalance.Debit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Credit - stampVoteBalance.Debit;

                                }
                                else if (stampVoteBalance.ClassificationId == 5)
                                {
                                    stampVoteBalance.Credit += (decimal)item.Amount!;
                                    stampVoteBalance.CreditDebitRunningBalance = stampVoteBalance.Debit - stampVoteBalance.Credit;

                                }
                                else
                                {
                                    throw new FinalAccountException("Ledger Account Classification Not Found.");
                                }
                            }




                        }
                        else
                        {
                            throw new FinalAccountException("Vote stamp ledger balance not found.");
                        }
                    }


                    var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(item.VoteDetailId!.Value, token.sabhaId, session.StartAt.Year);

                    if (voteBalance == null)
                    {
                        voteBalance = await _mixVoteBalanceService.CreateNewVoteBalance(item.VoteDetailId!.Value, session, token);

                        if (voteBalance == null)
                        {
                            throw new FinalAccountException("Unable to Create Vote Balance Entry");
                        }


                    }


                    if (voteBalance != null && voteBalance.Id.HasValue)
                    {
                        voteBalance.UpdatedAt = session.StartAt;
                        voteBalance.UpdatedBy = token.userId;


                        if (transactionType == CashBookTransactionType.DEBIT)
                        {
                            voteBalance.TransactionType = VoteBalanceTransactionTypes.Income;
                            voteBalance.SubCode = mx.Code;


                            if (voteBalance.ClassificationId == 1)
                            {
                                voteBalance.Credit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;

                            }
                            else if (voteBalance.ClassificationId == 2)
                            {
                                voteBalance.Credit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                            }
                            else if (voteBalance.ClassificationId == 3)
                            {
                                voteBalance.Credit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                            }
                            else if (voteBalance.ClassificationId == 4)
                            {
                                voteBalance.Credit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;

                            }
                            else if (voteBalance.ClassificationId == 5)
                            {
                                voteBalance.Debit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                            }
                            else
                            {
                                throw new FinalAccountException("Ledger Account Classification Not Found.");
                            }

                        }
                        


                        else if (transactionType == CashBookTransactionType.CREDIT)
                        {
                            voteBalance.TransactionType = VoteBalanceTransactionTypes.ReverseIncome;

                            if (voteBalance.ClassificationId == 1)
                            {
                                voteBalance.Debit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;

                            }
                            else if (voteBalance.ClassificationId == 2)
                            {
                                voteBalance.Debit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                            }
                            else if (voteBalance.ClassificationId == 3)
                            {
                                voteBalance.Debit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                            }
                            else if (voteBalance.ClassificationId == 4)
                            {
                                voteBalance.Debit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Credit - voteBalance.Debit;

                            }
                            else if (voteBalance.ClassificationId == 5)
                            {
                                voteBalance.Credit += (decimal)item.Amount!;
                                voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;

                            }
                            else
                            {
                                throw new FinalAccountException("Ledger Account Classification Not Found.");
                            }
                        }


                        if(voteBalance.ClassificationId == 2)
                        {
                            voteBalance.RunningBalance = (decimal)voteBalance.AllocationAmount! +  voteBalance.Debit - voteBalance.Credit;
                        }

                        voteBalance.ExchangedAmount = (decimal)item.Amount!;

                        voteBalance.OfficeId = token.officeId;
                        voteBalance.UpdatedBy = token.userId;
                        voteBalance.UpdatedAt = session.StartAt;
                        voteBalance.SystemActionAt = DateTime.Now;

                        var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                        await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);


                        if (transactionType == CashBookTransactionType.DEBIT && item.ClassificationId == 4)
                        {



                            var IncomeSubtitleId = await _unitOfWork.VoteDetails.IsDepositVote(voteBalance.VoteDetailId.Value);

                            if (IncomeSubtitleId.HasValue)
                            {

                                var newDeposit = new Deposit
                                {
                                    DepositSubCategoryId = IncomeSubtitleId.Value,
                                    InitialDepositAmount = (decimal)item.Amount!,
                                    DepositDate = session.StartAt,
                                    Description = item.Description,
                                    CreatedAt = session.StartAt,
                                    CreatedBy = token.userId,
                                    SabhaId = token.sabhaId,
                                    OfficeId = token.officeId,
                                    LedgerAccountId = voteBalance.VoteDetailId.Value!,
                                    MixOrderId = mx.Id,
                                    MixOrderLineId = item.Id,
                                    ReceiptNo = mx.Code,
                                    IsEditable = false,
                                    PartnerId = mx.PartnerId!.Value,
                                    SystemCreateAt = DateTime.Now,

                                };

                                await _unitOfWork.Deposits.AddAsync(newDeposit);

                            }
                        }

                        //else if(transactionType == CashBookTransactionType.CREDIT)
                        //{
                        //    var IncomeSubtitleId = await _unitOfWork.VoteDetails.IsDepositVote(voteBalance.VoteDetailId.Value);

                        //    if (IncomeSubtitleId.HasValue)
                        //    {



                        //    }
                        //}


                    }
                    else
                    {
                        throw new FinalAccountException("Vote balance not found.");
                    }



                }

                if (transactionType == CashBookTransactionType.DEBIT)
                {
                    var depostis = await _unitOfWork.Deposits.ClearDepots(mx.Id);

                    foreach (var item in depostis)
                    {
                        item.Status = 0;
                        item.UpdatedAt = session.StartAt;
                        item.UpdatedBy = token.userId;
                        item.SystemUpdateAt = DateTime.Now;

                    }
                }



                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private async Task<bool> CreateCashBookEntry(MixinOrder mx, CashBookTransactionType transactionType, CashBookIncomeCategory incomeCategory, Session session, HTokenClaim token)
        {

            try
            {
                var account = await _unitOfWork.AccountDetails.GetByIdAsync(mx.AccountDetailId!.Value);
                //var session = await _unitOfWork.Sessions.GetByIdAsync(mx.SessionId);

                if (account == null)
                {
                    throw new FinalAccountException("Account not found.");
                }

                if (transactionType == CashBookTransactionType.DEBIT)
                {
                    account.RunningBalance += mx.TotalAmount;
                }
                else if (transactionType == CashBookTransactionType.CREDIT)
                {

                    account.RunningBalance -= mx.TotalAmount;
                }
                var cashbook = new CashBook
                {
                    //Id
                    SabhaId = token.sabhaId,
                    OfiiceId = mx.OfficeId!.Value,
                    SessionId = mx.SessionId,
                    BankAccountId = mx.AccountDetailId.Value,
                    Date = mx.CreatedAt,

                    TransactionType = transactionType,
                    IncomeCategory = incomeCategory,
                    Code = mx.Code,
                    IncomeItemId = mx.Id,


                    CreatedAt = DateTime.Now,
                    CreatedBy = token.userId,
                    RunningTotal = account.RunningBalance,


                };


                if (!account.VoteId.HasValue) {

                    throw new GeneralException("Assign Ledger Account That Related To Bank Account");
                
                }
                var voteBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(account.VoteId!.Value, token.sabhaId, session.StartAt.Year);

                if (voteBalance != null)
                {

                    if (transactionType == CashBookTransactionType.DEBIT)
                    {
                        voteBalance.TransactionType = VoteBalanceTransactionTypes.Income;
                        voteBalance.Debit += mx.TotalAmount;
                        voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                    }
                    else if (transactionType == CashBookTransactionType.CREDIT)
                    {
                        voteBalance.TransactionType = VoteBalanceTransactionTypes.ReverseIncome;
                        voteBalance.Credit += mx.TotalAmount;
                        voteBalance.CreditDebitRunningBalance = voteBalance.Debit - voteBalance.Credit;
                    }


                    voteBalance.ExchangedAmount = mx.TotalAmount;

                    voteBalance.OfficeId = token.officeId;
                    var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalance);
                    await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                }
                else
                {
                    return false;
                    throw new Exception("Unable to Find Vote Balance");
                }



                //1 = cash
                //2 = cheque
                //3 cross
                //4 = direct
                if (mx.PaymentMethodId == 1)
                {
                    cashbook.CashAmount = mx.TotalAmount;
                    cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Cash;
                }
                else if (mx.PaymentMethodId == 2)
                {
                    cashbook.ChequeAmount = mx.TotalAmount;
                    cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Cheque;
                    cashbook.ChequeNo = mx.ChequeNumber;

                }
                else if (mx.PaymentMethodId == 3)
                {
                    cashbook.CrossAmount = mx.TotalAmount;
                    cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Cross;
                }
                else if (mx.PaymentMethodId == 4)
                {
                    cashbook.DirectAmount = mx.TotalAmount;
                    cashbook.IncomeExpenseMethod = CashBookIncomeExpenseMethod.Direct;
                }


                await _unitOfWork.CashBook.AddAsync(cashbook);

                return true;

            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public async Task<(bool, string?, MixinOrder)> PlaceVoteSurchargeOrderAndProcessPayments(MixinOrder scOrder, HTokenClaim token)
        {
            //using (var dbtransaction = _unitOfWork.BeginTransaction())
            //{
                try
                {
                    var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                    if (session != null)
                    {


                        var sessionDate = await _unitOfWork.Sessions.IsRescueSessionThenDate(session.Id);

                        if (sessionDate.HasValue)
                        {

                            scOrder.CreatedAt = (DateTime)sessionDate;

                            foreach (var item in scOrder!.MixinOrderLine)
                            {
                                item.CreatedAt = (DateTime)sessionDate;
                            }
                        }
                        else
                        {

                            scOrder.CreatedAt = DateTime.Now;
                            sessionDate = DateTime.Now;
                            foreach (var item in scOrder!.MixinOrderLine)
                            {
                                item.CreatedAt = DateTime.Now;
                            }
                        }


                        foreach (var item in scOrder!.MixinOrderLine)
                        { 
                            var voteAssignment = await _unitOfWork.VoteAssignments.GetByVoteId(item.VoteDetailId!.Value, token);

                        if (voteAssignment != null)
                        {
                            var customVote = await _unitOfWork.VoteAssignmentDetails.GetByAssignmentId(voteAssignment.Id);

                            if (customVote != null)
                            {
                                item.MixinVoteAssignmentDetailId = customVote.Id;
                                item.CustomVoteName = customVote.CustomVoteName;
                            }
                            else
                            {
                                throw new FinalAccountException("Vote Assignment Detail not found.");
                            }
                        }
                        else
                        {
                            throw new FinalAccountException("Vote Assignment not found.");
                        }


                    }





                        var docSeqNums = await _unitOfWork.DocumentSequenceNumbers
                        .GetNextSequenceNumberForYearOfficePrefixAsync(scOrder.CreatedAt.Year, scOrder.OfficeId.Value, "MIX");

                        if (docSeqNums != null)
                        {
                            scOrder.SessionId = session.Id;
                            scOrder.AppCategoryId = 8;
                            scOrder.State = OrderStatus.Paid;
                            scOrder.CashierId = token.userId;
                            scOrder.UpdatedAt = DateTime.Now;
                            scOrder.Code = $"{token.officeCode}/{DateTime.Now.Year}/{"SRC"}/{++docSeqNums.LastIndex}";
                        }



                        await _unitOfWork.MixinOrders.AddAsync(scOrder);
                        await _unitOfWork.CommitAsync();


                        if (await UpdateVoteBalance(scOrder, session, CashBookTransactionType.DEBIT, CashBookIncomeCategory.Surcharge, token))
                        {
                            await _unitOfWork.CommitAsync();
                        }
                        else
                        {
                            throw new FinalAccountException("Vote Balance Entry Not updated.");
                        }



                        if (await CreateCashBookEntry(scOrder, CashBookTransactionType.DEBIT, CashBookIncomeCategory.Surcharge, session, token))
                        {
                            await _unitOfWork.CommitAsync();
                        }
                        else
                        {
                            throw new FinalAccountException("Cashbook Entry Not Created.");
                        }



                        //dbtransaction.Commit();

                        return (true, "Surcharge Order  Payment SuccessFull ", scOrder);
                    }
                    else
                    {
                        throw new FinalAccountException("Active Session not found.");
                    }
                }
                catch (Exception ex)
                {
                    //dbtransaction.Rollback();
                    _logger.LogError(ex, "An error occurred during mixIn payment processing.{EventType}", "MixIn Order");


                    if (ex.GetType() == typeof(FinalAccountException))
                    {
                        return (false, ex.Message,new MixinOrder());
                    }
                    else
                    {
                        return (false, null,new MixinOrder());
                    }
                }
            }

        public async Task<IEnumerable<MixinOrder>> GetAllForEmployeeId(int empId)
        {
            return await _unitOfWork.MixinOrders.GetAllForEmployeeId(empId);
        }
    }
    //}
}