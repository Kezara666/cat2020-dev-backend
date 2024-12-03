using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CAT20.Core
{
    public interface IUserUnitOfWork : IDisposable, IAsyncDisposable
    {

        IUserDetailRepository UserDetails { get; }
        IUserRecoverQuestionRepository UserRecoverQuestions { get; }
        IAuditLogUserRepository AuditLogs { get; }

        IRuleRepository Rules { get; }
        IGroupRepository Groups { get; }
        IGroupRuleRepository GroupRules { get; }
        IGroupUserRepository GroupUsers { get; }
        IUserLoginActivityRepository UserLoginActivitys { get; }

        Task<int> CommitAsync();
    }
}
