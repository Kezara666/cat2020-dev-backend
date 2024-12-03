using CAT20.Core.DTO.OtherModule;
using CAT20.Core.Models.Enums.HRM;
using CAT20.Core.Models.HRM.LoanManagement;
using CAT20.Core.Models.HRM.PersonalFile;

namespace CAT20.WebApi.Resources.HRM.LoanManagement
{
    public partial class AdvanceBResource
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int AdvanceBTypeId { get; set; }
        public int LedgerAccId { get; set; }
        public bool IsNew { get; set; }
        public int? VoucherId { get; set; }
        public string? VoucherNo { get; set; }
        public string? AdvanceBNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestPercentage { get; set; }
        public decimal InstallmentAmount { get; set; }
        public decimal InterestAmount { get; set; }
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

        public DateOnly? TransferInOrOutDate { get; set; }
        public decimal TransferInOrOutBalanceAmount { get; set; }
        public DateOnly? DeceasedDate { get; set; }
        public decimal DeceasedBalance { get; set; }


        // Mandatory fields
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int RowStatus { get; set; }






        // Navigation properties
        public virtual FinalEmployeeResource? Employee { get; set; }
        public virtual AdvanceBTypeDataResource? AdvanceBTypeData { get; set; }
        public virtual FinalEmployeeResource? Guarantor1 { get; set; }
        public virtual FinalEmployeeResource? Guarantor2 { get; set; }
        //public ICollection<LoanOpeningBalance> LoanOpeningBalances { get; set; }
        public ICollection<AdvanceBAttachmentResource>? AdvanceBAttachments { get; set; }
        public ICollection<AdvanceBSettlementResource>? AdvanceBSettlements { get; set; }
    }
}
