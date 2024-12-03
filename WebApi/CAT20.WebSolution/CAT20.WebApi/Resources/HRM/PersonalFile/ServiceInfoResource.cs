using CAT20.Core.Models.Enums.HRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CAT20.Core.Models.HRM.PersonalFile
{
    public partial class ServiceInfoResource
    {
        public int? Id { get; set; }
        public int? EmployeeID { get; set; }
        public int SalaryStructureID { get; set; }
        public int ServiceTypeID { get; set; }
        public ServiceLevel ServiceLevelID { get; set; }
        public int JobTitleID { get; set; }
        public int Class { get; set; }
        public int Grade { get; set; }
        public string SalaryStep { get; set; }
        public int SalaryStepLevelID { get; set; }
        public decimal BasicSalary { get; set; }
        public DateTime IncrementDate { get; set; }
        public int AppointmentTypeID { get; set; }
        public string AppointmentLetterNumber { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int FundingSourceID { get; set; }
        public int ReimbursedPercentageID { get; set; }
        public int? AgraharaCategoryID { get; set; }
        public string? PensionNumber { get; set; }
        public string? WOPNumber { get; set; }
        public string? PSPFNumber { get; set; }


        // Navigation property

        //public virtual AppointmentTypeData? AppointmentTypeDatas { get; set; }
        //public virtual FundingSourceData? FundingSourceDatas { get; set; }
        //public virtual AgraharaCategoryData? AgraharaCategoryDatas { get; set; }
    }
}
