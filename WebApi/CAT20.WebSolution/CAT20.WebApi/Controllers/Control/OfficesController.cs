using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;
using CAT20.Services.Control;
using CAT20.WebApi.Controllers;
using CAT20.WebApi.Resources.Control;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.WebApi.Controllers.Control
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficesController : BaseController
    {
        private readonly IOfficeService _officeService;
        private readonly IMapper _mapper;
        private object _officeservice;

        public OfficesController(IOfficeService officeService, IMapper mapper)
        {
            _mapper = mapper;
            _officeService = officeService;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<Office>>> GetAll()
        {
            var offices = await _officeService.GetAllOffices();
            var officeResources = _mapper.Map<IEnumerable<Office>, IEnumerable<OfficeResource>>(offices);

            return Ok(officeResources);
        }

        [HttpGet]
        [Route("getAllOfficesForSabhaId/{id}")]
        public async Task<ActionResult<IEnumerable<Office>>> getAllOfficesForSabhaId(int id)
        {
            try
            {
                // Access the user ID from claims
                var userIdClaim = HttpContext.User.FindFirst("userid");
                if (userIdClaim != null)
                {
                    var userId = userIdClaim.Value;
                }
                var offices = await _officeService.getAllOfficesForSabhaId(id);
                var officeResources = _mapper.Map<IEnumerable<Office>, IEnumerable<OfficeResource>>(offices);

                return Ok(officeResources);
            }
            catch (Exception ex)
            {
                return BadRequest("Error retrieving offices");
            }
        }

        [HttpGet]
        [Route("getAllOfficesForSabhaIdAndOfficeType/{id}/{type}")]
        public async Task<ActionResult<IEnumerable<Office>>> getAllOfficesForSabhaIdAndOfficeType(int id, int type)
        {
            var offices = await _officeService.getAllOfficesForSabhaIdAndOfficeType(id, type);
            var officeResources = _mapper.Map<IEnumerable<Office>, IEnumerable<OfficeResource>>(offices);

            return Ok(officeResources);
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<ActionResult<OfficeResource>> GetById([FromRoute] int id)
        {
            var office = await _officeService.GetOfficeById(id);
            var officeResource = _mapper.Map<Office, OfficeResource>(office);
            return Ok(officeResource);
        }


        [HttpPost("saveofficesdetails")]
        public async Task<IActionResult> Post([frombody] Office officesdata)
        {
            var office = await _officeService.CreateOffice(officesdata);
            var officesresource = _mapper.Map<Office, OfficeResource>(office);
            return Ok(officesresource);
        }



        //[httppost("")]
        //public async task<actionresult<officeresource>> createoffice([frombody] saveofficeresource saveofficeresource)
        //{
        //    var validator = new saveofficeresourcevalidator();
        //    var validationresult = await validator.validateasync(saveofficeresource);

        //    if (!validationresult.isvalid)
        //        return badrequest(validationresult.errors); // this needs refining, but for demo it is ok

        //    var officetocreate = _mapper.map<saveofficeresource, office>(saveofficeresource);

        //    var newoffice = await _officeservice.createoffice(officetocreate);

        //    var office = await _officeservice.getofficebyid(newoffice.id);

        //    var officeresource = _mapper.map<office, officeresource>(office);

        //    return ok(officeresource);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<OfficeResource>> UpdateProduct(int id, [FromBody] SaveOfficeResource saveOfficeResource)
        //{
        //    var validator = new SaveOfficeResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveOfficeResource);

        //    var requestIsInvalid = id == 0 || !validationResult.IsValid;

        //    if (requestIsInvalid)
        //        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

        //    var officeToBeUpdate = await _officeService.GetOfficeById(id);

        //    if (officeToBeUpdate == null)
        //        return NotFound();

        //    var product = _mapper.Map<SaveOfficeResource, Office>(saveOfficeResource);

        //    await _officeService.UpdateOffice(officeToBeUpdate, product);

        //    var updatedOffice = await _officeService.GetOfficeById(id);
        //    var updatedOfficeResource = _mapper.Map<Office, OfficeResource>(updatedOffice);

        //    return Ok(updatedOfficeResource);
        //}

        //    [HttpDelete("{id}")]
        //    public async task<IActionResult> deleteOffice(int id)
        //    {
        //        if (id == 0)
        //            return BadRequest();

        //        var office = await _officeservice.getofficebyid(id);

        //        if (office == null)
        //            return notfound();

        //        await _officeservice.deleteoffice(office);

        //        return nocontent();
        //    }
        //}

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteOffice(int id)
            {
              //  if (id == 0)
              //     return BadRequest();

              //var office = await _officeservice.getOfficebyid(id);

              //  if (office == null)
              //      return NotFound();

              //await _officeservice.DeleteOffice(office);

               return NoContent();
            }
        }


        public class task<T>
        {
        }

        public class office
        {
        }

        internal class frombodyAttribute : Attribute
        {
        }
}