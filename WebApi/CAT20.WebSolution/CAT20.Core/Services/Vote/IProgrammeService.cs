using CAT20.Core.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Vote
{
    public interface IProgrammeService
    {
        Task<IEnumerable<Programme>> GetAllProgrammes();
        Task<Programme> GetProgrammeById(int id);
        Task<Programme> CreateProgramme(Programme newProgramme);
        Task UpdateProgramme(Programme programmeToBeUpdated, Programme programme);
        Task DeleteProgramme(Programme programme);

        Task<IEnumerable<Programme>> GetAllProgrammesForSabhaId(int id);
    }
}

