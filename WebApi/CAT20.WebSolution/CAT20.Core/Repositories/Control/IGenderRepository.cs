using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.Control
{
    public interface IGenderRepository : IRepository<Gender>
    {
        Task<IEnumerable<Gender>> GetAllWithGenderAsync();
        Task<Gender> GetWithGenderByIdAsync(int id);
        Task<IEnumerable<Gender>> GetAllWithGenderByGenderIdAsync(int Id);
    }
}
