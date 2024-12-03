using CAT20.Core;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Services.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.FinalAccount
{
    public class SabhaFundSourceService : ISabhaFundSourceService
    {
        private readonly IVoteUnitOfWork _unitOfWork;

        public SabhaFundSourceService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<IEnumerable<SabhaFundSource>> GetAllActive()
        {
            return _unitOfWork.SabhaFundSources.GetAllActive();  
        }
    }
}
