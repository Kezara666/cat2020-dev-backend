using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;

namespace CAT20.Core.DTO.Vote
{
    public class ClassificatioResource

    {

        public int? Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<MainLedgerAccountResource> MainLedgerAccount { get; set; }
    }
}
