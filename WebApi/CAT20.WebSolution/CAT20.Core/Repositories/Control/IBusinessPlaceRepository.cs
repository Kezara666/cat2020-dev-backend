using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.Control
{
    public interface IBusinessPlaceRepository : IRepository<BusinessPlace>
    {
        Task<IEnumerable<BusinessPlace>> GetAllAsync();
        Task<BusinessPlace> GetByIdAsync(int id);
        Task<BusinessPlace> GetByAssessmentNoAsync(string AssessmentNo);
        Task<BusinessPlace> GetByBusinessIdAsync(int BusinessId);
        Task<IEnumerable<BusinessPlace>> GetAllForSabhaAsync(int sabhaId); 
        Task<IEnumerable<BusinessPlace>> GetAllForPropertyOwnerIdAsync(int PropertyOwnerId); 
    }
}
