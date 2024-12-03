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
    public class UserRecoverQuestionRepository : Repository<UserRecoverQuestion>, IUserRecoverQuestionRepository
    {
        public UserRecoverQuestionRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<UserRecoverQuestion>> GetAllWithUserRecoverQuestionAsync()
        {
            return await userActivityDbContext.UserRecoverQuestions
                .ToListAsync();
        }

        public async Task<UserRecoverQuestion> GetWithUserRecoverQuestionByIdAsync(int id)
        {
            return await userActivityDbContext.UserRecoverQuestions
                .Where(m => m.ID == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserRecoverQuestion>> GetAllWithUserRecoverQuestionByUserRecoverQuestionIdAsync(int Id)
        {
            return await userActivityDbContext.UserRecoverQuestions
                .Where(m => m.ID == Id)
                .ToListAsync();
        }

        private UserActivityDBContext userActivityDbContext
        {
            get { return Context as UserActivityDBContext; }
        }
    }
}