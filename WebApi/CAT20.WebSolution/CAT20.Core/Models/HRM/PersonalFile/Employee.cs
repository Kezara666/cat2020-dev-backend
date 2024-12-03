using CAT20.Core.Models.Enums.HRM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.HRM.PersonalFile
{
    public partial class Employee
    {
        public int? Id { get; set; }
        public int? EmployeeTypeID { get; set; }
        public int? CarderStausID { get; set; }
        public string NICNumber { get; set; }
        public string? PassportNumber { get; set; }
        public string? PersonalFileNumber { get; set; }
        public string? EmployeeNo { get; set; }
        public string? PayNo { get; set; }
        public Title Title { get; set; }
        public string? Initials { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public GenderID? GenderID { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public CivilStatus? CivilStatus { get; set; }
        public DateTime? MarriedDate { get; set; }
        public RailwayWarrant? RailwayWarrant { get; set; }
        public string? MobileNo { get; set; }
        public string? PersonalEmail { get; set; }
        public string? PhotographPath { get; set; }
        public int SabhaId { get; set; }
        public int OfficeId { get; set; }
        public int? ProgrammeId { get; set; }
        public int? ProjectId { get; set; }
        public int? SubProjectId { get; set; }


        // mandatory fields
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int StatusId { get; set; }

        // Navigation property
        public virtual EmployeeTypeData EmployeeTypeDatas { get; set; }
        public virtual CarderStatusData CarderStatusDatas { get; set; }
        public virtual ICollection<Address>? Addresses { get; set; }
        public virtual ICollection<SpouserInfo>? SpouserInfos { get; set; }
        public virtual ICollection<ChildInfo>? ChildrenInfos { get; set; }              
        public virtual ICollection<ServiceInfo>? ServiceInfos { get; set; }
        public virtual ICollection<SalaryInfo>? SalaryInfos { get; set; }
        public virtual ICollection<NetSalaryAgent>? NetSalaryAgents { get; set; }
        public virtual ICollection<OtherRemittanceAgent>? OtherRemittanceAgents { get; set; }
        public virtual ICollection<SupportingDocument>? SupportingDocuments { get; set; }



    }
}