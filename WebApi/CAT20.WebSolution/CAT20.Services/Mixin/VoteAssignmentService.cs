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
using CAT20.Core.Models.User;
using CAT20.Core.HelperModels;
using CAT20.Core.CustomExceptions;

namespace CAT20.Services.Mixin
{
    public class VoteAssignmentService : IVoteAssignmentService
    {
        private readonly IMixinUnitOfWork _unitOfWork;
        public VoteAssignmentService(IMixinUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<VoteAssignment> Create(VoteAssignment newVoteAssignment)
        {
            try
            {


                if(await _unitOfWork.VoteDetails.IsAccountTransferLedgerAccount(newVoteAssignment.VoteId))
                {

                   if(await _unitOfWork.VoteAssignments.HasAssigned(newVoteAssignment.VoteId))
                    {
                        throw new Exception("Account already assigned This Account Transfer Vote");
                    }
                    else
                    {

                        var voteAssignmentsByBankId = await _unitOfWork.VoteAssignments.GetAssignedVoteIds(newVoteAssignment.BankAccountId);

                        foreach (var voteId in voteAssignmentsByBankId)
                        {
                            if (await _unitOfWork.VoteDetails.IsAccountTransferLedgerAccount(voteId))
                            {
                                throw new Exception("Account already assigned This Bank Account");
                            }
                        }

                        var account = await _unitOfWork.AccountDetails.GetByIdAsync(newVoteAssignment.BankAccountId);
                        account.VoteId = newVoteAssignment.VoteId;

                        await _unitOfWork.VoteAssignments.AddAsync(newVoteAssignment);
                    }
                }
                else
                {
                    await _unitOfWork.VoteAssignments.AddAsync(newVoteAssignment);
                }




                #region Audit Log
                //var note = new StringBuilder();
                //if (newVoteAssignment.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newVoteAssignment.ID,
                //    TransactionName = "VoteAssignment",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {

            }

            return newVoteAssignment;
        }


        public async Task<(bool,string?)> NewCreate(IEnumerable<VoteAssignment> newVoteAssignmentList,HTokenClaim token)
        {


            try
            {
                var responseMessage = string.Empty;
                foreach (var newVoteAssignment in newVoteAssignmentList) { 

                    var voteDetail = await _unitOfWork.VoteDetails.GetByIdAsync(newVoteAssignment.VoteId);

                    if (voteDetail != null)
                    {
                        string customvotename = string.Empty;
                        if((token.IsFinalAccountsEnabled==1 || token.languageid==2))
                        {
                            customvotename=voteDetail.NameEnglish;
                        }
                        else if(token.languageid == 1)
                        {
                            customvotename = voteDetail.NameSinhala;
                        }
                        else if (token.languageid == 3)
                        {
                            customvotename = voteDetail.NameTamil;
                        }
                        else
                        {
                            customvotename = voteDetail.NameEnglish;
                        }

                        newVoteAssignment.VoteAssignmentDetails = new List<VoteAssignmentDetails>
                        {
                            new VoteAssignmentDetails
                            {
                                VoteAssignmentId = newVoteAssignment.Id,
                                CustomVoteName = customvotename,
                                IsActive = 1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            }
                        };



                        if (await _unitOfWork.VoteDetails.IsAccountTransferLedgerAccount(newVoteAssignment.VoteId))
                        {

                            if (await _unitOfWork.VoteAssignments.HasAssigned(newVoteAssignment.VoteId))
                            {
                                responseMessage.Concat($"{voteDetail.Code} Already Assign To A Bank Account");
                            }
                            else
                            {

                                var voteAssignmentsByBankId = await _unitOfWork.VoteAssignments.GetAssignedVoteIds(newVoteAssignment.BankAccountId);

                                foreach (var voteId in voteAssignmentsByBankId)
                                {
                                    if (await _unitOfWork.VoteDetails.IsAccountTransferLedgerAccount(voteId))
                                    {
                                        responseMessage.Concat($"{voteDetail.Code} Already Assign To A Bank Account");
                                    }
                                }

                                var account = await _unitOfWork.AccountDetails.GetByIdAsync(newVoteAssignment.BankAccountId);
                                account.VoteId = newVoteAssignment.VoteId;

                                await _unitOfWork.VoteAssignments.AddAsync(newVoteAssignment);
                            }
                        }
                        else
                        {
                            await _unitOfWork.VoteAssignments.AddAsync(newVoteAssignment);
                        }
                    }
                    else
                    {
                        //throw new FinalAccountException("Unable To Find Ledger Account(s)");
                    }


                }

                #region Audit Log
                //var note = new StringBuilder();
                //if (newVoteAssignment.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newVoteAssignment.ID,
                //    TransactionName = "VoteAssignment",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                await _unitOfWork.CommitAsync();
                return (true, "Vote Assignment Successful, Following Errors May Have Occurred:\n " + responseMessage);
            }
            catch (Exception ex)
            {
                return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);

            }

        }

