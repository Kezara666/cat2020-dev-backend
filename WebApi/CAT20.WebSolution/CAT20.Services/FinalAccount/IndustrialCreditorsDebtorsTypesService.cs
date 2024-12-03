using CAT20.Core;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Services.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.FinalAccount
{
    public class IndustrialCreditorsDebtorsTypesService : IIndustrialCreditorsDebtorsTypesService
    {
        private readonly IVoteUnitOfWork _unitOfWork;

        public IndustrialCreditorsDebtorsTypesService(IVoteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<IndustrialCreditorsDebtorsTypes>> GetAllCreditorsTypesActive()
        {
            return _unitOfWork.IndustrialCreditorsDebtorsTypes.GetAllCreditorsTypesActive();
        }   
        public Task<IEnumerable<IndustrialCreditorsDebtorsTypes>> GetAllDebtorsTypesActive()
        {
            return _unitOfWork.IndustrialCreditorsDebtorsTypes.GetAllDebtorsTypesActive();
        }
    }
}
