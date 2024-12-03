using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using AutoMapper;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Models.OnlinePayment;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.AssessmentTax;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.OnlinePayment;
using CAT20.WebApi.Email;
using CAT20.WebApi.Resources.AssessmentTax;
using CAT20.WebApi.Resources.Control;
using CAT20.WebApi.Resources.Mixin;
using CAT20.WebApi.Resources.Mixin.Save;
using CAT20.WebApi.Resources.OnlinePayment;
using CAT20.WebApi.Resources.Pagination;
using CAT20.WebApi.SMS;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenTelemetry.Trace;
using System.IO;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Security.Cryptography;
using System.Web;
using DocumentFormat.OpenXml.Bibliography;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Services.WaterBilling;
using CAT20.Services.WaterBilling;
using CAT20.WebApi.Controllers.Control;
using CAT20.Core.HelperModels;

namespace CAT20.WebApi.Controllers.OnlinePayment
{
    [Route("api/[controller]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OnlinePaymentsController : ControllerBase
    {
        private readonly IOnlinePaymentService _onlinePaymentService;
        private readonly IAssessmentBalanceService _assessmentBalanceService;
        private readonly IMapper _mapper;
        private readonly IAssessmentService _assessmentService;
        private readonly IAssessmentRatesService _assessmentRatesService;
        private readonly ISessionService _sessionService;
        private readonly ISMSConfigurationService _smsConfigurationService;
        private readonly ISMSOutBoxService _smsOutBoxService;
        private readonly IEmailConfigurationService _emailConfigurationService;
        private readonly IEmailOutBoxService _emailOutBoxService;
        private readonly IConfiguration _configuration;
        private readonly ISabhaService _sabhaService;
        private readonly IPartnerService _partnerService;
        private readonly IWaterConnectionService _waterconnectionService;
        private readonly IWaterConnectionBalanceService _waterconnectionBalanceService;



        public OnlinePaymentsController(IConfiguration configuration, IOnlinePaymentService onlinePaymentService,
            IMapper mapper, IAssessmentBalanceService assessmentBalanceService, IAssessmentService assessmentService,
            IAssessmentRatesService assessmentRatesService, ISessionService sessionService,
            ISMSConfigurationService smsConfigurationService, ISMSOutBoxService smsOutBoxService,
            IEmailConfigurationService emailConfigurationService, IEmailOutBoxService emailOutBoxService, ISabhaService sabhaService, IPartnerService partnerService, IWaterConnectionService waterconnectionService, IWaterConnectionBalanceService waterconnectionBalanceService)
        {
            _onlinePaymentService = onlinePaymentService;
            _assessmentBalanceService = assessmentBalanceService;
            _assessmentService = assessmentService;
            _assessmentRatesService = assessmentRatesService;
            _sessionService = sessionService;
            _smsConfigurationService = smsConfigurationService;
            _smsOutBoxService = smsOutBoxService;
            _emailConfigurationService = emailConfigurationService;
            _emailOutBoxService = emailOutBoxService;
            _mapper = mapper;
            _configuration = configuration;
            _sabhaService = sabhaService;
            _partnerService = partnerService;
            _waterconnectionService = waterconnectionService;
            _waterconnectionBalanceService = waterconnectionBalanceService;
        }
        // [HttpGet("getAllService/{partnerId}")]
        // public async Task<IActionResult> getAllSabhaProvinceDistrict([FromRoute] int partnerId)
        // {
        //     var sabhaProviceDistricForPartner = await _onlinePaymentService.getSabhaProviceDistricForPartner(partnerId);
        //
        //     // Map the flattened list to SabhaResource
        //     var assessmentResources = _mapper.Map<IEnumerable<Sabha>, IEnumerable<SabhaResource>>(sabhaProviceDistricForPartner);
        //
        //     return Ok(assessmentResources);
        //
        // }

        [HttpGet("getAllAllowedLocalAuthoritiesForPartner/{partnerId}")]
        public async Task<IActionResult> GetAllAllowedLocalAuthoritiesForPartner([FromRoute] int partnerId)
        {
            var sabhaProviceDistricForPartner = await _onlinePaymentService.getSabhaProviceDistricForPartner(partnerId);

            // Flatten the list of lists into a single list
            var flatList = sabhaProviceDistricForPartner.SelectMany(x => x);

            // Map the flattened list to SabhaResource
            var assessmentResources = _mapper.Map<IEnumerable<Sabha>, IEnumerable<SabhaResource>>(flatList);

            return Ok(assessmentResources);
        }

        [HttpGet("getAllAssessments/{partnerId}/{sabhaId}")]
        public async Task<IActionResult> getAllAssessments([FromRoute] int partnerId, [FromRoute] int sabhaId)
        {
            var partner = await _partnerService.GetByIdWithDetails(partnerId);

            List<AssessmentResource> assessmentResourcesList = new List<AssessmentResource>();
            var sabhaProvinceDistrictForPartner = await _onlinePaymentService.GetAllForCustomerIdAndSabhaId(partnerId, sabhaId);

            if (partner != null && partner.PermittedThirdPartyAssessments.Any())
            {
                var thirdPartyAssmtIds = partner.PermittedThirdPartyAssessments.Select(a => a.AssessmentId).ToList();
                //var thirdPartyAssessmentsList = await _onlinePaymentService.GetAllForIds(thirdPartyAssmtIds);
                var thirdPartyAssessmentsList = await _onlinePaymentService.GetAllForIdsAndSabha(thirdPartyAssmtIds,sabhaId);

                foreach (var thirdpartyassessment in thirdPartyAssessmentsList)
                {
                    if (thirdpartyassessment.IsPartnerUpdated && thirdpartyassessment.PartnerId != null && thirdpartyassessment.PartnerId != 0)
                    {
                        thirdpartyassessment.Partner = await _partnerService.GetById(thirdpartyassessment.PartnerId.Value);
                    }

                    if (thirdpartyassessment.IsSubPartnerUpdated && thirdpartyassessment.SubPartnerId != null && thirdpartyassessment.SubPartnerId != 0)
                    {
                        thirdpartyassessment.SubPartner = await _partnerService.GetById(thirdpartyassessment.SubPartnerId.Value);
                    }
                }
                sabhaProvinceDistrictForPartner = sabhaProvinceDistrictForPartner.Concat(thirdPartyAssessmentsList);
            }

            var rates = await _assessmentRatesService.GetById(1);
            foreach (var assessment in sabhaProvinceDistrictForPartner)
            {
                var assessmentResources = _mapper.Map<Assessment, AssessmentResource>(assessment);

                var session = await _sessionService.GetActiveOrLastActiveSessionForOnlinePaymentCurrentMonth(assessment.OfficeId, "MIX");
                int sessionMonth = session?.CreatedAt.Month ?? -1;
                (var outstandingPayable, var payable, var deductionBalace, var payingBalance, var nextBalance, var discount,
                        var discountRate) =
                    _assessmentBalanceService.CalculatePaymentBalance(assessment.AssessmentBalance, rates, 0, sessionMonth,
                        false);
                assessmentResources.PayableBalance = payable;
                assessmentResourcesList.Add(assessmentResources);
            }

            // var assessmentResources = _mapper.Map<IEnumerable<Assessment>, IEnumerable<AssessmentResource>>(sabhaProviceDistricForPartner);

            return Ok(assessmentResourcesList);
        }

        //[HttpGet("getWaterBill/{partnerId}/{sabhaId}")]
        //public async Task<IEnumerable<WaterConnection>> getWaterBill([FromRoute] int partnerId, [FromRoute] int sabhaId)
        //{
        //    var waterConnections = await _onlinePaymentService.getWaterBill(partnerId, sabhaId);
        //    return waterConnections;
        //}

        [HttpGet("getWaterBill/{partnerId}/{sabhaId}")]
        public async Task<ActionResult<List<WaterConnectionResource>>> getWaterBill([FromRoute] int partnerId, [FromRoute] int sabhaId)
        {
            var partner = await _partnerService.GetByIdWithDetails(partnerId);
            var partnerResource = _mapper.Map<Partner, PartnerResource>(partner);

            var waterConnections = await _waterconnectionService.GetWaterConnectionsForSabhaAndPartnerId(sabhaId, partnerResource.Id);
            var waterConnectionResoureces = _mapper.Map<IEnumerable<WaterConnection>, IEnumerable<WaterConnectionResource>>(waterConnections);

            List<WaterConnectionResource> waterreslist = new List<WaterConnectionResource>();

            foreach (var waterConnectionResource in waterConnectionResoureces)
            {
                //var billingAccountResource = _mapper.Map<Partner, PartnerResource>(await _partnerService.GetById(waterConnectionResourece.BillingId));

                //waterConnectionResourece.PartnerAccount = partnerResource;
                //waterConnectionResourece.BillingAccount = billingAccountResource;
                if (waterConnectionResource.SubRoad != null  && waterConnectionResource.SubRoad.WaterProject != null)
                {
                    var wcb = await _waterconnectionBalanceService.GetForPaymentsByConnectionId(waterConnectionResource.SubRoad.WaterProject.OfficeId, waterConnectionResource.ConnectionId);

                    if (wcb != null)
                    {
                        var wcbRes = _mapper.Map<WaterConnection, WaterConnectionResource>(wcb);
                        var partnerAccountResource = _mapper.Map<Partner, PartnerResource>(await _partnerService.GetById(wcb.PartnerId!.Value));
                        var billingAccountResource = _mapper.Map<Partner, PartnerResource>(await _partnerService.GetById(wcb.BillingId!.Value));
                        wcbRes.PartnerAccount = partnerAccountResource;
                        wcbRes.BillingAccount = billingAccountResource;
                        //return Ok(wcbRes);
                        waterreslist.Add(wcbRes);
                    }
                }

            }
            return Ok(waterreslist);
        }


        [HttpGet("getShopRental/{partnerId}/{sabhaId}")]
        public async Task<IEnumerable<Shop>> getShopRental([FromRoute] int partnerId, [FromRoute] int sabhaId)
        {
            var shopRentalsList = await _onlinePaymentService.getShopRental(partnerId, sabhaId);
            return shopRentalsList;
        }

        [HttpPost("pay")]
        public async Task<IActionResult> showPay([FromBody] OrderDetails orderDetails)
        
        {
            try
            {
                var paymentGateway = await _onlinePaymentService.GetGateway(orderDetails.SabhaId);

                if (paymentGateway != null)
                {
                    var paymentDetail = _mapper.Map<OrderDetails, PaymentDetails>(orderDetails);
                    var paymentDetailId = await _onlinePaymentService.SavePaymentDetail(paymentDetail);
                    var getPaymentDetailId = await _onlinePaymentService.GetPaymentDetailById(paymentDetailId);

                    if (paymentDetailId > 0 && paymentDetailId == getPaymentDetailId.PaymentDetailId && paymentDetail.InputAmount >= 0)
                    {
                        orderDetails.PaymentDetailId = paymentDetailId;

                        // var redirectUrl = "http://localhost:4200/success-message";
                        var redirectUrl = _configuration["BOCPaymentGateway:RedirectUrl"];

                        var transactionId =
                            $"OID{orderDetails.Description.Substring(0, 2).ToUpper()}{orderDetails.OrderId} - PID{paymentDetailId}";


                        if (paymentGateway.BankName == "BOC")
                        {
                            //For BOC Gateway
                            var order = new
                            {
                                apiOperation = "INITIATE_CHECKOUT",
                                interaction = new
                                {
                                    operation = "PURCHASE",
                                    returnUrl = redirectUrl
                                },
                                order = new
                                {
                                    currency = "LKR",
                                    id = transactionId,
                                    amount = orderDetails.TotalAmount,
                                    description = $"{orderDetails.Description} payment",
                                    requestorName = orderDetails.PartnerName
                                }
                            };



                            var jsonOrder = JsonConvert.SerializeObject(order);
                            var apiUrlTemplate = _configuration["BOCPaymentGateway:ApiUrl"];
                            var merchantId = paymentGateway.MerchantId;
                            var ApiUrl = apiUrlTemplate.Replace("{MerchantId}", merchantId);
                            var apiUrl = ApiUrl;
                            var apiKey = $"merchant.{paymentGateway.MerchantId}:{paymentGateway.APIKey}";

                            using (var httpClient = new HttpClient())
                            {
                                httpClient.DefaultRequestHeaders.Add("Authorization",
                                    "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey)));

                                var content = new StringContent(jsonOrder, Encoding.UTF8, "application/json");
                                var response = await httpClient.PostAsync(apiUrl, content);

                                if (response.IsSuccessStatusCode)
                                {
                                    var result = await response.Content.ReadAsStringAsync();
                                    var responseObject = JsonConvert.DeserializeObject<BocResponse>(result);

                                    string sessionId = responseObject.session.id;
                                    string successIndicator = responseObject.successIndicator;

                                    paymentDetail.SessionId = sessionId;
                                    paymentDetail.ResultIndicator = successIndicator;
                                    paymentDetail.TransactionId = transactionId;

                                    var updatePaymentDetail = await _onlinePaymentService.UpdatePaymentDetail(paymentDetail);

                                    if (updatePaymentDetail != null && updatePaymentDetail > 0)
                                    {
                                        orderDetails.SessionId = result;
                                        orderDetails.ResultIndicator = successIndicator;
                                        orderDetails.TransactionId = transactionId;
                                    }
                                    else
                                    {
                                        return StatusCode(500);

                                    }

                                }
                                else
                                {
                                    return StatusCode(500);
                                }
                            }

                            var updPD = await _onlinePaymentService.GetPaymentDetailById(paymentDetailId);
                            if (updPD.TransactionId == orderDetails.TransactionId && updPD.ResultIndicator != null && updPD.SessionId != null)
                            {
                                return Ok(orderDetails);
                            }

                            // end for BOC Gateway
                        }
                        else if (paymentGateway.BankName == "PeoplesBank")
                        {
                            try
                            {
                                var transactionIdtocreate = $"OID{orderDetails.Description.Substring(0, 2).ToUpper()}{orderDetails.OrderId} - PID{paymentDetailId}";

                                getPaymentDetailId.TransactionId = transactionIdtocreate;

                                var updatedPaymentDetail = await _onlinePaymentService.UpdatePaymentDetail(getPaymentDetailId);

                                var partnerInfo = await _partnerService.GetById(getPaymentDetailId.PartnerId.Value);

                                var getPaymentDetailNew = await _onlinePaymentService.GetPaymentDetailById(getPaymentDetailId.PaymentDetailId);

                                var url = _configuration["PBPaymentGateway:ApiUrl"];
                                CYBSPEBBasic ipg = new CYBSPEBBasic();
                                string formHtml = ipg.GetPeoplesBankPaymentForm(getPaymentDetailNew, paymentGateway, url, partnerInfo);

                                //var jsonObject = JsonConvert.DeserializeObject<dynamic>(formHtmlAndSignatureJson);

                                //string signature = jsonObject.signature;
                                //string formHtml = jsonObject.formtext;

                                //if (formHtml.Length>0)
                                //{
                                //    var result = await formHtml.Content.ReadAsStringAsync();
                                //    var responseObject = JsonConvert.DeserializeObject<BocResponse>(result);

                                //    //string sessionId = responseObject.session.id;
                                //    //string successIndicator = responseObject.successIndicator;

                                //    //paymentDetail.SessionId = sessionId;
                                //    //paymentDetail.ResultIndicator = successIndicator;
                                //    //paymentDetail.TransactionId = transactionId;

                                //    var updatePaymentDetail = await _onlinePaymentService.UpdatePaymentDetail(paymentDetail);

                                //    if (updatePaymentDetail != null && updatePaymentDetail > 0)
                                //    {
                                //        orderDetails.SessionId = result;
                                //        orderDetails.ResultIndicator = successIndicator;
                                //        orderDetails.TransactionId = transactionId;
                                //    }
                                //    else
                                //    {
                                //        return StatusCode(500);

                                //    }
                                //}
                                //else
                                //{
                                //    return StatusCode(500);
                                //}
                                //getPaymentDetailNew.SessionId = signature;
                                var response = new { formHtml = formHtml, orderDetails = getPaymentDetailNew };
                                return Ok(response);
                            }
                            catch (Exception ex)
                            {
                                // Handle any exceptions and return an appropriate error response
                                return StatusCode(500, new { error = $"An error occurred: {ex.Message}" });
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return StatusCode(500);

                    }
                }
                else
                {
                    return NotFound("Gateway Not Found");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

            return StatusCode(500);

        }


        //[HttpPost("payPB")]
        //public IActionResult showPayPB([FromBody] PaymentFormData formData)
        //{
        //    try
        //    {
        //        CYBSPEBBasic ipg = new CYBSPEBBasic();
        //        string formHtml = ipg.GetDefaultForm(formData.OrderId, formData.PurchaseAmt);
        //        return Ok(formHtml);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500);
        //    }

        //    return StatusCode(500);

        //}

        //[HttpGet("callback")]
        //public IActionResult PaymentCallback([FromQuery] string status, [FromQuery] string transactionId)
        //{
        //    Console.WriteLine($"Payment status: {status}, Transaction ID: {transactionId}");
        //    return Ok();
        //}


        [HttpGet("report/{sabhaId}/{orderId}")]
        public async Task<IActionResult> getReport([FromRoute] int sabhaId, [FromRoute] string orderId)
        {
            // var apiUrl = "https://test-bankofceylon.mtf.gateway.mastercard.com/api/rest/history/version/1/merchant/TEST700193990100/transaction?" +
            //              "columns=merchant,order.id,transaction.id,transaction.amount,transaction.amount&" +
            //              "columnHeaders=Merchant,OrderID,TransactionID,Currency,Amount&" +
            //              "timeOfRecord.end=2024-02-05T13:40:54&timeOfRecord.start=2024-01-01T18:38:40";

            try
            {
                var paymentGateway = await _onlinePaymentService.GetGateway(sabhaId);
                if (paymentGateway.BankName == "BOC")
                {
                    var apiUrlTemplate = _configuration["BOCPaymentGateway:ApiReportUrl"];
                    var merchantId = paymentGateway.MerchantId;
                    int i = 1;
                    while (i >= 0)
                    {
                        var ApiUrl = apiUrlTemplate.Replace("{merchantId}", merchantId).Replace("{orderid}", orderId).Replace("{transactionid}", $"{i}");
                        var apiUrl = ApiUrl;
                        var apiKey = $"merchant.{paymentGateway.MerchantId}:{paymentGateway.APIKey}";

                        using (var httpClient = new HttpClient())
                        {
                            httpClient.DefaultRequestHeaders.Add("Authorization",
                                "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey)));

                            var response = await httpClient.GetAsync(apiUrl);

                            var result = "";
                            if (response.IsSuccessStatusCode)
                            {
                                result = await response.Content.ReadAsStringAsync();
                                var responseObject = JsonConvert.DeserializeObject<ReportResponse>(result);
                                if (responseObject.Result == "SUCCESS")
                                {
                                    return Ok(responseObject);

                                }
                                else
                                {
                                    i++;
                                    continue;
                                }

                            }
                            return StatusCode(500, "");


                        }
                    }
                }
                else if (paymentGateway.BankName == "PeoplesBank")
                {
                    ReportResponse reportResponse = new ReportResponse();
                    reportResponse.Result = "MNL"; 
                    return Ok(reportResponse); // No need to deserialize, just return the object
                }
                return StatusCode(500, "");


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("reportNative/{sabhaId}/{orderId}")]
        public async Task<IActionResult> getReportNative([FromRoute] int sabhaId, [FromRoute] string orderId)
        {
            // var apiUrl = "https://test-bankofceylon.mtf.gateway.mastercard.com/api/rest/history/version/1/merchant/TEST700193990100/transaction?" +
            //              "columns=merchant,order.id,transaction.id,transaction.amount,transaction.amount&" +
            //              "columnHeaders=Merchant,OrderID,TransactionID,Currency,Amount&" +
            //              "timeOfRecord.end=2024-02-05T13:40:54&timeOfRecord.start=2024-01-01T18:38:40";

            try
            {
                var paymentGateway = await _onlinePaymentService.GetGateway(sabhaId);

                var apiUrlTemplate = _configuration["BOCPaymentGateway:ApiReportUrl"];
                var merchantId = paymentGateway.MerchantId;
                int i = 1;
                while (i >= 0)
                {
                    var ApiUrl = apiUrlTemplate.Replace("{merchantId}", merchantId).Replace("{orderid}", orderId).Replace("{transactionid}", $"{i}");
                    var apiUrl = ApiUrl;
                    var apiKey = $"merchant.{paymentGateway.MerchantId}:{paymentGateway.APIKey}";

                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Add("Authorization",
                            "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey)));

                        var response = await httpClient.GetAsync(apiUrl);

                        var result = "";
                        if (response.IsSuccessStatusCode)
                        {
                            result = await response.Content.ReadAsStringAsync();
                            var responseObject = JsonConvert.DeserializeObject<ReportResponse>(result);
                            if (responseObject.Result == "SUCCESS")
                            {
                                return Ok(responseObject);

                            }
                            else
                            {
                                i++;
                                continue;
                            }

                        }
                        return StatusCode(500, "");


                    }
                }
                return StatusCode(500, "");


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


       [HttpPost("placeAssessmentOrder/{status}/{id}")]
        public async Task<IActionResult> PlaceAssessmentOrders([FromBody] List<SaveAsssessmentOrderResource> objectList,
            [FromRoute] int status, [FromRoute] int id, [FromQuery] int? cId)
        {
            try
            {
                if (objectList != null && objectList.Any(obj =>
                        obj.Id == 0 && (obj.MixinOrderLine != null) && (obj.MixinOrderLine.Count > 0)) && status == 1 &&
                    id != null)
                {
                    var assessmentsOrders =
                        _mapper.Map<IEnumerable<SaveAsssessmentOrderResource>, IEnumerable<MixinOrder>>(objectList);
                    var newOrders =
                        await _onlinePaymentService.PlaceAssessmentOrder(assessmentsOrders.ToList(), status, id, cId);

                    var successMessageResource = _mapper.Map<PaymentDetails, SuccessMessageResource>(newOrders);


                    if (successMessageResource.Status == 0)
                    {
                        return Ok(successMessageResource);
                    }

                    if (newOrders.Status == 1 && newOrders.PartnerMobileNo != null)
                    {
                        await SendMessage(newOrders);
                    }


                    if (newOrders.Status == 1 && newOrders.PartnerEmail != null)
                    {
                        await SendEmail(newOrders, assessmentsOrders.First());
                    }

                    return Ok(successMessageResource);
                }
                else
                {
                    var newOrders = await _onlinePaymentService.GetPaymentDetailById(id);
                    var successMessageResource = _mapper.Map<PaymentDetails, SuccessMessageResource>(newOrders);
                    return Ok(successMessageResource);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }





        [HttpPost("placeWaterBillOrder/{status}/{id}")]
        public async Task<IActionResult> PlaceWaetrBillOrders([FromBody] List<MixinOrder> objectList,
             [FromRoute] int status, [FromRoute] int id, [FromQuery] int? cId)
        {
            try
            {
                cId = -1;

             /*   var _token = new HTokenClaim
                {
                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                    IsFinalAccountsEnabled = int.Parse(HttpContext.User.FindFirst("IsFinalAccountsEnabled")!.Value),
                    officeCode = HttpContext.User.FindFirst("officeCode")!.Value,
                    officeTypeID = int.Parse(HttpContext.User.FindFirst("officeTypeID")!.Value),
                };
           
                **/

                if (objectList != null && objectList.Any(obj =>
                        obj.Id == 0 && (obj.MixinOrderLine != null) && (obj.MixinOrderLine.Count > 0)) && status == 1 &&
                    id != null)
                {
                    var assessmentsOrders = objectList;
                 /*   var assessmentsOrders =
                        _mapper.Map<IEnumerable<SaveMixinOrderResource>, IEnumerable<MixinOrder>>(objectList);*/
                    var newOrders =
                        await _onlinePaymentService.PlaceWaterBillOrder(assessmentsOrders.ToList(), status, id, cId );

                    var successMessageResource = _mapper.Map<PaymentDetails, SuccessMessageResource>(newOrders);


                    if (successMessageResource.Status == 0)
                    {
                        return Ok(successMessageResource);
                    }

                    if (newOrders.Status == 1 && newOrders.PartnerMobileNo != null)
                    {
                        await SendMessage(newOrders);
                    }


                    if (newOrders.Status == 1 && newOrders.PartnerEmail != null)
                    {
                        await SendEmail(newOrders, assessmentsOrders.First());
                    }

                    return Ok(successMessageResource);
                }
                else
                {
                    var newOrders = await _onlinePaymentService.GetPaymentDetailById(id);
                    var successMessageResource = _mapper.Map<PaymentDetails, SuccessMessageResource>(newOrders);
                    return Ok(successMessageResource);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }












        /*    [HttpPost("placeWaterBillOrder/{status}/{id}")]
            public async Task<IActionResult> PlaceWaterBillOrders([FromBody] List<SaveAsssessmentOrderResource> objectList,
               [FromRoute] int status, [FromRoute] int id, [FromQuery] int? cId)
            {
                try
                {
                    if (objectList != null && objectList.Any(obj =>
                            obj.Id == 0 && (obj.MixinOrderLine != null) && (obj.MixinOrderLine.Count > 0)) && status == 1 &&
                        id != null)
                    {
                        var assessmentsOrders =
                            _mapper.Map<IEnumerable<SaveAsssessmentOrderResource>, IEnumerable<MixinOrder>>(objectList);
                        var newOrders =
                            await _onlinePaymentService.PlaceAssessmentOrder(assessmentsOrders.ToList(), status, id, cId);

                        var successMessageResource = _mapper.Map<PaymentDetails, SuccessMessageResource>(newOrders);


                        if (successMessageResource.Status == 0)
                        {
                            return Ok(successMessageResource);
                        }

                        if (newOrders.Status == 1 && newOrders.PartnerMobileNo != null)
                        {
                            await SendMessage(newOrders);
                        }


                        if (newOrders.Status == 1 && newOrders.PartnerEmail != null)
                        {
                            await SendEmail(newOrders, assessmentsOrders.First());
                        }

                        return Ok(successMessageResource);
                    }
                    else
                    {
                        var newOrders = await _onlinePaymentService.GetPaymentDetailById(id);
                        var successMessageResource = _mapper.Map<PaymentDetails, SuccessMessageResource>(newOrders);
                        return Ok(successMessageResource);
                    }
                }
                catch (Exception e)
                {
                    return StatusCode(500);
                }

            }



            [HttpPost("placeWaterBillOrdersAndProcessPayments/")]
            public async Task<IActionResult> PlaceWaterBillOrdersAndProcessPayments(SaveWaterBillOrderResource obj)
            {
                try
                {

                    var _token = new HTokenClaim
                    {
                        userId = -1,
                        sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                        officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                        IsFinalAccountsEnabled = int.Parse(HttpContext.User.FindFirst("IsFinalAccountsEnabled")!.Value),
                        officeCode = HttpContext.User.FindFirst("officeCode")!.Value,
                        officeTypeID = int.Parse(HttpContext.User.FindFirst("officeTypeID")!.Value),
                    };

                    if (obj != null && obj.Id == 0 && (obj.MixinOrderLine != null) && (obj.MixinOrderLine.Count > 0))
                    {
                        var mixinOrderToCreate = _mapper.Map<SaveWaterBillOrderResource, MixinOrder>(obj);
                        var newMixinOrder = await _mixinOrderService.PlaceWaterBillOrdersAndProcessPayments(mixinOrderToCreate, _token);

                        if (newMixinOrder.Id != null)
                        {
                            return Ok(newMixinOrder);
                        }

                        return BadRequest();
                    }
                    return BadRequest();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }

            */


        [HttpGet("otherPayment/{status}/{id}")]
        public async Task<IActionResult> PlaceOtherPayment([FromRoute] int status, int id)
        {
            var placeOtherPaymentOrder = await _onlinePaymentService.PlaceOtherPaymentOrder(status, id);
            var successMessageResource = _mapper.Map<PaymentDetails, SuccessMessageResource>(placeOtherPaymentOrder);
            var mixinOrder = new MixinOrder();
            mixinOrder = null;

            try
            {

                if (successMessageResource.Status == 1)
                {


                    if (placeOtherPaymentOrder.PartnerMobileNo != null)
                    {
                        await SendMessage(placeOtherPaymentOrder);
                    }

                    if (placeOtherPaymentOrder.PartnerEmail != null)
                    {
                        await SendEmail(placeOtherPaymentOrder, mixinOrder);
                    }

                    return Ok(successMessageResource);
                }
                else
                {
                    return Ok(successMessageResource);
                }
            }
            catch (Exception e)
            {
                return Ok(successMessageResource);
            }
        }

        [HttpGet("bookingPayment/{status}/{id}/{orderId}")]
        public async Task<IActionResult> PlaceBookingPayment([FromRoute] int status, int id,int orderId)
        {
            var placeBookingPaymentOrder = await _onlinePaymentService.PlaceBookingPaymentOrder(status, id ,orderId);
            var successMessageResource = _mapper.Map<PaymentDetails, SuccessMessageResource>(placeBookingPaymentOrder);
            var mixinOrder = new MixinOrder();
            mixinOrder = null;

            try
            {

                if (successMessageResource.Status == 1)
                {
                    if (placeBookingPaymentOrder.PartnerMobileNo != null)
                    {
                        await SendMessage(placeBookingPaymentOrder);
                    }

                    if (placeBookingPaymentOrder.PartnerEmail != null)
                    {
                        await SendEmail(placeBookingPaymentOrder, mixinOrder);
                    }

                    return Ok(successMessageResource);
                }
                else
                {
                    return Ok(successMessageResource);
                }
            }
            catch (Exception e)
            {
                return Ok(successMessageResource);
            }
        }


        private async Task SendMessage(PaymentDetails paymentDetail)
        {
            var sabha = await _sabhaService.GetSabhaById(paymentDetail.SabhaId.Value);

            var message =
                $"Your {paymentDetail.Description} payment Rs.{Math.Round(paymentDetail.InputAmount ?? 0, 2)} has been received to {paymentDetail.Description} number {paymentDetail.AccountNo} on {paymentDetail.CreatedAt}.\nThank you,\nSecretary - {sabha.NameEnglish}";


            var map = new SMSResource
            {
                MobileNo = paymentDetail.PartnerMobileNo,
                Text = message,
                SabhaId = paymentDetail.SabhaId ?? -1,
                Module = "OnlineUser",
                Subject = "Payment Success Message"
            };
            SMSService sms = new SMSService(_smsConfigurationService, _smsOutBoxService);
            await sms.createSMS(map);
        }

        private async Task SendEmail(PaymentDetails paymentDetail, MixinOrder mixinOrder)
        {
            var sabha = await _sabhaService.GetSabhaById(paymentDetail.SabhaId.Value);

            await _emailOutBoxService.CreateEmailOutBox(new EmailOutBox
            {
                Attachment = string.Empty,
                Bc = string.Empty,
                Cc = string.Empty,
                CreatedByID = -1,
                EmailSendAttempts = 0,
                EmailStatus = EmailStatus.Pending,
                IsBodyHtml = true,
                MailContent = MailBody(paymentDetail, sabha, mixinOrder),
                Note = string.Empty,
                Recipient = paymentDetail.PartnerEmail,
                Subject = $"CAT20 Payment Confirmation #Transaction ID : {paymentDetail.TransactionId}"
            });
            EmailService email = new EmailService(_emailConfigurationService, _emailOutBoxService);
            await email.sendMail();
        }

        private string MailBody(PaymentDetails paymentDetails, Sabha sabha, MixinOrder mixinOrder)
        {


            var sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"en\">");
            sb.AppendLine("<head>");
            sb.AppendLine("    <meta charset=\"UTF-8\">");
            sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            sb.AppendLine("    <style>");
            sb.AppendLine("        body {");
            sb.AppendLine("            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;");
            sb.AppendLine("            color: #333;");
            sb.AppendLine("            background-color: #f5f5f5;");
            sb.AppendLine("            text-align: center;");
            sb.AppendLine("            padding: 20px;");
            sb.AppendLine("        }");
            sb.AppendLine("        .container {");
            sb.AppendLine("            max-width: 600px;");
            sb.AppendLine("            margin: 0 auto;");
            sb.AppendLine("        }");
            sb.AppendLine("        .header {");
            sb.AppendLine("            background-color: #007BFF;");
            sb.AppendLine("            color: #fff;");
            sb.AppendLine("            text-align: center;");
            sb.AppendLine("            padding: 20px;");
            sb.AppendLine("        }");
            sb.AppendLine("        .content {");
            sb.AppendLine("            padding: 20px;");
            sb.AppendLine("        }");
            sb.AppendLine("        .footer {");
            sb.AppendLine("            text-align: center;");
            sb.AppendLine("            padding: 10px;");
            sb.AppendLine("            background-color: #f8f9fa;");
            sb.AppendLine("        }");
            sb.AppendLine("        h2 {");
            sb.AppendLine("            color: #4CAF50;");
            sb.AppendLine("        }");
            sb.AppendLine("        strong {");
            sb.AppendLine("            color: #333;");
            sb.AppendLine("        }");
            sb.AppendLine("        a {");
            sb.AppendLine("            color: #007BFF;");
            sb.AppendLine("            text-decoration: none;");
            sb.AppendLine("        }");
            sb.AppendLine("        a:hover {");
            sb.AppendLine("            text-decoration: underline;");
            sb.AppendLine("        }");
            sb.AppendLine("        table {");
            sb.AppendLine("            width: 100%;");
            sb.AppendLine("            border-collapse: collapse;");
            sb.AppendLine("            margin-top: 20px;");
            sb.AppendLine("        }");
            sb.AppendLine("        table, th, td {");
            sb.AppendLine("            border: 1px solid #ddd;");
            sb.AppendLine("        }");
            sb.AppendLine("        th, td {");
            sb.AppendLine("            padding: 10px;");
            sb.AppendLine("            text-align: left;");
            sb.AppendLine("        }");
            sb.AppendLine("        th {");
            sb.AppendLine("            background-color: #f2f2f2;");
            sb.AppendLine("        }");
            sb.AppendLine("    </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("    <div class=\"container\">");
            sb.AppendLine("        <div class=\"header\">");
            sb.AppendLine("            <h1>Confirmation of Payment</h1>");
            sb.AppendLine("        </div>");
            sb.AppendLine("        <div class=\"content\">");
            sb.AppendLine("            <p>Dear Valued Customer,</p>");
            sb.AppendLine("            <p>Thank you for using the CAT20 Local Government Payment System.</p>");
            sb.AppendLine("            <p>Your confirmation of " + paymentDetails.Description +
                          " payment is attached herewith.</p>");
            sb.AppendLine(
                "            <p>If there is any dispute on this invoice, please report within the same day before 4.00 PM of receipt of this invoice to Billing Inquiry on " + sabha.Telephone1 + ".</p>");
            sb.AppendLine("            <p>Thank You.</p>");
            sb.AppendLine("            <p>Secretary - " + sabha.NameEnglish + " </p>");
            // sb.AppendLine("            <p><strong>pay.cat20.lk</strong></p>");
            sb.AppendLine(
                "            <p>Do not reply to this e-mail as it is automatically generated and not linked to a user.</p>");

            // Invoice Section
            sb.AppendLine("            <table>");
            sb.AppendLine("                <tr>");
            sb.AppendLine("                    <th>Payment Details</th>");
            sb.AppendLine("                    <th></th>");
            sb.AppendLine("                </tr>");
            sb.AppendLine("                <tr>");
            sb.AppendLine("                    <td><strong>Payment Amount:</strong></td>");
            sb.AppendLine("                    <td>" + Math.Round(paymentDetails.InputAmount ?? 0, 2) +
                          " LKR (Payment is subjected to confirmation)</td>");
            sb.AppendLine("                </tr>");
            sb.AppendLine("                <tr>");
            sb.AppendLine("                    <td><strong>" + paymentDetails.Description + " Number:</strong></td>");
            sb.AppendLine("                    <td>" + paymentDetails.AccountNo + "</td>");
            sb.AppendLine("                </tr>");
            sb.AppendLine("                <tr>");
            sb.AppendLine("                    <td><strong>Customer Name:</strong></td>");

            if (mixinOrder != null)
            {
                sb.AppendLine("                    <td>" + mixinOrder.CustomerName.ToString() + "</td>");
            }
            else
            {
                sb.AppendLine("                    <td>" + paymentDetails.PartnerName.ToString() + "</td>");
            }
            sb.AppendLine("                </tr>");
            // sb.AppendLine("                <tr>");
            // sb.AppendLine("                    <td><strong>Address:</strong></td>");
            // sb.AppendLine("                    <td>MADAME WATTA, KAHAPATHWALA</td>");
            // sb.AppendLine("                </tr>");
            sb.AppendLine("                <tr>");
            sb.AppendLine("                    <td><strong>Payment Date:</strong></td>");
            sb.AppendLine("                    <td>" + paymentDetails.CreatedAt + "</td>");
            sb.AppendLine("                </tr>");
            sb.AppendLine("                <tr>");
            sb.AppendLine("                    <td><strong>Payment Agent:</strong></td>");
            sb.AppendLine("                    <td>CAT20 Payment System</td>");
            sb.AppendLine("                </tr>");
            sb.AppendLine("            </table>");
            // sb.AppendLine("            <p>Download <a href=\"your_app_download_link\">CEB Care App</a> for mobile</p>");
            sb.AppendLine(
                "            <p style=\"text-align: center;\">Powered by Wayamba Development Authority © " + DateTime.Now.Year + "</p>");
            sb.AppendLine("        </div>");
            sb.AppendLine("        <div class=\"footer\">");
            sb.AppendLine("            <p>&copy; " + DateTime.Now.Year + " CAT20 Local Government Online Payment System</p>");
            sb.AppendLine("        </div>");
            sb.AppendLine("    </div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");


            string html = sb.ToString();


            var htmlContent = sb.ToString();
            return htmlContent;
        }

        [HttpPost("systemErrMsg")]
        public async Task<bool> SystemErrorMSG([FromBody] SystemErrorMessage systemErrorMessage)
        {
            var dispute = _mapper.Map<SystemErrorMessage, Dispute>(systemErrorMessage);


            var savedDispute = await _onlinePaymentService.CreateDispute(dispute);
            var disputeId = $"DID#{savedDispute.Id}";

            try
            {


                var message =
                    $" Dear Valued Customer, Unfortunately, an error has occurred, and your payment cannot be processed at this time. Please contact 0372228204 immediately. Your inquiry ID is {disputeId}\nThank you.";

                var map = new SMSResource
                {
                    MobileNo = systemErrorMessage.PartnerMobileNo,
                    Text = message,
                    SabhaId = systemErrorMessage.SabhaId ?? -1,
                    Module = "OnlineUser",
                    Subject = "Payment Failed Message"
                };
                SMSService sms = new SMSService(_smsConfigurationService, _smsOutBoxService);
                await sms.createSMS(map);
                return true;
            }
            catch (Exception e)
            {
                return false;
                Console.WriteLine(e);
                throw;
            }
        }


        [HttpGet("placeOrderBackup/{status}/{id}")]
        public async Task<IActionResult> PlaceOrderBackUp([FromRoute] int status, [FromRoute] int id)
        {
            try
            {
                var placeOrderBackUp = await _onlinePaymentService.PlaceOrderBackUp(status, id);
                return Ok(placeOrderBackUp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [HttpGet("result/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var paymentDetailById = await _onlinePaymentService.GetPaymentDetailById(id);
                return Ok(paymentDetailById);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }


        [HttpGet("getPaymentDetails/{officeId}")]
        public async Task<ActionResult<ResponseModel<PaymentDetails>>> GetAllPaymentDetails([FromRoute] int officeId, [FromQuery] int? pageNumber, [FromQuery] int? pageSize,
            [FromQuery] string? filterKeyWord)
        {
            try
            {
                var paymentDetailById = await _onlinePaymentService.GetAllPaymentDetails(officeId, pageNumber, pageSize, filterKeyWord);
                var model = new ResponseModel<PaymentDetails>
                {
                    totalResult = paymentDetailById.totalCount,
                    list = paymentDetailById.list
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("getOtherPaymentDetails/{officeId}")]
        public async Task<ActionResult<ResponseModel<PaymentDetails>>> GetOtherPaymentDetails([FromRoute] int officeId, [FromQuery] int? pageNumber, [FromQuery] int? pageSize,
            [FromQuery] string? filterKeyWord)
        {
            try
            {
                var paymentDetailById = await _onlinePaymentService.GetOtherPaymentDetails(officeId, pageNumber, pageSize, filterKeyWord);
                var model = new ResponseModel<PaymentDetails>
                {
                    totalResult = paymentDetailById.totalCount,
                    list = paymentDetailById.list
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [HttpGet("getDisputes/{officeId}")]
        public async Task<ActionResult<ResponseModel<PaymentDetails>>> getDisputes([FromRoute] int officeId, [FromQuery] int? pageNumber, [FromQuery] int? pageSize,
            [FromQuery] string? filterKeyWord)
        {
            try
            {
                var paymentDetailById = await _onlinePaymentService.getDisputes(officeId, pageNumber, pageSize, filterKeyWord);
                var model = new ResponseModel<PaymentDetails>
                {
                    totalResult = paymentDetailById.totalCount,
                    list = paymentDetailById.list
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("check/{id}/{cId}/{flag}")]
        public async Task<IActionResult> CheckByCashier([FromRoute] int id, [FromRoute] int cId, [FromRoute] int flag)
        {
            try
            {
                var paymentDetailById = await _onlinePaymentService.CheckByCashier(id, cId, flag);
                if (paymentDetailById != null)
                {
                    return Ok(paymentDetailById);
                }

                return StatusCode(500);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }


        [HttpPost("save")]
        public async Task<int?> saveGateway([FromBody] PaymentGatewayResource paymentGateway)
        {
            try
            {
                var paymentDetail = _mapper.Map<PaymentGatewayResource, PaymentGateway>(paymentGateway);
                var paymentId = await _onlinePaymentService.SaveGateway(paymentDetail);
                return paymentId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("get/{sabhaid}")]
        public async Task<PaymentGatewayResource> getGateway([FromRoute] int sabhaid)
        {
            var paymentGateway = await _onlinePaymentService.GetGateway(sabhaid);

            var paymentGatewayResource = _mapper.Map<PaymentGateway, PaymentGatewayResource>(paymentGateway);
            return paymentGatewayResource;
        }

        [HttpGet("getServiceCharge/{sabhaid}")]
        public async Task<decimal> getServiceCharge([FromRoute] int sabhaid)
        {
            try
            {
                var paymentGateway = await _onlinePaymentService.GetGateway(sabhaid);

                return paymentGateway.ServicePercentage;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("getGatewayAndConfigInfoForSabha/{sabhaid}")]
        public async Task<PaymentGatewayBasicResource> getGatewayAndConfigInfoForSabha([FromRoute] int sabhaid)
        {
            try
            {
                var paymentGateway = await _onlinePaymentService.GetGateway(sabhaid);
                var assessmentResources = _mapper.Map<PaymentGateway, PaymentGatewayBasicResource>(paymentGateway);
                return assessmentResources;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpGet("saveError/{id}")]
        public async Task<IActionResult> saveError([FromRoute] int id)
        {
            var er = _onlinePaymentService.SaveError(id);
            return Ok(5);
        }

        [HttpPost("saveOtherPayment")]
        public async Task<IActionResult> Save([FromBody] SaveMixinOrderResource obj, [FromQuery] int? cId, [FromQuery] bool? dispute)
        {
            try
            {
                if (obj != null && obj.Id == 0 && (obj.MixinOrderLine != null) && (obj.MixinOrderLine.Count > 0))
                {
                    var mixinOrderToCreate = _mapper.Map<SaveMixinOrderResource, MixinOrder>(obj);
                    var newMixinOrder = await _onlinePaymentService.Create(mixinOrderToCreate, cId, dispute);

                    if (newMixinOrder != null)
                    {
                        return Ok(newMixinOrder);
                    }

                    return StatusCode(500);
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("partnerPaymentHistory/{partnerId}")]
        public async Task<IEnumerable<PaymentDetails>> getPartnerPaymentHistory([FromRoute] int partnerId, [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            try
            {
                var paymentDetailById = await _onlinePaymentService.getPartnerPaymentHistory(partnerId, pageNumber, pageSize);
                return paymentDetailById;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [HttpGet("partnerDisputes/{partnerId}")]
        public async Task<IEnumerable<PaymentDetails>> getPartnerDisputes([FromRoute] int partnerId, [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            try
            {
                var paymentDetailById = await _onlinePaymentService.getPartnerDisputes(partnerId, pageNumber, pageSize);

                return paymentDetailById;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("getPaymentHistoryForOffice/{officeId}")]
        public async Task<ActionResult<ResponseModel<PaymentDetails>>> getPaymentHistoryForOffice([FromRoute] int officeId, [FromQuery] int? pageNumber, [FromQuery] int? pageSize,
            [FromQuery] string? filterKeyWord)
        {
            try
            {
                var paymentDetailById = await _onlinePaymentService.getPaymentHistoryForOffice(officeId, pageNumber, pageSize, filterKeyWord);
                var model = new ResponseModel<PaymentDetails>
                {
                    totalResult = paymentDetailById.totalCount,
                    list = paymentDetailById.list
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("activeScheduler")]
        public async Task<IActionResult> activeScheduler()
        {
            try
            {
                var paymentDetailSheduler = await _onlinePaymentService.paymentDetailSheduler();
                return Ok(paymentDetailSheduler);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("getBankStatementForSabhaAndDateRange/{sabhaid}/{fromdate}/{todate}")]
        public async Task<IActionResult> getBankStatementForSabhaAndDateRange([FromRoute] int sabhaid, string fromdate, string todate)
        {
            try
            {
                var paymentGateway = await _onlinePaymentService.GetGateway(sabhaid);

                var BankReportingServerApi = _configuration["BOCPaymentGateway:BankReportingServerApi"];
                var merchantId = paymentGateway.MerchantId;
                var reportApiKey = paymentGateway.ReportAPIKey;

                if (!DateTime.TryParse(fromdate, out DateTime startDate) || !DateTime.TryParse(todate, out DateTime endDate))
                {
                    return BadRequest("Invalid date format. Please use a valid date format.");
                }

                // string startTimeFormatted = startDate.ToString("yyyy-MM-ddTHH:mm:ssZ");
                // string endTimeFormatted = endDate.ToString("yyyy-MM-ddTHH:mm:ssZ");
                string startTimeFormatted = fromdate;
                string endTimeFormatted = todate;
                string columns = "transaction.acquirer.merchantId,timeOfRecord,order.id,transaction.id,order.status,sourceOfFunds.type,sourceOfFunds.provided.card.number,sourceOfFunds.provided.card.expiry.month,sourceOfFunds.provided.card.expiry.year,transaction.amount,transaction.currency,transaction.type,transaction.acquirer.id,response.acquirerCode,sourceOfFunds.provided.ach.bankAccountHolder,risk.response.gatewayCode,risk.response.reversalResult,accountFunding.recipient.account.fundingMethod,accountFunding.reference";
                string columnHeaders = "merchantId,dateandtime,orderid,transaction_id,order_status,payment_method,card_number,card_expiry_month,card_expiry_year,amount,currency,type,acquirer,acquirerCode,bankAccountHolder,riskresponse,riskreview,funding_method,reference";

                using (HttpClientHandler handler = new HttpClientHandler { Credentials = new NetworkCredential("merchant.default", reportApiKey) })
                using (HttpClient client = new HttpClient(handler))
                {
                    string safeHeader = Uri.EscapeDataString(columnHeaders);

                    var ApiUrl = BankReportingServerApi.Replace("{merchantId}", merchantId).Replace("{startTimeFormatted}", startTimeFormatted).Replace("{endTimeFormatted}", endTimeFormatted).Replace("{columns}", columns).Replace("{safeHeader}", safeHeader);

                    HttpResponseMessage response = await client.GetAsync(ApiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();

                        using (var reader = new StringReader(result))
                        {
                            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                            {
                                var records = csv.GetRecords<dynamic>()
                               .Where(record => record.order_status == "CAPTURED" && record.type == "PAYMENT") // Filter by order_status and type
                               .OrderByDescending(record => DateTime.Parse(record.dateandtime)) // Order by dateandtime in descending order
                               .ToList();

                                // Return the filtered records as JSON
                                return Ok(records);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.ReasonPhrase}");

                        return StatusCode((int)response.StatusCode, $"Error: {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error information
                Console.WriteLine($"Error: {ex.Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }

        //public class PaymentFormData
        //{
        //    public string OrderId { get; set; }
        //    public string PurchaseAmt { get; set; }
        //}

        //public class CYBSPEBBasic
        //{
        //    public string GetDefaultForm(string OrderID, string PurchaseAmt)
        //    {
        //        string SECRET_KEY = "d6ef666014be4cda9421ac41e51048d36527af1800914fd4ae371d43907ec101082cd843f0c34450a0b9299883dbf055af7bf4112ced4e3497024368698e7ba3394a8c3761a4455a9d35b61231b810e90c10bc5a7e1042c597b80c0ea02bdbe0291e061a2d7448648aedfb326a305a9b391469a697ce4b7aae5c56431eda3350";
        //        string access_key = "47e0d13063583e179ee8ecf126cdb878";
        //        string profile_id = "E6719FA3-6E97-41FA-9935-EBFE3DC5B466";
        //        string url = "https://testsecureacceptance.cybersource.com/pay";
        //        Dictionary<string, string> parameters = new Dictionary<string, string>();
        //        parameters["transaction_uuid"] = Guid.NewGuid().ToString();
        //        parameters["access_key"] = access_key;
        //        parameters["profile_id"] = profile_id;
        //        parameters["signed_field_names"] = "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,currency";
        //        parameters["unsigned_field_names"] = "auth_trans_ref_no,bill_to_forename,bill_to_surname,bill_to_address_line1,bill_to_address_city,bill_to_address_country,bill_to_email";
        //        parameters["signed_date_time"] = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
        //        parameters["locale"] = "en";
        //        parameters["transaction_type"] = "sale";
        //        parameters["reference_number"] = OrderID;
        //        parameters["auth_trans_ref_no"] = OrderID;
        //        parameters["amount"] = PurchaseAmt;
        //        parameters["currency"] = "LKR";
        //        parameters["bill_to_email"] = "null@cybersource.com";
        //        parameters["bill_to_forename"] = "noreal";
        //        parameters["bill_to_surname"] = "name";
        //        parameters["bill_to_address_line1"] = "1295 Charleston Rd";
        //        parameters["bill_to_address_city"] = "Mountain View";
        //        parameters["bill_to_address_country"] = "US";
        //        parameters["bill_to_address_postal_code"] = "94043";
        //        parameters["signature"] = Sign(parameters, SECRET_KEY);

        //        StringBuilder formText = new StringBuilder();
        //        formText.Append("<script src=\"https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js\"></script>");
        //        formText.Append("<script>$(\"#payment_confirmation\").ready(function(){$(\"#payment_confirmation\").submit();});</script>");
        //        formText.Append("<form type=\"hidden\" id=\"payment_confirmation\" action=\"" + url + "\" method=\"post\">");
        //        foreach (var kvp in parameters)
        //        {
        //            formText.Append("<input type='hidden' id='" + kvp.Key + "' name='" + kvp.Key + "' value='" + kvp.Value + "'/><br/>");
        //        }
        //        formText.Append("</form>");

        //        return formText.ToString();
        //    }

        //    private string Sign(Dictionary<string, string> parameters, string SECRET_KEY)
        //    {
        //        return SignData(BuildDataToSign(parameters), SECRET_KEY);
        //    }

        //    private string SignData(string data, string secretKey)
        //    {
        //        byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
        //        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        //        using (HMACSHA256 hmac = new HMACSHA256(keyBytes))
        //        {
        //            byte[] hashBytes = hmac.ComputeHash(dataBytes);
        //            return Convert.ToBase64String(hashBytes);
        //        }
        //    }

        //    private string BuildDataToSign(Dictionary<string, string> parameters)
        //    {
        //        string signedFieldNames = parameters["signed_field_names"];
        //        string[] signedFields = signedFieldNames.Split(',');
        //        List<string> dataToSign = new List<string>();
        //        foreach (string field in signedFields)
        //        {
        //            dataToSign.Add(field + "=" + parameters[field]);
        //        }
        //        return CommaSeparate(dataToSign);
        //    }

        //    private string CommaSeparate(List<string> dataToSign)
        //    {
        //        return string.Join(",", dataToSign);
        //    }
        //}


        [HttpPost("pbPaymentResponse")]
        public IActionResult PbPaymentResponse()
        {
            var baseUrl = _configuration["PBPaymentGateway:RedirectUrl"];
            //string baseUrl = "https://testpay.cat2020.lk";
            //string baseUrl = "http://localhost:4200";
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    string formData = reader.ReadToEnd();

                    var formDataDictionary = ParseFormData(formData);

                    string jsonData = JsonConvert.SerializeObject(formDataDictionary);

                    PbResponseDataModel model = JsonConvert.DeserializeObject<PbResponseDataModel>(jsonData);
                    
                    return Redirect($"{baseUrl}?data={HttpUtility.UrlEncode(jsonData)}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //return StatusCode(500, "An error occurred while processing the request.");
                return Redirect($"{baseUrl}/success-message?data={HttpUtility.UrlEncode("")}");
            }
        }
        //[HttpPost("bocPaymentResponse")]
        //public IActionResult BOCPaymentResponse()
        //{
        //    //string baseUrl = "http://localhost:4200";
        //    string baseUrl = "https://testpay.cat2020.lk";
        //    try
        //    {
        //        // Read the request body
        //        using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
        //        {
        //            string formData = reader.ReadToEnd();

        //            // Parse the URL-encoded form data
        //            var formDataDictionary = ParseFormData(formData);

        //            // Serialize the dictionary to JSON
        //            string jsonData = JsonConvert.SerializeObject(formDataDictionary);

        //            // Deserialize JSON data into ProcessDataModel
        //            PbResponseDataModel model = JsonConvert.DeserializeObject<PbResponseDataModel>(jsonData);

        //            // Return the JSON data
        //            //return Ok(model);

        //            //return Redirect($"http://localhost:4200/success-message?data={HttpUtility.UrlEncode(jsonData)}");
        //            return Redirect($"{baseUrl}/success-message?data={HttpUtility.UrlEncode(jsonData)}");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        // Log the exception for debugging purposes
        //        Console.WriteLine(e);
        //        return StatusCode(500, "An error occurred while processing the request.");
        //    }
        //}

        //[HttpPost("pbPaymentResponse")]
        //public async Task<IActionResult> PbPaymentResponse()
        //{
        //    try
        //    {
        //        // Read the request body
        //        using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
        //        {
        //            string formData = reader.ReadToEnd();

        //            // Parse the URL-encoded form data
        //            var formDataDictionary = ParseFormData(formData);

        //            // Serialize the dictionary to JSON
        //            string jsonData = JsonConvert.SerializeObject(formDataDictionary);

        //            // Deserialize JSON data into ProcessDataModel
        //            PbResponseDataModel pbresponse = JsonConvert.DeserializeObject<PbResponseDataModel>(jsonData);

        //            string baseUrl = "http://localhost:4200";
        //            int status = 1;

        //            if (pbresponse.decision == "ACCEPT" && pbresponse.reason_code == "100" && int.Parse(pbresponse.transaction_id) > 0)
        //            {
        //                var paymentdetail = await _onlinePaymentService.GetPaymentDetailById(int.Parse(pbresponse.transaction_id));
        //                if (paymentdetail != null)
        //                {
        //                    if (paymentdetail.Description == "Assessment") //Assessment
        //                    {
        //                        if (objectList != null && objectList.Any(obj => obj.Id == 0 && (obj.MixinOrderLine != null) && (obj.MixinOrderLine.Count > 0)) && status == 1 && id != null)
        //                        {
        //                            var assessmentsOrders =
        //                                _mapper.Map<IEnumerable<SaveAsssessmentOrderResource>, IEnumerable<MixinOrder>>(objectList);
        //                            var newOrders =
        //                                await _onlinePaymentService.PlaceAssessmentOrder(assessmentsOrders.ToList(), status, id, cId);

        //                            var successMessageResource = _mapper.Map<PaymentDetails, SuccessMessageResource>(newOrders);


        //                            if (successMessageResource.Status == 0)
        //                            {
        //                                return Ok(successMessageResource);
        //                            }

        //                            if (newOrders.Status == 1 && newOrders.PartnerMobileNo != null)
        //                            {
        //                                await SendMessage(newOrders);
        //                            }

        //                            if (newOrders.Status == 1 && newOrders.PartnerEmail != null)
        //                            {
        //                                await SendEmail(newOrders);
        //                            }

        //                            return Ok(successMessageResource);


        //                        }
        //                        else
        //                        {
        //                            var newOrders = await _onlinePaymentService.GetPaymentDetailById(id);
        //                            var successMessageResource = _mapper.Map<PaymentDetails, SuccessMessageResource>(newOrders);
        //                            return Ok(successMessageResource);
        //                        }

        //                    }
        //                    else
        //                    { //Other Payment
        //                        var placeOtherPaymentOrder = await _onlinePaymentService.PlaceOtherPaymentOrder(status, paymentdetail.PaymentDetailId.Value);
        //                        var successMessageResource = _mapper.Map<PaymentDetails, SuccessMessageResource>(placeOtherPaymentOrder);

        //                        try
        //                        {
        //                            if (successMessageResource.Status == 1)
        //                            {
        //                                if (placeOtherPaymentOrder.PartnerMobileNo != null)
        //                                {
        //                                    await SendMessage(placeOtherPaymentOrder);
        //                                }
        //                                if (placeOtherPaymentOrder.PartnerEmail != null)
        //                                {
        //                                    await SendEmail(placeOtherPaymentOrder);
        //                                }
        //                                //return Ok(successMessageResource);
        //                                //return Redirect($"{baseUrl}/success-message?data={HttpUtility.UrlEncode(jsonData)}");
        //                                string jsonDatasuccessMessage = JsonConvert.SerializeObject(successMessageResource);
        //                                return Redirect($"{baseUrl}/success-message?data={HttpUtility.UrlEncode(jsonDatasuccessMessage)}");
        //                            }
        //                            else
        //                            {
        //                                string jsonDatasuccessMessage = JsonConvert.SerializeObject(successMessageResource);
        //                                return Redirect($"{baseUrl}/success-message?data={HttpUtility.UrlEncode(jsonDatasuccessMessage)}");
        //                            }
        //                        }
        //                        catch (Exception e)
        //                        {
        //                            string jsonDatasuccessMessage = JsonConvert.SerializeObject(successMessageResource);
        //                            return Redirect($"{baseUrl}/success-message?data={HttpUtility.UrlEncode(jsonDatasuccessMessage)}");
        //                        }
        //                    }
        //                }
        //                else { 
        //                    return BadRequest();
        //                }
        //            }
        //            else
        //            {
        //                return BadRequest("Payment Failed");
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        // Log the exception for debugging purposes
        //        Console.WriteLine(e);
        //        return StatusCode(500, "An error occurred while processing the request.");
        //    }
        //}


        private Dictionary<string, string> ParseFormData(string formData)
        {
            var formDataDictionary = new Dictionary<string, string>();

            // Split the form data into key-value pairs
            string[] pairs = formData.Split('&');

            foreach (string pair in pairs)
            {
                // Split each pair into key and value
                string[] keyValue = pair.Split('=');

                // Decode the URL-encoded key and value
                string key = HttpUtility.UrlDecode(keyValue[0]);
                string value = HttpUtility.UrlDecode(keyValue[1]);

                // Add key-value pair to the dictionary
                formDataDictionary.Add(key, value);
            }

            return formDataDictionary;
        }
    }



    public class PbResponseDataModel
    {
        public string? utf8 { get; set; }
        public string? auth_cv_result { get; set; }
        public string? req_locale { get; set; }
        public string? req_payer_authentication_indicator { get; set; }
        public string? payer_authentication_acs_transaction_id { get; set; }
        public string? auth_trans_ref_no { get; set; }
        public string? req_card_type_selection_indicator { get; set; }
        public string? payer_authentication_enroll_veres_enrolled { get; set; }
        public string? req_bill_to_surname { get; set; }
        public string? req_card_expiry_date { get; set; }
        public string? card_type_name { get; set; }
        public string? auth_amount { get; set; }
        public string? auth_response { get; set; }
        public string? bill_trans_ref_no { get; set; }
        public string? req_payment_method { get; set; }
        public string? req_payer_authentication_merchant_name { get; set; }
        public string? auth_time { get; set; }
        public string? transaction_id { get; set; }
        public string? req_card_type { get; set; }
        public string? payer_authentication_transaction_id { get; set; }
        public string? payer_authentication_pares_status { get; set; }
        public string? payer_authentication_cavv { get; set; }
        public string? auth_avs_code { get; set; }
        public string? auth_code { get; set; }
        public string? payer_authentication_specification_version { get; set; }
        public string? req_bill_to_address_country { get; set; }
        public string? auth_cv_result_raw { get; set; }
        public string? req_profile_id { get; set; }
        public string? signed_date_time { get; set; }
        public string? req_bill_to_address_line1 { get; set; }
        public string? payer_authentication_validate_e_commerce_indicator { get; set; }
        public string? req_card_number { get; set; }
        public string? signature { get; set; }
        public string? req_bill_to_address_city { get; set; }
        public string? auth_cavv_result { get; set; }
        public string? reason_code { get; set; }
        public string? req_bill_to_forename { get; set; }
        public string? req_payer_authentication_acs_window_size { get; set; }
        public string? payment_account_reference { get; set; }
        public string? request_token { get; set; }
        public string? auth_cavv_result_raw { get; set; }
        public string? req_amount { get; set; }
        public string? req_bill_to_email { get; set; }
        public string? payer_authentication_reason_code { get; set; }
        public string? auth_avs_code_raw { get; set; }
        public string? req_currency { get; set; }
        public string? decision { get; set; }
        public string? message { get; set; }
        public string? signed_field_names { get; set; }
        public string? req_transaction_uuid { get; set; }
        public string? payer_authentication_eci { get; set; }
        public string? req_transaction_type { get; set; }
        public string? payer_authentication_xid { get; set; }
        public string? req_access_key { get; set; }
        public string? req_reference_number { get; set; }
        public string? payer_authentication_validate_result { get; set; }
        public string? auth_reconciliation_reference_number { get; set; }
    }
}