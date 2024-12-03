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
using CAT20.Core.HelperModels;
using AutoMapper;
using CAT20.Data;
using CAT20.Core.CustomExceptions;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace CAT20.Services.Mixin
{
    public class VoteAssignmentDetailsService : IVoteAssignmentDetailsService
    {
        private readonly IMixinUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VoteAssignmentDetailsService(IMixinUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<VoteAssignmentDetails> Save(VoteAssignmentDetails newVoteAssignmentDetails)
        {
            try
            {
                await _unitOfWork.VoteAssignmentDetails
                .AddAsync(newVoteAssignmentDetails);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newVoteAssignmentDetails.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newVoteAssignmentDetails.ID,
                //    TransactionName = "VoteAssignmentDetails",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }

            return newVoteAssignmentDetails;
        }

        private ICollection<HVoteAssignmentDetails> GetRecursive(int? parentId)
        {
            var vs = _mapper.Map<IEnumerable<HVoteAssignmentDetails>>(_unitOfWork.VoteAssignmentDetails.GetChildren(parentId));

            foreach (var v in vs)
            {
                v.Children = GetRecursive(v.Id).ToList();
            }

            return vs.ToList();
        }

        public async Task<(bool, string?)> NewSave(VoteAssignmentDetails newVoteAssignmentDetails, HTokenClaim token)
        {
            try
            {
                newVoteAssignmentDetails.CreatedBy = token.userId;
                newVoteAssignmentDetails.DateCreated = DateTime.Now;
                newVoteAssignmentDetails.IsActive = 1;

                if (newVoteAssignmentDetails.ParentId.HasValue )
                {
                    var parent = await _unitOfWork.VoteAssignmentDetails.GetById(newVoteAssignmentDetails.ParentId.Value);
                    if (parent != null)
                    {
                        if (parent.Depth ==3)
                        {
                            throw new GeneralException("Sub Level 4  Is Not Allowed");
                        }

                        newVoteAssignmentDetails.Depth = parent.Depth + 1;
                    }
                    else
                    {
                        throw new GeneralException("Parent not found");
                    }

                }
                await _unitOfWork.VoteAssignmentDetails .AddAsync(newVoteAssignmentDetails);

                await _unitOfWork.CommitAsync();

                return (true, "Successfully Saved !");

            }
            catch (Exception ex)
            {
                return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
            }

           
        }

        public async Task Delete(VoteAssignmentDetails voteAssignmentDetails)
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
                //    TransactionID = voteAssignmentDetails.ID,
                //    TransactionName = "VoteAssignmentDetails",
                //    User = 1,
                //    Note = note.ToString()
                //});
                #endregion

                //voteAssignmentDetails.IsActive = 0;
                if (!(await _unitOfWork.MixinOrderLines.IsRelationShipExist(voteAssignmentDetails.Id) || await _unitOfWork.MixOrderLineLogs.IsRelationShipExist(voteAssignmentDetails.Id)))
                {
                    _unitOfWork.VoteAssignmentDetails.Remove(voteAssignmentDetails);
                }
                else
                {
                    voteAssignmentDetails.IsActive = 0;
                }

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
            //_unitOfWork.VoteAssignmentDetailss.Remove(voteAssignmentDetails);
        }
        public async Task<IEnumerable<VoteAssignmentDetails>> GetAll()
        {
            return await _unitOfWork.VoteAssignmentDetails.GetAll();
        }

        public async Task<VoteAssignmentDetails> GetById(int id)
        {
            return await _unitOfWork.VoteAssignmentDetails.GetById(id);
        }
        public async Task Update(VoteAssignmentDetails voteAssignmentDetailsToBeUpdated, VoteAssignmentDetails voteAssignmentDetails)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (voteAssignmentDetailsToBeUpdated.NameEnglish != voteAssignmentDetails.NameEnglish)
                //    note.Append(" English Name :" + voteAssignmentDetailsToBeUpdated.NameEnglish + " Change to " + voteAssignmentDetails.NameEnglish);
                //if (voteAssignmentDetailsToBeUpdated.NameSinhala != voteAssignmentDetails.NameSinhala)
                //    note.Append(" Sinhala Name :" + voteAssignmentDetailsToBeUpdated.NameSinhala + " Change to " + voteAssignmentDetails.NameSinhala);
                //if (voteAssignmentDetailsToBeUpdated.NameTamil != voteAssignmentDetails.NameTamil)
                //    note.Append(" Tamil Name :" + voteAssignmentDetailsToBeUpdated.NameTamil + " Change to " + voteAssignmentDetails.NameTamil);
                //if (voteAssignmentDetailsToBeUpdated.Code != voteAssignmentDetails.Code)
                //    note.Append(" Code :" + voteAssignmentDetailsToBeUpdated.Code + " Change to " + voteAssignmentDetails.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = voteAssignmentDetailsToBeUpdated.ID,
                //    TransactionName = "VoteAssignmentDetails",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                voteAssignmentDetailsToBeUpdated.IsActive = 1;
                voteAssignmentDetailsToBeUpdated.DateModified = DateTime.Now;
                voteAssignmentDetailsToBeUpdated.CustomVoteName = voteAssignmentDetails.CustomVoteName;
                voteAssignmentDetailsToBeUpdated.VoteAssignmentId = voteAssignmentDetails.VoteAssignmentId;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }

        public async Task<IEnumerable<VoteAssignmentDetails>> GetAllVoteAssignmentDetailsForVoteAssignmentId(int id)
        {
            return await _unitOfWork.VoteAssignmentDetails.GetAllVoteAssignmentDetailsForVoteAssignmentId(id);
        }

        public async Task<IEnumerable<VoteAssignmentDetails>> GetAllForOfficeId(int id)
        {
            return await _unitOfWork.VoteAssignmentDetails.GetAllForOfficeId(id);
        }

        public async Task<IEnumerable<VoteAssignmentDetails>> GetAllForSabhaId(int id)
        {
            return await _unitOfWork.VoteAssignmentDetails.GetAllForSabhaId(id);
        }

        // Custom Votes
        public async Task<(bool, string?)> SaveCustomVotesAsync(List<VoteAssignmentDetails> customVotes)
        {
            try
            {
                foreach (var customVote in customVotes)
                {
                    if (customVote.Id == 0)
                    {
                        await _unitOfWork.VoteAssignmentDetails.AddAsync(customVote);
                    }
                    else
                    {
                        var existingVote = await _unitOfWork.VoteAssignmentDetails.GetById(customVote.Id);
                        if (existingVote != null)
                        {
                            existingVote.VoteAssignmentId = customVote.VoteAssignmentId;
                            existingVote.CustomVoteName = customVote.CustomVoteName;
                            existingVote.IsActive = customVote.IsActive;
                            existingVote.DateModified = DateTime.Now;
                        }
                    }
                }
                await _unitOfWork.CommitAsync();
                return (true, "Successfully Saved !");
            }
            catch (Exception ex)
            {
                return (false, "error occoured " + ex.Message);
            }
        }

        public async Task<(bool, string?)> DeleteCustomVoteAsync(int id)
        {
            try
            {
                var vote = await _unitOfWork.VoteAssignmentDetails.GetById(id);
                if (vote != null)
                {
                    _unitOfWork.VoteAssignmentDetails.Remove(vote);
                    await _unitOfWork.CommitAsync();
                    return (true, "Deleted Successfully");
                }
                return (false, "Not Found !");
            }
            catch (Exception ex)
            {
                    return (false, "error Ocoured" + ex.Message);
            }
        }

        public async Task<IEnumerable<HVoteAssignmentDetails>> GetCustomVoteWithSubLevelsForVoteAssignmentId(int assignmentId)
        {
            var rootResourse = _mapper.Map<IEnumerable<HVoteAssignmentDetails>>(await _unitOfWork.VoteAssignmentDetails.GetCustomVoteWithZeroLevelsForVoteAssignmentId(assignmentId));
            if (rootResourse != null)
            {
                foreach (var item in rootResourse)
                {
                    item.Children = GetRecursive(item.Id);
                }

                //rootResourse.Children = GetRecursive(rootResourse.Id);


            }

            return rootResourse;
        }



        public async Task<HVoteAssignmentDetails> GetCustomVoteWithSubLevels(int Id)
        {
            //var root = await _unitOfWork.VoteAssignmentDetails.GetCustomVoteWithSubLevels(Id);
            var rootResourse = _mapper.Map<HVoteAssignmentDetails>(await _unitOfWork.VoteAssignmentDetails.GetCustomVoteWithSubLevels(Id));
            if (rootResourse != null) { 

             rootResourse.Children = GetRecursive(rootResourse.Id);


            }

            return rootResourse;
        }



        public async Task<IEnumerable<HVoteAssignmentDetails>> getCustomVoteWithSubLevelsForVoteId(int voteId, HTokenClaim token)
        {
            var rootResourse = _mapper.Map<IEnumerable<HVoteAssignmentDetails>>(await _unitOfWork.VoteAssignmentDetails.getCustomVoteWithZeroLevelsForVoteId(voteId,token));
            if (rootResourse != null)
            {
                foreach (var item in rootResourse)
                {
                    item.Children = GetRecursive(item.Id);
                }

                //rootResourse.Children = GetRecursive(rootResourse.Id);

            }

            return rootResourse;
        }

        public async Task<IEnumerable<HVoteAssignmentDetails>> getCustomVoteWithSubLevelsForOfficeAndBankAccountId(int bankaccountid, HTokenClaim token)
        {
            var rootResourse = _mapper.Map<IEnumerable<HVoteAssignmentDetails>>(await _unitOfWork.VoteAssignmentDetails.getCustomVoteWithSubLevelsForOfficeAndBankAccountId(bankaccountid, token));
            if (rootResourse != null)
            {
                foreach (var item in rootResourse)
                {
                    item.Children = GetRecursive(item.Id);
                }
                //rootResourse.Children = GetRecursive(rootResourse.Id);
            }
            return rootResourse;
        }

        public async Task<IEnumerable<HVoteAssignmentDetails>> getCustomVoteWithSubLevelsForAccountId(int accountid, HTokenClaim token)
        {
            var rootResourse = _mapper.Map<IEnumerable<HVoteAssignmentDetails>>(await _unitOfWork.VoteAssignmentDetails.getCustomVoteWithZeroLevelsForAccountId(accountid, token));
            if (rootResourse != null)
            {
                foreach (var item in rootResourse)
                {
                    item.Children = GetRecursive(item.Id);
                }

                //rootResourse.Children = GetRecursive(rootResourse.Id);

            }

            return rootResourse;
        }
    }
}