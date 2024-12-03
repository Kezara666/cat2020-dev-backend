using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.Models.Vote;
using CAT20.WebApi.Resources.Vote.Save;
using CAT20.WebApi.Resources.Vote;
using CAT20.Core.Services.Vote;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CAT20.WebApi.Controllers.Control;
using CAT20.Core.HelperModels;
using CAT20.Core.DTO;
using CAT20.Core.DTO.Final;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.DTO.Vote;
using CAT20.WebApi.Resources.Pagination;
using CAT20.Core.DTO.Vote.Save;

namespace CAT20.Api.Controllers
{
    [Route("api/vote/voteAllocations")]
    [ApiController]
    public class VoteAllocationsController : BaseController
    {
        private readonly IVoteBalanceService _voteAllocationService;
        private readonly IMapper _mapper;

        public VoteAllocationsController(IVoteBalanceService voteAllocationService, IMapper mapper)
        {
            this._mapper = mapper;
            this._voteAllocationService = voteAllocationService;
        }

        [HttpGet("getAllVoteAllocations")]
        public async Task<ActionResult<IEnumerable<VoteBalance>>> GetAllVoteAllocations()
        {
            var voteAllocations = await _voteAllocationService.GetAllVoteAllocations();
            var voteAllocationResources = _mapper.Map<IEnumerable<VoteBalance>, IEnumerable<VoteBalanceResource>>(voteAllocations);

            return Ok(voteAllocationResources);
        }

        [HttpGet("getVoteAllocationById")]
        public async Task<ActionResult<VoteBalanceResource>> GetVoteAllocationById(int id)
        {
            var voteAllocation = await _voteAllocationService.GetVoteAllocationById(id);
            var voteAllocationResource = _mapper.Map<VoteBalance, VoteBalanceResource>(voteAllocation);
            return Ok(voteAllocationResource);
        }
        
        [HttpGet("getVoteAllocationByVoteDetailId/{id}")]
        public async Task<ActionResult<VoteBalanceResource>> GetVoteAllocationByVoteDetailId([FromRoute] int id)
        {
            var voteAllocation = await _voteAllocationService.getVoteAllocationByVoteDetailId(id);
            var voteAllocationResource = _mapper.Map<VoteBalance, VoteBalanceResource>(voteAllocation);
            return Ok(voteAllocationResource);
        }


        [HttpGet("getActiveVoteAllocationByVoteDetailId/{id}")]
        public async Task<ActionResult<VoteBalanceResource>> GetActiveVoteAllocationByVoteDetailId([FromRoute] int id)
        {
            try
            {
                var _token = new HTokenClaim
                {

                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                };

                var result = await _voteAllocationService.GetActiveVoteBalance(id,_token);

                if(result.Item1 != null)
                {
                    var voteAllocationResource = _mapper.Map<VoteBalance, VoteBalanceResource>(result.Item1);
                    return Ok(new ApiResponseModel<VoteBalanceResource>
                    {
                        Status = 200,
                        Message = result.Item2,
                        Data = voteAllocationResource
                    });

                }
                else  if(result.Item1 == null && result.Item2 == null)
                {
                    return Ok(new ApiResponseModel<object>
                    {
                        Status = 400,
                        Message = "Ensure That the Vote Has an Allocated Amount."
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
               


            }catch (Exception e)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Status = 500,
                    Message = "Internal Server Error!!."
                });
            }
        }


