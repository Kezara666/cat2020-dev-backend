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
    public class GendersController : BaseController
    {
        private readonly IGenderService _genderService;
        private readonly IMapper _mapper;

        public GendersController(IGenderService genderService, IMapper mapper)
        {
            _mapper = mapper;
            _genderService = genderService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Gender>>> GetAllProducts()
        {
            var genders = await _genderService.GetAllGenders();
            var genderResources = _mapper.Map<IEnumerable<Gender>, IEnumerable<GenderResource>>(genders);

            return Ok(genderResources);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GenderResource>> GetGenderById(int id)
        {
            var gender = await _genderService.GetGenderById(id);
            var genderResource = _mapper.Map<Gender, GenderResource>(gender);
            return Ok(genderResource);
        }

        //[HttpPost("")]
        //public async Task<ActionResult<GenderResource>> CreateGender([FromBody] SaveGenderResource saveGenderResource)
        //{
        //    var validator = new SaveGenderResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveGenderResource);

        //    if (!validationResult.IsValid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var genderToCreate = _mapper.Map<SaveGenderResource, Gender>(saveGenderResource);

        //    var newGender = await _genderService.CreateGender(genderToCreate);

        //    var gender = await _genderService.GetGenderById(newGender.ID);

        //    var genderResource = _mapper.Map<Gender, GenderResource>(gender);

        //    return Ok(genderResource);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<GenderResource>> UpdateProduct(int id, [FromBody] SaveGenderResource saveGenderResource)
        //{
        //    var validator = new SaveGenderResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveGenderResource);

        //    var requestIsInvalid = id == 0 || !validationResult.IsValid;

        //    if (requestIsInvalid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var genderToBeUpdate = await _genderService.GetGenderById(id);

        //    if (genderToBeUpdate == null)
        //        return NotFound();

        //    var product = _mapper.Map<SaveGenderResource, Gender>(saveGenderResource);

        //    await _genderService.UpdateGender(genderToBeUpdate, product);

        //    var updatedGender = await _genderService.GetGenderById(id);
        //    var updatedGenderResource = _mapper.Map<Gender, GenderResource>(updatedGender);

        //    return Ok(updatedGenderResource);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var gender = await _genderService.GetGenderById(id);

        //    if (gender == null)
        //        return NotFound();

        //    await _genderService.DeleteGender(gender);

        //    return NoContent();
        //}
    }
}