using AutoMapper;
using CAT20.Core.DTO;
using CAT20.Core.DTO.OtherModule;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.User;
using CAT20.Core.Services.Control;
using CAT20.WebApi.Configuration;
using CAT20.WebApi.Resources.Control;
using CAT20.WebApi.Resources.Final;
using CAT20.WebApi.Resources.Pagination;
using CAT20.WebApi.Resources.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace CAT20.WebApi.Controllers.Control
{
    [Route("api/mixin/partners")]
    [ApiController]
    public class PartnersController : BaseController
    {
        private readonly IPartnerService _partnerService;
        private readonly IMapper _mapper;
        private ISMSConfigurationService _smsconfigurationService;
        private ISMSOutBoxService _smsOutBoxService;
        private readonly IWebHostEnvironment _environment;
        public IConfiguration _configuration;
        private readonly string _uploadsFolder;

        //----- [Start - dependency injection _gnService] ----
        private readonly IGnDivisionService _gnDivisionService;
        //----- [End - dependency injection _gnService] ------

        public PartnersController(IWebHostEnvironment environment, IOptions<AppSettings> appSettings, IPartnerService partnerService, IConfiguration config, IMapper mapper, ISMSConfigurationService smsconfigurationService, ISMSOutBoxService smsOutBoxService, IGnDivisionService gnDivisionService)
        {
            _mapper = mapper;
            _partnerService = partnerService;
            _smsconfigurationService = smsconfigurationService;
            _smsOutBoxService = smsOutBoxService;

            //----- [Start - dependency injection _gnService] ----
            _gnDivisionService = gnDivisionService;
            //----- [End - dependency injection _gnService] ------

            _environment = environment;
            _uploadsFolder = appSettings.Value.UploadsFolder;
            _configuration = config;
        }

        [HttpGet]
        [Route("getAllForPartnerType/{type}")]
        public async Task<ActionResult> GetAllForPartnerType(int type)
        {
            var partners = await _partnerService.GetAllForPartnerType(type);
            var partnerResources = _mapper.Map<IEnumerable<Partner>, IEnumerable<PartnerResource>>(partners);

            return Ok(partnerResources);
        }

        [HttpGet]
        [Route("getAllForPartnerTypeAndSabha/{type}/{sabhaId}")]
        public async Task<ActionResult> GetAllForPartnerTypeAndSabha(int type, int sabhaid)
        {
            var partners = await _partnerService.GetAllForPartnerTypeAndSabha(type, sabhaid);
            var partnerResources = _mapper.Map<IEnumerable<Partner>, IEnumerable<PartnerResource>>(partners);

            return Ok(partnerResources);
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult> GetAll()
        {
            var partners = await _partnerService.GetAll();
            var partnerResources = _mapper.Map<IEnumerable<Partner>, IEnumerable<PartnerResource>>(partners);

            return Ok(partnerResources);
        }

        [HttpGet]
        [Route("getAllForSabha/{sabhaId}")]
        public async Task<ActionResult> GetAllForSabha(int sabhaId)
        {
            var partners = await _partnerService.GetAllForSabha(sabhaId);
            var partnerResources = _mapper.Map<IEnumerable<Partner>, IEnumerable<PartnerResource>>(partners);

            return Ok(partnerResources);
        }

        [HttpGet]
        [Route("getAllCustomersForSabha/{sabhaId}")]
        public async Task<ActionResult> GetAllCustomersForSabha(int sabhaId)
        {
            var partners = await _partnerService.GetAllCustomersForSabha(sabhaId);
            var partnerResources = _mapper.Map<IEnumerable<Partner>, IEnumerable<PartnerResource>>(partners);

            return Ok(partnerResources);
        }

        [HttpGet]
        [Route("getAllBusinessesForSabha/{sabhaId}")]
        public async Task<ActionResult> GetAllBusinessesForSabha(int sabhaId)
        {
            var partners = await _partnerService.GetAllBusinessesForSabha(sabhaId);
            var partnerResources = _mapper.Map<IEnumerable<Partner>, IEnumerable<PartnerResource>>(partners);

            return Ok(partnerResources);
        }

        [HttpGet]
        [Route("getAllBusinessOwnersForSabha/{sabhaId}")]
        public async Task<ActionResult> GetAllBusinessOwnersForSabha(int sabhaId)
        {
            var partners = await _partnerService.GetAllBusinessOwnersForSabha(sabhaId);
            var partnerResources = _mapper.Map<IEnumerable<Partner>, IEnumerable<PartnerResource>>(partners);

            return Ok(partnerResources);
        }

        [HttpGet]
        [Route("getAllBusinessOwnersForOffice/{officeId}")]
        public async Task<ActionResult> GetAllForOffice(int officeId)
        {
            var partners = await _partnerService.GetAllBusinessOwnersForOffice(officeId);
            var partnerResources = _mapper.Map<IEnumerable<Partner>, IEnumerable<PartnerResource>>(partners);

            return Ok(partnerResources);
        }


        [HttpGet]
        [Route("getById/{id}")]
        public async Task<ActionResult<PartnerResource>> GetById([FromRoute] int id)
        {
            var partner = await _partnerService.GetByIdWithDetails(id);
            var partnerResource = _mapper.Map<Partner, PartnerResource>(partner);

            if (partnerResource == null)
                return NotFound();

            return Ok(partnerResource);
        }

        /*[HttpGet]
        [Route("getByNIC/{nic}")]
        public async Task<ActionResult<PartnerResource>> GetByNIC([FromRoute] string nic)
        {
            var partner = await _partnerService.GetByNIC(nic);
            var partnerResource = _mapper.Map<Partner, PartnerResource>(partner);

            if (partnerResource == null)
                return NotFound();

            return Ok(partnerResource);
        }*/

        [HttpGet]
        [Route("getByNIC/{nic}")]
        public async Task<ActionResult<PartnerResource>> GetByNIC([FromRoute] string nic)
        {
            if (nic != null && nic.Length >= 10)
            {
                var partner = await _partnerService.GetByNIC(nic);
                var partnerResource = _mapper.Map<Partner, PartnerResource>(partner);

                if (partnerResource == null)
                    return NotFound();


                //if (partner.GnDivisionId != 0)
                //{
                //    //get partner gnDivision ID
                //    var partnerGnID = partner.GnDivisionId;

                //    //pass that gnDivision ID to gnService and get the gnDivision details
                //    var partnerGnDetails = await _gnDivisionService.GetById(partnerGnID.Value);

                //    //get partner gnDivision Name
                //    partnerResource.GnDivisionName = partnerGnDetails.Description;
                //}



                return Ok(partnerResource);
            }
           else
            {
                return NotFound();
            }

        }

        [HttpGet]
        [Route("findBusinessByRegNo/{regNo}")]
        public async Task<ActionResult<PartnerResource>> GetBusinessByReg( string regNo)
        {
                 string decodedRegNo = Uri.UnescapeDataString(regNo);
                 var partner = await _partnerService.GetBusinessByRegNo(decodedRegNo);
                var partnerResource = _mapper.Map<Partner, PartnerResource>(partner);

                if (partnerResource == null)
                    return NotFound();


                //if (partner.GnDivisionId != 0)
                //{
                //    //get partner gnDivision ID
                //    var partnerGnID = partner.GnDivisionId;

                //    //pass that gnDivision ID to gnService and get the gnDivision details
                //    var partnerGnDetails = await _gnDivisionService.GetById(partnerGnID.Value);

                //    //get partner gnDivision Name
                //    partnerResource.GnDivisionName = partnerGnDetails.Description;
                //}



                return Ok(partnerResource);
            

        }

        [HttpGet]
        [Route("getByPassportNo/{passport}")]
        public async Task<ActionResult<PartnerResource>> GetByPassportNo([FromRoute] string passport)
        {
            if (passport != null && passport.Length >= 5)
            {
                var partner = await _partnerService.GetByPassportNo(passport);
                var partnerResource = _mapper.Map<Partner, PartnerResource>(partner);

                if (partnerResource == null)
                    return NotFound();


                //if (partner.GnDivisionId != 0)
                //{
                //    //get partner gnDivision ID
                //    var partnerGnID = partner.GnDivisionId;

                //    //pass that gnDivision ID to gnService and get the gnDivision details
                //    var partnerGnDetails = await _gnDivisionService.GetById(partnerGnID.Value);

                //    //get partner gnDivision Name
                //    partnerResource.GnDivisionName = partnerGnDetails.Description;
                //}



                return Ok(partnerResource);
            }
            else
            {
                return NotFound();
            }

        }

        /*[HttpGet]
        [Route("getByPhoneNo/{phoneNo}")]
        public async Task<ActionResult<PartnerResource>> GetByPhoneNo([FromRoute] string phoneNo)
        {
            var partner = await _partnerService.GetByPhoneNo(phoneNo);
            var partnerResource = _mapper.Map<Partner, PartnerResource>(partner);

            if (partnerResource == null)
                return NotFound();

            return Ok(partnerResource);
        }*/

        [HttpGet]
        [Route("getByPhoneNo/{phoneNo}")]
        public async Task<ActionResult<PartnerResource>> GetByPhoneNo([FromRoute] string phoneNo)
        {
            if(phoneNo != null && phoneNo.Length>=10)
            { 
            var partner = await _partnerService.GetByPhoneNo(phoneNo);
            var partnerResource = _mapper.Map<Partner, PartnerResource>(partner);

            if (partnerResource == null)
                return NotFound();

            //if (partner.GnDivisionId != 0)
            //{
            //    //get partner gnDivision ID
            //    var partnerGnID = partner.GnDivisionId;

            //    //pass that gnDivision ID to gnService and get the gnDivision details
            //    var partnerGnDetails = await _gnDivisionService.GetById(partnerGnID.Value);

            //    //get partner gnDivision Name
            //    partnerResource.GnDivisionName = partnerGnDetails.Description;
            //}

            return Ok(partnerResource);
            }
            else { return NotFound(); }
        }

        [HttpGet]
        [Route("findBusinessByPhoneNo/{phoneNo}")]
        public async Task<ActionResult<PartnerResource>> GetBusinessByPhoneNo([FromRoute] string phoneNo)
        {
            if (phoneNo != null && phoneNo.Length >= 10)
            {
                var partner = await _partnerService.GetBusinessByPhoneNo(phoneNo);
                var partnerResource = _mapper.Map<Partner, PartnerResource>(partner);

                if (partnerResource == null)
                    return NotFound();

                //if (partner.GnDivisionId != 0)
                //{
                //    //get partner gnDivision ID
                //    var partnerGnID = partner.GnDivisionId;

                //    //pass that gnDivision ID to gnService and get the gnDivision details
                //    var partnerGnDetails = await _gnDivisionService.GetById(partnerGnID.Value);

                //    //get partner gnDivision Name
                //    partnerResource.GnDivisionName = partnerGnDetails.Description;
                //}

                return Ok(partnerResource);
            }
            else { return NotFound(); }
        }

        [HttpGet]
        [Route("getByEmail/{email}")]
        public async Task<ActionResult<PartnerResource>> GetByEmail([FromRoute] string email)
        {
            if (email != null && email.Length >= 5 && IsValidEmail(email))
            {
                var partner = await _partnerService.GetByEmail(email);
                var partnerResource = _mapper.Map<Partner, PartnerResource>(partner);

                if (partnerResource == null)
                    return NotFound();

                //if (partner.GnDivisionId != 0)
                //{
                //    //get partner gnDivision ID
                //    var partnerGnID = partner.GnDivisionId;

                //    //pass that gnDivision ID to gnService and get the gnDivision details
                //    var partnerGnDetails = await _gnDivisionService.GetById(partnerGnID.Value);

                //    //get partner gnDivision Name
                //    partnerResource.GnDivisionName = partnerGnDetails.Description;
                //}

                return Ok(partnerResource);
            }
            else { return NotFound(); }
        }

        static bool IsValidEmail(string email)
        {
            // Define a regular expression for email validation
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Create a Regex object
            Regex regex = new Regex(pattern);

            // Use the IsMatch method to test whether the string matches the regular expression
            return regex.IsMatch(email);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save(PartnerResource obj)
        {
            try
            {
                if (obj != null && (obj.Id == 0 || obj.Id == null) && obj.IsTempory == 0 && obj.IsBusiness==1)
                {
                    if (obj.BusinessRegNo!=null && obj.Name.Length!=null)
                    {
                        int isallowed = 1;

                        var BussinessRegistrationNoCheck = await _partnerService.GetBusinessByRegNo(obj.BusinessRegNo.Trim());

                        if(BussinessRegistrationNoCheck != null)
                        {
                            return Ok(new ApiResponseModel<Partner>
                            {
                                Status = 400,
                                Message = "RegNo Already Exists",
                    
                            });
                        }








                        if (obj.BusinessRegNo.Length >= 4)
                        {
                            try
                            {
                                var PartnerByBusRegNo = await _partnerService.GetBusinessByRegNo(obj.BusinessRegNo.Trim());
                                if (PartnerByBusRegNo != null)
                                {
                                    isallowed = 0;
                                    return BadRequest();
                                }
                            }
                            catch (Exception ex)
                            {
                                isallowed = 0;
                                return BadRequest();
                            }
                        }

                        if (isallowed == 1)
                        {
                            var partnerResource = obj;
                            partnerResource.CreatedAt = DateTime.Now;
                            partnerResource.Active = 1;

                            if (partnerResource.GnDivisionId == 0)
                            {
                                partnerResource.GnDivisionId = -1;
                            }

                            var partnerToCreate = _mapper.Map<PartnerResource, Partner>(partnerResource);
                            var newPartner = await _partnerService.Create(partnerToCreate);

                            //var partner = await _partnerService.GetById(newPartner.Id);
                            partnerResource = _mapper.Map<Partner, PartnerResource>(newPartner);

                            return Ok(partnerResource);
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                    return BadRequest();

                }
                else if (obj != null && (obj.Id == 0 || obj.Id == null)  && obj.IsTempory==0)
                {
                    if((obj.MobileNumber.Length == 10 || obj.Email.Length > 6) && (obj.NicNumber.Length >= 10 || obj.PassportNo.Length > 5))
                    { 
                    int isallowed = 1;
                    if (obj.MobileNumber.Length >= 10 || obj.Email.Length >= 6)
                    {
                            if (obj.MobileNumber.Length >= 10)
                            {
                                try
                                {
                                    var checkpartnermobile = await _partnerService.GetByPhoneNo(obj.MobileNumber.Trim());
                                    if (checkpartnermobile != null)
                                        if (checkpartnermobile != null)
                                        {
                                            isallowed = 0;
                                            return BadRequest();
                                        }
                                }
                                catch (Exception ex)
                                {
                                    isallowed = 0;
                                    return BadRequest();
                                }
                            }
                            else if (obj.Email.Length >= 6)
                            {
                                try
                                {
                                    var checkpartneremail = await _partnerService.GetByEmail(obj.Email.Trim());
                                        if (checkpartneremail != null)
                                        {
                                            isallowed = 0;
                                            return BadRequest();
                                        }
                                }
                                catch (Exception ex)
                                {
                                    isallowed = 0;
                                    return BadRequest();
                                }
                            }
                            else
                            {
                                return BadRequest();
                            }
                    }
                    if (obj.NicNumber.Length >= 10)
                    {
                            try
                            {
                                var checkpartnernic = await _partnerService.GetByNIC(obj.NicNumber.Trim());
                                if (checkpartnernic != null)
                                {
                                    isallowed = 0;
                                    return BadRequest();
                                }
                            }
                            catch (Exception ex)
                            {
                                isallowed = 0;
                                return BadRequest();
                            }
                    }


                    if (isallowed == 1)
                    {
                        var partnerResource = obj;
                        partnerResource.CreatedAt = DateTime.Now;
                        partnerResource.Active = 1;

                        if (partnerResource.GnDivisionId == 0)
                        {
                            partnerResource.GnDivisionId = -1;
                        }

                        var partnerToCreate = _mapper.Map<PartnerResource, Partner>(partnerResource);
                        var newPartner = await _partnerService.Create(partnerToCreate);

                        //var partner = await _partnerService.GetById(newPartner.Id);
                        partnerResource = _mapper.Map<Partner, PartnerResource>(newPartner);

                        return Ok(partnerResource);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                    return BadRequest();

                }
                else if (obj != null && (obj.Id == 0 || obj.Id == null) && obj.IsTempory == 1)
                {
                            var partnerResource = obj;
                            partnerResource.CreatedAt = DateTime.Now;
                            partnerResource.Active = 1;

                            if (partnerResource.GnDivisionId == 0)
                            {
                                partnerResource.GnDivisionId = -1;
                            }

                            var partnerToCreate = _mapper.Map<PartnerResource, Partner>(partnerResource);
                            var newPartner = await _partnerService.Create(partnerToCreate);

                            //var partner = await _partnerService.GetById(newPartner.Id);
                            partnerResource = _mapper.Map<Partner, PartnerResource>(newPartner);

                            return Ok(partnerResource);
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

                    var partner = _mapper.Map<PartnerResource, Partner>(obj);
                    await _partnerService.Update(partnerToBeUpdate, partner);
                    var updatedPartner = await _partnerService.GetByIdWithDetails(obj.Id);
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
                    return Ok(updatedPartnerResource);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("partnerNicChange")]
        public async Task<IActionResult> partnerNicChange(PartnerResource obj)
        {
            try
            {
                if (obj != null && obj.Id == 0)
                {
                    return BadRequest();
                }
                else
                {

                    int isallowed = 1;
                    //if (obj.MobileNumber.Length > 8)
                    //{
                    //    try
                    //    {
                    //        var checkpartnermobile = await _partnerService.GetByPhoneNo(obj.MobileNumber.Trim());
                    //        if (checkpartnermobile != null)
                    //            isallowed = 0;
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        isallowed = 0;
                    //return BadRequest();
                    //    }
                    //}
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
                            return BadRequest();

                        }
                    }

                    if (isallowed == 1)
                    {
                        var updatedPartnerResource = new PartnerResource();
                        //if (obj.IsEditable == 1)
                        //{
                        obj.UpdatedAt = DateTime.Now;
                        var partnerToBeUpdate = await _partnerService.GetById(obj.Id);

                        if (partnerToBeUpdate == null)
                            return NotFound();

                        var partner = _mapper.Map<PartnerResource, Partner>(obj);
                        await _partnerService.partnerNICchange(partnerToBeUpdate, partner);
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
                        return Ok(updatedPartnerResource);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpPost("businessRegNoChange")]
        public async Task<IActionResult> businessRegNumberChange(PartnerResource obj)
        {
            try
            {
                if (obj != null && obj.Id == 0)
                {
                    return BadRequest();
                }
                else
                {

                    int isallowed = 1;
                    //if (obj.MobileNumber.Length > 8)
                    //{
                    //    try
                    //    {
                    //        var checkpartnermobile = await _partnerService.GetByPhoneNo(obj.MobileNumber.Trim());
                    //        if (checkpartnermobile != null)
                    //            isallowed = 0;
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        isallowed = 0;
                    //return BadRequest();
                    //    }
                    //}
                    if (obj.BusinessRegNo != null)
                    {
                        try
                        {
                            var checkpartnernic = await _partnerService.GetBusinessByRegNo(obj.BusinessRegNo.Trim());
                            if (checkpartnernic != null)
                                isallowed = 0;
                        }
                        catch (Exception ex)
                        {
                            isallowed = 0;
                            return BadRequest();

                        }
                    }

                    if (isallowed == 1)
                    {
                        var updatedPartnerResource = new PartnerResource();
                        //if (obj.IsEditable == 1)
                        //{
                        obj.UpdatedAt = DateTime.Now;
                        var partnerToBeUpdate = await _partnerService.GetById(obj.Id);

                        if (partnerToBeUpdate == null)
                            return NotFound();

                        var partner = _mapper.Map<PartnerResource, Partner>(obj);
                        await _partnerService.businessRegNumberchange(partnerToBeUpdate, partner);
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
                        return Ok(updatedPartnerResource);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }




        [HttpPost("partnerMobileNochange")]
        public async Task<IActionResult> partnerMobileNochange(PartnerResource obj)
        {
            try
            {
                if (obj != null && obj.Id == 0)
                {
                    return BadRequest();
                }
                else
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
                            return BadRequest();
                        }
                    }


                    if (isallowed == 1)
                    {
                        var updatedPartnerResource = new PartnerResource();
                        //if (obj.IsEditable == 1)
                        //{
                        obj.UpdatedAt = DateTime.Now;
                        var partnerToBeUpdate = await _partnerService.GetById(obj.Id);

                        if (partnerToBeUpdate == null)
                            return NotFound();

                        var partner = _mapper.Map<PartnerResource, Partner>(obj);
                        await _partnerService.partnerMobileNochange(partnerToBeUpdate, partner);
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
                        return Ok(updatedPartnerResource);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        //------------------------- [Start - save patners as a bulk of data] -------
        [HttpPost("bulkSave")]
        public async Task<IActionResult> BulkSave([FromBody] List<PartnerResource> objList)
        {
            try
            {
                if (objList != null && objList.Count > 0)
                {
                    try
                    {
                        int isallowed = 0; //not allowed
                        foreach (var obj in objList)
                        {
                            if (obj != null && obj.Id == 0)
                            {
                                //avoid existing mobile number and nic
                                if (obj.MobileNumber.Length > 8)
                                {
                                    var checkpartnermobile = await _partnerService.GetByPhoneNo(obj.MobileNumber.Trim());
                                    if (checkpartnermobile == null)
                                    {
                                        isallowed = 1;
                                    }
                                    else
                                    {
                                        isallowed = 0;
                                    }

                                }
                                else
                                {
                                    isallowed = 0;
                                }

                                if (obj.NicNumber.Length > 8)
                                {
                                    var checkpartnernic = await _partnerService.GetByNIC(obj.NicNumber.Trim());
                                    if (checkpartnernic == null)
                                    {
                                        isallowed = 1;
                                    }
                                    else
                                    {
                                        isallowed = 0;
                                    }
                                }
                                else
                                {
                                    isallowed = 0;
                                }
                            }
                            else
                            {
                                isallowed = 0;
                            }

                            if (obj.GnDivisionId == 0)
                            {
                                obj.GnDivisionId = -1;
                            }
                        }

                        if (isallowed == 1)
                        {
                            var partnersToCreate = _mapper.Map<IEnumerable<PartnerResource>, IEnumerable<Partner>>(objList);
                            var newPartners = await _partnerService.BulkCreate(partnersToCreate);

                            if (newPartners.Count() > 0)
                            {
                                return Ok(newPartners);
                            }
                            else
                            {
                                return BadRequest();
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
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        //------------------------- [End - save patners as a bulk of data] -------



        [HttpGet]
        [Route("getAllForPartnersBySearchQuery")]
        public async Task<ActionResult<ResponseModel<PartnerResource>>> GetAllForPartnersBySearchQuery([FromRoute] int sabhaId,  [FromRoute] int stage, [FromQuery] List<int?> includedIds, [FromQuery] int PageNo, [FromQuery] int pageSize, [FromQuery] string? filterKeyword)

        {
            HTokenClaim _token = new HTokenClaim
            {
                userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
            };

            var partners = await _partnerService.GetAllForPartnersBySearchQuery(null,includedIds,filterKeyword,PageNo, 12);
            var partnerResources = _mapper.Map<IEnumerable<Partner>, IEnumerable<VendorResource>>(partners.list);
            var model = new ResponseModel<VendorResource>
            {
                totalResult = partners.totalCount,
                list = partnerResources
            };


            return Ok(model);
        }

        [HttpPost]
        [Route("uploadPartnerImage")]
        public async Task<ActionResult> UploadPartnerImage([FromForm] HUploadUserDocument document)
        {
            String _ProfilePhotosFolder = _uploadsFolder + "/Customers/ProfilePhotos";
            var newUserDetail = await _partnerService.CreatePartnerImage(document, _environment, _ProfilePhotosFolder);
            return Ok(document);
        }

        [HttpGet]
        [Route("getPartnerImageById/{ID}")]
        public async Task<ActionResult<PartnerResource>> GetPartnerImageById(int ID)
        {
            var partnerImage = await _partnerService.GetPartnerImageById(ID);

            if (partnerImage == null)
            {
                return NotFound($"Customer with ID {ID} not found.");
            }
            string ServerDomain = _configuration.GetValue<string>("AppSettings:ServerDomain");
            string _baseUrl = $"{Request.Scheme}://{ServerDomain}/api/Files/cust_photo/";

            if (!string.IsNullOrEmpty(partnerImage.ProfilePicPath))
            {
                partnerImage.ProfilePicPath = _baseUrl + partnerImage.ProfilePicPath;
            }

            var userImageResource = _mapper.Map<Partner, PartnerResource>(partnerImage);

            return Ok(userImageResource);
        }

        [HttpPost]
        [Route("uploadPartnerDocument")]
        public async Task<ActionResult> UploadPartnerDocument([FromForm] HUploadPartnerDocument document)
        {
            String _DocumentsFolder = _uploadsFolder + "/Customers/Documents";
            var newUserDetail = await _partnerService.CreatePartnerDocument(document, _environment, _DocumentsFolder);
            return Ok(document);
        }

        [HttpGet]
        [Route("getPartnerDocumentsById/{ID}")]
        public async Task<ActionResult<PartnerResource>> GetPartnerDocumentsById(int ID)
        {
            var partnerWithDocs = await _partnerService.GetPartnerWithDocumentsById(ID);

            if (partnerWithDocs == null)
            {
                return NotFound($"Customer with ID {ID} not found.");
            }
            string ServerDomain = _configuration.GetValue<string>("AppSettings:ServerDomain");
            string _baseUrl = $"{Request.Scheme}://{ServerDomain}/api/Files/cust_docs/";

            if (partnerWithDocs !=null && partnerWithDocs.PartnerDocuments.Any())
            {
                foreach (var document in partnerWithDocs.PartnerDocuments)
                {
                    document.FileName = _baseUrl + document.FileName;
                }
            }
            else
            {
                return NotFound();
            }

            var partnerResource = _mapper.Map<Partner, PartnerResource>(partnerWithDocs);

            return Ok(partnerResource);
        }

        [HttpGet]
        [Route("deletePartnerDocument/{docId}/{partnerId}")]
        public async Task<ActionResult> DeletePartnerDocument(int docId, int partnerId)
        {
            String _DocumentsFolder = _uploadsFolder + "/Customers/Documents";
            var newUserDetail = await _partnerService.DeletePartnerDocument(docId, partnerId, _DocumentsFolder);

            var partnerResource = _mapper.Map<Partner, PartnerResource>(newUserDetail);
            return Ok(partnerResource);
        }
    }
}
