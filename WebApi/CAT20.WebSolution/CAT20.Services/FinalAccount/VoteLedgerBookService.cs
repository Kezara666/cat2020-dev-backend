using AutoMapper;
using CAT20.Core;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.Services.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.FinalAccount
{
    public class VoteLedgerBookService: IVoteLedgerBookService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VoteLedgerBookService(IVoteUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VoteLedgerBookDailyBalance>> GetDailyBalances(int officeId, int sessionId, DateTime date, int createdby)
        {
            return await _unitOfWork.VoteLedgerBook.GetDailyBalancesAsync(officeId, sessionId, date, createdby);
        }
    }
}
