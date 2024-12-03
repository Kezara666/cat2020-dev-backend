using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.Models.User;
using CAT20.Core.Services.User;
using CAT20.WebApi.Resources.User;
using CAT20.WebApi.Resources.User.Save;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using CAT20.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using CAT20.Core.Services.Control;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using CAT20.WebApi.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CAT20.WebApi.Controllers;
using CAT20.Common.Utility;
using CAT20.Core.Models.Mixin;
using CAT20.WebApi.Resources.Mixin;
using CAT20.Services.User;
using CAT20.Core.Services.Mixin;
using CAT20.Core.HelperModels;
using DocumentFormat.OpenXml.InkML;
using CAT20.Core.Models.WaterBilling;
using CAT20.WebApi.Resources.WaterBilling;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Bcpg;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using CAT20.Core.HelperModels;
using CAT20.WebApi.Configuration;
using Microsoft.Extensions.Options;
using System.Configuration;

namespace CAT20.WebApi.Controllers.Control
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserDetailService _userDetailService;
        private readonly IMapper _mapper;
        public IConfiguration _configuration;
        private readonly IEmailOutBoxService _emailOutBoxService;
        private readonly IEmailConfigurationService _emailConfigurationService;
        private readonly IOfficeService _officeService;
        private readonly IOfficeTypeService _officeTypeService;
        private readonly IUserLoginActivityService _userLoginActivityService;
        private readonly IMixinOrderService _mixinOrderService;
        private readonly IWebHostEnvironment _environment;
        private readonly string _uploadsFolder;

        public UsersController(IWebHostEnvironment environment, IOptions<AppSettings> appSettings, IUserDetailService userDetailService, IMapper mapper, IConfiguration config, IEmailOutBoxService emailOutBoxService, IEmailConfigurationService emailConfigurationService, IOfficeService officeService, IOfficeTypeService officeTypeService, IUserLoginActivityService userLoginActivityService, IMixinOrderService mixinOrderService)
        {
            _mapper = mapper;
            _userDetailService = userDetailService;
            _configuration = config;
            _emailOutBoxService = emailOutBoxService;
            _emailConfigurationService = emailConfigurationService;
            _officeService = officeService;
            _officeTypeService = officeTypeService;
            _userLoginActivityService = userLoginActivityService;
            _mixinOrderService = mixinOrderService;

            _environment = environment;
            _uploadsFolder = appSettings.Value.UploadsFolder;
        }

        [HttpGet]
        [Route("getAllUsers/{sabahaID}")]
        public async Task<ActionResult<IEnumerable<UserDetailBasicResource>>> GetAllUserDetailsForSabhaId([FromRoute] int sabahaID)
        {
            var userDetails = await _userDetailService.GetAllUserDetailsForSabhaId(sabahaID);
            var userDetailResources = _mapper.Map<IEnumerable<UserDetail>, IEnumerable<UserDetailBasicResource>>(userDetails).ToList();
            if (userDetailResources.Count > 0)
            {
                for (int i = 0; i < userDetailResources.Count; i++)
                {
                    if (userDetailResources[i].OfficeID != null && userDetailResources[i].OfficeID != 0)
                    {
                        userDetailResources[i].Office = await _officeService.GetOfficeById(userDetailResources[i].OfficeID.Value);
                        //userDetailResources[i].Office.officeType = await _officeTypeService.GetOfficeTypeById(userDetailResources[i].Office.OfficeTypeID.Value);
                    }
                }
            }
            return Ok(userDetailResources);
        }

        [HttpGet]
        [Route("getUserById/{id}")]
        public async Task<ActionResult<UserDetailResource>> GetUserDetailById([FromRoute] int id)
        {
            var userDetail = await _userDetailService.GetUserDetailById(id);
            var userDetailResource = _mapper.Map<UserDetail, UserDetailResource>(userDetail);
            return Ok(userDetailResource);
        }

        //[HttpPut("")]
        //public async Task<ActionResult<UserDetailResource>> GetUserDetailByUsernamePassword(UserDetailResource saveUserDetailResource)
        //{
        //    var userDetail = _mapper.Map< UserDetailResource, UserDetail>(saveUserDetailResource);
        //    var userDetailForsave = await _userDetailService.GetUserDetailByUsernamePassword(userDetail);
        //    var userDetailResource = _mapper.Map<UserDetail, UserDetailResource>(userDetailForsave);
        //    return Ok(userDetailResource);
        //}

        //[HttpPost("")]
        //public async Task<ActionResult<UserDetailResource>> GetToken(UserDetailResource _userData)
        //{
        //    if (_userData != null && _userData.Username != null && _userData.Password != null)
        //    {
        //        var userDetail = _mapper.Map<UserDetailResource, UserDetail>(_userData);
        //        var userDetailForsave = await _userDetailService.GetUserDetailByUsernamePassword(userDetail);
        //        var userDetailResource = _mapper.Map<UserDetail, UserDetailResource>(userDetailForsave);

        //        if (userDetailResource != null)
        //        {
        //            //create claims details based on the user information
        //            var claims = new[] {
        //                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
        //                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        //                //new Claim("SabhaID", userDetailResource.SabhaID.ToString()),
        //                //new Claim("OfficeID", userDetailResource.OfficeID.ToString()),
        //                new Claim("UserID", userDetailResource.ID.ToString()),
        //                new Claim("NameWithInitials", userDetailResource.NameWithInitials),
        //                new Claim("UserName", userDetailResource.Username)
        //            };

        //            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        //            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //            var token = new JwtSecurityToken(
        //                _configuration["Jwt:Issuer"],
        //                _configuration["Jwt:Audience"],
        //                claims,
        //                expires: DateTime.UtcNow.AddMinutes(10),
        //                signingCredentials: signIn);

        //            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        //        }
        //        else
        //        {
        //            return BadRequest("Invalid credentials");
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpPost("saveUser")]
        public async Task<ActionResult<UserDetailResource>> CreateUserDetail([FromBody] SaveUserDetailResource saveUserDetailResource)
        {
            saveUserDetailResource.ActiveStatus = 1;
            //var validator = new SaveUserDetailResourceValidator();
            //var validationResult = await validator.ValidateAsync(saveUserDetailResource);

            //if (!validationResult.IsValid)
            //    return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            if (saveUserDetailResource.Password == string.Empty)
                saveUserDetailResource.Password = RandomPassword(saveUserDetailResource.Username);

            CryptoProvider crypto = new CryptoProvider();

            var userDetailToCreate = _mapper.Map<SaveUserDetailResource, UserDetail>(saveUserDetailResource);
            userDetailToCreate.Password = crypto.GetHash(saveUserDetailResource.Password.Trim());

            var newUserDetail = await _userDetailService.CreateUserDetail(userDetailToCreate);

            var userDetail = await _userDetailService.GetUserDetailById(newUserDetail.ID);

            var userDetailResource = _mapper.Map<UserDetail, UserDetailResource>(userDetail);

            #region Create Email Record
            if (userDetail != null)
            {
                try
                {
                    var emailSettings = await _emailConfigurationService.GetEmailConfigurationById(1);
                    await _emailOutBoxService.CreateEmailOutBox(new Core.Models.Control.EmailOutBox
                    {
                        Attachment = string.Empty,
                        Bc = string.Empty,
                        Cc = string.Empty,
                        CreatedByID = newUserDetail.ID,
                        EmailSendAttempts = 0,
                        EmailStatus = Core.Models.Enums.EmailStatus.Pending,
                        IsBodyHtml = true,
                        MailContent = createUserMailBody(userDetail.Username, saveUserDetailResource.Password, emailSettings.SystemURL),
                        Note = string.Empty,
                        Recipient = userDetail.Username,
                        Subject = "New User Account"
                    });
                }
                catch (Exception e)
                {
                }

            }
            #endregion
            EmailService email = new EmailService(_emailConfigurationService, _emailOutBoxService);
            await email.sendMail();

            return Ok(userDetailResource);
        }

        [HttpPost("updateUser")]
        public async Task<ActionResult<UserDetailResource>> UpdateUser([FromBody] SaveUserDetailResource saveUserDetailResource)
        {
            //var validator = new SaveUserDetailResourceValidator();
            //var validationResult = await validator.ValidateAsync(saveUserDetailResource);

            //var requestIsInvalid = saveUserDetailResource.ID == 0 || !validationResult.IsValid;

            //if (requestIsInvalid)
            //    return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var userDetailToBeUpdate = await _userDetailService.GetUserDetailById(saveUserDetailResource.ID);

            if (userDetailToBeUpdate == null)
                return NotFound();


            var userDetail = _mapper.Map<SaveUserDetailResource, UserDetail>(saveUserDetailResource);

            if (userDetailToBeUpdate.Password != saveUserDetailResource.Password)
            {
                CryptoProvider crypto = new CryptoProvider();
                userDetail.Password = crypto.GetHash(saveUserDetailResource.Password.Trim());
            }

            await _userDetailService.UpdateUserDetail(userDetailToBeUpdate, userDetail);
            var updatedUserDetail = await _userDetailService.GetUserDetailById(saveUserDetailResource.ID);
            var updatedUserDetailResource = _mapper.Map<UserDetail, UserDetailResource>(updatedUserDetail);

            if (userDetailToBeUpdate.Password != saveUserDetailResource.Password)
            {
                #region Create Email Record for Changed Password
                if (userDetail != null)
                {
                    try
                    {
                        var emailSettings = await _emailConfigurationService.GetEmailConfigurationById(1);
                        await _emailOutBoxService.CreateEmailOutBox(new Core.Models.Control.EmailOutBox
                        {
                            Attachment = string.Empty,
                            Bc = string.Empty,
                            Cc = string.Empty,
                            CreatedByID = updatedUserDetail.ID,
                            EmailSendAttempts = 0,
                            EmailStatus = Core.Models.Enums.EmailStatus.Pending,
                            IsBodyHtml = true,
                            MailContent = changedPasswordMailBody(userDetail.NameWithInitials, userDetail.Username, saveUserDetailResource.Password, emailSettings.SystemURL),
                            Note = string.Empty,
                            Recipient = userDetail.Username,
                            Subject = "Password Changed - CAT2020.lk"
                        });
                    }
                    catch (Exception e)
                    {
                    }

                }
                #endregion
                EmailService email = new EmailService(_emailConfigurationService, _emailOutBoxService);
                await email.sendMail();
            }

            return Ok(updatedUserDetailResource);
        }

        [HttpDelete("deleteUser")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id == 0)
                return BadRequest();

            var userDetail = await _userDetailService.GetUserDetailById(id);

            if (userDetail == null)
                return NotFound();

            await _userDetailService.DeleteUserDetail(userDetail);

            return NoContent();
        }

        private string createUserMailBody(string userName, string password, string url)
        {
            var _sb = new StringBuilder();

            _sb.Append("<!DOCTYPE html> ");
            _sb.Append("<html>");
            _sb.Append("<head> ");
            _sb.Append("<title></title> ");
            _sb.Append("<meta charset = 'utf-8' /> ");
            _sb.Append("<style type='text/css'> ");
            _sb.Append(".outer_table { ");
            _sb.Append("    border-bottom: #58d0da solid 1px;");
            _sb.Append("    border-left: #58d0da solid 1px;  ");
            _sb.Append("    border-right: #58d0da solid 1px; ");
            _sb.Append("} ");
            _sb.Append("  ");
            _sb.Append(".outer_table_td { ");
            _sb.Append("    background-color: #c0edf1; ");
            _sb.Append("    height: 40px;              ");
            _sb.Append("    font-family: Tahoma;       ");
            _sb.Append("    font-size: 14px;           ");
            _sb.Append("    font-size-adjust: none;    ");
            _sb.Append("    font-weight: bold;         ");
            _sb.Append("}");
            _sb.Append(" ");
            _sb.Append(".txt_normal { ");
            _sb.Append("    font-family: Tahoma; ");
            _sb.Append("    font-size: 11px; ");
            _sb.Append("    font-size-adjust: none; ");
            _sb.Append("    color: #585858; ");
            _sb.Append("    height: 18px; ");
            _sb.Append("} ");
            _sb.Append(" ");
            _sb.Append(".inner_table_td_green { ");
            _sb.Append("    background-color: #cee6e8; ");
            _sb.Append("    font-family: Tahoma; ");
            _sb.Append("    font-size: 11px; ");
            _sb.Append("    color: #000000; ");
            _sb.Append("    height: 22px; ");
            _sb.Append("    text-indent: 5px; ");
            _sb.Append("} ");
            _sb.Append("");
            _sb.Append(" .inner_table_td_blue { ");
            _sb.Append("     background-color: #f3f3f3; ");
            _sb.Append("     font-family: Tahoma; ");
            _sb.Append("     font-size: 11px;");
            _sb.Append("     color: #045d86;");
            _sb.Append("     height: 22px;");
            _sb.Append("     text-indent: 10px; ");
            _sb.Append(" } ");
            _sb.Append("</style> ");
            _sb.Append(" </head> ");
            _sb.Append("<body> ");
            _sb.Append("<table border = '0' cellspacing='0' cellpadding='0' Class='outer_table'> ");
            _sb.Append("<tbody> ");
            _sb.Append("<tr> ");
            _sb.Append("<td Class='outer_table_td' style='background-color:#58d0da' width='5'>&nbsp;</td> ");
            _sb.Append("<td align='left' valign='middle' Class='outer_table_td' style='padding-left:10px'>");
            _sb.Append("    Notification - New User Account");
            _sb.Append("</td> ");
            _sb.Append("<td Class='outer_table_td' width='5'>&nbsp;</td> ");
            _sb.Append("</tr> ");
            _sb.Append("<tr> ");
            _sb.Append("    <td colspan='3' >&nbsp;</td> ");
            _sb.Append("</tr> ");
            _sb.Append("<tr> ");
            _sb.Append("<td>&nbsp;</td> ");
            _sb.Append("<td align='left' valign='top' style='padding-left:10px; padding-right:10px'> ");
            _sb.Append("    <Table width='100%' border='0' cellspacing='2' cellpadding='2'> ");
            _sb.Append("        <tbody> ");

            _sb.Append("<tr> ");
            _sb.Append("<td Class='txt_normal'> ");
            _sb.Append("                Dear &nbsp; User,<br> ");
            _sb.Append("                <br> ");
            _sb.Append("            </td> ");
            _sb.Append("        </tr> ");
            _sb.Append("        <tr> ");

            _sb.Append("<td Class='txt_normal'>");
            _sb.Append("");
            _sb.Append("                 Rensponse by CAT20 Automation System. ");
            _sb.Append("                <br> ");
            _sb.Append("    <br> ");
            _sb.Append("            </td> ");
            _sb.Append("        </tr> ");

            _sb.Append("    </tbody> ");
            _sb.Append("</table> ");
            _sb.Append("<Table width = '100%' border='0' cellspacing='2' cellpadding='2'> ");
            _sb.Append("    <tbody> ");
            _sb.Append("        <tr> ");
            _sb.Append("    <td Class='inner_table_td_green'>URL</td> ");
            _sb.Append("            <td Class='inner_table_td_blue'><a href='" + url + "'>CAT2020.lk</a></td> ");
            _sb.Append("        </tr> ");

            _sb.Append("        <tr> ");
            _sb.Append("            <td Class='inner_table_td_green'>User Name</td> ");
            _sb.Append("            <td Class='inner_table_td_blue'>" + userName + "</td> ");
            _sb.Append("        </tr> ");

            _sb.Append("        <tr> ");
            _sb.Append("                    <td Class='inner_table_td_green'>Password</td> ");
            _sb.Append("            <td Class='inner_table_td_blue'>" + password + "  </td> ");
            _sb.Append("        </tr> ");
            _sb.Append("            </tbody> ");
            _sb.Append("        </table> ");
            _sb.Append("        <Table width = '100%' border='0' cellspacing='2' cellpadding='2'> ");
            _sb.Append("            <tbody> ");
            _sb.Append("                                    <tr> ");
            _sb.Append("                                    <td>&nbsp;</td> ");
            _sb.Append("                </tr> ");
            _sb.Append("                <tr> ");
            _sb.Append("                                    <td Class='txt_normal'> ");
            _sb.Append("                        This is an auto generated email sent to you from CAT20. Please do not reply to this email. ");
            _sb.Append("                    </td> ");
            _sb.Append("                </tr> ");
            _sb.Append("                <tr> ");
            _sb.Append("                                        <td Class='txt_normal'> ");
            _sb.Append("                        Regards,<br> ");
            _sb.Append("                        System Admin ");
            _sb.Append("                    </td> ");
            _sb.Append("                </tr> ");
            _sb.Append("            </tbody> ");
            _sb.Append("        </table> ");
            _sb.Append("    </td> ");
            _sb.Append("    <td align = 'left' valign='top'>&nbsp;</td> ");
            _sb.Append("</tr> ");
            _sb.Append("<tr> ");
            _sb.Append("                                            <td colspan = '3' >&nbsp;</td> ");
            _sb.Append("</tr> ");
            _sb.Append("</tbody> ");
            _sb.Append("</table> ");
            _sb.Append("</body> ");
            _sb.Append("</html> ");

            return _sb.ToString();
        }


        private string changedPasswordMailBody(string namewithInitials, string userName, string password, string url)
        {
            var _sb = new StringBuilder();

            _sb.Append("<!DOCTYPE html> ");
            _sb.Append("<html>");
            _sb.Append("<head> ");
            _sb.Append("<title></title> ");
            _sb.Append("<meta charset = 'utf-8' /> ");
            _sb.Append("<style type='text/css'> ");
            _sb.Append(".outer_table { ");
            _sb.Append("    border-bottom: #58d0da solid 1px;");
            _sb.Append("    border-left: #58d0da solid 1px;  ");
            _sb.Append("    border-right: #58d0da solid 1px; ");
            _sb.Append("} ");
            _sb.Append("  ");
            _sb.Append(".outer_table_td { ");
            _sb.Append("    background-color: #c0edf1; ");
            _sb.Append("    height: 40px;              ");
            _sb.Append("    font-family: Tahoma;       ");
            _sb.Append("    font-size: 14px;           ");
            _sb.Append("    font-size-adjust: none;    ");
            _sb.Append("    font-weight: bold;         ");
            _sb.Append("}");
            _sb.Append(" ");
            _sb.Append(".txt_normal { ");
            _sb.Append("    font-family: Tahoma; ");
            _sb.Append("    font-size: 11px; ");
            _sb.Append("    font-size-adjust: none; ");
            _sb.Append("    color: #585858; ");
            _sb.Append("    height: 18px; ");
            _sb.Append("} ");
            _sb.Append(" ");
            _sb.Append(".inner_table_td_green { ");
            _sb.Append("    background-color: #cee6e8; ");
            _sb.Append("    font-family: Tahoma; ");
            _sb.Append("    font-size: 11px; ");
            _sb.Append("    color: #000000; ");
            _sb.Append("    height: 22px; ");
            _sb.Append("    text-indent: 5px; ");
            _sb.Append("} ");
            _sb.Append("");
            _sb.Append(" .inner_table_td_blue { ");
            _sb.Append("     background-color: #f3f3f3; ");
            _sb.Append("     font-family: Tahoma; ");
            _sb.Append("     font-size: 11px;");
            _sb.Append("     color: #045d86;");
            _sb.Append("     height: 22px;");
            _sb.Append("     text-indent: 10px; ");
            _sb.Append(" } ");
            _sb.Append("</style> ");
            _sb.Append(" </head> ");
            _sb.Append("<body> ");
            _sb.Append("<table border = '0' cellspacing='0' cellpadding='0' Class='outer_table'> ");
            _sb.Append("<tbody> ");
            _sb.Append("<tr> ");
            _sb.Append("<td Class='outer_table_td' style='background-color:#58d0da' width='5'>&nbsp;</td> ");
            _sb.Append("<td align='left' valign='middle' Class='outer_table_td' style='padding-left:10px'>");
            _sb.Append("    Notification - Password Changed");
            _sb.Append("</td> ");
            _sb.Append("<td Class='outer_table_td' width='5'>&nbsp;</td> ");
            _sb.Append("</tr> ");
            _sb.Append("<tr> ");
            _sb.Append("    <td colspan='3' >&nbsp;</td> ");
            _sb.Append("</tr> ");
            _sb.Append("<tr> ");
            _sb.Append("<td>&nbsp;</td> ");
            _sb.Append("<td align='left' valign='top' style='padding-left:10px; padding-right:10px'> ");
            _sb.Append("    <Table width='100%' border='0' cellspacing='2' cellpadding='2'> ");
            _sb.Append("        <tbody> ");

            _sb.Append("<tr> ");
            _sb.Append("<td Class='txt_normal'> ");
            _sb.Append("                Dear " + namewithInitials + ",<br> ");
            _sb.Append("                <br> ");
            _sb.Append("            </td> ");
            _sb.Append("        </tr> ");
            _sb.Append("        <tr> ");

            _sb.Append("<td Class='txt_normal'>");
            _sb.Append("");
            _sb.Append("                 Rensponse by CAT20 Automation System. ");
            _sb.Append("                <br> ");
            _sb.Append("    <br> ");
            _sb.Append("            </td> ");
            _sb.Append("        </tr> ");

            _sb.Append("    </tbody> ");
            _sb.Append("</table> ");
            _sb.Append("<Table width = '100%' border='0' cellspacing='2' cellpadding='2'> ");
            _sb.Append("    <tbody> ");
            _sb.Append("        <tr> ");
            _sb.Append("    <td Class='inner_table_td_green'>URL</td> ");
            _sb.Append("            <td Class='inner_table_td_blue'><a href='" + url + "'>CAT2020.lk</a></td> ");
            _sb.Append("        </tr> ");

            _sb.Append("        <tr> ");
            _sb.Append("            <td Class='inner_table_td_green'>User Name</td> ");
            _sb.Append("            <td Class='inner_table_td_blue'>" + userName + "</td> ");
            _sb.Append("        </tr> ");

            //_sb.Append("        <tr> ");
            //_sb.Append("                    <td Class='inner_table_td_green'>Password</td> ");
            //_sb.Append("            <td Class='inner_table_td_blue'>" + password + "  </td> ");
            //_sb.Append("        </tr> ");
            _sb.Append("            </tbody> ");
            _sb.Append("        </table> ");
            _sb.Append("        <Table width = '100%' border='0' cellspacing='2' cellpadding='2'> ");
            _sb.Append("            <tbody> ");
            _sb.Append("                                    <tr> ");
            _sb.Append("                                    <td>&nbsp;</td> ");
            _sb.Append("                </tr> ");
            _sb.Append("                <tr> ");
            _sb.Append("                                    <td Class='txt_normal'> ");
            _sb.Append("                        This is an auto generated email sent to you from CAT20. Please do not reply to this email. ");
            _sb.Append("                    </td> ");
            _sb.Append("                </tr> ");
            _sb.Append("                <tr> ");
            _sb.Append("                                        <td Class='txt_normal'> ");
            _sb.Append("                        Regards,<br> ");
            _sb.Append("                        System Admin ");
            _sb.Append("                    </td> ");
            _sb.Append("                </tr> ");
            _sb.Append("            </tbody> ");
            _sb.Append("        </table> ");
            _sb.Append("    </td> ");
            _sb.Append("    <td align = 'left' valign='top'>&nbsp;</td> ");
            _sb.Append("</tr> ");
            _sb.Append("<tr> ");
            _sb.Append("                                            <td colspan = '3' >&nbsp;</td> ");
            _sb.Append("</tr> ");
            _sb.Append("</tbody> ");
            _sb.Append("</table> ");
            _sb.Append("</body> ");
            _sb.Append("</html> ");

            return _sb.ToString();
        }

        private string RandomPassword(string name)
        {
            var builder = new StringBuilder();
            try
            {
                char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

                builder.Append(name.Substring(1, 1).ToUpper());
                builder.Append(DateTime.Now.Second.ToString().Substring(1, 1));
                builder.Append(DateTime.Now.Day.ToString().Substring(0, 1));
                builder.Append(alpha[DateTime.Now.Hour]);
                builder.Append(name.Substring(name.Length - 2, 1).ToLower());
                builder.Append(DateTime.Now.Millisecond.ToString().Substring(0, 1));

            }
            catch (Exception e)
            {
                var message = e.Message;
            }
            return builder.ToString();
        }


        [HttpGet]
        [Route("getLastUserLoginActivityForUserId/{id}")]
        public async Task<ActionResult<UserLoginActivity>> GetLastUserLoginActivityForUserId(int id)
        {
            var returnActivity = await _userLoginActivityService.GetLastUserLoginActivityForUserId(id); ;

            return Ok(returnActivity);
        }

        [HttpGet]
        [Route("logout/{id}")]
        public async Task<ActionResult<UserLoginActivity>> Logout(int id)
        {
            try
            {
                var userActivityToBeUpdate = await _userLoginActivityService.GetLastUserLoginActivityForUserId(id);

                if (userActivityToBeUpdate == null)
                    return NotFound();

                await _userLoginActivityService.Logout(userActivityToBeUpdate);
                var updatedUserActivity = await _userLoginActivityService.GetLastUserLoginActivityForUserId(id);

                return Ok(updatedUserActivity);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("updateUserLastActivity/{id}/{rulename}")]
        public async Task<ActionResult<UserLoginActivity>> UpdateUserLastActivity(int id, string rulename)
        {
            try
            {
                var userActivityToBeUpdate = await _userLoginActivityService.GetLastUserLoginActivityForUserId(id);

                if (userActivityToBeUpdate == null)
                    return NotFound();

                await _userLoginActivityService.updateUserLastActivity(userActivityToBeUpdate, rulename);
                var updatedUserActivity = await _userLoginActivityService.GetLastUserLoginActivityForUserId(id);

                return Ok(updatedUserActivity);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("getAllReceiptCreatedUsersForOffice/{officeid}")]
        public async Task<ActionResult<IEnumerable<UserDetailBasicResource>>> GetAllReceiptCreatedUsersForOffice([FromRoute] int officeid)
        {
            var createdbydistictOrders = await _mixinOrderService.GetAllReceiptCreatedUsersForOffice(officeid);

            List<UserDetail> ReceiptCreatedUsersList = new List<UserDetail>();
            foreach (var item in createdbydistictOrders)
            {
                if(item.CreatedBy==-1 || item.CreatedBy > 0)
                { 
                var userDetail = await _userDetailService.GetUserDetailById(item.CreatedBy);
                ReceiptCreatedUsersList.Add(userDetail);
                }
            }

            var userDetailResources = _mapper.Map<IEnumerable<UserDetail>, IEnumerable<UserDetailBasicResource>>(ReceiptCreatedUsersList).ToList();
            if (userDetailResources.Count > 0)
            {
                for (int i = 0; i < userDetailResources.Count; i++)
                {
                    if (userDetailResources[i].OfficeID != null && userDetailResources[i].OfficeID != 0)
                    {
                        userDetailResources[i].Office = await _officeService.GetOfficeById(userDetailResources[i].OfficeID.Value);
                    }
                }
            }
            return Ok(userDetailResources);
        }

        [HttpGet]
        [Route("getAllReceiptCreatedUsersForSabha/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<UserDetailBasicResource>>> GetAllReceiptCreatedUsersForSabha([FromRoute] int sabhaid)
        {


            var officelist = await _officeService.getAllOfficesForSabhaId(sabhaid);
            List<UserDetail> ReceiptCreatedUsersList = new List<UserDetail>();

            foreach (var office in officelist)
            {
                var createdbydistictOrders = await _mixinOrderService.GetAllReceiptCreatedUsersForOffice(office.ID.Value);
                foreach (var item in createdbydistictOrders)
                {
                    var userDetail = await _userDetailService.GetUserDetailById(item.CreatedBy);
                    ReceiptCreatedUsersList.Add(userDetail);
                }

            }

            var userDetailResources = _mapper.Map<IEnumerable<UserDetail>, IEnumerable<UserDetailBasicResource>>(ReceiptCreatedUsersList).ToList();
            if (userDetailResources.Count > 0)
            {
                for (int i = 0; i < userDetailResources.Count; i++)
                {
                    if (userDetailResources[i].OfficeID != null && userDetailResources[i].OfficeID != 0)
                    {

                        userDetailResources[i].Office = await _officeService.GetOfficeById(userDetailResources[i].OfficeID.Value);
                    }
                }
            }
            return Ok(userDetailResources);
        }

        //---
        [HttpGet]
        [Route("getUserByPhoneNo/{phoneNo}")]
        public async Task<ActionResult<UserDetailResource>> GetUserDetailByPhoneNo([FromRoute] string phoneNo)
        {
            try
            {
                var _token = new HTokenClaim
                {

                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                };

                if (phoneNo != null && phoneNo.Length >= 10)
                {
                    var userDetail = await _userDetailService.GetByPhoneNoAsync(_token.sabhaId,phoneNo);
                    var userDetailResource = _mapper.Map<UserDetail, UserDetailResource>(userDetail);

                    if (userDetailResource == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Ok(userDetailResource);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }catch (Exception ex)
            {
                return BadRequest();
            }
        }
        //---

        //---
        [HttpGet]
        [Route("getUserByNIC/{nic}")]
        public async Task<ActionResult<UserDetailResource>> GetUserDetailByNIC([FromRoute] string nic)
        {
            try
            {
                var _token = new HTokenClaim
                {

                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                };
                if (nic != null && nic.Length >= 10)
                {
                    var userDetail = await _userDetailService.GetByNICAsync(_token.sabhaId,nic);
                    var userDetailResource = _mapper.Map<UserDetail, UserDetailResource>(userDetail);

                    if (userDetailResource == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Ok(userDetailResource);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest(); 
            }
        }
        //---
        [HttpPost]
        [Route("uploadUserImage")]
        public async Task<ActionResult> UploadUserImage([FromForm] HUploadUserDocument document)
        {
            //await Task.Delay(1000);
            //return await UploadUserFile(document, true);

           var  newUserDetail = await _userDetailService.CreateUserImage(document, _environment, _uploadsFolder);
            return Ok(document);

        }


        [HttpPost]
        [Route("uploadUserSignature")]
        public async Task<ActionResult> UploadUserSignature([FromForm] HUploadUserDocument document)
        {

            var newUserDetail = await _userDetailService.CreateUserSignature(document, _environment, _uploadsFolder);
            return Ok(document);
        }


        private async Task<ActionResult<UserDetailResource>> UploadUserFile(UserDetailResource userDetailsResource, bool isImage)
        {
            if (userDetailsResource.ID == null || userDetailsResource.ID == 0)
            {
                return BadRequest("Invalid user ID.");
            }

            var userDetail = await _userDetailService.GetUserDetailById(userDetailsResource.ID);
            if (userDetail == null)
            {
                return NotFound("User not found.");
            }

            userDetailsResource.ID = 0;
            var detailToUpload = _mapper.Map<UserDetailResource, UserDetail>(userDetailsResource);

            UserDetail newUserDetail;
            if (isImage)
            {
                //newUserDetail = await _userDetailService.CreateUserImage(detailToUpload, _environment, _uploadsFolder);
            }
            else
            {
                //  newUserDetail = await _userDetailService.CreateUserSignature(detailToUpload, _environment, _uploadsFolder);
            }

            string birtReportServer = _configuration.GetValue<string>("AppSettings:ServerDomain");
            string _baseUrl = $"{Request.Scheme}://{birtReportServer}/api/Files/retrieve/";
            //string baseUrl = $"{Request.Scheme}://{Request.Host.Value}/Files/retrieve/";
            if (isImage)
            {
                userDetailsResource.ProfilePicPath = _baseUrl + userDetailsResource.ProfilePicPath;
            }
            else
            {
                userDetailsResource.UserSignPath = _baseUrl + userDetailsResource.UserSignPath;
            }

            return Ok();
        }

        [HttpGet]
        [Route("getUserImageById/{ID}")]
        public async Task<ActionResult<UserDetailResource>> GetUserImageById(int ID)
        {
            var userImage = await _userDetailService.GetUserImageById(ID);

            if (userImage == null)
            {
                return NotFound($"User with ID {ID} not found.");
            }
            string birtReportServer = _configuration.GetValue<string>("AppSettings:ServerDomain");
            string _baseUrl = $"{Request.Scheme}://{birtReportServer}/api/Files/retrieve/";
            //string _baseUrl = $"{Request.Scheme}://{Request.Host.Value}/api/Files/retrieve/";


            if (!string.IsNullOrEmpty(userImage.ProfilePicPath))
            {
                userImage.ProfilePicPath = _baseUrl + userImage.ProfilePicPath;
            }

            var userImageResource = _mapper.Map<UserDetail, UserDetailResource>(userImage);

            return Ok(userImageResource);
        }


        [HttpGet]
        [Route("getUserSignatureById/{ID}")]
        public async Task<ActionResult<UserDetailResource>> GetUserSignById(int ID)
        {
            var userSign = await _userDetailService.GetUserImageById(ID);
            var defaultUserImage = "defaultProfilePic.png";

            string birtReportServer = _configuration.GetValue<string>("AppSettings:ServerDomain");
            string _baseUrl = $"{Request.Scheme}://{birtReportServer}/api/Files/retrieve/";
            //string _baseUrl = $"{Request.Scheme}://{Request.Host.Value}/api/Files/retrieve/";

            if (userSign == null)
            {
                userSign.UserSignPath = _baseUrl + defaultUserImage;
            }

            if (!string.IsNullOrEmpty(userSign.UserSignPath))
            {
                userSign.UserSignPath = _baseUrl + userSign.UserSignPath;
            }

            var userSignResource = _mapper.Map<UserDetail, UserDetailResource>(userSign);

            return Ok(userSignResource);
        }

        //[HttpGet]
        //[Route("getUserSignById/{ID}")]
        //public async Task<ActionResult<UserDetailResource>> GetUserSignById(int ID)
        //{

        //    var userSign = await _userDetailService.GetUserSignById(ID);
        //    string _baseUrl = $"{Request.Scheme}://{Request.Host.Value}/api/Files/retrieve/";

        //    foreach (var entity in userSign)
        //    {
        //        if (!string.IsNullOrEmpty(entity.UserSignPath))
        //        {
        //            entity.UserSignPath = _baseUrl + entity.UserSignPath;
        //        }
        //    }

        //    var userSignResource = _mapper.Map<IEnumerable<UserDetail>, IEnumerable<UserDetailResource>>(userSign);

        //    return Ok(userSignResource);

        //}


    }

   
}
