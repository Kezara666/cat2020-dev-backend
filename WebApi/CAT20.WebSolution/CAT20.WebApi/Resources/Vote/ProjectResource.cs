﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using CAT20.Core.Models.Vote;

namespace CAT20.WebApi.Resources.Vote
{
    public partial class ProjectResource
    {
        //public ProjectResource()
        //{
        //    subProject = new HashSet<SubProjectResource>();
        //}

        public int ID { get; set; }
        public string NameSinhala { get; set; }
        public string NameEnglish { get; set; }
        public string NameTamil { get; set; }
        public string Code { get; set; }
        public int? Status { get; set; }
        public int? ProgrammeID { get; set; }
        public int? SabhaID { get; set; }

        //public virtual Programme programme { get; set; }
        //public virtual ICollection<SubProjectResource> subProject { get; set; }
    }
}