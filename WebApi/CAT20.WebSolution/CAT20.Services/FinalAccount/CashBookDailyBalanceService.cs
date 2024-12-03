using AutoMapper;
using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.FinalAccount
{
    public class CashBookDailyBalanceService : ICashBookDailyBalanceService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CashBookDailyBalanceService(IVoteUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Create(CashBookDailyBalance newCashBookDailyBalance)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                        await _unitOfWork.CashBookDailyBalance.AddAsync(newCashBookDailyBalance);
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

        public async Task<bool> BulkCreate(IEnumerable<CashBookDailyBalance> newCashBookDailyBalanceList)
        {
           
                try
                {
                    await _unitOfWork.CashBookDailyBalance.AddRangeAsync(newCashBookDailyBalanceList);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
        }

        public async Task<bool> GetTotalsAndCreateDailyCashBookDailyBalances(int officeId, int sessionId, DateTime date, int createdby)
        {
            try
            {
                var CashBookDailyBalancesList = await _unitOfWork.CashBook.GetDailyBalancesAsync(officeId, sessionId, date, createdby);

                if(CashBookDailyBalancesList!=null && CashBookDailyBalancesList.Any())
                { 
                    await _unitOfWork.CashBookDailyBalance.AddRangeAsync(CashBookDailyBalancesList);
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
