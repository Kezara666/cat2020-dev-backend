using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.WebApi.Resources.FInalAccount.Save;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveCommitmentLineResource
    {
        public int? Id { get; set; }
        public int? CommitmentId { get; set; }
        //public int? VoteId { get; set; }
        public decimal Amount { get; set; }
        public string? Comment { get; set; }
        public FinalAccountActionStates Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }

        public virtual List<SaveCommitmentLineVotesResource> CommitmentLineVotes { get; set; }
    }
}
