using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Services.ShopRental;
using CAT20.Core.Models.Common;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Resources;
using System.Transactions;
using CAT20.Common.Envelop;
using CAT20.Core.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using CAT20.Core.Models.Control;

namespace CAT20.Services.ShopRental
{

    public class PropertyNatureService : IPropertyNatureService
    {
        private readonly IShopRentalUnitOfWork _unitOfWork;
        public PropertyNatureService(IShopRentalUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PropertyNature> GetById(int id)
        {
            return await _unitOfWork.PropertyNatures.GetById(id);
        }

        public async Task<IEnumerable<PropertyNature>> GetAll()
        {
            return await _unitOfWork.PropertyNatures.GetAll();
        }

        public async Task<IEnumerable<PropertyNature>> GetAllForSabha(int sabhaId)
        {
            return await _unitOfWork.PropertyNatures.GetAllForSabha(sabhaId);
        }

        public async Task<PropertyNature> Create(PropertyNature newPropertyNature)
        {
            try
            {
                await _unitOfWork.PropertyNatures
                .AddAsync(newPropertyNature);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newPropertyNature.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newPropertyNature.ID,
                //    TransactionName = "PropertyNature",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
            }

            return newPropertyNature;
        }


        public async Task Update(PropertyNature proportyNatureToBeUpdated, PropertyNature propertyNature)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (rentalPlaceToBeUpdated.NameEnglish != rentalPlace.NameEnglish)
                //    note.Append(" English Name :" + rentalPlaceToBeUpdated.NameEnglish + " Change to " + rentalPlace.NameEnglish);
                //if (rentalPlaceToBeUpdated.NameSinhala != rentalPlace.NameSinhala)
                //    note.Append(" Sinhala Name :" + rentalPlaceToBeUpdated.NameSinhala + " Change to " + rentalPlace.NameSinhala);
                //if (rentalPlaceToBeUpdated.NameTamil != rentalPlace.NameTamil)
                //    note.Append(" Tamil Name :" + rentalPlaceToBeUpdated.NameTamil + " Change to " + rentalPlace.NameTamil);
                //if (rentalPlaceToBeUpdated.Code != rentalPlace.Code)
                //    note.Append(" Code :" + rentalPlaceToBeUpdated.Code + " Change to " + rentalPlace.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = rentalPlaceToBeUpdated.ID,
                //    TransactionName = "PropertyNature",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                proportyNatureToBeUpdated.Name = propertyNature.Name;
                //rentalPlaceToBeUpdated.DateModified = DateTime.Now;
                //rentalPlaceToBeUpdated.BankAccountId = rentalPlace.BankAccountId;
                //rentalPlaceToBeUpdated.OfficeId = rentalPlace.OfficeId;
                //rentalPlaceToBeUpdated.VoteId = rentalPlace.VoteId;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }

        public async Task Delete(PropertyNature obj)
        {
            _unitOfWork.PropertyNatures.Remove(obj);
            await _unitOfWork.CommitAsync();
        }
}
}