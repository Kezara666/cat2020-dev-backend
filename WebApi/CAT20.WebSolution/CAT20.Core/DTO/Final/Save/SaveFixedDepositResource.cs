using CAT20.Core.DTO.Vote.Save;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveFixedDepositResource
    {
        public int? Id { get; set; }
        public int DepositTypeVote { get; set; }
        public int BankBranchId { get; set; }
        public string Reference { get; set; }

        public decimal InterestRate { get; set; }

        public decimal FDAmount { get; set; }

        public DateTime DepositDate { get; set; }

        public int? Duration { get; set; }
        public DateTime RenewableDate { get; set; }

        //public int? OfficeId { get; set; }
        //public int? SabhaId { get; set; }
        //public int Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public int? CreatedBy { get; set; }
        //public int? UpdatedBy { get; set; }

        //public DateTime? SystemActionAt { get; set; }

        public int? CustomVoteId { get; set; }
    }
}
