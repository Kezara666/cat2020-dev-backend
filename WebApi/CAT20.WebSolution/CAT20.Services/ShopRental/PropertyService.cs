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

    public class PropertyService : IPropertyService
    {
        private readonly IShopRentalUnitOfWork _unitOfWork;
        public PropertyService(IShopRentalUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Property> GetById(int id)
        {
            return await _unitOfWork.Properties.GetById(id);
        }

        public async Task<IEnumerable<Property>> GetAll()
        {
            return await _unitOfWork.Properties.GetAll();
        }

        public async Task<IEnumerable<Property>> GetAllForSabha(int sabhaId)
        {
            return await _unitOfWork.Properties.GetAllForSabha(sabhaId);
        }

        public async Task<IEnumerable<Property>> GetAllForOffice(int officeId)
        {
            try
            {
                return await _unitOfWork.Properties.GetAllForOffice(officeId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IEnumerable<Property>> GetAllByPropertyTypeAndSabha(int propertyType, int sabhaId)
        {
            return await _unitOfWork.Properties.GetAllByPropertyTypeAndSabha(propertyType, sabhaId);
        }

        public async Task<IEnumerable<Property>> GetAllByPropertyTypeAndOffice(int propertyType, int officeId)
        {
            return await _unitOfWork.Properties.GetAllByPropertyTypeAndSabha(propertyType, officeId);
        }

        public async Task<IEnumerable<Property>> GetAllByPropertyNature(int propertyNature)
        {
            return await _unitOfWork.Properties.GetAllByPropertyNature(propertyNature);
        }

        public async Task<IEnumerable<Property>> GetAllByPropertyType(int propertyNature)
        {
            return await _unitOfWork.Properties.GetAllByPropertyType(propertyNature);
        }

        public async Task<IEnumerable<Property>> GetAllByPropertyNatureAndSabha(int propertyNature, int sabhaId)
        {
            return await _unitOfWork.Properties.GetAllByPropertyNatureAndSabha(propertyNature, sabhaId);
        }

        public async Task<IEnumerable<Property>> GetAllByPropertyNatureAndOffice(int propertyNature, int officeId)
        {
            return await _unitOfWork.Properties.GetAllByPropertyNatureAndOffice(propertyNature, officeId);
        }

        public async Task<Property> GetByPropertyNo(string propertyNo)
        {
            return await _unitOfWork.Properties.GetByPropertyNo(propertyNo);
        }

        //public async Task<IEnumerable<Property>> GetAllForRentalPlace(int rentalPlaceId)
        //{
        //    return await _unitOfWork.Properties.GetAllForRentalPlace(rentalPlaceId);
        //}

        public async Task<IEnumerable<Property>> GetAllForFloor(int floorId)
        {
            return await _unitOfWork.Properties.GetAllForFloor(floorId);
        }

        public async Task<Property> Create(Property newProperty)
        {
            try
            {
                await _unitOfWork.Properties
                .AddAsync(newProperty);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newProperty.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newProperty.ID,
                //    TransactionName = "Property",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                return null;
            }

            return newProperty;
        }


        public async Task Update(Property rentalPlaceToBeUpdated, Property rentalPlace)
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
                //    TransactionName = "Property",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                rentalPlaceToBeUpdated.Status = rentalPlace.Status;
                rentalPlaceToBeUpdated.PropertyTypeId = rentalPlace.PropertyTypeId;
                //rentalPlaceToBeUpdated.RentalPlaceId = rentalPlace.RentalPlaceId;
                rentalPlaceToBeUpdated.FloorId = rentalPlace.FloorId;
                rentalPlaceToBeUpdated.PropertyNo = rentalPlace.PropertyNo;
                rentalPlaceToBeUpdated.PropertyNatureId = rentalPlace.PropertyNatureId;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }

        public async Task Delete(Property obj)
        {
            try { 
            _unitOfWork.Properties.Remove(obj);
            await _unitOfWork.CommitAsync();
            }
            catch   (Exception ex)
            {

            }
        }

        //--------
        public async Task<IEnumerable<Property>> GetAllZeroOpeningBlannceForFloor(int floorId)
        {
            return await _unitOfWork.Properties.GetAllZeroOpeningBlannceForFloor(floorId);
        }

        public async Task<IEnumerable<Property>> GetAllZeroVotesForFloor(int floorId)
        {
            return await _unitOfWork.Properties.GetAllZeroVotesForFloor(floorId);
        }
        //--------


        //--------
        public async Task<IEnumerable<Property>> GetAllZeroShopsForFloor(int floorId)
        {
            return await _unitOfWork.Properties.GetAllZeroShopsForFloor(floorId);
        }

        public async Task<IEnumerable<Property>> GetByIds(List<int?> propertyKeyIds)
        {
            return await _unitOfWork.Properties.GetByIds(propertyKeyIds);
        }
        //--------
    }
}