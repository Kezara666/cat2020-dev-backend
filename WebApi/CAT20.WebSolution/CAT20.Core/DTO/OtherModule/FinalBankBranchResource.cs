using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.OtherModule
{
    public class FinalBankBranchResource
    {
        public int ID { get; set; }
        public int BankCode { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string BranchAddress { get; set; }
        public string TelNo1 { get; set; }
        public string TelNo2 { get; set; }
        public string TelNo3 { get; set; }
        public string TelNo4 { get; set; }
        public string FaxNo { get; set; }
        public string District { get; set; }
        public int? Status { get; set; }

        public FinalBankDetailResource? Bank { get; set; }
    }
}
