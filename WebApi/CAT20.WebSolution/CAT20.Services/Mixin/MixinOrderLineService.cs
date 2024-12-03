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
    public class MixinOrderLineService : IMixinOrderLineService
    {
        private readonly IMixinUnitOfWork _unitOfWork;
        public MixinOrderLineService(IMixinUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<MixinOrderLine> Create(MixinOrderLine newMixinOrderLine)
        {
            try
            {
                await _unitOfWork.MixinOrderLines
                .AddAsync(newMixinOrderLine);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newMixinOrderLine.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newMixinOrderLine.ID,
                //    TransactionName = "MixinOrderLine",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }

            return newMixinOrderLine;
        }
        public async Task Delete(MixinOrderLine mixinOrderLine)
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
                //    TransactionID = mixinOrderLine.ID,
                //    TransactionName = "MixinOrderLine",
                //    User = 1,
                //    Note = note.ToString()
                //});
                //mixinOrderLine.Active = 0;

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
            //_unitOfWork.MixinOrderLines.Remove(mixinOrderLine);
        }
        public async Task<IEnumerable<MixinOrderLine>> GetAllForOrderId(int mixinOrderId)
        {
            return await _unitOfWork.MixinOrderLines.GetAllForOrderId(mixinOrderId);
        }

        public async Task<IEnumerable<MixinOrderLine>> GetAllForOfficeDate(int officeid,DateTime orderdate)
        {
            return await _unitOfWork.MixinOrderLines.GetAllForOfficeDate(officeid,orderdate);
        }

        public async Task<IEnumerable<MixinOrderLine>> GetAllForVoteIdAndVoteorBal(int voteid, int voteorbal)
        {
            return await _unitOfWork.MixinOrderLines.GetAllForVoteIdAndVoteorBal(voteid,  voteorbal);
        }

        public async Task<MixinOrderLine> GetById(int id)
        {
            return await _unitOfWork.MixinOrderLines.GetById(id);
        }
        public async Task Update(MixinOrderLine mixinOrderLineToBeUpdated, MixinOrderLine mixinOrderLine)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (mixinOrderLineToBeUpdated.NameEnglish != mixinOrderLine.NameEnglish)
                //    note.Append(" English Name :" + mixinOrderLineToBeUpdated.NameEnglish + " Change to " + mixinOrderLine.NameEnglish);
                //if (mixinOrderLineToBeUpdated.NameSinhala != mixinOrderLine.NameSinhala)
                //    note.Append(" Sinhala Name :" + mixinOrderLineToBeUpdated.NameSinhala + " Change to " + mixinOrderLine.NameSinhala);
                //if (mixinOrderLineToBeUpdated.NameTamil != mixinOrderLine.NameTamil)
                //    note.Append(" Tamil Name :" + mixinOrderLineToBeUpdated.NameTamil + " Change to " + mixinOrderLine.NameTamil);
                //if (mixinOrderLineToBeUpdated.Code != mixinOrderLine.Code)
                //    note.Append(" Code :" + mixinOrderLineToBeUpdated.Code + " Change to " + mixinOrderLine.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = mixinOrderLineToBeUpdated.ID,
                //    TransactionName = "MixinOrderLine",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                //mixinOrderLineToBeUpdated.IsActive = 1;
                //mixinOrderLineToBeUpdated.DateModified = DateTime.Now;
                //mixinOrderLineToBeUpdated.BankAccountId = mixinOrderLine.BankAccountId;
                //mixinOrderLineToBeUpdated.OfficeId = mixinOrderLine.OfficeId;
                //mixinOrderLineToBeUpdated.VoteId = mixinOrderLine.VoteId;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }

        public async Task<IEnumerable<MixinOrderLine>> GetAllForVoteAssignmentId(int id)
        {
            return await _unitOfWork.MixinOrderLines.GetAllForVoteAssignmentId(id);
        }
        public async Task<IEnumerable<MixinOrderLine>> GetAllForVoteAssignmentDetailId(int id)
        {
            return await _unitOfWork.MixinOrderLines.GetAllForVoteAssignmentDetailId(id);
        }

    }
}