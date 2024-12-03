using CAT20.Core.HelperModels;
using CAT20.Core.Models.User;
using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.User
{
    public interface IUserDetailService
    {
        Task<IEnumerable<UserDetail>> GetAllUserDetails();
        Task<UserDetail> GetUserDetailById(int id);

        //------ [Start - GetUserDetailByUsername] ---
        Task<UserDetail> GetUserDetailByUsername(string username);
        //------ [End - GetUserDetailByUsername] -----

        Task<UserDetail> CreateUserDetail(UserDetail newUserDetail);
        Task UpdateUserDetail(UserDetail userDetailToBeUpdated, UserDetail userDetail);
        Task DeleteUserDetail(UserDetail userDetail);

        Task<UserDetail> GetUserDetailByUsernamePassword(UserDetail userDetail);
        Task<UserDetail> Authenticate (string username, string password);

        Task<IEnumerable<UserDetail>> GetAllUserDetailsForSabhaId(int id);
        Task<IEnumerable<UserDetail>> GetAllUserDetailsForOfficeId(int id);
        Task<IEnumerable<UserDetail>> GetAllUserDetailsForSabhaIdandOfficeId(int SabhaId, int OfficeId);

        Task <bool> IsPINValid(string username,string pin);
      //  Task<UserDetail> IsPINValid(string username, string pin);

        //--------
        Task<UserDetail> GetByNICAsync(int sabahId, string NIC);
        Task<UserDetail> GetByPhoneNoAsync(int sabahId, string PhoneNo);
        //--------
        Task<UserDetail> CreateUserImage(HUploadUserDocument obj, object environment, string _uploadsFolder);
        Task<UserDetail> CreateUserSignature(HUploadUserDocument obj, object environment, string _uploadsFolder);
        Task<UserDetail> GetUserImageById(int id);
        Task<UserDetail> GetUserSignById(int id);


    }
}