        public async Task Delete(VoteAssignment voteAssignment)
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
                //    TransactionID = voteAssignment.ID,
                //    TransactionName = "VoteAssignment",
                //    User = 1,
                //    Note = note.ToString()
                //});
                //voteAssignment.IsActive = 0;

                #endregion

                //voteAssignment.IsActive = 0;
                _unitOfWork.VoteAssignments.Remove(voteAssignment);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
            //_unitOfWork.VoteAssignments.Remove(voteAssignment);
        }
        public async Task<IEnumerable<VoteAssignment>> GetAllForSabhaId(int sabhaid)
        {
            return await _unitOfWork.VoteAssignments.GetAllForSabhaId(sabhaid);
        }

        public async Task<IEnumerable<VoteAssignment>> GetAllForOfficeId(int officeid)
        {
            return await _unitOfWork.VoteAssignments.GetAllForOfficeId(officeid);
        }

        public async Task<IEnumerable<VoteAssignment>> GetAllForOfficeIdAndAccountDetailId(int officeid, int accountdetailid)
        {
            try { 
            return await _unitOfWork.VoteAssignments.GetAllForOfficeIdAndAccountDetailId(officeid, accountdetailid);
            }
            catch(Exception)
            {
                return null;
            }
        }

        public async Task<bool> HasVoteAssignmentsForAccountDetailId(int accountDetailId)
        {
            try
            {
                return await _unitOfWork.VoteAssignments.HasVoteAssignmentsForAccountDetailId(accountDetailId);
            }
            catch (Exception)
            {
                return true;
            }
        }

        public async Task<VoteAssignment> GetById(int id)
        {
            return await _unitOfWork.VoteAssignments.GetById(id);
        }
        public async Task Update(VoteAssignment voteAssignmentToBeUpdated, VoteAssignment voteAssignment)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (voteAssignmentToBeUpdated.NameEnglish != voteAssignment.NameEnglish)
                //    note.Append(" English Name :" + voteAssignmentToBeUpdated.NameEnglish + " Change to " + voteAssignment.NameEnglish);
                //if (voteAssignmentToBeUpdated.NameSinhala != voteAssignment.NameSinhala)
                //    note.Append(" Sinhala Name :" + voteAssignmentToBeUpdated.NameSinhala + " Change to " + voteAssignment.NameSinhala);
                //if (voteAssignmentToBeUpdated.NameTamil != voteAssignment.NameTamil)
                //    note.Append(" Tamil Name :" + voteAssignmentToBeUpdated.NameTamil + " Change to " + voteAssignment.NameTamil);
                //if (voteAssignmentToBeUpdated.Code != voteAssignment.Code)
                //    note.Append(" Code :" + voteAssignmentToBeUpdated.Code + " Change to " + voteAssignment.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = voteAssignmentToBeUpdated.ID,
                //    TransactionName = "VoteAssignment",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                voteAssignmentToBeUpdated.IsActive = 1;
                voteAssignmentToBeUpdated.DateModified = DateTime.Now;
                voteAssignmentToBeUpdated.BankAccountId = voteAssignment.BankAccountId;
                voteAssignmentToBeUpdated.OfficeId = voteAssignment.OfficeId;
                voteAssignmentToBeUpdated.VoteId = voteAssignment.VoteId;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }

        public async Task<IEnumerable<VoteAssignment>> GetAllForVoteId(int id)
            {
                return await _unitOfWork.VoteAssignments.GetAllForVoteId(id);
            }

        public async Task<bool> HasVoteAssignmentsForVoteId(int id)
        {
            return await _unitOfWork.VoteAssignments.HasVoteAssignmentsForVoteId(id);
        }

        public async Task<int> GetAssignedBankAccountForSubOffice(int Officeid)
        {
            return await _unitOfWork.VoteAssignments.GetAssignedBankAccountForSubOffice(Officeid);
        }

      
        public async Task<int> GetAccountIdByVoteId(int voteId, HTokenClaim token)
        {
            return await _unitOfWork.VoteAssignments.GetAccountIdByVoteId(voteId, token);
        }
    }
}