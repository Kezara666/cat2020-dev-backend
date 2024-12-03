using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveVoteJournalAdjustmentResource
    {
        public int? Id { get; set; }
        public string? JournalNo { get; set; }
        public int? SabahId { get; set; }
        public int? OfficeId { get; set; }

        public decimal FromAmount { get; set; }
        public decimal ToAmount { get; set; }

        public DateTime? RequestDate { get; set; }

        public int? RequestBy { get; set; }

        public string? RequestNote { get; set; }


        public DateTime? ActionDate { get; set; }

        public int? ActionBy { get; set; }

        public string? ActionNote { get; set; }

        public DateTime? SystemRequestDate { get; set; }

        public DateTime? SystemActionDate { get; set; }


        public ICollection<SaveVoteJournalItemFromResource>? VoteJournalItemsFrom { get; set; }
        public ICollection<SaveVoteJournalItemToResource>? VoteJournalItemsTo { get; set; }
    }
}
