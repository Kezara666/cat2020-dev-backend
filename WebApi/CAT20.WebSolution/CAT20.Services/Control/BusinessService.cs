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
using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Services.Control
{
    public class BusinessService : IBusinessService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public BusinessService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //public async Task<Business> CreateBusiness(Business newBusiness)
        //{
        //    try
        //    {
        //        await _unitOfWork.Businesses.AddAsync(newBusiness);

        //        #region Audit Log
        //        //var note = new StringBuilder();
        //        //if (newBusiness.ID == 0)
        //        //    note.Append("Created on ");
        //        //else
        //        //    note.Append("Edited on ");
        //        //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
        //        //note.Append(" by ");
        //        //note.Append("System");


        //        //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
        //        //{
        //        //    dateTime = DateTime.Now,
        //        //    TransactionID = newBusiness.ID,
        //        //    TransactionName = "Business",
        //        //    User = 1,
        //        //    Note = note.ToString()
        //        //});

        //        #endregion

        //         await _unitOfWork.CommitAsync();
        //        return newBusiness;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return newBusiness;
        //    //entity.GetType().GetProperty("Id").GetValue(entity, null);
        //}

        public async Task<Business> CreateBusiness(Business newBusiness)
        {
            try
            {
                await _unitOfWork.Businesses.AddAsync(newBusiness);
                await _unitOfWork.CommitAsync(); 
                return newBusiness;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("Concurrency Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null; 
        }

        public async Task Delete(Business businesses)
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
                //    TransactionID = businesses.ID,
                //    TransactionName = "Business",
                //    User = 1,
                //    Note = note.ToString()
                //});
                businesses.Status = 0;

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
            //_unitOfWork.Businesses.Remove(businesses);
        }
        public async Task<IEnumerable<Business>> GetAll()
        {
            return await _unitOfWork.Businesses.GetAllAsync();
        }
        public async Task<Business> GetById(int id)
        {
            return await _unitOfWork.Businesses.GetByIdAsync(id);
        }

        public async Task<Business> GetByRegNo(string RegNo)
        {
            return await _unitOfWork.Businesses.GetByRegNoAsync(RegNo);
        }

        public async Task<Business> GetByApplicationNo(string RegNo)
        {
            return await _unitOfWork.Businesses.GetByApplicationNoAsync(RegNo);
        }

        public async Task<Business> GetByRegNoAndOffice(string RegNo, int officeId)
        {
            return await _unitOfWork.Businesses.GetByRegNoAndOfficeAsync(RegNo, officeId);
        }

        public async Task<Business> GetByApplicationNoAndOffice(string ApplicationNo, int officeId)
        {
            return await _unitOfWork.Businesses.GetByApplicationNoAndOfficeAsync(ApplicationNo, officeId);
        }

        public async Task Update(Business businessesToBeUpdated, Business businesses)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (businessesToBeUpdated.NameEnglish != businesses.NameEnglish)
                //    note.Append(" English Name :" + businessesToBeUpdated.NameEnglish + " Change to " + businesses.NameEnglish);
                //if (businessesToBeUpdated.NameSinhala != businesses.NameSinhala)
                //    note.Append(" Sinhala Name :" + businessesToBeUpdated.NameSinhala + " Change to " + businesses.NameSinhala);
                //if (businessesToBeUpdated.NameTamil != businesses.NameTamil)
                //    note.Append(" Tamil Name :" + businessesToBeUpdated.NameTamil + " Change to " + businesses.NameTamil);
                //if (businessesToBeUpdated.Code != businesses.Code)
                //    note.Append(" Code :" + businessesToBeUpdated.Code + " Change to " + businesses.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = businessesToBeUpdated.ID,
                //    TransactionName = "Business",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                businessesToBeUpdated.BusinessName = businesses.BusinessName;
                businessesToBeUpdated.BusinessSubOwners = businesses.BusinessSubOwners;
                businessesToBeUpdated.BusinessNature = businesses.BusinessNature;
                businessesToBeUpdated.BusinessSubNature = businesses.BusinessSubNature;
                businessesToBeUpdated.BusinessNatureId = businesses.BusinessNatureId;
                businessesToBeUpdated.BusinessSubNatureId = businesses.BusinessSubNatureId;
                businessesToBeUpdated.BusinessStartDate = businesses.BusinessStartDate;
                businessesToBeUpdated.BusinessRegNo = businesses.BusinessRegNo;
                businessesToBeUpdated.TaxType = businesses.TaxType;
                businessesToBeUpdated.TaxTypeId = businesses.TaxTypeId;
                businessesToBeUpdated.BusinessTelNo = businesses.BusinessTelNo;
                businessesToBeUpdated.BusinessEmail = businesses.BusinessEmail;
                businessesToBeUpdated.BusinessWeb = businesses.BusinessWeb;
                businessesToBeUpdated.NoOfEmployees = businesses.NoOfEmployees;
                //businessesToBeUpdated.LastYearValue = businesses.LastYearValue;
                //businessesToBeUpdated.CurrentYear = businesses.CurrentYear;
                //businessesToBeUpdated.OtherCharges = businesses.OtherCharges;
                //businessesToBeUpdated.AnnualValue = businesses.AnnualValue;
                //businessesToBeUpdated.TaxAmountByNature = businesses.TaxAmountByNature;
                //businessesToBeUpdated.TaxAmount = businesses.TaxAmount;
                //businessesToBeUpdated.TotalTaxAmount = businesses.TotalTaxAmount;
                businessesToBeUpdated.UpdatedAt = businesses.UpdatedAt;
                businessesToBeUpdated.Status = businesses.Status;
                businessesToBeUpdated.UpdatedBy = businesses.UpdatedBy;
                businessesToBeUpdated.BusinessOwnerId = businesses.BusinessOwnerId;
                businessesToBeUpdated.PropertyOwnerId = businesses.PropertyOwnerId;
                businessesToBeUpdated.BusinessPlaceId = businesses.BusinessPlaceId;
                //businessesToBeUpdated.BusinessTaxes[0].ApplicationNo = businesses.BusinessTaxes[0].ApplicationNo;
                //businessesToBeUpdated.BusinessTaxes[0].AnnualValue = businesses.BusinessTaxes[0].AnnualValue;
                //businessesToBeUpdated.BusinessTaxes[0].TotalTaxAmount = businesses.BusinessTaxes[0].TotalTaxAmount;
                //businessesToBeUpdated.BusinessTaxes[0].TotalTaxAmount = businesses.BusinessTaxes[0].TotalTaxAmount;
                //businessesToBeUpdated.BusinessTaxes[0].LastYearValue = businesses.BusinessTaxes[0].LastYearValue;

                //foreach (var BusinessTax in businessesToBeUpdated.BusinessTaxes)
                //{
                //    BusinessTax.ApplicationNo = businesses.BusinessTaxes.Where(m => m.Id == BusinessTax.Id).First().ApplicationNo;
                //    BusinessTax.AnnualValue = businesses.BusinessTaxes.Where(m => m.Id == BusinessTax.Id).First().AnnualValue;
                //    BusinessTax.LastYearValue = businesses.BusinessTaxes.Where(m => m.Id == BusinessTax.Id).First().LastYearValue;
                //}

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }
        public async Task<IEnumerable<Business>> GetAllForSabha(int sabhaid)
        {
            return await _unitOfWork.Businesses.GetAllForSabhaAsync(sabhaid);
        }

        public async Task<IEnumerable<Business>> GetAllForBusinessOwnerId(int BusinessOwnerid)
        {
            return await _unitOfWork.Businesses.GetAllForBusinessOwnerIdAsync(BusinessOwnerid);
        }

        public async Task<IEnumerable<Business>> GetAllBusinessAndIndustrialTaxesForOwnerId(int BusinessOwnerid)
        {
            return await _unitOfWork.Businesses.GetAllBusinessAndIndustrialTaxesForOwnerId(BusinessOwnerid);
        }

        public async Task<IEnumerable<Business>> GetAllBusinessAndIndustrialTaxesForOwnerIdAndSabhaId(int BusinessOwnerid, int sabhaid)
        {
            return await _unitOfWork.Businesses.GetAllBusinessAndIndustrialTaxesForOwnerIdAndSabhaId(BusinessOwnerid, sabhaid);
        }

        public async Task<IEnumerable<Business>> GetAllForBusinessNatureAndSabha(int id, int sabhaid)
        {
            return await _unitOfWork.Businesses.GetAllForBusinessNatureAndSabhaAsync(id,  sabhaid);
        }

        public async Task<IEnumerable<Business>> GetAllForBusinessSubNatureAndSabha(int id, int sabhaid)
        {
            return await _unitOfWork.Businesses.GetAllForBusinessSubNatureAndSabhaAsync(id, sabhaid);
        }

        public async Task<IEnumerable<Business>> GetAllBusinessAndIndustrialTaxesForSabha(int sabhaid)
        {
            return await _unitOfWork.Businesses.GetAllBusinessAndIndustrialTaxesForSabha(sabhaid);
        }

        public async Task<IEnumerable<Business>> GetAllBusinessLicensesForSabha(int sabhaid)
        {
            return await _unitOfWork.Businesses.GetAllBusinessLicensesForSabha(sabhaid);
        }

        public async Task<IEnumerable<Business>> GetAllBusinessAndIndustrialTaxesForSabhaAndOfficer(int sabhaid, int officerid)
        {
            return await _unitOfWork.Businesses.GetAllBusinessAndIndustrialTaxesForSabhaAndOfficer(sabhaid,officerid);
        }

        public async Task<IEnumerable<Business>> GetAllBusinessLicensesForSabhaAndOfficer(int sabhaid, int officerid)
        {
            return await _unitOfWork.Businesses.GetAllBusinessLicensesForSabhaAndOfficer(sabhaid, officerid);
        }


    }
}