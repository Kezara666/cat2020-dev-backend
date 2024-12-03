using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.OtherModule
{
    public class FinalBankDetailResource
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int BankCode { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<FinalBankBranchResource>? bankBranch { get; set; }
    }
}
