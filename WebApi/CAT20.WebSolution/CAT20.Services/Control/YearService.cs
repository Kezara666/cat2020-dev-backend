using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;

namespace CAT20.Services.Control
{
    public class YearService : IYearService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public YearService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Year> CreateYear(Year newYear)
        {
            await _unitOfWork.Years
                .AddAsync(newYear);
            await _unitOfWork.CommitAsync();

            return newYear;
        }
        public async Task DeleteYear(Year year)
        {
            _unitOfWork.Years.Remove(year);

            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<Year>> GetAllYears()
        {
            return await _unitOfWork.Years.GetAllAsync();
        }
        public async Task<Year> GetYearById(int id)
        {
            return await _unitOfWork.Years.GetByIdAsync(id);
        }
        public async Task UpdateYear(Year yearToBeUpdated, Year year)
        {
            //yearToBeUpdated.Name = year.t;

            await _unitOfWork.CommitAsync();
        }
    }
}