using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Vote;
using CAT20.Core.Models.Common;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace CAT20.Services.Vote
{
    public class ProgrammeService : IProgrammeService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        public ProgrammeService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Programme> CreateProgramme(Programme newProgramme)
        {
            try
            {
                await _unitOfWork.Programmes
                .AddAsync(newProgramme);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newProgramme.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newProgramme.ID,
                //    TransactionName = "Programme",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }

            return newProgramme;
        }
        public async Task DeleteProgramme(Programme programme)
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
                //    TransactionID = programme.ID,
                //    TransactionName = "Programme",
                //    User = 1,
                //    Note = note.ToString()
                //});
                programme.Status = 0;

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
            //_unitOfWork.Programmes.Remove(programme);
        }
        public async Task<IEnumerable<Programme>> GetAllProgrammes()
        {
            return await _unitOfWork.Programmes.GetAllAsync();
        }
        public async Task<Programme> GetProgrammeById(int id)
        {
            return await _unitOfWork.Programmes.GetByIdAsync(id);
        }
        public async Task UpdateProgramme(Programme programmeToBeUpdated, Programme programme)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (programmeToBeUpdated.NameEnglish != programme.NameEnglish)
                //    note.Append(" English Name :" + programmeToBeUpdated.NameEnglish + " Change to " + programme.NameEnglish);
                //if (programmeToBeUpdated.NameSinhala != programme.NameSinhala)
                //    note.Append(" Sinhala Name :" + programmeToBeUpdated.NameSinhala + " Change to " + programme.NameSinhala);
                //if (programmeToBeUpdated.NameTamil != programme.NameTamil)
                //    note.Append(" Tamil Name :" + programmeToBeUpdated.NameTamil + " Change to " + programme.NameTamil);
                //if (programmeToBeUpdated.Code != programme.Code)
                //    note.Append(" Code :" + programmeToBeUpdated.Code + " Change to " + programme.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = programmeToBeUpdated.ID,
                //    TransactionName = "Programme",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                programmeToBeUpdated.NameSinhala = programme.NameSinhala;
                programmeToBeUpdated.NameTamil = programme.NameTamil;
                programmeToBeUpdated.NameEnglish = programme.NameEnglish;
                programmeToBeUpdated.Code = programme.Code;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }

        public async Task<IEnumerable<Programme>> GetAllProgrammesForSabhaId(int id)
        {
            return await _unitOfWork.Programmes.GetAllProgrammesForSabhaIdAsync(id);
        }

    }
}