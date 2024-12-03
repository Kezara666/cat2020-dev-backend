using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.Models.Vote;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Models.Control;
using CAT20.WebApi.Resources.Vote.Save;
using CAT20.WebApi.Resources.Vote;
using CAT20.WebApi.Resources.Control;
using CAT20.WebApi.Resources.ShopRental;
using CAT20.Core.Services.Vote;
using CAT20.Core.Services.ShopRental;
using CAT20.Core.Services.Control;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CAT20.Services.Vote;
using CAT20.Services.Control;
using CAT20.Services.ShopRental;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.SMS;

namespace CAT20.WebApi.Controllers.SMS
{
    [Route("api/sms")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        private readonly IRentalPlaceService _rentalPlaceService;
        private readonly IPropertyService _propertyService;
        private readonly IFloorService _floorService;
        private readonly IOfficeService _officeService;
        private readonly IMapper _mapper;
        private ISMSConfigurationService _smsconfigurationService;
        private ISMSOutBoxService _smsOutBoxService;

        public SMSController(ISMSConfigurationService smsconfigurationService, ISMSOutBoxService smsOutBoxService)
        {
            _smsconfigurationService = smsconfigurationService;
            _smsOutBoxService = smsOutBoxService;
        }

        [HttpPost("createSMS")]
        public async Task<ActionResult<SMSResource>> CreateSMS([FromBody] SMSResource SMSResource)
        {
            SMSService sms = new SMSService(_smsconfigurationService, _smsOutBoxService);
            await sms.createSMS(SMSResource);
            return Ok(SMSResource);
        }
       
        //[HttpPost("sendSMS")]
        //public async Task<ActionResult<SendSMS>> SendSMS([FromBody] SMSResource SMSResource)
        //{
        //    #region Create SMS Record
        //    if (SMSResource != null)
        //    {
        //        if (SMSResource.MobileNo.Length == 10)
        //        {
        //            if (SMSResource.Text.Length > 1)
        //            {
        //            try
        //            {
        //                    var smsSettings = await _smsconfigurationService.GetSMSConfigurationBySabhaId(SMSResource.SabhaId);
        //                    await _smsOutBoxService.CreateSMSOutBox(new Core.Models.Control.SMSOutBox
        //                    {
        //                    Module = SMSResource.Module,
        //                    SabhaID = SMSResource.SabhaId,
        //                    Recipient = SMSResource.MobileNo,
        //                    SMSContent = SMSResource.Text,
        //                    Subject = SMSResource.Subject,
        //                    SMSSendAttempts = 0,
        //                    SMSStatus = Core.Models.Enums.SMSStatus.Pending,
        //                    Note = string.Empty,
        //                    SenderId= smsSettings.SenderId,
        //                    });
        //            }
        //            catch (Exception ex)
        //            {
        //                    Console.WriteLine(ex.Message);
        //                    return BadRequest(ex.Message);
        //            }

        //            }
        //            else
        //            {
        //                Console.WriteLine("SMS Text Should Be at Least 2 letters");
        //                return BadRequest("SMS Text Should Be at Least 2 letters");
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Mobile Number Should Contain 10 digits");
        //            return BadRequest("Mobile Number Should Contain 10 digits");
        //        }

        //    }
        //    #endregion
        //    SMSService sms = new SMSService(_smsconfigurationService, _smsOutBoxService);
        //    await sms.sendSMS();
        //    return Ok();
        //}


      


        //var smsService = new SmsService();
        ////var smsRequest = new SmsRequest
        ////{
        ////    To = "PHONE_NUMBER",
        ////    Message = "Hello from your .NET Core app!",
        ////};

        //try
        //{
        //    var response = await smsService.SendSmsAsync(smsRequest);
        //    Console.WriteLine("SMS sent successfully. Response: " + response);
        //    return Ok("SMS sent successfully. Response: " + response);
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("Error: " + ex.Message);
        //    return BadRequest(ex.Message);
        //}
    }
}
