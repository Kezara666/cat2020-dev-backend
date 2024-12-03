using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Services.Control
{
    public interface IDocumentSequenceNumberService
    {
        Task<IEnumerable<DocumentSequenceNumber>> GetAllForOffice(int id);
        Task<DocumentSequenceNumber> GetNextSequenceNumberForYearOfficePrefix(int year, int officeid, string prefix);
        Task<bool> CheckIsExistingAndIfNotCreateSequenceNoForYear(int year, int officeid, string prefix);
        Task Update(DocumentSequenceNumber documentSequenceNumbertobeupdated, DocumentSequenceNumber documentSequenceNumber);
        Task<DocumentSequenceNumber> Create(DocumentSequenceNumber newdocumentSequenceNumber);
    }
}

