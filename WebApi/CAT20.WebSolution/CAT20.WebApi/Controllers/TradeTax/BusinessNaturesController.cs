using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.Models.Vote;
using CAT20.Core.Models.TradeTax;
using CAT20.WebApi.Resources.Vote.Save;
using CAT20.WebApi.Resources.Vote;
using CAT20.WebApi.Resources.TradeTax;
using CAT20.Core.Services.Vote;
using CAT20.Core.Services.TradeTax;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CAT20.Services.Vote;
using CAT20.Services.TradeTax;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.Mixin;

namespace CAT20.WebApi.Controllers.TradeTax
{
    [Route("api/tradetax/businessNatures")]
    [ApiController]
    public class BusinessNaturesController : BaseController
    {
        private readonly IBusinessNatureService _businessNatureService;
        private readonly IBusinessSubNatureService _businessSubNatureService;
        private readonly IMapper _mapper;

        public BusinessNaturesController(IBusinessNatureService businessNatureService, IMapper mapper, IBusinessSubNatureService businessSubNatureService)
        {
            _mapper = mapper;
            _businessNatureService = businessNatureService;
            _businessSubNatureService = businessSubNatureService;
        }

        [HttpGet("getAllBusinessNatures")]
        public async Task<ActionResult<IEnumerable<BusinessNature>>> GetAllBusinessNatures()
        {
            var businessNatures = await _businessNatureService.GetAllBusinessNatures();
            var businessNatureResources = _mapper.Map<IEnumerable<BusinessNature>, IEnumerable<BusinessNatureResource>>(businessNatures);

            return Ok(businessNatureResources);
        }
        [HttpGet]
        [Route("getBusinessNatureById/{id}")]
        public async Task<ActionResult<BusinessNatureResource>> GetBusinessNatureById(int id)
        {
            var businessNature = await _businessNatureService.GetBusinessNatureById(id);
            var businessNatureResource = _mapper.Map<BusinessNature, BusinessNatureResource>(businessNature);
            return Ok(businessNatureResource);
        }

        [HttpPost("saveBusinessNature")]
        public async Task<ActionResult<BusinessNatureResource>> CreateBusinessNature([FromBody] BusinessNatureResource saveBusinessNatureResource)
        {
            try
            {
                var businessNatureToCreate = _mapper.Map<BusinessNatureResource, BusinessNature>(saveBusinessNatureResource);

                var newBusinessNature = await _businessNatureService.CreateBusinessNature(businessNatureToCreate);

                var businessNatureResource = _mapper.Map<BusinessNature, BusinessNatureResource>(newBusinessNature);

                return Ok(businessNatureResource);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("bulkSaveBusinessNature")]
        public async Task<ActionResult> CreateBusinessNature([FromBody] List<BusinessNatureResource> objBusinessNatureResourceList)
        {
            try { 
            if (objBusinessNatureResourceList != null && objBusinessNatureResourceList.Count > 0)
            {
                for (int i = 0; i < objBusinessNatureResourceList.Count; i++)
                {
                    var businessNatureToCreate = _mapper.Map<BusinessNatureResource, BusinessNature>(objBusinessNatureResourceList[i]);
                    var newBusinessNature = await _businessNatureService.CreateBusinessNature(businessNatureToCreate);

                    var businessNature = await _businessNatureService.GetBusinessNatureById(newBusinessNature.ID.Value);
                    var businessNatureResource = _mapper.Map<BusinessNature, BusinessNatureResource>(businessNature);
                }
                return Ok(objBusinessNatureResourceList);
            }
            else { 
            return BadRequest();
            }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("updateBusinessNature")]
        public async Task<ActionResult<BusinessNatureResource>> UpdateBusinessNature(BusinessNatureResource saveBusinessNatureResource)
        {
            var businessNatureToBeUpdate = await _businessNatureService.GetBusinessNatureById(saveBusinessNatureResource.ID.Value);

            if (businessNatureToBeUpdate == null)
                return NotFound();

            var product = _mapper.Map<BusinessNatureResource, BusinessNature>(saveBusinessNatureResource);

            await _businessNatureService.UpdateBusinessNature(businessNatureToBeUpdate, product);

            var updatedBusinessNature = await _businessNatureService.GetBusinessNatureById(saveBusinessNatureResource.ID.Value);
            var updatedBusinessNatureResource = _mapper.Map<BusinessNature, BusinessNatureResource>(updatedBusinessNature);

            return Ok(updatedBusinessNatureResource);
        }

        [HttpPost]
        [Route("deleteBusinessNature/{id}")]
        public async Task<IActionResult> DeleteBusinessNature(int id)
        {
            if (id == 0)
                return BadRequest("No Records Found");

            var businessNature = await _businessNatureService.GetBusinessNatureById(id);

            if (businessNature == null)
                return BadRequest("No Records Found");

            var businessSubNature = (await _businessSubNatureService.GetAllBusinessSubNaturesForBusinessNatureID(businessNature.ID.Value)).Count();

            if (businessSubNature > 0)
                return BadRequest("Can't Delete. This has child records");

            await _businessNatureService.DeleteBusinessNature(businessNature);

            return NoContent();
        }

        [HttpGet]
        [Route("getAllBusinessNaturesForSabhaId/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<BusinessNature>>> GetAllBusinessNatureBySabhaId([FromRoute] int SabhaId)
        {
            var businessNatures = await _businessNatureService.GetAllBusinessNatureBySabhaId(SabhaId);
            var businessNatureResources = _mapper.Map<IEnumerable<BusinessNature>, IEnumerable<BusinessNatureResource>>(businessNatures);

            if(businessNatureResources != null)
            {
                foreach (var businessNatureResource in businessNatureResources)
                {
                    try { 
                    var businessSubNatures = await _businessSubNatureService.GetAllBusinessSubNaturesForBusinessNatureID(businessNatureResource.ID.Value);
                    if (businessSubNatures!=null)
                    {
                        var businessSubNatureResources = _mapper.Map<IEnumerable<BusinessSubNature>, IEnumerable<BusinessSubNatureResource>>(businessSubNatures).ToList();
                        businessNatureResource.businessSubNatures = businessSubNatureResources;
                    }
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }

            return Ok(businessNatureResources);
        }
    }
}
