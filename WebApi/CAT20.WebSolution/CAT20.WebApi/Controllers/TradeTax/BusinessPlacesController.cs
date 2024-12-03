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

namespace CAT20.WebApi.Controllers.TradeTax
{
    [Route("api/tradetax/businessPlacees")]
    [ApiController]
    public class BusinessPlacesController : BaseController
    {
        private readonly IBusinessPlaceService _businessPlaceService;
        private readonly IMapper _mapper;

        public BusinessPlacesController(IBusinessPlaceService businessPlaceService, IMapper mapper)
        {
            _mapper = mapper;
            _businessPlaceService = businessPlaceService;
        }

        [HttpGet]
        [Route("getBusinessPlaceById/{id}")]
        public async Task<ActionResult<BusinessPlaceResource>> GetBusinessPlaceById(int id)
        {
            var businessPlace = await _businessPlaceService.GetById(id);
            var businessPlaceResource = _mapper.Map<BusinessPlace, BusinessPlaceResource>(businessPlace);
            return Ok(businessPlaceResource);
        }

        [HttpGet]
        [Route("getBusinessPlaceByBusinessId/{id}")]
        public async Task<ActionResult<BusinessPlaceResource>> GetByBusinessId(int id)
        {
            var businessPlace = await _businessPlaceService.GetByBusinessId(id);
            var businessPlaceResource = _mapper.Map<BusinessPlace, BusinessPlaceResource>(businessPlace);
            return Ok(businessPlaceResource);
        }

        [HttpGet]
        [Route("getBusinessPlaceByAssessmentNo/{assessmentNo}")]
        public async Task<ActionResult<BusinessPlaceResource>> GetByAssessmentNo(string assessmentNo)
        {
            var businessPlace = await _businessPlaceService.GetByAssessmentNo(assessmentNo);
            var businessPlaceResource = _mapper.Map<BusinessPlace, BusinessPlaceResource>(businessPlace);
            return Ok(businessPlaceResource);
        }


        [HttpPost("saveBusinessPlace")]
        public async Task<ActionResult<BusinessPlaceResource>> CreateBusinessPlace([FromBody] BusinessPlaceResource saveBusinessPlaceResource)
        {
            if(saveBusinessPlaceResource.Id == null || saveBusinessPlaceResource.Id ==0)
            { 
            try {
                saveBusinessPlaceResource.Status = 1;
                saveBusinessPlaceResource.Id = null;
                var businessPlaceToCreate = _mapper.Map<BusinessPlaceResource, BusinessPlace>(saveBusinessPlaceResource);

            var newBusinessPlace = await _businessPlaceService.Create(businessPlaceToCreate);

            //var businessPlace = await _businessPlaceService.GetById(newBusinessPlace.Id.Value);

            var businessPlaceResource = _mapper.Map<BusinessPlace, BusinessPlaceResource>(newBusinessPlace);

            return Ok(businessPlaceResource);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            }
            else
            {
                var businessPlaceToBeUpdate = await _businessPlaceService.GetById(saveBusinessPlaceResource.Id.Value);

                if (businessPlaceToBeUpdate == null)
                    return NotFound();

                var product = _mapper.Map<BusinessPlaceResource, BusinessPlace>(saveBusinessPlaceResource);

                await _businessPlaceService.Update(businessPlaceToBeUpdate, product);

                var updatedBusinessPlace = await _businessPlaceService.GetById(saveBusinessPlaceResource.Id.Value);
                var updatedBusinessPlaceResource = _mapper.Map<BusinessPlace, BusinessPlaceResource>(updatedBusinessPlace);

                return Ok(updatedBusinessPlaceResource);
            }
        }

        [HttpPost("updateBusinessPlace")]
        public async Task<ActionResult<BusinessPlaceResource>> UpdateBusinessPlace(BusinessPlaceResource saveBusinessPlaceResource)
        {
            var businessPlaceToBeUpdate = await _businessPlaceService.GetById(saveBusinessPlaceResource.Id.Value);

            if (businessPlaceToBeUpdate == null)
                return NotFound();

            var product = _mapper.Map<BusinessPlaceResource, BusinessPlace>(saveBusinessPlaceResource);

            await _businessPlaceService.Update(businessPlaceToBeUpdate, product);

            var updatedBusinessPlace = await _businessPlaceService.GetById(saveBusinessPlaceResource.Id.Value);
            var updatedBusinessPlaceResource = _mapper.Map<BusinessPlace, BusinessPlaceResource>(updatedBusinessPlace);

            return Ok(updatedBusinessPlaceResource);
        }

        [HttpPost]
        [Route("deleteBusinessPlace/{id}")]
        public async Task<IActionResult> DeleteBusinessPlace(int id)
        {
            if (id == 0)
                return BadRequest();

            var businessPlace = await _businessPlaceService.GetById(id);

            if (businessPlace == null)
                return NotFound();

            await _businessPlaceService.Delete(businessPlace);

            return NoContent();
        }

        [HttpGet]
        [Route("getAllBusinessPlacesForSabhaId/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<BusinessPlace>>> GetAllBusinessPlaceBySabhaId([FromRoute] int SabhaId)
        {
            var businessPlacees = await _businessPlaceService.GetAllForSabha(SabhaId);
            var businessPlaceResources = _mapper.Map<IEnumerable<BusinessPlace>, IEnumerable<BusinessPlaceResource>>(businessPlacees);

            return Ok(businessPlaceResources);
        }

        [HttpGet]
        [Route("getAllBusinessPlacesForPropertyOwnerId/{propertyownerid}")]
        public async Task<ActionResult<IEnumerable<BusinessPlace>>> GetAllBusinessPlacesForBusinessPlaceOwnerId([FromRoute] int propertyownerid)
        {
            var businessPlacees = await _businessPlaceService.GetAllForPropertyOwnerId(propertyownerid);
            var businessPlaceResources = _mapper.Map<IEnumerable<BusinessPlace>, IEnumerable<BusinessPlaceResource>>(businessPlacees);

            return Ok(businessPlaceResources);
        }

    }
}
