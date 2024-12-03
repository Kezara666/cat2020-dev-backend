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
using Microsoft.EntityFrameworkCore;
using CAT20.Core.Models.AuditTrails;
using DocumentFormat.OpenXml.Wordprocessing;
using Irony;
using CAT20.Core.Services.AssessmentTax;
using CAT20.Core.Services.AuditTrails;
using CAT20.Common;

namespace CAT20.Services.ShopRental
{

    public class RentalPlaceService : IRentalPlaceService
    {
        private readonly IShopRentalUnitOfWork _unitOfWork;
        private readonly IAuditTrailUnitOfWork _auditTrailUnitOfWork;
        public RentalPlaceService(IShopRentalUnitOfWork unitOfWork, IAuditTrailUnitOfWork auditTrailUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _auditTrailUnitOfWork = auditTrailUnitOfWork;
        }

        public async Task<RentalPlace> GetById(int id)
        {
            return await _unitOfWork.RentalPlaces.GetById(id);
        }

        public async Task<IEnumerable<RentalPlace>> GetAll()
        {
            return await _unitOfWork.RentalPlaces.GetAll();
        }

        public async Task<IEnumerable<RentalPlace>> GetAllForOffice(int officeid)
        {
            return await _unitOfWork.RentalPlaces.GetAllForOffice(officeid);
        }

        public async Task<IEnumerable<RentalPlace>> GetAllForSabha(int sabhhaid)
        {
            try
            {
                return await _unitOfWork.RentalPlaces.GetAllForSabha(sabhhaid);
            }
            catch { 
                return Enumerable.Empty<RentalPlace>();
            
            }
        }

        public async Task<RentalPlace> Create(RentalPlace newRentalPlace)
        {
            try
            {
                await _unitOfWork.RentalPlaces
                .AddAsync(newRentalPlace);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newRentalPlace.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newRentalPlace.ID,
                //    TransactionName = "RentalPlace",
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

            return newRentalPlace;
        }


        public async Task Update(RentalPlace rentalPlaceToBeUpdated, RentalPlace rentalPlace)
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
                //    TransactionName = "RentalPlace",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion


                ////1. valid floor rows ids
                //var validFloorRowIds = rentalPlace.Floors.Select(fl => fl.Id).ToList();

                ////2. missing floor rows
                //var missedFloors = rentalPlaceToBeUpdated.Floors
                //    .Where(fl => fl.RentalPlaceId == rentalPlaceToBeUpdated.Id && !validFloorRowIds.Contains(fl.Id)) .ToList();

                //if (missedFloors.Count > 0)
                //{
                //    foreach (var floor in missedFloors)
                //    {
                //        _unitOfWork.Floors.Remove(floor);
                //    }
                //    await _unitOfWork.CommitAsync();
                //}

                rentalPlaceToBeUpdated.Status = rentalPlace.Status;
                rentalPlaceToBeUpdated.GnDivisionId = rentalPlace.GnDivisionId;
                rentalPlaceToBeUpdated.OfficeId = rentalPlace.OfficeId;
                rentalPlaceToBeUpdated.Name = rentalPlace.Name;
                rentalPlaceToBeUpdated.AddressLine1 = rentalPlace.AddressLine1;
                rentalPlaceToBeUpdated.AddressLine2 = rentalPlace.AddressLine2;

                //rentalPlaceToBeUpdated.Floors = rentalPlace.Floors;

                //rentalPlaceToBeUpdated.City = rentalPlace.City;
                //rentalPlaceToBeUpdated.Zip = rentalPlace.Zip;
                rentalPlaceToBeUpdated.UpdatedBy = rentalPlace.UpdatedBy;
                rentalPlaceToBeUpdated.UpdatedAt = DateTime.Now;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }

