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
using CAT20.Core.Services.Vote;
using CAT20.WebApi.Resources.TradeTax;
using CAT20.Core.Services.TradeTax;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.Mixin;

namespace CAT20.WebApi.Controllers.TradeTax
{
    [Route("api/tradetax/businessSubNatures")]
    [ApiController]
    public class BusinessSubNaturesController : BaseController
    {
        private readonly IBusinessSubNatureService _businessSubNatureService;
        private readonly IAccountDetailService _accountDetailService;
        private readonly IMapper _mapper;

        public BusinessSubNaturesController(IBusinessSubNatureService businessSubNatureService, IMapper mapper, IAccountDetailService accountDetailService)
        {
            _mapper = mapper;
            _businessSubNatureService = businessSubNatureService;
            _accountDetailService = accountDetailService;
        }

        [HttpGet("getAllBusinessSubNatures")]
        public async Task<ActionResult<IEnumerable<BusinessSubNature>>> GetAllBusinessSubNatures()
        {
            var businessSubNatures = await _businessSubNatureService.GetAllBusinessSubNatures();

            var businessSubNatureResources = _mapper.Map<IEnumerable<BusinessSubNature>, IEnumerable<BusinessSubNatureResource>>(businessSubNatures);

            return Ok(businessSubNatureResources);
        }

        [HttpGet]
        [Route("getBusinessSubNatureById/{id}")]
        public async Task<ActionResult<BusinessSubNatureResource>> GetBusinessSubNatureById([FromRoute] int id)
        {
            var businessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(id);
            var businessSubNatureResource = _mapper.Map<BusinessSubNature, BusinessSubNatureResource>(businessSubNature);
            return Ok(businessSubNatureResource);
        }

        [HttpPost("saveBusinessSubNature")]
        public async Task<ActionResult<BusinessSubNatureResource>> CreateBusinessSubNature([FromBody] BusinessSubNatureResource saveBusinessSubNatureResource)
        {

            var businessSubNatureToCreate = _mapper.Map<BusinessSubNatureResource, BusinessSubNature>(saveBusinessSubNatureResource);

            var newBusinessSubNature = await _businessSubNatureService.CreateBusinessSubNature(businessSubNatureToCreate);

            var businessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(newBusinessSubNature.ID.Value);

            var businessSubNatureResource = _mapper.Map<BusinessSubNature, BusinessSubNatureResource>(businessSubNature);

            return Ok(businessSubNatureResource);
        }


        [HttpPost("bulkSaveBusinessSubNature")]
        public async Task<ActionResult> BulkCreateBusinessSubNature([FromBody] List<BusinessSubNatureResource> objBusinessSubNatureResourceList)
        {
            try
            {
                if (objBusinessSubNatureResourceList != null && objBusinessSubNatureResourceList.Count > 0)
                {
                    for (int i = 0; i < objBusinessSubNatureResourceList.Count; i++)
                    {
                        var businessSubNatureToCreate = _mapper.Map<BusinessSubNatureResource, BusinessSubNature>(objBusinessSubNatureResourceList[i]);
                        var newBusinessSubNature = await _businessSubNatureService.CreateBusinessSubNature(businessSubNatureToCreate);

                        var businessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(newBusinessSubNature.ID.Value);
                        var businessSubNatureResource = _mapper.Map<BusinessSubNature, BusinessSubNatureResource>(businessSubNature);
                    }
                    return Ok(objBusinessSubNatureResourceList);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }


        [HttpPost("updateBusinessSubNature")]
        public async Task<ActionResult<BusinessSubNatureResource>> UpdateBusinessSubNature(BusinessSubNatureResource saveBusinessSubNatureResource)
        {
            var businessSubNatureToBeUpdate = await _businessSubNatureService.GetBusinessSubNatureById(saveBusinessSubNatureResource.ID.Value);

            if (businessSubNatureToBeUpdate == null)
                return NotFound();

            var product = _mapper.Map<BusinessSubNatureResource, BusinessSubNature>(saveBusinessSubNatureResource);

            await _businessSubNatureService.UpdateBusinessSubNature(businessSubNatureToBeUpdate, product);

            var updatedBusinessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(saveBusinessSubNatureResource.ID.Value);
            var updatedBusinessSubNatureResource = _mapper.Map<BusinessSubNature, BusinessSubNatureResource>(updatedBusinessSubNature);

            return Ok(updatedBusinessSubNatureResource);
        }

        [HttpPost]
        [Route("deleteBusinessSubNature/{id}")]
        public async Task<IActionResult> DeleteBusinessSubNature([FromRoute] int id)
        {
            if (id == 0)
                return BadRequest("No Records Found");

            var businessSubNature = await _businessSubNatureService.GetBusinessSubNatureById(id);

            if (businessSubNature == null)
                return BadRequest("No record Found");

            try
            {
              await _businessSubNatureService.DeleteBusinessSubNature(businessSubNature);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            return NoContent();
        }

        [HttpGet]
        [Route("getAllBusinessSubNaturesForSabhaId/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<BusinessSubNature>>> GetAllBusinessSubNaturesForSabhaId([FromRoute] int SabhaId)
        {
            var businessSubNatures = await _businessSubNatureService.GetAllBusinessSubNaturesForSabhaId(SabhaId);
            var businessSubNatureResources = _mapper.Map<IEnumerable<BusinessSubNature>, IEnumerable<BusinessSubNatureResource>>(businessSubNatures);

            return Ok(businessSubNatureResources);
        }


        [HttpGet]
        [Route("getAllBusinessSubNaturesForBusinessNatureID/{BusinessNatureID}")]
        public async Task<ActionResult<IEnumerable<BusinessSubNature>>> GetAllBusinessSubNaturesForBusinessNatureID([FromRoute] int BusinessNatureID)
        {
            var businessSubNatures = await _businessSubNatureService.GetAllBusinessSubNaturesForBusinessNatureID(BusinessNatureID);

            var businessSubNatureResources = _mapper.Map<IEnumerable<BusinessSubNature>, IEnumerable<BusinessSubNatureResource>>(businessSubNatures);

            return Ok(businessSubNatureResources);
        }
    }
}
