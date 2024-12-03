using AutoMapper;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core;
using CAT20.Core.Services.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.FinalAccount
{
    public class VoteLedgerBookDailyBalanceService : IVoteLedgerBookDailyBalanceService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VoteLedgerBookDailyBalanceService(IVoteUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Create(VoteLedgerBookDailyBalance newVoteLedgerBookDailyBalance)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    await _unitOfWork.VoteLedgerBookDailyBalance.AddAsync(newVoteLedgerBookDailyBalance);
                    await _unitOfWork.CommitAsync();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> BulkCreate(IEnumerable<VoteLedgerBookDailyBalance> newVoteLedgerBookDailyBalanceList)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    await _unitOfWork.VoteLedgerBookDailyBalance.AddRangeAsync(newVoteLedgerBookDailyBalanceList);
                    await _unitOfWork.CommitAsync();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> GetTotalsAndCreateDailyVoteLedgerBookDailyBalances(int officeId, int sessionId, DateTime date, int createdby)
        {
            try
            {
                var VoteLedgerBookDailyBalancesList = await _unitOfWork.VoteLedgerBook.GetDailyBalancesAsync(officeId, sessionId, date, createdby);

                if (VoteLedgerBookDailyBalancesList != null && VoteLedgerBookDailyBalancesList.Any())
                {
                    await _unitOfWork.VoteLedgerBookDailyBalance.AddRangeAsync(VoteLedgerBookDailyBalancesList);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
