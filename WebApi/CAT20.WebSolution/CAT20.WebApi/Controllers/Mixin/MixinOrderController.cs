using AutoMapper;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Services.User;
using CAT20.Core.Services.Vote;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.Mixin;
using CAT20.WebApi.Resources.Mixin.Save;
using CAT20.WebApi.Resources.Pagination;
using CAT20.WebApi.Resources.Vote;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using CAT20.WebApi.Resources.Control;
using CAT20.Core.HelperModels;
using CAT20.Services.Mixin;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.ShopRental;

namespace CAT20.Api.Controllers
{
    [Route("api/mixin/mixinOrder")]
    [ApiController]
    public class MixinOrderController : BaseController
    {
        private readonly IMixinOrderService _mixinOrderService;
        private readonly IMixinOrderLineService _mixinOrderLineService;
        private readonly IMixinCancelOrderService _mixinCancelOrderService;
        private readonly IVoteDetailService _voteDetailService;
        private readonly IAccountDetailService _accountDetailService;
        private readonly IOfficeService _officeService;
        private readonly ISabhaService _sabhaService;
        private readonly ISelectedLanguageService _selectedLanguageService;
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;
        private readonly IVoteAssignmentService _voteAssignmentService;
        private readonly IVoteAssignmentDetailsService _voteAssignmentDetailsService;
        private readonly IGnDivisionService _gnDivisionService;
        private readonly IPaymentNbtService _paymentNbtService;
        private readonly IPaymentVatService _paymentVatService;
        private readonly IUserDetailService _userDetailService;
        private readonly IPartnerService _partnerService;
        private readonly IBalancesheetTitleService _balancesheetTitleServiceService;
        private readonly IBalancesheetSubtitleService _balancesheetSubtitleService;
        private readonly IDocumentSequenceNumberService _documentSequenceNumberService;
        private readonly IBankingService _bankingService;
        private readonly ISessionService _sessionService;
        private readonly IBusinessTaxService _businessTaxService;
        private readonly IMixFinalAccountCorrectionService _mixFinalAccountCorrectionService;

        public MixinOrderController(IMixinOrderService mixinOrderService, IMapper mapper, IMixinOrderLineService mixinOrderLineService,
            IVoteDetailService voteDetailService, IAccountDetailService accountDetailService, IOfficeService officeService, ISabhaService sabhaService,
            ISelectedLanguageService selectedLanguageService, ILanguageService languageService, IVoteAssignmentService voteAssignmentService,
            IVoteAssignmentDetailsService voteAssignmentDetailsService, IGnDivisionService gnDivisionService,
            IPaymentNbtService paymentNbtService, IPaymentVatService paymentVatService, IUserDetailService userDetailService,
            IPartnerService partnerService, IDocumentSequenceNumberService documentSequenceNumberService,
            IBalancesheetTitleService balancesheetTitleServiceService, IBalancesheetSubtitleService balancesheetSubtitleService, IMixinCancelOrderService mixinCancelOrderService,
            IBankingService bankingService, ISessionService sessionService, IBusinessTaxService businessTaxService,
            IMixFinalAccountCorrectionService mixFinalAccountCorrectionService)
        {
            this._mapper = mapper;
            this._mixinOrderService = mixinOrderService;
            _mixinOrderLineService = mixinOrderLineService;
            _voteDetailService = voteDetailService;
            _accountDetailService = accountDetailService;
            _officeService = officeService;
            _selectedLanguageService = selectedLanguageService;
            _languageService = languageService;
            _voteAssignmentService = voteAssignmentService;
            _voteAssignmentDetailsService = voteAssignmentDetailsService;
            _gnDivisionService = gnDivisionService;
            _paymentNbtService = paymentNbtService;
            _paymentVatService = paymentVatService;
            _userDetailService = userDetailService;
            _partnerService = partnerService;
            _documentSequenceNumberService = documentSequenceNumberService;
            _sabhaService = sabhaService;
            _balancesheetTitleServiceService = balancesheetTitleServiceService;
            _balancesheetSubtitleService = balancesheetSubtitleService;
            _mixinCancelOrderService = mixinCancelOrderService;
            _bankingService = bankingService;
            _sessionService = sessionService;
            _businessTaxService = businessTaxService;
            _mixFinalAccountCorrectionService = mixFinalAccountCorrectionService;
        }


        //[HttpGet]
        //[Route("printOrder/{id}")]
        //public HttpResponseMessage PrintOrder(int id)
        //{
        //    Document document = new Document(PageSize.A5, 36, 36, 45, 110);
        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
        //        var imagePathLogo = HttpContext.Current.Server.MapPath("~/assets/layouts/layout/img/po_logo.jpg");
        //        var imagePathRevised = HttpContext.Current.Server.MapPath("~/assets/layouts/layout/img/revised.jpg");
        //        var imagePathCancel = HttpContext.Current.Server.MapPath("~/assets/layouts/layout/img/cancel.jpg");
        //        var imagePathYesNO = HttpContext.Current.Server.MapPath("~/assets/layouts/layout/img/yes_no.jpg");
        //        var purchaseOrder = BizObjectFactory.GetPurchaseOrderBO().GetProxy(id);

        //        var identity = (ClaimsIdentity)User.Identity;
        //        var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
        //        var Loginuser = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

        //        writer.PageEvent = new ITextEvents(purchaseOrder, imagePathLogo, imagePathRevised, imagePathCancel);

        //        document = new PurchaseOrderFacade().GenerateOrder(purchaseOrder, document, imagePathYesNO, Loginuser.FirstName);

        //        document.Close();
        //        byte[] bytes = memoryStream.ToArray();
        //        memoryStream.Close();

        //        var fileName = new StringBuilder();
        //        fileName.Append("MO_");
        //        fileName.Append(purchaseOrder.Number).Append("_");
        //        fileName.Append(DateTime.Now.Month.ToString().PadLeft(2, '0'));
        //        fileName.Append(DateTime.Now.Day.ToString().PadLeft(2, '0'));
        //        fileName.Append(DateTime.Now.Hour.ToString().PadLeft(2, '0'));
        //        fileName.Append(DateTime.Now.Minute.ToString().PadLeft(2, '0'));
        //        fileName.Append(".pdf");

        //        HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
        //        httpResponseMessage.Content = new ByteArrayContent(bytes.ToArray());
        //        httpResponseMessage.Content.Headers.Add("x-filename", fileName.ToString());
        //        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //        httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        //        httpResponseMessage.StatusCode = HttpStatusCode.OK;
        //        return httpResponseMessage;
        //    }
        //}


        //[HttpPost]
        //[Route("printPreview")]
        //public HttpResponseMessage printPreview(PurchaseOrderModel purchaseOrder)
        //{
        //    Document document = new Document(PageSize.A5, 36, 36, 45, 110);
        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
        //        var imagePathLogo = HttpContext.Current.Server.MapPath("~/assets/layouts/layout/img/po_logo.jpg");
        //        var imagePathRevised = HttpContext.Current.Server.MapPath("~/assets/layouts/layout/img/revised.jpg");
        //        var imagePathCancel = HttpContext.Current.Server.MapPath("~/assets/layouts/layout/img/cancel.jpg");
        //        var imagePathYesNO = HttpContext.Current.Server.MapPath("~/assets/layouts/layout/img/yes_no.jpg");
        //        var identity = (ClaimsIdentity)User.Identity;
        //        var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
        //        var Loginuser = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

        //        document = new PurchaseOrderFacade().PrintPreview(purchaseOrder, document, imagePathYesNO, Loginuser.FirstName);

        //        //water mark
        //        BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ITALIC, BaseFont.CP1252, false);
        //        BaseColor bc = new BaseColor(0, 0, 0, 65);
        //        iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 60.5F, iTextSharp.text.Font.ITALIC, bc);

        //        // Dim wfont = New Font(BaseFont.TIMES_ROMAN, 1.0F, BaseFont.COURIER, BaseColor.LIGHT_GRAY)
        //        ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_CENTER, new Phrase("Reference Purpose Only", times), 245.5F, 480.0F, -55);
        //        //End water mark

        //        document.Close();
        //        byte[] bytes = memoryStream.ToArray();
        //        memoryStream.Close();

        //        var fileName = new StringBuilder();
        //        fileName.Append("PO_");
        //        fileName.Append(purchaseOrder.number).Append("_");
        //        fileName.Append(DateTime.Now.Month.ToString().PadLeft(2, '0'));
        //        fileName.Append(DateTime.Now.Day.ToString().PadLeft(2, '0'));
        //        fileName.Append(DateTime.Now.Hour.ToString().PadLeft(2, '0'));
        //        fileName.Append(DateTime.Now.Minute.ToString().PadLeft(2, '0'));
        //        fileName.Append(".pdf");

