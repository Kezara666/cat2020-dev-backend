using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;

namespace CAT20.Services.Control
{
    public class SabhaService : ISabhaService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public SabhaService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Sabha> CreateSabha(Sabha newSabha)
        {
            await _unitOfWork.Sabhas
                .AddAsync(newSabha);
            await _unitOfWork.CommitAsync();

            return newSabha;
        }
        public async Task DeleteSabha(Sabha sabha)
        {
            _unitOfWork.Sabhas.Remove(sabha);

            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<Sabha>> GetAllSabhas()
        {
            return await _unitOfWork.Sabhas.GetAllAsync();
        } 
        public async Task<Sabha> GetSabhaById(int id)
        {
            try { 
            return await _unitOfWork.Sabhas.GetWithSabhaByIdAsync(id);
            }
            catch(Exception ex) 
            {
                return null;
            }
        }
        public async Task<IEnumerable<Sabha>>  GetSabhaByDistrictId(int districtID)
        {
            return await _unitOfWork.Sabhas.GetSabhaByDistrictId(districtID);
        }
        public async Task UpdateSabha(Sabha sabhaToBeUpdated, Sabha sabha)
        {

            await _unitOfWork.CommitAsync();
        }
    }
}