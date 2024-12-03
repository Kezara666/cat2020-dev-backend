using CAT20.Core.Models.Control;
using CAT20.Core.Repositories.Control;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CAT20.Data.Repositories.Control
{
    public class SessionRepository : Repository<Session>, ISessionRepository
    {
        public SessionRepository(DbContext context) : base(context)
        {
        }
        public async Task<Session> GetByIdAsync(int id)
        {
            return await controlDbContext.Sessions
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Session> GetByOfficeAndModule(int officeid, string Module)
        {
            return await controlDbContext.Sessions
                .Where(m => m.Active == 1 && m.OfficeId == officeid && m.Module.Trim() == Module.Trim())
                .FirstOrDefaultAsync();
        }
        public async Task<Session> GetAnyByOfficeAndModule(int? officeid, string Module)
        {
            return await controlDbContext.Sessions
                .Where(m =>  m.OfficeId == officeid && m.Module.Trim() == Module.Trim())
                .OrderByDescending(m => m.StartAt)
                .FirstOrDefaultAsync();
        }

        public async Task<Session> GetActiveOrLastActiveSessionForOnlinePaymentCurrentMonth(int? officeid, string Module)
        {
            return await controlDbContext.Sessions
                .Where(m => m.OfficeId == officeid && m.Module.Trim() == Module.Trim() && m.StartAt.Year==DateTime.Now.Year && m.StartAt.Month == DateTime.Now.Month)
                .OrderByDescending(m => m.StartAt)
                .FirstOrDefaultAsync();
        }

        public async Task<Session> GetActiveSessionByOfficeAndModuleAsync(int officeid, string Module)
        {
            return await controlDbContext.Sessions
                .Where(m => m.Active == 1 && m.OfficeId == officeid && m.Module.Trim() == Module.Trim())
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Session>> GetAllActiveSessionsByOfficeAsync(int officeId)
        {
            return await controlDbContext.Sessions
                .Where(m => m.Active == 1 && m.OfficeId == officeId)
                .OrderByDescending(m => m.StartAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Session>> GetAllSessionsByOfficeAndModuleAsync(int officeId, string module)
        {
            return await controlDbContext.Sessions
                .Where(m => m.OfficeId == officeId && m.Module == module)
                .OrderByDescending(m => m.StartAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Session>> GetAllSessionsByOfficeAsync(int officeId)
        {
            return await controlDbContext.Sessions
                .Where(m => m.OfficeId == officeId)
                .OrderByDescending(m => m.StartAt)
                .ToListAsync();
        }
         

        public async Task<Session> GetByOfficeModuleAndDate(int officeid, string Module, DateTime fordate)
        {
            return await controlDbContext.Sessions
                .Where(m => m.OfficeId == officeid && m.Module.Trim() == Module.Trim() &&
                m.StartAt.Year == fordate.Date.Year
            && m.StartAt.Month == fordate.Date.Month
            && m.StartAt.Day == fordate.Date.Day
                )
                .OrderByDescending(m => m.StartAt)
                .FirstOrDefaultAsync();
        }


        public async Task<Session> GetActiveSessionByOfficeAsync(int? officeid)
        {
            return await controlDbContext.Sessions
                .Where(m => m.Active == 1 && m.OfficeId == officeid)
                .OrderByDescending(m => m.StartAt)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> HasActiveSession(int? officeid)
        {
            return await controlDbContext.Sessions
                .Where(m => m.Active == 1 && m.OfficeId == officeid)
                 .OrderByDescending(m => m.StartAt)
                .AnyAsync();
        }

        public async Task<Session> GetByOfficeAndDateAsync(int officeid, DateTime fordate)
        {
            return await controlDbContext.Sessions
                .Where(m => m.OfficeId == officeid &&
                m.StartAt.Year == fordate.Date.Year
            && m.StartAt.Month == fordate.Date.Month
            && m.StartAt.Day == fordate.Date.Day
                )
                .FirstOrDefaultAsync();
        }


        public async Task<DateTime> GetCurrentSessionDateForProcess(int id)
        {
            return await controlDbContext.Sessions
                .Where(s => s.Id == id)
                .Select(s => s.StartAt)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetCurrentSessionMonthForProcess(int id)
        {
            return await controlDbContext.Sessions
                .Where(s => s.Id == id)
                .Select(s => s.StartAt.Month)
                .FirstOrDefaultAsync();
        }

        public async Task<DateTime?> IsRescueSessionThenDate(int id)
        {
            //return await controlDbContext.Sessions
            //    .Where(s => s.Id == id && s.Rescue == 1)
            //    .Select(s => s.StartAt)
            //    .FirstOrDefaultAsync();


            return await controlDbContext.Sessions
                        .Where(s => s.Id == id && s.Rescue == 1)
                        .Select(s => (DateTime?)s.StartAt.Date)
                        .FirstOrDefaultAsync();
        }
        public async Task<DateTime?> IsRescueSessionThenDateO(int id)
        {
            //return await controlDbContext.Sessions
            //    .Where(s => s.Id == id && s.Rescue == 1)
            //    .Select(s => s.StartAt)
            //    .FirstOrDefaultAsync();


            return await controlDbContext.Sessions
                        .Where(s => s.Id == id)
                        .Select(s => (DateTime?)s.StartAt.Date)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Session>> GetLast10SessionsForOffice(int officeId)
        {
            return await controlDbContext.Sessions
                .Where(m => m.OfficeId == officeId)
                .OrderByDescending(m => m.Id).Take(10)
                .ToListAsync();
        }

        public async Task<Session> HasLastSessionMatched(int officeId)
        {
           return await controlDbContext.Sessions
                .OrderByDescending(m => m.StartAt)
                .FirstOrDefaultAsync(m => m.OfficeId == officeId);

           
        }


        public async Task<Session> GetLastEndedSessionByOfficeAsync(int officeId, string Module)
        {
            return await controlDbContext.Sessions
               .Where(m => m.OfficeId == officeId && m.Module.Trim() == Module.Trim() && m.Active == 0)
               .OrderByDescending(m => m.StartAt)
               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Session>> GetLast2SessionsForOffice(int officeId)
        {
            return await controlDbContext.Sessions
                .Where(m => m.OfficeId == officeId)
                .OrderByDescending(m => m.Id).Take(2)
                .ToListAsync();
        }

        public async Task<IEnumerable<Session>> GetAllSessionsForOfficeByYearMonth(int officeId, int year, int month)
        {
            return await controlDbContext.Sessions
                .Where(m => m.OfficeId == officeId && m.StartAt.Year == year && m.StartAt.Month == month)
                .ToListAsync();
        }

        private ControlDbContext controlDbContext
        {
            get { return Context as ControlDbContext; }
        }
    }
}