using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.Models.Vote;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.TradeTax;
using CAT20.WebApi.Resources.Vote.Save;
using CAT20.WebApi.Resources.Vote;
using CAT20.WebApi.Resources.TradeTax;
using CAT20.WebApi.Resources.Control;
using CAT20.Core.Services.Vote;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.TradeTax;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CAT20.Services.Vote;
using CAT20.Services.TradeTax;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.Mixin;
using CAT20.Core.Models.Mixin;
using CAT20.Services.Control;
using CAT20.WebApi.Resources.Mixin.Save;
using CAT20.Core.Models.Enums;

namespace CAT20.WebApi.Controllers.TradeTax
{
    [Route("api/tradetax/businesses")]
    [ApiController]
    public class BusinessesController : BaseController
    {
        private readonly IBusinessService _businessService;
        private readonly IBusinessTaxService _businessTaxService;
        private readonly IBusinessPlaceService _businessPlaceService;
        private readonly IBusinessNatureService _businessNatureService;
        private readonly IBusinessSubNatureService _businessSubNatureService;
        private readonly ITaxTypeService _taxTypeService;
        private readonly IPartnerService _partnerService;
        private readonly IMapper _mapper;

        public BusinessesController(IBusinessService businessService, IPartnerService partnerService, IBusinessNatureService businessNatureService, ITaxTypeService taxTypeService, IBusinessTaxService businessTaxService, IBusinessPlaceService businessPlaceService, IMapper mapper, IBusinessSubNatureService businessSubNatureService)
        {
            _mapper = mapper;
            _businessService = businessService;
            _businessTaxService = businessTaxService;
            _businessPlaceService = businessPlaceService;
            _businessNatureService = businessNatureService;
            _businessSubNatureService = businessSubNatureService;
            _taxTypeService = taxTypeService;
            _partnerService = partnerService;
        }

