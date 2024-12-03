using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using CAT20.Core;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Models.User;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.OnlinePayment;
using CAT20.Services.Control;
using CAT20.WebApi.Email;
using CAT20.WebApi.Resources.Control;
using CAT20.WebApi.Resources.OnlinePayment;
using CAT20.WebApi.Resources.User.Save;
using CAT20.WebApi.SMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CAT20.WebApi.Controllers.OnlinePayment;

[Route("api/onlinePaymentsAuth")]
public class AuthOnlinePaymentController : Controller
{
    private readonly IPartnerService _partnerService;
    private readonly IOnlinePaymentService _onlinePaymentService;
    private readonly IMapper _mapper;
    private readonly IConfiguration Configuration;
    private readonly ISMSConfigurationService _smsConfigurationService;
    private readonly ISMSOutBoxService _smsOutBoxService;
    private readonly IDistrictService _districtService;
    private readonly ISabhaService _sabhaService;
    private readonly IGnDivisionService _gnDivisionService;
    private readonly IOfficeService _officeService;
    private readonly IEmailOutBoxService _emailOutBoxService;
    private readonly IEmailConfigurationService _emailConfigurationService;


    public AuthOnlinePaymentController(IOfficeService officeService, IGnDivisionService gnDivisionService,
        ISabhaService sabhaService, IDistrictService districtService, IPartnerService partnerService, IMapper mapper,
        IConfiguration configuration, IOnlinePaymentService onlinePaymentService, IEmailConfigurationService emailConfigurationService, IEmailOutBoxService emailOutBoxService,
        ISMSConfigurationService smsConfigurationService, ISMSOutBoxService smsOutBoxService)
    {
        _partnerService = partnerService;
        _mapper = mapper;
        Configuration = configuration;
        _onlinePaymentService = onlinePaymentService;
        _smsConfigurationService = smsConfigurationService;
        _smsOutBoxService = smsOutBoxService;
        _districtService = districtService;
        _sabhaService = sabhaService;
        _gnDivisionService = gnDivisionService;
        _officeService = officeService;
        _emailConfigurationService = emailConfigurationService;
        _emailOutBoxService = emailOutBoxService;
    }

