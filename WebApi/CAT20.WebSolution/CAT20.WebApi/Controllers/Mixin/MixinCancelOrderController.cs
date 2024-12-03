using AutoMapper;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Services.Vote;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.Mixin;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.Api.Controllers
{
    [Route("api/mixin/mixinOrder")]
    [ApiController]
    public class MixinCancelOrderController : BaseController
    {
        private readonly IMixinCancelOrderService _mixinOrderService;
        private readonly IMixinOrderLineService _mixinOrderLineService;
        private readonly IVoteDetailService _voteDetailService;
        private readonly IAccountDetailService _accountDetailService;
        private readonly IOfficeService _officeService;
        private readonly ISelectedLanguageService _selectedLanguageService;
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public MixinCancelOrderController(IMixinCancelOrderService mixinOrderService, IMapper mapper, IMixinOrderLineService mixinOrderLineService,
            IVoteDetailService voteDetailService, IAccountDetailService accountDetailService, IOfficeService officeService, ISabhaService sabhaService,
            ISelectedLanguageService selectedLanguageService, ILanguageService languageService)
        {
            this._mapper = mapper;
            this._mixinOrderService = mixinOrderService;
            _mixinOrderLineService = mixinOrderLineService;
            _voteDetailService = voteDetailService;
            _accountDetailService = accountDetailService;
            _officeService = officeService;
            _selectedLanguageService = selectedLanguageService;
            _languageService = languageService;
        }

        [HttpGet]
        [Route("getAllForSabhaId/{sabhaid}")]
        //public async Task<ActionResult> GetAllForSabhaId(int sabhaid)
        //{
        //    var mixinOrders = await _mixinOrderService.GetAllForSabhaId(sabhaid);
        //    var mixinCancelOrderResources = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinCancelOrderResource>>(mixinOrders);

        //    if (mixinCancelOrderResources != null && mixinCancelOrderResources.Count() > 0)
        //    {
        //        var custommixinOrderList = new List<dynamic>();

        //        for (int i = 0; i < mixinCancelOrderResources.Count(); i++)
        //        {
        //            var mixinCancelOrderResource = mixinCancelOrderResources.ToList()[i];

        //            var office = await _officeService.GetOfficeById(mixinCancelOrderResource.OfficeId);
        //            var officeResource = _mapper.Map<Office, OfficeResource>(office);

        //            var voteDetail = await _voteDetailService.GetVoteDetailById(mixinCancelOrderResource.VoteId);
        //            var voteDetailResource = _mapper.Map<VoteDetail, VoteDetailResource>(voteDetail);

        //            var accountDetail = await _accountDetailService.GetAccountDetailById(mixinCancelOrderResource.BankAccountId);
        //            var accountDetailResource = _mapper.Map<AccountDetail, AccountDetailResource>(accountDetail);

        //            var mixinOrderLine = await _mixinOrderLineService.GetAllMixinOrderLineForMixinOrderId(mixinCancelOrderResource.Id);
        //            var mixinOrderLineResources = _mapper.Map<IEnumerable<MixinOrderLine>, IEnumerable<MixinOrderLineResource>>(mixinOrderLine);

        //            custommixinOrderList.Add(new
        //            {
        //                id = mixinCancelOrderResource.Id,
        //                isActive = mixinCancelOrderResource.IsActive,
        //                voteDetail = voteDetailResource,
        //                VoteId = mixinCancelOrderResource.VoteId,
        //                office = officeResource,
        //                officeId = mixinCancelOrderResource.OfficeId,
        //                accountDetail = accountDetailResource,
        //                bankAccountId = mixinCancelOrderResource.BankAccountId,
        //                dateCreated = mixinCancelOrderResource.DateCreated,
        //                dateModified = mixinCancelOrderResource.DateModified,
        //                sabhaId = mixinCancelOrderResource.SabhaId,
        //                voteCode = voteDetailResource.Code,
        //                voteNameSinahala = voteDetailResource.NameSinhala,
        //                voteNameTamil = voteDetailResource.NameTamil,
        //                voteNameEnglish = voteDetailResource.NameEnglish,
        //                accountNo = accountDetailResource.AccountNo,
        //                accountName = accountDetailResource.NameSinhala,
        //                accountNameTamil = accountDetailResource.NameTamil,
        //                accountNameEnglish = accountDetailResource.NameEnglish,
        //                mixinOrderLine = mixinOrderLineResources,
        //            });
        //        }
        //        return Ok(custommixinOrderList);
        //    }
        //    return Ok("No Data Found");
        //}

        //[HttpGet]
        //[Route("GetAllForOfficeId/{officeid}")]
        //public async Task<ActionResult> GetAllForOfficeId(int officeid)
        //{
        //    var mixinOrders = await _mixinOrderService.GetAllForOfficeId(officeid);
        //    var mixinCancelOrderResources = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinCancelOrderResource>>(mixinOrders);

        //    if (mixinCancelOrderResources != null && mixinCancelOrderResources.Count() > 0)
        //    {
        //        var custommixinOrderList = new List<dynamic>();

        //        for (int i = 0; i < mixinCancelOrderResources.Count(); i++)
        //        {
        //            var mixinCancelOrderResource = mixinCancelOrderResources.ToList()[i];

        //            var office = await _officeService.GetOfficeById(mixinCancelOrderResource.OfficeId);
        //            var officeResource = _mapper.Map<Office, OfficeResource>(office);

        //            var voteDetail = await _voteDetailService.GetVoteDetailById(mixinCancelOrderResource.VoteId);
        //            var voteDetailResource = _mapper.Map<VoteDetail, VoteDetailResource>(voteDetail);

        //            var accountDetail = await _accountDetailService.GetAccountDetailById(mixinCancelOrderResource.BankAccountId);
        //            var accountDetailResource = _mapper.Map<AccountDetail, AccountDetailResource>(accountDetail);

        //            var mixinOrderLine = await _mixinOrderLineService.GetAllMixinOrderLineForMixinOrderId(mixinCancelOrderResource.Id);
        //            var mixinOrderLineResources = _mapper.Map<IEnumerable<MixinOrderLine>, IEnumerable<MixinOrderLineResource>>(mixinOrderLine);

        //            custommixinOrderList.Add(new
        //            {
        //                id = mixinCancelOrderResource.Id,
        //                isActive = mixinCancelOrderResource.IsActive,
        //                voteDetail = voteDetailResource,
        //                VoteId = mixinCancelOrderResource.VoteId,
        //                office = officeResource,
        //                officeId = mixinCancelOrderResource.OfficeId,
        //                accountDetail = accountDetailResource,
        //                bankAccountId = mixinCancelOrderResource.BankAccountId,
        //                dateCreated = mixinCancelOrderResource.DateCreated,
        //                dateModified = mixinCancelOrderResource.DateModified,
        //                sabhaId = mixinCancelOrderResource.SabhaId,
        //                voteCode = voteDetailResource.Code,
        //                voteNameSinahala = voteDetailResource.NameSinhala,
        //                voteNameTamil = voteDetailResource.NameTamil,
        //                voteNameEnglish = voteDetailResource.NameEnglish,
        //                accountNo = accountDetailResource.AccountNo,
        //                accountName = accountDetailResource.NameSinhala,
        //                accountNameTamil = accountDetailResource.NameTamil,
        //                accountNameEnglish = accountDetailResource.NameEnglish,
        //                mixinOrderLine = mixinOrderLineResources,
        //            });
        //        }
        //        return Ok(custommixinOrderList);
        //    }
        //    return Ok("No Data Found");
        //}

        //[HttpGet]
        //[Route("getAllOfficeGroupedForMixinOrder/{sabhaid}")]
        //public async Task<ActionResult> GetAllOfficeGroupedForMixinOrder(int sabhaid)
        //{
        //    var mixinOrders = await _mixinOrderService.GetAllForSabhaId(sabhaid);
        //    var mixinCancelOrderResources = _mapper.Map<IEnumerable<MixinOrder>, IEnumerable<MixinCancelOrderResource>>(mixinOrders);

        //    if (mixinCancelOrderResources != null && mixinCancelOrderResources.Count() > 0)
        //    {
        //        var custommixinOrderList = new List<dynamic>();
        //        for (int i = 0; i < mixinCancelOrderResources.Count(); i++)
        //        {
        //            var mixinCancelOrderResource = mixinCancelOrderResources.ToList()[i];

        //            var office = await _officeService.GetOfficeById(mixinCancelOrderResource.OfficeId);
        //            var officeResource = _mapper.Map<Office, OfficeResource>(office);

        //            var voteDetail = await _voteDetailService.GetVoteDetailById(mixinCancelOrderResource.VoteId);
        //            var voteDetailResource = _mapper.Map<VoteDetail, VoteDetailResource>(voteDetail);

        //            var accountDetail = await _accountDetailService.GetAccountDetailById(mixinCancelOrderResource.BankAccountId);
        //            var accountDetailResource = _mapper.Map<AccountDetail, AccountDetailResource>(accountDetail);

        //            var selectedlanguage = await _selectedLanguageService.GetSelectedLanguageforSabhaId(mixinCancelOrderResource.SabhaId);
        //            var sellanguage = _mapper.Map<SelectedLanguage, SelectedLanguageResource>(selectedlanguage);

        //            var language = await _languageService.GetLanguageById(sellanguage.LanguageId.Value);
        //            var lang = _mapper.Map<Language, LanguageResource>(language);

        //            var mixinOrderLine = await _mixinOrderLineService.GetAllMixinOrderLineForMixinOrderId(mixinCancelOrderResource.Id);
        //            var mixinOrderLineResources = _mapper.Map<IEnumerable<MixinOrderLine>, IEnumerable<MixinOrderLineResource>>(mixinOrderLine);

        //            String officeName = "";
        //            String voteName = "";
        //            if (lang.Description == "Sinhala")
        //            {
        //                officeName = officeResource.NameSinhala;
        //                voteName = voteDetailResource.NameSinhala;
        //            }
        //            if (lang.Description == "English")
        //            {
        //                officeName = officeResource.NameEnglish;
        //                voteName = voteDetailResource.NameEnglish;
        //            }
        //            if (lang.Description == "Tamil")
        //            {
        //                officeName = officeResource.NameTamil;
        //                voteName = voteDetailResource.NameTamil;
        //            }

        //            custommixinOrderList.Add(new
        //            {
        //                id = mixinCancelOrderResource.Id,
        //                voteId = voteDetail.ID,
        //                voteCode = voteDetail.Code,
        //                voteName = voteName,
        //                office = officeName,
        //                accountNo = accountDetailResource.AccountNo,
        //                mixinOrderLine = mixinOrderLineResources,
        //            });
        //        }

        //        var groupedOfficesMixinOrderList = new List<dynamic>();
        //        var DistinctItems = custommixinOrderList.GroupBy(x => x.voteId).Select(y => y.First());
        //        List<dynamic> Result = null;
        //        foreach (var voteAssign in DistinctItems)
        //        {
        //            string offices = "";
        //            foreach (var assignoffices in custommixinOrderList.Where(x => x.voteId == voteAssign.voteId))
        //            {
        //                offices = offices + " | " + assignoffices.office;
        //            }

        //            groupedOfficesMixinOrderList.Add(new
        //            {
        //                voteId = voteAssign.voteId,
        //                voteCode = voteAssign.voteCode,
        //                voteName = voteAssign.voteName,
        //                office = offices,
        //                accountNo = voteAssign.accountNo,
        //                mixinOrderLine = voteAssign.mixinOrderLine,
        //            });
        //        }
        //        return Ok(groupedOfficesMixinOrderList);
        //    }
        //    return Ok("No Data Found");
        //}

        //[HttpGet]
        //[Route("getById/{id}")]
        //public async Task<ActionResult<MixinCancelOrderResource>> GetById([FromRoute] int id)
        //{
        //    var mixinOrder = await _mixinOrderService.GetById(id);
        //    var mixinCancelOrderResource = _mapper.Map<MixinOrder, MixinCancelOrderResource>(mixinOrder);

        //    var mixinOrderLine = await _mixinOrderLineService.GetAllMixinOrderLineForMixinOrderId(mixinCancelOrderResource.Id);
        //    var mixinOrderLineResources = _mapper.Map<IEnumerable<MixinOrderLine>, IEnumerable<MixinOrderLineResource>>(mixinOrderLine);

        //    if (mixinOrderLineResources != null)
        //        mixinCancelOrderResource.MixinOrderLine = mixinOrderLine.ToList();

        //    if (mixinCancelOrderResource == null)
        //        return NotFound();

        //    return Ok(mixinCancelOrderResource);
        //}

        //[HttpPost("save")]
        //public async Task<IActionResult> Save([FromBody] List<MixinCancelOrderResource> objmixinCancelOrderResourceList)
        //{
        //    try
        //    {
        //        if (objmixinCancelOrderResourceList != null && objmixinCancelOrderResourceList.Count > 0 && objmixinCancelOrderResourceList[0].Id == 0)
        //        {
        //            for (int i = 0; i < objmixinCancelOrderResourceList.Count; i++)
        //            {
        //                objmixinCancelOrderResourceList[i].CreatedAt = DateTime.Now;

        //                var mixinOrderToCreate = _mapper.Map<MixinCancelOrderResource, MixinOrder>(objmixinCancelOrderResourceList[i]);
        //                var newMixinOrder = await _mixinOrderService.Create(mixinOrderToCreate);

        //                var mixinOrder = await _mixinOrderService.GetById(newMixinOrder.Id);
        //                var mixinCancelOrderResource = _mapper.Map<MixinOrder, MixinCancelOrderResource>(mixinOrder);
        //            }
        //            return Ok(objmixinCancelOrderResourceList);
        //        }
        //        else
        //        {
        //            var updatedMixinCancelOrderResource = new MixinCancelOrderResource();
        //            for (int i = 0; i < objmixinCancelOrderResourceList.Count; i++)
        //            {
        //                objmixinCancelOrderResourceList[i].UpdatedAt = DateTime.Now;
        //                var mixinOrderToBeUpdate = await _mixinOrderService.GetById(objmixinCancelOrderResourceList[i].Id);
        //                if (mixinOrderToBeUpdate == null)
        //                    return NotFound();
        //                var mixinOrder = _mapper.Map<MixinCancelOrderResource, MixinOrder>(objmixinCancelOrderResourceList[i]);
        //                await _mixinOrderService.Update(mixinOrderToBeUpdate, mixinOrder);
        //                var updatedMixinOrder = await _mixinOrderService.GetById(objmixinCancelOrderResourceList[i].Id);
        //                updatedMixinCancelOrderResource = _mapper.Map<MixinOrder, MixinCancelOrderResource>(updatedMixinOrder);
        //            }
        //            return Ok(updatedMixinCancelOrderResource);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }
        //}


        //[HttpPost]
        //[Route("delete/{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var mixinOrder = await _mixinOrderService.GetById(id);

        //    if (mixinOrder == null)
        //        return NotFound();

        //    var mixinOrderLine = await _mixinOrderLineService.GetAllMixinOrderLineForMixinOrderId(mixinOrder.Id);

        //    if (mixinOrderLine.Count() == 0)
        //    {
        //        await _mixinOrderService.Delete(mixinOrder);
        //        return NoContent();
        //    }
        //    else
        //    {
        //        return Ok("Please remove all child records first..!");
        //    }
        //}

        [HttpGet]
        [Route("getByOrderId/{orderid}")]
        public async Task<ActionResult<IEnumerable<MixinOrder>>> GetByOrderId([FromRoute] int orderid)
        {
            var mixinCancelOrders = await _mixinOrderService.GetByOrderId(orderid);
            var mixinCancelOrderResources = _mapper.Map<MixinCancelOrder, MixinCancelOrderResource>(mixinCancelOrders);

            return Ok(mixinCancelOrderResources);
        }
    }
}
