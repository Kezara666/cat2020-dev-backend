using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.WebApi.Resources.FInalAccount;
using System;

namespace CAT20.Core.DTO.Final.Save
{
    public class SaveCommitmentResource
    {
        public int? Id { get; set; }

        public VoucherPayeeCategory PayeeCategory { get; set; }

        public int PayeeId { get; set; }
        public int BankId { get; set; }

        public string PayeeName { get; set; }

        public decimal TotalAmount { get; set; }

        public string Description { get; set; }

        public string? CommitmentSequenceNumber { get; set; }



        public virtual List<SaveCommitmentLineResource> CommitmentLine { get; set; }



    }
}