    [HttpPost]
    public async Task<IActionResult> register([FromBody] LoginRequest loginRequest)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (loginRequest.MobileNo != "")
                {
                    var partner = await _partnerService.GetByPhoneNo(loginRequest.MobileNo);

                    var logInDetail = _mapper.Map<LoginRequest, LogInDetails>(loginRequest);
                    if (partner.NicNumber != null && partner.NicNumber.Length > 5)
                    {
                        logInDetail.NIC = partner.NicNumber;
                    }
                    else
                    {
                        logInDetail.NIC = "000000000V";
                    }

                    var logInId = await _onlinePaymentService.SaveLogInInfo(logInDetail);
                    if (logInId == null)
                    {
                        return StatusCode(500, "Internal Server Error");
                    }

                    if (partner != null)
                    {
                        var claims = new[]
                        {
                        new Claim(JwtRegisteredClaimNames.Sub, Configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

                        new Claim("partnerId", partner.Id.ToString()),
                        new Claim("partnerId", partner.Id.ToString()),
                        new Claim("partnerName", partner.Name),
                        new Claim("partnerNIC", partner.NicNumber ?? "000000000Z"),
                        new Claim("partnerMobileNo", loginRequest.MobileNo ?? "0000000000"),
                        new Claim("partnerEmailAddress", loginRequest.EmailAddress ?? "null@cat2020.lk")
                    };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            Configuration["Jwt:Issuer"],
                            Configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddHours(72),
                            signingCredentials: signIn
                        );


                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            partnerId = partner.Id,
                            partnerName = partner.Name,
                            partnerNIC = partner.NicNumber,
                            partnerMobileNo = loginRequest.MobileNo,
                            partnerStreet1 = partner.Street1,
                            partnerStreet2 = partner.Street2,
                            partnerCity = partner.City,
                            partnerZip = partner.Zip,
                            partnerEmail = partner.Email,
                            sabhaId = partner.SabhaId,
                            createdBy = partner.CreatedBy,
                            updatedBy = partner.UpdatedBy
                        });
                    }

                    return StatusCode(404, "Partner not found!");
                }
                else if (loginRequest.EmailAddress != "")
                {
                    var partner = await _partnerService.GetByEmail(loginRequest.EmailAddress);

                    var logInDetail = _mapper.Map<LoginRequest, LogInDetails>(loginRequest);
                    if (partner.NicNumber !=null && partner.NicNumber.Length > 5)
                    {
                        logInDetail.NIC = partner.NicNumber;
                    }
                    else
                    {
                        logInDetail.NIC = "000000000V";
                    }

                    var logInId = await _onlinePaymentService.SaveLogInInfo(logInDetail);
                    if (logInId == null)
                    {
                        return StatusCode(500, "Internal Server Error");
                    }

                    if (partner != null)
                    {
                        var claims = new[]
                        {
                        new Claim(JwtRegisteredClaimNames.Sub, Configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

                        new Claim("partnerId", partner.Id.ToString()),
                        new Claim("partnerId", partner.Id.ToString()),
                        new Claim("partnerName", partner.Name),
                        new Claim("partnerNIC", partner.NicNumber ?? "000000000Z"),
                        new Claim("partnerMobileNo", loginRequest.MobileNo ?? "0000000000"),
                        new Claim("partnerEmailAddress", loginRequest.EmailAddress ?? "null@cat2020.lk")
                    };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            Configuration["Jwt:Issuer"],
                            Configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddHours(72),
                            signingCredentials: signIn
                        );


                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            partnerId = partner.Id,
                            partnerName = partner.Name,
                            partnerNIC = partner.NicNumber ?? "-",
                            partnerMobileNo = loginRequest.MobileNo ?? "-",
                            partnerStreet1 = partner.Street1,
                            partnerStreet2 = partner.Street2,
                            partnerCity = partner.City,
                            partnerZip = partner.Zip,
                            partnerEmail = partner.Email ?? "-",
                            sabhaId = partner.SabhaId,
                            createdBy = partner.CreatedBy,
                            updatedBy = partner.UpdatedBy
                        });
                    }
                    return StatusCode(404, "Partner not found!");
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    [HttpPost("isAvailable")]
    public async Task<IActionResult> isAvailable([FromBody] VerifiedResource verifiedResource)
    {
        
        var verified = await _onlinePaymentService.isAvailable(verifiedResource.NIC, verifiedResource.MobileNo);
        
        
        
        if (verified != null && verified.isIntheSysterm == false)
        {
            return NotFound();
        } 
        var map = _mapper.Map<Verified, PaymentSMSResource>(verified);
        SMSService sms = new SMSService(_smsConfigurationService, _smsOutBoxService);
        await sms.createSMS(map);
        return Ok(map);
    }

    [HttpPost("isMobileAvailable")]
    public async Task<IActionResult> isMobileAvailable([FromBody] VerifiedResource verifiedResource)
    {

        var verified = await _onlinePaymentService.isMobileAvailable(verifiedResource.MobileNo);



        if (verified != null && verified.isIntheSysterm == false)
        {
            return NotFound();
        }
        var map = _mapper.Map<Verified, PaymentSMSResource>(verified);
        SMSService sms = new SMSService(_smsConfigurationService, _smsOutBoxService);
        await sms.createSMS(map);
        return Ok(map);
    }

    [HttpPost("isEmailAvailable")]
    public async Task<IActionResult> isEmailAvailable([FromBody] VerifiedResource verifiedResource)
    {

        var verified = await _onlinePaymentService.isEmailAvailable(verifiedResource.EmailAddress);

        if (verified != null && verified.isIntheSysterm == false)
        {
            return NotFound();
        }
        var map = _mapper.Map<Verified, PaymentEmailResource>(verified);

        if (map.Email != null && map.SabhaId!=null && map.OTP!=null)
        {
            #region Create Email Record for Online Payment Login OTP 
            if (map != null)
            {
                try
                {
                    var emailSettings = await _emailConfigurationService.GetEmailConfigurationById(1);
                    await _emailOutBoxService.CreateEmailOutBox(new Core.Models.Control.EmailOutBox
                    {
                        Attachment = string.Empty,
                        Bc = string.Empty,
                        Cc = string.Empty,
                        CreatedByID = -1,
                        EmailSendAttempts = 0,
                        EmailStatus = Core.Models.Enums.EmailStatus.Pending,
                        IsBodyHtml = true,
                        MailContent = loginOTPEmailContent(map.Email, map.OTP, emailSettings.SystemURL, map),
                        Note = string.Empty,
                        Recipient = map.Email,
                        Subject = "One Time Password (OTP) Confirmation - pay.cat2020.lk"
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
        return Ok(map);
    }

    private string loginOTPEmailContent(string Email, int OTP, string url, PaymentEmailResource mapeddata)
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
        _sb.Append("    Notification - One Time Password ");
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
        _sb.Append("                 Please use the below OTP to login in to CAT20 Local Government Payment System. ");
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
        _sb.Append("            <td Class='inner_table_td_blue'><a href='" + url + "'>pay.cat2020.lk</a></td> ");
        _sb.Append("        </tr> ");

        _sb.Append("        <tr> ");
        _sb.Append("            <td Class='inner_table_td_green'>User Name</td> ");
        _sb.Append("            <td Class='inner_table_td_blue'>" + Email + "</td> ");
        _sb.Append("        </tr> ");

        _sb.Append("        <tr> ");
        _sb.Append("                    <td Class='inner_table_td_green'>One-time Password (OTP)</td> ");
        _sb.Append("            <td Class='inner_table_td_blue'><b>" + OTP + "</b>  </td> ");
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
        _sb.Append("                        This OTP is valid only for 5 minutes. ");
        _sb.Append("                    </td> ");
        _sb.Append("                </tr> ");
        _sb.Append("                <tr> ");
        _sb.Append("                                    <td Class='txt_normal'> ");
        //_sb.Append("                        This OTP is valid only for 5 minutes. ");
        _sb.Append("                    </td> ");
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

    [HttpPost("isPartnerAvailable")]
    public async Task<IActionResult> isPartnerAvailable([FromBody] VerifiedResource verifiedResource)
    {
        var verified = await _onlinePaymentService.isPartnerAvailable(verifiedResource.NIC, verifiedResource.MobileNo,
            verifiedResource.sabhaId);
        if (verified != null && verified.isIntheSysterm == true)
        {
            return Ok(verified);
        }

        var map = _mapper.Map<Verified, PaymentSMSResource>(verified);
        SMSService sms = new SMSService(_smsConfigurationService, _smsOutBoxService);
        await sms.createSMS(map);
        return Ok(map);
    }


    [HttpGet("getDistrict")]
    public async Task<ActionResult<IEnumerable<District>>> GetAllProducts()
    {
        var districts = await _districtService.GetAllDistricts();
        var districtResources = _mapper.Map<IEnumerable<District>, IEnumerable<DistrictResource>>(districts);

        return Ok(districtResources);
    }

    [HttpGet("getSabha/{districtID}")]
    public async Task<ActionResult<SabhaResource>> GetSabhaByDistrictId([FromRoute] int districtID)
    {
        var sabhas = await _sabhaService.GetSabhaByDistrictId(districtID);
        var sabhaResources = _mapper.Map<IEnumerable<Sabha>, IEnumerable<SabhaResource>>(sabhas);
        return Ok(sabhaResources);
    }

    [HttpGet]
    [Route("getGnDivision/{sabhaid}")]
    public async Task<ActionResult<IEnumerable<GnDivisions>>> getAllForSabha(int sabhaid)
    {
        List<GnDivisions> gnDivisionsforsabha = new List<GnDivisions>();
        var offices = await _officeService.getAllOfficesForSabhaId(sabhaid);
        foreach (var office in offices)
        {
            var gnDivisions = await _gnDivisionService.GetAllForOffice(office.ID.Value);
            gnDivisionsforsabha.AddRange(gnDivisions);
        }

        var gnDivisionResources =
            _mapper.Map<IEnumerable<GnDivisions>, IEnumerable<GnDivisionsResource>>(gnDivisionsforsabha);

        return Ok(gnDivisionResources);
    }

    [HttpPost("partners/save")]
    public async Task<IActionResult> Save([FromBody] PartnerResourceOn obj)
    {
        try
        {
            if (obj != null && obj.Id == null)
            {
                int isallowed = 1;
                if (obj.MobileNumber.Length > 8)
                {
                    try
                    {
                        var checkpartnermobile = await _partnerService.GetByPhoneNo(obj.MobileNumber.Trim());
                        if (checkpartnermobile != null)
                            isallowed = 0;
                    }
                    catch (Exception ex)
                    {
                        isallowed = 0;
                    }
                }

                if (obj.NicNumber.Length > 8)
                {
                    try
                    {
                        var checkpartnernic = await _partnerService.GetByNIC(obj.NicNumber.Trim());
                        if (checkpartnernic != null)
                            isallowed = 0;
                    }
                    catch (Exception ex)
                    {
                        isallowed = 0;
                    }
                }

                if (isallowed == 1)
                {
                    var partnerResource = obj;
                    partnerResource.CreatedAt = DateTime.Now;
                    partnerResource.Active = 1;

                    var partnerToCreate = _mapper.Map<PartnerResourceOn, Partner>(partnerResource);
                    var newPartner = await _partnerService.Create(partnerToCreate);

                    var partner = await _partnerService.GetById(newPartner.Id);
                    partnerResource = _mapper.Map<Partner, PartnerResourceOn>(partner);

                    return Ok(partnerResource);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                var updatedPartnerResource = new PartnerResource();
                //if (obj.IsEditable == 1)
                //{
                obj.UpdatedAt = DateTime.Now;
                var partnerToBeUpdate = await _partnerService.GetById(obj.Id);

                if (partnerToBeUpdate == null)
                    return NotFound();

                var partner = _mapper.Map<PartnerResourceOn, Partner>(obj);
                await _partnerService.Update(partnerToBeUpdate, partner);
                var updatedPartner = await _partnerService.GetById(obj.Id);
                updatedPartnerResource = _mapper.Map<Partner, PartnerResource>(updatedPartner);

                //creating a SMS to send cutomer's mobile number - apply the following code to the places you want to send sms notifications
                //SMSService sms = new SMSService(_smsconfigurationService, _smsOutBoxService);
                //SMSResource newsms = new SMSResource();
                //newsms.MobileNo = updatedPartnerResource.MobileNumber.Trim();
                //newsms.Text = "Hi ! "+ updatedPartnerResource.Name.Trim() + ", Your Customer Information Updated Successfully. Regards - Cat2020.lk";
                //newsms.SabhaId = updatedPartnerResource.SabhaId.Value;
                //newsms.Module = "Partner";
                //newsms.Subject = "Customer Information Updated Notification";

                //await sms.createSMS(newsms);
                //end of SMS creation


                //}
                return Ok();
            }
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpGet("getAllProvince")]
    public async Task<IActionResult> GetAllProvince()
    {
        try
        {
            var provinceList = await _onlinePaymentService.GetAllProvince();
            return Ok(provinceList);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("getAllSabha/{provinceId}")]
    public async Task<IActionResult> getAllSabha([FromRoute] int provinceId)
    {
        try
        {
            var sabhaList = await _onlinePaymentService.getAllSabha(provinceId);
            return Ok(sabhaList);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("health")]
    public async Task<IActionResult> Heath()
    {
        return Ok("Healthy");
    }
}