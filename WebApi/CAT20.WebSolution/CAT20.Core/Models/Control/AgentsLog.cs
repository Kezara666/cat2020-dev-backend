using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Control
{
    public class AgentsLog
    {
        public int Id;
        public int AgentId;
        public int? BracnId;
        public string? Reference;
        public string? BankAccountNumber;
        public int SabhaId;
        public int Status;
        public DateTime UpdatedAt;
        public int CreatedBy;
        public int UpdatedBy;
        public DateTime SystemActionAt;
        public byte[]? RowVersion;
    }
}
