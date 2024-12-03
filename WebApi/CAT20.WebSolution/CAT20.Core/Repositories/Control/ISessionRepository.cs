using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.Control
{
    public interface ISessionRepository : IRepository<Session>
    {
        Task<Session> GetByIdAsync(int id);
        Task<Session> GetByOfficeAndModule(int officeid, string Module);
        Task<Session> GetAnyByOfficeAndModule(int? officeid, string Module);
        Task<Session> GetActiveOrLastActiveSessionForOnlinePaymentCurrentMonth(int? officeid, string Module);
        Task<IEnumerable<Session>> GetLast10SessionsForOffice(int officeId);
        Task<Session> GetByOfficeModuleAndDate(int officeid, string Module, DateTime date);
        Task<Session> GetByOfficeAndDateAsync(int officeid, DateTime date);
        Task<IEnumerable<Session>> GetAllActiveSessionsByOfficeAsync(int officeId);
        Task<IEnumerable<Session>> GetAllSessionsByOfficeAsync(int officeId);
        Task<IEnumerable<Session>> GetAllSessionsByOfficeAndModuleAsync(int officeId, string module);
        Task<Session> GetActiveSessionByOfficeAndModuleAsync(int officeId, string module);
        Task<Session> GetActiveSessionByOfficeAsync(int? officeId);
        Task<bool> HasActiveSession(int? officeId);

        Task<Session> HasLastSessionMatched(int officeId);

        Task<DateTime?> IsRescueSessionThenDate(int id);
        Task<DateTime?> IsRescueSessionThenDateO(int id);
        Task<DateTime> GetCurrentSessionDateForProcess(int id);
        Task<int> GetCurrentSessionMonthForProcess(int id);


        Task<Session> GetLastEndedSessionByOfficeAsync(int officeId, string Module);
        Task<IEnumerable<Session>> GetLast2SessionsForOffice(int officeId);

        Task<IEnumerable<Session>> GetAllSessionsForOfficeByYearMonth(int officeId, int year, int month);
    }
}
