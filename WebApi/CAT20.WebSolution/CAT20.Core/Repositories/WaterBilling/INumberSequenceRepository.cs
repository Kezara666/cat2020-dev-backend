using CAT20.Core.Models.WaterBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.WaterBilling
{
    public interface INumberSequenceRepository : IRepository<NumberSequence>
    {
        Task<NumberSequence> GetNumberByOfficeId(int officeId);
        Task UpdateAsync(NumberSequence entity);
        Task UpdateCoreNumberAsync(NumberSequence entity);
        Task UpdateApplicationNumberAsync(NumberSequence entity);



    }
}
