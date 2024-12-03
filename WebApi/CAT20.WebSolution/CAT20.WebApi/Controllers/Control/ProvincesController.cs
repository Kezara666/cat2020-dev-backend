using System;
using System.Collections.Generic;
using System.Data;
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
    public class ProvincesController : BaseController
    {
        private readonly IProvinceService _provinceService;
        private readonly IMapper _mapper;

        public ProvincesController(IProvinceService provinceService, IMapper mapper)
        {
            _mapper = mapper;
            _provinceService = provinceService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Province>>> GetAllProducts()
        {
            var provinces = await _provinceService.GetAllProvinces();
            var provinceResources = _mapper.Map<IEnumerable<Province>, IEnumerable<ProvinceResource>>(provinces);

            return Ok(provinceResources);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProvinceResource>> GetProvinceById(int id)
        {
            var province = await _provinceService.GetProvinceById(id);
            var provinceResource = _mapper.Map<Province, ProvinceResource>(province);
            return Ok(provinceResource);
        }

        //[HttpPost("")]
        //public async Task<ActionResult<ProvinceResource>> CreateProvince([FromBody] SaveProvinceResource saveProvinceResource)
        //{
        //    var validator = new SaveProvinceResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveProvinceResource);

        //    if (!validationResult.IsValid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var provinceToCreate = _mapper.Map<SaveProvinceResource, Province>(saveProvinceResource);

        //    var newProvince = await _provinceService.CreateProvince(provinceToCreate);

        //    var province = await _provinceService.GetProvinceById(newProvince.ID);

        //    var provinceResource = _mapper.Map<Province, ProvinceResource>(province);

        //    return Ok(provinceResource);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<ProvinceResource>> UpdateProduct(int id, [FromBody] SaveProvinceResource saveProvinceResource)
        //{
        //    var validator = new SaveProvinceResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveProvinceResource);

        //    var requestIsInvalid = id == 0 || !validationResult.IsValid;

        //    if (requestIsInvalid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var provinceToBeUpdate = await _provinceService.GetProvinceById(id);

        //    if (provinceToBeUpdate == null)
        //        return NotFound();

        //    var product = _mapper.Map<SaveProvinceResource, Province>(saveProvinceResource);

        //    await _provinceService.UpdateProvince(provinceToBeUpdate, product);

        //    var updatedProvince = await _provinceService.GetProvinceById(id);
        //    var updatedProvinceResource = _mapper.Map<Province, ProvinceResource>(updatedProvince);

        //    return Ok(updatedProvinceResource);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var province = await _provinceService.GetProvinceById(id);

        //    if (province == null)
        //        return NotFound();

        //    await _provinceService.DeleteProvince(province);

        //    return NoContent();
        //}
    }
}
