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
    public class GnDivisionsController : BaseController

    {
        private readonly IGnDivisionService _gnDivisionService;
        private readonly IMapper _mapper;
        private readonly IOfficeService _officeService;

        public GnDivisionsController(IGnDivisionService gnDivisionService, IMapper mapper, IOfficeService officeService)
        {
            _mapper = mapper;
            _gnDivisionService = gnDivisionService;
            _officeService = officeService;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<GnDivisions>>> GetAll()
        {
            var gnDivisions = await _gnDivisionService.GetAll();
            var gnDivisionResources = _mapper.Map<IEnumerable<GnDivisions>, IEnumerable<GnDivisionsResource>>(gnDivisions);

            return Ok(gnDivisionResources);
        }

        [HttpGet]
        [Route("getAllForOffice/{officeid}")]
        public async Task<ActionResult<IEnumerable<GnDivisions>>> getAllForOffice(int officeid)
        {
            var gnDivisions = await _gnDivisionService.GetAllForOffice(officeid);
            var gnDivisionResources = _mapper.Map<IEnumerable<GnDivisions>, IEnumerable<GnDivisionsResource>>(gnDivisions);

            gnDivisionResources.ToList().Add(new GnDivisionsResource
            {
                Id = -1,
                Description = "Default",
                Code = "000",
                OfficeId = officeid,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Active = true
            });

            return Ok(gnDivisionResources);
        }

        [HttpGet]
        [Route("getAllForSabha/{sabhaid}")]
        public async Task<ActionResult<IEnumerable<GnDivisions>>> getAllForSabha(int sabhaid)
        {
            List<GnDivisions> gnDivisionsforsabha = new List<GnDivisions>();
            var offices = await _officeService.getAllOfficesForSabhaId(sabhaid);
            foreach (var office in offices)
            {
                var gnDivisions = await _gnDivisionService.GetAllForOffice(office.ID.Value);
                gnDivisionsforsabha.AddRange(gnDivisions);
            }

            gnDivisionsforsabha.Add(new GnDivisions
            {
                Id = -1,
                Description="Default",
                Code="000",
                OfficeId= offices.ToList()[0].ID.Value  ,
                CreatedAt=DateTime.Now,
                UpdatedAt=DateTime.Now,
                Active=true
             });
            
            var gnDivisionResources = _mapper.Map<IEnumerable<GnDivisions>, IEnumerable<GnDivisionsResource>>(gnDivisionsforsabha);

            return Ok(gnDivisionResources);
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<ActionResult<GnDivisionsResource>> GetById([FromRoute] int id)
        {
            var gnDivision = await _gnDivisionService.GetById(id);
            var gnDivisionResource = _mapper.Map<GnDivisions, GnDivisions>(gnDivision);
            return Ok(gnDivisionResource);
        }

        [HttpGet("searchforoffice")]
        public async Task<ActionResult<IEnumerable<GnDivisions>>> Search(int officeid, string description, string? code)
        {
            try
            {
                var gnDivisions = await _gnDivisionService.Search(officeid, description, code);
                if (gnDivisions.Any())
                {
                    var gnDivisionResources = _mapper.Map<IEnumerable<GnDivisions>, IEnumerable<GnDivisionsResource>>(gnDivisions);
                    
                    gnDivisionResources.ToList().Add(new GnDivisionsResource
                    {
                        Id = -1,
                        Description = "Default",
                        Code = "000",
                        OfficeId = officeid,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        Active = true
                    });
                    return Ok(gnDivisionResources);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }


        [HttpGet("searchforsabha")]
        public async Task<ActionResult<IEnumerable<GnDivisions>>> SearchForSabha(int sabhaid, string description, string? code)
        {
            List<GnDivisions> gnDivisionsforsabha = new List<GnDivisions>();
            var offices = await _officeService.getAllOfficesForSabhaId(sabhaid);
            try
            {
                foreach (var office in offices)
                {
                    var gnDivisions = await _gnDivisionService.Search(office.ID.Value, description, code);
                    if (gnDivisions.Any())
                    {
                        gnDivisionsforsabha.AddRange(gnDivisions);
                    }
                    else
                    {
                        //return NotFound();
                    }
                }
                var gnDivisionResources = _mapper.Map<IEnumerable<GnDivisions>, IEnumerable<GnDivisionsResource>>(gnDivisionsforsabha);
                gnDivisionResources.ToList().Add(new GnDivisionsResource
                {
                    Id = -1,
                    Description = "Default",
                    Code = "000",
                    OfficeId = offices.ToList()[0].ID.Value,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Active = true
                });
                return Ok(gnDivisionResources);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }



        [HttpPost("saveGnDivisionsDetails")]

        public async Task<IActionResult> Post([FromBody] GnDivisions gnDivisionsData)
        {
            var gnDivisions = await _gnDivisionService.CreateGnDivision(gnDivisionsData);
            var gnDivisionsResource = _mapper.Map<GnDivisions, GnDivisionsResource>(gnDivisions);
            return Ok(gnDivisionsResource);
        }


        //[HttpPost("saveGnDivisionDetails")]

            //public async Task<IActionResult> Post([FromBody]  GnDivisions gnDivisonsData)
            //{
            //    var gnDivisionsData = await GnDivisionService.
            //    var sabhaResource = _mapper.Map<Sabha, SabhaResource>(sabha);
            //    return Ok(sabhaResource);
            //}

        }
}