using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.Vote
{
    public class SaveVoteBalanceTakeHold
    {
        public int? Id { get; set; }

        public decimal? TakeHoldRate { get; set; }

        public decimal TakeHoldAmount { get; set; }

        public string? RequestNote { get; set; }
    }
}
