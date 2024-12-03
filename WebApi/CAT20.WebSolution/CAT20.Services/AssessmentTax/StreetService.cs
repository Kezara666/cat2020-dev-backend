using CAT20.Core;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Services.AssessmentTax;

namespace CAT20.Services.AssessmentTax
{

    public class StreetService : IStreetService
    {
        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        public StreetService(IAssessmentTaxUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Street> GetById(int id)
        {
            return await _unitOfWork.Streets.GetById(id);
        }

        public async Task<IEnumerable<Street>> GetAll()
        {
            return await _unitOfWork.Streets.GetAll();
        }

        public async Task<IEnumerable<Street>> GetAllForOffice(int officeid)
        {
            return await _unitOfWork.Streets.GetAllForOffice(officeid);
        }

        public async Task<IEnumerable<Street>> GetAllForSabha(int sabhhaid)
        {
            return await _unitOfWork.Streets.GetAllForSabha(sabhhaid);
        }

        public async Task<IEnumerable<Street>> GetAllForWard(int wardid)
        {
            return await _unitOfWork.Streets.GetAllForWard(wardid);
        }

        public async Task<Street> Create(Street newStreet)
        {
            try
            {
                await _unitOfWork.Streets
                .AddAsync(newStreet);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newStreet.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newStreet.ID,
                //    TransactionName = "Street",
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

            return newStreet;
        }

        public async Task<bool> BulkCreate(IEnumerable<Street> newObjsList)
        {
            try
            {
                await _unitOfWork.Streets
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

        public async Task Update(Street streetToBeUpdated, Street street)
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
                //    TransactionName = "Street",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                streetToBeUpdated.Status = street.Status;
                streetToBeUpdated.WardId = street.WardId;
                streetToBeUpdated.StreetNo = street.StreetNo;
                streetToBeUpdated.StreetCode = street.StreetCode;
                streetToBeUpdated.StreetName = street.StreetName;
                streetToBeUpdated.UpdatedBy = street.UpdatedBy;
                streetToBeUpdated.UpdatedAt = DateTime.Now;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }

        public async Task<bool> Delete(Street obj)
        {
            if (await _unitOfWork.Streets.IsRelationshipsExist(obj.Id!.Value))
            {
                _unitOfWork.Streets.Remove(obj);
                await _unitOfWork.CommitAsync();

                return true;

            }

            return false;
        }
    }
}