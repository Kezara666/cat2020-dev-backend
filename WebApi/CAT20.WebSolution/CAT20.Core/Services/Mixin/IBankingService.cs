using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Mixin
{
    public interface IBankingService
    {
        Task<Banking> GetById(int id);
        Task<Banking> Create(Banking newBanking);
        Task<Banking> GetLastBankingDateForOfficeId(int officeid);
        //Task<Banking> GetByCode(string code, int officeid);
        //Task<Banking> Create(Banking newBanking);
        //Task Update(Banking mixinOrderToBeUpdated, Banking mixinOrder);
        ////Task Cancel(Banking mixinOrder);
        //Task Paid(Banking mixinOrder, int cashierid, string documentCode);
        //Task ApproveCancelOrder(Banking mixinOrder, int officerid);
        //Task DisapproveCancelOrder(Banking mixinOrder, int officerid);
        //Task CancelOrder(Banking mixinOrder, int officerid);
        //Task DeleteOrder(Banking mixinOrder, int officerid);
        //Task PostOrder(Banking mixinOrder, int officerid);

        //Task updateState(Banking mixinOrder, OrderStatus state);

        //Task<IEnumerable<Banking>> GetAll();
        //Task<IEnumerable<Banking>> GetAllForOffice(int officeid);
        //Task<IEnumerable<Banking>> GetAllForOfficeAndState(int officeid, OrderStatus state);
        //Task<IEnumerable<Banking>> GetAllForOfficeAndStateAndDate(int officeid, OrderStatus state, DateTime fordate);
        //Task<IEnumerable<Banking>> GetAllForUserAndState(int userid, OrderStatus state);
        //Task<IEnumerable<Banking>> GetAllForSessionAndState(int sessionid, OrderStatus state);
    }
}

