using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Control
{
    public class Agents
    {
        public int? Id;
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
        public DateTime SystemActionAt;


        public virtual BankBranch? BankBranch { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        

    }
}
