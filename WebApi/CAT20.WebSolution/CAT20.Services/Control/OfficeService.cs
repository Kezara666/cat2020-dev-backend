using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;

namespace CAT20.Services.Control
{
    public class OfficeService : IOfficeService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public OfficeService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Office> CreateOffice(Office newOffice)
        {
            try
            {
                var office = await _unitOfWork.Offices.GetByIdAsync(newOffice.ID!.Value);
                if(office !=null){
                    office.SabhaID = newOffice.SabhaID;
                    office.NameSinhala = newOffice.NameSinhala;
                    office.NameTamil = newOffice.NameTamil;
                    office.NameEnglish = newOffice.NameEnglish;
                    office.OfficeTypeID = newOffice.OfficeTypeID;
                    office.Code = newOffice.Code;


                }
                else
                {
                await _unitOfWork.Offices.AddAsync(newOffice);

                }
                await _unitOfWork.CommitAsync();

                return newOffice;
            }catch (Exception ex)
            {
                return null;
            }
        }
        public async Task DeleteOffice(Office office)
        {
            _unitOfWork.Offices.Remove(office);

            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<Office>> GetAllOffices()
        {
            return await _unitOfWork.Offices.GetAllAsync();
        }
        public async Task<IEnumerable<Office>> getAllOfficesForSabhaId(int id)
        {
            return await _unitOfWork.Offices.GetAllWithOfficeBySabhaIdAsync(id);
        }

        public async Task<IEnumerable<Office>> getAllOfficesForSabhaIdAndOfficeType(int id, int otype)
        {
            return await _unitOfWork.Offices.GetAllWithOfficeBySabhaIdAndOfficeTypeAsync(id, otype);
        }

        
        public async Task<Office> GetOfficeById(int id)
        {
            return await _unitOfWork.Offices.GetByIdAsync(id);
        }

        public async Task<Office> GetOfficeByIdWithSabhaDetails(int id)
        {
            return await _unitOfWork.Offices.GetOfficeByIdWithSabhaDetails(id);
        }

        public async Task UpdateOffice(Office officeToBeUpdated, Office office)
        {
            //officeToBeUpdated.NameEnglish = office.NameEnglisht;
            //officeToBeUpdated.NameSinhala = office.NameSinhala;
            //officeToBeUpdated.NameTamil = office.NameTamil;
            //officeToBeUpdated.Code = office.Code;
            //officeToBeUpdated.OfficeTypeID = office.OfficeTypeID;

            await _unitOfWork.CommitAsync();
        }
    }
}