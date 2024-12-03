using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.Models.Vote;
using CAT20.Core.Models.TradeTax;
using CAT20.WebApi.Resources.Vote.Save;
using CAT20.WebApi.Resources.Vote;
using CAT20.Core.Services.Vote;
using CAT20.WebApi.Resources.TradeTax;
using CAT20.Core.Services.TradeTax;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.Mixin;

namespace CAT20.WebApi.Controllers.TradeTax
{
    [Route("api/tradetax/taxValues")]
    [ApiController]
    public class TaxValuesController : BaseController
    {
        private readonly ITaxValueService _taxValueService;
        private readonly IAccountDetailService _accountDetailService;
        private readonly IMapper _mapper;

        public TaxValuesController(ITaxValueService taxValueService, IMapper mapper, IAccountDetailService accountDetailService)
        {
            _mapper = mapper;
            _taxValueService = taxValueService;
            _accountDetailService = accountDetailService;
        }

        [HttpGet("getAllTaxValues")]
        public async Task<ActionResult<IEnumerable<TaxValue>>> GetAllTaxValues()
        {
            var taxValues = await _taxValueService.GetAllTaxValues();

            var taxValueResources = _mapper.Map<IEnumerable<TaxValue>, IEnumerable<TaxValueResource>>(taxValues);

            return Ok(taxValueResources);
        }

        [HttpGet]
        [Route("getTaxValueById/{id}")]
        public async Task<ActionResult<TaxValueResource>> GetTaxValueById([FromRoute] int id)
        {
            var taxValue = await _taxValueService.GetTaxValueById(id);
            var taxValueResource = _mapper.Map<TaxValue, TaxValueResource>(taxValue);
            return Ok(taxValueResource);
        }

        [HttpPost("saveTaxValue")]
        public async Task<ActionResult<TaxValueResource>> CreateTaxValue([FromBody] TaxValueResource saveTaxValueResource)
        {

            var taxValueToCreate = _mapper.Map<TaxValueResource, TaxValue>(saveTaxValueResource);

            var newTaxValue = await _taxValueService.CreateTaxValue(taxValueToCreate);

            var taxValue = await _taxValueService.GetTaxValueById(newTaxValue.ID);

            var taxValueResource = _mapper.Map<TaxValue, TaxValueResource>(taxValue);

            return Ok(taxValueResource);
        }

        [HttpPost("updateTaxValue")]
        public async Task<ActionResult<TaxValueResource>> UpdateTaxValue(TaxValueResource saveTaxValueResource)
        {
            var taxValueToBeUpdate = await _taxValueService.GetTaxValueById(saveTaxValueResource.ID);

            if (taxValueToBeUpdate == null)
                return NotFound();

            var product = _mapper.Map<TaxValueResource, TaxValue>(saveTaxValueResource);

            await _taxValueService.UpdateTaxValue(taxValueToBeUpdate, product);

            var updatedTaxValue = await _taxValueService.GetTaxValueById(saveTaxValueResource.ID);
            var updatedTaxValueResource = _mapper.Map<TaxValue, TaxValueResource>(updatedTaxValue);

            return Ok(updatedTaxValueResource);
        }

        [HttpPost]
        [Route("deleteTaxValue/{id}")]
        public async Task<IActionResult> DeleteTaxValue([FromRoute] int id)
        {
            if (id == 0)
                return BadRequest();

            var taxValue = await _taxValueService.GetTaxValueById(id);

            if (taxValue == null)
                return NotFound();

            await _taxValueService.DeleteTaxValue(taxValue);

            return NoContent();
        }

        [HttpGet]
        [Route("getAllTaxValuesForSabhaId/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<TaxValue>>> GetAllTaxValuesForSabhaId([FromRoute] int SabhaId)
        {
            var taxValues = await _taxValueService.GetAllTaxValuesForSabhaId(SabhaId);
            var taxValueResources = _mapper.Map<IEnumerable<TaxValue>, IEnumerable<TaxValueResource>>(taxValues);

            return Ok(taxValueResources);
        }


        [HttpGet]
        [Route("getAllTaxValuesForTaxTypeID/{TaxTypeID}")]
        public async Task<ActionResult<IEnumerable<TaxValue>>> GetAllTaxValuesForTaxTypeID([FromRoute] int TaxTypeID)
        {
            var taxValues = await _taxValueService.GetAllTaxValuesForTaxTypeID(TaxTypeID);

            var taxValueResources = _mapper.Map<IEnumerable<TaxValue>, IEnumerable<TaxValueResource>>(taxValues);

            return Ok(taxValueResources);
        }

        [HttpGet]
        [Route("getAllTaxValuesForTaxTypeIDAndSabhaID/{taxTypeID}/{sabhaID}")]
        public async Task<ActionResult<IEnumerable<TaxValue>>> GetAllTaxValuesForTaxTypeIDAndSabhaID([FromRoute] int taxTypeID, int sabhaID)
        {
            var taxValues = await _taxValueService.GetAllTaxValuesForTaxTypeIDAndSabhaID(taxTypeID, sabhaID);

            var taxValueResources = _mapper.Map<IEnumerable<TaxValue>, IEnumerable<TaxValueResource>>(taxValues);

            return Ok(taxValueResources);
        }

        [HttpGet]
        [Route("getAllTaxValuesForSabhaID/{taxTypeID}/{sabhaID}")]
        public async Task<ActionResult<IEnumerable<TaxValue>>> GetAllTaxValuesForSabhaID([FromRoute] int sabhaID)
        {
            var taxValues = await _taxValueService.GetAllTaxValuesForSabhaID(sabhaID);

            var taxValueResources = _mapper.Map<IEnumerable<TaxValue>, IEnumerable<TaxValueResource>>(taxValues);

            return Ok(taxValueResources);
        }
    }
}
