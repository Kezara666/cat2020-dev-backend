using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.Control
{
    public interface IGnDivisionsRepository : IRepository<GnDivisions>
    {
        Task<IEnumerable<GnDivisions>> GetAllAsync();
        Task<GnDivisions> GetByIdAsync(int id);
        Task<IEnumerable<GnDivisions>> GetAllForOfficeAsync(int officeid);
        Task<IEnumerable<GnDivisions>> SearchAsync(int officeid, string description, string? code);
        
        //linking repository

        Task<IEnumerable<GnDivisions>> GetAllForListAsync(List<int> gnDivisionIdList);
    }
}
