using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;

namespace CAT20.Services.Control
{
    public class GnDivisionService : IGnDivisionService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public GnDivisionService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GnDivisions> CreateGnDivision(GnDivisions newGnDivision)
        {
            try
            {
                var gnDivision = await _unitOfWork.GnDivisions.GetByIdAsync(newGnDivision.Id!.Value);
                if (gnDivision != null)
                {   
                    gnDivision.OfficeId = newGnDivision.OfficeId;
                    gnDivision.Description = newGnDivision.Description;
                    gnDivision.Code = newGnDivision.Code;

                }
                else
                {
                    await _unitOfWork.GnDivisions.AddAsync(newGnDivision);

                }
                await _unitOfWork.CommitAsync();

                return newGnDivision;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //public async Task<GnDivisions> CreateGnDivision(GnDivisions newGnDivision)
        //{
        //    await _unitOfWork.GnDivisions.AddAsync(newGnDivision);
        //    await _unitOfWork.CommitAsync();

        //    return newGnDivision;
        //}

        public async Task<IEnumerable<GnDivisions>> GetAll()
        {
            return await _unitOfWork.GnDivisions.GetAllAsync();
        }
        public async Task<IEnumerable<GnDivisions>> GetAllForOffice(int Officeid)
        {
            try { 
            return await _unitOfWork.GnDivisions.GetAllForOfficeAsync(Officeid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<GnDivisions> GetAllForOffice(GnDivisions gnDivisionsData)
        {
            throw new NotImplementedException();
        }

        public async Task<GnDivisions> GetById(int id)
        {
            return await _unitOfWork.GnDivisions.GetByIdAsync(id);
        }
        public async Task<IEnumerable<GnDivisions>> Search(int officeid, string description, string? code)
        {
            return await _unitOfWork.GnDivisions.SearchAsync(officeid, description, code);
        }
    }
}