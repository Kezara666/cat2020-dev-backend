using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;

namespace CAT20.Services.Control
{
    public class GenderService : IGenderService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public GenderService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Gender> CreateGender(Gender newGender)
        {
            await _unitOfWork.Genders
                .AddAsync(newGender);
            await _unitOfWork.CommitAsync();

            return newGender;
        }
        public async Task DeleteGender(Gender gender)
        {
            _unitOfWork.Genders.Remove(gender);

            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<Gender>> GetAllGenders()
        {
            return await _unitOfWork.Genders.GetAllAsync();
        }
        public async Task<Gender> GetGenderById(int id)
        {
            return await _unitOfWork.Genders.GetByIdAsync(id);
        }
        public async Task UpdateGender(Gender genderToBeUpdated, Gender gender)
        {
            //genderToBeUpdated.Name = gender.t;

            await _unitOfWork.CommitAsync();
        }
    }
}