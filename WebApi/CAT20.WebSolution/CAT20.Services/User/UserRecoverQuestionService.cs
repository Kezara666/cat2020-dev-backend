using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.User;
using CAT20.Core.Services.User;

namespace CAT20.Services.User
{
    public class UserRecoverQuestionService : IUserRecoverQuestionService
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public UserRecoverQuestionService(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UserRecoverQuestion> CreateUserRecoverQuestion(UserRecoverQuestion newUserRecoverQuestion)
        {
            await _unitOfWork.UserRecoverQuestions
                .AddAsync(newUserRecoverQuestion);
            await _unitOfWork.CommitAsync();

            return newUserRecoverQuestion;
        }
        public async Task DeleteUserRecoverQuestion(UserRecoverQuestion userRecoverQuestion)
        {
            _unitOfWork.UserRecoverQuestions.Remove(userRecoverQuestion);

            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<UserRecoverQuestion>> GetAllUserRecoverQuestions()
        {
            return await _unitOfWork.UserRecoverQuestions.GetAllAsync();
        }
        public async Task<UserRecoverQuestion> GetUserRecoverQuestionById(int id)
        {
            return await _unitOfWork.UserRecoverQuestions.GetByIdAsync(id);
        }
        public async Task UpdateUserRecoverQuestion(UserRecoverQuestion userRecoverQuestionToBeUpdated, UserRecoverQuestion userRecoverQuestion)
        {
            //userRecoverQuestionToBeUpdated.Name = userRecoverQuestion.t;

            await _unitOfWork.CommitAsync();
        }
    }
}