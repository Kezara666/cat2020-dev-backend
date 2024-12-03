using System;
using System.Collections.Generic;

namespace CAT20.Core.Models.Control
{
    public partial class Application
    {
        public Application()
        {
            ConfigApplication = new HashSet<ConfigApplication>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<ConfigApplication> ConfigApplication { get; set; }
    }
}