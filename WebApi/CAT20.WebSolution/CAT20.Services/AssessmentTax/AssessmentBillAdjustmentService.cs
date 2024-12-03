using AutoMapper;
using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Services.AssessmentTax;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.Vote;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.AssessmentTax
{
    public class AssessmentBillAdjustmentService : IAssessmentBillAdjustmentService
    {

        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        private readonly ILogger<AssessmentService> _logger;
        private readonly IMapper _mapper;
        private readonly IAssessmentBalanceService _assessmentBalanceService;
        private readonly IPartnerService _partnerService;
        private readonly IVoteBalanceService _voteBalanceService;

        public AssessmentBillAdjustmentService(ILogger<AssessmentService> logger, IMapper mapper, IAssessmentTaxUnitOfWork unitOfWork, IAssessmentBalanceService assessmentBalanceService, IPartnerService partnerService, IVoteBalanceService voteBalanceService)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _assessmentBalanceService = assessmentBalanceService;
            _partnerService = partnerService;
            _voteBalanceService = voteBalanceService;
        }

        public async Task<(bool, string?)> ApproveRejectBillAdjustment(AssessmentBillAdjustment assessmentBillAdjustment, HTokenClaim token)
        {
            try
            {
                if (assessmentBillAdjustment.DraftApproveRejectWithdraw == 2)
                {

                }

                if (assessmentBillAdjustment.DraftApproveRejectWithdraw == 3)
                {
                    var assessmentBillAdjustmentToUpdate = await _unitOfWork.AssessmentBillAdjustments.GetByIdAsync(assessmentBillAdjustment.Id);

                    if (assessmentBillAdjustmentToUpdate != null)
                    {

                    }
                    else
                    {
                        throw new GeneralException("Unable To Found");
                    }
                }

                return (true,"ok");
            }catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


        public async Task<(bool, string?)> CreateBillAdjustmentRequest(AssessmentBillAdjustment assessmentBillAdjustment, HTokenClaim token)
        {
            try
            {

                var assmt = await _unitOfWork.Assessments.GetByIdAsync(assessmentBillAdjustment.AssessmentId);

                if (assmt != null)
                {
                    assmt.HasBillAdjustmentRequest = true;
                    assessmentBillAdjustment.RequestDate = DateTime.Now;
                    assessmentBillAdjustment.RequestBy = token.userId;
                    assessmentBillAdjustment.DraftApproveRejectWithdraw = 1;
                    await _unitOfWork.AssessmentBillAdjustments.AddAsync(assessmentBillAdjustment);
                    await _unitOfWork.CommitAsync();

                    return (true, "Bill Adjustment Request Created Successfully");
                }
                else
                {
                    throw new GeneralException("Unable To Found");
                }

            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error in CreateBillAdjustmentRequest");
                return (false, "Error in CreateBillAdjustmentRequest");
            }
        }

        public async Task<(int totalCount, IEnumerable<AssessmentBillAdjustment> list)> GetAllBillAdjustmentForSabha(int sabhaId,int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {
            return await _unitOfWork.AssessmentBillAdjustments.GetAllBillAdjustmentForSabha(sabhaId,pageNo,pageSize,filterKeyword, token);
        }

        public async Task<(int totalCount, IEnumerable<AssessmentBillAdjustment> list)> GetAllPendingBillAdjustmentFor(int sabhaId, int pageNo, int pageSize, string? filterKeyword, HTokenClaim token)
        {
           return await _unitOfWork.AssessmentBillAdjustments.GetAllPendingBillAdjustmentFor(sabhaId, pageNo, pageSize, filterKeyword, token);
        }

        public async Task<(bool, string?)> WithdrawBillAdjustmentRequest(int assessmentBillAdjustmentId, HTokenClaim token)
        {
            try
            {
                var billAdjustment = await _unitOfWork.AssessmentBillAdjustments.GetById(assessmentBillAdjustmentId,token);

                if(billAdjustment != null)
                {

                    billAdjustment.DraftApproveRejectWithdraw = 0;
                    await _unitOfWork.CommitAsync();

                    return (true, "Bill Adjustment Request Withdrawn Successfully");

                }
                else
                {
                    throw new Exception("Bill Adjustment Request Not Found");
                }

            }
            catch (Exception ex)
            {

                return (false, "Error in WithdrawBillAdjustmentRequest");

            }


        }
    }
}
