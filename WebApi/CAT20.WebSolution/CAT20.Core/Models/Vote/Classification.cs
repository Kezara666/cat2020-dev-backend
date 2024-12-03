using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Vote
{
    public  class Classification
    {


        public Classification()
        {
            MainLedgerAccount = new HashSet<MainLedgerAccount>();
        }

        public int? Id { get; set; }
        public string Description { get; set; }
        public string? Code { get; set; }
        public int Status  { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<MainLedgerAccount> MainLedgerAccount { get; set; }

    }
}
