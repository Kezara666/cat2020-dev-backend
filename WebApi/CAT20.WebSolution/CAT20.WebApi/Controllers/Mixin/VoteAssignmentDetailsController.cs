using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using CAT20.Api.Validators;
using CAT20.Core.DTO;
using CAT20.Core.DTO.Mix;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Services.Vote;
using CAT20.Services.Mixin;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.Mixin;
using CAT20.WebApi.Resources.Vote;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.Api.Controllers
{
    [Route("api/mixin/voteAssignmentDetails")]
    [ApiController]
    public class VoteAssignmentDetailsController : BaseController
    {
        private readonly IVoteAssignmentService _voteAssignmentService;
        private readonly IVoteAssignmentDetailsService _voteAssignmentDetailsService;
        private readonly IMixinOrderLineService _mixinOrderLineService;
        private readonly IMapper _mapper;
        private readonly IVoteDetailService _voteDetailService;
        private readonly ICustomVoteSubLevel1Service _customVoteSubLevel1Service;
        private readonly ICustomVoteSubLevel2Service _customVoteSubLevel2Service;

        public VoteAssignmentDetailsController(IVoteAssignmentDetailsService voteAssignmentDetailsService, IMapper mapper, IVoteAssignmentService voteAssignmentService, IMixinOrderLineService mixinOrderLineService, IVoteDetailService voteDetailService, ICustomVoteSubLevel1Service customVoteSubLevel1Service, ICustomVoteSubLevel2Service customVoteSubLevel2Service)
        {
            this._mapper = mapper;
            this._voteAssignmentService = voteAssignmentService;
            _voteAssignmentDetailsService = voteAssignmentDetailsService;
            _mixinOrderLineService = mixinOrderLineService;
            _voteDetailService = voteDetailService;
            _customVoteSubLevel1Service = customVoteSubLevel1Service;
            _customVoteSubLevel2Service = customVoteSubLevel2Service;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<VoteAssignmentDetails>>> GetAll()
        {
            var voteAssignmentDetails = await _voteAssignmentDetailsService.GetAll();
            var voteAssignmentDetailsResources = _mapper.Map<IEnumerable<VoteAssignmentDetails>, IEnumerable<VoteAssignmentDetailsResource>>(voteAssignmentDetails);

            return Ok(voteAssignmentDetailsResources);
        }

        //---- previous api ------------
        /*
        [HttpGet]
        [Route("getById/{id}")]
        public async Task<ActionResult<VoteAssignmentDetailsResource>> GetById([FromRoute] int id)
        {
            var voteAssignmentDetails = await _voteAssignmentDetailsService.GetById(id);
            var voteAssignmentDetailsResource = _mapper.Map<VoteAssignmentDetails, VoteAssignmentDetailsResource>(voteAssignmentDetails);

            if (voteAssignmentDetailsResource == null)
                return NotFound();

            return Ok(voteAssignmentDetailsResource);
        }
        */
        //-----

        //-------------[Start - update api]-------
        [HttpGet]
        [Route("getById/{id}")]
        public async Task<ActionResult<VoteAssignmentDetailsResource>> GetById([FromRoute] int id)
        {
            var voteAssignmentDetails = await _voteAssignmentDetailsService.GetById(id);

            //---- adding voteDetail to the response
            var voteDetail = await _voteDetailService.GetVoteDetailById(voteAssignmentDetails.voteAssignment.VoteId);
            voteAssignmentDetails.voteAssignment.voteDetail = voteDetail;
            //-----

            var voteAssignmentDetailsResource = _mapper.Map<VoteAssignmentDetails, VoteAssignmentDetailsResource>(voteAssignmentDetails);

            if (voteAssignmentDetailsResource == null)
                return NotFound();

            return Ok(voteAssignmentDetailsResource);
        }
        //-------------[End - update api]---------

        //[HttpPost("save")]
        //public async Task<IActionResult> Save([FromBody] List<VoteAssignmentDetailsResource> objvoteAssignmentDetailResourceList)
        //{
        //    try
        //    {
        //        if (objvoteAssignmentDetailResourceList != null && objvoteAssignmentDetailResourceList.Count > 0 && objvoteAssignmentDetailResourceList[0].Id == 0)
        //        {
        //            for (int i = 0; i < objvoteAssignmentDetailResourceList.Count; i++)
        //            {
        //                objvoteAssignmentDetailResourceList[i].DateCreated = DateTime.Now;

        //                var voteAssignmentDetailsToCreate = _mapper.Map<VoteAssignmentDetailsResource, VoteAssignmentDetails>(objvoteAssignmentDetailResourceList[i]);
        //                var newVoteAssignmentDetails = await _voteAssignmentDetailsService.Save(voteAssignmentDetailsToCreate);
        //                var voteAssignmentDetails = await _voteAssignmentDetailsService.GetById(newVoteAssignmentDetails.Id);
        //                var voteAssignmentDetailsResource = _mapper.Map<VoteAssignmentDetails, VoteAssignmentDetailsResource>(voteAssignmentDetails);
        //            }
        //            return Ok(objvoteAssignmentDetailResourceList);
        //        }
        //        else
        //        {
        //            var updatedVoteAssignmentDetailsResource = new VoteAssignmentDetailsResource();
        //            for (int i = 0; i < objvoteAssignmentDetailResourceList.Count; i++)
        //            {
        //                objvoteAssignmentDetailResourceList[i].DateModified = DateTime.Now;
        //                var voteAssignmentDetailsToBeUpdate = await _voteAssignmentDetailsService.GetById(objvoteAssignmentDetailResourceList[i].Id);
        //                if (voteAssignmentDetailsToBeUpdate == null)
        //                    return NotFound();
        //                var voteAssignmentDetails = _mapper.Map<VoteAssignmentDetailsResource, VoteAssignmentDetails>(objvoteAssignmentDetailResourceList[i]);
        //                await _voteAssignmentDetailsService.Update(voteAssignmentDetailsToBeUpdate, voteAssignmentDetails);
        //                var updatedVoteAssignmentDetails = await _voteAssignmentDetailsService.GetById(objvoteAssignmentDetailResourceList[i].Id);
        //                updatedVoteAssignmentDetailsResource = _mapper.Map<VoteAssignmentDetails, VoteAssignmentDetailsResource>(updatedVoteAssignmentDetails);
        //            }
        //            return Ok(updatedVoteAssignmentDetailsResource);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }
        //}


        [HttpPost]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            var voteAssignmentDetails = await _voteAssignmentDetailsService.GetById(id);

            if (voteAssignmentDetails == null)
                return NotFound();

            var mixinOrdersForVote = await _mixinOrderLineService.GetAllForVoteIdAndVoteorBal(id,1);

            if (mixinOrdersForVote==null)
            {
                await _voteAssignmentDetailsService.Delete(voteAssignmentDetails);
                return NoContent();
            }
            else
            {
                return BadRequest("Please remove all child records first..!");
            }
        }

        [HttpGet]
        [Route("GetAllForOfficeId/{officeId}")]
        public async Task<ActionResult<IEnumerable<VoteAssignmentDetails>>> GetAllForOfficeId([FromRoute] int officeId)
        {
            try
            {
                //var voteAssignmentDetails = await _voteAssignmentDetailsService.GetAllForOfficeId(officeId);
                var voteAssignmentDetailsResources = _mapper.Map<IEnumerable<VoteAssignmentDetails>, IEnumerable<VoteAssignmentDetailsResource>>(await _voteAssignmentDetailsService.GetAllForOfficeId(officeId));

                var filteredVoteAssignmentDetails = new List<VoteAssignmentDetailsResource>();

                foreach (var voteAssignmentDetail in voteAssignmentDetailsResources)
                {
                    var voteDetail = await _voteDetailService.GetVoteDetailById(voteAssignmentDetail.voteAssignment.VoteId);
                    if (voteDetail != null)
                    {
                        var voteDetailResource = _mapper.Map<VoteDetail, VoteDetailResource>(voteDetail);

                        voteAssignmentDetail.voteAssignment.voteDetail = voteDetailResource;
                        filteredVoteAssignmentDetails.Add(voteAssignmentDetail);
                    }
                  
                }

                return Ok(filteredVoteAssignmentDetails);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }



        [HttpGet]
        [Route("getAllVoteAssignmentDetailsForVoteAssignmentId/{id}")]
        public async Task<ActionResult<VoteAssignmentDetailsResource>> GetAllVoteAssignmentDetailsForVoteAssignmentId([FromRoute] int id)
        {
            var voteAssignmentDetails = await _voteAssignmentDetailsService.GetAllVoteAssignmentDetailsForVoteAssignmentId(id);
            var voteAssignmentDetailsResources = _mapper.Map<IEnumerable<VoteAssignmentDetails>, IEnumerable<VoteAssignmentDetailsResource>>(voteAssignmentDetails);

            if (voteAssignmentDetailsResources == null)
                return NotFound();

            return Ok(voteAssignmentDetailsResources);
        }


        [HttpPost("save")]
        public async Task<IActionResult> SaveCustomVoteSubLevel1(SaveVoteAssignmentDetails saveVoteAssignmentDetails)
        {
            var _token = new HTokenClaim
            {
                userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
            };
          var voteAssignmentDetails = _mapper.Map<SaveVoteAssignmentDetails, VoteAssignmentDetails>(saveVoteAssignmentDetails); 
            var result =  await _voteAssignmentDetailsService.NewSave(voteAssignmentDetails, _token);
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

        [HttpGet("getCustomVoteWithSubLevelsForVoteAssignmentId/{assignmentId}")]
        public async Task<IActionResult> GetCustomVoteWithSubLevelsForVoteAssignmentId(int assignmentId)
        {
            return Ok(await _voteAssignmentDetailsService.GetCustomVoteWithSubLevelsForVoteAssignmentId(assignmentId));
        }

        [HttpGet("getCustomVoteWithSubLevels/{id}")]
        public async Task<IActionResult> GetCustomVoteWithSubLevels(int id)
        {
            return Ok( await _voteAssignmentDetailsService.GetCustomVoteWithSubLevels(id));
        }


        [HttpGet("getCustomVoteWithSubLevelsForVoteId/{voteId}")]
        public async Task<IActionResult> getCustomVoteWithSubLevelsForVoteId(int voteId)
        {
            var _token = new HTokenClaim
            {
                userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
            };

            return Ok(await _voteAssignmentDetailsService.getCustomVoteWithSubLevelsForVoteId(voteId,_token));
        }

        //[HttpGet("getCustomVoteWithSubLevelsForAccountId/{accountId}")]
        //public async Task<IActionResult> getCustomVoteWithSubLevelsForAccountId(int accountId)
        //{
        //    var _token = new HTokenClaim
        //    {
        //        userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
        //        sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
        //        officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
        //    };
        //    var voteDetails = await _voteDetailService.GetAllVoteDetailsForSabhaId(_token.sabhaId);
        //    var voteAssignmentDetails = await _voteAssignmentDetailsService.getCustomVoteWithSubLevelsForAccountId(accountId, _token);

        //    foreach (var customvote in voteAssignmentDetails)
        //    {
        //        customvote.voteAssignment.voteDetail = voteDetails?.FirstOrDefault(m => m.ID == customvote?.voteAssignment?.VoteId);
        //    }

        //    return Ok(voteAssignmentDetails);
        //}

        [HttpGet("getCustomVoteWithSubLevelsForAccountId/{accountId}")]
        public async Task<IActionResult> getCustomVoteWithSubLevelsForAccountId(int accountId)
        {
            var _token = new HTokenClaim
            {
                userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
            };

            // Get all vote details for the Sabha ID
            var voteDetails = await _voteDetailService.GetAllVoteDetailsForSabhaId(_token.sabhaId);

            // Get vote assignment details for the accountId
            var voteAssignmentDetails = await _voteAssignmentDetailsService.getCustomVoteWithSubLevelsForAccountId(accountId, _token);

            // Apply the vote details to the root level and recursively to all children
            foreach (var customvote in voteAssignmentDetails)
            {
                ApplyVoteDetailsToChildren(customvote, voteDetails);
            }

            return Ok(voteAssignmentDetails);
        }

        // Recursive method to apply vote details to all children
        private void ApplyVoteDetailsToChildren(HVoteAssignmentDetails voteAssignmentDetails, IEnumerable<VoteDetail> voteDetails)
        {
            // Apply vote detail to the current level
            voteAssignmentDetails.voteAssignment.voteDetail = voteDetails?.FirstOrDefault(m => m.ID == voteAssignmentDetails?.voteAssignment?.VoteId);

            // Recursively apply the vote detail to all child items
            if (voteAssignmentDetails.Children != null && voteAssignmentDetails.Children.Any())
            {
                foreach (var child in voteAssignmentDetails.Children)
                {
                    ApplyVoteDetailsToChildren(child, voteDetails);
                }
            }
        }


    }
}
