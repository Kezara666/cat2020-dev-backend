using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Enums;
using CAT20.Core.Services.AssessmentTax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.AssessmentTax
{
    public class AssessmentATDService : IAssessmentATDService
    {


        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        public AssessmentATDService(IAssessmentTaxUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Save(AssessmentATD obj, HTokenClaim token)
        {
            try
            {
                if (obj != null && obj.Id > 0)
                {
                    var ATD = await _unitOfWork.AssessmentATDs.GetByIdAsync(obj.Id);

                    if (ATD != null)
                    {
                        if(ATD.ATDRequestStatus==ATDRequestStatus.PendingApproval && obj.ATDRequestStatus == ATDRequestStatus.Approved)
                        {
                            ATD.ATDRequestStatus = ATDRequestStatus.Approved;
                            ATD.ActionDate = DateTime.Now;
                            ATD.ActionBy = obj.ActionBy;
                            ATD.ActionNote = obj.ActionNote;
                        }
                        else if (ATD.ATDRequestStatus == ATDRequestStatus.PendingApproval && obj.ATDRequestStatus == ATDRequestStatus.Rejected)
                        {
                            ATD.ATDRequestStatus = ATDRequestStatus.Rejected;
                            ATD.ActionDate = DateTime.Now;
                            ATD.ActionBy = obj.ActionBy;
                            ATD.ActionNote = obj.ActionNote;
                        }
                    }
                    else
                    {
                        throw new Exception("ATD not found");
                    }
                }
                else
                {
                    var assmt = await _unitOfWork.Assessments.GetByIdAsync(obj.AssessmentId!.Value);

                    if (assmt != null)
                    {
                        obj.ATDRequestStatus = ATDRequestStatus.PendingApproval;
                        obj.SabhaId = token.sabhaId;
                        obj.OfficeId = token.officeId;
                        obj.CreatedBy = token.userId;
                        obj.CreatedAt = DateTime.Now;
                        assmt.HasAssetsChangeRequest = true;
                        await _unitOfWork.AssessmentATDs.AddAsync(obj);
                    }
                    else
                    {
                        throw new Exception("Assessment not found");
                    }
                }
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<(int totalCount, IEnumerable<AssessmentATD> list)> GetAllATDRequestsForAssessment(int assessmentId, int pageNo)
        {
            try
            {
                return await _unitOfWork.AssessmentATDs.GetAllATDRequestsForAssessment(assessmentId, pageNo);
            }
            catch (Exception ex)
            {
                List<AssessmentATD> list1 = new List<AssessmentATD>();
                return (0, list1);
            }
        }

        public Task<(int totalCount, IEnumerable<AssessmentATD> list)> GetAllATDRequestsForOffice(int? officeId, ATDRequestStatus atdStatus, int pageNo)
        {
            return _unitOfWork.AssessmentATDs.GetAllATDRequestsForOffice(officeId, atdStatus, pageNo);
        }

        public Task<(int totalCount, IEnumerable<AssessmentATD> list)> GetAllATDRequestsForSabha(int? officeId, ATDRequestStatus atdStatus, int pageNo)
        {
            return _unitOfWork.AssessmentATDs.GetAllATDRequestsForSabha(officeId, atdStatus, pageNo);
        }

        public Task<AssessmentATD> GetATDById(int Id)
        {
            return _unitOfWork.AssessmentATDs.GetATDById(Id);
        }

        public async Task Delete(AssessmentATD obj)
        {
            //_unitOfWork.AssessmentATDs.Remove(obj);
            //await _unitOfWork.CommitAsync();
        }


        public async Task<bool> ApproveRejectATD(HApproveRejectATD obj)
        {
            try
            {
                var ATD = await _unitOfWork.AssessmentATDs.GetATDById(obj.Id.Value);

                if (ATD != null)
                {
                    ATD.ActionBy = obj.ActionBy;
                    ATD.ActionDate = DateTime.Now;
                    ATD.ATDRequestStatus = obj.ATDRequestStatus;

                    var asmt = await _unitOfWork.Assessments.GetForAssetschnage(obj.AssessmentId.Value);

                    if (asmt != null && asmt.AssessmentTempPartner != null)
                    {
                        if (obj.ATDRequestStatus == ATDRequestStatus.Approved)
                        {
                            if (asmt.IsPartnerUpdated)
                            {
                                var partner = await _unitOfWork.Partners.GetByIdAsync(ATD.AssessmentATDOwnerslogs.First(a=>a.OwnerStatus==AssessmentOwnerStatus.New && a.OwnerType==AssessmentOwnerType.Owner).PartnerId);

                                if (partner != null)
                                {
                                   partner.IsEditable = 0;
                                   asmt.PartnerId = partner.Id;
                                   asmt.IsPartnerUpdated = true;
                                   asmt.UpdatedBy = obj.ActionBy;

                                    asmt.AssessmentTempPartner.Name = partner.Name;
                                    asmt.AssessmentTempPartner.Street1 = partner.Street1;
                                    asmt.AssessmentTempPartner.Street2 = partner.Street2;
                                    asmt.AssessmentTempPartner.MobileNumber = partner.MobileNumber;
                                    asmt.AssessmentTempPartner.NICNumber = partner.NicNumber;
                                    asmt.AssessmentTempPartner.UpdatedBy = obj.ActionBy;
                                }
                                else
                                {
                                   throw new Exception("Unable To Find Customer");
                                }
                            }
                            else
                            {
                                throw new Exception("Customer is not Updated Yet.");
                            }
                        }
                        else if (obj.ATDRequestStatus == ATDRequestStatus.PendingApproval)
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
