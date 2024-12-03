using CAT20.Core.Models.User;
using CAT20.Core.Repositories.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.User
{
    public class UserLoginActivityRepository : Repository<UserLoginActivity>, IUserLoginActivityRepository
    {
        public UserLoginActivityRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<UserLoginActivity>> GetAllWithUserLoginActivityAsync()
        {
            return await userActivityDbContext.UserLoginActivitys
                .ToListAsync();
        }

        public async Task<UserLoginActivity> GetWithUserLoginActivityByIdAsync(int id)
        {
            return await userActivityDbContext.UserLoginActivitys
                .Where(m => m.ID == id)
             .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserLoginActivity>> GetAllLoginActivityByUserIdAsync(int UserId)
        {
            return await userActivityDbContext.UserLoginActivitys
                .Where(m => m.UserId == UserId)
                .ToListAsync();
        }

        public async Task<UserLoginActivity> GetLastUserLoginActivityForUserId(int id)
        {
            var retult = await userActivityDbContext.UserLoginActivitys
                .Where(m=> m.UserId==id)
                .OrderByDescending(m => m.ID)
                .FirstOrDefaultAsync();
            return retult;
        }

        private UserActivityDBContext userActivityDbContext
        {
            get { return Context as UserActivityDBContext; }
        }
    }
}