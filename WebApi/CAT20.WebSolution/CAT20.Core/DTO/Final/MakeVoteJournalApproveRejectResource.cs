using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final
{
    public class MakeVoteJournalApproveRejectResource
    {
        public int JournalId { get; set; }
        public string ActionNote { get; set; }
        public int State { get; set; }
    }
}
