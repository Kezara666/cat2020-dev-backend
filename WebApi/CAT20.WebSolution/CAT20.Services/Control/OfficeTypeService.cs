using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;
using CAT20.Core.Services.Control;

namespace CAT20.Services.Control
{
    public class OfficeTypeService : IOfficeTypeService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public OfficeTypeService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OfficeType> CreateOfficeType(OfficeType newOfficeType)
        {
            await _unitOfWork.OfficeTypes
                .AddAsync(newOfficeType);
            await _unitOfWork.CommitAsync();

            return newOfficeType;
        }
        public async Task DeleteOfficeType(OfficeType officeType)
        {
            _unitOfWork.OfficeTypes.Remove(officeType);

            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<OfficeType>> GetAllOfficeTypes()
        {
            return await _unitOfWork.OfficeTypes.GetAllAsync();
        }
        public async Task<OfficeType> GetOfficeTypeById(int id)
        {
            return await _unitOfWork.OfficeTypes.GetByIdAsync(id);
        }
        public async Task UpdateOfficeType(OfficeType officeTypeToBeUpdated, OfficeType officeType)
        {
            //officeTypeToBeUpdated.Name = officeType.t;

            await _unitOfWork.CommitAsync();
        }
    }
}