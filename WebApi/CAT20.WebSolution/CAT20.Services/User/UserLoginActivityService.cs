using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.User;
using CAT20.Core.Services.User;

namespace CAT20.Services.User
{
    public class UserLoginActivityService : IUserLoginActivityService
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public UserLoginActivityService(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UserLoginActivity> CreateUserLoginActivity(UserLoginActivity newUserLoginActivity)
        {
            await _unitOfWork.UserLoginActivitys
                .AddAsync(newUserLoginActivity);
            await _unitOfWork.CommitAsync();

            return newUserLoginActivity;
        }
        public async Task DeleteUserLoginActivity(UserLoginActivity userLoginActivity)
        {
            _unitOfWork.UserLoginActivitys.Remove(userLoginActivity);

            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<UserLoginActivity>> GetAllUserLoginActivitys()
        {
            return await _unitOfWork.UserLoginActivitys.GetAllAsync();
        }

        public async Task<UserLoginActivity> GetLastUserLoginActivityForUserId(int id)
        {
            try
            {
                return await _unitOfWork.UserLoginActivitys.GetLastUserLoginActivityForUserId(id);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<UserLoginActivity>> GetAllLoginActivityByUserId(int UserId)
        {
            return await _unitOfWork.UserLoginActivitys.GetAllLoginActivityByUserIdAsync(UserId);
        }
        
        public async Task<UserLoginActivity> GetUserLoginActivityById(int id)
        {
            return await _unitOfWork.UserLoginActivitys.GetByIdAsync(id);
        }
        public async Task UpdateUserLoginActivity(UserLoginActivity userLoginActivityToBeUpdated, UserLoginActivity userLoginActivity)
        {
            //userLoginActivityToBeUpdated.Name = userLoginActivity.t;

            await _unitOfWork.CommitAsync();
        }

        public async Task Logout(UserLoginActivity userLoginActivityToBeUpdated)
        {
            userLoginActivityToBeUpdated.LogoutTime = System.DateTime.Now ;

            await _unitOfWork.CommitAsync();
        }

        public async Task updateUserLastActivity(UserLoginActivity userLoginActivityToBeUpdated, string rulename)
        {
            userLoginActivityToBeUpdated.LastActiveTime = System.DateTime.Now;
            userLoginActivityToBeUpdated.RuleName = rulename;

            await _unitOfWork.CommitAsync();
        }

    }
}