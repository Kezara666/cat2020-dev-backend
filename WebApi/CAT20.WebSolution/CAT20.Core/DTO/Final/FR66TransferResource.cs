using CAT20.Core.DTO.OtherModule;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Final
{
    public class FR66TransferResource
    {
        public int? Id { get; set; }
        public string FR66No { get; set; }
        public int SabahId { get; set; }
        public int OfficeId { get; set; }

        public VoteTransferActions ActionState { get; set; }

        [Precision(18, 2)]
        public decimal FromAmount { get; set; }
        [Precision(18, 2)]
        public decimal ToAmount { get; set; }


        [Required]
        public DateTime? RequestDate { get; set; }

        [Required]
        public int? RequestBy { get; set; }

        public string? RequestNote { get; set; }


        public DateTime? ActionDate { get; set; }

        public int? ActionBy { get; set; }

        public string? ActionNote { get; set; }

        [Required]
        public DateTime? SystemRequestDate { get; set; }

        public DateTime? SystemActionDate { get; set; }


        public ICollection<FR66FromItemResource>? FR66FromItems { get; set; }
        public ICollection<FR66ToItemResource>? FR66ToItems { get; set; }

        /*linking model*/

        public FinalUserActionByResources? UserRequestBy { get; set; }
        public FinalUserActionByResources? UserActionBy { get; set; }

    }
}
