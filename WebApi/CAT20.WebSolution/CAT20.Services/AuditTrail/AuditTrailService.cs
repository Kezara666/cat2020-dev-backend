using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.AuditTrails;
using CAT20.Core.Services.AuditTrails;
using CAT20.Common;

namespace CAT20.Services.AuditTrails
{
    public class AuditTrailService : LoggerBase, IAuditTrailService
    {
        private readonly IAuditTrailUnitOfWork _unitOfWork;
        public AuditTrailService(IAuditTrailUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<AuditTrail> CreateAuditTrail(AuditTrail newAuditTrail)
        {
            await _unitOfWork.AuditTrails
                .AddAsync(newAuditTrail);
            await _unitOfWork.CommitAsync();

            return newAuditTrail;
        }
        protected override Type LogPrefix
        {
            get { return this.GetType(); }
        }

        public async Task<AuditTrail> Save(AuditTrail newAuditTrail)
        {
            await _unitOfWork.AuditTrails
                .AddAsync(newAuditTrail);
            await _unitOfWork.CommitAsync();

            return newAuditTrail;
        }
        //public async Task DeleteAuditTrail(AuditTrail AuditTrail)
        //{
        //    _unitOfWork.AuditTrails.Remove(AuditTrail);

        //    await _unitOfWork.CommitAsync();
        //}
        //public async Task<IEnumerable<AuditTrail>> GetAllAuditTrails()
        //{
        //    return await _unitOfWork.AuditTrails.GetAllAsync();
        //}
        //public async Task<AuditTrail> GetAuditTrailById(int id)
        //{
        //    return await _unitOfWork.AuditTrails.GetByIdAsync(id);
        //}
        //public async Task UpdateAuditTrail(AuditTrail AuditTrailToBeUpdated, AuditTrail AuditTrail)
        //{
        //    //AuditTrailToBeUpdated.Name = AuditTrail.t;

        //    await _unitOfWork.CommitAsync();
        //}
    }
}