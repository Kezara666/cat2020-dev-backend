using AutoMapper;
using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.DTO.Assessment.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;
using CAT20.Core.Services.AssessmentTax;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.Vote;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CAT20.Services.AssessmentTax
{
    public class AmalgamationSubDivisionService : IAmalgamationSubDivisionService
    {
        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        private readonly IAssessmentService _assessmentService;
        private readonly ILogger<AssessmentService> _logger;
        private readonly IMapper _mapper;
        private readonly IAssessmentBalanceService _assessmentBalanceService;
        private readonly IPartnerService _partnerService;
        private readonly IVoteBalanceService _voteBalanceService;

        public AmalgamationSubDivisionService(ILogger<AssessmentService> logger, IMapper mapper, IAssessmentTaxUnitOfWork unitOfWork,IAssessmentService assessmentService, IAssessmentBalanceService assessmentBalanceService, IPartnerService partnerService, IVoteBalanceService voteBalanceService)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _assessmentService = assessmentService;
            _assessmentBalanceService = assessmentBalanceService;
            _partnerService = partnerService;
            _voteBalanceService = voteBalanceService;
        }


        public async Task<(bool, string?)> CreateAmalgamation(SaveAssessmentAmalgamationResource saveAssessmentAmalgamationResources, HTokenClaim token)
        {
            // Enable asynchronous flow for the TransactionScope
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var derivedAssessment = _mapper.Map<SaveAmalgamationAssessmentResource, Assessment>(saveAssessmentAmalgamationResources.AmalgamationAssessment!);
                    var amalgamations = _mapper.Map<IEnumerable<SaveAmalgamationAssessmentResource>, IEnumerable<Amalgamation>>(saveAssessmentAmalgamationResources.Amalgamations!.ToList());

                    foreach (var assmt in amalgamations)
                    {
                        if (!await _unitOfWork.Assessments.ISKFormExist(assmt.KFormId!.Value, token))
                        {
                            throw new GeneralException($"Unable To Find Assessment For K-Form Id {assmt.Id!.Value}");
                        }
                    }

                    var newAmalgamationSubdivision = new AmalgamationSubDivision
                    {
                        SabhaId = token.sabhaId,
                        RequestedAction = saveAssessmentAmalgamationResources.RequestedAction,
                        CreatedAt = DateTime.Now,
                        CreatedBy = token.sabhaId,
                        Status = 1,
                        Type = 1,
                        Amalgamations = new List<Amalgamation>(),
                        AmalgamationSubDivisionActions = _mapper.Map<IEnumerable<SaveAmalgamationSubDivisionActionsResource>, IEnumerable<AmalgamationSubDivisionActions>>(saveAssessmentAmalgamationResources.AmalgamationSubDivisionActions!.ToList()).ToList(),
                    };



                    // Add the first set of changes

                    if (derivedAssessment.Id.HasValue)
                    {
                        var foundAssessment = await _unitOfWork.Assessments.GetByIdAsync(derivedAssessment.Id!.Value);
                        if (foundAssessment == null) {
                        
                            throw new GeneralException("Unable To Found Derived Assessment");
                        }
                      

                    }
                    else
                    {
                        derivedAssessment.Allocation!.NextYearAllocationAmount = derivedAssessment.Allocation.AllocationAmount;
                        derivedAssessment.Allocation.AllocationAmount = 0m;
                        derivedAssessment.StreetId = saveAssessmentAmalgamationResources.Amalgamations!.FirstOrDefault()!.StreetId;
                        derivedAssessment.OfficeId = saveAssessmentAmalgamationResources.Amalgamations!.FirstOrDefault()!.OfficeId;
                        derivedAssessment.SabhaId = saveAssessmentAmalgamationResources.Amalgamations!.FirstOrDefault()!.SabhaId;
                        derivedAssessment.AssessmentStatus = AssessmentStatus.Temporary;
                        derivedAssessment.IsWarrant = false;
                        derivedAssessment.CreatedAt = DateTime.Now;
                        derivedAssessment.CreatedBy = token.userId;

                        await _unitOfWork.Assessments.AddAsync(derivedAssessment);

                        if (await _unitOfWork.Assessments.ISAssessmentNoExist(derivedAssessment.AssessmentNo, derivedAssessment.StreetId!.Value, token))
                        {

                            throw new GeneralException($"{derivedAssessment.AssessmentNo} Assessment No Already Exist ");
                        }

                    }

                    await _unitOfWork.StepAsync(); // This will now be part of the TransactionScope

                    newAmalgamationSubdivision.Amalgamations = amalgamations.ToList();
                    newAmalgamationSubdivision.DerivedAssessmentId = derivedAssessment.Id;

                    // Add the second set of changes
                    await _unitOfWork.AmalgamationSubDivision.AddAsync(newAmalgamationSubdivision);
                    await _unitOfWork.StepAsync(); // This will now be part of the TransactionScope

                    // Complete the transaction if all operations succeed
                    transactionScope.Complete();

                    return (true, "Successfully Submitted Amalgamation Request");
                }
                catch (Exception ex)
                {
                    // The transaction will be rolled back automatically if an exception occurs
                    return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
                }
            }
        }

        public async Task<(bool, string?)> CreateSubDivisions(SaveAssessmentSubDivisionResource saveAssessmentSubDivisionResource, HTokenClaim token)
        {
            // Enable asynchronous flow for the TransactionScope
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {


                    var subdivisionAssessments = _mapper.Map<IEnumerable<SaveSubDivisionAssessmentResource>, IEnumerable<Assessment>>(saveAssessmentSubDivisionResource.SubDivisions!.ToList());


                    var parentAssessment = await _unitOfWork.Assessments.GetForAmalgamationOrSubdivision(subdivisionAssessments.FirstOrDefault().ParentAssessmentId, token);

                    if (parentAssessment != null)
                    {
                        var newAmalgmationSubdivision = new AmalgamationSubDivision
                        {
                            ParentAssessmentId = parentAssessment.Id,
                            SabhaId = parentAssessment.SabhaId,
                            RequestedAction = saveAssessmentSubDivisionResource.RequestedAction,
                            CreatedAt = DateTime.Now,
                            CreatedBy = token.sabhaId,
                            Status = 1,
                            Type = 2,
                            SubDivisions = new List<SubDivision>(),
                            AmalgamationSubDivisionActions = _mapper.Map<IEnumerable<SaveAmalgamationSubDivisionActionsResource>, IEnumerable<AmalgamationSubDivisionActions>>(saveAssessmentSubDivisionResource.AmalgamationSubDivisionActions!.ToList()).ToList(),
                        };

                        foreach (var asmt in subdivisionAssessments)
                        {
                            asmt.Allocation.NextYearAllocationAmount = asmt.Allocation.AllocationAmount;
                            asmt.Allocation.AllocationAmount = 0m;

                            if (await _unitOfWork.Assessments.ISAssessmentNoExist(asmt.AssessmentNo, parentAssessment.StreetId!.Value, token))
                            {

                                throw new GeneralException($"{asmt.AssessmentNo} Assessment No Already Exist ");
                            }


                            asmt.SabhaId = parentAssessment.SabhaId;
                            asmt.OfficeId = parentAssessment.OfficeId;
                            asmt.PartnerId = 0;
                            asmt.SubPartnerId = 0;
                            asmt.PropertyAddress = parentAssessment.PropertyAddress;
                            asmt.StreetId = parentAssessment.StreetId;
                            asmt.AssessmentStatus = AssessmentStatus.Temporary;
                            asmt.IsWarrant = false;
                            asmt.CreatedAt = DateTime.Now;
                            asmt.CreatedBy = token.userId;

                        }


                        await _unitOfWork.Assessments.AddRangeAsync(subdivisionAssessments);

                        await _unitOfWork.StepAsync();

                        foreach (var asmt in subdivisionAssessments)
                        {

                            var newSubDivision = new SubDivision
                            {
                                Id = null,
                                KFormId = asmt.Id,

                            };

                            newAmalgmationSubdivision.SubDivisions!.Add(newSubDivision);
                        }

                        await _unitOfWork.AmalgamationSubDivision.AddAsync(newAmalgmationSubdivision);

                        await _unitOfWork.StepAsync();



                        transactionScope.Complete();
                        return (true, "Successfully Submit Subdivision Request");
                    }
                    else
                    {
                        throw new GeneralException($"Unable To Found Assessment For K-Form Id{subdivisionAssessments.FirstOrDefault().ParentAssessmentId}");
                    }


                }
                catch (Exception ex)
                {

                    return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);

                }
            }
        }

        public async Task<(int totalCount, IEnumerable<AmalgamationSubDivision> list)> GetAllAmalgamationSubDivisionForSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {
            return await _unitOfWork.AmalgamationSubDivision.GetAllAmalgamationSubDivisionForSabha(sabhaId, pageNo, pageSize, filterKeyword, token);
        }

        public async Task<(int totalCount, IEnumerable<AmalgamationSubDivision> list)> GetAllPendingAmalgamationSubDivisionForSabha(int sabhaId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {
            return await _unitOfWork.AmalgamationSubDivision.GetAllPendingAmalgamationSubDivisionForSabha(sabhaId, pageNo, pageSize, filterKeyword, token);
        }


        public async Task<(bool, string?)> ApproveRejectAmalgamationSubdivisions(SaveAmalgamationSubDivisionActionsResource actionsResource, HTokenClaim token)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
            {

                if (actionsResource.ActionState == 2)
                {
                    var amalgmationSubdivision = await _unitOfWork.AmalgamationSubDivision.GetById(actionsResource.AmalgamationSubDivisionId!.Value, token);

                    if (amalgmationSubdivision != null)
                    {




                        if (amalgmationSubdivision.Type == 1) // Amalgamation   
                        {
                            var derivedAssessment = await _unitOfWork.Assessments.GetByIdAsync(amalgmationSubdivision.DerivedAssessmentId!.Value);

                            if (derivedAssessment != null)
                            {
                                derivedAssessment.AssessmentStatus = AssessmentStatus.NextYearActive;

                                await _assessmentService.InitNextYearBalance(null, null, derivedAssessment.Id, token);
                            }
                            else
                            {

                                throw new GeneralException("Unable To Found Derived Assessment");
                            }

                            foreach (var amalgamation in amalgmationSubdivision.Amalgamations!)
                            {
                                var amalgamationAssessment = await _unitOfWork.Assessments.GetByIdAsync(amalgamation.KFormId!.Value);

                                if (amalgamationAssessment != null)
                                {
                                    amalgamationAssessment.AssessmentStatus = amalgmationSubdivision.RequestedAction;

                                }
                                else
                                {
                                    throw new GeneralException("Unable To Found Amalgamation Assessment");
                                }

                            }


                        }
                        else if (amalgmationSubdivision.Type == 2) // SubDivision
                        {
                            var parentAssessment = await _unitOfWork.Assessments.GetById(amalgmationSubdivision.ParentAssessmentId!.Value);

                            if (parentAssessment != null)
                            {

                                    parentAssessment.AssessmentStatus = amalgmationSubdivision.RequestedAction;

                                    var tempPartner = parentAssessment.AssessmentTempPartner;
                                    var tempSubPartners = parentAssessment.AssessmentTempSubPartner;

                                   


                                    var partner = parentAssessment.IsPartnerUpdated ? await _partnerService.GetById(parentAssessment.PartnerId) : null;


                                    foreach (var subDivision in amalgmationSubdivision.SubDivisions!)
                                    {
                                        var subDivisionAssessment = await _unitOfWork.Assessments.GetByIdAsync(subDivision.KFormId!.Value);

                                        if (subDivisionAssessment != null)
                                        {
                                            subDivisionAssessment.AssessmentStatus = AssessmentStatus.NextYearActive;

                                            subDivisionAssessment.AssessmentTempPartner = (AssessmentTempPartner)tempPartner!.RestClone(subDivisionAssessment.Id!.Value);
                                   

                                            subDivisionAssessment.AssessmentTempSubPartner = new List<AssessmentTempSubPartner>();

                                            foreach (var subPartner in tempSubPartners)
                                            {
                                                subDivisionAssessment.AssessmentTempSubPartner!.Add(subPartner.RestClone(subDivisionAssessment.Id!.Value) as AssessmentTempSubPartner);
                                            }


                                            if (parentAssessment.IsPartnerUpdated)
                                            {
                                                subDivisionAssessment.PartnerId = partner!.Id;



                                            }

                                            //await _assessmentService.InitNextYearBalance(null, null, subDivisionAssessment.Id, token);

                                        }
                                        else
                                        {
                                            throw new GeneralException("Unable To Found SubDivision Assessment");
                                        }

                                    }



                                }
                                else
                            {

                                throw new GeneralException("Unable To Found Parent Assessment");
                            }
                        }
                        else
                        {
                            throw new GeneralException("Invalid Amalgamation Subdivision Type");
                        }

                        var newAction = new AmalgamationSubDivisionActions
                        {
                            Id=null,
                            AmalgamationSubDivisionId = amalgmationSubdivision.Id,
                            ActionState = actionsResource.ActionState,
                            ActionBy = token.userId,
                            SystemActionAt = DateTime.Now,
                            Comment = actionsResource.Comment,
                        };

                        amalgmationSubdivision.Status = 2;
                        await _unitOfWork.AmalgamationSubDivisionActions.AddAsync(newAction);
                        await _unitOfWork.StepAsync();
                        transactionScope.Complete();
                        return (true, "Successfully Approved Amalgamation Subdivision");

                    }
                    else
                    {
                        throw new GeneralException("Unable To Found  Amalgamation Or SubDivision");
                    }

                }
                else
                {
                    var amalgmationSubdivision = await _unitOfWork.AmalgamationSubDivision.GetById(actionsResource.AmalgamationSubDivisionId!.Value, token);

                    if (amalgmationSubdivision != null)
                    {

                        foreach (var subDivision in amalgmationSubdivision.SubDivisions!)
                        {
                            var subDivisionAssessment = await _unitOfWork.Assessments.GetByIdAsync(subDivision.KFormId!.Value);

                            if (subDivisionAssessment != null)
                            {
                                subDivisionAssessment.AssessmentStatus = AssessmentStatus.Delete;

                            }
                            else
                            {
                                throw new GeneralException("Unable To Found SubDivision Assessment");
                            }


                        }

                        amalgmationSubdivision.Status = -2;

                        var newAction = new AmalgamationSubDivisionActions
                        {
                            AmalgamationSubDivisionId = amalgmationSubdivision.Id,
                            ActionState = actionsResource.ActionState,
                            ActionBy = token.userId,
                            SystemActionAt = DateTime.Now,
                            Comment = actionsResource.Comment,
                        };


                        await _unitOfWork.AmalgamationSubDivisionActions.AddAsync(newAction);


                        //amalgmationSubdivision.Status = -2;
                        await _unitOfWork.CommitAsync();
                        return (true, "Successfully Rejected Amalgamation Subdivision");


                    }
                    else
                    {
                        throw new GeneralException("Unable To Found  Amalgamation Or SubDivision");
                    }
                }
            }
            catch (Exception ex)
            {

                return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);

            }
        }
        }
    }
}
