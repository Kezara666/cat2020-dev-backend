using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface IMonthService
    {
        Task<IEnumerable<Month>> GetAllMonths();
        Task<Month> GetMonthById(int id);
        Task<Month> CreateMonth(Month newMonth);
        Task UpdateMonth(Month monthToBeUpdated, Month month);
        Task DeleteMonth(Month month);
    }
}