        [HttpGet]
        [Route("getBusinessById/{id}")]
        public async Task<ActionResult<BusinessResource>> GetBusinessById(int id)
        {
            var business = await _businessService.GetById(id);

            business.BusinessNature = new BusinessNature();
            business.BusinessSubNature = new BusinessSubNature();
            business.TaxType =  new TaxType();
            business.BusinessOwner = new Partner();
            business.PropertyOwner = new Partner();
            business.BusinessPlace = new BusinessPlace();

            try { 
            business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
            business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
            business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
            business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

            if(business.PropertyOwnerId!=null && business.PropertyOwnerId!=0)
            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

            if(business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

            var businessTaxResource = await _businessTaxService.GetBusinessTaxForBusinessId(business.Id.Value);
            business.BusinessTaxes = businessTaxResource.ToList();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            var businessResource = _mapper.Map<Business, BusinessResource>(business);

            return Ok(businessResource);
        }

        [HttpGet]
        [Route("getBusinessByRegNo/{regNo}")]
        public async Task<ActionResult<BusinessResource>> GetBusinessByRegNo(string regNo)
        {
            var business = await _businessService.GetByRegNo(regNo);
            var businessResource = _mapper.Map<Business, BusinessResource>(business);
            return Ok(businessResource);
        }

        [HttpGet]
        [Route("getByBusinessRegNoAndOffice/{regNo}/{officeid}")]
        public async Task<ActionResult<BusinessResource>> GetByBusinessRegNoAndOffice(string regNo, int officeid)
        {
            var business = await _businessService.GetByRegNoAndOffice(regNo, officeid);
            var businessResource = _mapper.Map<Business, BusinessResource>(business);
            return Ok(businessResource);
        }

        [HttpGet]
        [Route("getBusinessByApplicationNo/{applicationNo}")]
        public async Task<ActionResult<BusinessResource>> GetByApplicationNo(string applicationNo)
        {
            var business = await _businessService.GetByApplicationNo(applicationNo);
            var businessResource = _mapper.Map<Business, BusinessResource>(business);
            return Ok(businessResource);
        }

        [HttpGet]
        [Route("getByBusinessApplicationNoAndOffice/{applicationNo}/{officeid}")]
        public async Task<ActionResult<BusinessResource>> GetByBusinessApplicationNoAndOffice(string applicationNo, int officeid)
        {
            var business = await _businessService.GetByApplicationNoAndOffice(applicationNo, officeid);
            var businessResource = _mapper.Map<Business, BusinessResource>(business);
            return Ok(businessResource);
        }

        //[HttpPost("saveBusiness")]
        //public async Task<ActionResult<BusinessResource>> CreateBusiness([FromBody] BusinessResource saveBusinessResource)
        //{
        //    try
        //    {
        //        var businessToCreate = _mapper.Map<BusinessResource, Business>(saveBusinessResource);

        //    var newBusiness = await _businessService.Create(businessToCreate);

        //    //var business = await _businessService.GetById(newBusiness.Id.Value);

        //    //var businessResource = _mapper.Map<Business, BusinessResource>(business);

        //    //return Ok(businessResource);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    return Ok(saveBusinessResource);
        //}

        //[HttpGet]
        //[Route("getBusinessTaxForBusinessIdAndYear/{businessid}/{year}")]
        //public async Task<ActionResult<BusinessTaxResource>> GetBusinessTaxForBusinessIdAndYear(int businessid, int year)
        //{
        //    var businessTax = await _businessTaxService.GetBusinessTaxForBusinessIdAndYear(businessid, year);
        //    var businessTaxResource = _mapper.Map<BusinessTaxes, BusinessTaxResource>(businessTax);
        //    return Ok(businessTaxResource);
        //}

        [HttpGet]
        [Route("getBusinessTaxForBusinessIdAndYear/{businessid}/{year}")]
        public async Task<ActionResult<Business>> GetBusinessTaxForBusinessIdAndYear(int businessid, int year)
        {
            var business = await _businessService.GetById(businessid);
            

            business.BusinessNature = new BusinessNature();
            business.BusinessSubNature = new BusinessSubNature();
            business.TaxType = new TaxType();
            business.BusinessOwner = new Partner();
            business.PropertyOwner = new Partner();
            business.BusinessPlace = new BusinessPlace();

            try
            {
                business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                    business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                    business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                var businessTaxResource = await _businessTaxService.GetBusinessTaxForBusinessIdAndYear(businessid, year);
                business.BusinessTaxes = businessTaxResource.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            var businessResource = _mapper.Map<Business, BusinessResource>(business);

            return Ok(businessResource);

        }

        [HttpPost("saveBusiness")]
        public async Task<ActionResult<Business>> CreateBusiness(SaveBusinessResource obj)
        {
            try
            {
                if (obj != null && obj.Id == 0 && (obj.BusinessTaxes != null) && (obj.BusinessTaxes.Count == 1))
                {
                    var businessResource = obj;
                    
                    System.DateTime dt = System.DateTime.Now;

                    for (int i = 0; i < businessResource.BusinessTaxes.Count(); i++)
                    {
                        businessResource.BusinessTaxes[i].CreatedAt = System.DateTime.Now ;
                    }

                    var businessToCreate = _mapper.Map<SaveBusinessResource, Business>(businessResource);

                    try
                    {
                        businessToCreate.Id = null;
                    var newBusiness = await _businessService.CreateBusiness(businessToCreate);

                        if (businessToCreate.BusinessOwnerId != null && businessToCreate.BusinessOwnerId != 0)
                        { 
                           var businessownertobeupdated = await _partnerService.GetById(businessToCreate.BusinessOwnerId.Value);
                            var businessownerobj = businessownertobeupdated;
                            businessownerobj.IsEditable = 0;

                            await _partnerService.Update(businessownertobeupdated, businessownerobj);
                        }

                        var business = await _businessService.GetById(newBusiness.Id.Value);

                        business.BusinessNature = new BusinessNature();
                        business.BusinessSubNature = new BusinessSubNature();
                        business.TaxType = new TaxType();
                        business.BusinessOwner = new Partner();
                        business.PropertyOwner = new Partner();
                        business.BusinessPlace = new BusinessPlace();

                        try
                        {
                            business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                            business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                            business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                            business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                            if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                                business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);
                            else
                                business.PropertyOwner = null;

                            if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                                business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);
                            else
                                business.BusinessPlace = null;

                            var businessTaxResource = await _businessTaxService.GetBusinessTaxForBusinessId(business.Id.Value);
                            business.BusinessTaxes = businessTaxResource.ToList();

                        }
                        catch (Exception ex)
                        {
                            return BadRequest(ex.Message.ToString());
                        }
                        businessResource = _mapper.Map<Business, SaveBusinessResource>(business);

                        return Ok(businessResource);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                    return BadRequest();
                }
                else
                {
                    try
                    {
                        var businessToBeUpdate = await _businessService.GetById(obj.Id.Value);
                        var businessTaxResource = await _businessTaxService.GetBusinessTaxForBusinessId(businessToBeUpdate.Id.Value);
                        businessToBeUpdate.BusinessTaxes = businessTaxResource.ToList();

                        if (businessToBeUpdate == null)
                            return NotFound();

                        var product = _mapper.Map<SaveBusinessResource, Business>(obj);
                        await _businessService.Update(businessToBeUpdate, product);

                        await _businessTaxService.Update(businessToBeUpdate.BusinessTaxes[0], product.BusinessTaxes[0]);

                        var updatedBusiness = await _businessService.GetById(obj.Id.Value);
                        //updatedBusiness.BusinessTaxes.Add = await _businessTaxService.GetById(obj.BusinessTaxes[0].Id.Value);

                        var updatedBusinessResource = _mapper.Map<Business, BusinessResource>(updatedBusiness);

                        return Ok(updatedBusinessResource);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [HttpPost("updateBusiness")]
        public async Task<ActionResult<BusinessResource>> UpdateBusiness(BusinessResource saveBusinessResource)
        {
            try
            {
                var businessToBeUpdate = await _businessService.GetById(saveBusinessResource.Id.Value);

            if (businessToBeUpdate == null)
                return NotFound();

            var product = _mapper.Map<BusinessResource, Business>(saveBusinessResource);

            await _businessService.Update(businessToBeUpdate, product);

            var updatedBusiness = await _businessService.GetById(saveBusinessResource.Id.Value);
            var updatedBusinessResource = _mapper.Map<Business, BusinessResource>(updatedBusiness);

            return Ok(updatedBusinessResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("deleteBusiness/{id}")]
        public async Task<IActionResult> DeleteBusiness(int id)
        {
            if (id == 0)
                return BadRequest();

            var business = await _businessService.GetById(id);

            if (business == null)
                return NotFound();

            await _businessService.Delete(business);

            return NoContent();
        }

        [HttpGet]
        [Route("getAllBusinessesForSabhaId/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessBySabhaId([FromRoute] int SabhaId)
        {
            try
            {

                var businesses = await _businessService.GetAllForSabha(SabhaId);
                //var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses).ToList();

                foreach (var business in businesses)
                {
                    //var businessTaxes = await _businessTaxService.GetBusinessTaxForBusinessIdAndYear(business.Id.Value, 2023);
                    //var businessTaxesResources = _mapper.Map<IEnumerable<BusinessTaxes>, IEnumerable<BusinessTaxesResource>>(businessTaxes).ToList();
                    //business.BusinessTaxes=businessTaxesResources;

                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();

                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        var businessTaxResource = await _businessTaxService.GetBusinessTaxForBusinessId(business.Id.Value);
                        business.BusinessTaxes = businessTaxResource.ToList();

                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                    //var businessResource = _mapper.Map<Business, BusinessResource>(business);
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllBusinessesForBusinessOwnerId/{businessownerid}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessesForBusinessOwnerId([FromRoute] int businessownerid)
        {
            //var businesses = await _businessService.GetAllForBusinessOwnerId(businessownerid);
            //var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses);

            //return Ok(businessResources);

            try
            {

                var businesses = await _businessService.GetAllForBusinessOwnerId(businessownerid);
                //var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses).ToList();

                foreach (var business in businesses)
                {
                    //var businessTaxes = await _businessTaxService.GetBusinessTaxForBusinessIdAndYear(business.Id.Value, 2023);
                    //var businessTaxesResources = _mapper.Map<IEnumerable<BusinessTaxes>, IEnumerable<BusinessTaxesResource>>(businessTaxes).ToList();
                    //business.BusinessTaxes=businessTaxesResources;

                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();

                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        var businessTaxResource = await _businessTaxService.GetBusinessTaxForBusinessId(business.Id.Value);
                        business.BusinessTaxes = businessTaxResource.ToList();

                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                    //var businessResource = _mapper.Map<Business, BusinessResource>(business);
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllForBusinessNatureAndSabha/{natureid}/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllForBusinessNatureAndSabha([FromRoute] int natureid, int sabhaid)
        {
            var businesses = await _businessService.GetAllForBusinessNatureAndSabha(natureid, sabhaid);
            var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses);

            return Ok(businessResources);
        }

        [HttpGet]
        [Route("getAllForBusinessSubNatureAndSabha/{subnatureid}/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllForBusinessSubNatureAndSabha([FromRoute] int subnatureid, int sabhaid)
        {
            var businesses = await _businessService.GetAllForBusinessSubNatureAndSabha(subnatureid, sabhaid);
            var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses);

            return Ok(businessResources);
        }


        [HttpGet]
        [Route("getAllBusinessAndIndustrialTaxesForOwnerId/{businessownerid}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessAndIndustrialTaxesForOwnerId([FromRoute] int businessownerid)
        {
            //var businesses = await _businessService.GetAllForBusinessOwnerId(businessownerid);
            //var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses);

            //return Ok(businessResources);

            try
            {
                var businesses = await _businessService.GetAllBusinessAndIndustrialTaxesForOwnerId(businessownerid);
                //var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses).ToList();

                foreach (var business in businesses)
                {
                    //var businessTaxes = await _businessTaxService.GetBusinessTaxForBusinessIdAndYear(business.Id.Value, 2023);
                    //var businessTaxesResources = _mapper.Map<IEnumerable<BusinessTaxes>, IEnumerable<BusinessTaxesResource>>(businessTaxes).ToList();
                    //business.BusinessTaxes=businessTaxesResources;

                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();

                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        var businessTaxResource = await _businessTaxService.GetBusinessTaxForBusinessId(business.Id.Value);
                        business.BusinessTaxes = businessTaxResource.ToList();

                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                    //var businessResource = _mapper.Map<Business, BusinessResource>(business);
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessAndIndustrialTaxesForOwnerIdAndSabhaId/{businessownerid}/{sabhaId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessAndIndustrialTaxesForOwnerIdAndSabhaId([FromRoute] int businessownerid, int sabhaId)
        {
            //var businesses = await _businessService.GetAllForBusinessOwnerId(businessownerid);
            //var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses);

            //return Ok(businessResources);

            try
            {
                var businesses = await _businessService.GetAllBusinessAndIndustrialTaxesForOwnerIdAndSabhaId(businessownerid, sabhaId);
                //var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses).ToList();

                foreach (var business in businesses)
                {
                    //var businessTaxes = await _businessTaxService.GetBusinessTaxForBusinessIdAndYear(business.Id.Value, 2023);
                    //var businessTaxesResources = _mapper.Map<IEnumerable<BusinessTaxes>, IEnumerable<BusinessTaxesResource>>(businessTaxes).ToList();
                    //business.BusinessTaxes=businessTaxesResources;

                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();

                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        var businessTaxResource = await _businessTaxService.GetBusinessTaxForBusinessId(business.Id.Value);
                        business.BusinessTaxes = businessTaxResource.ToList();

                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                    //var businessResource = _mapper.Map<Business, BusinessResource>(business);
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessAndIndustrialTaxesForSabha/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessAndIndustrialTaxesForSabha([FromRoute] int SabhaId)
        {
            try
            {

                var businesses = await _businessService.GetAllBusinessAndIndustrialTaxesForSabha(SabhaId);
                //var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses).ToList();

                foreach (var business in businesses)
                {
                    //var businessTaxes = await _businessTaxService.GetBusinessTaxForBusinessIdAndYear(business.Id.Value, 2023);
                    //var businessTaxesResources = _mapper.Map<IEnumerable<BusinessTaxes>, IEnumerable<BusinessTaxesResource>>(businessTaxes).ToList();
                    //business.BusinessTaxes=businessTaxesResources;

                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();

                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        var businessTaxResource = await _businessTaxService.GetBusinessTaxForBusinessId(business.Id.Value);
                        business.BusinessTaxes = businessTaxResource.ToList();

                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                    //var businessResource = _mapper.Map<Business, BusinessResource>(business);
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessAndIndustrialTaxesForSabhaAndOfficer/{SabhaId}/{officerId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessAndIndustrialTaxesForSabhaAndOfficer([FromRoute] int SabhaId, int officerid)
        {
            try
            {

                var businesses = await _businessService.GetAllBusinessAndIndustrialTaxesForSabhaAndOfficer(SabhaId, officerid);
                //var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses).ToList();

                foreach (var business in businesses)
                {
                    //var businessTaxes = await _businessTaxService.GetBusinessTaxForBusinessIdAndYear(business.Id.Value, 2023);
                    //var businessTaxesResources = _mapper.Map<IEnumerable<BusinessTaxes>, IEnumerable<BusinessTaxesResource>>(businessTaxes).ToList();
                    //business.BusinessTaxes=businessTaxesResources;

                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();

                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        var businessTaxResource = await _businessTaxService.GetBusinessTaxForBusinessId(business.Id.Value);
                        business.BusinessTaxes = businessTaxResource.ToList();

                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                    //var businessResource = _mapper.Map<Business, BusinessResource>(business);
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        

        [HttpGet]
        [Route("getAllBusinessLicensesForSabha/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForSabha([FromRoute] int SabhaId)
        {
            try
            {
                var businesses = await _businessService.GetAllBusinessLicensesForSabha(SabhaId);
                //var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses).ToList();

                foreach (var business in businesses)
                {
                    //var businessTaxes = await _businessTaxService.GetBusinessTaxForBusinessIdAndYear(business.Id.Value, 2023);
                    //var businessTaxesResources = _mapper.Map<IEnumerable<BusinessTaxes>, IEnumerable<BusinessTaxesResource>>(businessTaxes).ToList();
                    //business.BusinessTaxes=businessTaxesResources;

                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();

                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        var businessTaxResource = await _businessTaxService.GetBusinessTaxForBusinessId(business.Id.Value);
                        business.BusinessTaxes = businessTaxResource.ToList();

                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                    //var businessResource = _mapper.Map<Business, BusinessResource>(business);
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllBusinessLicensesForSabhaAndOfficer/{SabhaId}/{OfficerId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForSabhaAndOfficer([FromRoute] int SabhaId,int OfficerId)
        {
            try
            {
                var businesses = await _businessService.GetAllBusinessLicensesForSabhaAndOfficer(SabhaId,OfficerId);
                //var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses).ToList();

                foreach (var business in businesses)
                {
                    //var businessTaxes = await _businessTaxService.GetBusinessTaxForBusinessIdAndYear(business.Id.Value, 2023);
                    //var businessTaxesResources = _mapper.Map<IEnumerable<BusinessTaxes>, IEnumerable<BusinessTaxesResource>>(businessTaxes).ToList();
                    //business.BusinessTaxes=businessTaxesResources;

                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();

                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        var businessTaxResource = await _businessTaxService.GetBusinessTaxForBusinessId(business.Id.Value);
                        business.BusinessTaxes = businessTaxResource.ToList();

                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                    //var businessResource = _mapper.Map<Business, BusinessResource>(business);
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet]
        //[Route("getAllBusinessLicensesForSabhaAndTaxStatus/{SabhaId}")]
        //public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForSabhaAndTaxStatus([FromRoute] int SabhaId, TaxStatus taxStatus)
        //{
        //    try
        //    {

        //        var businesses = await _businessService.GetAllBusinessLicensesForSabhaAndTaxStatus(SabhaId, taxStatus); ;
        //        //var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses).ToList();

        //        foreach (var business in businesses)
        //        {
        //            //var businessTaxes = await _businessTaxService.GetBusinessTaxForBusinessIdAndYear(business.Id.Value, 2023);
        //            //var businessTaxesResources = _mapper.Map<IEnumerable<BusinessTaxes>, IEnumerable<BusinessTaxesResource>>(businessTaxes).ToList();
        //            //business.BusinessTaxes=businessTaxesResources;

        //            business.BusinessNature = new BusinessNature();
        //            business.BusinessSubNature = new BusinessSubNature();
        //            business.TaxType = new TaxType();
        //            business.BusinessOwner = new Partner();
        //            business.PropertyOwner = new Partner();
        //            business.BusinessPlace = new BusinessPlace();

        //            try
        //            {
        //                business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
        //                business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
        //                business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
        //                business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

        //                if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
        //                    business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

        //                if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
        //                    business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

        //                var businessTaxResource = await _businessTaxService.GetBusinessTaxForBusinessId(business.Id.Value);
        //                business.BusinessTaxes = businessTaxResource.ToList();

        //            }
        //            catch (Exception ex)
        //            {
        //                return BadRequest(ex.Message.ToString());
        //            }
        //            //var businessResource = _mapper.Map<Business, BusinessResource>(business);
        //        }
        //        var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses).ToList();
        //        return Ok(businessResources);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        [HttpGet]
        [Route("getAllBusinessLicensesForMOHPendingApproval/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForMOHPendingApproval([FromRoute] int SabhaId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForMOHPendingApproval(SabhaId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessLicensesForSecretaryPendingApproval/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForSecretaryPendingApproval([FromRoute] int SabhaId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForSecretaryPendingApproval(SabhaId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessLicensesForChairmanPendingApproval/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForChairmanPendingApproval([FromRoute] int SabhaId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForChairmanPendingApproval(SabhaId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessLicensesForMOHApproved/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForMOHApproved([FromRoute] int SabhaId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForMOHApproved(SabhaId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessLicensesForSecretaryApproved/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForSecretaryApproved([FromRoute] int SabhaId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForSecretaryApproved(SabhaId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessLicensesForChairmanApproved/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForChairmanApproved([FromRoute] int SabhaId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForChairmanApproved(SabhaId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet]
        [Route("getAllBusinessLicensesForMOHDisapproved/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForMOHDisapproved([FromRoute] int SabhaId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForMOHDisapproved(SabhaId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessLicensesForSecretaryDisapproved/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForSecretaryDisapproved([FromRoute] int SabhaId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForSecretaryDisapproved(SabhaId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessLicensesForChairmanDisapproved/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForChairmanDisapproved([FromRoute] int SabhaId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForChairmanDisapproved(SabhaId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessLicensesForSabhaAndTaxStatus/{SabhaId}/{taxstatus}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForSabhaAndTaxStatus([FromRoute] int SabhaId, TaxStatus taxstatus)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForSabhaAndTaxStatus(SabhaId,  taxstatus); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllBusinessLicensesForAllApproved/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForAllApproved([FromRoute] int SabhaId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForAllApproved(SabhaId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllBusinessLicensesForDisapprovedAny/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForDisapprovedAny([FromRoute] int SabhaId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForDisapprovedAny(SabhaId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllBusinessLicensesForApprovalPendingAny/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForApprovalPendingAny([FromRoute] int SabhaId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForApprovalPendingAny(SabhaId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllBusinessLicensesForOfficerIdApprovalPendingAnyAnd/{SabhaId}/{officerId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForOfficerIdApprovalPendingAny([FromRoute] int SabhaId, int officerId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForOfficerIdApprovalPendingAny(SabhaId, officerId); 
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }











        [HttpGet]
        [Route("getAllBusinessLicensesForOfficerIdMOHPendingApproval/{SabhaId}/{OfficerId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForOfficerIdMOHPendingApproval([FromRoute] int SabhaId, int OfficerId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForOfficerIdMOHPendingApproval(SabhaId, OfficerId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessLicensesForOfficerIdSecretaryPendingApproval/{SabhaId}/{OfficerId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForOfficerIdSecretaryPendingApproval([FromRoute] int SabhaId, int OfficerId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForOfficerIdSecretaryPendingApproval(SabhaId, OfficerId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessLicensesForOfficerIdChairmanPendingApproval/{SabhaId}/{OfficerId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForOfficerIdChairmanPendingApproval([FromRoute] int SabhaId, int OfficerId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForOfficerIdChairmanPendingApproval(SabhaId, OfficerId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessLicensesForOfficerIdMOHApproved/{SabhaId}/{OfficerId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForOfficerIdMOHApproved([FromRoute] int SabhaId, int OfficerId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForOfficerIdMOHApproved(SabhaId, OfficerId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessLicensesForOfficerIdSecretaryApproved/{SabhaId}/{OfficerId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForOfficerIdSecretaryApproved([FromRoute] int SabhaId, int OfficerId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForOfficerIdSecretaryApproved(SabhaId, OfficerId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessLicensesForOfficerIdChairmanApproved/{SabhaId}/{OfficerId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForOfficerIdChairmanApproved([FromRoute] int SabhaId, int OfficerId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForOfficerIdChairmanApproved(SabhaId, OfficerId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet]
        [Route("getAllBusinessLicensesForOfficerIdMOHDisapproved/{SabhaId}/{OfficerId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForOfficerIdMOHDisapproved([FromRoute] int SabhaId, int OfficerId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForOfficerIdMOHDisapproved(SabhaId, OfficerId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessLicensesForOfficerIdSecretaryDisapproved/{SabhaId}/{OfficerId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForOfficerIdSecretaryDisapproved([FromRoute] int SabhaId, int OfficerId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForOfficerIdSecretaryDisapproved(SabhaId, OfficerId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessLicensesForOfficerIdChairmanDisapproved/{SabhaId}/{OfficerId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForOfficerIdChairmanDisapproved([FromRoute] int SabhaId, int OfficerId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForOfficerIdChairmanDisapproved(SabhaId, OfficerId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessLicensesForOfficerIdSabhaAndTaxStatus/{SabhaId}/{taxstatus}/{OfficerId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForOfficerIdSabhaAndTaxStatus([FromRoute] int SabhaId, TaxStatus taxstatus, int OfficerId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForOfficerIdSabhaAndTaxStatus(SabhaId, taxstatus, OfficerId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllBusinessLicensesForOfficerIdAllApproved/{SabhaId}/{OfficerId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForOfficerIdAllApproved([FromRoute] int SabhaId, int OfficerId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForOfficerIdAllApproved(SabhaId, OfficerId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllBusinessLicensesForOfficerIdDisapprovedAny/{SabhaId}/{OfficerId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessLicensesForOfficerIdDisapprovedAny([FromRoute] int SabhaId, int OfficerId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessLicensesForOfficerIdDisapprovedAny(SabhaId, OfficerId); ;
                var MOHPendingApprovalBusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        MOHPendingApprovalBusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(MOHPendingApprovalBusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("updateBusinessTaxLine")]
        public async Task<ActionResult<BusinessResource>> UpdateBusiness(BusinessTaxesResource businessTaxResource)
        {
            try
            {
                var businessTaxToBeUpdate = await _businessTaxService.GetById(businessTaxResource.Id.Value);

                if (businessTaxToBeUpdate == null)
                    return NotFound();

                var businestaxline = _mapper.Map<BusinessTaxesResource, BusinessTaxes>(businessTaxResource);

                await _businessTaxService.Update(businessTaxToBeUpdate, businestaxline);

                var business = await _businessService.GetById(businestaxline.BusinessId);

                business.BusinessNature = new BusinessNature();
                business.BusinessSubNature = new BusinessSubNature();
                business.TaxType = new TaxType();
                business.BusinessOwner = new Partner();
                business.PropertyOwner = new Partner();
                business.BusinessPlace = new BusinessPlace();

                try
                {
                    business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                    business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                    business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                    business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                    if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                        business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                    if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                        business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                    //var businessTaxResource = await _businessTaxService.GetBusinessTaxForBusinessId(business.Id.Value);
                    business.BusinessTaxes.Add(businestaxline);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message.ToString());
                }
                var businessResource = _mapper.Map<Business, BusinessResource>(business);

                return Ok(businessResource);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet]
        [Route("getAllBusinessAndIndustrialTaxesForSabhaAndOfficerAndStatus/{SabhaId}/{taxstatus}/{OfficerId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessAndIndustrialTaxesForSabhaAndOfficerAndStatus([FromRoute] int SabhaId, TaxStatus taxstatus, int OfficerId)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessAndIndustrialTaxesForSabhaAndOfficerAndStatus(SabhaId, taxstatus, OfficerId); ;
                var BusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        BusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(BusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllBusinessAndIndustrialTaxesForSabhaAndStatus/{SabhaId}/{taxstatus}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessAndIndustrialTaxesForSabhaAndOfficerAndStatus([FromRoute] int SabhaId, TaxStatus taxstatus)
        {
            try
            {
                var businessTaxes = await _businessTaxService.GetAllBusinessAndIndustrialTaxesForSabhaAndStatus(SabhaId, taxstatus); 
                var BusinessList = new List<Business>();
                foreach (var businesstax in businessTaxes)
                {
                    var business = await _businessService.GetById(businesstax.BusinessId);
                    business.BusinessNature = new BusinessNature();
                    business.BusinessSubNature = new BusinessSubNature();
                    business.TaxType = new TaxType();
                    business.BusinessOwner = new Partner();
                    business.PropertyOwner = new Partner();
                    business.BusinessPlace = new BusinessPlace();
                    business.BusinessTaxes = new List<BusinessTaxes>();
                    try
                    {
                        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
                        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
                        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
                        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

                        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
                            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

                        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
                            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

                        business.BusinessTaxes.Add(businesstax);
                        BusinessList.Add(business);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                }
                var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(BusinessList).ToList();
                return Ok(businessResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //[HttpGet]
        //[Route("getAllBusinessAndIndustrialTaxesForSabhaAndStatus/{SabhaId}/{taxstatus}")]
        //public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinessAndIndustrialTaxesForSabhaAndOfficerAndStatus([FromRoute] int SabhaId, TaxStatus taxstatus)
        //{
        //    try
        //    {
        //        var BusinessList = await _businessService.GetAllBusinessAndIndustrialTaxesForSabhaAndStatus(SabhaId, taxstatus);
        //        foreach (var business in BusinessList)
        //        {
        //            business.BusinessNature = new BusinessNature();
        //            business.BusinessSubNature = new BusinessSubNature();

        //            business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
        //            business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
        //        }

        //        //var businessTaxes = await _businessTaxService.GetAllBusinessAndIndustrialTaxesForSabhaAndStatus(SabhaId, taxstatus); 
        //        //var BusinessList = new List<Business>();
        //        //foreach (var businesstax in businessTaxes)
        //        //{
        //        //    var business = await _businessService.GetById(businesstax.BusinessId);
        //        //    business.BusinessNature = new BusinessNature();
        //        //    business.BusinessSubNature = new BusinessSubNature();
        //        //    business.TaxType = new TaxType();
        //        //    business.BusinessOwner = new Partner();
        //        //    business.PropertyOwner = new Partner();
        //        //    business.BusinessPlace = new BusinessPlace();
        //        //    business.BusinessTaxes = new List<BusinessTaxes>();
        //        //    try
        //        //    {
        //        //        business.BusinessNature = await _businessNatureService.GetBusinessNatureById(business.BusinessNatureId.Value);
        //        //        business.BusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(business.BusinessSubNatureId.Value);
        //        //        business.TaxType = await _taxTypeService.GetTaxTypeById(business.TaxTypeId.Value);
        //        //        business.BusinessOwner = await _partnerService.GetById(business.BusinessOwnerId.Value);

        //        //        if (business.PropertyOwnerId != null && business.PropertyOwnerId != 0)
        //        //            business.PropertyOwner = await _partnerService.GetById(business.PropertyOwnerId.Value);

        //        //        if (business.BusinessPlaceId != null && business.BusinessPlaceId != 0)
        //        //            business.BusinessPlace = await _businessPlaceService.GetById(business.BusinessPlaceId.Value);

        //        //        business.BusinessTaxes.Add(businesstax);
        //        //        BusinessList.Add(business);
        //        //    }
        //        //    catch (Exception ex)
        //        //    {
        //        //        return BadRequest(ex.Message.ToString());
        //        //    }
        //        //}
        //        var businessResources = _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(BusinessList).ToList();
        //        return Ok(businessResources);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
