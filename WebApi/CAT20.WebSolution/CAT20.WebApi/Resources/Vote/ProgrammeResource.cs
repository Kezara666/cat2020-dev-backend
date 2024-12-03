using CAT20.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAT20.WebApi.Resources.Vote
{
    public class ProgrammeResource
    {
        public int ID { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public string Code { get; set; }
        public int? Status { get; set; }
        public int? SabhaID { get; set; }
    }
}
