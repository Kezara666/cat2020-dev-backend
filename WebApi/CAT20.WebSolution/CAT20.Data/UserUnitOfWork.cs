using CAT20.Core;
using CAT20.Core.Repositories.User;
using CAT20.Data.Repositories.User;

namespace CAT20.Data
{
    public class UserUnitOfWork : IUserUnitOfWork
    {
        private readonly UserActivityDBContext _context;


        private UserDetailRepository _userDetailRepository;
        private UserRecoverQuestionRepository _userRecoverQuestionRepository;
        private AuditLogUserRepository _auditLogUserRepository;

        private RuleRepository _ruleRepository;
        private GroupRepository _groupRepository;
        private GroupUserRepository _groupUserRepository;
        private GroupRuleRepository _groupRuleRepository;
        private UserLoginActivityRepository _userLoginActivityRepository;
        public UserUnitOfWork(UserActivityDBContext context)
        {
            this._context = context;
        }

        public IUserDetailRepository UserDetails => _userDetailRepository = _userDetailRepository ?? new UserDetailRepository(_context);
        public IUserRecoverQuestionRepository UserRecoverQuestions => _userRecoverQuestionRepository = _userRecoverQuestionRepository ?? new UserRecoverQuestionRepository(_context);
        public IAuditLogUserRepository AuditLogs => _auditLogUserRepository = _auditLogUserRepository ?? new AuditLogUserRepository(_context);

        public IRuleRepository Rules => _ruleRepository = _ruleRepository ?? new RuleRepository(_context);
        public IGroupRepository Groups => _groupRepository = _groupRepository ?? new GroupRepository(_context);
        public IGroupUserRepository GroupUsers => _groupUserRepository = _groupUserRepository ?? new GroupUserRepository(_context);
        public IGroupRuleRepository GroupRules => _groupRuleRepository = _groupRuleRepository ?? new GroupRuleRepository(_context);
        public IUserLoginActivityRepository UserLoginActivitys => _userLoginActivityRepository = _userLoginActivityRepository ?? new UserLoginActivityRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            Dispose();
            await Task.CompletedTask;
        }

        //public void Dispose()
        //{
        //    _context.Dispose();
        //}
    }
}