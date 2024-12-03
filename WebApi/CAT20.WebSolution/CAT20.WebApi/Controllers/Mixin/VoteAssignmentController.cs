using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Services.Vote;
using CAT20.Services.Control;
using CAT20.Services.Mixin;
using CAT20.WebApi.Controllers;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.Control;
using CAT20.WebApi.Resources.Mixin;
using CAT20.WebApi.Resources.Vote;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using DocumentFormat.OpenXml.Wordprocessing;

namespace CAT20.Api.Controllers
{
    [Route("api/mixin/voteAssignments")]
    [ApiController]
    public class VoteAssignmentsController : BaseController
    {
        private readonly IVoteAssignmentService _voteAssignmentService;
        private readonly IVoteAssignmentDetailsService _voteAssignmentDetailsService;
        private readonly IVoteDetailService _voteDetailService;
        private readonly IAccountDetailService _accountDetailService;
        private readonly IOfficeService _officeService;
        private readonly ISabhaService _sabhaService;
        private readonly ISelectedLanguageService _selectedLanguageService;
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public VoteAssignmentsController(IVoteAssignmentService voteAssignmentService, IMapper mapper, IVoteAssignmentDetailsService voteAssignmentDetailsService,
            IVoteDetailService voteDetailService, IAccountDetailService accountDetailService, IOfficeService officeService, ISabhaService sabhaService,
            ISelectedLanguageService selectedLanguageService, ILanguageService languageService)
        {
            this._mapper = mapper;
            this._voteAssignmentService = voteAssignmentService;
            _voteAssignmentDetailsService = voteAssignmentDetailsService;
            _voteDetailService = voteDetailService;
            _accountDetailService = accountDetailService;
            _officeService = officeService;
            _sabhaService = sabhaService;
            _selectedLanguageService = selectedLanguageService;
            _languageService = languageService;
        }

