using CAT20.Core;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.AssessmentTax;
using CAT20.Core.Services.Vote;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.Vote
{
    public class ExpenditureVoteAllocationService : IExpenditureVoteAllocationService
    {

        private readonly IVoteUnitOfWork _unitOfWork;
        public ExpenditureVoteAllocationService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ExpenditureVoteAllocation> CreateCommitment(ExpenditureVoteAllocation expenditureVoteAllocationToCreate)
        {


            await _unitOfWork.ExpenditureVoteAllocation
               .AddAsync(expenditureVoteAllocationToCreate);
            await _unitOfWork.CommitAsync();


            return expenditureVoteAllocationToCreate;
        }

        public async Task<ExpenditureVoteAllocation> CreateExpenditureVoteAllocation(ExpenditureVoteAllocation newVoteAllocation)
        {
            await _unitOfWork.ExpenditureVoteAllocation
                   .AddAsync(newVoteAllocation);
            await _unitOfWork.CommitAsync();


            return newVoteAllocation;
        }

        public async Task DeleteExpenditureVoteAllocation(ExpenditureVoteAllocation expenditureVoteAllocation)
        {
            expenditureVoteAllocation.Status = 0;
            await _unitOfWork.CommitAsync();
        }

        public Task DeleteExpenditureVoteAllocation(VoteAllocation voteAllocation)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ExpenditureVoteAllocation>> GetAllExpendditureVoteAllocationsForSabhaId(int SabhaId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ExpenditureVoteAllocation>> GetAllExpendditureVoteAllocationsForVoteDetailIdandSabhaIdandYear(int VoteDetailId, int SabhaId, int Year)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ExpenditureVoteAllocation>> GetAllExpenditureVoteAllocationsForVoteDetailIdandSabhaIdandYear(int voteDetailId, int sabhaId, int year)
        {
            return await _unitOfWork.ExpenditureVoteAllocation.GetAllExpenditureVoteAllocationsForVoteDetailIdandSabhaIdandYearAsync(voteDetailId, sabhaId, year);
        }

        public Task<IEnumerable<ExpenditureVoteAllocation>> GetAllVoteAllocations()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ExpenditureVoteAllocation>> GetAllWithExpenditureVoteAllocationByVoteDetailIdSabhaId(int voteDetailId, int sabhaId)
        {
            return await _unitOfWork.ExpenditureVoteAllocation.GetAllWithExpenditureVoteAllocationByVoteDetailIdSabhaIdAsync(voteDetailId, sabhaId);
        }

        public Task<IEnumerable<ExpenditureVoteAllocation>> GetAllWithVoteAllocationByVoteDetailIdAsync(int VoteDetailId)
        {
            throw new NotImplementedException();
        }

        public async Task<ExpenditureVoteAllocation> GetExpenditureVoteAllocationById(int iD)
        {
            return await _unitOfWork.ExpenditureVoteAllocation.GetByIdAsync(iD);
        }

        public async Task UpdateExpenditureVoteAllocation(ExpenditureVoteAllocation expenditureVoteAllocationToBeUpdate, ExpenditureVoteAllocation expenditureVoteAllocation)
        {

            expenditureVoteAllocationToBeUpdate.Amount = expenditureVoteAllocation.Amount;

            await _unitOfWork.CommitAsync();
        }


    }
}