        //        HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
        //        httpResponseMessage.Content = new ByteArrayContent(bytes.ToArray());
        //        httpResponseMessage.Content.Headers.Add("x-filename", fileName.ToString());
        //        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //        httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        //        httpResponseMessage.StatusCode = HttpStatusCode.OK;
        //        return httpResponseMessage;
        //    }
        //}

        public class custommixinorderclass
        {
            public MixinOrder mixinOrder;
            public string votebalcode;
        }

        [HttpGet]
        [Route("getByPaymentDetailId/{id}")]
        public async Task<IActionResult> getByPaymentDetailId([FromRoute] int id)
        {
            try
            {
                var byPaymentDetailId = await _mixinOrderService.getByPaymentDetailId(id);
                return Ok(byPaymentDetailId);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }



        [HttpGet]
        [Route("getById/{id}")]
        public async Task<ActionResult> getMixinOrderById([FromRoute] int id)
        {
            var mixinOrder = await _mixinOrderService.GetById(id);

            if (mixinOrder == null)
                return NotFound();

            if (mixinOrder.State == Core.Models.Enums.OrderStatus.Cancel_Pending || mixinOrder.State == Core.Models.Enums.OrderStatus.Cancel_Approved || mixinOrder.State == Core.Models.Enums.OrderStatus.Cancel_Disapproved)
            {
                var mixincancelorder = await _mixinCancelOrderService.GetByOrderId(mixinOrder.Id);
                var mixinCancelOrderResource = _mapper.Map<MixinCancelOrder, MixinCancelOrderResource>(mixincancelorder);
                var mixincancelordernew = _mapper.Map<MixinCancelOrderResource, MixinCancelOrder>(mixinCancelOrderResource);

                mixinOrder.MixinCancelOrder = new MixinCancelOrder();
                mixinOrder.MixinCancelOrder = mixincancelordernew;
            }
            return Ok(mixinOrder);
        }

        [HttpGet]
        [Route("getMixinOrderForRepaymentById/{id}")]
        public async Task<ActionResult> getMixinOrderForRepaymentById([FromRoute] int id)
        {

            var mxOrderResource = _mapper.Map<MixinOrder, MixinOrderResource>(await _mixinOrderService.GetMixinOrderForRepaymentById(id));

            foreach(var item in mxOrderResource.MixinOrderLine)
            {
                if (item.VoteDetailId.HasValue)
                {
                    item.VoteDetail = _mapper.Map<VoteDetail, VoteDetailResource>(await _voteDetailService.GetVoteDetailById(item.VoteDetailId.Value));
                }
            }

            return Ok(mxOrderResource);


        }


        [HttpGet]
        [Route("getByIdAndOffice/{id}/{officeid}")]
        public async Task<ActionResult> GetByIdAndOffice([FromRoute] int id, int officeid)
        {
            var mixinOrder = await _mixinOrderService.GetByIdAndOffice(id, officeid);

            if (mixinOrder == null)
                return NotFound();

            //var mixinOrderLine = await _mixinOrderLineService.GetAllForOrderId(mixinOrder.Id);
            //var mixinOrderLineList = mixinOrderLine.ToList();

            //mixinOrder.Partner = await _partnerService.GetById(mixinOrder.PartnerId.Value);

            //if (mixinOrder.CreatedBy != null)
            //    mixinOrder.UserDetail = await _userDetailService.GetUserDetailById(mixinOrder.CreatedBy);

            //if (mixinOrder.CashierId != null)
            //    mixinOrder.Cashier = await _userDetailService.GetUserDetailById(mixinOrder.CashierId.Value);

            //mixinOrder.Office = await _officeService.GetOfficeById(mixinOrder.OfficeId.Value);

            //    for (int i = 0; i < mixinOrderLineList.Count(); i++)
            //    {

            //    if (mixinOrderLineList[i].VoteOrBal == 1 && mixinOrder.BusinessId==0)
            //    {
            //        int voteassignmentID = 0;
            //        var voteAssignmentDetails = await _voteAssignmentDetailsService.GetById(mixinOrderLineList[i].MixinVoteAssignmentDetailId);
            //        var voteassignment = await _voteAssignmentService.GetById(voteAssignmentDetails.VoteAssignmentId);

            //        if (voteAssignmentDetails != null)
            //    {
            //        var voteDetail = await _voteDetailService.GetVoteDetailById(voteassignment.VoteId);
            //        mixinOrderLineList[i].CustomVoteName = voteDetail.Code;
            //    }
            //    }


            //    var paymentNbt = await _paymentNbtService.GetById(mixinOrderLineList[i].PaymentNbtId.Value);
            //            mixinOrderLineList[i].PaymentNbt = paymentNbt;
            //        var paymentVat = await _paymentVatService.GetById(mixinOrderLineList[i].PaymentVatId.Value);
            //            mixinOrderLineList[i].PaymentVat = paymentVat;
            //    }
            //    mixinOrder.MixinOrderLine=mixinOrderLineList;
            if (mixinOrder.State == Core.Models.Enums.OrderStatus.Cancel_Pending || mixinOrder.State == Core.Models.Enums.OrderStatus.Cancel_Approved || mixinOrder.State == Core.Models.Enums.OrderStatus.Cancel_Disapproved)
            {
                var mixincancelorder = await _mixinCancelOrderService.GetByOrderId(mixinOrder.Id);
                var mixinCancelOrderResource = _mapper.Map<MixinCancelOrder, MixinCancelOrderResource>(mixincancelorder);
                var mixincancelordernew = _mapper.Map<MixinCancelOrderResource, MixinCancelOrder>(mixinCancelOrderResource);

                mixinOrder.MixinCancelOrder = new MixinCancelOrder();
                mixinOrder.MixinCancelOrder = mixincancelordernew;
            }
            return Ok(mixinOrder);
        }




        [HttpGet]
        [Route("getOrderByBarcode/{barcode}/{officeid}")]
        public async Task<ActionResult> GetOrderByBarcode([FromRoute] string barcode, int officeid)
        {
            try
            {
                var mixinOrder = await _mixinOrderService.GetByCode(barcode, officeid);

                if (mixinOrder == null)
                    return NotFound();

                //var mixinOrderLine = await _mixinOrderLineService.GetAllForOrderId(mixinOrder.Id);
                //var mixinOrderLineList = mixinOrderLine.ToList();

                //mixinOrder.Partner = await _partnerService.GetById(mixinOrder.PartnerId.Value);

                //if (mixinOrder.CreatedBy != null)
                //    mixinOrder.UserDetail = await _userDetailService.GetUserDetailById(mixinOrder.CreatedBy);

                //if (mixinOrder.CashierId != null)
                //    mixinOrder.Cashier = await _userDetailService.GetUserDetailById(mixinOrder.CashierId.Value);

                //mixinOrder.Office = await _officeService.GetOfficeById(mixinOrder.OfficeId.Value);

                //for (int i = 0; i < mixinOrderLineList.Count(); i++)
                //{

                //    if (mixinOrderLineList[i].VoteOrBal == 1)
                //    {
                //        int voteassignmentID = 0;
                //        var voteAssignmentDetails = await _voteAssignmentDetailsService.GetById(mixinOrderLineList[i].MixinVoteAssignmentDetailId);
                //        var voteassignment = await _voteAssignmentService.GetById(voteAssignmentDetails.VoteAssignmentId);

                //        if (voteAssignmentDetails != null)
                //        {
                //            var voteDetail = await _voteDetailService.GetVoteDetailById(voteassignment.VoteId);
                //            mixinOrderLineList[i].CustomVoteName = voteDetail.Code;
                //        }
                //    }
                //    else
                //    {
                //        var subbalancesheettitle = await _balancesheetSubtitleService.GetBalancesheetSubtitleById(mixinOrderLineList[i].MixinVoteAssignmentDetailId);
                //        var balancesheettitle = await _balancesheetTitleServiceService.GetBalancesheetTitleById(subbalancesheettitle.BalsheetTitleID.Value);
                //        mixinOrderLineList[i].CustomVoteName = balancesheettitle.Code + "-" + subbalancesheettitle.Code;
                //    }
                //    var paymentNbt = await _paymentNbtService.GetById(mixinOrderLineList[i].PaymentNbtId.Value);
                //    mixinOrderLineList[i].PaymentNbt = paymentNbt;
                //    var paymentVat = await _paymentVatService.GetById(mixinOrderLineList[i].PaymentVatId.Value);
                //    mixinOrderLineList[i].PaymentVat = paymentVat;
                //}
                //mixinOrder.MixinOrderLine = mixinOrderLineList;

                return Ok(mixinOrder);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("getOrderByBarcodeOfficeSession/{barcode}/{officeid}/{sessionid}")]
        public async Task<ActionResult> GetOrderByBarcodeOfficeSession([FromRoute] string barcode, int officeid, int sessionid)
        {
            try
            {
                var mixinOrder = await _mixinOrderService.GetOrderByBarcodeOfficeSession(barcode, officeid, sessionid);

                if (mixinOrder == null)
                    return NotFound();

                //var mixinOrderLine = await _mixinOrderLineService.GetAllForOrderId(mixinOrder.Id);
                //var mixinOrderLineList = mixinOrderLine.ToList();

                //mixinOrder.Partner = await _partnerService.GetById(mixinOrder.PartnerId.Value);

                //if (mixinOrder.CreatedBy != null)
                //    mixinOrder.UserDetail = await _userDetailService.GetUserDetailById(mixinOrder.CreatedBy);

                //if (mixinOrder.CashierId != null)
                //    mixinOrder.Cashier = await _userDetailService.GetUserDetailById(mixinOrder.CashierId.Value);

                //mixinOrder.Office = await _officeService.GetOfficeById(mixinOrder.OfficeId.Value);

                //for (int i = 0; i < mixinOrderLineList.Count(); i++)
                //{

                //    if (mixinOrderLineList[i].VoteOrBal == 1)
                //    {
                //        int voteassignmentID = 0;
                //        var voteAssignmentDetails = await _voteAssignmentDetailsService.GetById(mixinOrderLineList[i].MixinVoteAssignmentDetailId);
                //        var voteassignment = await _voteAssignmentService.GetById(voteAssignmentDetails.VoteAssignmentId);

                //        if (voteAssignmentDetails != null)
                //        {
                //            var voteDetail = await _voteDetailService.GetVoteDetailById(voteassignment.VoteId);
                //            mixinOrderLineList[i].CustomVoteName = voteDetail.Code;
                //        }
                //    }
                //    else
                //    {
                //        var subbalancesheettitle = await _balancesheetSubtitleService.GetBalancesheetSubtitleById(mixinOrderLineList[i].MixinVoteAssignmentDetailId);
                //        var balancesheettitle = await _balancesheetTitleServiceService.GetBalancesheetTitleById(subbalancesheettitle.BalsheetTitleID.Value);
                //        mixinOrderLineList[i].CustomVoteName = balancesheettitle.Code + "-" + subbalancesheettitle.Code;
                //    }
                //    var paymentNbt = await _paymentNbtService.GetById(mixinOrderLineList[i].PaymentNbtId.Value);
                //    mixinOrderLineList[i].PaymentNbt = paymentNbt;
                //    var paymentVat = await _paymentVatService.GetById(mixinOrderLineList[i].PaymentVatId.Value);
                //    mixinOrderLineList[i].PaymentVat = paymentVat;
                //}
                //mixinOrder.MixinOrderLine = mixinOrderLineList;

                return Ok(mixinOrder);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //barcode
        //[HttpGet]
        //[Route("generateBarCode/{stringtoEncode}")]
        //public IActionResult GenerateBarCode(string stringtoEncode)
        //{
        //    try { 
        //    BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();
        //    barcode.IncludeLabel= true;
        //    System.Drawing.Image img = barcode.Encode(BarcodeLib.TYPE.CODE39, stringtoEncode , Color.Black,Color.White,250,100);
        //    var data = ConvertImageToBytes(img);
        //    Stream stream = new MemoryStream(data);
        //    return new FileStreamResult(stream, "image/png");
        //    }
        //    catch(Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //private byte[] ConvertImageToBytes(System.Drawing.Image img)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        img.Save(ms,System.Drawing.Imaging.ImageFormat.Png);
        //        return ms.ToArray();
        //    }
        //}

        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult<MixinOrderResource>> GetAll()
        {
            var mixinOrder = await _mixinOrderService.GetAll();
            var mixinOrderResource = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrder);

            if (mixinOrder == null)
                return NotFound();

            return Ok(mixinOrderResource);
        }

        [HttpGet]
        [Route("getAllForOffice/{officeid}")]
        public async Task<ActionResult<MixinOrderResource>> GetAllForOffice(int officeid)
        {
            var mixinOrder = await _mixinOrderService.GetAllForOffice(officeid);
            var mixinOrderResource = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrder);

            if (mixinOrder == null)
                return NotFound();

            return Ok(mixinOrderResource);
        }

        [HttpGet]
        [Route("getAllForOfficeAndStateWithPagination/{officeid}/{state}")]
        public async Task<ActionResult<ResponseModel<MixinOrderResource>>> GetAllForOfficeAndState(int officeid,
            Core.Models.Enums.OrderStatus state, [FromQuery] int pageNumber, [FromQuery] int pageSize,
            [FromQuery] string? filterKeyWord)
        {

            var mixinOrders =
                await _mixinOrderService.GetAllForOfficeAndState(officeid, state, pageNumber, pageSize, filterKeyWord);
            var mixinOrderResources =
                _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrders.list);

            if (mixinOrders.list == null)
                return NotFound();

            if (mixinOrderResources != null && mixinOrderResources.Count() >= 1)
            {
                var accountdetails = await _accountDetailService.GetAllAccountDetails();
                var accountdetailResources =
                    _mapper.Map<IEnumerable<AccountDetail>, IEnumerable<AccountDetailResource>>(accountdetails);

                foreach (var mxorder in mixinOrderResources)
                {
                    //var mixinOrderLine = await _mixinOrderLineService.GetAllForOrderId(mxorder.Id);
                    //mxorder.MixinOrderLine= mixinOrderLine.ToList();
                    if (mxorder.AccountDetailId != 0 && mxorder.AccountDetailId != null)
                    {
                        var accountdetailResource =
                            accountdetailResources.FirstOrDefault(t => t.ID == mxorder.AccountDetailId);
                        mxorder.AccountDetail = accountdetailResource;
                    }
                }
            }

            var model = new ResponseModel<MixinOrderResource>
            {
                totalResult = mixinOrders.totalCount,
                list = mixinOrderResources
            };

            // return Ok(mixinOrderResources);
            return Ok(model);
        }

        [HttpGet]
        [Route("getAllForOfficeAndState/{officeid}/{state}")]
        public async Task<ActionResult<MixinOrderResource>> GetAllForOfficeAndState(int officeid, Core.Models.Enums.OrderStatus state)
        {
            var mixinOrders = await _mixinOrderService.GetAllForOfficeAndState(officeid, state);
            var mixinOrderResources = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrders);

            if (mixinOrders == null)
                return NotFound();

            if (mixinOrderResources != null && mixinOrderResources.Count() >= 1)
            {
                var accountdetails = await _accountDetailService.GetAllAccountDetails();
                var accountdetailResources = _mapper.Map<IEnumerable<AccountDetail>, IEnumerable<AccountDetailResource>>(accountdetails);

                foreach (var mxorder in mixinOrderResources)
                {
                    //var mixinOrderLine = await _mixinOrderLineService.GetAllForOrderId(mxorder.Id);
                    //mxorder.MixinOrderLine= mixinOrderLine.ToList();
                    if (mxorder.AccountDetailId != 0 && mxorder.AccountDetailId != null)
                    {
                        var accountdetailResource = accountdetailResources.FirstOrDefault(t => t.ID == mxorder.AccountDetailId);
                        mxorder.AccountDetail = accountdetailResource;
                    }
                }
            }

            return Ok(mixinOrderResources);
        }

