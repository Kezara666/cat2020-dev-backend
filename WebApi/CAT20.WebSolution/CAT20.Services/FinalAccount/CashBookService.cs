using AutoMapper;
using CAT20.Core;
using CAT20.Core.DTO.Final;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Services.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.FinalAccount
{
    public class CashBookService: ICashBookService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CashBookService(IVoteUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<(int totalCount, IEnumerable<CashBookResource> list)> GetAllTransactionsForSabhaByAccount(int sabhaId, int accountId, int pageNo, int pageSize, string? filterKeyWord)
        {
            var result = await _unitOfWork.CashBook.GetAllTransactionsForSabhaByAccount(sabhaId, accountId, pageNo, pageSize, filterKeyWord);
            return (result.totalCount, _mapper.Map<IEnumerable<CashBookResource>>(result.list));
        }

        public async Task<IEnumerable<CashBookDailyBalance>> GetDailyBalances(int officeId, int sessionId, DateTime date, int createdby)
        {
            return await _unitOfWork.CashBook.GetDailyBalancesAsync(officeId, sessionId, date, createdby);
        }
    }
}
