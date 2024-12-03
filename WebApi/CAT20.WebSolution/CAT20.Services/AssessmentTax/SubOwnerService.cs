using CAT20.Core;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Services.AssessmentTax;

namespace CAT20.Services.AssessmentTax
{

    public class SubOwnerService : IAssessmentTempSubPartnerService
    {
        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        public SubOwnerService(IAssessmentTaxUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AssessmentTempSubPartner> GetById(int id)
        {
            return await _unitOfWork.TempSubPartners.GetById(id);
        }

        public async Task<IEnumerable<AssessmentTempSubPartner>> GetAll()
        {
            return await _unitOfWork.TempSubPartners.GetAll();
        }

        public async Task<IEnumerable<AssessmentTempSubPartner>> GetAllForOffice(int officeid)
        {
            return await _unitOfWork.TempSubPartners.GetAllForOffice(officeid);
        }

        public async Task<IEnumerable<AssessmentTempSubPartner>> GetAllForSabha(int sabhhaid)
        {
            return await _unitOfWork.TempSubPartners.GetAllForSabha(sabhhaid);
        }

        public async Task<IEnumerable<AssessmentTempSubPartner>> GetAllForAssessmentId(int assessmentid)
        {
            return await _unitOfWork.TempSubPartners.GetAllForAssessmentId(assessmentid);
        }

        public async Task<AssessmentTempSubPartner> Create(AssessmentTempSubPartner newSubOwner)
        {
            try
            {
                await _unitOfWork.TempSubPartners.AddAsync(newSubOwner);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newSubOwner.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newSubOwner.ID,
                //    TransactionName = "SubOwner",
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

            return newSubOwner;
        }

        public async Task<IEnumerable<AssessmentTempSubPartner>> BulkCreate(IEnumerable<AssessmentTempSubPartner> newObjsList)
        {
            try
            {
                await _unitOfWork.TempSubPartners
                .AddRangeAsync(newObjsList);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return newObjsList;
        }

        public async Task Update(AssessmentTempSubPartner SubOwnerToBeUpdated, AssessmentTempSubPartner SubOwner)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (SubOwnerToBeUpdated.NameEnglish != SubOwner.NameEnglish)
                //    note.Append(" English Name :" + SubOwnerToBeUpdated.NameEnglish + " Change to " + SubOwner.NameEnglish);
                //if (SubOwnerToBeUpdated.NameSinhala != SubOwner.NameSinhala)
                //    note.Append(" Sinhala Name :" + SubOwnerToBeUpdated.NameSinhala + " Change to " + SubOwner.NameSinhala);
                //if (SubOwnerToBeUpdated.NameTamil != SubOwner.NameTamil)
                //    note.Append(" Tamil Name :" + SubOwnerToBeUpdated.NameTamil + " Change to " + SubOwner.NameTamil);
                //if (SubOwnerToBeUpdated.Code != SubOwner.Code)
                //    note.Append(" Code :" + SubOwnerToBeUpdated.Code + " Change to " + SubOwner.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = SubOwnerToBeUpdated.ID,
                //    TransactionName = "SubOwner",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                //SubOwnerToBeUpdated.Status = SubOwner.Status;
                SubOwnerToBeUpdated.NICNumber = SubOwner.NICNumber;
                SubOwnerToBeUpdated.AssessmentId = SubOwner.AssessmentId;
                //SubOwnerToBeUpdated.Title = SubOwner.Title;
                SubOwnerToBeUpdated.Name = SubOwner.Name;
                SubOwnerToBeUpdated.UpdatedBy = SubOwner.UpdatedBy;
                SubOwnerToBeUpdated.UpdatedAt = DateTime.Now;

                await _unitOfWork.CommitAsync();


            }
            catch (Exception)
            {
            }
        }

        public async Task Delete(AssessmentTempSubPartner obj)
        {
            _unitOfWork.TempSubPartners.Remove(obj);
            await _unitOfWork.CommitAsync();
        }
    }
}