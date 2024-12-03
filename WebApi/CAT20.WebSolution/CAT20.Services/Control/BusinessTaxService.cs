using CAT20.Core;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Services.Control;

namespace CAT20.Services.Control
{
    public class BusinessTaxService : IBusinessTaxService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public BusinessTaxService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BusinessTaxes> Create(BusinessTaxes newBusinessTax)
        {
            try
            {
                await _unitOfWork.BusinessTaxes
                .AddAsync(newBusinessTax);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newBusinessTax.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newBusinessTax.ID,
                //    TransactionName = "BusinessTax",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {

            }

            return newBusinessTax;
        }
        public async Task Delete(BusinessTaxes businessTaxTaxes)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Deleted on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = businessTaxTaxes.ID,
                //    TransactionName = "BusinessTax",
                //    User = 1,
                //    Note = note.ToString()
                //});
                businessTaxTaxes.Status = 0;

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
            //_unitOfWork.BusinessTaxes.Remove(businessTaxTaxes);
        }
        //public async Task<IEnumerable<BusinessTaxes>> GetAll()
        //{
        //    return await _unitOfWork.BusinessTaxes.GetAllAsync();
        //}
        public async Task<BusinessTaxes> GetById(int id)
        {
            return await _unitOfWork.BusinessTaxes.GetByIdAsync(id);
        }

        public async Task<IEnumerable<BusinessTaxes>> GetBusinessTaxForBusinessIdAndYear(int id, int year)
        {
            return await _unitOfWork.BusinessTaxes.GetBusinessTaxForBusinessIdAndYearAsync(id, year);
        }

        public async Task<IEnumerable<BusinessTaxes>> GetBusinessTaxForBusinessId(int id)
        {
            return await _unitOfWork.BusinessTaxes.GetBusinessTaxForBusinessIdAsync(id);
        }

        //public async Task<BusinessTaxes> GetByRegNo(string RegNo)
        //{
        //    return await _unitOfWork.BusinessTaxes.GetByRegNoAsync(RegNo);
        //}

        //public async Task<BusinessTaxes> GetByApplicationNo(string RegNo)
        //{
        //    return await _unitOfWork.BusinessTaxes.GetByApplicationNoAsync(RegNo);
        //}

        //public async Task<BusinessTaxes> GetByRegNoAndOffice(string RegNo, int officeId)
        //{
        //    return await _unitOfWork.BusinessTaxes.GetByRegNoAndOfficeAsync(RegNo, officeId);
        //}

        //public async Task<BusinessTaxes> GetByApplicationNoAndOffice(string ApplicationNo, int officeId)
        //{
        //    return await _unitOfWork.BusinessTaxes.GetByApplicationNoAndOfficeAsync(ApplicationNo, officeId);
        //}

        public async Task Update(BusinessTaxes businessTaxTaxesToBeUpdated, BusinessTaxes businessTaxTaxes)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (businessTaxTaxesToBeUpdated.NameEnglish != businessTaxTaxes.NameEnglish)
                //    note.Append(" English Name :" + businessTaxTaxesToBeUpdated.NameEnglish + " Change to " + businessTaxTaxes.NameEnglish);
                //if (businessTaxTaxesToBeUpdated.NameSinhala != businessTaxTaxes.NameSinhala)
                //    note.Append(" Sinhala Name :" + businessTaxTaxesToBeUpdated.NameSinhala + " Change to " + businessTaxTaxes.NameSinhala);
                //if (businessTaxTaxesToBeUpdated.NameTamil != businessTaxTaxes.NameTamil)
                //    note.Append(" Tamil Name :" + businessTaxTaxesToBeUpdated.NameTamil + " Change to " + businessTaxTaxes.NameTamil);
                //if (businessTaxTaxesToBeUpdated.Code != businessTaxTaxes.Code)
                //    note.Append(" Code :" + businessTaxTaxesToBeUpdated.Code + " Change to " + businessTaxTaxes.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = businessTaxTaxesToBeUpdated.ID,
                //    TransactionName = "BusinessTax",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                businessTaxTaxesToBeUpdated.ApplicationNo = businessTaxTaxes.ApplicationNo;
                businessTaxTaxesToBeUpdated.LastYearValue = businessTaxTaxes.LastYearValue;
                businessTaxTaxesToBeUpdated.CurrentYear = businessTaxTaxes.CurrentYear;
                businessTaxTaxesToBeUpdated.OtherCharges = businessTaxTaxes.OtherCharges;
                businessTaxTaxesToBeUpdated.AnnualValue = businessTaxTaxes.AnnualValue;
                businessTaxTaxesToBeUpdated.TaxAmountByNature = businessTaxTaxes.TaxAmountByNature;
                businessTaxTaxesToBeUpdated.TaxAmount = businessTaxTaxes.TaxAmount;
                businessTaxTaxesToBeUpdated.TotalTaxAmount = businessTaxTaxes.TotalTaxAmount;
                businessTaxTaxesToBeUpdated.UpdatedAt = businessTaxTaxes.UpdatedAt;
                businessTaxTaxesToBeUpdated.UpdatedBy = businessTaxTaxes.UpdatedBy;
                businessTaxTaxesToBeUpdated.is_moh_approved = businessTaxTaxes.is_moh_approved;
                businessTaxTaxesToBeUpdated.is_secretary_approved = businessTaxTaxes.is_secretary_approved;
                businessTaxTaxesToBeUpdated.is_chairman_approved = businessTaxTaxes.is_chairman_approved;
                businessTaxTaxesToBeUpdated.MOHApprovedBy = businessTaxTaxes.MOHApprovedBy;
                businessTaxTaxesToBeUpdated.SecretaryApprovedBy = businessTaxTaxes.SecretaryApprovedBy;
                businessTaxTaxesToBeUpdated.ChairmanApprovedBy = businessTaxTaxes.ChairmanApprovedBy;
                businessTaxTaxesToBeUpdated.MOHApprovedAt = businessTaxTaxes.MOHApprovedAt;
                businessTaxTaxesToBeUpdated.SecretaryApprovedAt = businessTaxTaxes.SecretaryApprovedAt;
                businessTaxTaxesToBeUpdated.ChairmanApprovedAt = businessTaxTaxes.ChairmanApprovedAt;
                businessTaxTaxesToBeUpdated.TaxState = businessTaxTaxes.TaxState;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForMOHPendingApproval(int sabhaId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForMOHPendingApproval(sabhaId);
        }
        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForSecretaryPendingApproval(int sabhaId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForSecretaryPendingApproval(sabhaId);
        }
        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForChairmanPendingApproval(int sabhaId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForChairmanPendingApproval(sabhaId);
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForMOHApproved(int sabhaId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForMOHApproved(sabhaId);
        }
        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForSecretaryApproved(int sabhaId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForSecretaryApproved(sabhaId);
        }
        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForChairmanApproved(int sabhaId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForChairmanApproved(sabhaId);
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForMOHDisapproved(int sabhaId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForMOHDisapproved(sabhaId);
        }
        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForSecretaryDisapproved(int sabhaId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForSecretaryDisapproved(sabhaId);
        }
        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForChairmanDisapproved(int sabhaId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForChairmanDisapproved(sabhaId);
        }


        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForSabhaAndTaxStatus(int sabhaId, TaxStatus taxStatus)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForSabhaAndTaxStatus(sabhaId, taxStatus);
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForAllApproved(int sabhaId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForAllApproved(sabhaId);
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForDisapprovedAny(int sabhaId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForDisapprovedAny(sabhaId);
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForApprovalPendingAny(int sabhaId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForApprovalPendingAny(sabhaId);
        }






        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdMOHPendingApproval(int sabhaId, int officerId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForOfficerIdMOHPendingApproval(sabhaId, officerId);
        }
        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdSecretaryPendingApproval(int sabhaId, int officerId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForOfficerIdSecretaryPendingApproval(sabhaId, officerId);
        }
        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdChairmanPendingApproval(int sabhaId, int officerId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForOfficerIdChairmanPendingApproval(sabhaId, officerId);
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdMOHApproved(int sabhaId, int officerId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForOfficerIdMOHApproved(sabhaId, officerId);
        }
        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdSecretaryApproved(int sabhaId, int officerId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForOfficerIdSecretaryApproved(sabhaId, officerId);
        }
        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdChairmanApproved(int sabhaId, int officerId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForOfficerIdChairmanApproved(sabhaId, officerId);
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdMOHDisapproved(int sabhaId, int officerId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForOfficerIdMOHDisapproved(sabhaId, officerId);
        }
        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdSecretaryDisapproved(int sabhaId, int officerId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForOfficerIdSecretaryDisapproved(sabhaId, officerId);
        }
        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdChairmanDisapproved(int sabhaId, int officerId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForOfficerIdChairmanDisapproved(sabhaId, officerId);
        }


        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdSabhaAndTaxStatus(int sabhaId, TaxStatus taxStatus, int officerId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForOfficerIdSabhaAndTaxStatus(sabhaId, taxStatus, officerId);
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdAllApproved(int sabhaId, int officerId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForOfficerIdAllApproved(sabhaId, officerId);
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdDisapprovedAny(int sabhaId, int officerId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForOfficerIdDisapprovedAny(sabhaId, officerId);
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessLicensesForOfficerIdApprovalPendingAny(int sabhaId, int officerId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessLicensesForOfficerIdApprovalPendingAny(sabhaId, officerId);
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessAndIndustrialTaxesForSabhaAndOfficerAndStatus(int sabhaId, TaxStatus taxStatus, int officerId)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessAndIndustrialTaxesForSabhaAndOfficerAndStatus(sabhaId, taxStatus, officerId);
        }

        public async Task<IEnumerable<BusinessTaxes>> GetAllBusinessAndIndustrialTaxesForSabhaAndStatus(int sabhaId, TaxStatus taxStatus)
        {
            return await _unitOfWork.BusinessTaxes.GetAllBusinessAndIndustrialTaxesForSabhaAndStatus(sabhaId, taxStatus);
        }
        //public async Task<IEnumerable<BusinessTax>> GetAllForSabha(int sabhaid)
        //{
        //    return await _unitOfWork.BusinessTaxes.GetAllForSabhaAsync(sabhaid);
        //}

        //public async Task<IEnumerable<BusinessTax>> GetAllForBusinessTaxOwnerId(int BusinessTaxOwnerid)
        //{
        //    return await _unitOfWork.Busi.GetAllForBusinessTaxOwnerIdAsync(BusinessTaxOwnerid);
        //}
        //public async Task<IEnumerable<BusinessTax>> GetAllForBusinessTaxNatureAndSabha(int id, int sabhaid)
        //{
        //    return await _unitOfWork.BusinessTaxes.GetAllForBusinessTaxNatureAndSabhaAsync(id,  sabhaid);
        //}

        //public async Task<IEnumerable<BusinessTax>> GetAllForBusinessTaxSubNatureAndSabha(int id, int sabhaid)
        //{
        //    return await _unitOfWork.BusinessTaxes.GetAllForBusinessTaxSubNatureAndSabhaAsync(id, sabhaid);
        //}
    }
}