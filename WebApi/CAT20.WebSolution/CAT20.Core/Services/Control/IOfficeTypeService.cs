using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface IOfficeTypeService
    {
        Task<IEnumerable<OfficeType>> GetAllOfficeTypes();
        Task<OfficeType> GetOfficeTypeById(int id);
        Task<OfficeType> CreateOfficeType(OfficeType newOfficeType);
        Task UpdateOfficeType(OfficeType officeTypeToBeUpdated, OfficeType officeType);
        Task DeleteOfficeType(OfficeType officeType);
    }
}

