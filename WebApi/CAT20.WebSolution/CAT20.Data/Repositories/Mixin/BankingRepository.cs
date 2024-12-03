using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Interfaces;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Repositories.Mixin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Mixin
{
    public class BankingRepository : Repository<Banking>, IBankingRepository
    {
        public BankingRepository(DbContext context) : base(context)
        {
        }

        public async Task<Banking> GetById(int id)
        {
             return await mixinDbContext.Bankings
                .AsNoTracking()
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Banking> GetLastBankingDateForOfficeId(int officeid)
        {
            //var banking = await mixinDbContext.Bankings
            //  .Where(m => (m.MixinOrder.OfficeId == officeid))
            //  .OrderByDescending(m => m.BankedDate)
            //    .FirstOrDefaultAsync();
            //return banking;

            var banking = await mixinDbContext.Bankings.AsNoTracking()
             .Where(m => (m.OfficeId == officeid))
             .OrderByDescending(m => m.BankedDate)
               .FirstOrDefaultAsync();
            return banking;
        }

        //public async Task<List<Banking>> GetBankingListAsync()
        //{
        //    return await mixinDbContext.Bankings
        //        .FromSqlRaw<Banking>("GetBankingList")
        //        .ToListAsync();
        //}

        //public async Task<Banking> GetByIdAndOffice(int id, int officeid)
        //{
        //    return await mixinDbContext.Banking
        //      .Include(t => t.MixinOrder)
        //      .Where(m => (m.MixinOrder.OfficeId == officeid))
        //        .ToListAsync();
        //}


        //public async Task<IEnumerable<MixinOrder>> GetAllForBankAccountOfficeDate(int bankaccountid, int officeid, DateTime bankeddate)
        //{
        //    return await mixinDbContext.Banking
        //      .Include(t => t.MixinOrder)
        //      .Where(m => (m.MixinOrder.AccountDetailId == bankaccountid) && (m.MixinOrder.OfficeId == officeid) &&  (m.BankedDate).ToShortDateString() == bankeddate.ToShortDateString())
        //      .ToListAsync();
        //}

        //public async Task<IEnumerable<MixinOrder>> GetAllForOffice(int officeId)
        //{
        //    return
        //        await mixinDbContext.MixinOrders
        //        .Where(m => m.OfficeId == officeId && m.State != OrderStatus.Deleted)
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<MixinOrder>> GetAllForOfficeAndState(int officeId, OrderStatus state)
        //{
        //     var result=   await mixinDbContext.MixinOrders
        //        .Where(m => m.OfficeId == officeId && m.State==state)
        //        .OrderByDescending(m=> m.Id)
        //        .ToListAsync();
        //    return result;
        //}

        //public async Task<IEnumerable<MixinOrder>> GetAllForOfficeAndStateAndDate(int officeId, OrderStatus state, DateTime fordate)
        //{
        //    if (state == OrderStatus.Posted)
        //    {
        //        var result = await mixinDbContext.MixinOrders
        //           .Where(m => m.OfficeId == officeId && ( m.State == OrderStatus.Posted || m.State == OrderStatus.Cancel_Disapproved)
        //           &&
        //                        m.CreatedAt.Year == fordate.Date.Year
        //                        && m.CreatedAt.Month == fordate.Date.Month
        //                        && m.CreatedAt.Day == fordate.Date.Day
        //                        )
        //           .OrderByDescending(m => m.Id)
        //           .ToListAsync();
        //        return result;
        //    }
        //    else
        //    {
        //        var result = await mixinDbContext.MixinOrders
        //           .Where(m => m.OfficeId == officeId && m.State == state
        //           &&
        //                        m.CreatedAt.Year == fordate.Date.Year
        //                        && m.CreatedAt.Month == fordate.Date.Month
        //                        && m.CreatedAt.Day == fordate.Date.Day
        //                        )
        //           .OrderByDescending(m => m.Id)
        //           .ToListAsync();
        //        return result;
        //    }
        //}

        //public async Task<IEnumerable<MixinOrder>> GetAllForUserAndState(int userid, OrderStatus state)
        //{
        //    var result = await mixinDbContext.MixinOrders
        //       .Where(m => m.CreatedBy == userid && m.State == state)
        //       .OrderByDescending(m => m.Id)
        //       .ToListAsync();
        //    return result;
        //}

        //public async Task<IEnumerable<MixinOrder>> GetAllForSessionAndState(int sessionId, OrderStatus state)
        //{
        //    return
        //        await mixinDbContext.MixinOrders
        //        .Where(m => m.SessionId == sessionId && m.State == state)
        //        .ToListAsync();
        //}

        private MixinDbContext mixinDbContext
        {
            get { return Context as MixinDbContext; }
        }
      
    }
}