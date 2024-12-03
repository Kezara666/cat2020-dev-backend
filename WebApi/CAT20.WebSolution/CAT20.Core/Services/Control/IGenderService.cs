using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface IGenderService
    {
        Task<IEnumerable<Gender>> GetAllGenders();
        Task<Gender> GetGenderById(int id);
        Task<Gender> CreateGender(Gender newGender);
        Task UpdateGender(Gender genderToBeUpdated, Gender gender);
        Task DeleteGender(Gender gender);
    }
}

