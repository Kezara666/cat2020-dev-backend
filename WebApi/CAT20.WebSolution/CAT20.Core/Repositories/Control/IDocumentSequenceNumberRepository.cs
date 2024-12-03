using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Control;

namespace CAT20.Core.Repositories.Control
{
    public interface IDocumentSequenceNumberRepository : IRepository<DocumentSequenceNumber>
    {
        Task<IEnumerable<DocumentSequenceNumber>> GetAllForOfficeAsync(int id);
        Task<DocumentSequenceNumber> GetNextSequenceNumberForYearOfficePrefixAsync(int year, int? officeid, string prefix);
    }
}
