using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Services.Mixin;
using CAT20.Services.Mixin;
using CAT20.WebApi.Controllers;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.Mixin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.Api.Controllers
{
    [Route("api/mixin/mixinOrderLine")]
    [ApiController]
    public class MixinOrderLineController : BaseController
    {
        private readonly IMixinOrderService _mixinOrderService;
        private readonly IMixinOrderLineService _mixinOrderLineService;
        private readonly IMapper _mapper;

        public MixinOrderLineController(IMixinOrderLineService mixinOrderLineService, IMapper mapper, IMixinOrderService mixinOrderService)
        {
            this._mapper = mapper;
            this._mixinOrderService = mixinOrderService;
            _mixinOrderLineService = mixinOrderLineService;
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<ActionResult<MixinOrderLineResource>> GetById([FromRoute] int id)
        {
            var mixinOrderLine = await _mixinOrderLineService.GetById(id);
            var mixinOrderLineResource = _mapper.Map<MixinOrderLine, MixinOrderLineResource>(mixinOrderLine);

            if (mixinOrderLineResource == null)
                return NotFound();

            return Ok(mixinOrderLineResource);
        }

        [HttpGet("getAllForOrderId/{mixinOrderId}")]
        public async Task<ActionResult<IEnumerable<MixinOrderLine>>> GetAllForOrderId(int mixinOrderId)
        {
            var mixinOrderLine = await _mixinOrderLineService.GetAllForOrderId(mixinOrderId);
            var mixinOrderLineResources = _mapper.Map<IEnumerable<MixinOrderLine>, IEnumerable<MixinOrderLineResource>>(mixinOrderLine);

            return Ok(mixinOrderLineResources);
        }



        //[HttpPost("save")]
        //public async Task<IActionResult> Save([FromBody] List<MixinOrderLineResource> objmixinOrderLineResourceList)
        //{
        //    try
        //    {
        //        if (objmixinOrderLineResourceList != null && objmixinOrderLineResourceList.Count > 0 && objmixinOrderLineResourceList[0].Id == 0)
        //        {
        //            for (int i = 0; i < objmixinOrderLineResourceList.Count; i++)
        //            {
        //                objmixinOrderLineResourceList[i].CreatedAt = DateTime.Now;

        //                var mixinOrderLineToCreate = _mapper.Map<MixinOrderLineResource, MixinOrderLine>(objmixinOrderLineResourceList[i]);
        //                var newMixinOrderLine = await _mixinOrderLineService.Save(mixinOrderLineToCreate);
        //                var mixinOrderLine = await _mixinOrderLineService.GetById(newMixinOrderLine.Id);
        //                var mixinOrderLineResource = _mapper.Map<MixinOrderLine, MixinOrderLineResource>(mixinOrderLine);
        //            }
        //            return Ok(objmixinOrderLineResourceList);
        //        }
        //        else
        //        {
        //            var updatedMixinOrderLineResource = new MixinOrderLineResource();
        //            for (int i = 0; i < objmixinOrderLineResourceList.Count; i++)
        //            {
        //                objmixinOrderLineResourceList[i].UpdatedAt = DateTime.Now;
        //                var mixinOrderLineToBeUpdate = await _mixinOrderLineService.GetById(objmixinOrderLineResourceList[i].Id);
        //                if (mixinOrderLineToBeUpdate == null)
        //                    return NotFound();
        //                var mixinOrderLine = _mapper.Map<MixinOrderLineResource, MixinOrderLine>(objmixinOrderLineResourceList[i]);
        //                await _mixinOrderLineService.Update(mixinOrderLineToBeUpdate, mixinOrderLine);
        //                var updatedMixinOrderLine = await _mixinOrderLineService.GetById(objmixinOrderLineResourceList[i].Id);
        //                updatedMixinOrderLineResource = _mapper.Map<MixinOrderLine, MixinOrderLineResource>(updatedMixinOrderLine);
        //            }
        //            return Ok(updatedMixinOrderLineResource);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }
        //}


        [HttpPost]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            var mixinOrderLine = await _mixinOrderLineService.GetById(id);

            if (mixinOrderLine == null)
                return NotFound();

            await _mixinOrderLineService.Delete(mixinOrderLine);
                return NoContent();
        }

        //[HttpGet]
        //[Route("GetAllForOfficeId/{officeId}")]
        //public async Task<ActionResult<IEnumerable<MixinOrderLine>>> GetAllForOfficeId([FromRoute] int officeId)
        //{
        //    var mixinOrderLine = await _mixinOrderLineService.GetAllForOfficeId(officeId);
        //    var mixinOrderLineResources = _mapper.Map<IEnumerable<MixinOrderLine>, IEnumerable<MixinOrderLineResource>>(mixinOrderLine);

        //    return Ok(mixinOrderLineResources);
        //}

        //[HttpGet]
        //[Route("getAllMixinOrderLineForMixinOrderId/{id}")]
        //public async Task<ActionResult<MixinOrderLineResource>> GetAllMixinOrderLineForMixinOrderId([FromRoute] int id)
        //{
        //    var mixinOrderLine = await _mixinOrderLineService.GetAllMixinOrderLineForMixinOrderId(id);
        //    var mixinOrderLineResources = _mapper.Map<IEnumerable<MixinOrderLine>, IEnumerable<MixinOrderLineResource>>(mixinOrderLine);

        //    if (mixinOrderLineResources == null)
        //        return NotFound();

        //    return Ok(mixinOrderLineResources);
        //}

    }
}
