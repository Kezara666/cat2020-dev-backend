using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CAT20.Api.Validators;
using CAT20.Core.Models.User;
using CAT20.Core.Services.User;
using CAT20.WebApi.Resources.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using CAT20.Data;
using Microsoft.EntityFrameworkCore;
using CAT20.Services.User;
using CAT20.WebApi.Resources.User.Save;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using CAT20.Core.Models.Control;
using CAT20.WebApi.Resources.Control;
using CAT20.Core.Services.Control;
using CAT20.Services.Control;
using static CAT20.WebApi.Controllers.Control.UsersController;
using CAT20.Common.Utility;
using CAT20.WebApi.Email;
using System.Linq;
using CAT20.Core.HelperModels;

namespace CAT20.WebApi.Controllers.Control
{
    [Route("api/access")]
    [ApiController]
    public class AccessController : BaseController
    {
        public IConfiguration _configuration;
        private readonly IUserDetailService _userDetailService;
        private readonly ISabhaService _sabhaService;
        private readonly IOfficeService _officeService;
        private readonly IDistrictService _districtService;
        private readonly IProvinceService _provinceService;
        private readonly ISelectedLanguageService _selectedLanguageService;
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;
        private readonly IUserLoginActivityService _userLoginActivityService;

        //--- [Start - dependency injection - email services] ----
        private readonly IEmailConfigurationService _emailConfigurationService;
        private readonly IEmailOutBoxService _emailOutBoxService;
        //--- [End - dependency injection - email services] ----


        //--- [Start - dependency injection - rule services] ----
        private readonly IRuleService _ruleService;
        //--- [End - dependency injection - rule services] ------

        public AccessController(IUserLoginActivityService userLoginActivityService, IUserDetailService userDetailService, IMapper mapper, IConfiguration config, ISabhaService sabhaService, IDistrictService districtService, IProvinceService provinceService, ISelectedLanguageService selectedLanguageService, ILanguageService languageService, IOfficeService officeService, IEmailConfigurationService emailConfigurationService, IEmailOutBoxService emailOutBoxService, IRuleService ruleService)
        {
            _mapper = mapper;
            _userDetailService = userDetailService;
            _configuration = config;
            _sabhaService = sabhaService;
            _districtService = districtService;
            _provinceService = provinceService;
            _selectedLanguageService = selectedLanguageService;
            _languageService = languageService;
            _officeService = officeService;
            _userLoginActivityService = userLoginActivityService;

            //--- [Start - dependency injection - email services] ----
            _emailConfigurationService = emailConfigurationService;
            _emailOutBoxService = emailOutBoxService;
            //--- [End - dependency injection - email services] ----


            //--- [Start - dependency injection - rule services] ----
            _ruleService = ruleService;
            //--- [End - dependency injection - rule services] ------
        }

        
        [HttpGet("screenUnlock/{pin}")]
        public async Task<IActionResult> screenUnlock(string pin)
        {
            try
            {
                var _token = new HTokenClaim
                {
                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    username = HttpContext.User.FindFirst("username")!.Value,
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                    officeCode = HttpContext.User.FindFirst("officeCode")!.Value,
                    officeTypeID = int.Parse(HttpContext.User.FindFirst("officeTypeID")!.Value),
                };

                if (pin != null)
                {
                    CryptoProvider crypto = new CryptoProvider();
                    //pin = crypto.GetHash(pin.Trim());

                    bool isValid = await _userDetailService.IsPINValid(_token.username, pin);

                  //  bool isValid = true;

                    if (isValid) { 
                        return Ok(true);
                    }
                    else
                    {
                        return Ok(false);
                    }
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
        }



        //------ [Start - GetUserDetailByUsername] ---
        [HttpGet]
        [Route("forgotPassword/getUserByUsername/{username}")]
        public async Task<ActionResult<UserDetailResource>> GetUserDetailByUsername([FromRoute] string username)
        {
            var userDetail = await _userDetailService.GetUserDetailByUsername(username);
            var userDetailResource = _mapper.Map<UserDetail, UserDetailResource>(userDetail);
            return Ok(userDetailResource);
        }
        //------ [End - GetUserDetailByUsername] ---


        //------ [Start - Update user password with temporary pasword /forgot password] ---
        //*****************************
        [HttpPost("forgotPassword/updateForgotPassword")]
        public async Task<ActionResult<UserDetailResource>> SetUserTemporaryPassword([FromBody] SaveUserDetailResource saveUserDetailResource)
        {
            var userDetailToBeUpdate = await _userDetailService.GetUserDetailById(saveUserDetailResource.ID);

            if (userDetailToBeUpdate == null)
                return NotFound();


            var userDetail = _mapper.Map<SaveUserDetailResource, UserDetail>(saveUserDetailResource);

            if (saveUserDetailResource.Password != string.Empty)
                saveUserDetailResource.Password = RandomPassword(saveUserDetailResource.Username);
            
            CryptoProvider crypto = new CryptoProvider();
            userDetail.Password = crypto.GetHash(saveUserDetailResource.Password.Trim());

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
                            MailContent = accountRecoveryUserMailBody(userDetail.Username, saveUserDetailResource.Password, emailSettings.SystemURL),
                            Note = string.Empty,
                            Recipient = userDetail.Username,
                            Subject = "Temporary Password [Account Recovery] - CAT2020.lk"
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
        //*****************************


        //*****************************
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
        //*****************************


        //*****************************
        private string accountRecoveryUserMailBody(string userName, string password, string url)
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
            _sb.Append("    Notification - User Account Recovery");
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
            _sb.Append("                Dear &nbsp;User,<br> ");
            _sb.Append("                <br> ");
            _sb.Append("            </td> ");
            _sb.Append("        </tr> ");
            _sb.Append("        <tr> ");

            _sb.Append("<td Class='txt_normal'>");
            _sb.Append("");
            _sb.Append("                 Response by CAT20 Automation System. ");
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

        [HttpGet]
        [Route("CheckHealth")]
        public async Task<IActionResult> CheckHealth()
        {
            try  { 
            var sabhainfo = await _sabhaService.GetSabhaById(1);
            if (sabhainfo!=null && sabhainfo.Status==1)
            {
                    return StatusCode(200, new { Status = "Healthy" });
                }
            else
            {
                return StatusCode(500, new { Status = "Unhealthy" });
            }
            }
            catch(Exception ex) {
                return StatusCode(500, new { Status = "Unhealthy" });
            }
        }

        //*****************************
        //------ [End - Update user password with temporary pasword /forgot password] ---


        //[HttpPost]
        //public async Task<IActionResult> Post(UserDetailResource _userData)
        //{
        //    if (_userData != null && _userData.Username != null && _userData.Password != null)
        //    {
        //        var userDetail = _mapper.Map<UserDetailResource, UserDetail>(_userData);
        //        var userDetailForLogin = await _userDetailService.GetUserDetailByUsernamePassword(userDetail);
        //        var user = _mapper.Map<UserDetail, UserDetailResource>(userDetailForLogin);

        //        if (user != null)
        //        {
        //            //create claims details based on the user information
        //            var claims = new[] {
        //                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
        //                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        //                new Claim("SabhaID", user.SabhaID.ToString()),
        //                new Claim("OfficeID", user.OfficeID.ToString()),
        //                new Claim("UserID", user.ID.ToString()),
        //                new Claim("NameWithInitials", user.NameWithInitials),
        //                new Claim("UserName", user.Username)
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
    }

}
