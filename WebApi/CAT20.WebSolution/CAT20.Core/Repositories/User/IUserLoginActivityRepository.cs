using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.User;

namespace CAT20.Core.Repositories.User
{
    public interface IUserLoginActivityRepository : IRepository<UserLoginActivity>
    {
        Task<IEnumerable<UserLoginActivity>> GetAllWithUserLoginActivityAsync();
        Task<UserLoginActivity> GetWithUserLoginActivityByIdAsync(int id);
        Task<IEnumerable<UserLoginActivity>> GetAllLoginActivityByUserIdAsync(int UserId);
        Task<UserLoginActivity> GetLastUserLoginActivityForUserId(int UserId);
    }
}