        [HttpGet]
        [Route("getAllForSabhaAndState/{sabhaid}/{state}")]
        public async Task<ActionResult<IEnumerable<MixinOrderResource>>> GetAllForSabhaAndState(int sabhaid, Core.Models.Enums.OrderStatus state)
        {
            var officelist = await _officeService.getAllOfficesForSabhaId(sabhaid);
            var officelistResources = _mapper.Map<IEnumerable<Office>, IEnumerable<OfficeResource>>(officelist);

            List<MixinOrderResource> mixinOrderResourcesForSabha = new List<MixinOrderResource>();
            foreach (var office in officelistResources)
            {
                var mixinOrders = await _mixinOrderService.GetAllForOfficeAndState(office.ID, state);
                var mixinOrderResources = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrders);

                if (mixinOrders == null)
                    return NotFound();

                if (mixinOrderResources != null && mixinOrderResources.Any())
                {
                    var accountdetails = await _accountDetailService.GetAllAccountDetails();
                    var accountdetailResources = _mapper.Map<IEnumerable<AccountDetail>, IEnumerable<AccountDetailResource>>(accountdetails);

                    foreach (var mxorder in mixinOrderResources)
                    {
                        //var mixinOrderLine = await _mixinOrderLineService.GetAllForOrderId(mxorder.Id);
                        //mxorder.MixinOrderLine = mixinOrderLine.ToList();
                        if (mxorder.AccountDetailId != 0 && mxorder.AccountDetailId != null)
                        {
                            var accountdetailResource = accountdetailResources.FirstOrDefault(t => t.ID == mxorder.AccountDetailId);
                            mxorder.AccountDetail = accountdetailResource;
                        }
                    }
                }
                mixinOrderResourcesForSabha.AddRange(mixinOrderResources);
            }
            return Ok(mixinOrderResourcesForSabha);
        }



