using CAT20.Core;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Services.AssessmentTax;

namespace CAT20.Services.AssessmentTax
{

    public class AssessmetTempPartnerService : IAssessmetTempPartnerService
    {
        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        public AssessmetTempPartnerService(IAssessmentTaxUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AssessmentTempPartner> GetById(int id)
        {
            return await _unitOfWork.TempPartners.GetById(id);
        }

        //public async Task<IEnumerable<AssessmetTempPartner>> GetAll()
        //{
        //    return await _unitOfWork.AssessmetTempPartners.GetAll();
        //}

        //public async Task<IEnumerable<AssessmetTempPartner>> GetAllForOffice(int officeid)
        //{
        //    return await _unitOfWork.AssessmetTempPartners.GetAllForOffice(officeid);
        //}

        //public async Task<IEnumerable<AssessmetTempPartner>> GetAllForSabha(int sabhhaid)
        //{
        //    return await _unitOfWork.AssessmetTempPartners.GetAllForOffice(sabhhaid);
        //}

        //public async Task<IEnumerable<AssessmetTempPartner>> GetAllForWard(int wardid)
        //{
        //    return await _unitOfWork.AssessmetTempPartners.GetAllForWard(wardid);
        //}

        public async Task<AssessmentTempPartner> Create(AssessmentTempPartner newAssessmetTempPartner)
        {
            try
            {
                await _unitOfWork.TempPartners
                .AddAsync(newAssessmetTempPartner);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newAssessmetTempPartner.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newAssessmetTempPartner.ID,
                //    TransactionName = "AssessmetTempPartner",
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

            return newAssessmetTempPartner;
        }


        public async Task Update(AssessmentTempPartner streetToBeUpdated, AssessmentTempPartner street)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (rentalPlaceToBeUpdated.NameEnglish !=street.NameEnglish)
                //    note.Append(" English Name :" +streetToBeUpdated.NameEnglish + " Change to " +street.NameEnglish);
                //if (rentalPlaceToBeUpdated.NameSinhala !=street.NameSinhala)
                //    note.Append(" Sinhala Name :" +streetToBeUpdated.NameSinhala + " Change to " +street.NameSinhala);
                //if (rentalPlaceToBeUpdated.NameTamil !=street.NameTamil)
                //    note.Append(" Tamil Name :" +streetToBeUpdated.NameTamil + " Change to " +street.NameTamil);
                //if (rentalPlaceToBeUpdated.Code !=street.Code)
                //    note.Append(" Code :" +streetToBeUpdated.Code + " Change to " +street.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID =streetToBeUpdated.ID,
                //    TransactionName = "AssessmetTempPartner",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                //streetToBeUpdated.Status = street.Status;
                //streetToBeUpdated.WardId = street.WardId;
                //streetToBeUpdated.AssessmetTempPartnerNo = street.AssessmetTempPartnerNo;
                //streetToBeUpdated.AssessmetTempPartnerCode = street.AssessmetTempPartnerCode;
                //streetToBeUpdated.AssessmetTempPartnerName = street.AssessmetTempPartnerName;
                //streetToBeUpdated.UpdatedBy = street.UpdatedBy;
                //streetToBeUpdated.UpdatedAt = DateTime.Now;

                //await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }

        public async Task Delete(AssessmentTempPartner obj)
        {
            _unitOfWork.TempPartners.Remove(obj);
            await _unitOfWork.CommitAsync();
        }
    }
}