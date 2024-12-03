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
    public class MonthsController : BaseController
    {
        private readonly IMonthService _monthService;
        private readonly IMapper _mapper;

        public MonthsController(IMonthService monthService, IMapper mapper)
        {
            _mapper = mapper;
            _monthService = monthService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Month>>> GetAllMonths()
        {
            var months = await _monthService.GetAllMonths();
            var monthResources = _mapper.Map<IEnumerable<Month>, IEnumerable<MonthResource>>(months);

            return Ok(monthResources);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MonthResource>> GetMonthById(int id)
        {
            var month = await _monthService.GetMonthById(id);
            var monthResource = _mapper.Map<Month, MonthResource>(month);
            return Ok(monthResource);
        }

        //[HttpPost("")]
        //public async Task<ActionResult<MonthResource>> CreateMonth([FromBody] SaveMonthResource saveMonthResource)
        //{
        //    var validator = new SaveMonthResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveMonthResource);

        //    if (!validationResult.IsValid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var monthToCreate = _mapper.Map<SaveMonthResource, Month>(saveMonthResource);

        //    var newMonth = await _monthService.CreateMonth(monthToCreate);

        //    var month = await _monthService.GetMonthById(newMonth.ID);

        //    var monthResource = _mapper.Map<Month, MonthResource>(month);

        //    return Ok(monthResource);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<MonthResource>> UpdateProduct(int id, [FromBody] SaveMonthResource saveMonthResource)
        //{
        //    var validator = new SaveMonthResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveMonthResource);

        //    var requestIsInvalid = id == 0 || !validationResult.IsValid;

        //    if (requestIsInvalid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var monthToBeUpdate = await _monthService.GetMonthById(id);

        //    if (monthToBeUpdate == null)
        //        return NotFound();

        //    var product = _mapper.Map<SaveMonthResource, Month>(saveMonthResource);

        //    await _monthService.UpdateMonth(monthToBeUpdate, product);

        //    var updatedMonth = await _monthService.GetMonthById(id);
        //    var updatedMonthResource = _mapper.Map<Month, MonthResource>(updatedMonth);

        //    return Ok(updatedMonthResource);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var month = await _monthService.GetMonthById(id);

        //    if (month == null)
        //        return NotFound();

        //    await _monthService.DeleteMonth(month);

        //    return NoContent();
        //}
    }
}