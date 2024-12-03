using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final
{
    public class MakeVoteTransferApproveRejectResource
    {
        public int Id { get; set; }
        public string ActionNote { get; set; }
        public int State { get; set; }

        public int Entity { get; set; }

        /*
         entity: 1: FR66, 2: Supplementary, 3: Cut Provision, 
         */
    }
}