        public async Task<TransferObject<RentalPlace>> Save(RentalPlace rentalPlace)
        {
            AuditTrail auditTrail = null;
            RentalPlace rentalPlaceToBeUpdated = new RentalPlace();
            try
            {
                if (rentalPlace.Id == null)
                {
                    auditTrail = Data.Resources.Utility.CreateAuditTrail(null, rentalPlace, AuditTrailAction.Insert, new List<string>(), rentalPlace.ClaimedUserID.Value);
                }
                else
                {
                    rentalPlaceToBeUpdated = await _unitOfWork.RentalPlaces.GetById(rentalPlace.Id.Value);
                    auditTrail = Data.Resources.Utility.CreateAuditTrail(rentalPlaceToBeUpdated, rentalPlace, AuditTrailAction.Update, new List<string>(), rentalPlace.ClaimedUserID.Value);
                }

                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    Timeout = TimeSpan.FromMinutes(1),
                    IsolationLevel = IsolationLevel.ReadCommitted
                }, TransactionScopeAsyncFlowOption.Enabled))
                {

                    if (rentalPlace.Id == null)  //Create New Entity
                    {
                        var addResult = _unitOfWork.RentalPlaces.Save(rentalPlace);

                        if (addResult.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
                            return addResult;
                    }
                    else //Update Existing Entity
                    {
                            ////1. valid floor rows ids
                            //var validFloorRowIds = rentalPlace.Floors.Select(fl => fl.Id).ToList();

                            ////2. missing floor rows
                            //var missedFloors = rentalPlaceToBeUpdated.Floors
                            //    .Where(fl => fl.RentalPlaceId == rentalPlaceToBeUpdated.Id && !validFloorRowIds.Contains(fl.Id)).ToList();

                            //if (missedFloors.Count > 0)
                            //{
                            //    foreach (var floor in missedFloors)
                            //    {
                            //        _unitOfWork.Floors.Remove(floor);
                            //    }
                            //    await _unitOfWork.CommitAsync();
                            //}

                            rentalPlaceToBeUpdated.Status = rentalPlace.Status;
                            rentalPlaceToBeUpdated.GnDivisionId = rentalPlace.GnDivisionId;
                            rentalPlaceToBeUpdated.OfficeId = rentalPlace.OfficeId;
                            rentalPlaceToBeUpdated.Name = rentalPlace.Name;
                            rentalPlaceToBeUpdated.AddressLine1 = rentalPlace.AddressLine1;
                            rentalPlaceToBeUpdated.AddressLine2 = rentalPlace.AddressLine2;

                            //rentalPlaceToBeUpdated.Floors = rentalPlace.Floors;

                            //rentalPlaceToBeUpdated.City = rentalPlace.City;
                            //rentalPlaceToBeUpdated.Zip = rentalPlace.Zip;
                            rentalPlaceToBeUpdated.UpdatedBy = rentalPlace.UpdatedBy;
                            rentalPlaceToBeUpdated.UpdatedAt = DateTime.Now;
                    }

                    try
                    {
                        var auditTO = _auditTrailUnitOfWork.AuditTrails.Save(auditTrail);
                        if (auditTO.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
                        {
                            return new TransferObject<RentalPlace>
                            {
                                StatusInfo = auditTO.StatusInfo
                            };
                        }

                        await _unitOfWork.CommitAsync();
                        await _auditTrailUnitOfWork.CommitAsync();

                        scope.Complete();
                    }
                    catch (Exception ex)
                    {
                        return new TransferObject<RentalPlace>
                        {
                            StatusInfo = new StatusInfo
                            {
                                Status = Common.Enums.ServiceStatus.Error,
                                Message = $"An error occurred: {ex.Message}"
                            }
                        };
                    }
                }
                return new TransferObject<RentalPlace>();
            }
            catch (Exception ex)
            {
                return new TransferObject<RentalPlace>
                {
                    StatusInfo = new StatusInfo
                    {
                        Status = Common.Enums.ServiceStatus.Error,
                        Message = $"An error occurred: {ex.Message}"
                    }
                };
            }
        }


        public async Task Delete(RentalPlace obj)
        {
            _unitOfWork.RentalPlaces.Remove(obj);
            await _unitOfWork.CommitAsync();
        }
    }
}