        [HttpGet]
        [Route("getAllForSabhaId/{sabhaid}")]
        public async Task<ActionResult> GetAllForSabhaId(int sabhaid)
        {
            var voteAssignments = await _voteAssignmentService.GetAllForSabhaId(sabhaid);
            var voteAssignmentResources = _mapper.Map<IEnumerable<VoteAssignment>, IEnumerable<VoteAssignmentResource>>(voteAssignments);

            if (voteAssignmentResources != null && voteAssignmentResources.Any())
            {
                var customvoteAssignmentList = new List<dynamic>();

                var sabha = await _sabhaService.GetSabhaById(sabhaid);
                var sabhaResources = _mapper.Map<Sabha, SabhaResource>(sabha);

                var offices = await _officeService.getAllOfficesForSabhaId(sabhaid);
                var officeResources = _mapper.Map<IEnumerable<Office>, IEnumerable<OfficeResource>>(offices);

                var voteDetails = await _voteDetailService.GetAllVoteDetailsForSabhaId(sabhaid);
                var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

                var accountDetails = await _accountDetailService.GetAllAccountDetails();
                var accountDetailResources = _mapper.Map<IEnumerable<AccountDetail>, IEnumerable<AccountDetailResource>>(accountDetails);

                var voteAssignmentDetails = await _voteAssignmentDetailsService.GetAll();
                var voteAssignmentDetailsResources = _mapper.Map<IEnumerable<VoteAssignmentDetails>, IEnumerable<VoteAssignmentDetailsResource>>(voteAssignmentDetails);

                foreach (var voteAssignmentResource in voteAssignmentResources)
                {

                    try
                    {
                        if (sabha.AccountSystemVersionId == 2)
                        {
                            customvoteAssignmentList.Add(new
                            {
                                id = voteAssignmentResource.Id,
                                isActive = voteAssignmentResource.IsActive,
                                voteDetail = voteDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.VoteId),
                                VoteId = voteAssignmentResource.VoteId,
                                office = officeResources.FirstOrDefault(t => t.ID == voteAssignmentResource.OfficeId),
                                officeId = voteAssignmentResource.OfficeId,
                                accountDetail = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId),
                                bankAccountId = voteAssignmentResource.BankAccountId,
                                dateCreated = voteAssignmentResource.DateCreated,
                                dateModified = voteAssignmentResource.DateModified,
                                sabhaId = voteAssignmentResource.SabhaId,
                                voteCode = voteDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.VoteId)?.Code,
                                voteNameSinhala = voteDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.VoteId)?.NameEnglish,
                                voteNameTamil = voteDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.VoteId)?.NameEnglish,
                                voteNameEnglish = voteDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.VoteId)?.NameEnglish,
                                accountNo = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId)?.AccountNo,
                                accountName = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId)?.NameSinhala,
                                accountNameTamil = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId)?.NameTamil,
                                accountNameEnglish = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId)?.NameEnglish,
                                voteAssignmentDetails = voteAssignmentDetailsResources
                                    .Where(t => t.VoteAssignmentId == voteAssignmentResource.Id)
                                    .ToList(),
                            });
                        }
                        else
                        {
                            customvoteAssignmentList.Add(new
                            {
                                id = voteAssignmentResource.Id,
                                isActive = voteAssignmentResource.IsActive,
                                voteDetail = voteDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.VoteId),
                                VoteId = voteAssignmentResource.VoteId,
                                office = officeResources.FirstOrDefault(t => t.ID == voteAssignmentResource.OfficeId),
                                officeId = voteAssignmentResource.OfficeId,
                                accountDetail = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId),
                                bankAccountId = voteAssignmentResource.BankAccountId,
                                dateCreated = voteAssignmentResource.DateCreated,
                                dateModified = voteAssignmentResource.DateModified,
                                sabhaId = voteAssignmentResource.SabhaId,
                                voteCode = voteDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.VoteId)?.Code,
                                voteNameSinhala = voteDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.VoteId)?.NameSinhala,
                                voteNameTamil = voteDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.VoteId)?.NameTamil,
                                voteNameEnglish = voteDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.VoteId)?.NameEnglish,
                                accountNo = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId)?.AccountNo,
                                accountName = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId)?.NameSinhala,
                                accountNameTamil = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId)?.NameTamil,
                                accountNameEnglish = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId)?.NameEnglish,
                                voteAssignmentDetails = voteAssignmentDetailsResources
                                    .Where(t => t.VoteAssignmentId == voteAssignmentResource.Id)
                                    .ToList(),
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception (log or perform necessary actions)
                    }
                }
                // Remove items where voteCode is null and then sort the list by voteCode in ascending order
                var sortedCustomVoteAssignmentList = customvoteAssignmentList
                    .Where(v => v.voteCode != null)
                    .OrderBy(v => v.voteCode)
                    .ToList();

                return Ok(sortedCustomVoteAssignmentList);
            }
            return Ok("No Data Found");
        }

        //[HttpGet]
        //[Route("getAllForSabhaId/{sabhaid}")]
        //public async Task<ActionResult> GetAllForSabhaId(int sabhaid)
        //{
        //    try
        //    {
        //        var voteAssignments = await _voteAssignmentService.GetAllForSabhaId(sabhaid);
        //        var sabha = await _sabhaService.GetSabhaById(sabhaid);
        //        //var sabhaResources = _mapper.Map<Sabha, SabhaResource>(sabha);

        //        if (voteAssignments != null && voteAssignments.Any())
        //        {
        //            var customvoteAssignmentList = new List<dynamic>();

        //            foreach (var voteAssignment in voteAssignments)
        //            {
        //                //var office = await _officeService.GetOfficeById(voteAssignment.OfficeId);
        //                //var officeResource = _mapper.Map<Office, OfficeResource>(await _officeService.GetOfficeById(voteAssignment.OfficeId));

        //                //var voteDetail = await _voteDetailService.GetVoteDetailById(voteAssignment.VoteId);
        //                var voteDetailResource = _mapper.Map<VoteDetail, VoteDetailResource>(await _voteDetailService.GetVoteDetailById(voteAssignment.VoteId));

        //                //var accountDetail = await _accountDetailService.GetAccountDetailById(voteAssignment.BankAccountId);
        //                var accountDetailResource = _mapper.Map<AccountDetail, AccountDetailResource>(await _accountDetailService.GetAccountDetailById(voteAssignment.BankAccountId));

        //                //var voteAssignmentDetails = await _voteAssignmentDetailsService.GetAllVoteAssignmentDetailsForVoteAssignmentId(voteAssignment.Id);
        //                var voteAssignmentDetailsResources = _mapper.Map<IEnumerable<VoteAssignmentDetails>, IEnumerable<VoteAssignmentDetailsResource>>(await _voteAssignmentDetailsService.GetAllVoteAssignmentDetailsForVoteAssignmentId(voteAssignment.Id));

        //                String voteDetailResourceNameSinhala = "";
        //                String voteDetailResourceNameEnglish = "";
        //                String voteDetailResourceNameTamil = "";
        //                if (sabha.AccountSystemVersionId == 2) 
        //                {
        //                    voteDetailResourceNameSinhala = voteDetailResource?.NameEnglish;
        //                    voteDetailResourceNameEnglish = voteDetailResource?.NameEnglish;
        //                    voteDetailResourceNameTamil = voteDetailResource?.NameEnglish;
        //                } 
        //                else 
        //                {
        //                    voteDetailResourceNameSinhala = voteDetailResource?.NameSinhala;
        //                    voteDetailResourceNameEnglish = voteDetailResource?.NameEnglish;
        //                    voteDetailResourceNameTamil = voteDetailResource?.NameTamil;
        //                }

        //                var customVoteAssignment = new
        //                {
        //                    id = voteAssignment.Id,
        //                    isActive = voteAssignment.IsActive,
        //                    voteDetail = voteDetailResource,
        //                    VoteId = voteAssignment.VoteId,
        //                    office = _mapper.Map<Office, OfficeResource>(await _officeService.GetOfficeById(voteAssignment.OfficeId)),
        //                    officeId = voteAssignment.OfficeId,
        //                    accountDetail = accountDetailResource,
        //                    bankAccountId = voteAssignment.BankAccountId,
        //                    dateCreated = voteAssignment.DateCreated,
        //                    dateModified = voteAssignment.DateModified,
        //                    sabhaId = voteAssignment.SabhaId,
        //                    voteCode = voteDetailResource?.Code,
        //                    voteNameSinhala = voteDetailResourceNameSinhala,
        //                    voteNameTamil = voteDetailResourceNameTamil,
        //                    voteNameEnglish = voteDetailResourceNameEnglish,
        //                    accountNo = accountDetailResource?.AccountNo,
        //                    accountName = accountDetailResource?.NameSinhala,
        //                    accountNameTamil = accountDetailResource?.NameTamil,
        //                    accountNameEnglish = accountDetailResource?.NameEnglish,
        //                    voteAssignmentDetails = voteAssignmentDetailsResources.ToList(),
        //                };

        //                customvoteAssignmentList.Add(customVoteAssignment);
        //            }

        //            return Ok(customvoteAssignmentList);
        //        }

        //        return Ok("No Data Found");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle the exception (log or perform necessary actions)
        //        return StatusCode(500, "Internal Server Error");
        //    }
        //}

        [HttpGet]
        [Route("GetAllForOfficeId/{officeid}")]
        public async Task<ActionResult> GetAllForOfficeId(int officeid)
        {
            var voteAssignments = await _voteAssignmentService.GetAllForOfficeId(officeid);
            var voteAssignmentResources = _mapper.Map<IEnumerable<VoteAssignment>, IEnumerable<VoteAssignmentResource>>(voteAssignments);

            if (voteAssignmentResources != null && voteAssignmentResources.Count() > 0)
            {
                var customvoteAssignmentList = new List<dynamic>();

                var office = await _officeService.GetOfficeById(officeid);
                var officeResource = _mapper.Map<Office, OfficeResource>(office);

                var sabha = await _sabhaService.GetSabhaById(office.SabhaID.Value);
                var sabhaResources = _mapper.Map<Sabha, SabhaResource>(sabha);

                var voteDetails = await _voteDetailService.GetAllVoteDetailsForSabhaId(sabha.ID.Value);
                var voteDetailResources = _mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailResource>>(voteDetails);

                var accountDetails = await _accountDetailService.GetAllAccountDetailByOfficeId(sabha.ID.Value);
                var accountDetailResources = _mapper.Map<IEnumerable<AccountDetail>, IEnumerable<AccountDetailResource>>(accountDetails);

                var voteAssignmentDetails = await _voteAssignmentDetailsService.GetAll();
                var voteAssignmentDetailsResources = _mapper.Map<IEnumerable<VoteAssignmentDetails>, IEnumerable<VoteAssignmentDetailsResource>>(voteAssignmentDetails);

                for (int i = 0; i < voteAssignmentResources.Count(); i++)
                {
                    var voteAssignmentResource = voteAssignmentResources.ToList()[i];
                    /// check votdetails is existing
                    var voteDetail = voteDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.VoteId);
                    if (voteDetail != null)
                    {
                        if (sabha.AccountSystemVersionId == 2)
                        {
                            try
                            {
                                customvoteAssignmentList.Add(new
                                {
                                    id = voteAssignmentResource.Id,
                                    isActive = voteAssignmentResource.IsActive,
                                    voteDetail = voteDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.VoteId),
                                    VoteId = voteAssignmentResource.VoteId,
                                    office = officeResource,
                                    officeId = voteAssignmentResource.OfficeId,
                                    accountDetail = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId),
                                    bankAccountId = voteAssignmentResource.BankAccountId,
                                    dateCreated = voteAssignmentResource.DateCreated,
                                    dateModified = voteAssignmentResource.DateModified,
                                    sabhaId = voteAssignmentResource.SabhaId,
                                    voteCode = voteDetail.Code,
                                    voteNameSinhala = voteDetail.NameEnglish,
                                    voteNameTamil = voteDetail.NameEnglish,
                                    voteNameEnglish = voteDetail.NameEnglish,
                                    accountNo = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId).AccountNo,
                                    accountName = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId).NameSinhala,
                                    accountNameTamil = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId).NameTamil,
                                    accountNameEnglish = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId).NameEnglish,
                                    voteAssignmentDetails = voteAssignmentDetailsResources.Where(t => t.VoteAssignmentId == voteAssignmentResource.Id).ToList(),
                                });
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else
                        {
                            try
                            {
                                customvoteAssignmentList.Add(new
                                {
                                    id = voteAssignmentResource.Id,
                                    isActive = voteAssignmentResource.IsActive,
                                    voteDetail = voteDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.VoteId),
                                    VoteId = voteAssignmentResource.VoteId,
                                    office = officeResource,
                                    officeId = voteAssignmentResource.OfficeId,
                                    accountDetail = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId),
                                    bankAccountId = voteAssignmentResource.BankAccountId,
                                    dateCreated = voteAssignmentResource.DateCreated,
                                    dateModified = voteAssignmentResource.DateModified,
                                    sabhaId = voteAssignmentResource.SabhaId,
                                    voteCode = voteDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.VoteId).Code,
                                    voteNameSinhala = voteDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.VoteId).NameSinhala,
                                    voteNameTamil = voteDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.VoteId).NameTamil,
                                    voteNameEnglish = voteDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.VoteId).NameEnglish,
                                    accountNo = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId).AccountNo,
                                    accountName = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId).NameSinhala,
                                    accountNameTamil = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId).NameTamil,
                                    accountNameEnglish = accountDetailResources.FirstOrDefault(t => t.ID == voteAssignmentResource.BankAccountId).NameEnglish,
                                    voteAssignmentDetails = voteAssignmentDetailsResources.Where(t => t.VoteAssignmentId == voteAssignmentResource.Id).ToList(),
                                });
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        //Close checking votedetail exisitng
                    }
                }
                // Remove items where voteCode is null and then sort the list by voteCode in ascending order
                var sortedCustomVoteAssignmentList = customvoteAssignmentList
                    .Where(v => v.voteCode != null)
                    .OrderBy(v => v.voteCode)
                    .ToList();

                return Ok(sortedCustomVoteAssignmentList);
            }
            return Ok("No Data Found");
        }

        [HttpGet]
        [Route("getAllOfficeGroupedForVoteAssignment/{sabhaid}")]
        public async Task<ActionResult> GetAllOfficeGroupedForVoteAssignment(int sabhaid)
        {
            var voteAssignments = await _voteAssignmentService.GetAllForSabhaId(sabhaid);
            var voteAssignmentResources = _mapper.Map<IEnumerable<VoteAssignment>, IEnumerable<VoteAssignmentResource>>(voteAssignments);

            if (voteAssignmentResources != null && voteAssignmentResources.Count() > 0)
            {
                var customvoteAssignmentList = new List<dynamic>();
                for (int i = 0; i < voteAssignmentResources.Count(); i++)
                {
                    var voteAssignmentResource = voteAssignmentResources.ToList()[i];

                    var office = await _officeService.GetOfficeById(voteAssignmentResource.OfficeId);
                    var officeResource = _mapper.Map<Office, OfficeResource>(office);

                    var voteDetail = await _voteDetailService.GetVoteDetailById(voteAssignmentResource.VoteId);
                    var voteDetailResource = _mapper.Map<VoteDetail, VoteDetailResource>(voteDetail);

                    var accountDetail = await _accountDetailService.GetAccountDetailById(voteAssignmentResource.BankAccountId);
                    var accountDetailResource = _mapper.Map<AccountDetail, AccountDetailResource>(accountDetail);

                    var selectedlanguage = await _selectedLanguageService.GetSelectedLanguageforSabhaId(voteAssignmentResource.SabhaId);
                    var sellanguage = _mapper.Map<SelectedLanguage, SelectedLanguageResource>(selectedlanguage);

                    var language = await _languageService.GetLanguageById(sellanguage.LanguageId.Value);
                    var lang = _mapper.Map<Language, LanguageResource>(language);

                    var voteAssignmentDetails = await _voteAssignmentDetailsService.GetAllVoteAssignmentDetailsForVoteAssignmentId(voteAssignmentResource.Id);
                    var voteAssignmentDetailsResources = _mapper.Map<IEnumerable<VoteAssignmentDetails>, IEnumerable<VoteAssignmentDetailsResource>>(voteAssignmentDetails);

                    String officeName = "";
                    String voteName = "";
                    String accountname = "";

                    if (voteDetailResource != null && accountDetailResource != null)
                    {
                        if (lang.Description == "Sinhala")
                        {
                            officeName = officeResource.NameSinhala;
                            voteName = voteDetailResource.NameSinhala;
                            accountname = accountDetailResource.NameSinhala;
                        }
                        if (lang.Description == "English")
                        {
                            officeName = officeResource.NameEnglish;
                            voteName = voteDetailResource.NameEnglish;
                            accountname = accountDetailResource.NameEnglish;
                        }
                        if (lang.Description == "Tamil")
                        {
                            officeName = officeResource.NameTamil;
                            voteName = voteDetailResource.NameTamil;
                            accountname = accountDetailResource.NameTamil;
                        }

                        customvoteAssignmentList.Add(new
                        {
                            id = voteAssignmentResource.Id,
                            voteId = voteDetail.ID,
                            voteCode = voteDetail.Code,
                            voteName = voteName,
                            office = officeName,
                            accountNo = accountDetailResource.AccountNo + " - " + accountname,
                            voteAssignmentDetails = voteAssignmentDetailsResources,
                        });
                    }
                }

                var groupedOfficesVoteAssignmentList = new List<dynamic>();
                var DistinctItems = customvoteAssignmentList.GroupBy(x => x.voteId).Select(y => y.First());
                List<dynamic> Result = null;
                foreach (var voteAssign in DistinctItems)
                {
                    string offices = "";
                    foreach (var assignoffices in customvoteAssignmentList.Where(x => x.voteId == voteAssign.voteId))
                    {
                        offices = offices + " | " + assignoffices.office;
                    }

                    groupedOfficesVoteAssignmentList.Add(new
                    {
                        voteId = voteAssign.voteId,
                        voteCode = voteAssign.voteCode,
                        voteName = voteAssign.voteName,
                        office = offices,
                        accountNo = voteAssign.accountNo,
                        voteAssignmentDetails = voteAssign.voteAssignmentDetails,
                    });
                }
                return Ok(groupedOfficesVoteAssignmentList);
            }
            return Ok("No Data Found");
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<ActionResult<VoteAssignmentResource>> GetById([FromRoute] int id)
        {
            var voteAssignment = await _voteAssignmentService.GetById(id);
            var voteAssignmentResource = _mapper.Map<VoteAssignment, VoteAssignmentResource>(voteAssignment);

            var voteAssignmentDetails = await _voteAssignmentDetailsService.GetAllVoteAssignmentDetailsForVoteAssignmentId(voteAssignmentResource.Id);
            var voteAssignmentDetailsResources = _mapper.Map<IEnumerable<VoteAssignmentDetails>, IEnumerable<VoteAssignmentDetailsResource>>(voteAssignmentDetails);

            if (voteAssignmentDetailsResources != null)
                voteAssignmentResource.VoteAssignmentDetails = voteAssignmentDetailsResources.ToList();

            if (voteAssignmentResource == null)
                return NotFound();

            return Ok(voteAssignmentResource);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] List<VoteAssignmentResource> objvoteAssignmentResourceList)
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
                    languageid = int.Parse(HttpContext.User.FindFirst("languageid")!.Value),
                };

                if (objvoteAssignmentResourceList != null && objvoteAssignmentResourceList.Count > 0 && objvoteAssignmentResourceList[0].Id == 0)
                {
                    //for (int i = 0; i < objvoteAssignmentResourceList.Count; i++)
                    //{
                    //    objvoteAssignmentResourceList[i].DateCreated = DateTime.Now;

                    //    var voteAssignmentToCreate = _mapper.Map<VoteAssignmentResource, VoteAssignment>(objvoteAssignmentResourceList[i]);
                    //    var newVoteAssignment = await _voteAssignmentService.Create(voteAssignmentToCreate);

                    //    var voteAssignment = await _voteAssignmentService.GetById(newVoteAssignment.Id);
                    //    var voteAssignmentResource = _mapper.Map<VoteAssignment, VoteAssignmentResource>(voteAssignment);
                    //}
                    //return Ok(objvoteAssignmentResourceList);


                    var voteAssignmentToCreate = _mapper.Map<IEnumerable<VoteAssignmentResource>, IEnumerable<VoteAssignment>>(objvoteAssignmentResourceList);

                    var result = await _voteAssignmentService.NewCreate(voteAssignmentToCreate, _token);

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
                else
                {
                    var updatedVoteAssignmentResource = new VoteAssignmentResource();
                    for (int i = 0; i < objvoteAssignmentResourceList.Count; i++)
                    {
                        objvoteAssignmentResourceList[i].DateModified = DateTime.Now;
                        var voteAssignmentToBeUpdate = await _voteAssignmentService.GetById(objvoteAssignmentResourceList[i].Id);
                        if (voteAssignmentToBeUpdate == null)
                            return NotFound();
                        var voteAssignment = _mapper.Map<VoteAssignmentResource, VoteAssignment>(objvoteAssignmentResourceList[i]);
                        await _voteAssignmentService.Update(voteAssignmentToBeUpdate, voteAssignment);
                        var updatedVoteAssignment = await _voteAssignmentService.GetById(objvoteAssignmentResourceList[i].Id);
                        updatedVoteAssignmentResource = _mapper.Map<VoteAssignment, VoteAssignmentResource>(updatedVoteAssignment);
                    }
                    return Ok(updatedVoteAssignmentResource);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            var voteAssignment = await _voteAssignmentService.GetById(id);

            if (voteAssignment == null)
                return NotFound();

            var voteAssignmentDetails = await _voteAssignmentDetailsService.GetAllVoteAssignmentDetailsForVoteAssignmentId(voteAssignment.Id);

            if (voteAssignmentDetails.Count() == 0)
            {
                await _voteAssignmentService.Delete(voteAssignment);
                return NoContent();
            }
            else
            {
                return Ok("Please remove all child records first..!");
            }
        }

        [HttpGet]
        [Route("getAllForVoteId/{voteId}")]
        public async Task<ActionResult<IEnumerable<VoteAssignment>>> GetAllForVoteId([FromRoute] int voteId)
        {
            var voteAssignments = await _voteAssignmentService.GetAllForVoteId(voteId);
            var voteAssignmentResources = _mapper.Map<IEnumerable<VoteAssignment>, IEnumerable<VoteAssignmentResource>>(voteAssignments);

            return Ok(voteAssignmentResources);
        }


        [HttpGet]
        [Route("GetAllForOfficeIdAndAccountDetailId/{officeid}/{accountdetailid}")]
        public async Task<ActionResult> GetAllForOfficeIdAndAccountDetailId(int officeid, int accountdetailid)
        {
            //var voteAssignments = await _voteAssignmentService.GetAllForOfficeIdAndAccountDetailId(officeid, accountdetailid);
            //var voteAssignmentResources = _mapper.Map<IEnumerable<VoteAssignment>, IEnumerable<VoteAssignmentResource>>(voteAssignments);
            var voteAssignmentResources = _mapper.Map<IEnumerable<VoteAssignment>, IEnumerable<VoteAssignmentResource>>(await _voteAssignmentService.GetAllForOfficeIdAndAccountDetailId(officeid, accountdetailid));

            if (voteAssignmentResources != null && voteAssignmentResources.Count() > 0)
            {
                var customvoteAssignmentList = new List<dynamic>();

                for (int i = 0; i < voteAssignmentResources.Count(); i++)
                {
                    var voteAssignmentResource = voteAssignmentResources.ToList()[i];

                    //var office = await _officeService.GetOfficeById(voteAssignmentResource.OfficeId);
                    //var officeResource = _mapper.Map<Office, OfficeResource>(office);
                    var officeResource = _mapper.Map<Office, OfficeResource>(await _officeService.GetOfficeById(voteAssignmentResource.OfficeId));

                    //var voteDetail = await _voteDetailService.GetVoteDetailById(voteAssignmentResource.VoteId);
                    //var voteDetailResource = _mapper.Map<VoteDetail, VoteDetailResource>(voteDetail);
                    var voteDetailResource = _mapper.Map<VoteDetail, VoteDetailResource>(await _voteDetailService.GetVoteDetailById(voteAssignmentResource.VoteId));


                    //var accountDetail = await _accountDetailService.GetAccountDetailById(voteAssignmentResource.BankAccountId);
                    //var accountDetailResource = _mapper.Map<AccountDetail, AccountDetailResource>(accountDetail);
                    var accountDetailResource = _mapper.Map<AccountDetail, AccountDetailResource>(await _accountDetailService.GetAccountDetailById(voteAssignmentResource.BankAccountId));

                    //var voteAssignmentDetails = await _voteAssignmentDetailsService.GetAllVoteAssignmentDetailsForVoteAssignmentId(voteAssignmentResource.Id);
                    //var voteAssignmentDetailsResources = _mapper.Map<IEnumerable<VoteAssignmentDetails>, IEnumerable<VoteAssignmentDetailsResource>>(voteAssignmentDetails);
                    var voteAssignmentDetailsResources = _mapper.Map<IEnumerable<VoteAssignmentDetails>, IEnumerable<VoteAssignmentDetailsResource>>(await _voteAssignmentDetailsService.GetAllVoteAssignmentDetailsForVoteAssignmentId(voteAssignmentResource.Id));

                    if (voteDetailResource != null)
                    {
                        customvoteAssignmentList.Add(new
                        {
                            id = voteAssignmentResource.Id,
                            isActive = voteAssignmentResource.IsActive,
                            voteDetail = voteDetailResource,
                            VoteId = voteAssignmentResource.VoteId,
                            office = officeResource,
                            officeId = voteAssignmentResource.OfficeId,
                            accountDetail = accountDetailResource,
                            bankAccountId = voteAssignmentResource.BankAccountId,
                            dateCreated = voteAssignmentResource.DateCreated,
                            dateModified = voteAssignmentResource.DateModified,
                            sabhaId = voteAssignmentResource.SabhaId,
                            voteCode = voteDetailResource.Code,
                            voteNameSinahala = voteDetailResource.NameSinhala,
                            voteNameTamil = voteDetailResource.NameTamil,
                            voteNameEnglish = voteDetailResource.NameEnglish,
                            accountNo = accountDetailResource.AccountNo,
                            accountName = accountDetailResource.NameSinhala,
                            accountNameTamil = accountDetailResource.NameTamil,
                            accountNameEnglish = accountDetailResource.NameEnglish,
                            voteAssignmentDetails = voteAssignmentDetailsResources,
                        });
                    }
                }
                // Remove items where voteCode is null and then sort the list by voteCode in ascending order
                var sortedCustomVoteAssignmentList = customvoteAssignmentList
                    .Where(v => v.voteCode != null)
                    .OrderBy(v => v.voteCode)
                    .ToList();

                return Ok(sortedCustomVoteAssignmentList);
            }
            return Ok("No Data Found");
        }

        [HttpGet]
        [Route("GetAllAssetsAndIncomeVotesForOfficeIdAndAccountDetailId/{officeid}/{accountdetailid}")]
        public async Task<ActionResult> GetAllAssetsAndIncomeVotesForOfficeIdAndAccountDetailId(int officeid, int accountdetailid)
        {
            //var voteAssignments = await _voteAssignmentService.GetAllForOfficeIdAndAccountDetailId(officeid, accountdetailid);
            //var voteAssignmentResources = _mapper.Map<IEnumerable<VoteAssignment>, IEnumerable<VoteAssignmentResource>>(voteAssignments);
            var voteAssignmentResources = _mapper.Map<IEnumerable<VoteAssignment>, IEnumerable<VoteAssignmentResource>>(await _voteAssignmentService.GetAllForOfficeIdAndAccountDetailId(officeid, accountdetailid));

            if (voteAssignmentResources != null && voteAssignmentResources.Count() > 0)
            {
                var customvoteAssignmentList = new List<dynamic>();

                for (int i = 0; i < voteAssignmentResources.Count(); i++)
                {
                    var voteAssignmentResource = voteAssignmentResources.ToList()[i];

                    //var office = await _officeService.GetOfficeById(voteAssignmentResource.OfficeId);
                    //var officeResource = _mapper.Map<Office, OfficeResource>(office);
                    var officeResource = _mapper.Map<Office, OfficeResource>(await _officeService.GetOfficeById(voteAssignmentResource.OfficeId));

                    //var voteDetail = await _voteDetailService.GetVoteDetailById(voteAssignmentResource.VoteId);
                    //var voteDetailResource = _mapper.Map<VoteDetail, VoteDetailResource>(voteDetail);
                    var voteDetailResource = _mapper.Map<VoteDetail, VoteDetailResource>(await _voteDetailService.GetVoteDetailById(voteAssignmentResource.VoteId));


                    //var accountDetail = await _accountDetailService.GetAccountDetailById(voteAssignmentResource.BankAccountId);
                    //var accountDetailResource = _mapper.Map<AccountDetail, AccountDetailResource>(accountDetail);
                    var accountDetailResource = _mapper.Map<AccountDetail, AccountDetailResource>(await _accountDetailService.GetAccountDetailById(voteAssignmentResource.BankAccountId));

                    //var voteAssignmentDetails = await _voteAssignmentDetailsService.GetAllVoteAssignmentDetailsForVoteAssignmentId(voteAssignmentResource.Id);
                    //var voteAssignmentDetailsResources = _mapper.Map<IEnumerable<VoteAssignmentDetails>, IEnumerable<VoteAssignmentDetailsResource>>(voteAssignmentDetails);
                    var voteAssignmentDetailsResources = _mapper.Map<IEnumerable<VoteAssignmentDetails>, IEnumerable<VoteAssignmentDetailsResource>>(await _voteAssignmentDetailsService.GetAllVoteAssignmentDetailsForVoteAssignmentId(voteAssignmentResource.Id));

                    if (voteDetailResource != null)
                    {
                        customvoteAssignmentList.Add(new
                        {
                            id = voteAssignmentResource.Id,
                            isActive = voteAssignmentResource.IsActive,
                            voteDetail = voteDetailResource,
                            VoteId = voteAssignmentResource.VoteId,
                            office = officeResource,
                            officeId = voteAssignmentResource.OfficeId,
                            accountDetail = accountDetailResource,
                            bankAccountId = voteAssignmentResource.BankAccountId,
                            dateCreated = voteAssignmentResource.DateCreated,
                            dateModified = voteAssignmentResource.DateModified,
                            sabhaId = voteAssignmentResource.SabhaId,
                            voteCode = voteDetailResource.Code,
                            voteNameSinahala = voteDetailResource.NameSinhala,
                            voteNameTamil = voteDetailResource.NameTamil,
                            voteNameEnglish = voteDetailResource.NameEnglish,
                            accountNo = accountDetailResource.AccountNo,
                            accountName = accountDetailResource.NameSinhala,
                            accountNameTamil = accountDetailResource.NameTamil,
                            accountNameEnglish = accountDetailResource.NameEnglish,
                            voteAssignmentDetails = voteAssignmentDetailsResources,
                        });
                    }
                }
                // Remove items where voteCode is null and then sort the list by voteCode in ascending order
                var sortedCustomVoteAssignmentList = customvoteAssignmentList
                    .Where(v => v.voteCode != null)
                    .OrderBy(v => v.voteCode)
                    .ToList();

                return Ok(sortedCustomVoteAssignmentList);
            }
            return Ok("No Data Found");
        }

        [HttpGet]
        [Route("getAccountTransferVoteForBankAccountId/{sabhaid}/{accountDetailId}")]
        public async Task<VoteDetailResource> GetAccountTransferVoteForBankAccountId(int sabhaid, int accountDetailId)
        {
            var AccountTranferVotes = await _voteDetailService.GetAllAccountTransferLedgerAccountsForSabhaId(sabhaid);
            if (AccountTranferVotes != null && AccountTranferVotes.Any())
            {
                List<VoteAssignment> AccTransferVoteAssignmentList = new List<VoteAssignment>();

                foreach (var vtdetail in AccountTranferVotes)
                {
                    var voteassignemt = await _voteAssignmentService.GetAllForVoteId(vtdetail.ID.Value);
                    foreach (var voteasgn in voteassignemt)
                    {
                        voteasgn.voteDetail = vtdetail;
                    }
                    AccTransferVoteAssignmentList.Concat(voteassignemt);
                }

                var voteassignmentsforaccountid = AccTransferVoteAssignmentList.FindAll(v => v.BankAccountId == accountDetailId && v.SabhaId == sabhaid);

                var voteassignmentforaccountid = voteassignmentsforaccountid.FirstOrDefault();
                var voteDetailResource = _mapper.Map<VoteDetail, VoteDetailResource>(voteassignmentforaccountid.voteDetail);

                return voteDetailResource;
            }
            return null;
        }

        [HttpGet("getCustomVoteWithSubLevelsForOfficeAndBankAccountId/{bankaccountid}")]
        public async Task<IActionResult> getCustomVoteWithSubLevelsForOfficeAndBankAccountId(int bankaccountid)
        {
            var _token = new HTokenClaim
            {
                userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
            };

            var voteAssignmentDetails = await _voteAssignmentDetailsService.getCustomVoteWithSubLevelsForOfficeAndBankAccountId(bankaccountid, _token);
            var voteDetails = await _voteDetailService.GetAllVoteDetailsForSabhaId(_token.sabhaId);

            var customvoteAssignmentList = new List<VoteAssignmentDetailsMixinCustomResource>();

            foreach (var customvote in voteAssignmentDetails)
            {
                if (voteDetails?.FirstOrDefault(m => m.ID == customvote?.voteAssignment?.VoteId) != null)
                {
                    customvoteAssignmentList.Add(new VoteAssignmentDetailsMixinCustomResource
                    {
                        Id = customvote.Id,
                        customVoteName = customvote.CustomVoteName,
                        VoteAssignmentId = customvote.VoteAssignmentId,
                        Code = (voteDetails?.FirstOrDefault(m => m.ID == customvote?.voteAssignment?.VoteId).Code),
                        VoteCode = (voteDetails?.FirstOrDefault(m => m.ID == customvote?.voteAssignment?.VoteId).Code),
                        voteCodewithCustomName = (voteDetails?.FirstOrDefault(m => m.ID == customvote?.voteAssignment?.VoteId)?.Code) + " - " + customvote.CustomVoteName,
                        IsActive = customvote.IsActive,
                        VoteDetail = (voteDetails?.FirstOrDefault(m => m.ID == customvote?.voteAssignment?.VoteId)),
                        DateCreated = customvote.DateCreated,
                        DateModified = customvote.DateModified,
                        Depth = customvote.Depth,
                        ParentId = customvote.ParentId,
                        IsSubLevel = customvote.IsSubLevel
                    });
                }

            }
            return Ok(customvoteAssignmentList);
        }
    }
}
