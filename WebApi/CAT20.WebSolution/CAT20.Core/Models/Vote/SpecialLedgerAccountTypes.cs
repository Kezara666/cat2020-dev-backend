using CAT20.Core.DTO.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Vote
{
    public class  SpecialLedgerAccountTypes
    {
        public int Id { get; set; }
        public string? NameInEnglish { get; set; }
        public string? NameInSinhala { get; set; }
        public string? NameInTamil { get; set; }

        public virtual SpecialLedgerAccountTypesResource? Type { get; set; }

    /**/

    // mandatory fields
    public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }



    }
}
