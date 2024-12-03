using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums.HRM;
using CAT20.Core.Models.HRM.PersonalFile;
using System;
using System.Collections.Generic;

namespace CAT20.Core.DTO.OtherModule
{
    public partial class FinalEmployeeResource
    {
        public int? Id { get; set; }
        //public int EmployeeTypeID { get; set; }
        //public int CarderStausID { get; set; }
        public string NICNumber { get; set; }
        //public string? PassportNumber { get; set; }
        //public string? PersonalFileNumber { get; set; }
        public string EmployeeNo { get; set; }
        public string? PayNo { get; set; }
        public Title Title { get; set; }
        public string Initials { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        //public GenderID GenderID { get; set; }
        //public DateTime DateOfBirth { get; set; }
        //public CivilStatus CivilStatus { get; set; }
        //public DateTime? MarriedDate { get; set; }
        //public RailwayWarrant? RailwayWarrant { get; set; }
        public string? MobileNo { get; set; }
        //public string? PersonalEmail { get; set; }
        //public string? PhotographPath { get; set; }
        //public int SabhaId { get; set; }
        //public int OfficeId { get; set; }
        //public int ProgrammeId { get; set; }
        //public int? ProjectId { get; set; }
        //public int? SubProjectId { get; set; }


        // Navigation property
        ////public virtual EmployeeTypeData? EmployeeTypeDatas { get; set; }
        ////public virtual CarderStatusData? CarderStatusDatas { get; set; }
        //public virtual ICollection<AddressResource>? Addresses { get; set; }
        //public virtual ICollection<SpouserInfoResource>? SpouserInfos { get; set; }
        //public virtual ICollection<ChildInfoResource>? ChildrenInfos { get; set; }
        //public virtual ICollection<ServiceInfoResource>? ServiceInfos { get; set; }
        //public virtual ICollection<SalaryInfoResource>? SalaryInfos { get; set; }
        //public virtual ICollection<NetSalaryAgentResource>? NetSalaryAgents { get; set; }
        //public virtual ICollection<OtherRemittanceAgentResource>? OtherRemittanceAgents { get; set; }
        //public virtual ICollection<SupportingDocumentResource>? SupportingDocuments { get; set; }

        public string? EmployeeName { get; set; }
    }
}
