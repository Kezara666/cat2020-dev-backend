using CAT20.Core.Models.HRM.LoanManagement;

namespace CAT20.WebApi.Resources.HRM.LoanManagement
{
    public class AdvanceBAttachmentResource
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

    }
}
