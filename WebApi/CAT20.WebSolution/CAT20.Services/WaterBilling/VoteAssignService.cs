using CAT20.Core;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Services.Vote;
using CAT20.Core.Services.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.WaterBilling
{
    public class VoteAssignService : IVoteAssignService
    {
        private readonly IWaterBillingUnitOfWork _unitOfWork;
        private readonly IVoteDetailService _voteDetailService;
        private readonly IVoteAssignmentDetailsService _voteAssignmentDetailsService;

        public VoteAssignService(IWaterBillingUnitOfWork wb_unitOfWork, IVoteDetailService voteDetailService, IVoteAssignmentDetailsService voteAssignmentDetailsService)
        {
            _unitOfWork = wb_unitOfWork;
            _voteDetailService = voteDetailService;
            _voteAssignmentDetailsService = voteAssignmentDetailsService;
        }
        public async Task<VoteAssign> Create(VoteAssign newVoteAssign)
        {
            try
            {

                var voteAssignmentDetail = await _voteAssignmentDetailsService.GetById(newVoteAssign.vote.Value);
                if (voteAssignmentDetail == null)
                {
                    throw new Exception("Vote Assignment Detail not found");
                }

                var voteDetail = await _voteDetailService.GetVoteDetailById(voteAssignmentDetail.voteAssignment.VoteId);
                //voteAssignmentDetail.voteAssignment.voteDetail = voteDetail;

                if (voteDetail != null)
                {
                    newVoteAssign.VoteDetailsId = voteDetail.ID!.Value;
                }
                else
                {
                    throw new Exception("Vote Detail not found");
                }

                await _unitOfWork.VoteAssigns.AddAsync(newVoteAssign);
                await _unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return newVoteAssign;
        }

        public Task Delete(VoteAssign obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VoteAssign>> GetAllForWaterProject(int waterProjectId)
        {
            return await _unitOfWork.VoteAssigns.GetAllForWaterProject(waterProjectId);
        }

        public async Task<IEnumerable<VoteAssign>> GetAllVoteAssigns()
        {
            return await _unitOfWork.VoteAssigns.GetAllAsync();
        }

        public async Task<VoteAssign> GetById(int id)
        {
            return await _unitOfWork.VoteAssigns.GetByIdAsync(id);
        }

        public async Task Update(VoteAssign objToBeUpdated, VoteAssign obj)
        {

            objToBeUpdated.vote = obj.vote;
            objToBeUpdated.UpdatedBy = obj.UpdatedBy;
            objToBeUpdated.UpdatedAt = DateTime.Now;

            await _unitOfWork.CommitAsync(); 
        }
    }
}
