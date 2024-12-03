using CAT20.Core.Models.Control;

namespace CAT20.WebApi.Resources.Control
{
    public class AgentsResource
    {
        public int Id;
        public string? AgentCode;
        public int? BranchId;
        public string? Reference;
        public string? BankAccountNumber;
        public int SabhaId;
        public int Status;
        public DateTime CreatedAt;
        public DateTime UpdatedAt;
        public int CreatedBy;
        public int UpdatedBy;


        public virtual BankBranchResource? BankBranch { get; set; }
    }
}
