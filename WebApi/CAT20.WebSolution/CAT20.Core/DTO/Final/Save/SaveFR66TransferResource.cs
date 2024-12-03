using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveFR66TransferResource
    {
        public int? Id { get; set; }
        public string? FR66No { get; set; }
        public int SabahId { get; set; }
        public int OfficeId { get; set; }

        public VoteTransferActions ActionState { get; set; }

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


        public ICollection<SaveFR66FromItemResource>? FR66FromItems { get; set; }
        public ICollection<SaveFR66ToItemResource>? FR66ToItems { get; set; }
    }
}
