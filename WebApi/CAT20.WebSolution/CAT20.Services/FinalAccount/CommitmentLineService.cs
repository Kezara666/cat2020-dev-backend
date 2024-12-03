using CAT20.Core;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Services.FinalAccount;


namespace CAT20.Services.FinalAccount
{
    public class CommitmentLineService : ICommitmentLineService
    {

        private readonly IVoteUnitOfWork _unitOfWork;
        public CommitmentLineService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CommitmentLine> CreateCommitmentLine(CommitmentLine newCommitmentLine)
        {
            try
            {
                await _unitOfWork.CommitmentLine
                .AddAsync(newCommitmentLine);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
            }

            return newCommitmentLine;
        }

       

    }
}
