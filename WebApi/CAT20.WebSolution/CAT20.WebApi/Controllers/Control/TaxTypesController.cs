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
    [Route("api/taxtype")]
    [ApiController]
    public class TaxTypesController : BaseController
    {
        private readonly ITaxTypeService _taxtypeService;
        private readonly IMapper _mapper;

        public TaxTypesController(ITaxTypeService taxtypeService, IMapper mapper)
        {
            _mapper = mapper;
            _taxtypeService = taxtypeService;
        }

        [HttpGet("GetAllTaxTypes")]
        public async Task<ActionResult<IEnumerable<TaxType>>> GetAllTaxTypes()
        {
            var taxtypes = await _taxtypeService.GetAllTaxTypes();
            var taxtypeResources = _mapper.Map<IEnumerable<TaxType>, IEnumerable<TaxTypeResource>>(taxtypes);

            return Ok(taxtypeResources);
        }

        [HttpGet("GetAllBasicTaxTypes")]
        public async Task<ActionResult<IEnumerable<TaxType>>> GetAllBasicTaxTypes()
        {
            var taxtypes = await _taxtypeService.GetAllBasicTaxTypes();
            var taxtypeResources = _mapper.Map<IEnumerable<TaxType>, IEnumerable<TaxTypeResource>>(taxtypes);

            return Ok(taxtypeResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaxTypeResource>> GetTaxTypeById(int id)
        {
            var taxtype = await _taxtypeService.GetTaxTypeById(id);
            var taxtypeResource = _mapper.Map<TaxType, TaxTypeResource>(taxtype);
            return Ok(taxtypeResource);
        }

        //[HttpPost("")]
        //public async Task<ActionResult<TaxTypeResource>> CreateTaxType([FromBody] SaveTaxTypeResource saveTaxTypeResource)
        //{
        //    var validator = new SaveTaxTypeResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveTaxTypeResource);

        //    if (!validationResult.IsValid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var taxtypeToCreate = _mapper.Map<SaveTaxTypeResource, TaxType>(saveTaxTypeResource);

        //    var newTaxType = await _taxtypeService.CreateTaxType(taxtypeToCreate);

        //    var taxtype = await _taxtypeService.GetTaxTypeById(newTaxType.ID);

        //    var taxtypeResource = _mapper.Map<TaxType, TaxTypeResource>(taxtype);

        //    return Ok(taxtypeResource);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<TaxTypeResource>> UpdateProduct(int id, [FromBody] SaveTaxTypeResource saveTaxTypeResource)
        //{
        //    var validator = new SaveTaxTypeResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveTaxTypeResource);

        //    var requestIsInvalid = id == 0 || !validationResult.IsValid;

        //    if (requestIsInvalid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var taxtypeToBeUpdate = await _taxtypeService.GetTaxTypeById(id);

        //    if (taxtypeToBeUpdate == null)
        //        return NotFound();

        //    var product = _mapper.Map<SaveTaxTypeResource, TaxType>(saveTaxTypeResource);

        //    await _taxtypeService.UpdateTaxType(taxtypeToBeUpdate, product);

        //    var updatedTaxType = await _taxtypeService.GetTaxTypeById(id);
        //    var updatedTaxTypeResource = _mapper.Map<TaxType, TaxTypeResource>(updatedTaxType);

        //    return Ok(updatedTaxTypeResource);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var taxtype = await _taxtypeService.GetTaxTypeById(id);

        //    if (taxtype == null)
        //        return NotFound();

        //    await _taxtypeService.DeleteTaxType(taxtype);

        //    return NoContent();
        //}
    }
}