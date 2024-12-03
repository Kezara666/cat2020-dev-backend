using CAT20.Core.Models;
using CAT20.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAT20.WebApi.Resources.User.Save
{
    public class SavePreviledgeResource
    {
        //public Previledge()
        //{
        //    userHasPreviledge = new HashSet<UserHasPreviledge>();
        //}

        public int ID { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public int? Status { get; set; }
        public int? OfficeID { get; set; }
        public int? SabhaID { get; set; }
        public string Description { get; set; }

        //public virtual ICollection<UserHasPreviledge> userHasPreviledge { get; set; }
    }
}
