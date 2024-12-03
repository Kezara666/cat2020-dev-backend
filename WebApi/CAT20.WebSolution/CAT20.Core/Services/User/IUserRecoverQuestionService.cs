using CAT20.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.User
{
    public interface IUserRecoverQuestionService
    {
        Task<IEnumerable<UserRecoverQuestion>> GetAllUserRecoverQuestions();
        Task<UserRecoverQuestion> GetUserRecoverQuestionById(int id);
        Task<UserRecoverQuestion> CreateUserRecoverQuestion(UserRecoverQuestion newUserRecoverQuestion);
        Task UpdateUserRecoverQuestion(UserRecoverQuestion userRecoverQuestionToBeUpdated, UserRecoverQuestion userRecoverQuestion);
        Task DeleteUserRecoverQuestion(UserRecoverQuestion userRecoverQuestion);
    }
}

