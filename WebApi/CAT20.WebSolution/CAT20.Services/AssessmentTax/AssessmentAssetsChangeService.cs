using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Services.AssessmentTax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.AssessmentTax
{
    public class AssessmentAssetsChangeService : IAssessmentAssetsChangeService
    {


        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        public AssessmentAssetsChangeService(IAssessmentTaxUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public async Task<bool> Create(AssessmentAssetsChange obj)
        {
            try
            {
                var assmt = await _unitOfWork.Assessments.GetByIdAsync(obj.AssessmentId!.Value);

                if (assmt != null)
                {
                    assmt.HasAssetsChangeRequest = true;
                    await _unitOfWork.AssessmentAssetsChanges.AddAsync(obj);

                }
                else
                {
                    throw new Exception("Assessment not found");
                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<(int totalCount, IEnumerable<AssessmentAssetsChange> list)> GetAllAssetsChangeForAssessment(int assessmentId, int pageNo)
        {
            try
            {
                return await _unitOfWork.AssessmentAssetsChanges.GetAllAssetsChangeForAssessment(assessmentId, pageNo);
            }
            catch (Exception ex)
            {
                List<AssessmentAssetsChange> list1 = new List<AssessmentAssetsChange>();
                return (0, list1);
            }
        }

        public Task<(int totalCount, IEnumerable<AssessmentAssetsChange> list)> GetAllAssetsRequestForOffice(int? officeId, int pageNo)
        {
            return _unitOfWork.AssessmentAssetsChanges.GetAllAssetsRequestForOffice(officeId, pageNo);
        }

        public Task<Assessment> GetAssessmentForAssets(int? sabhaId, int? kFormId)
        {
            return _unitOfWork.Assessments.GetAssessmentForAssets(sabhaId, kFormId);
        }

        public async Task<(int totalCount, IEnumerable<Assessment> list)> GetForPendingAssetsRequest(int? sabhaId, int pageNo)
        {
            return await _unitOfWork.AssessmentAssetsChanges.GetForPendingAssetsRequest(sabhaId, pageNo);
        }

    
        public async Task<bool> ApproveRejectAssetsChanage(HApproveRejectAssetsChange obj)
        {
            try
            {

                var astCng = await _unitOfWork.AssessmentAssetsChanges.GetByIdAsync(obj.Id);



                if (astCng != null)
                {
                    astCng.ActionBy = obj.ActionBy;
                    astCng.ActionDate = DateTime.Now;
                    astCng.DraftApproveReject = obj.DraftApproveReject;

                    var asmt = await _unitOfWork.Assessments.GetForAssetschnage(obj.AssessmentId.Value);

                    if (asmt !=null && asmt.AssessmentTempPartner !=null) {

                        if (obj.DraftApproveReject == 1)
                        {


                            if (!string.IsNullOrWhiteSpace(astCng.OldNumber) && !string.IsNullOrWhiteSpace(astCng.NewNumber) && !asmt.AssessmentNo!.Equals(astCng.NewNumber))
                            {
                                asmt.AssessmentNo = astCng.NewNumber;
                            }

                            if (asmt.IsPartnerUpdated)
                            {

                                var partner = await _unitOfWork.Partners.GetByIdAsync(asmt.PartnerId);

                                if(partner != null)
                                {
                                                    

                                    if (!string.IsNullOrWhiteSpace(astCng.OldName) && !string.IsNullOrWhiteSpace(astCng.NewName) && !partner.Name.Equals(astCng.NewName))
                                    {
                                        partner.Name = astCng.NewName;
                                    }

                                    if (!string.IsNullOrWhiteSpace(astCng.OldAddressLine1) && !string.IsNullOrWhiteSpace(astCng.NewAddressLine1) && !partner.Street1!.Equals(astCng.NewAddressLine1))
                                    {
                                        partner.Street1 = astCng.NewAddressLine1;
                                    }

                                    if (!string.IsNullOrWhiteSpace(astCng.OldAddressLine2) && !string.IsNullOrWhiteSpace(astCng.NewAddressLine2) && !partner.Street2!.Equals(astCng.NewAddressLine2))
                                    {
                                        partner.Street2 = astCng.NewAddressLine2;
                                    }

                                }
                                else
                                {
                                    throw new Exception("Unable To Find Patner");
                                }

                            }
                            else
                            {
                                if ( !string.IsNullOrWhiteSpace(astCng.OldName) && !string.IsNullOrWhiteSpace(astCng.NewName)   &&  !asmt.AssessmentTempPartner.Name!.Equals(astCng.NewName))
                                {
                                    asmt.AssessmentTempPartner!.Name = astCng.NewName;
                                }

                                if (!string.IsNullOrWhiteSpace(astCng.OldAddressLine1) && !string.IsNullOrWhiteSpace(astCng.NewAddressLine1) && !asmt.AssessmentTempPartner.Street1!.Equals(astCng.NewAddressLine1))
                                {
                                    asmt.AssessmentTempPartner!.Street1 = astCng.NewAddressLine1;
                                }

                                if (!string.IsNullOrWhiteSpace(astCng.OldAddressLine2) && !string.IsNullOrWhiteSpace(astCng.NewAddressLine2) && !asmt.AssessmentTempPartner.Street2!.Equals(astCng.NewAddressLine2))
                                {
                                    asmt.AssessmentTempPartner!.Street2 = astCng.NewAddressLine2;
                                }

                                
                               

                            }

                            asmt.HasAssetsChangeRequest = false;
                        }
                        else if (obj.DraftApproveReject == 0)
                        {
                            asmt.HasAssetsChangeRequest = false;
                        }
                        else
                        {
                            asmt.HasAssetsChangeRequest = false;
                        }



                    }
                    else
                    {
                        throw new Exception("Assessment Not found Or Record Updated  By Other Operations");
                    }


                }
                else
                {
                    throw new Exception("Not found");
                }
                

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
