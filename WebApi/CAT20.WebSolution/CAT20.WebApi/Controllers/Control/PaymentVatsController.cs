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
    public class PaymentVatsController : BaseController
    {
        private readonly IPaymentVatService _paymentVatService;
        private readonly IMapper _mapper;

        public PaymentVatsController(IPaymentVatService paymentVatService, IMapper mapper)
        {
            _mapper = mapper;
            _paymentVatService = paymentVatService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<PaymentVat>>> getAll()
        {
            var paymentVats = await _paymentVatService.GetAll();
            var paymentVatResources = _mapper.Map<IEnumerable<PaymentVat>, IEnumerable<PaymentVatResource>>(paymentVats);

            return Ok(paymentVatResources);
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<ActionResult<PaymentVatResource>> GetById([FromRoute] int id)
        {
            var paymentVat = await _paymentVatService.GetById(id);
            var paymentVatResource = _mapper.Map<PaymentVat, PaymentVatResource>(paymentVat);
            return Ok(paymentVatResource);
        }
    }
}