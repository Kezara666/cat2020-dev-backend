using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.HRM.PersonalFile
{
    public partial class JobTitleData
    {
        public int Id { get; set; }
        public int ServiceTypeDataId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Status { get; set; }

        // Navigation property
        public virtual ICollection<ServiceInfo>? ServiceInfos { get; set; }
        public virtual ServiceTypeData ServiceTypeData { get; set; }
    }
}
