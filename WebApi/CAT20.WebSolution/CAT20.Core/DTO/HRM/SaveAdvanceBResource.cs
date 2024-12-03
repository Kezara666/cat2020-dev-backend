﻿using CAT20.Core.Models.Enums.HRM;
using CAT20.Core.Models.HRM.LoanManagement;
using CAT20.Core.Models.HRM.PersonalFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.DTO.HRM
{
    public class SaveAdvanceBResource
    {
        //public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int AdvanceBTypeId { get; set; }
        public int LedgerAccId { get; set; }
        public bool IsNew { get; set; }
        public int? VoucherId { get; set; }
        public string? VoucherNo { get; set; }
        public string? AdvanceBNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal? InterestPercentage { get; set; }
        public decimal? InstallmentAmount { get; set; }
        public decimal? OddInstallmentAmount { get; set; }
        public decimal? InterestAmount { get; set; }
        public int NumberOfInstallments { get; set; }
        public int RemainingInstallments { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? BankAccId { get; set; }
        public string? Description { get; set; }
        public int? Guarantor1Id { get; set; }
        public int? Guarantor2Id { get; set; }
        public int SabhaId { get; set; }
        public int OfficeId { get; set; }

        public AdvanceBStatus AdvanceBStatus { get; set; }

        public DateTime? TransferInOrOutDate { get; set; }
        public decimal? TransferInOrOutBalanceAmount { get; set; }
        public DateTime? DeceasedDate { get; set; }
        public decimal? DeceasedBalance { get; set; }


        //// Mandatory fields
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public int? CreatedBy { get; set; }
        //public int? UpdatedBy { get; set; }
        //public int RowStatus { get; set; }






        //// Navigation properties
        //public virtual Employee? Employee { get; set; }
        //public virtual AdvanceBTypeData? AdvanceBTypeData { get; set; }
        //public virtual Employee? Guarantor1 { get; set; }
        //public virtual Employee? Guarantor2 { get; set; }
        ////public ICollection<LoanOpeningBalance> LoanOpeningBalances { get; set; }
        //public ICollection<AdvanceBAttachment>? AdvanceBAttachments { get; set; }
        //public ICollection<AdvanceBSettlement>? AdvanceBSettlements { get; set; }


        //public DateTime? SystemActionAt { get; set; }
    }
}
