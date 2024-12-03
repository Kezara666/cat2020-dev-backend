using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Vote
{
    public interface IBalancesheetTitleService
    {
        Task<IEnumerable<BalancesheetTitle>> GetAllBalancesheetTitles();
        Task<BalancesheetTitle> GetBalancesheetTitleById(int id);
        Task<BalancesheetTitle> CreateBalancesheetTitle(BalancesheetTitle newBalancesheetTitle);
        Task UpdateBalancesheetTitle(BalancesheetTitle balancesheetTitleToBeUpdated, BalancesheetTitle balancesheetTitle);
        Task DeleteBalancesheetTitle(BalancesheetTitle balancesheetTitle);
        Task<IEnumerable<BalancesheetTitle>> GetAllWithBalancesheetTitleByBalancesheetTitleId(int Id);
        Task<IEnumerable<BalancesheetTitle>> GetAllBalancesheetTitleBySabhaId(int Id);
        Task<IEnumerable<BalancesheetTitle>> GetAllWithBalancesheetTitleByAccountDetailId(int Id);
        Task<IEnumerable<BalancesheetTitle>> GetAllWithBalancesheetTitleByClassificationCategoryId(int ClassificationId, int CategoryId, int sabhaid);
        Task<IEnumerable<BalancesheetTitle>> GetAllWithBalancesheetTitleByClassificationId(int ClassificationId, int sabhaid);
        Task<IEnumerable<BalancesheetTitle>> GetAllWithBalancesheetTitleByCategoryId(int CategoryId, int sabhaid);





    }
}

