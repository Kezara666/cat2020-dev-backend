namespace CAT20.Core.DTO.Final.Save
{
    public class SaveVoucherChequeRequest
    {
        public List<VoucherResource> Vouchers { get; set; }
        public List<SaveVoucherChequeResources> VoucherCheques { get; set; }
    }
}
