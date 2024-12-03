using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Mixin;

namespace CAT20.Core.Repositories.Mixin
{
    public interface IBankingRepository : IRepository<Banking>
    {
        Task<Banking> GetById(int id);
        Task<Banking> GetLastBankingDateForOfficeId(int officeid);
        //Task<Banking> GetByCode(string code, int officeid);
        //Task<IEnumerable<Banking>> GetAll();
        //Task<IEnumerable<Banking>> GetAllForOffice(int officeid);
        //Task<IEnumerable<Banking>> GetAllForOfficeAndState(int Id, OrderStatus state);
        //Task<IEnumerable<Banking>> GetAllForOfficeAndStateAndDate(int Id, OrderStatus state, DateTime date);
        //Task<IEnumerable<Banking>> GetAllForUserAndState(int Id, OrderStatus state);
        //Task<IEnumerable<Banking>> GetAllForSessionAndState(int SessionId, OrderStatus state);
    }
}
