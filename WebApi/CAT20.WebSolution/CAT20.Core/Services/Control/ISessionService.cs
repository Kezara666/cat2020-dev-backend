using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface ISessionService
    {
        Task<IEnumerable<Session>> GetAllActiveSessionsByOffice(int officeId);
        Task<IEnumerable<Session>> GetAllSessionsByOffice(int officeId);
        Task<IEnumerable<Session>> GetAllSessionsByOfficeAndModule(int officeId, string module);
        Task<IEnumerable<Session>> GetLast10SessionsForOffice(int officeId);
        Task<Session> GetActiveSessionByOfficeAndModule(int officeId, string module);
        Task<Session> GetActiveSessionByOffice(int officeId);
        Task<Session> GetById(int id);
        Task<Session> GetByOfficeAndModule(int officeid, string Module);
        Task<Session> GetAnyByOfficeAndModule(int? officeid, string Module);
        Task<Session> GetByOfficeModuleAndDate(int officeid, string Module, DateTime date);
        Task<Session> GetByOfficeAndDate(int officeid, DateTime date);
        Task<(bool, string?, Session)> Create(Session newSession, HTokenClaim token);
        Task<(bool, string)> EndSession(Session session, Session toupdate);
        Task AllowReceiptsForExpiredSession(Session session, Session toupdate);
        Task<DateTime?> IsRescueSessionThenDate(int sessionId);
        Task<Session> GetActiveOrLastActiveSessionForOnlinePaymentCurrentMonth(int? officeid, string Module);

        Task<Session> GetLastEndedSessionByOfficeAsync(int officeId, string Module);

        Task<IEnumerable<Session>> GetLast2SessionsForOffice(int officeId);

        Task<IEnumerable<Session>> GetAllSessionsForOfficeByYearMonth(int officeId, int year, int month);
    }
}

