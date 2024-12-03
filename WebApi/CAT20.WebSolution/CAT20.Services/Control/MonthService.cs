using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;

namespace CAT20.Services.Control
{
    public class MonthService : IMonthService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public MonthService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Month> CreateMonth(Month newMonth)
        {
            await _unitOfWork.Months
                .AddAsync(newMonth);
            await _unitOfWork.CommitAsync();

            return newMonth;
        }
        public async Task DeleteMonth(Month month)
        {
            _unitOfWork.Months.Remove(month);

            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<Month>> GetAllMonths()
        {
            return await _unitOfWork.Months.GetAllAsync();
        }
        public async Task<Month> GetMonthById(int id)
        {
            return await _unitOfWork.Months.GetByIdAsync(id);
        }
        public async Task UpdateMonth(Month monthToBeUpdated, Month month)
        {
            //monthToBeUpdated.Name = month.t;

            await _unitOfWork.CommitAsync();
        }
    }
}