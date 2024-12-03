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
    public class YearsController : BaseController
    {
        private readonly IYearService _yearService;
        private readonly IMapper _mapper;

        public YearsController(IYearService yearService, IMapper mapper)
        {
            _mapper = mapper;
            _yearService = yearService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Year>>> GetAllYears()
        {
            var years = await _yearService.GetAllYears();
            var yearResources = _mapper.Map<IEnumerable<Year>, IEnumerable<YearResource>>(years);

            return Ok(yearResources);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<YearResource>> GetYearById(int id)
        {
            var year = await _yearService.GetYearById(id);
            var yearResource = _mapper.Map<Year, YearResource>(year);
            return Ok(yearResource);
        }

        //[HttpPost("")]
        //public async Task<ActionResult<YearResource>> CreateYear([FromBody] SaveYearResource saveYearResource)
        //{
        //    var validator = new SaveYearResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveYearResource);

        //    if (!validationResult.IsValid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var yearToCreate = _mapper.Map<SaveYearResource, Year>(saveYearResource);

        //    var newYear = await _yearService.CreateYear(yearToCreate);

        //    var year = await _yearService.GetYearById(newYear.ID);

        //    var yearResource = _mapper.Map<Year, YearResource>(year);

        //    return Ok(yearResource);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<YearResource>> UpdateProduct(int id, [FromBody] SaveYearResource saveYearResource)
        //{
        //    var validator = new SaveYearResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveYearResource);

        //    var requestIsInvalid = id == 0 || !validationResult.IsValid;

        //    if (requestIsInvalid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var yearToBeUpdate = await _yearService.GetYearById(id);

        //    if (yearToBeUpdate == null)
        //        return NotFound();

        //    var product = _mapper.Map<SaveYearResource, Year>(saveYearResource);

        //    await _yearService.UpdateYear(yearToBeUpdate, product);

        //    var updatedYear = await _yearService.GetYearById(id);
        //    var updatedYearResource = _mapper.Map<Year, YearResource>(updatedYear);

        //    return Ok(updatedYearResource);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var year = await _yearService.GetYearById(id);

        //    if (year == null)
        //        return NotFound();

        //    await _yearService.DeleteYear(year);

        //    return NoContent();
        //}
    }
}