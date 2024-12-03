using AutoMapper;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Services.AssessmentTax;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.Services.Mixin;
using CAT20.Services.FinalAccount;
using CAT20.WebApi.Resources.Control;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CAT20.WebApi.Controllers.Control
{

    [Route("api/mixin/sessions")]
    [ApiController]
    public class SessionsController : BaseController
    {
        private readonly ISessionService _sessionService;
        private readonly IOfficeService _officeService;
        private readonly IMapper _mapper;
        private readonly IMixinOrderService _mixinOrderService;
        private readonly IBusinessTaxService _businessTaxService;
        private readonly IDocumentSequenceNumberService _documentSequenceNumberService;
        private readonly IAssessmentEndSessionService _assessmentEndSession;

        public SessionsController( ISessionService sessionService, IOfficeService officeService, IMapper mapper, IMixinOrderService mixinOrderService, IBusinessTaxService businessTaxService, IDocumentSequenceNumberService documentSequenceNumberService, IAssessmentEndSessionService assessmentEndSession)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _officeService = officeService;
            _mixinOrderService = mixinOrderService;
            _businessTaxService = businessTaxService;
            _documentSequenceNumberService = documentSequenceNumberService;
            _assessmentEndSession = assessmentEndSession;
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<ActionResult<SessionResource>> GetById([FromRoute] int id)
        {
            var session = await _sessionService.GetById(id);
            var sessionResource = _mapper.Map<Session, SessionResource>(session);

            if (sessionResource == null)
                return NotFound();

            return Ok(sessionResource);
        }


        [HttpGet]
        [Route("getByOfficeAndModule/{officeid}/{module}")]
        public async Task<ActionResult<SessionResource>> GetByOfficeAndModule(int officeid, string module)
        {
            //var session = await _sessionService.GetByOfficeAndModule(officeid, module);//removed module parameter due to no need of different sessions for module. only one active session for a office
            var session = await _sessionService.GetActiveSessionByOffice(officeid);
            var sessionResource = _mapper.Map<Session, SessionResource>(session);

            if (sessionResource == null)
                return NotFound();
            return Ok(sessionResource);
        }
        [HttpGet]
        [Route("getAnyByOfficeAndModule/{officeid}/{module}")]
        public async Task<ActionResult<SessionResource>> GetAnyByOfficeAndModule(int officeid, string module)
        {
            //var session = await _sessionService.GetByOfficeAndModule(officeid, module);//removed module parameter due to no need of different sessions for module. only one active session for a office
            var session = await _sessionService.GetAnyByOfficeAndModule(officeid, module);
            var sessionResource = _mapper.Map<Session, SessionResource>(session);

            if (sessionResource == null)
                return NotFound();

            return Ok(sessionResource);
        }

        [HttpGet]
        [Route("GetActiveOrLastActiveSessionForOnlinePaymentCurrentMonth/{officeid}/{module}")]
        public async Task<ActionResult<SessionResource>> GetActiveOrLastActiveSessionForOnlinePaymentCurrentMonth(int officeid, string module)
        {
            //var session = await _sessionService.GetByOfficeAndModule(officeid, module);//removed module parameter due to no need of different sessions for module. only one active session for a office
            var session = await _sessionService.GetActiveOrLastActiveSessionForOnlinePaymentCurrentMonth(officeid, module);
            var sessionResource = _mapper.Map<Session, SessionResource>(session);

            if (sessionResource == null)
                return NotFound();

            return Ok(sessionResource);
        }

        [HttpGet]
        [Route("getByOfficeModuleAndDate/{officeid}/{module}/{date}")]
        public async Task<ActionResult<SessionResource>> GetByOfficeModuleAndDate(int officeid, string module, DateTime date)
        {
            //var session = await _sessionService.GetByOfficeModuleAndDate(officeid, module, date); //removed due to only one active session for office
            var session = await _sessionService.GetByOfficeAndDate(officeid, date);
            var sessionResource = _mapper.Map<Session, SessionResource>(session);

            if (sessionResource == null)
                return NotFound();

            return Ok(sessionResource);
        }


        [HttpGet]
        [Route("getAllActiveSessionsByOffice/{officeId}")]
        public async Task<ActionResult> GetAllActiveSessionsByOffice(int officeId)
        {
            var sessions = await _sessionService.GetAllActiveSessionsByOffice(officeId);
            var sessionResources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);

            return Ok(sessionResources);
        }

        [HttpGet]
        [Route("getAllSessionsByOfficeAndModule/{officeId}/{module}")]
        public async Task<ActionResult> GetAllSessionsByOfficeAndModule(int officeId, string module)
        {
            var sessions = await _sessionService.GetAllSessionsByOfficeAndModule(officeId, module);
            var sessionResources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);

            return Ok(sessionResources);
        }

        [HttpGet]
        [Route("getAllSessionsByOffice/{officeId}")]
        public async Task<ActionResult> GetAllSessionsByOffice(int officeId)
        {
            var sessions = await _sessionService.GetAllSessionsByOffice(officeId);
            var sessionResources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);

            return Ok(sessionResources);
        }

        [HttpGet]
        [Route("GetActiveSessionByOfficeAndModule/{officeId}/{module}")]
        public async Task<ActionResult> GetActiveSessionByOfficeAndModule(int officeId, string module)
        {
            var sessions = await _sessionService.GetActiveSessionByOfficeAndModule(officeId, module);
            var sessionResources = _mapper.Map<Session, SessionResource>(sessions);

            return Ok(sessionResources);
        }


        [HttpPost("startSession")]
        public async Task<IActionResult> StartSession(SessionResource obj)
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

                if (obj != null && obj.Id == 0)
                {
                    var sessionResource = obj;

                    var sessions = await _sessionService.GetActiveSessionByOfficeAndModule(obj.OfficeId, obj.Module);
                    var sessionResources = _mapper.Map<Session, SessionResource>(sessions);

                    var office = await _officeService.GetOfficeById(obj.OfficeId);

                    if (sessionResources == null)
                    {
                        sessionResource.Name = obj.Module + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH:mm");
                        sessionResource.CreatedAt = DateTime.Now;
                        sessionResource.StartAt = DateTime.Now;
                        sessionResource.CreatedBy = obj.CreatedBy;
                        sessionResource.OfficeId = obj.OfficeId;
                        sessionResource.Name = obj.Name;
                        sessionResource.Module = obj.Module;
                        sessionResource.Active = 1;

                        var sessionToCreate = _mapper.Map<SessionResource, Session>(sessionResource);
                        var result = await _sessionService.Create(sessionToCreate,_token);


                        if (result.Item1)
                        {
                            return Ok(new ApiResponseModel<SessionResource>
                            {
                                Status = 200,
                                Message = result.Item2!,
                                Data = _mapper.Map<Session, SessionResource>(result.Item3),
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

                        //var session = await _sessionService.GetById(newSession.Id);
                        //sessionResource = _mapper.Map<Session, SessionResource>(session);



                        return Ok(sessionResource);
                    }
                    else
                    {
                        return BadRequest("Please Close the existing active session");
                    }

                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return NotFound();
        }

        [HttpPost("startCustomSession")]
        public async Task<IActionResult> StartCustomSession(SessionResource obj)
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


                if (obj != null && obj.Id == 0)
                {
                    var sessionResource = obj;

                    //var sessions = await _sessionService.GetActiveSessionByOfficeAndModule(obj.OfficeId, obj.Module);
                    var sessions = await _sessionService.GetActiveSessionByOffice(obj.OfficeId);
                    var sessionResources = _mapper.Map<Session, SessionResource>(sessions);

                    var office = await _officeService.GetOfficeById(obj.OfficeId);

                    if (sessionResources == null)
                    {
                        sessionResource.Name = obj.Module + "-" + obj.StartAt.ToString("yyyy-MM-dd-HH:mm");
                        sessionResource.CreatedAt = DateTime.Now;
                        sessionResource.StartAt = obj.StartAt;
                        sessionResource.CreatedBy = obj.CreatedBy;
                        sessionResource.OfficeId = obj.OfficeId;
                        sessionResource.Name = obj.Name;
                        sessionResource.Module = obj.Module;
                        sessionResource.Active = 1;
                        sessionResource.Rescue = 1;
                        sessionResource.RescueStartedAt = DateTime.Now;

                        var sessionToCreate = _mapper.Map<SessionResource, Session>(sessionResource);
                        var result = await _sessionService.Create(sessionToCreate,_token);

                        if (result.Item1)
                        {
                            return Ok(new ApiResponseModel<SessionResource>
                            {
                                Status = 200,
                                Message = result.Item2!,
                                Data = _mapper.Map<Session, SessionResource>(result.Item3),
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
                                Status = 500,
                                Message = "Internal Server Error"
                            });
                        }

                        //var session = await _sessionService.GetById(newSession.Id);
                        //sessionResource = _mapper.Map<Session, SessionResource>(session);

                        //return Ok(sessionResource);
                    }
                    else
                    {
                        return BadRequest("Please Close the existing active session");
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return NotFound();
        }


        [HttpPost("endSession")]
        public async Task<IActionResult> EndSession(SessionResource obj)
        {
            try
            {
                var updatedSessionResource = new SessionResource();
                if (obj.Id != null)
                {
                    obj.UpdatedAt = DateTime.Now;
                    obj.StopAt = DateTime.Now;
                    obj.Rescue = 0;
                    var sessionToBeUpdate = await _sessionService.GetById(obj.Id);

                    if (sessionToBeUpdate == null)
                        return NotFound();

                    var mixinCAncelPendingOrders = await _mixinOrderService.GetAllForOfficeAndStateAndDate(obj.OfficeId, OrderStatus.Cancel_Pending, obj.StartAt);
                    if (mixinCAncelPendingOrders.Count() > 0)
                    {
                        return BadRequest();
                    }

                    //updated order statuses on day end //session end
                    var mixinDraftOrders = await _mixinOrderService.GetAllForOfficeAndState(obj.OfficeId, OrderStatus.Draft);
                    foreach (var order in mixinDraftOrders)
                    {
                        if (order.State == OrderStatus.Draft)
                        {
                            await _mixinOrderService.DeleteOrder(order, order.OfficeId.Value);

                            if (order.BusinessId != 0)
                            {
                                try
                                {
                                    var businessTaxToBeUpdate = await _businessTaxService.GetById(order.BusinessTaxId.Value);
                                    var businestaxline = businessTaxToBeUpdate;
                                    businestaxline.TaxState = TaxStatus.Draft;

                                    await _businessTaxService.Update(businessTaxToBeUpdate, businestaxline);
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                        }
                    }
                    var mixinPaidOrders = await _mixinOrderService.GetAllForOfficeAndState(obj.OfficeId, OrderStatus.Paid);
                    foreach (var order in mixinPaidOrders)
                    {
                        if (order.State == OrderStatus.Paid)
                        {
                            await _mixinOrderService.PostOrder(order, order.OfficeId.Value);
                        }
                    }

                    //end

                    var session = _mapper.Map<SessionResource, Session>(obj);
                   var result =  await _sessionService.EndSession(sessionToBeUpdate, session);
                    //var updatedSession = await _sessionService.GetById(obj.Id);
                    //updatedSessionResource = _mapper.Map<Session, SessionResource>(updatedSession);

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
                            Status = 500,
                            Message = "Internal Server Error"
                        });
                    }

                }
                //await _assessmentEndSession.EndSessionDisableTransaction(obj.OfficeId!);
                return Ok(updatedSessionResource);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("allowReceiptsForExpiredSession")]
        public async Task<IActionResult> AllowReceiptsForExpiredSession(SessionResource obj)
        {
            try
            {
                var updatedSessionResource = new SessionResource();
                if (obj.Id != null)
                {
                    obj.RescueStartedAt = DateTime.Now;
                    obj.Rescue = 1;
                    var sessionToBeUpdate = await _sessionService.GetById(obj.Id);

                    if (sessionToBeUpdate == null)
                        return NotFound();

                    var session = _mapper.Map<SessionResource, Session>(obj);
                    await _sessionService.AllowReceiptsForExpiredSession(sessionToBeUpdate, session);
                    var updatedSession = await _sessionService.GetById(obj.Id);
                    updatedSessionResource = _mapper.Map<Session, SessionResource>(updatedSession);

                }
                return Ok(updatedSessionResource);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("getSessionDate/{sessionId}")]
        public async Task<IActionResult> GetSessionDate(int? sessionId)
        {
            if (sessionId.HasValue && sessionId > 0)
            {
                DateTime? sessionDate = await _sessionService.IsRescueSessionThenDate(sessionId.Value);

                if (sessionDate.HasValue)
                {
                    return Ok(sessionDate.Value); // Return the session date as a successful response
                }
                else
                {
                    return NotFound(); // Or return NotFound if the session date is not available
                }
            }

            return BadRequest(); // Return BadRequest if the sessionId is not valid
        }



        [HttpGet]
        [Route("getLastEndedSessionByOffice/{officeId}/{module}")]
        public async Task<ActionResult<SessionResource>> GetLastEndedSessionByOffice([FromRoute] int officeId, string Module)
        {
            var session = await _sessionService.GetLastEndedSessionByOfficeAsync(officeId, Module);
            var sessionResource = _mapper.Map<Session, SessionResource>(session);

            if (sessionResource == null)
                return NotFound();

            return Ok(sessionResource);
        }


        [HttpGet]
        [Route("getLast2SessionsForOffice/{officeId}")]
        public async Task<ActionResult> GetLast2SessionsForOffice(int officeId)
        {
            var sessions = await _sessionService.GetLast2SessionsForOffice(officeId);
            var sessionResources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);

            return Ok(sessionResources);
        }

        [HttpGet]
        [Route("getAllSessionsForOfficeByYearMonth/{officeId}/{year}/{month}")]
        public async Task<ActionResult> GetAllSessionsForOfficeByYearMonth(int officeId, int year, int month)
        {
            var sessions = await _sessionService.GetAllSessionsForOfficeByYearMonth(officeId, year, month);
            var sessionResources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);

            return Ok(sessionResources);
        }
    }

}