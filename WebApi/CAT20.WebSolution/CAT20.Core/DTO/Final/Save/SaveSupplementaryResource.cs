using CAT20.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveSupplementaryResource
    {
        public int? Id { get; set; }
        public string? SPLNo { get; set; }
        public int? SabahId { get; set; }
        public int? OfficeId { get; set; }
        public int VoteDetailId { get; set; }
        public int VoteBalanceId { get; set; }

        //public VoteTransferActions ActionState { get; set; }

        public decimal Amount { get; set; }


        //public DateTime? RequestDate { get; set; }

        //public int? RequestBy { get; set; }

        public string? RequestNote { get; set; }


        //public DateTime? ActionDate { get; set; }

        //public int? ActionBy { get; set; }

        //public string? ActionNote { get; set; }



    }
}
