using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Models.Common;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Resources;
using System.Transactions;
using CAT20.Common.Envelop;
using CAT20.Core.Models.Enums;

namespace CAT20.Services.Mixin
{

    public class BankingService : IBankingService
    {
        private readonly IMixinUnitOfWork _unitOfWork;
        public BankingService(IMixinUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Banking> GetById(int id)
        {
            return await _unitOfWork.Bankings.GetById(id);
        }

        public async Task<Banking> Create(Banking newBanking)
        {
            try
            {
                await _unitOfWork.Bankings
                .AddAsync(newBanking);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newBanking.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newBanking.ID,
                //    TransactionName = "Banking",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message.ToString());
            }
            return newBanking;
        }

        public async Task<Banking> GetLastBankingDateForOfficeId(int officeid)
        {
            return await _unitOfWork.Bankings.GetLastBankingDateForOfficeId(officeid);
        }

        //public async Task<Banking> GetByCode(string code, int officeId)
        //{
        //    return await _unitOfWork.Bankings.GetByCode(code, officeId);
        //}


        //public TransferObject<Banking> Save(Banking mixinOrder, SequenceNumber sequenceNumber, List<BankingLine> mixinOrderLineList)
        //{
        //    var saveRequestItemList = new List<BankingLine>();
        //    foreach (var item in mixinOrderLineList)
        //    {
        //        var mixinOrderLine = BizObjectFactory.GetBankingLineBO().GetProxy(item.ID.Value);
        //        mixinOrderLine.Ordered = true;
        //        mixinOrderLine.State = Models.Interfaces.State.Modified;
        //        mixinOrderLine.User = mixinOrder.User;
        //        mixinOrderLine.DateModified = DateTime.Now;
        //        saveRequestItemList.Add(mixinOrderLine);
        //    }

        //    if (mixinOrder.ID == null)
        //    {
        //        mixinOrder.TxHistoryList = new List<BankingTxHistory>();

        //        // Tx History record
        //        var txHistory = new BankingTxHistory();
        //        txHistory.Notes = "Request created on " + mixinOrder.RequestedDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " by " + mixinOrder.User + " on behalf of " + mixinOrder.RequestedBy + " - Amount : " + mixinOrder.AmountAfterTax.Value.ToString("N");
        //        txHistory.State = Models.Interfaces.State.Added;
        //        txHistory.User = mixinOrder.User;
        //        mixinOrder.TxHistoryList.Add(txHistory);
        //    }
        //    else
        //    {
        //        // Tx History record
        //        var txHistory = new BankingTxHistory();
        //        txHistory.Notes = "Request altered on " + mixinOrder.RequestedDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " by " + mixinOrder.User + " on behalf of " + mixinOrder.RequestedBy + " - Amount : " + mixinOrder.AmountAfterTax.Value.ToString("N");
        //        txHistory.State = Models.Interfaces.State.Added;
        //        txHistory.User = mixinOrder.User;
        //        mixinOrder.TxHistoryList.Add(txHistory);

        //        // Update Purchase Request record(s)
        //        var dbBankingItems = BizObjectFactory.GetBankingItemBO().GetAllFor(mixinOrder);
        //        foreach (var item in dbBankingItems)
        //        {
        //            var reqItem = mixinOrder.ItemList.FirstOrDefault(t => t.ID == item.ID);
        //            if (item.PRItemID == null)
        //            {
        //                if (reqItem == null)
        //                {
        //                    // Manage deleted list
        //                    item.State = Models.Interfaces.State.Deleted;
        //                    mixinOrder.ItemList.Add(item);
        //                }
        //            }
        //            else
        //            {
        //                if (reqItem == null)
        //                {
        //                    // Manage deleted list
        //                    item.State = Models.Interfaces.State.Deleted;
        //                    mixinOrder.ItemList.Add(item);

        //                    var mixinOrderLine = BizObjectFactory.GetBankingLineBO().GetProxy(item.PRItemID.Value);
        //                    mixinOrderLine.Ordered = false;
        //                    mixinOrderLine.State = Models.Interfaces.State.Modified;
        //                    mixinOrderLine.User = mixinOrder.User;
        //                    mixinOrderLine.DateModified = DateTime.Now;
        //                    saveRequestItemList.Add(mixinOrderLine);
        //                }
        //            }
        //        }
        //    }

        //    AuditTrail auditTrail = null;
        //    if (mixinOrder.ID == null)
        //        auditTrail = Resources.Utility.CreateAuditTrail(null, mixinOrder, Models.Enums.AuditTrailAction.Insert, new List<string>(), 0);
        //    else
        //    {
        //        var childListNames = new List<string>();
        //        childListNames.Add("ItemList");
        //        childListNames.Add("TaxCodeList");

        //        var dbBanking = BizObjectFactory.GetBankingBO().GetSingle(mixinOrder.ID.Value);
        //        auditTrail = Resources.Utility.CreateAuditTrail(dbBanking, mixinOrder, Models.Enums.AuditTrailAction.Update, childListNames, 0);
        //    }

        //    EmailOutBox emailOutBox = null;
        //    var transferObject = new TransferObject<Banking>();
        //    var options = new TransactionOptions();
        //    options.Timeout = TimeSpan.FromMinutes(1);
        //    options.IsolationLevel = IsolationLevel.ReadCommitted;
        //    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
        //    {
        //        transferObject = BizObjectFactory.GetBankingBO().Save(mixinOrder);
        //        if (transferObject.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
        //            return transferObject;

        //        if (sequenceNumber != null)
        //        {
        //            var sequenceNumberTO = BizObjectFactory.GetSequenceNumberBO().Save(sequenceNumber);
        //            if (sequenceNumberTO.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
        //            {
        //                transferObject.StatusInfo = sequenceNumberTO.StatusInfo;
        //                return transferObject;
        //            }
        //        }

        //        var auditTO = BizObjectFactory.GetAuditTrailBO().Save(auditTrail);
        //        if (auditTO.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
        //        {
        //            transferObject.StatusInfo = auditTO.StatusInfo;
        //            return transferObject;
        //        }

        //        foreach (var item in saveRequestItemList)
        //        {
        //            var prTO = BizObjectFactory.GetBankingLineBO().Save(item);
        //            if (prTO.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
        //            {
        //                transferObject.StatusInfo = prTO.StatusInfo;
        //                return transferObject;
        //            }
        //        }

        //        emailOutBox = GenerateEmailItem(mixinOrder, websettings.FunctionURLs);
        //        var emailTO = BizObjectFactory.GetEmailOutBoxBO().Save(emailOutBox);
        //        if (emailTO.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
        //        {
        //            transferObject.StatusInfo = emailTO.StatusInfo;
        //            return transferObject;
        //        }
        //        scope.Complete();
        //    }
        //    return transferObject;
        //}



        //public async Task Cancel(Banking mixinOrder)
        //{
        //    try
        //    {
        //        #region Audit Log

        //        //var note = new StringBuilder();
        //        //note.Append("Deleted on ");
        //        //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
        //        //note.Append(" by ");
        //        //note.Append("System");

        //        //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
        //        //{
        //        //    dateTime = DateTime.Now,
        //        //    TransactionID = mixinOrder.ID,
        //        //    TransactionName = "Banking",
        //        //    User = 1,
        //        //    Note = note.ToString()
        //        //});
        //        //mixinOrder.Active = 0;

        //        #endregion
        //        mixinOrder.State = OrderStatus.Cancel_Pending;
        //        mixinOrder.UpdatedAt = DateTime.Now;
        //        await _unitOfWork.CommitAsync();
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    //_unitOfWork.Bankings.Remove(mixinOrder);
        //}


        //public async Task<IEnumerable<Banking>> GetAll()
        //{
        //    return await _unitOfWork.Bankings.GetAll();
        //}

        //public async Task<IEnumerable<Banking>> GetAllForOffice(int officeid)
        //{
        //    return await _unitOfWork.Bankings.GetAllForOffice(officeid);
        //}

        //public async Task<IEnumerable<Banking>> GetAllForOfficeAndState(int officeid, OrderStatus state)
        //{
        //    return await _unitOfWork.Bankings.GetAllForOfficeAndState(officeid, state);
        //}

        //public async Task<IEnumerable<Banking>> GetAllForOfficeAndStateAndDate(int officeid, OrderStatus state, DateTime fordate)
        //{
        //    return await _unitOfWork.Bankings.GetAllForOfficeAndStateAndDate(officeid, state, fordate);
        //}

        //public async Task<IEnumerable<Banking>> GetAllForUserAndState(int userid, OrderStatus state)
        //{
        //    return await _unitOfWork.Bankings.GetAllForUserAndState(userid, state);
        //}

        //public async Task<IEnumerable<Banking>> GetAllForSessionAndState(int sessionid, OrderStatus state)
        //{
        //    return await _unitOfWork.Bankings.GetAllForSessionAndState(sessionid, state);
        //}

        //public async Task Update(Banking mixinOrderToBeUpdated, Banking mixinOrder)
        //{
        //    try
        //    {
        //        #region Audit Log

        //        //var note = new StringBuilder();
        //        //note.Append("Edited on ");
        //        //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
        //        //note.Append(" by ");
        //        //note.Append(" System.");

        //        //if (mixinOrderToBeUpdated.NameEnglish != mixinOrder.NameEnglish)
        //        //    note.Append(" English Name :" + mixinOrderToBeUpdated.NameEnglish + " Change to " + mixinOrder.NameEnglish);
        //        //if (mixinOrderToBeUpdated.NameSinhala != mixinOrder.NameSinhala)
        //        //    note.Append(" Sinhala Name :" + mixinOrderToBeUpdated.NameSinhala + " Change to " + mixinOrder.NameSinhala);
        //        //if (mixinOrderToBeUpdated.NameTamil != mixinOrder.NameTamil)
        //        //    note.Append(" Tamil Name :" + mixinOrderToBeUpdated.NameTamil + " Change to " + mixinOrder.NameTamil);
        //        //if (mixinOrderToBeUpdated.Code != mixinOrder.Code)
        //        //    note.Append(" Code :" + mixinOrderToBeUpdated.Code + " Change to " + mixinOrder.Code);

        //        //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
        //        //{
        //        //    dateTime = DateTime.Now,
        //        //    TransactionID = mixinOrderToBeUpdated.ID,
        //        //    TransactionName = "Banking",
        //        //    User = 1,
        //        //    Note = note.ToString()
        //        //});

        //        #endregion

        //        //mixinOrderToBeUpdated.IsActive = 1;
        //        //mixinOrderToBeUpdated.DateModified = DateTime.Now;
        //        //mixinOrderToBeUpdated.BankAccountId = mixinOrder.BankAccountId;
        //        //mixinOrderToBeUpdated.OfficeId = mixinOrder.OfficeId;
        //        //mixinOrderToBeUpdated.VoteId = mixinOrder.VoteId;

        //        await _unitOfWork.CommitAsync();
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //public async Task<IEnumerable<Banking>> GetAllForVoteId(int id)
        //    {
        //        return await _unitOfWork.Bankings.GetAllForVoteId(id);
        //    }

    }
}