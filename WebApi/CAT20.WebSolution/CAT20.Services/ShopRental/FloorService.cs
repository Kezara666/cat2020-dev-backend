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
using CAT20.Core.Models.TradeTax;

namespace CAT20.Services.ShopRental
{

    public class FloorService : IFloorService
    {
        private readonly IShopRentalUnitOfWork _unitOfWork;
        public FloorService(IShopRentalUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Floor> GetById(int id)
        {
            return await _unitOfWork.Floors.GetById(id);
        }

        public async Task<IEnumerable<Floor>> GetAll()
        {
            return await _unitOfWork.Floors.GetAll();
        }

        public async Task<IEnumerable<Floor>> GetAllForRentalPlace(int rentalPlaceId)
        {
            return await _unitOfWork.Floors.GetAllForRentalPlace(rentalPlaceId);
        }

        public async Task<Floor> Create(Floor newFloor)
        {
            try
            {
                await _unitOfWork.Floors
                .AddAsync(newFloor);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newFloor.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newFloor.ID,
                //    TransactionName = "Floor",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
            }

            return newFloor;
        }


        public async Task Update(Floor floorToBeUpdated, Floor floor)
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
                //    TransactionName = "Floor",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                //rentalPlaceToBeUpdated.IsActive = 1;
                //rentalPlaceToBeUpdated.DateModified = DateTime.Now;
                //rentalPlaceToBeUpdated.BankAccountId = rentalPlace.BankAccountId;
                //rentalPlaceToBeUpdated.OfficeId = rentalPlace.OfficeId;
                //rentalPlaceToBeUpdated.VoteId = rentalPlace.VoteId;

                floorToBeUpdated.Name = floor.Name;
                floorToBeUpdated.Number = floor.Number;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }

        public async Task Delete(Floor obj)
        {
            _unitOfWork.Floors.Remove(obj);
            await _unitOfWork.CommitAsync();
        }
    }
}