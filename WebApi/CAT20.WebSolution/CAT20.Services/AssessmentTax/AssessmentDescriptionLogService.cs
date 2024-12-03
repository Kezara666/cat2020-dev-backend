using CAT20.Core;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Services.AssessmentTax;

namespace CAT20.Services.AssessmentTax
{
    public class AssessmentDescriptionLogService : IAssessmentDescriptionLogService
    {

        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        public AssessmentDescriptionLogService(IAssessmentTaxUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> Create(AssessmentDescriptionLog obj)
        {
            try
            {
                var assmt = await _unitOfWork.Assessments.GetByIdAsync(obj.AssessmentId!.Value);

                if (assmt.DescriptionChangeRequest)
                {
                    return false;
                }

                assmt.UpdatedAt = DateTime.Now;
                assmt.UpdatedBy = obj.CreatedBy;
                assmt.DescriptionChangeRequest = true;

                await _unitOfWork.AssessmentDescriptionLogs.AddAsync(obj);
                await _unitOfWork.CommitAsync();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> ApproveDisapproveDescription(AssessmentDescriptionLog obj, bool isApproved)
        {
            try
            {
                var assmt = await _unitOfWork.Assessments.GetByIdAsync(obj.AssessmentId!.Value);

                var assmtDesLog = await _unitOfWork.AssessmentDescriptionLogs.GetByIdAsync(obj.Id!.Value);

                assmtDesLog.ActionBy = obj.ActionBy;
                assmtDesLog.ActionNote = obj.ActionNote;
                assmtDesLog.UpdatedAt = DateTime.Now; ;

                if (isApproved)
                {
                    assmtDesLog.ActivatedDate = DateTime.Now;
                    assmt.DescriptionId = obj.DescriptionId;
                }

                assmt.UpdatedAt = DateTime.Now;
                assmt.UpdatedBy = obj.UpdatedBy;
                assmt.DescriptionChangeRequest = false;

                await _unitOfWork.CommitAsync();

                return true;


            }
            catch (Exception ex)
            {
                return false;


            }
        }
    }
}
