using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.User;
using CAT20.Core.Services.User;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Interfaces;
using System.Globalization;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.HelperModels;
using DocumentFormat.OpenXml.Spreadsheet;
using CAT20.Core.Models.Control;
using CAT20.Data.ShopRentalDb;

namespace CAT20.Services.User
{
    public class UserDetailService : IUserDetailService
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public UserDetailService(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UserDetail> CreateUserDetail(UserDetail newUserDetail)
        {
            try
            {
                await _unitOfWork.UserDetails.AddAsync(newUserDetail);
                await _unitOfWork.CommitAsync();

                #region AuditLog

                var _sb = new StringBuilder();
                _sb.Append("Created on " + DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                _sb.Append(" by System");

                await _unitOfWork.AuditLogs.AddAsync(new AuditLogUser
                {
                    Transaction = Transaction.User,
                    SourceID = newUserDetail.ID,
                    RecordDateTime = DateTime.Now,
                    Notes = _sb.ToString(),
                    UserID = newUserDetail.ID,
                });
                await _unitOfWork.CommitAsync();

                #endregion

            }
            catch (Exception e)
            {
                var exception = e.Message;
            }

            return newUserDetail;
        }
        public async Task DeleteUserDetail(UserDetail userDetail)
        {
            _unitOfWork.UserDetails.Remove(userDetail);
            await _unitOfWork.CommitAsync();

            #region AuditLog

            var _sb = new StringBuilder();
            _sb.Append("Deleted on " + DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            _sb.Append(" by System");

            await _unitOfWork.AuditLogs.AddAsync(new AuditLogUser
            {
                Transaction = Transaction.User,
                SourceID = userDetail.ID,
                //User = UserDetail,
                RecordDateTime = DateTime.Now,
                Notes = _sb.ToString(),
                UserID = userDetail.ID,
            });
            await _unitOfWork.CommitAsync();

            #endregion

        }
        public async Task<IEnumerable<UserDetail>> GetAllUserDetails()
        {
            try
            {
                return await _unitOfWork.UserDetails.GetAllAsync();
            }
            catch (Exception e) { return null; }
        }
        public async Task<UserDetail> GetUserDetailById(int id)
        {
            try
            {
                return await _unitOfWork.UserDetails.GetByIdAsync(id);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        //------ [Start - GetUserDetailByUsername] ---
        public async Task<UserDetail> GetUserDetailByUsername(string username)
        {
            return await _unitOfWork.UserDetails.GetWithUserDetailByUsernameAsync(username);
        }
        //------ [End - GetUserDetailByUsername] -----

        public async Task<UserDetail> GetUserDetailByUsernamePassword(UserDetail userDetail)
        {
            return await _unitOfWork.UserDetails.GetWithUserDetailByUsernamePasswordAsync(userDetail);
        }

        public async Task UpdateUserDetail(UserDetail userDetailToBeUpdated, UserDetail userDetail)
        {
            string currentpassword = userDetailToBeUpdated.Password;
            userDetailToBeUpdated.NameInFull = userDetail.NameInFull;
            userDetailToBeUpdated.NameWithInitials = userDetail.NameWithInitials;
            userDetailToBeUpdated.Password = userDetail.Password;
            userDetailToBeUpdated.NIC = userDetail.NIC;
            userDetailToBeUpdated.ContactNo = userDetail.ContactNo;
            userDetailToBeUpdated.Birthday = userDetail.Birthday;
            userDetailToBeUpdated.GenderID = userDetail.GenderID;
            userDetailToBeUpdated.Q1Id = userDetail.Q1Id;
            userDetailToBeUpdated.Answer1 = userDetail.Answer1;
            userDetailToBeUpdated.Q2Id = userDetail.Q2Id;
            userDetailToBeUpdated.Answer2 = userDetail.Answer2;
            userDetailToBeUpdated.ProfilePicPath=userDetail.ProfilePicPath;
            userDetailToBeUpdated.UserSignPath = userDetail.UserSignPath;
            #region AuditLog

            var _sb = new StringBuilder();
            if (currentpassword != userDetail.Password)
            {
                _sb.Append("Password Changed");
            }
            else
            {
                _sb.Append("User Details Updated");
            }

            await _unitOfWork.AuditLogs.AddAsync(new AuditLogUser
            {
                Transaction = Transaction.User,
                SourceID = userDetail.ID,
                RecordDateTime = DateTime.Now,
                Notes = _sb.ToString(),
                UserID = userDetail.ID,
            });
            await _unitOfWork.CommitAsync();

            #endregion
        }

        public async Task<UserDetail> Authenticate(string username, string password)
        {
            //await _unitOfWork.AuditLogs.AddAsync(new UserLoginActivity
            //{
            //    Username = username,
            //    SourceID = newUserDetail.ID,
            //    RecordDateTime = DateTime.Now,
            //    Notes = _sb.ToString(),
            //    UserID = newUserDetail.ID,
            //});
            try
            {
                return await _unitOfWork.UserDetails.Authenticate(username, password);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> IsPINValid(string username, string pin)
        {
            try
            {
                return await _unitOfWork.UserDetails.IsPINValid(username, pin);
            }
            catch (Exception ex)
            {
                return false;
            }
        }




        public async Task<IEnumerable<UserDetail>> GetAllUserDetailsForSabhaId(int id)
        {
            try
            {
                return await _unitOfWork.UserDetails.GetAllUserDetailsForSabhaIdAsync(id);
            }
            catch (Exception e) {
                return null;
            }
        }

        public async Task<IEnumerable<UserDetail>> GetAllUserDetailsForOfficeId(int id)
        {
            return await _unitOfWork.UserDetails.GetAllUserDetailsForOfficeIdAsync(id);
        }

        public async Task<IEnumerable<UserDetail>> GetAllUserDetailsForSabhaIdandOfficeId(int SabhaId, int OfficeId)
        {
            return await _unitOfWork.UserDetails.GetAllUserDetailsForSabhaIdandOfficeIdAsync(SabhaId, OfficeId);
        }

        //----
        public async Task<UserDetail> GetByNICAsync( int sabahId, string NIC)
        {
            return await _unitOfWork.UserDetails.GetByNICAsync(sabahId,NIC);
        }
        //---
        public async Task<UserDetail> GetByPhoneNoAsync( int sabahId, string PhoneNo)
        {
            return await _unitOfWork.UserDetails.GetByPhoneNoAsync(sabahId,PhoneNo);
        }
        //---
        public async Task<UserDetail> CreateUserImage(HUploadUserDocument obj, object environment, string _uploadsFolder)
        {
            string? filePath = null;

            try
            {
                var userDetail = await _unitOfWork.UserDetails.GetByIdAsync(obj.Id);

                if (userDetail != null)
                {



                    if (obj.File == null || obj.File.Length == 0)
                    {
                        return null;
                    }

                    if (!Directory.Exists(_uploadsFolder))
                    {
                        Directory.CreateDirectory(_uploadsFolder);
                    }

                    string uniqueFileName = "P_" +userDetail.ID+ "_" + Guid.NewGuid().ToString().Substring(0, 12) + "_" + obj.File.FileName;

                    filePath = Path.Combine(_uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await obj.File.CopyToAsync(stream);
                    }

                    userDetail.ProfilePicPath = uniqueFileName;

                    //await _unitOfWork.UserDetails.AddAsync(obj);
                    await _unitOfWork.CommitAsync();

                    return userDetail;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            return null;
        

        }

        public async Task<UserDetail> CreateUserSignature(HUploadUserDocument obj, object environment, string _uploadsFolder)
        {
            string? filePath = null;

            try
            {
                var userDetail = await _unitOfWork.UserDetails.GetByIdAsync(obj.Id);

                if (userDetail != null)
                {



                    if (obj.File == null || obj.File.Length == 0)
                    {
                        return null;
                    }

                    if (!Directory.Exists(_uploadsFolder))
                    {
                        Directory.CreateDirectory(_uploadsFolder);
                    }

                    string uniqueFileName = "S_" + userDetail.ID + "_" + Guid.NewGuid().ToString().Substring(0, 12) + "_" + obj.File.FileName;

                    filePath = Path.Combine(_uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await obj.File.CopyToAsync(stream);
                    }

                    userDetail.UserSignPath = uniqueFileName;

                    await _unitOfWork.CommitAsync();

                    return userDetail;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            return null;


        }
        
        public async Task<UserDetail> GetUserImageById(int id)
        {
            var userDetail = await _unitOfWork.UserDetails.GetByIdAsync(id);

            if (userDetail == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            return userDetail;
         
        }


        public async Task<UserDetail> GetUserSignById(int id)
        {
            var userDetail = await _unitOfWork.UserDetails.GetByIdAsync(id);

            if (userDetail == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            return userDetail;
        }
    }
}