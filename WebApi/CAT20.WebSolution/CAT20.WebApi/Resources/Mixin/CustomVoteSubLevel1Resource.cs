﻿using CAT20.Core.Models.ShopRental;
using System;
using System.Collections.Generic;

namespace CAT20.WebApi.Resources.Mixin
{
    public partial class CustomVoteSubLevel1Resource
    {
        public CustomVoteSubLevel1Resource()
        {
            CustomVoteSubLevel2s = new HashSet<CustomVoteSubLevel2Resource>();
        }

        public int? Id { get; set; }
        public string Description { get; set; }
        public int CustomVoteId { get; set; }
        public int? Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual VoteAssignmentDetailsResource? CustomVote { get; set; }
        public virtual ICollection<CustomVoteSubLevel2Resource>? CustomVoteSubLevel2s { get; set; }
    }
}