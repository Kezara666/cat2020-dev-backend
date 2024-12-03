using CAT20.Core.DTO.Final;
using CAT20.Core.Models.FinalAccount;

namespace CAT20.WebApi.Resources.Final;

public class CommitmentLineResource
{
    public int? Id { get; set; }
    public int CommitmentId { get; set; }
    public decimal Amount { get; set; }
    public string? Comment { get; set; }
    public virtual CommitmentResource? Commitment { get; set; }

    public virtual List<CommitmentLineVotesResource>? CommitmentLineVotes { get; set; }
}