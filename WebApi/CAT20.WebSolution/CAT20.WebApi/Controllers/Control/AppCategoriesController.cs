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
    public class AppCategoriesController : BaseController
    {
        private readonly IAppCategoryService _appCategoryService;
        private readonly IMapper _mapper;

        public AppCategoriesController(IAppCategoryService appCategoryService, IMapper mapper)
        {
            _mapper = mapper;
            _appCategoryService = appCategoryService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<AppCategory>>> GetAllProducts()
        {
            var appCategories = await _appCategoryService.GetAllAppCategorys();
            var appCategoryResources = _mapper.Map<IEnumerable<AppCategory>, IEnumerable<AppCategoryResource>>(appCategories);

            return Ok(appCategoryResources);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AppCategoryResource>> GetAppCategoryById(int id)
        {
            var appCategory = await _appCategoryService.GetAppCategoryById(id);
            var appCategoryResource = _mapper.Map<AppCategory, AppCategoryResource>(appCategory);
            return Ok(appCategoryResource);
        }

        //[HttpPost("")]
        //public async Task<ActionResult<AppCategoryResource>> CreateAppCategory([FromBody] SaveAppCategoryResource saveAppCategoryResource)
        //{
        //    var validator = new SaveAppCategoryResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveAppCategoryResource);

        //    if (!validationResult.IsValid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var appCategoryToCreate = _mapper.Map<SaveAppCategoryResource, AppCategory>(saveAppCategoryResource);

        //    var newAppCategory = await _appCategoryService.CreateAppCategory(appCategoryToCreate);

        //    var appCategory = await _appCategoryService.GetAppCategoryById(newAppCategory.ID);

        //    var appCategoryResource = _mapper.Map<AppCategory, AppCategoryResource>(appCategory);

        //    return Ok(appCategoryResource);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<AppCategoryResource>> UpdateProduct(int id, [FromBody] SaveAppCategoryResource saveAppCategoryResource)
        //{
        //    var validator = new SaveAppCategoryResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveAppCategoryResource);

        //    var requestIsInvalid = id == 0 || !validationResult.IsValid;

        //    if (requestIsInvalid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var appCategoryToBeUpdate = await _appCategoryService.GetAppCategoryById(id);

        //    if (appCategoryToBeUpdate == null)
        //        return NotFound();

        //    var product = _mapper.Map<SaveAppCategoryResource, AppCategory>(saveAppCategoryResource);

        //    await _appCategoryService.UpdateAppCategory(appCategoryToBeUpdate, product);

        //    var updatedAppCategory = await _appCategoryService.GetAppCategoryById(id);
        //    var updatedAppCategoryResource = _mapper.Map<AppCategory, AppCategoryResource>(updatedAppCategory);

        //    return Ok(updatedAppCategoryResource);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var appCategory = await _appCategoryService.GetAppCategoryById(id);

        //    if (appCategory == null)
        //        return NotFound();

        //    await _appCategoryService.DeleteAppCategory(appCategory);

        //    return NoContent();
        //}
    }
}