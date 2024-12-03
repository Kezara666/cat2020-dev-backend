using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core;
using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.Vote;
using CAT20.Core.Services.Vote;

namespace CAT20.Services.Vote
{
    public class ClassificationService :IClassificationService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        public ClassificationService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Classification>> GetAllClassifications()
        {
            return await _unitOfWork.Classifications.GetAllClassifications();
        }
        public async Task<Classification> GetClassificationById(int id)
        {
            return await _unitOfWork.Classifications.GetClassificationById(id);
        }
        public async Task<IEnumerable<ClassificationAdvancedModel>> GetAllClassificationsWithLedgerAccountsForSabha(int sabhaId)
        {
            return await _unitOfWork.Classifications.GetAllClassificationsWithLedgerAccountsForSabha(sabhaId);
        }
    }
}