        [HttpGet]
        [Route("getAllForOfficeAndStateAndDate/{officeid}/{state}/{date}")]
        public async Task<ActionResult<MixinOrderResource>> GetAllForOfficeAndStateAndDate(int officeid, Core.Models.Enums.OrderStatus state, DateTime date)
        {
            var mixinOrders = await _mixinOrderService.GetAllForOfficeAndStateAndDate(officeid, state, date);
            var mixinOrderResources = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrders);

            if (mixinOrders == null)
                return NotFound();

            if (mixinOrderResources != null && mixinOrderResources.Count() >= 1)
            {
                var accountdetails = await _accountDetailService.GetAllAccountDetails();
                var accountdetailResources = _mapper.Map<IEnumerable<AccountDetail>, IEnumerable<AccountDetailResource>>(accountdetails);

                foreach (var mxorder in mixinOrderResources)
                {
                    //var mixinOrderLine = await _mixinOrderLineService.GetAllForOrderId(mxorder.Id);
                    //mxorder.MixinOrderLine= mixinOrderLine.ToList();
                    if (mxorder.AccountDetailId != 0 && mxorder.AccountDetailId != null)
                    {
                        var accountdetailResource = accountdetailResources.FirstOrDefault(t => t.ID == mxorder.AccountDetailId);
                        mxorder.AccountDetail = accountdetailResource;
                    }
                }
            }


