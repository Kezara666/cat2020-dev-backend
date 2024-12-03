using CAT20.Core;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.Vote;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.AssessmentTax;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Services.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.AssessmentTax
{
    public class AssmtVoteAssignService : IAssmtVoteAssignService
    {
        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        private readonly IAssmtVoteAssignService _voteAssignService;
        private readonly IVoteAssignmentService _voteAssignmentService;
        private readonly IVoteAssignmentDetailsService _voteAssignmentDetailsService;
        private readonly IVoteDetailService _voteDetailService;
        private readonly IAccountDetailService _accountDetailService;

        public AssmtVoteAssignService(IAssessmentTaxUnitOfWork wb_unitOfWork,  IVoteAssignmentService voteAssignmentService, IVoteAssignmentDetailsService voteAssignmentDetailsService, IVoteDetailService voteDetailService, IAccountDetailService accountDetailService)
        {
            _unitOfWork = wb_unitOfWork;
            _voteAssignmentService = voteAssignmentService;
            _voteAssignmentDetailsService = voteAssignmentDetailsService;
            _voteDetailService = voteDetailService;
            _accountDetailService = accountDetailService;
        }
        public async Task<AssessmentVoteAssign> Create(AssessmentVoteAssign newAssmtVoteAssign)
        {
            try
            {
                var voteAssignmentDetail = await _voteAssignmentDetailsService.GetById(newAssmtVoteAssign.VoteAssignmentDetailId.Value);
                if (voteAssignmentDetail == null)
                {
                    throw new Exception("Vote Assignment Detail not found");
                }

                var voteDetail = await _voteDetailService.GetVoteDetailById(voteAssignmentDetail.voteAssignment.VoteId);
                //voteAssignmentDetail.voteAssignment.voteDetail = voteDetail;

                if (voteDetail != null)
                {
                    newAssmtVoteAssign.VoteDetailId = voteDetail.ID;
                }
                else
                {
                    throw new Exception("Vote Detail not found");
                }


                await _unitOfWork.AssmtVoteAssigns.AddAsync(newAssmtVoteAssign);
                await _unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return newAssmtVoteAssign;
        }

        public Task Delete(AssessmentVoteAssign obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AssessmentVoteAssign>> GetAllForSabha(int sabhaid)
        {
            return await _unitOfWork.AssmtVoteAssigns.GetAllForSabha(sabhaid);
        }

        public async Task<IEnumerable<AssessmentVoteAssign>> GetAllAssmtVoteAssigns()
        {
            return await _unitOfWork.AssmtVoteAssigns.GetAllAsync();
        }

        public async Task<AssessmentVoteAssign> GetById(int id)
        {
            return await _unitOfWork.AssmtVoteAssigns.GetByIdAsync(id);
        }

        public async Task Update(AssessmentVoteAssign objToBeUpdated, AssessmentVoteAssign obj)
        {

            objToBeUpdated.VoteAssignmentDetailId = obj.VoteAssignmentDetailId;
            objToBeUpdated.UpdatedBy = obj.UpdatedBy;
            objToBeUpdated.UpdatedAt = DateTime.Now;

            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> SetVoteId(int sabhaId)
        {
            try
            {
                var voteassigns = await _unitOfWork.AssmtVoteAssigns.GetAllForSabha(sabhaId);

                foreach (var voteAssign in voteassigns)
                {
                    var voteAssignmentDetail = await _voteAssignmentDetailsService.GetById(voteAssign.VoteAssignmentDetailId.Value);
                    if(voteAssignmentDetail == null)
                    {
                        throw new Exception("Vote Assignment Detail not found");
                    }

                    var voteDetail = await _voteDetailService.GetVoteDetailById(voteAssignmentDetail.voteAssignment.VoteId);
                    //voteAssignmentDetail.voteAssignment.voteDetail = voteDetail;

                    if (voteDetail != null)
                    {
                        voteAssign.VoteDetailId = voteDetail.ID;
                    }
                    else
                    {
                        throw new Exception("Vote Detail not found");
                    }



                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }   
        }
    }
}
