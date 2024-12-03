using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.Vote
{
    public partial class VoteDetailBasicModel
    {
        [Key]
        public int? ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}