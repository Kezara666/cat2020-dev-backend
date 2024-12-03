using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;
using CAT20.WebApi.Resources.Control;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.WebApi.Controllers.Control
{
    [Route("api/[controller]")]
    [ApiController]
    public class SabhasController : ControllerBase
    {
        private readonly ISabhaService _sabhaService;
        private readonly IMapper _mapper;

        public SabhasController(ISabhaService sabhaService, IMapper mapper)
        {
            _mapper = mapper;
            _sabhaService = sabhaService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Sabha>>> GetAllProducts()
        {
            var sabhas = await _sabhaService.GetAllSabhas();
            var sabhaResources = _mapper.Map<IEnumerable<Sabha>, IEnumerable<SabhaResource>>(sabhas);

            return Ok(sabhaResources);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SabhaResource>> GetSabhaById(int id)
        {
            var sabha = await _sabhaService.GetSabhaById(id);
            var sabhaResource = _mapper.Map<Sabha, SabhaResource>(sabha);
            return Ok(sabhaResource);
        }

        [HttpPost("saveSabhaDetails")]
        public async Task<IActionResult> Post([FromBody] Sabha sabhadata)
        {
            var sabha = await _sabhaService.CreateSabha(sabhadata);
            var sabhaResource = _mapper.Map<Sabha, SabhaResource>(sabha);
            return Ok(sabhaResource);
        }

        [HttpGet("getAllSabha")]
        public async Task<ActionResult<IEnumerable<SabhaResource>>> GetAllSabha()
        {
            var sabhas = await _sabhaService.GetAllSabhas();
            var sabhasResource = _mapper.Map<IEnumerable<Sabha>, IEnumerable<SabhaResource>>(sabhas);
            return Ok(sabhasResource);
        }
    }
}