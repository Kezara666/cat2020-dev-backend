using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface IGnDivisionService
    {
        Task<IEnumerable<GnDivisions>> GetAll();
        Task<GnDivisions> GetById(int id);
        Task<IEnumerable<GnDivisions>> GetAllForOffice(int officeid);
        Task<GnDivisions> CreateGnDivision(GnDivisions newGnDivision);  
        Task<IEnumerable<GnDivisions>> Search(int officeid, string description, string? code);
        Task<GnDivisions> GetAllForOffice(GnDivisions gnDivisionsData);
    }
}

