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

namespace CAT20.Services.Mixin
{
    public class MixinCancelOrderService : IMixinCancelOrderService
    {
        private readonly IMixinUnitOfWork _unitOfWork;
        public MixinCancelOrderService(IMixinUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<MixinCancelOrder> Create(MixinCancelOrder newMixinCancelOrder)
        {
            try
            {
                await _unitOfWork.MixinCancelOrders
                .AddAsync(newMixinCancelOrder);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newMixinCancelOrder.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newMixinCancelOrder.ID,
                //    TransactionName = "MixinCancelOrder",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                //await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }

            return newMixinCancelOrder;
        }
        public async Task Delete(MixinCancelOrder mixinCancelOrder)
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
                //    TransactionID = mixinCancelOrder.ID,
                //    TransactionName = "MixinCancelOrder",
                //    User = 1,
                //    Note = note.ToString()
                //});
                //mixinCancelOrder.Active = 0;

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
            //_unitOfWork.MixinCancelOrders.Remove(mixinCancelOrder);
        }
        //public async Task<IEnumerable<MixinCancelOrder>> GetAllForSabhaId(int sabhaid)
        //{
        //    return await _unitOfWork.MixinCancelOrders.GetAllForSabhaId(sabhaid);
        //}

        //public async Task<IEnumerable<MixinCancelOrder>> GetAllForOfficeId(int officeid)
        //{
        //    return await _unitOfWork.MixinCancelOrders.GetAllForOfficeId(officeid);
        //}

        public async Task<MixinCancelOrder> GetById(int id)
        {
            return await _unitOfWork.MixinCancelOrders.GetById(id);
        }
        public async Task Update(MixinCancelOrder mixinCancelOrderToBeUpdated, MixinCancelOrder mixinCancelOrder)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (mixinCancelOrderToBeUpdated.NameEnglish != mixinCancelOrder.NameEnglish)
                //    note.Append(" English Name :" + mixinCancelOrderToBeUpdated.NameEnglish + " Change to " + mixinCancelOrder.NameEnglish);
                //if (mixinCancelOrderToBeUpdated.NameSinhala != mixinCancelOrder.NameSinhala)
                //    note.Append(" Sinhala Name :" + mixinCancelOrderToBeUpdated.NameSinhala + " Change to " + mixinCancelOrder.NameSinhala);
                //if (mixinCancelOrderToBeUpdated.NameTamil != mixinCancelOrder.NameTamil)
                //    note.Append(" Tamil Name :" + mixinCancelOrderToBeUpdated.NameTamil + " Change to " + mixinCancelOrder.NameTamil);
                //if (mixinCancelOrderToBeUpdated.Code != mixinCancelOrder.Code)
                //    note.Append(" Code :" + mixinCancelOrderToBeUpdated.Code + " Change to " + mixinCancelOrder.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = mixinCancelOrderToBeUpdated.ID,
                //    TransactionName = "MixinCancelOrder",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                mixinCancelOrderToBeUpdated.ApprovedBy = mixinCancelOrder.ApprovedBy;
                mixinCancelOrderToBeUpdated.UpdatedAt = mixinCancelOrder.UpdatedAt;
                mixinCancelOrderToBeUpdated.ApprovalComment = mixinCancelOrder.ApprovalComment;

                //mixinCancelOrderToBeUpdated.IsActive = 1;
                //mixinCancelOrderToBeUpdated.DateModified = DateTime.Now;
                //mixinCancelOrderToBeUpdated.BankAccountId = mixinCancelOrder.BankAccountId;
                //mixinCancelOrderToBeUpdated.OfficeId = mixinCancelOrder.OfficeId;
                //mixinCancelOrderToBeUpdated.VoteId = mixinCancelOrder.VoteId;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }

        public async Task<MixinCancelOrder> GetByOrderId(int id)
        {
            return await _unitOfWork.MixinCancelOrders.GetByOrderId(id);
        }
        //public async Task<IEnumerable<MixinCancelOrder>> GetAllForVoteId(int id)
        //    {
        //        return await _unitOfWork.MixinCancelOrders.GetAllForVoteId(id);
        //    }

    }
}