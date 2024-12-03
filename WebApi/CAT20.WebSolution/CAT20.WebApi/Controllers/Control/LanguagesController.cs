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
    public class LanguagesController : BaseController
    {
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public LanguagesController(ILanguageService languageService, IMapper mapper)
        {
            _mapper = mapper;
            _languageService = languageService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Language>>> GetAllProducts()
        {
            var languages = await _languageService.GetAllLanguages();
            var languageResources = _mapper.Map<IEnumerable<Language>, IEnumerable<LanguageResource>>(languages);

            return Ok(languageResources);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LanguageResource>> GetLanguageById(int id)
        {
            var language = await _languageService.GetLanguageById(id);
            var languageResource = _mapper.Map<Language, LanguageResource>(language);
            return Ok(languageResource);
        }

        //[HttpPost("")]
        //public async Task<ActionResult<LanguageResource>> CreateLanguage([FromBody] SaveLanguageResource saveLanguageResource)
        //{
        //    var validator = new SaveLanguageResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveLanguageResource);

        //    if (!validationResult.IsValid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var languageToCreate = _mapper.Map<SaveLanguageResource, Language>(saveLanguageResource);

        //    var newLanguage = await _languageService.CreateLanguage(languageToCreate);

        //    var language = await _languageService.GetLanguageById(newLanguage.ID);

        //    var languageResource = _mapper.Map<Language, LanguageResource>(language);

        //    return Ok(languageResource);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<LanguageResource>> UpdateProduct(int id, [FromBody] SaveLanguageResource saveLanguageResource)
        //{
        //    var validator = new SaveLanguageResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveLanguageResource);

        //    var requestIsInvalid = id == 0 || !validationResult.IsValid;

        //    if (requestIsInvalid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var languageToBeUpdate = await _languageService.GetLanguageById(id);

        //    if (languageToBeUpdate == null)
        //        return NotFound();

        //    var product = _mapper.Map<SaveLanguageResource, Language>(saveLanguageResource);

        //    await _languageService.UpdateLanguage(languageToBeUpdate, product);

        //    var updatedLanguage = await _languageService.GetLanguageById(id);
        //    var updatedLanguageResource = _mapper.Map<Language, LanguageResource>(updatedLanguage);

        //    return Ok(updatedLanguageResource);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var language = await _languageService.GetLanguageById(id);

        //    if (language == null)
        //        return NotFound();

        //    await _languageService.DeleteLanguage(language);

        //    return NoContent();
        //}
    }
}