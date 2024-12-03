using CAT20.Core.Models.Control;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.FinalAccount

{
    public partial class CommitmentLine
    {
        [Key]
        public int? Id { get; set; }
        public int CommitmentId { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? Amount { get; set; }
        public string? Comment { get; set; }
        [JsonIgnore]
        public virtual Commitment? Commitment { get; set; }

        public int RowStatus { get; set; }
        
        public  virtual List<CommitmentLineVotes>? CommitmentLineVotes { get; set; }
    }
}