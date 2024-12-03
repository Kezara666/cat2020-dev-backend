using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Services.AssessmentTax;

namespace CAT20.Services.AssessmentTax
{

    public class AssessmentPropertyTypeService : IAssessmentPropertyTypeService
    {
        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        private readonly IAssessmentRenewalService _assessmentRenewalService;

        public AssessmentPropertyTypeService(IAssessmentTaxUnitOfWork unitOfWork,IAssessmentRenewalService assessmentRenewalService)
        {
            _unitOfWork = unitOfWork;
            _assessmentRenewalService = assessmentRenewalService;
        }

        public async Task<AssessmentPropertyType> GetById(int id)
        {
            return await _unitOfWork.AssessmentPropertyTypes.GetById(id);
        }

        public async Task<IEnumerable<AssessmentPropertyType>> GetAll()
        {
            return await _unitOfWork.AssessmentPropertyTypes.GetAll();
        }

        public async Task<IEnumerable<AssessmentPropertyType>> GetAllForSabha(int sabhhaid)
        {
            return await _unitOfWork.AssessmentPropertyTypes.GetAllForSabha(sabhhaid);
        }

        public async Task<AssessmentPropertyType> Create(AssessmentPropertyType newAssessmentPropertyType)
        {
            try
            {
                await _unitOfWork.AssessmentPropertyTypes
                .AddAsync(newAssessmentPropertyType);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newAssessmentPropertyType.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newAssessmentPropertyType.ID,
                //    TransactionName = "AssessmentPropertyType",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return newAssessmentPropertyType;
        }

        public async Task<bool> BulkCreate(IEnumerable<AssessmentPropertyType> newObjsList)
        {
            try
            {
                foreach (var propertytype in newObjsList)
                {
                    propertytype.NextYearQuarterRate = propertytype.QuarterRate;
                    propertytype.NextYearWarrantRate = propertytype.WarrantRate;
                }

                await _unitOfWork.AssessmentPropertyTypes
                .AddRangeAsync(newObjsList);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public async Task<bool> Update(AssessmentPropertyType assessmentPropertyType,HTokenClaim token)
        {
            try
            {


                //assessmentPropertyTypeToBeUpdated.PropertyTypeName = assessmentPropertyType.PropertyTypeName;
                //assessmentPropertyTypeToBeUpdated.PropertyTypeNo = assessmentPropertyType.PropertyTypeNo;
                ////assessmentPropertyTypeToBeUpdated.QuarterRate = assessmentPropertyType.QuarterRate;
                ////assessmentPropertyTypeToBeUpdated.WarrantRate = assessmentPropertyType.WarrantRate;
                //assessmentPropertyTypeToBeUpdated.Status = assessmentPropertyType.Status;
                ////assessmentPropertyTypeToBeUpdated.SabhaId = assessmentPropertyType.SabhaId;
                //assessmentPropertyTypeToBeUpdated.UpdatedBy = assessmentPropertyType.UpdatedBy;

                var propertyType = await _unitOfWork.AssessmentPropertyTypes.GetByIdAsync(assessmentPropertyType.Id);

                if (propertyType != null)
                {

                    propertyType.PropertyTypeName = assessmentPropertyType.PropertyTypeName;
                    propertyType.PropertyTypeNo = assessmentPropertyType.PropertyTypeNo;
                    propertyType.NextYearQuarterRate = assessmentPropertyType.NextYearQuarterRate;
                    propertyType.NextYearWarrantRate = assessmentPropertyType?.NextYearWarrantRate;
                    propertyType.UpdatedBy = token.userId;
                    propertyType.UpdatedAt = DateTime.Now;

                    await _unitOfWork.CommitAsync();

                    await _assessmentRenewalService.UpdateAssessmentNextYearQuarters(token,propertyType.Id);

                    return true;
                }
                else
                {
                    throw new Exception("Unable To Found Property Type");
                }

               


            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Delete(AssessmentPropertyType obj)
        {
            if (await _unitOfWork.AssessmentPropertyTypes.IsRelationshipsExist(obj.Id!.Value))
            {
                _unitOfWork.AssessmentPropertyTypes.Remove(obj);
                await _unitOfWork.CommitAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}