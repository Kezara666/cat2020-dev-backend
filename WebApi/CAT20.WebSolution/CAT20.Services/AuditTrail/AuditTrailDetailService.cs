using CAT20.Core;
using CAT20.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.AuditTrails;
using CAT20.Core.Services.AuditTrails;
using CAT20.Data.Repositories;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace CAT20.Services.AuditTrails
{
    public class AuditTrailDetailService : LoggerBase, IAuditTrailDetailService
    {
        private readonly IAuditTrailUnitOfWork _unitOfWork;
        public AuditTrailDetailService(IAuditTrailUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<AuditTrailDetail> CreateAuditTrailDetail(AuditTrailDetail newAuditTrailDetail)
        {
            await _unitOfWork.AuditTrailDetails
                .AddAsync(newAuditTrailDetail);
            await _unitOfWork.CommitAsync();

            return newAuditTrailDetail;
        }

        #region Get All For
        public async Task<IList<AuditTrailDetail>> GetAllForAsync(AuditTrail auditTrail)
        {
            return await _unitOfWork.AuditTrailDetails.GetAllForAsync(auditTrail);
        }
        #endregion

        #region Object Type

        protected override Type LogPrefix
        {
            get { return this.GetType(); }
        }

        #endregion

        //public async Task DeleteAuditTrailDetail(AuditTrailDetail AuditTrailDetail)
        //{
        //    _unitOfWork.AuditTrailDetails.Remove(AuditTrailDetail);

        //    await _unitOfWork.CommitAsync();
        //}
        //public async Task<IEnumerable<AuditTrailDetail>> GetAllAuditTrailDetails()
        //{
        //    return await _unitOfWork.AuditTrailDetails.GetAllAsync();
        //}
        //public async Task<AuditTrailDetail> GetAuditTrailDetailById(int id)
        //{
        //    return await _unitOfWork.AuditTrailDetails.GetByIdAsync(id);
        //}
        //public async Task UpdateAuditTrailDetail(AuditTrailDetail AuditTrailDetailToBeUpdated, AuditTrailDetail AuditTrailDetail)
        //{
        //    //AuditTrailDetailToBeUpdated.Name = AuditTrailDetail.t;

        //    await _unitOfWork.CommitAsync();
        //}
    }
}