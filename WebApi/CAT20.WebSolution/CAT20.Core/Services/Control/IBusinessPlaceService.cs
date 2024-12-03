using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface IBusinessPlaceService
    {
        Task<IEnumerable<BusinessPlace>> GetAll();
        Task<BusinessPlace> GetById(int id);
        Task<BusinessPlace> GetByBusinessId(int id);
        Task<BusinessPlace> GetByAssessmentNo(string AssessmentNo);
        Task<BusinessPlace> Create(BusinessPlace newBusinessPlace);
        Task Update(BusinessPlace businessPlaceToBeUpdated, BusinessPlace businessPlace);
        Task Delete(BusinessPlace businessPlace);
        Task<IEnumerable<BusinessPlace>> GetAllForSabha(int id);
        Task<IEnumerable<BusinessPlace>> GetAllForPropertyOwnerId(int id);
    }
}

