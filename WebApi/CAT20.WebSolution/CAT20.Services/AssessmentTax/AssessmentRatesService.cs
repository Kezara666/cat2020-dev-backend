using CAT20.Core;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Services.AssessmentTax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.AssessmentTax
{
    public class AssessmentRatesService : IAssessmentRatesService
    {
        private readonly IAssessmentTaxUnitOfWork _unitOfWork;

        public AssessmentRatesService(IAssessmentTaxUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AssessmentRates> GetById(int id)
        {
           return await _unitOfWork.AssessmentRates.GetByIdAsync(id);
        }
    }
}
