using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;
using CAT20.WebApi.Resources.Control;
using Microsoft.AspNetCore.Mvc;
using CAT20.WebApi.Resources.Vote.Save;
using CAT20.WebApi.Resources.Vote;
using Microsoft.AspNetCore.Authorization;

namespace CAT20.WebApi.Controllers.Control
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankDetailsController : BaseController
    {
        private readonly IBankDetailService _bankDetailService;
        private readonly IMapper _mapper;

        public BankDetailsController(IBankDetailService bankDetailService, IMapper mapper)
        {
            _mapper = mapper;
            _bankDetailService = bankDetailService;
        }

        [HttpGet]
        [Route("GetAllBankDetails")]
        public async Task<ActionResult<IEnumerable<BankDetail>>> GetAllBankDetails()
        {
            var bankDetails = await _bankDetailService.GetAllBankDetails();
            var bankDetailResources = _mapper.Map<IEnumerable<BankDetail>, IEnumerable<BankDetailResource>>(bankDetails);

            return Ok(bankDetailResources);
        }
        [HttpGet]
        [Route("GetBankDetailById/{id}")]
        public async Task<ActionResult<BankDetailResource>> GetBankDetailById([FromRoute] int id)
        {
            var bankDetail = await _bankDetailService.GetBankDetailById(id);
            var bankDetailResource = _mapper.Map<BankDetail, BankDetailResource>(bankDetail);
            return Ok(bankDetailResource);
        }

        //[HttpPost("")]
        //public async Task<ActionResult<BankDetailResource>> CreateBankDetail([FromBody] SaveBankDetailResource saveBankDetailResource)
        //{
        //    var validator = new SaveBankDetailResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveBankDetailResource);

        //    if (!validationResult.IsValid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var bankDetailToCreate = _mapper.Map<SaveBankDetailResource, BankDetail>(saveBankDetailResource);

        //    var newBankDetail = await _bankDetailService.CreateBankDetail(bankDetailToCreate);

        //    var bankDetail = await _bankDetailService.GetBankDetailById(newBankDetail.ID);

        //    var bankDetailResource = _mapper.Map<BankDetail, BankDetailResource>(bankDetail);

        //    return Ok(bankDetailResource);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<BankDetailResource>> UpdateProduct(int id, [FromBody] SaveBankDetailResource saveBankDetailResource)
        //{
        //    var validator = new SaveBankDetailResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveBankDetailResource);

        //    var requestIsInvalid = id == 0 || !validationResult.IsValid;

        //    if (requestIsInvalid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var bankDetailToBeUpdate = await _bankDetailService.GetBankDetailById(id);

        //    if (bankDetailToBeUpdate == null)
        //        return NotFound();

        //    var product = _mapper.Map<SaveBankDetailResource, BankDetail>(saveBankDetailResource);

        //    await _bankDetailService.UpdateBankDetail(bankDetailToBeUpdate, product);

        //    var updatedBankDetail = await _bankDetailService.GetBankDetailById(id);
        //    var updatedBankDetailResource = _mapper.Map<BankDetail, BankDetailResource>(updatedBankDetail);

        //    return Ok(updatedBankDetailResource);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var bankDetail = await _bankDetailService.GetBankDetailById(id);

        //    if (bankDetail == null)
        //        return NotFound();

        //    await _bankDetailService.DeleteBankDetail(bankDetail);

        //    return NoContent();
        //}
    }
}