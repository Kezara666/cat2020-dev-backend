using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface IYearService
    {
        Task<IEnumerable<Year>> GetAllYears();
        Task<Year> GetYearById(int id);
        Task<Year> CreateYear(Year newYear);
        Task UpdateYear(Year yearToBeUpdated, Year year);
        Task DeleteYear(Year year);
    }
}

