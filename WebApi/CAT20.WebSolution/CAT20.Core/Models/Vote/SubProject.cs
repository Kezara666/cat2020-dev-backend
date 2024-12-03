using System;
using System.Collections.Generic;
using CAT20.Core.Models.Vote;
using System.ComponentModel.DataAnnotations;

namespace CAT20.Core.Models.Vote
{
    public partial class SubProject
    {
        [Key]
        public int ID { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public string Code { get; set; }
        public int? Status { get; set; }
        public int? ProjectID { get; set; }
        public int? SabhaID { get; set; }
        public int? ProgrammeID { get; set; }
        public virtual Project project { get; set; }
    }
}