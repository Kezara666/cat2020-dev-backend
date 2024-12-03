using CAT20.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.User
{
    public interface IUserLoginActivityService
    {
        Task<IEnumerable<UserLoginActivity>> GetAllUserLoginActivitys();
        Task<IEnumerable<UserLoginActivity>> GetAllLoginActivityByUserId(int userid);
        Task<UserLoginActivity> GetLastUserLoginActivityForUserId(int id);
        Task<UserLoginActivity> GetUserLoginActivityById(int id);
        Task<UserLoginActivity> CreateUserLoginActivity(UserLoginActivity newUserLoginActivity);
        Task UpdateUserLoginActivity(UserLoginActivity userLoginActivityToBeUpdated, UserLoginActivity userLoginActivity);
        Task Logout(UserLoginActivity userLoginActivityToBeUpdated);
        Task updateUserLastActivity(UserLoginActivity userLoginActivityToBeUpdated, string rulename);
        Task DeleteUserLoginActivity(UserLoginActivity userLoginActivity);
    }
}