        [HttpPost("saveVoteAllocation")]
        public async Task<ActionResult<VoteBalanceResource>> CreateVoteAllocation([FromBody] SaveVoteAllocationResource saveVoteAllocationResource)
        {
            try
            {
                //var validator = new SaveVoteAllocationResourceValidator();
                //var validationResult = await validator.ValidateAsync(saveVoteAllocationResource);

                //if (!validationResult.IsValid)
                //    return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

                var _token = new HTokenClaim
                {

                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                };



                if (saveVoteAllocationResource.IsAllocationOrEstimateIncomeGreaterThanZero)
                {



                    var voteAllocationToCreate =
                        _mapper.Map<SaveVoteAllocationResource, VoteBalance>(saveVoteAllocationResource);

                    var result = await _voteAllocationService.CreateVoteAllocation(voteAllocationToCreate,_token);

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
                    return BadRequest();    
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [HttpPost("takeHold")]

        public async Task<ActionResult<VoteBalanceResource>> TakeHold([FromBody] SaveVoteBalanceTakeHold saveVoteBalanceTakeHold)
        {
            try
            {
                var _token = new HTokenClaim
                {

                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                };



                var result = await _voteAllocationService.TakeHold(saveVoteBalanceTakeHold, _token);



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
                        Message = "Failed to take hold"
                    });
                }



            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPost("releaseHold")]
        public async Task<ActionResult<VoteBalanceResource>> ReleaseHold(ReleaseVoteBalanceTakeHold releaseVoteBalanceTake)
        {
            try
            {
                var _token = new HTokenClaim
                {

                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                };

                var result = await _voteAllocationService.ReleaseTakeHold(releaseVoteBalanceTake, _token);

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
                        Message = "Failed to release amount"
                    });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });

            }
        }



        [HttpPost("updateVoteAllocation")]
        public async Task<ActionResult<VoteBalanceResource>> UpdateVoteAllocation(SaveVoteAllocationResource saveVoteAllocationResource)
        {
            try
            {
                //var validator = new SaveVoteAllocationResourceValidator();
                //var validationResult = await validator.ValidateAsync(saveVoteAllocationResource);

                //if (!validationResult.IsValid)
                //    return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

                var _token = new HTokenClaim
                {

                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                };



                if (saveVoteAllocationResource.IsAllocationOrEstimateIncomeGreaterThanZero)
                {



                    var voteAllocationToCreate =
                        _mapper.Map<SaveVoteAllocationResource, VoteBalance>(saveVoteAllocationResource);

                    var result = await _voteAllocationService.UpdateVoteAllocation(voteAllocationToCreate, _token);

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
                    return BadRequest();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        [Route("deleteVoteAllocation/{id}")]
        public async Task<IActionResult> DeleteVoteAllocation([FromRoute]int id)
        {
            if (id == 0)
                return BadRequest();

            var voteAllocation = await _voteAllocationService.GetVoteAllocationById(id);

            if (voteAllocation == null)
                return NotFound();

            await _voteAllocationService.DeleteVoteAllocation(voteAllocation);

            return NoContent();
        }

        [HttpGet]
        [Route("getVoteAllocationForSabhaByYearByProgramByVoteDetail/{SabhaId}/{year}/{classificationId}")]
        public async Task<ActionResult<IEnumerable<VoteBalance>>> GetVoteAllocationForSabhaByYearAndProgram([FromRoute] int sabhaId, [FromRoute] int year, [FromRoute] int classificationId, [FromQuery] int? programId, [FromQuery] int? voteDetailId, [FromQuery] int pageNo, [FromQuery] int pageSize, [FromQuery] string? filterKeyword)
        {

            var _token = new HTokenClaim
            {

                userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
            };

            var result = await _voteAllocationService.GetVoteAllocationForSabhaByYearAndProgram(sabhaId,year,classificationId, programId,voteDetailId,pageNo,pageSize,filterKeyword,_token);
            var voteAllocationResources = _mapper.Map<IEnumerable<VoteBalance>, IEnumerable<VoteBalanceResource>>(result.list);



            var model = new ResponseModel<VoteBalanceResource>
            {
                totalResult = result.totalCount,
                list = voteAllocationResources
            };

            return Ok(model);

        }

        [HttpGet]
        [Route("GetAllVoteAllocationByVoteDetailIdAsync/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<VoteBalance>>> GetAllWithVoteAllocationByVoteDetailIdAsync([FromRoute] int id)
        {
            var voteAllocations = await _voteAllocationService.GetAllWithVoteAllocationByVoteDetailIdAsync(id);
            var voteAllocationResources = _mapper.Map<IEnumerable<VoteBalance>, IEnumerable<VoteBalanceResource>>(voteAllocations);

            return Ok(voteAllocationResources);
        }


        [HttpGet]
        [Route("getAllVoteAllocationsForVoteDetailIdandSabhaId/{VoteDetailId}/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<VoteBalance>>> GetAllWithVoteAllocationByVoteDetailIdSabhaId([FromRoute] int VoteDetailId, [FromRoute] int SabhaId)
        {
            var voteAllocations = await _voteAllocationService.GetAllWithVoteAllocationByVoteDetailIdSabhaId(VoteDetailId, SabhaId);
            var voteAllocationResources = _mapper.Map<IEnumerable<VoteBalance>, IEnumerable<VoteBalanceResource>>(voteAllocations);

            return Ok(voteAllocationResources);
        }

        [HttpGet]
        [Route("getAllVoteAllocationsForVoteDetailIdandSabhaIdandYear/{VoteDetailId}/{SabhaId}/{Year}")]
        public async Task<ActionResult<IEnumerable<VoteBalance>>> GetAllVoteAllocationsForVoteDetailIdandSabhaIdandYear([FromRoute] int VoteDetailId, [FromRoute] int SabhaId, [FromRoute] int Year)
        {
            var voteAllocations = await _voteAllocationService.GetAllVoteAllocationsForVoteDetailIdandSabhaIdandYear(VoteDetailId, SabhaId, Year);
            var voteAllocationResources = _mapper.Map<IEnumerable<VoteBalance>, IEnumerable<VoteBalanceResource>>(voteAllocations);

            return Ok(voteAllocationResources);
        }

        [HttpGet]
        [Route("getAllWithVoteAllocationBySabhaId/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<VoteBalance>>> GetAllWithVoteAllocationBySabhaId([FromRoute] int SabhaId)
        {
            var voteAllocations = await _voteAllocationService.GetAllWithVoteAllocationBySabhaId(SabhaId);
            var voteAllocationResources = _mapper.Map<IEnumerable<VoteBalance>, IEnumerable<VoteBalanceResource>>(voteAllocations);

            return Ok(voteAllocationResources);
        }


        [HttpGet("getAllWithVoteAllocationHasTakeHoldBySabha/{SabhaId}")]
        public async Task<ActionResult<IEnumerable<VoteBalance>>> GetAllWithVoteAllocationHasTakeHoldBySabha([FromRoute] int sabhaId,  [FromQuery] int pageNo, [FromQuery] int pageSize, [FromQuery] string? filterKeyword)
        {
            var voteAllocations = await _voteAllocationService.GetAllWithVoteAllocationHasTakeHoldBySabhaIdAsync(sabhaId,pageNo,pageSize,filterKeyword);
            var voteAllocationResources = _mapper.Map<IEnumerable<VoteBalance>, IEnumerable<VoteBalanceResource>>(voteAllocations.list);


            var model = new ResponseModel<VoteBalanceResource>
            {
                totalResult = voteAllocations.totalCount,
                list = voteAllocationResources
            };

            return Ok(model);
        }

        [HttpPost("saveComparativeFiguresBalance")]
        public async Task<ActionResult<VoteBalanceResource>> saveComparativeFiguresBalance([FromBody] List<saveCompartiveFigureBalance> figureBalance)
        {
            try
            {
                //var validator = new SaveVoteAllocationResourceValidator();
                //var validationResult = await validator.ValidateAsync(saveVoteAllocationResource);

                //if (!validationResult.IsValid)
                //    return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

                var _token = new HTokenClaim
                {

                    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
                    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
                    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
                };



              


                    //var voteAllocationToCreate =
                        //_mapper.Map<SaveVoteAllocationResource, VoteBalance>(saveVoteAllocationResource);

                    var result = await _voteAllocationService.SaveComparativeFiguresBalance(figureBalance, _token);

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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
