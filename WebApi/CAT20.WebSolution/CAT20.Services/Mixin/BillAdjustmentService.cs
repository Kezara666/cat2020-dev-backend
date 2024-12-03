using AutoMapper;
using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Services.AssessmentTax;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Services.WaterBilling;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.Mixin
{
    public class BillAdjustmentService : IBillAdjustmentService
    {

        private readonly ILogger<MixinOrderService> _logger;
        private readonly IMapper _mapper;
        private readonly IMixinUnitOfWork _unitOfWork;
        private readonly IMixinVoteBalanceService _mixVoteBalanceService;
        private readonly IAssessmentBalanceService _assessmentBalanceService;
        private readonly IAssessmentCancelOrderService _assessmentCancelOrderService;
        private readonly IVoteAssignmentService _voteAssignmentService;
        private readonly IWaterConnectionBalanceService _waterConnectionBalanceService;
        private readonly IWaterBillCancelOrderService _waterBillCancelOrderService;

        private readonly IShopRentalProcessPaymentService _shopRentalProcessPaymentService;
        private readonly IShopRentalCancelOrderService _shopRentalCancelOrderService;

        public BillAdjustmentService(ILogger<MixinOrderService> logger, IMapper mapper, IMixinUnitOfWork unitOfWork, IMixinVoteBalanceService voteBalanceService, IAssessmentBalanceService assessmentBalanceService, IAssessmentCancelOrderService assessmentCancelOrderService, IVoteAssignmentService voteAssignmentService, IWaterConnectionBalanceService waterConnectionBalanceService, IWaterBillCancelOrderService waterBillCancelOrderService, IShopRentalProcessPaymentService shopRentalProcessPaymentService, IShopRentalCancelOrderService shopRentalCancelOrderService)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mixVoteBalanceService = voteBalanceService;
            _assessmentBalanceService = assessmentBalanceService;
            _assessmentCancelOrderService = assessmentCancelOrderService;
            _voteAssignmentService = voteAssignmentService;
            _waterConnectionBalanceService = waterConnectionBalanceService;
            _waterBillCancelOrderService = waterBillCancelOrderService;

            _shopRentalProcessPaymentService = shopRentalProcessPaymentService;
            _shopRentalCancelOrderService = shopRentalCancelOrderService;
        }

        public Task<bool> AssessmentBillAdjustmentService(int adjustmentId,string actionNote, HTokenClaim token)
        {
            throw new NotImplementedException();
        }
    }
}
