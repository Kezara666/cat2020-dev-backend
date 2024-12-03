﻿using CAT20.Core.DTO.OtherModule;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.DTO.Final
{

    public class VoteJournalAdjustmentResource
    {
        public int? Id { get; set; }
        public string JournalNo { get; set; }
        public int SabahId { get; set; }
        public int OfficeId { get; set; }

        public VoteJournalAdjustmentActions ActionState { get; set; }

        public decimal FromAmount { get; set; }
        public decimal ToAmount { get; set; }


        public DateTime? RequestDate { get; set; }

        public int? RequestBy { get; set; }

        public string? RequestNote { get; set; }


        public DateTime? ActionDate { get; set; }

        public int? ActionBy { get; set; }

        public string? ActionNote { get; set; }

        public DateTime? SystemRequestDate { get; set; }

        public DateTime? SystemActionDate { get; set; }


        public ICollection<VoteJournalItemFromResource>? VoteJournalItemsFrom { get; set; }
        public ICollection<VoteJournalItemToResource>? VoteJournalItemsTo { get; set; }


        // mandatory fields
        //public int? RowStatus { get; set; }



/*linking model*/

        public FinalUserActionByResources? UserRequestBy { get; set; }
        public FinalUserActionByResources? UserActionBy { get; set; }


    }
}