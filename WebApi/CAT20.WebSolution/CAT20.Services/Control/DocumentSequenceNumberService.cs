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
using CAT20.Core.Models.AssessmentTax;

namespace CAT20.Services.Control
{
    public class DocumentSequenceNumberService : IDocumentSequenceNumberService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public DocumentSequenceNumberService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<DocumentSequenceNumber> GetNextSequenceNumberForYearOfficePrefix(int year, int officeid, string prefix)
        {
            return await _unitOfWork.DocumentSequenceNumbers.GetNextSequenceNumberForYearOfficePrefixAsync(year, officeid, prefix);
        }

        public async Task Update(DocumentSequenceNumber documentSequenceNumberToBeUpdated, DocumentSequenceNumber documentSequenceNumber)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (documentSequenceNumberToBeUpdated.NameEnglish != documentSequenceNumber.NameEnglish)
                //    note.Append(" English Name :" + documentSequenceNumberToBeUpdated.NameEnglish + " Change to " + documentSequenceNumber.NameEnglish);
                //if (documentSequenceNumberToBeUpdated.NameSinhala != documentSequenceNumber.NameSinhala)
                //    note.Append(" Sinhala Name :" + documentSequenceNumberToBeUpdated.NameSinhala + " Change to " + documentSequenceNumber.NameSinhala);
                //if (documentSequenceNumberToBeUpdated.NameTamil != documentSequenceNumber.NameTamil)
                //    note.Append(" Tamil Name :" + documentSequenceNumberToBeUpdated.NameTamil + " Change to " + documentSequenceNumber.NameTamil);
                //if (documentSequenceNumberToBeUpdated.Code != documentSequenceNumber.Code)
                //    note.Append(" Code :" + documentSequenceNumberToBeUpdated.Code + " Change to " + documentSequenceNumber.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = documentSequenceNumberToBeUpdated.ID,
                //    TransactionName = "DocumentSequenceNumber",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                documentSequenceNumberToBeUpdated.LastIndex = documentSequenceNumber.LastIndex;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }
        public async Task<IEnumerable<DocumentSequenceNumber>> GetAllForOffice(int id)
        {
            return await _unitOfWork.DocumentSequenceNumbers.GetAllForOfficeAsync(id);
        }

        public async Task<DocumentSequenceNumber> Create(DocumentSequenceNumber newDocumentSequenceNumber)
        {
            try
            {
                await _unitOfWork.DocumentSequenceNumbers
                .AddAsync(newDocumentSequenceNumber);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return newDocumentSequenceNumber;
        }


        public async Task<bool> CheckIsExistingAndIfNotCreateSequenceNoForYear(int year, int officeid, string prefix)
        {

            try
            {
                var documentSequenceNo = await _unitOfWork.DocumentSequenceNumbers.GetNextSequenceNumberForYearOfficePrefixAsync(year, officeid, prefix);
                if (documentSequenceNo == null)
                {
                    DocumentSequenceNumber newDocumentSequenceNumber = new DocumentSequenceNumber();
                    newDocumentSequenceNumber.Year = year;
                    newDocumentSequenceNumber.OfficeId = officeid;
                    newDocumentSequenceNumber.Prefix = prefix;
                    newDocumentSequenceNumber.LastIndex = 0;

                    await _unitOfWork.DocumentSequenceNumbers
                    .AddAsync(newDocumentSequenceNumber);

                    await _unitOfWork.CommitAsync();

                    return true;


                }
                else
                {
                    return true;
                }
           
            }catch (Exception ex)
            {
                return false;
            }
        }
    }
}