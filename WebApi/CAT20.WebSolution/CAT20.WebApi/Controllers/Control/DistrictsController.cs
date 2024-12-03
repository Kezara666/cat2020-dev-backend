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
    public class DistrictsController : BaseController
    {
        private readonly IDistrictService _districtService;
        private readonly IMapper _mapper;

        public DistrictsController(IDistrictService districtService, IMapper mapper)
        {
            _mapper = mapper;
            _districtService = districtService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<District>>> GetAllProducts()
        {
            var districts = await _districtService.GetAllDistricts();
            var districtResources = _mapper.Map<IEnumerable<District>, IEnumerable<DistrictResource>>(districts);

            return Ok(districtResources);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DistrictResource>> GetDistrictById(int id)
        {
            var district = await _districtService.GetDistrictById(id);
            var districtResource = _mapper.Map<District, DistrictResource>(district);
            return Ok(districtResource);
        }

        //[HttpPost("")]
        //public async Task<ActionResult<DistrictResource>> CreateDistrict([FromBody] SaveDistrictResource saveDistrictResource)
        //{
        //    var validator = new SaveDistrictResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveDistrictResource);

        //    if (!validationResult.IsValid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var districtToCreate = _mapper.Map<SaveDistrictResource, District>(saveDistrictResource);

        //    var newDistrict = await _districtService.CreateDistrict(districtToCreate);

        //    var district = await _districtService.GetDistrictById(newDistrict.ID);

        //    var districtResource = _mapper.Map<District, DistrictResource>(district);

        //    return Ok(districtResource);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<DistrictResource>> UpdateProduct(int id, [FromBody] SaveDistrictResource saveDistrictResource)
        //{
        //    var validator = new SaveDistrictResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveDistrictResource);

        //    var requestIsInvalid = id == 0 || !validationResult.IsValid;

        //    if (requestIsInvalid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var districtToBeUpdate = await _districtService.GetDistrictById(id);

        //    if (districtToBeUpdate == null)
        //        return NotFound();

        //    var product = _mapper.Map<SaveDistrictResource, District>(saveDistrictResource);

        //    await _districtService.UpdateDistrict(districtToBeUpdate, product);

        //    var updatedDistrict = await _districtService.GetDistrictById(id);
        //    var updatedDistrictResource = _mapper.Map<District, DistrictResource>(updatedDistrict);

        //    return Ok(updatedDistrictResource);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var district = await _districtService.GetDistrictById(id);

        //    if (district == null)
        //        return NotFound();

        //    await _districtService.DeleteDistrict(district);

        //    return NoContent();
        //}
    }
}