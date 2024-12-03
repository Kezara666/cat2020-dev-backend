using CAT20.Core;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Services.AssessmentTax;

namespace CAT20.Services.AssessmentTax
{

    public class DescriptionService : IDescriptionService
    {
        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        public DescriptionService(IAssessmentTaxUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Description> GetById(int id)
        {
            return await _unitOfWork.Descriptions.GetById(id);
        }

        public async Task<IEnumerable<Description>> GetAll()
        {
            return await _unitOfWork.Descriptions.GetAll();
        }

        public async Task<IEnumerable<Description>> GetAllForSabha(int sabhhaid)
        {
            return await _unitOfWork.Descriptions.GetAllForSabha(sabhhaid);
        }

        public async Task<Description> Create(Description newDescription)
        {
            try
            {
                await _unitOfWork.Descriptions
                .AddAsync(newDescription);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newDescription.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newDescription.ID,
                //    TransactionName = "Description",
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

            return newDescription;
        }

        public async Task<bool> BulkCreate(IEnumerable<Description> newObjsList)
        {
            try
            {
                await _unitOfWork.Descriptions
                .AddRangeAsync(newObjsList);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task Update(Description descriptionToBeUpdated, Description description)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (descriptionToBeUpdated.NameEnglish != description.NameEnglish)
                //    note.Append(" English Name :" + descriptionToBeUpdated.NameEnglish + " Change to " + description.NameEnglish);
                //if (descriptionToBeUpdated.NameSinhala != description.NameSinhala)
                //    note.Append(" Sinhala Name :" + descriptionToBeUpdated.NameSinhala + " Change to " + description.NameSinhala);
                //if (descriptionToBeUpdated.NameTamil != description.NameTamil)
                //    note.Append(" Tamil Name :" + descriptionToBeUpdated.NameTamil + " Change to " + description.NameTamil);
                //if (descriptionToBeUpdated.Code != description.Code)
                //    note.Append(" Code :" + descriptionToBeUpdated.Code + " Change to " + description.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = descriptionToBeUpdated.ID,
                //    TransactionName = "Description",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                descriptionToBeUpdated.Status = description.Status;
                descriptionToBeUpdated.DescriptionText = description.DescriptionText;
                descriptionToBeUpdated.DescriptionNo = description.DescriptionNo;
                descriptionToBeUpdated.SabhaId = description.SabhaId;
                descriptionToBeUpdated.UpdatedBy = description.UpdatedBy;

                await _unitOfWork.CommitAsync();


            }
            catch (Exception)
            {
            }
        }

        public async Task<bool> Delete(Description obj)
        {
            if (await _unitOfWork.Descriptions.IsRelationshipsExist(obj.Id!.Value))
            {
                _unitOfWork.Descriptions.Remove(obj);
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