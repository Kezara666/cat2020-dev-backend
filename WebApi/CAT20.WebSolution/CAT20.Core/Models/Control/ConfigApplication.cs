using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.Control
{
    public partial class ConfigApplication
    {
        public int Id { get; set; }
        public int SessionConfigId { get; set; }
        public int ApplicationId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Application Application { get; set; }
        //public virtual SessionConfig SessionConfig { get; set; }
    }
}