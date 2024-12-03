using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;
using CAT20.WebApi.Controllers;
using CAT20.WebApi.Resources.Control;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.WebApi.Controllers.Control
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentNbtsController : BaseController
    {
        private readonly IPaymentNbtService _paymentNbtService;
        private readonly IMapper _mapper;

        public PaymentNbtsController(IPaymentNbtService paymentNbtService, IMapper mapper)
        {
            _mapper = mapper;
            _paymentNbtService = paymentNbtService;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<PaymentNbt>>> GetAll()
        {
            var paymentNbts = await _paymentNbtService.GetAll();
            var paymentNbtResources = _mapper.Map<IEnumerable<PaymentNbt>, IEnumerable<PaymentNbtResource>>(paymentNbts);

            return Ok(paymentNbtResources);
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<ActionResult<PaymentNbtResource>> GetById([FromRoute] int id)
        {
            var paymentNbt = await _paymentNbtService.GetById(id);
            var paymentNbtResource = _mapper.Map<PaymentNbt, PaymentNbtResource>(paymentNbt);
            return Ok(paymentNbtResource);
        }
    }
}