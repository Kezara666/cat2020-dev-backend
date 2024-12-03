using CAT20.Core;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Services.AssessmentTax;

namespace CAT20.Services.AssessmentTax
{

    public class WardService : IWardService
    {
        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        public WardService(IAssessmentTaxUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Ward> GetById(int id)
        {
            return await _unitOfWork.Wards.GetById(id);
        }

        public async Task<IEnumerable<Ward>> GetAll()
        {
            return await _unitOfWork.Wards.GetAll();
        }

        public async Task<IEnumerable<Ward>> GetAllForOffice(int officeid)
        {
            return await _unitOfWork.Wards.GetAllForOffice(officeid);
        }

        public async Task<IEnumerable<Ward>> GetAllForSabha(int sabhhaid)
        {
            return await _unitOfWork.Wards.GetAllForSabha(sabhhaid);
        }

        public async Task<Ward> Create(Ward newWard)
        {
            try
            {
                await _unitOfWork.Wards
                .AddAsync(newWard);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newWard.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newWard.ID,
                //    TransactionName = "Ward",
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

            return newWard;
        }

        public async Task<bool> BulkCreate(IEnumerable<Ward> newObjsList)
        {
            try
            {
                await _unitOfWork.Wards
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

        public async Task Update(Ward wardToBeUpdated, Ward ward)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (wardToBeUpdated.NameEnglish != ward.NameEnglish)
                //    note.Append(" English Name :" + wardToBeUpdated.NameEnglish + " Change to " + ward.NameEnglish);
                //if (wardToBeUpdated.NameSinhala != ward.NameSinhala)
                //    note.Append(" Sinhala Name :" + wardToBeUpdated.NameSinhala + " Change to " + ward.NameSinhala);
                //if (wardToBeUpdated.NameTamil != ward.NameTamil)
                //    note.Append(" Tamil Name :" + wardToBeUpdated.NameTamil + " Change to " + ward.NameTamil);
                //if (wardToBeUpdated.Code != ward.Code)
                //    note.Append(" Code :" + wardToBeUpdated.Code + " Change to " + ward.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = wardToBeUpdated.ID,
                //    TransactionName = "Ward",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                wardToBeUpdated.Status = ward.Status;
                wardToBeUpdated.WardNo = ward.WardNo;
                wardToBeUpdated.WardCode = ward.WardCode;
                wardToBeUpdated.WardName = ward.WardName;
                wardToBeUpdated.UpdatedBy = ward.UpdatedBy;
                wardToBeUpdated.UpdatedAt = DateTime.Now;

                await _unitOfWork.CommitAsync();


            }
            catch (Exception)
            {
            }
        }

        public async Task<bool> Delete(Ward obj)
        {


            if (await _unitOfWork.Wards.IsRelationshipsExist(obj.Id!.Value))
            {
                _unitOfWork.Wards.Remove(obj);
                await _unitOfWork.CommitAsync();
                return true;
            }


            return false;

        }
    }
}