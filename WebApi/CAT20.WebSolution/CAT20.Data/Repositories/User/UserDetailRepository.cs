using CAT20.Core.Models.User;
using CAT20.Core.Repositories.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.User
{
    public class UserDetailRepository : Repository<UserDetail>, IUserDetailRepository
    {
        public UserDetailRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<UserDetail>> GetAllWithUserDetailAsync()
        {
            return await userActivityDbContext.UserDetails
                .ToListAsync();
        }

        public async Task<UserDetail> GetWithUserDetailByIdAsync(int id)
        {
            return await userActivityDbContext.UserDetails
                .Where(m => m.ID == id)
                .FirstOrDefaultAsync();
        }

        //------ [Start - GetWithUserDetailByUsernameAsync] -----
        public async Task<UserDetail> GetWithUserDetailByUsernameAsync(string username)
        {
            return await userActivityDbContext.UserDetails
                .Where(m => m.Username == username)
                .FirstOrDefaultAsync();
        }
        //------ [End - GetWithUserDetailByUsernameAsync] -----

        public async Task<UserDetail> GetWithUserDetailByUsernamePasswordAsync(UserDetail userDetail)
        {
            return await userActivityDbContext.UserDetails
                .Where(m => m.Username == userDetail.Username && m.Password == userDetail.Password)
                .FirstOrDefaultAsync();
        }

      

      

        public async Task<IEnumerable<UserDetail>> GetAllWithUserDetailByUserDetailIdAsync(int Id)
        {
            return await userActivityDbContext.UserDetails
                .Where(m => m.ID == Id)
                .ToListAsync();
        }

        public async Task<UserDetail> Authenticate(string username, string password)
        {
            return await userActivityDbContext.UserDetails
                .Where(m => m.Username == username.ToString() && m.Password == password.ToString() && m.ActiveStatus==1).FirstOrDefaultAsync();
        }

        public async Task<bool> IsPINValid(string username, string pin)
        {
            var user = await userActivityDbContext.UserDetails
                .Where(m => m.Username == username.ToString() && m.Pin == pin.ToString()).FirstOrDefaultAsync();

            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

      
        public async Task<IEnumerable<UserDetail>> GetAllUserDetailsForSabhaIdAsync(int sabhaID)
        {
            return await userActivityDbContext.UserDetails.Where(m => m.SabhaID == sabhaID && m.ActiveStatus == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserDetail>> GetAllUserDetailsForOfficeIdAsync(int Id)
        {
            return await userActivityDbContext.UserDetails.Where(m => m.OfficeID == Id && m.ActiveStatus == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserDetail>> GetAllUserDetailsForSabhaIdandOfficeIdAsync(int SabhaId, int OfficeId)
        {
            return await userActivityDbContext.UserDetails.Where(m => m.SabhaID == SabhaId && m.OfficeID == OfficeId && m.ActiveStatus == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserLoginActivity>> GetAllUserLoginActivitiesByUserIdAsync(int Id)
        {
            return await userActivityDbContext.UserLoginActivitys.Where(m => m.UserId == Id && m.IsSuccessLogin==1)
                .OrderByDescending(m => m.LoginTime)
                .ToListAsync();
        }

        //---
        public async Task<UserDetail> GetByNICAsync(int sabahId, string NIC)
        {
            return await userActivityDbContext.UserDetails
                .Where(m => m.NIC.Trim() == NIC.Trim() && m.ActiveStatus == 1 &&  m.SabhaID == sabahId).FirstOrDefaultAsync();
        }
        //---
        public async Task<UserDetail> GetByPhoneNoAsync(int sabahId, string PhoneNo)
        {
            return await userActivityDbContext.UserDetails
                .Where(m => m.ContactNo.Trim() == PhoneNo.Trim() && m.ActiveStatus == 1 && m.SabhaID == sabahId).FirstOrDefaultAsync();
        }
        //---
        private UserActivityDBContext userActivityDbContext
        {
            get { return Context as UserActivityDBContext; }
        }
    }
}