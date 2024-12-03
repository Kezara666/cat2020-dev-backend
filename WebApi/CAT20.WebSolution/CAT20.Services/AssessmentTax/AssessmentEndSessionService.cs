using CAT20.Core;
using CAT20.Core.Services.AssessmentTax;
using Microsoft.Extensions.Logging;

namespace CAT20.Services.AssessmentTax
{
    public class AssessmentEndSessionService : IAssessmentEndSessionService
    {
        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        private readonly ILogger<AssessmentService> _logger;
        public AssessmentEndSessionService(ILogger<AssessmentService> logger, IAssessmentTaxUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> EndSessionDisableTransaction(int officeId)
        {
            try
            {
                var assmtbals = await _unitOfWork.Assessments.GetForEndSessionToDisableTransaction(officeId);


                foreach (var b in assmtbals)
                {
                    b.HasTransaction = false;
                }

                //await _unitOfWork.CommitAsync();
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "First Init.{EventType}", "Assessment End Session");
                return false;
            }
        }
    }
}
