using CAT20.Core;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Services.AssessmentTax;

namespace CAT20.Services.AssessmentTax
{
    public class AssessmentPropertyTypeLogService : IAssessmentPropertyTypeLogService
    {

        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        public AssessmentPropertyTypeLogService(IAssessmentTaxUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public async Task<bool> Create(AssessmentPropertyTypeLog obj)
        {
            try
            {
                var assmt = await _unitOfWork.Assessments.GetByIdAsync(obj.AssessmentId!.Value);

                if (assmt.PropertyTypeChangeRequest)
                {

                    return false;
                }

                assmt.UpdatedAt = DateTime.Now;
                assmt.UpdatedBy = obj.CreatedBy;
                assmt.PropertyTypeChangeRequest = true;

                await _unitOfWork.AssessmentPropertyTypeLogs.AddAsync(obj);
                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        public async Task<bool> ApproveDisapprovePropertyType(AssessmentPropertyTypeLog obj, bool isApproved)
        {
            try
            {
                var assmt = await _unitOfWork.Assessments.GetByIdAsync(obj.AssessmentId!.Value);

                var assmtPropTypeLog = await _unitOfWork.AssessmentPropertyTypeLogs.GetByIdAsync(obj.Id!.Value);

                assmtPropTypeLog.ActionBy = obj.ActionBy;
                assmtPropTypeLog.ActionNote = obj.ActionNote;
                assmtPropTypeLog.UpdatedAt = DateTime.Now;

                if (isApproved)
                {
                    assmtPropTypeLog.ActivatedDate = DateTime.Now;
                    assmt.PropertyTypeId = obj.PropertyTypeId;
                }

                assmt.UpdatedAt = DateTime.Now;
                assmt.UpdatedBy = obj.UpdatedBy;
                assmt.PropertyTypeChangeRequest = false;
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
