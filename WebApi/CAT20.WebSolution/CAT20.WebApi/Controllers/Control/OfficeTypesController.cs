using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;
using CAT20.WebApi.Resources.Control;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.WebApi.Controllers.Control
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeTypesController : BaseController
    {
        private readonly IOfficeTypeService _officeTypeService;
        private readonly IMapper _mapper;

        public OfficeTypesController(IOfficeTypeService officeTypeService, IMapper mapper)
        {
            _mapper = mapper;
            _officeTypeService = officeTypeService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<OfficeType>>> GetAllProducts()
        {
            var officeTypes = await _officeTypeService.GetAllOfficeTypes();
            var officeTypeResources = _mapper.Map<IEnumerable<OfficeType>, IEnumerable<OfficeTypeResource>>(officeTypes);

            return Ok(officeTypeResources);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OfficeTypeResource>> GetOfficeTypeById(int id)
        {
            var officeType = await _officeTypeService.GetOfficeTypeById(id);
            var officeTypeResource = _mapper.Map<OfficeType, OfficeTypeResource>(officeType);
            return Ok(officeTypeResource);
        }

        //[HttpPost("")]
        //public async Task<ActionResult<OfficeTypeResource>> CreateOfficeType([FromBody] SaveOfficeTypeResource saveOfficeTypeResource)
        //{
        //    var validator = new SaveOfficeTypeResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveOfficeTypeResource);

        //    if (!validationResult.IsValid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var officeTypeToCreate = _mapper.Map<SaveOfficeTypeResource, OfficeType>(saveOfficeTypeResource);

        //    var newOfficeType = await _officeTypeService.CreateOfficeType(officeTypeToCreate);

        //    var officeType = await _officeTypeService.GetOfficeTypeById(newOfficeType.ID);

        //    var officeTypeResource = _mapper.Map<OfficeType, OfficeTypeResource>(officeType);

        //    return Ok(officeTypeResource);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<OfficeTypeResource>> UpdateProduct(int id, [FromBody] SaveOfficeTypeResource saveOfficeTypeResource)
        //{
        //    var validator = new SaveOfficeTypeResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveOfficeTypeResource);

        //    var requestIsInvalid = id == 0 || !validationResult.IsValid;

        //    if (requestIsInvalid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var officeTypeToBeUpdate = await _officeTypeService.GetOfficeTypeById(id);

        //    if (officeTypeToBeUpdate == null)
        //        return NotFound();

        //    var product = _mapper.Map<SaveOfficeTypeResource, OfficeType>(saveOfficeTypeResource);

        //    await _officeTypeService.UpdateOfficeType(officeTypeToBeUpdate, product);

        //    var updatedOfficeType = await _officeTypeService.GetOfficeTypeById(id);
        //    var updatedOfficeTypeResource = _mapper.Map<OfficeType, OfficeTypeResource>(updatedOfficeType);

        //    return Ok(updatedOfficeTypeResource);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var officeType = await _officeTypeService.GetOfficeTypeById(id);

        //    if (officeType == null)
        //        return NotFound();

        //    await _officeTypeService.DeleteOfficeType(officeType);

        //    return NoContent();
        //}
    }
}