            return Ok(mixinOrderResources);
        }

        [HttpGet]
        [Route("getAllForUserAndState/{userid}/{state}")]
        public async Task<ActionResult<MixinOrderResource>> GetAllForUserAndState(int userid, Core.Models.Enums.OrderStatus state)
        {
            var mixinOrders = await _mixinOrderService.GetAllForUserAndState(userid, state);
            var mixinOrderResources = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrders);

            if (mixinOrders == null)
                return NotFound();

            if (mixinOrderResources != null && mixinOrderResources.Count() >= 1)
            {
                var accountdetails = await _accountDetailService.GetAllAccountDetails();
                var accountdetailResources = _mapper.Map<IEnumerable<AccountDetail>, IEnumerable<AccountDetailResource>>(accountdetails);

                foreach (var mxorder in mixinOrderResources)
                {
                    //var mixinOrderLine = await _mixinOrderLineService.GetAllForOrderId(mxorder.Id);
                    //mxorder.MixinOrderLine= mixinOrderLine.ToList();
                    if (mxorder.AccountDetailId != 0 && mxorder.AccountDetailId != null)
                    {
                        var accountdetailResource = accountdetailResources.FirstOrDefault(t => t.ID == mxorder.AccountDetailId);
                        mxorder.AccountDetail = accountdetailResource;
                    }
                }
            }


            return Ok(mixinOrderResources);
        }


        [HttpGet]
        [Route("getPlacedOrdersByUserByCategoryByState/{userid}/{category}/{state}")]
        public async Task<ActionResult<MixinOrderResource>> GetPlacedOrdersByUserByCategoryByState(int userid, int category, Core.Models.Enums.OrderStatus state)
        {
            //var mixinOrders = await _mixinOrderService.GetAllForUserAndState(userid, state);
            var mixinOrders = await _mixinOrderService.GetPlacedOrdersByUserByCategoryByState(userid, category, state);
            var mixinOrderResources = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrders);

            if (mixinOrders == null)
                return NotFound();

            if (mixinOrderResources != null && mixinOrderResources.Count() >= 1)
            {
                var accountdetails = await _accountDetailService.GetAllAccountDetails();
                var accountdetailResources = _mapper.Map<IEnumerable<AccountDetail>, IEnumerable<AccountDetailResource>>(accountdetails);

                foreach (var mxorder in mixinOrderResources)
                {
                    //var mixinOrderLine = await _mixinOrderLineService.GetAllForOrderId(mxorder.Id);
                    //mxorder.MixinOrderLine= mixinOrderLine.ToList();
                    if (mxorder.AccountDetailId != 0 && mxorder.AccountDetailId != null)
                    {
                        var accountdetailResource = accountdetailResources.FirstOrDefault(t => t.ID == mxorder.AccountDetailId);
                        mxorder.AccountDetail = accountdetailResource;
                    }
                }
            }


            return Ok(mixinOrderResources);
        }

        [HttpGet]
        [Route("getAllPlacedAssessmentOrders/{assessmentId}/{pageNumber}")]
        public async Task<ActionResult<ResponseModel<MixinOrderResource>>> GetAllPlacedAssessmentOrders(int assessmentId, int pageNumber)
        {
            (var totalCount, var mixinOrders) = await _mixinOrderService.GetAllPlacedAssessmentOrders(assessmentId, pageNumber);
            var mixinOrderResources = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrders);


            var model = new ResponseModel<MixinOrderResource>
            {
                totalResult = totalCount,
                list = mixinOrderResources
            };

            return Ok(model);
        }

        [HttpGet]
        [Route("getAllPlacedWaterConnectionOrders/{wcId}/{pageNumber}")]
        public async Task<ActionResult<ResponseModel<MixinOrderResource>>> getAllPlacedWaterConnectionOrders(int wcId, int pageNumber)
        {
            (var totalCount, var mixinOrders) = await _mixinOrderService.getAllPlacedWaterConnectionOrders(wcId, pageNumber);
            var mixinOrderResources = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrders);


            var model = new ResponseModel<MixinOrderResource>
            {
                totalResult = totalCount,
                list = mixinOrderResources
            };

            return Ok(model);
        }


        [HttpGet]
        [Route("getAllForSessionAndState/{sessionid}/{state}")]
        public async Task<ActionResult<MixinOrderResource>> GetAllForSessionAndState(int sessionid, Core.Models.Enums.OrderStatus state)
        {
            var mixinOrders = await _mixinOrderService.GetAllForSessionAndState(sessionid, state);
            var mixinOrderResources = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrders);

            if (mixinOrders == null)
                return NotFound();

            if (mixinOrderResources != null && mixinOrderResources.Count() >= 1)
            {
                var accountdetails = await _accountDetailService.GetAllAccountDetails();
                var accountdetailResources = _mapper.Map<IEnumerable<AccountDetail>, IEnumerable<AccountDetailResource>>(accountdetails);

                foreach (var mxorder in mixinOrderResources)
                {
                    //var mixinOrderLine = await _mixinOrderLineService.GetAllForOrderId(mxorder.Id);
                    //mxorder.MixinOrderLine= mixinOrderLine.ToList();
                    if (mxorder.AccountDetailId != 0 && mxorder.AccountDetailId != null)
                    {
                        var accountdetailResource = accountdetailResources.FirstOrDefault(t => t.ID == mxorder.AccountDetailId);
                        mxorder.AccountDetail = accountdetailResource;
                    }
                }
            }



            return Ok(mixinOrderResources);
        }


        [HttpGet]
        [Route("getAllCashBookForOfficeId/{officeid}")]
        public async Task<ActionResult<MixinOrderResource>> GetAllCashBookForOfficeId(int officeid)
        {
            var mixinOrder = await _mixinOrderService.GetAllCashBookForOfficeId(officeid);
            var mixinOrderResource = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrder);

            if (mixinOrder == null)
                return NotFound();

            return Ok(mixinOrderResource);
        }

        [HttpGet]
        [Route("getAllCashBookForOfficeIdBankAccountId/{officeid}/{bankaccountid}")]
        public async Task<ActionResult<MixinOrderResource>> GetAllCashBookForOfficeIdBankAccountId(int officeid, int bankaccountid)
        {
            var mixinOrder = await _mixinOrderService.GetAllCashBookForOfficeIdBankAccountId(officeid, bankaccountid);
            var mixinOrderResource = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrder);

            if (mixinOrder == null)
                return NotFound();

            return Ok(mixinOrderResource);
        }

        [HttpGet]
        [Route("getAllPaidOrdersForOfficeIdCurrentSession/{officeid}")]
        public async Task<ActionResult<MixinOrderResource>> GetAllPaidOrdersForOfficeIdCurrentSession(int officeid)

        {
            int currentsession = 0;

            try
            {
                var sessions = await _sessionService.GetActiveSessionByOfficeAndModule(officeid, "MIX");
                if (sessions != null)
                {
                    var sessionResources = _mapper.Map<Session, CAT20.WebApi.Resources.Control.SessionResource>(sessions);
                    currentsession = sessionResources.Id;
                }
                else
                {
                    currentsession = 0;
                }
            }
            catch (Exception ex)
            {
                currentsession = 0;
            }

            var mixinOrder = await _mixinOrderService.GetAllPaidOrdersForOfficeIdCurrentSession(officeid, currentsession);
            var mixinOrderResource = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrder);

            if (mixinOrder == null)
                return NotFound();

            return Ok(mixinOrderResource);
        }

        [HttpGet]
        [Route("getAllPaidOrdersForOfficeIdBankAccountIdCurrentSession/{officeid}/{bankaccid}")]
        public async Task<ActionResult<MixinOrderResource>> GetAllPaidOrdersForOfficeIdBankAccountIdCurrentSession(int officeid, int bankaccid)
        {
            int currentsession = 0;

            try
            {
                var sessions = await _sessionService.GetActiveSessionByOfficeAndModule(officeid, "MIX");
                var sessionResources = _mapper.Map<Session, CAT20.WebApi.Resources.Control.SessionResource>(sessions);
                currentsession = sessionResources.Id;
            }
            catch (Exception ex)
            {
                currentsession = 0;
            }

            var mixinOrder = await _mixinOrderService.GetAllPaidOrdersForOfficeIdBankAccountIdCurrentSession(officeid, bankaccid, currentsession);
            var mixinOrderResource = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrder);

            if (mixinOrder == null)
                return NotFound();

            return Ok(mixinOrderResource);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save(SaveMixinOrderResource obj)
        {
            try
            {
                if (obj != null && obj.Id == 0 && (obj.MixinOrderLine != null) && (obj.MixinOrderLine.Count > 0) && obj.TotalAmount>0)
                {
                    var mixinOrderToCreate = _mapper.Map<SaveMixinOrderResource, MixinOrder>(obj);
                    var newMixinOrder = await _mixinOrderService.Create(mixinOrderToCreate);

                    if (newMixinOrder != null)
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



        [HttpPost("placeAssessmentOrders")]
        public async Task<IActionResult> PlaceAssessmentOrders(List<SaveAsssessmentOrderResource> objectList)
        {
            try
            {
                if (objectList != null && objectList.Any(obj => obj.Id == 0 && (obj.MixinOrderLine != null) && (obj.MixinOrderLine.Count > 0)))
                {
                    var assessmentsOrders = _mapper.Map<IEnumerable<SaveAsssessmentOrderResource>, IEnumerable<MixinOrder>>(objectList);
                    var newOrders = await _mixinOrderService.PlaceAssessmentOrder(assessmentsOrders.ToList());
                    var newOrdersResource = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<OrderResponseResource>>(newOrders);

                    return Ok(newOrdersResource);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("placeWaterBillOrdersAndProcessPayments")]
        public async Task<IActionResult> PlaceWaterBillOrdersAndProcessPayments(SaveWaterBillOrderResource obj)
        {
            try
            {

                var _token = new HTokenClaim
                {
                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                    IsFinalAccountsEnabled = int.Parse(HttpContext.User.FindFirst("IsFinalAccountsEnabled")!.Value),
                    officeCode = HttpContext.User.FindFirst("officeCode")!.Value,
                    officeTypeID = int.Parse(HttpContext.User.FindFirst("officeTypeID")!.Value),
                };

                if (obj != null && obj.Id == 0 && (obj.MixinOrderLine != null) && (obj.MixinOrderLine.Count > 0))
                {
                    var mixinOrderToCreate = _mapper.Map<SaveWaterBillOrderResource, MixinOrder>(obj);
                    var newMixinOrder = await _mixinOrderService.PlaceWaterBillOrdersAndProcessPayments(mixinOrderToCreate,_token);

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

        [HttpPost("placeVoteSurchargeOrderAndProcessPayments")]
        public async Task<IActionResult> PlaceVoteSurchargeOrderAndProcessPayments(SaveSurchargeOrderResource obj)
        {
            try
            {

                    var _token = new HTokenClaim
                    {
                        userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                        sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                        officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                        IsFinalAccountsEnabled = int.Parse(HttpContext.User.FindFirst("IsFinalAccountsEnabled")!.Value),
                        officeCode = HttpContext.User.FindFirst("officeCode")!.Value,
                        officeTypeID = int.Parse(HttpContext.User.FindFirst("officeTypeID")!.Value),
                    };


                    if (obj != null && obj.Id == 0 && (obj.MixinOrderLine != null) && (obj.MixinOrderLine.Count > 0))
                {
                     var surchargeOrderToCreate = _mapper.Map<SaveSurchargeOrderResource, MixinOrder>(obj);

                     var result = await _mixinOrderService.PlaceVoteSurchargeOrderAndProcessPayments(surchargeOrderToCreate,_token);
                     var resutResource = _mapper.Map<MixinOrder, MixinOrderResource>(result.Item3);


                    if (result.Item1)
                    {
                        return Ok(new ApiResponseModel<MixinOrderResource>
                        {
                            Status = 200,
                            Message = result.Item2!,
                            Data = resutResource,
                        });

                    }


                    else if (!result.Item1 && result.Item2 != null)
                    {
                        return Ok(new ApiResponseModel<object>
                        {
                            Status = 400,
                            Message = result.Item2!,
                        });
                    }


                    else
                    {
                        return BadRequest(new ApiResponseModel<object>
                        {
                            Status = 400,
                            Message = "Internal Server Error"
                        });
                    }

                }
                else
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Status = 400,
                        Message = "Internal Server Error"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        [Route("paid/{id}/{cashierid}")]
        public async Task<IActionResult> Paid(int id, int cashierid)
        {
            try
            {
                var _token = new HTokenClaim
                {
                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                    IsFinalAccountsEnabled = int.Parse(HttpContext.User.FindFirst("IsFinalAccountsEnabled")!.Value),
                    officeCode = HttpContext.User.FindFirst("officeCode")!.Value,
                    officeTypeID = int.Parse(HttpContext.User.FindFirst("officeTypeID")!.Value),
                };


                if (id != 0 && cashierid != 0)
                {
                    var result = await _mixinOrderService.ProcessPayment(id, cashierid, _token);

                    if (result.Item1)
                    {
                        return Ok(new ApiResponseModel<object>
                        {
                            Status = 200,
                            Message = result.Item2!,
                        });

                    }


                    else if (!result.Item1 && result.Item2 != null)
                    {
                        return Ok(new ApiResponseModel<object>
                        {
                            Status = 400,
                            Message = result.Item2!,
                        });
                    }


                    else
                    {
                        return BadRequest(new ApiResponseModel<object>
                        {
                            Status = 400,
                            Message = "Internal Server Error"
                        });
                    }
                }

                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost]
        [Route("approveCancelOrder")]
        public async Task<IActionResult> ApproveCancelOrder(MixinCancelOrderResource mixincancelorder)
        {

            try
            {


                var _token = new HTokenClaim
                {
                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                    IsFinalAccountsEnabled = int.Parse(HttpContext.User.FindFirst("IsFinalAccountsEnabled")!.Value),
                    officeCode = HttpContext.User.FindFirst("officeCode")!.Value,
                    officeTypeID = int.Parse(HttpContext.User.FindFirst("officeTypeID")!.Value),
                };

                //if (mixincancelorder.MixinOrderId == 0)
                //    return BadRequest();

                //var mixinOrder = await _mixinOrderService.GetById(mixincancelorder.MixinOrderId);
                //var mixinCancelOrder = await _mixinCancelOrderService.GetById(mixincancelorder.Id);

                //if (mixinOrder == null || mixinCancelOrder == null)
                //    return NotFound();

                //if (mixinOrder.State == Core.Models.Enums.OrderStatus.Cancel_Pending)
                //{
                //   
                //    await _mixinCancelOrderService.Update(mixinCancelOrder, mixinCancelOrderToCreate);

                //    await _mixinOrderService.ApproveCancelOrder(mixinOrder, mixincancelorder.ApprovedBy.Value);
                //    return Ok(mixinCancelOrderToCreate);
                //}
                //else
                //{
                //    return BadRequest("The Order is not in Pending Cancel state.");
                //}  
                var mixinCancelOrder = _mapper.Map<MixinCancelOrderResource, MixinCancelOrder>(mixincancelorder);

                var result = await _mixinOrderService.ApproveCancelOrder(mixinCancelOrder, _token);

                if (result.Item1)
                {
                    return Ok(new ApiResponseModel<object>
                    {
                        Status = 200,
                        Message = result.Item2!,
                    });

                }
                else if (!result.Item1 && result.Item2 != null)
                {
                    return Ok(new ApiResponseModel<object>
                    {
                        Status = 400,
                        Message = result.Item2!
                    });
                }
                else
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Status = 400,
                        Message = "Internal Server Error"
                    });
                }


            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }


        }


        [HttpPost]
        [Route("disapproveCancelOrder")]
        public async Task<IActionResult> DisapproveCancelOrder(MixinCancelOrderResource mixincancelorder)
        {
            if (mixincancelorder.MixinOrderId == 0)
                return BadRequest();

            var mixinOrder = await _mixinOrderService.GetById(mixincancelorder.MixinOrderId);
            var mixinCancelOrder = await _mixinCancelOrderService.GetById(mixincancelorder.Id);

            if (mixinOrder == null)
                return NotFound();

            if (mixinOrder.State == Core.Models.Enums.OrderStatus.Cancel_Pending)
            {
                var mixinCancelOrderToCreate = _mapper.Map<MixinCancelOrderResource, MixinCancelOrder>(mixincancelorder);
                await _mixinCancelOrderService.Update(mixinCancelOrder, mixinCancelOrderToCreate);

                await _mixinOrderService.DisapproveCancelOrder(mixinOrder, mixincancelorder.ApprovedBy.Value);
                return Ok(mixinCancelOrderToCreate);
            }
            else
            {
                return BadRequest("The Order is not in Pending Cancel state.");
            }
        }


        [HttpPost]
        [Route("approveTradeLicense")]
        public async Task<IActionResult> ApproveTradeLicense(MixinOrderResource objmixinorder)
        {
            if (objmixinorder.Id == 0)
                return BadRequest();

            var mixinOrder = await _mixinOrderService.GetById(objmixinorder.Id);

            if (mixinOrder == null)
                return NotFound();

            if (mixinOrder.TradeLicenseStatus == Core.Models.Enums.TradeLicenseStatus.Pending_Approval)
            {
                await _mixinOrderService.ApproveTradeLicense(mixinOrder, mixinOrder.OfficeId.Value);
                return Ok(objmixinorder);
            }
            else
            {
                return BadRequest("The Order is not in Pending Cancel state.");
            }
        }


        [HttpPost]
        [Route("disapproveTradeLicense")]
        public async Task<IActionResult> DisapproveTradeLicense(MixinOrderResource objmixinorder)
        {
            if (objmixinorder.Id == 0)
                return BadRequest();

            var mixinOrder = await _mixinOrderService.GetById(objmixinorder.Id);

            if (mixinOrder == null)
                return NotFound();

            if (mixinOrder.TradeLicenseStatus == Core.Models.Enums.TradeLicenseStatus.Pending_Approval)
            {
                await _mixinOrderService.DiapproveTradeLicense(mixinOrder, mixinOrder.OfficeId.Value);
                return Ok(objmixinorder);
            }
            else
            {
                return BadRequest("The Order is not in Pending Cancel state.");
            }
        }

        //[HttpPost]
        //[Route("approveCancelOrder/{id}/{officerid}")]
        //public async Task<IActionResult> ApproveCancelOrder(int id, int officerid)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var mixinOrder = await _mixinOrderService.GetById(id);

        //    if (mixinOrder == null)
        //        return NotFound();

        //    if (mixinOrder.State == Core.Models.Enums.OrderStatus.Cancel_Pending)
        //    {
        //        await _mixinOrderService.ApproveCancelOrder(mixinOrder, officerid);
        //        return NoContent();
        //    }
        //    else
        //    {
        //        return BadRequest("The Order is not in Pending state.");
        //    }
        //}

        //[HttpPost]
        //[Route("disapproveCancelOrder/{id}/{officerid}")]
        //public async Task<IActionResult> DisapproveCancelOrder(int id, int officerid)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var mixinOrder = await _mixinOrderService.GetById(id);

        //    if (mixinOrder == null)
        //        return NotFound();

        //    if (mixinOrder.State == Core.Models.Enums.OrderStatus.Cancel_Pending)
        //    {
        //        await _mixinOrderService.DisapproveCancelOrder(mixinOrder, officerid);
        //        return NoContent();
        //    }
        //    else
        //    {
        //        return BadRequest("The Order is not in Pending state.");
        //    }
        //}

        [HttpPost]
        [Route("cancel")]
        public async Task<IActionResult> CancelOrder(MixinCancelOrderResource mixincancelorder)
        {
            try
            {


                if (mixincancelorder.MixinOrderId == 0)
                    return BadRequest();

                var mixinOrder = await _mixinOrderService.GetById(mixincancelorder.MixinOrderId);

                if (mixinOrder == null)
                    return NotFound();



                if (mixinOrder.State == Core.Models.Enums.OrderStatus.Paid)
                {
                    var mixinCancelOrderToCreate = _mapper.Map<MixinCancelOrderResource, MixinCancelOrder>(mixincancelorder);
                    await _mixinCancelOrderService.Create(mixinCancelOrderToCreate);

                    var result = await _mixinOrderService.CancelOrder(mixinOrder, mixincancelorder.CreatedBy);

                    if (result.Item1)
                    {
                        return Ok(new ApiResponseModel<object>
                        {
                            Status = 200,
                            Message = result.Item2!,
                        });

                    }
                    else if (!result.Item1 && result.Item2 != null)
                    {
                        return Ok(new ApiResponseModel<object>
                        {
                            Status = 400,
                            Message = result.Item2!
                        });
                    }
                    else
                    {
                        return BadRequest(new ApiResponseModel<object>
                        {
                            Status = 400,
                            Message = "Internal Server Error"
                        });
                    }
                }
                else
                {
                    return BadRequest("The Order is not in Paid state.");
                }
            }
            catch (Exception ex) { 
               return BadRequest(ex.Message);
            }
        }




        [HttpPost]
        [Route("delete/{id}/{officerid}")]
        public async Task<IActionResult> DeleteOrder(int id, int officerid)
        {
            if (id == 0)
                return BadRequest();

            var mixinOrder = await _mixinOrderService.GetById(id);

            if (mixinOrder == null)
                return NotFound();

            if (mixinOrder.State == Core.Models.Enums.OrderStatus.Draft)
            {
                if (await _mixinOrderService.DeleteOrder(mixinOrder, officerid))
                {
                    return Ok();
                }

                return BadRequest();

                //if (mixinOrder.BusinessId != 0)
                //{
                //    try
                //    {
                //        var businessTaxToBeUpdate = await _businessTaxService.GetById(mixinOrder.BusinessTaxId.Value);
                //        var businestaxline = businessTaxToBeUpdate;
                //        businestaxline.TaxState = TaxStatus.Draft;

                //        await _businessTaxService.Update(businessTaxToBeUpdate, businestaxline);
                //    }
                //    catch (Exception ex)
                //    {
                //    }
                //}
                //return NoContent();
            }
            else
            {
                return BadRequest("The Order is not in draft state.");
            }
        }

        [HttpPost]
        [Route("UpdateState")]
        public async Task<IActionResult> updateState(SaveMixinOrderResource obj)
        {
            if (obj.Id == 0)
                return BadRequest();

            var mixinOrder = await _mixinOrderService.GetById(obj.Id);

            if (mixinOrder == null)
                return NotFound();

            await _mixinOrderService.updateState(mixinOrder, obj.State);
            return NoContent();

        }

        //[HttpGet]
        //[Route("getAllForVoteId/{voteId}")]
        //public async Task<ActionResult<IEnumerable<MixinOrder>>> GetAllForVoteId([FromRoute] int voteId)
        //{
        //    var mixinOrders = await _mixinOrderService.GetAllForVoteId(voteId);
        //    var mixinOrderResources = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrders);

        //    return Ok(mixinOrderResources);
        //}

        [HttpPost]
        [Route("banking")]
        public async Task<ActionResult<Banking>> Banking(List<Banking> model)
        {
            try
            {
                if (model.Count() > 0)
                {
                    List<Banking> savedbankedList = new List<Banking>();

                    try
                    {
                        foreach (var item in model)
                        {
                            Banking savedbankeditem = new Banking();
                            item.Id = null;
                            item.CreatedAt = DateTime.Now;
                            item.BankedDate.ToShortDateString();
                            savedbankeditem = await _bankingService.Create(item);
                            savedbankedList.Add(savedbankeditem);
                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message.ToString());
                    }
                    return Ok(savedbankedList);
                }
                return BadRequest();
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message.ToString());
            }
        }


        [HttpGet]
        [Route("getLastBankingDateForOfficeId/{officeid}")]
        public async Task<ActionResult> GetLastBankingDateForOfficeId([FromRoute] int officeid)
        {
            try
            {
                var banking = await _bankingService.GetLastBankingDateForOfficeId(officeid);
                return Ok(banking);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }



        [HttpPost]
        [Route("updateTradeLicenseStatus/{id}/{status}")]
        public async Task<IActionResult> UpdateTradeLicenseStatus(int id, int userid, TradeLicenseStatus status)
        {
            if (id == 0)
                return BadRequest();

            var mixinOrder = await _mixinOrderService.GetById(id);
            await _mixinOrderService.UpdateTradeLicenseStatus(mixinOrder, status);
            var updatedmixinOrder = await _mixinOrderService.GetById(id);
            return Ok(updatedmixinOrder);
        }



        [HttpGet]
        [Route("getAllTradeLicensesForOfficeAndState/{officeid}/{state}")]
        public async Task<ActionResult<MixinOrderResource>> GetAllTradeLicensesForOfficeAndState(int officeid, Core.Models.Enums.TradeLicenseStatus state)
        {
            var mixinOrder = await _mixinOrderService.GetAllTradeLicensesForOfficeAndState(officeid, state);
            var mixinOrderResource = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrder);
            foreach (var mxorder in mixinOrderResource)
            {
                //var mixinOrderLine = await _mixinOrderLineService.GetAllForOrderId(mxorder.Id);
                //mxorder.MixinOrderLine = mixinOrderLine.ToList();

                if (mxorder.AccountDetailId != 0 && mxorder.AccountDetailId != null)
                {
                    var accountdetail = await _accountDetailService.GetAccountDetailById(mxorder.AccountDetailId.Value);
                    var accountdetailResource = _mapper.Map<AccountDetail, AccountDetailResource>(accountdetail);
                    mxorder.AccountDetail = accountdetailResource;
                }
            }
            if (mixinOrder == null)
                return NotFound();

            return Ok(mixinOrderResource);
        }

        [HttpGet]
        [Route("getAllTradeLicensesForOfficeAndStateAndTaxType/{officeid}/{state}/{taxtypeid}")]
        public async Task<ActionResult<MixinOrderResource>> GetAllTradeLicensesForOfficeAndStateAndTaxType(int officeid, Core.Models.Enums.TradeLicenseStatus state, int taxtypeid)
        {
            var mixinOrder = await _mixinOrderService.GetAllTradeLicensesForOfficeAndStateAndTaxType(officeid, state, taxtypeid);
            var mixinOrderResource = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrder);
            foreach (var mxorder in mixinOrderResource)
            {
                //var mixinOrderLine = await _mixinOrderLineService.GetAllForOrderId(mxorder.Id);
                //mxorder.MixinOrderLine = mixinOrderLine.ToList();

                if (mxorder.AccountDetailId != 0 && mxorder.AccountDetailId != null)
                {
                    var accountdetail = await _accountDetailService.GetAccountDetailById(mxorder.AccountDetailId.Value);
                    var accountdetailResource = _mapper.Map<AccountDetail, AccountDetailResource>(accountdetail);
                    mxorder.AccountDetail = accountdetailResource;
                }
            }
            if (mixinOrder == null)
                return NotFound();

            return Ok(mixinOrderResource);
        }

        [HttpGet]
        [Route("getAllTradeTaxOrdersForUserAndState/{userid}/{state}")]
        public async Task<ActionResult<MixinOrderResource>> GetAllTradeTaxOrdersForUserAndState(int userid, Core.Models.Enums.OrderStatus state)
        {
            var mixinOrders = await _mixinOrderService.GetAllTradeTaxOrdersForUserAndState(userid, state);
            var mixinOrderResources = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrders);

            foreach (var mixinorderresource in mixinOrderResources)
            {
                if (mixinorderresource.AccountDetailId != 0 && mixinorderresource.AccountDetailId != null)
                {
                    var accountdetail = await _accountDetailService.GetAccountDetailById(mixinorderresource.AccountDetailId.Value);
                    var accountdetailResource = _mapper.Map<AccountDetail, AccountDetailResource>(accountdetail);
                    mixinorderresource.AccountDetail = accountdetailResource;
                }
            }

            if (mixinOrders == null)
                return NotFound();

            return Ok(mixinOrderResources);
        }


        [HttpGet]
        [Route("getAllTotalAmountsByAppCategoryCurrentSessionForOffice/{officeid}")]
        public async Task<IEnumerable<Object>> GetAllTotalAmountsByAppCategoryCurrentSessionForOffice(int officeId)
        {
            var currentsession = await _sessionService.GetActiveSessionByOfficeAndModule(officeId, "MIX");
            if (currentsession != null && currentsession.Id > 0)
            {
                var appcatwisetotalincome = await _mixinOrderService.GetAllTotalAmountsByAppCategoryForSession(currentsession);

                return appcatwisetotalincome;
            }
            return null;
        }

        [HttpGet]
        [Route("getAllTotalAmountsByAppCategoryForSessionId/{sessionid}")]
        public async Task<IEnumerable<Object>> GetAllTotalAmountsByAppCategoryForSessionId(int sessionid)
        {
            var currentsession = await _sessionService.GetById(sessionid);
            if (currentsession != null && currentsession.Id > 0)
            {
                var appcatwisetotalincome = await _mixinOrderService.GetAllTotalAmountsByAppCategoryForSession(currentsession);
                return appcatwisetotalincome;
            }
            return null;
        }


        [HttpGet]
        [Route("getAllOnlinePaymentTotalAmountsByAppCategoryCurrentSessionForOffice/{officeid}")]
        public async Task<IEnumerable<Object>> GetAllOnlinePaymentTotalAmountsByAppCategoryCurrentSessionForOffice(int officeId)
        {
            var currentsession = await _sessionService.GetActiveSessionByOfficeAndModule(officeId, "MIX");
            if (currentsession != null && currentsession.Id > 0)
            {
                var appcatwisetotalincome = await _mixinOrderService.GetAllOnlinePaymentTotalAmountsByAppCategoryForSession(currentsession);

                return appcatwisetotalincome;
            }
            return null;
        }

        [HttpGet]
        [Route("getAllOnlinePaymentTotalAmountsByAppCategoryForSessionId/{sessionid}")]
        public async Task<IEnumerable<Object>> GetAllOnlinePaymentTotalAmountsByAppCategoryForSessionId(int sessionid)
        {
            var currentsession = await _sessionService.GetById(sessionid);
            if (currentsession != null && currentsession.Id > 0)
            {
                var appcatwisetotalincome = await _mixinOrderService.GetAllOnlinePaymentTotalAmountsByAppCategoryForSession(currentsession);
                return appcatwisetotalincome;
            }
            return null;
        }

        [HttpGet]
        [Route("getAllTotalAmountsByAppCategoryWiseLast10SessionsForOffice/{officeid}")]
        public async Task<IEnumerable<Object>> GetAllTotalAmountsByAppCategoryWiseLast30SessionsForOffice(int officeId)
        {
            var Lastsessions = await _sessionService.GetLast10SessionsForOffice(officeId);
            if (Lastsessions != null && Lastsessions.ToList().Count() > 0)
            {
                List<Object> appcatwisetotalincomeforlast30sessions = new List<object>();
                foreach (var session in Lastsessions)
                {
                    if (session != null && session.Id > 0)
                    {
                        var appcatwisetotalincome = await _mixinOrderService.GetAllTotalAmountsByAppCategoryForSession(session);
                        appcatwisetotalincomeforlast30sessions.AddRange(appcatwisetotalincome);
                    }
                }
                return appcatwisetotalincomeforlast30sessions;
            }
            return null;
        }

        //[HttpGet]
        //[Route("getAllSarapRecieptsForDateAndState/{date}/{state}")]
        //public async Task<ActionResult<MixinOrderResource>> GetAllSarapRecieptsForDateAndState(DateTime date, Core.Models.Enums.OrderStatus state)
        //{
        //    var mixinOrder = await _mixinOrderService.GetAllForSessionAndState(sessionid, state);
        //    var mixinOrderResource = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrder);

        //    if (mixinOrder == null)
        //        return NotFound();

        //    return Ok(mixinOrderResource);
        //}



        //--------------[placeShopRentalOrder]----------------------------------------------
        //Note : modified : 2024/04/03
        [HttpPost("placeShopRentalMixinOrder")]
        public async Task<IActionResult> PlaceShopRentalOrder(SaveShoprentalOrderResource newMixinShopRentalOrder)
        {
            try
            {
                if (newMixinShopRentalOrder != null && newMixinShopRentalOrder.Id == 0 && (newMixinShopRentalOrder.MixinOrderLine != null) && (newMixinShopRentalOrder.MixinOrderLine.Count > 0))
                {
                    var mixinOrderToCreate = _mapper.Map<SaveShoprentalOrderResource, MixinOrder>(newMixinShopRentalOrder);
                    var newMixinOrder = await _mixinOrderService.PlaceShopRentalOrder(mixinOrderToCreate);

                    if (newMixinOrder != null)
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
        //--------------[placeShopRentalOrder]----------------------------------------------

        [HttpGet]
        [Route("getAllPaidPostedOredersByShopId/{orderstate}/{shopId}")]
        public async Task<ActionResult<MixinOrderResource>> GetAllPaidPostedOredersByShopId(OrderStatus orderstate, int shopId)
        {
            var mixinOrders = await _mixinOrderService.GetPaidPostedOrdersByShopId(orderstate, shopId);
            var mixinOrderResources = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrders);

            if (mixinOrders == null)
                return NotFound();

            if (mixinOrderResources != null && mixinOrderResources.Count() >= 1)
            {
                var accountdetails = await _accountDetailService.GetAllAccountDetails();
                var accountdetailResources = _mapper.Map<IEnumerable<AccountDetail>, IEnumerable<AccountDetailResource>>(accountdetails);

                foreach (var mxorder in mixinOrderResources)
                {
                    if (mxorder.AccountDetailId != 0 && mxorder.AccountDetailId != null)
                    {
                        var accountdetailResource = accountdetailResources.FirstOrDefault(t => t.ID == mxorder.AccountDetailId);
                        mxorder.AccountDetail = accountdetailResource;
                    }
                }
            }

            return Ok(mixinOrderResources);
        }
        //-----------



        [HttpGet("searchMixOrderByKeyword")]
    public async Task<ActionResult<ResponseModel<MixinOrderResource>>> SearchOrderByKeyword([FromQuery] List<int?> officeIds , [FromQuery] int pageNo, [FromQuery] int pageSize, [FromQuery] string keyword)
    {
        (var totalCount, var mixinOrders) = await _mixinOrderService.SearchOrderByKeyword(officeIds, OrderStatus.Paid,pageNo,pageSize,keyword);
        var mixinOrderResources = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrders);

            var model = new ResponseModel<MixinOrderResource>
        {
            totalResult = totalCount,
            list = mixinOrderResources
        };

        return Ok(model);

     }

        [HttpGet("searchMixOrderByKeywordForAdjustment")]
        public async Task<ActionResult<ResponseModel<MixinOrderResource>>> SearchOrderForAdjesment([FromQuery] List<int?> officeIds, [FromQuery] int pageNo, [FromQuery] int pageSize, [FromQuery] string keyword)
        {
            (var totalCount, var mixinOrders) = await _mixinOrderService.SearchOrderForAdjesment(officeIds, OrderStatus.Paid, pageNo, pageSize, keyword);
            var mixinOrderResources = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrders);

            var model = new ResponseModel<MixinOrderResource>
            {
                totalResult = totalCount,
                list = mixinOrderResources
            };

            return Ok(model);

        }


        //---
        [HttpPost("saveMixinOrderJournalEntry")]
        public async Task<IActionResult> SaveMixinOrderJournalEntry(SaveMixinOrderResource obj)
        {
            try
            {
                if (obj != null && obj.Id == 0 && (obj.MixinOrderLine != null) && (obj.MixinOrderLine.Count > 0))
                {
                    var mixinOrderToCreate = _mapper.Map<SaveMixinOrderResource, MixinOrder>(obj);
                    var newMixinOrder = await _mixinOrderService.CreateMixinOrderJournalEntry(mixinOrderToCreate);

                    if (newMixinOrder != null)
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
        //---

        //---
        [HttpGet]
        [Route("getAllMixinOrderJournalEntryOrdersByOrderStatusAndOfficeId/{orderstate}/{officeId}")]
        public async Task<ActionResult<MixinOrderResource>> GetAllMixinOrderJournalEntryOrdersByOrderStatusAndOfficeId(OrderStatus orderstate, int officeId)
        {
            var mixinOrder = await _mixinOrderService.GetAllMixinOrderJournalEntryOrdersByOrderStatusAndOfficeId(orderstate, officeId);
            var mixinOrderResource = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrder);

            if (mixinOrder == null)
                return NotFound();

            return Ok(mixinOrderResource);
        }
        //---

        //---
        [HttpGet]
        [Route("getMixinOrderJournalEntryOrderId/{mxId}")]
        public async Task<ActionResult> GetMixinOrderJournalEntryOrderId([FromRoute] int mxId)
        {

            var mxOrderResource = _mapper.Map<MixinOrder, MixinOrderResource>(await _mixinOrderService.GetMixinOrderJournalEntryOrderId(mxId));

            foreach (var item in mxOrderResource.MixinOrderLine)
            {
                if (item.VoteDetailId.HasValue)
                {
                    item.VoteDetail = _mapper.Map<VoteDetail, VoteDetailResource>(await _voteDetailService.GetVoteDetailById(item.VoteDetailId.Value));
                }
            }

            return Ok(mxOrderResource);
        }
        //---

        [HttpPatch]
        [Route("rejectMixinOrderJournalEntryOrderById")]
        public async Task<IActionResult> RejecttMixinOrderJournalEntryOrderById([FromBody] int mxId)
        {
            var _token = new HTokenClaim
            {
                userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                IsFinalAccountsEnabled = int.Parse(HttpContext.User.FindFirst("IsFinalAccountsEnabled")!.Value),
                officeCode = HttpContext.User.FindFirst("officeCode")!.Value,
                officeTypeID = int.Parse(HttpContext.User.FindFirst("officeTypeID")!.Value),
            };


            var mixinOrder = await _mixinOrderService.GetById(mxId);

            if (mixinOrder != null)
            {
                await _mixinOrderService.RejecttMixinOrderJournalEntryOrder(mixinOrder, _token);

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        //--

        //--
        //[HttpPatch]
        //[Route("approveMixinOrderJournalEntryOrderByIds")]
        //public async Task<IActionResult> ApproveMixinOrderJournalEntryOrderByIds([FromBody] List<int> mixIds)
        //{
        //    var _token = new HTokenClaim
        //    {
        //        userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
        //        sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
        //        officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
        //        IsFinalAccountsEnabled = int.Parse(HttpContext.User.FindFirst("IsFinalAccountsEnabled")!.Value),
        //        officeCode = HttpContext.User.FindFirst("officeCode")!.Value,
        //        officeTypeID = int.Parse(HttpContext.User.FindFirst("officeTypeID")!.Value),
        //    };

        //    if (mixIds != null || mixIds.Count != 0)
        //    {
        //        foreach (var mixId in mixIds)
        //        {
        //            var mixinOrder = await _mixinOrderService.GetById(mixId);

        //            if (mixinOrder != null)
        //            {
        //                await _mixinOrderService.ApproveMixinOrderJournalEntryOrder(mixinOrder, _token);
        //            }
        //            else
        //            {
        //                return NotFound();
        //            }
        //        }

        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest("The mixIds list is empty or null.");
        //    }
        //}

        [HttpPatch]
        [Route("approveMixinOrderJournalEntryOrderByIds")]
        public async Task<IActionResult> ApproveMixinOrderJournalEntryOrderByIds([FromBody] List<int> mixIds)
        {
            try
            {
                var _token = new HTokenClaim
                {
                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                    IsFinalAccountsEnabled = int.Parse(HttpContext.User.FindFirst("IsFinalAccountsEnabled")!.Value),
                    officeCode = HttpContext.User.FindFirst("officeCode")!.Value,
                    officeTypeID = int.Parse(HttpContext.User.FindFirst("officeTypeID")!.Value),
                };

                foreach (var mixId in mixIds)
                {
                    var mixinOrder = await _mixinOrderService.GetById(mixId);

                    if (mixinOrder.Id != 0 && _token.userId != 0)
                    {
                        var result = await _mixinOrderService.ProcessPayment(mixinOrder.Id, _token.userId, _token);

                        if (result.Item1)
                        {
                            return Ok(new ApiResponseModel<object>
                            {
                                Status = 200,
                                Message = result.Item2!,
                            });

                        }

                        else if (!result.Item1 && result.Item2 != null)
                        {
                            return Ok(new ApiResponseModel<object>
                            {
                                Status = 400,
                                Message = result.Item2!,
                            });
                        }
                        else
                        {
                            return BadRequest(new ApiResponseModel<object>
                            {
                                Status = 400,
                                Message = "Internal Server Error"
                            });
                        }
                    }
                }

                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost("alignLedgerAccountAndCashBooksForOlderReceipts/{sabhaId}")]
        public async Task<IActionResult> AlignLedgerAccountAndCashBooksForOlderReceipts(int sabhaId)
        {
            try
            {
                var _token = new HTokenClaim
                {
                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                    IsFinalAccountsEnabled = int.Parse(HttpContext.User.FindFirst("IsFinalAccountsEnabled")!.Value),
                    officeCode = HttpContext.User.FindFirst("officeCode")!.Value,
                    officeTypeID = int.Parse(HttpContext.User.FindFirst("officeTypeID")!.Value),
                };

                var result  = await _mixFinalAccountCorrectionService.AlignLedgerAccountAndCashBooksForOlderReceipts(sabhaId, _token);

                if (result.Item1)
                {
                    return Ok(new ApiResponseModel<object>
                    {
                        Status = 200,
                        Message = result.Item2!,
                    });

                }


                else if (!result.Item1 && result.Item2 != null)
                {
                    return Ok(new ApiResponseModel<object>
                    {
                        Status = 400,
                        Message = result.Item2!,
                    });
                }


                else
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Status = 400,
                        Message = "Internal Server Error"
                    });
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        [HttpGet]
        [Route("getAllForEmployeeId/{empId}")]
        public async Task<ActionResult<MixinOrderResource>> GetAllForEmployeeId(int empId)
        {
            var mixinOrders = await _mixinOrderService.GetAllForEmployeeId(empId);
            var mixinOrderResource = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinOrderResource>>(mixinOrders);

            if (mixinOrders == null)
                return NotFound();

            return Ok(mixinOrderResource);
        }
    }
}
