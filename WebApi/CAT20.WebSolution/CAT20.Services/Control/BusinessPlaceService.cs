using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;
using CAT20.Core.Models.Common;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace CAT20.Services.Control
{
    public class BusinessPlaceService : IBusinessPlaceService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public BusinessPlaceService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BusinessPlace> Create(BusinessPlace newBusinessPlace)
        {
            try
            {
                await _unitOfWork.BusinessPlaces
                .AddAsync(newBusinessPlace);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newBusinessPlace.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newBusinessPlace.ID,
                //    TransactionName = "BusinessPlace",
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

            return newBusinessPlace;
        }
        public async Task Delete(BusinessPlace businessPlaces)
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
                //    TransactionID = businessPlaces.ID,
                //    TransactionName = "BusinessPlace",
                //    User = 1,
                //    Note = note.ToString()
                //});
                businessPlaces.Status = 0;

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
            //_unitOfWork.BusinessPlaces.Remove(businessPlaces);
        }
        public async Task<IEnumerable<BusinessPlace>> GetAll()
        {
            return await _unitOfWork.BusinessPlaces.GetAllAsync();
        }
        public async Task<BusinessPlace> GetById(int id)
        {
            return await _unitOfWork.BusinessPlaces.GetByIdAsync(id);
        }

        public async Task<BusinessPlace> GetByBusinessId(int id)
        {
            return await _unitOfWork.BusinessPlaces.GetByBusinessIdAsync(id);
        }

        public async Task<BusinessPlace> GetByAssessmentNo(string AssessmentNo)
        {
            return await _unitOfWork.BusinessPlaces.GetByAssessmentNoAsync(AssessmentNo);
        }
        public async Task Update(BusinessPlace businessPlacesToBeUpdated, BusinessPlace businessPlaces)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (businessPlacesToBeUpdated.NameEnglish != businessPlaces.NameEnglish)
                //    note.Append(" English Name :" + businessPlacesToBeUpdated.NameEnglish + " Change to " + businessPlaces.NameEnglish);
                //if (businessPlacesToBeUpdated.NameSinhala != businessPlaces.NameSinhala)
                //    note.Append(" Sinhala Name :" + businessPlacesToBeUpdated.NameSinhala + " Change to " + businessPlaces.NameSinhala);
                //if (businessPlacesToBeUpdated.NameTamil != businessPlaces.NameTamil)
                //    note.Append(" Tamil Name :" + businessPlacesToBeUpdated.NameTamil + " Change to " + businessPlaces.NameTamil);
                //if (businessPlacesToBeUpdated.Code != businessPlaces.Code)
                //    note.Append(" Code :" + businessPlacesToBeUpdated.Code + " Change to " + businessPlaces.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = businessPlacesToBeUpdated.ID,
                //    TransactionName = "BusinessPlace",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion
                
                businessPlacesToBeUpdated.RIUserId = businessPlaces.RIUserId;
                businessPlacesToBeUpdated.GnDivision = businessPlaces.GnDivision;
                businessPlacesToBeUpdated.Ward = businessPlaces.Ward;
                businessPlacesToBeUpdated.Street = businessPlaces.Street;
                businessPlacesToBeUpdated.AssessmentNo = businessPlaces.AssessmentNo;
                businessPlacesToBeUpdated.Address1 = businessPlaces.Address1;
                businessPlacesToBeUpdated.Address2 = businessPlaces.Address2;
                businessPlacesToBeUpdated.City = businessPlaces.City;
                businessPlacesToBeUpdated.Zip = businessPlaces.Zip;
                businessPlacesToBeUpdated.Status = businessPlaces.Status;
                businessPlacesToBeUpdated.UpdatedAt = System.DateTime.Now;
                businessPlacesToBeUpdated.UpdatedBy = businessPlaces.UpdatedBy;
                businessPlacesToBeUpdated.BusinessId = businessPlaces.BusinessId;
                businessPlacesToBeUpdated.PropertyOwnerId = businessPlaces.PropertyOwnerId;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }
        public async Task<IEnumerable<BusinessPlace>> GetAllForSabha(int sabhaid)
        {
            return await _unitOfWork.BusinessPlaces.GetAllForSabhaAsync(sabhaid);
        }

        public async Task<IEnumerable<BusinessPlace>> GetAllForPropertyOwnerId(int PropertyOwnerId)
        {
            return await _unitOfWork.BusinessPlaces.GetAllForPropertyOwnerIdAsync(PropertyOwnerId);
        }
    }
}