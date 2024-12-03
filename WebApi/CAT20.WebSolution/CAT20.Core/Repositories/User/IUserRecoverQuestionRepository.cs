using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.User;

namespace CAT20.Core.Repositories.User
{
    public interface IUserRecoverQuestionRepository : IRepository<UserRecoverQuestion>
    {
        Task<IEnumerable<UserRecoverQuestion>> GetAllWithUserRecoverQuestionAsync();
        Task<UserRecoverQuestion> GetWithUserRecoverQuestionByIdAsync(int id);
        Task<IEnumerable<UserRecoverQuestion>> GetAllWithUserRecoverQuestionByUserRecoverQuestionIdAsync(int Id);
    }
}
