using CAT20.Core.Models.Enums;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Repositories.OnlinePayment;
using CAT20.Data;
using CAT20.Data.Repositories;
using Microsoft.EntityFrameworkCore;


public class PaymentDetailsRepository : Repository<PaymentDetails>, IPaymentDetailsRepository
{
    public PaymentDetailsRepository(DbContext context) : base(context)
    {
    }

    public async Task<PaymentDetails> GetById(int? id)
    {
        return await onlinePaymentDbContext.PaymentDetails.Include(d => d.Dispute)
            .Where(p => p.PaymentDetailId == id)
            .FirstOrDefaultAsync();
    }

    public async Task<(int totalCount, IEnumerable<PaymentDetails> list)> GetAllPaymentDetails(int officeId,
        int? pageNumber, int? pageSize,
        string? filterKeyword)
    {
        var pageN = pageNumber ?? 0;
        var pageS = pageSize ?? 0;
        if (filterKeyword != "undefined")
        {
            filterKeyword = "%" + filterKeyword + "%";
        }
        else if (filterKeyword == "undefined")
        {
            filterKeyword = null;
        }

        var keyword = filterKeyword ?? "";
        var list = await onlinePaymentDbContext.PaymentDetails.Where(p =>
                ((((p.Description == "Assessment") && p.Status == 1 && p.Error == 0 && p.Check == 0 &&
                   p.OfficeId == officeId)
                  ) && (string.IsNullOrEmpty(keyword) ||
                                                   (EF.Functions.Like(p.PartnerNIC, keyword)) ||
                                                   (EF.Functions.Like(p.PartnerMobileNo, keyword)) ||
                                                   (EF.Functions.Like(p.TransactionId, keyword)) ||
                                                   (EF.Functions.Like(p.AccountNo, keyword)))))
            .OrderByDescending(p => p.PaymentDetailId)
            .Skip((pageN - 1) * pageS)
            .Take(pageS)
            .ToListAsync();

        var totalCount = 0;
        totalCount = await onlinePaymentDbContext.PaymentDetails.Where(p =>
                ((((p.Description == "Assessment") && p.Status == 1 && p.Error == 0 && p.Check == 0 &&
                   p.OfficeId == officeId)
                  ) && (string.IsNullOrEmpty(keyword) ||
                                                   (EF.Functions.Like(p.PartnerNIC, keyword)) ||
                                                   (EF.Functions.Like(p.PartnerMobileNo, keyword)) ||
                                                   (EF.Functions.Like(p.TransactionId, keyword)) ||
                                                   (EF.Functions.Like(p.AccountNo, keyword)))))
            .CountAsync();


        return (totalCount, list);
    }


    public async Task<(int totalCount, IEnumerable<PaymentDetails> list)> getDisputes(int officeId,
        int? pageNumber, int? pageSize,
        string? filterKeyword)
    {
        var pageN = pageNumber ?? 0;
        var pageS = pageSize ?? 0;
        if (filterKeyword != "undefined")
        {
            filterKeyword = "%" + filterKeyword + "%";
        }
        else if (filterKeyword == "undefined")
        {
            filterKeyword = null;
        }
        
        List<string> allowedDescriptions = new List<string>
            { "Assessment","Water Bill", "Shop Rental", "Trade Tax", "Gully", "Hall Book" };

        var keyword = filterKeyword ?? "";
        var list = await onlinePaymentDbContext.PaymentDetails.Include(d => d.Dispute)
            .Where(p =>
                ((allowedDescriptions.Contains(p.Description) && p.Status == 0 && p.Error == 1 && p.Check == 0 &&
                   p.OfficeId == officeId) && (string.IsNullOrEmpty(keyword) ||
                                                (EF.Functions.Like(p.PartnerNIC, keyword)) ||
                                                (EF.Functions.Like(p.PartnerMobileNo, keyword)) ||
                                                (EF.Functions.Like(p.TransactionId, keyword)) ||
                                                (EF.Functions.Like(p.Dispute.Id, keyword.Substring(0,3))))))
            .OrderByDescending(p => p.PaymentDetailId)
            .Skip((pageN - 1) * pageS)
            .Take(pageS)
            .ToListAsync();

        var totalCount = 0;
        totalCount = await onlinePaymentDbContext.PaymentDetails.Include(d => d.Dispute).Where(p =>
                ((allowedDescriptions.Contains(p.Description) && p.Status == 0 && p.Error == 1 && p.Check == 0 &&
                   p.OfficeId == officeId) && (string.IsNullOrEmpty(keyword) ||
                                                (EF.Functions.Like(p.PartnerNIC, keyword)) ||
                                                (EF.Functions.Like(p.PartnerMobileNo, keyword)) ||
                                                (EF.Functions.Like(p.TransactionId, keyword)) ||
                                                (EF.Functions.Like(p.Dispute.Id, keyword.Substring(0,3))))))
            .CountAsync();


        return (totalCount, list);
    }


    public async Task<(int totalCount, IEnumerable<PaymentDetails> list)> GetOtherPaymentDetails(int officeId,
        int? pageNumber, int? pageSize,
        string? filterKeyword)
    {
        var pageN = pageNumber ?? 0;
        var pageS = pageSize ?? 0;
        if (filterKeyword != "undefined")
        {
            filterKeyword = "%" + filterKeyword + "%";
        }
        else if (filterKeyword == "undefined")
        {
            filterKeyword = null;
        }

        var keyword = filterKeyword ?? "";
        List<string> allowedDescriptions = new List<string>
            { "Water Bill", "Shop Rental", "Trade Tax", "Gully", "Hall Book", "Street Line Certificate", "Advertisement", "BOP", "Building Applications", "Three Wheel Park", "Environment License", "Other Payment" };
        int activeStatus = 1;
        int errorStatus = 1;
        int noErrorStatus = 0;

        var list = await onlinePaymentDbContext.PaymentDetails
            .Include(p => p.OtherDescription)
            .Where(p =>
                (((allowedDescriptions.Contains(p.Description) && p.Status == activeStatus &&
                   p.Error == noErrorStatus &&
                   p.Check == 0 && p.OfficeId == officeId) ||
                  (allowedDescriptions.Contains(p.Description) && p.Status == 0 && p.Error == errorStatus &&
                   p.Check == 0 && p.OfficeId == officeId)) && (string.IsNullOrEmpty(keyword) ||
                                                                (EF.Functions.Like(p.PartnerNIC, keyword)) ||
                                                                (EF.Functions.Like(p.PartnerMobileNo, keyword)) ||
                                                                (EF.Functions.Like(p.TransactionId, keyword)) ||
                                                                (EF.Functions.Like(p.AccountNo, keyword)))))
            .OrderByDescending(p => p.PaymentDetailId)
            .Skip((pageN - 1) * pageS)
            .Take(pageS)
            .ToListAsync();

        var totalCount = 0;
        totalCount = await onlinePaymentDbContext.PaymentDetails
            .Include(p => p.OtherDescription)
            .Where(p =>
                (((allowedDescriptions.Contains(p.Description) && p.Status == activeStatus &&
                   p.Error == noErrorStatus &&
                   p.Check == 0 && p.OfficeId == officeId) ||
                  (allowedDescriptions.Contains(p.Description) && p.Status == 0 && p.Error == errorStatus &&
                   p.Check == 0 && p.OfficeId == officeId)) && (string.IsNullOrEmpty(keyword) ||
                                                                (EF.Functions.Like(p.PartnerNIC, keyword)) ||
                                                                (EF.Functions.Like(p.PartnerMobileNo, keyword)) ||
                                                                (EF.Functions.Like(p.TransactionId, keyword)) ||
                                                                (EF.Functions.Like(p.AccountNo, keyword)))))
            .CountAsync();

        return (totalCount, list);
    }

    public async Task<IEnumerable<PaymentDetails>> getPartnerPaymentHistory(int partnerId, int? pageNumber,
        int? pageSize)
    {
        var pageN = pageNumber ?? 0;
        var pageS = pageSize ?? 0;

        return await onlinePaymentDbContext.PaymentDetails.Where(p => p.PartnerId == partnerId && (p.Status != 0 || p.Error != 0))
            .OrderByDescending(p => p.PaymentDetailId)
            .Skip((pageN - 1) * pageS)
            .Take(pageS)
            .ToListAsync();
    }

    public async Task<IEnumerable<PaymentDetails>> getPartnerDisputes(int partnerId, int? pageNumber,
        int? pageSize)
    {
        var pageN = pageNumber ?? 0;
        var pageS = pageSize ?? 0;

        var partnerDispute = await onlinePaymentDbContext.PaymentDetails.Include(d => d.Dispute)
            .Where(p => (p.PartnerId == partnerId && p.Error == 1 && p.Status == 0 && p.Dispute != null) )
            .OrderByDescending(p => p.PaymentDetailId)
            .Skip((pageN - 1) * pageS)
            .Take(pageS)
            .ToListAsync();

        return partnerDispute;
    }

    public async Task<(int totalCount, IEnumerable<PaymentDetails> list)> getPaymentHistoryForOffice(int officeId,
        int? pageNumber, int? pageSize,
        string? filterKeyword)
    {
        var pageN = pageNumber ?? 0;
        var pageS = pageSize ?? 0;
        if (filterKeyword != "undefined")
        {
            filterKeyword = "%" + filterKeyword + "%";
        }
        else if (filterKeyword == "undefined")
        {
            filterKeyword = null;
        }

        var keyword = filterKeyword ?? "";

        var list = await onlinePaymentDbContext.PaymentDetails.Where(p => ((p.OfficeId == officeId && p.Check == 1 && p.Status == 1)
                                                                           && (string.IsNullOrEmpty(keyword) ||
                                                                               (EF.Functions.Like(p.PartnerNIC,
                                                                                   keyword)) ||
                                                                               (EF.Functions.Like(p.PartnerMobileNo,
                                                                                   keyword)) ||
                                                                               (EF.Functions.Like(p.TransactionId,
                                                                                   keyword)) ||
                                                                               (EF.Functions.Like(p.AccountNo,
                                                                                   keyword)))))
            .OrderByDescending(p => p.PaymentDetailId)
            .Skip((pageN - 1) * pageS)
            .Take(pageS)
            .ToListAsync();

        var totalCount = 0;
        totalCount = await onlinePaymentDbContext.PaymentDetails.Where(p => ((p.OfficeId == officeId && p.Check == 1 && p.Status == 1)
                                                                             && (string.IsNullOrEmpty(keyword) ||
                                                                                 (EF.Functions.Like(p.PartnerNIC,
                                                                                     keyword)) ||
                                                                                 (EF.Functions.Like(p.PartnerMobileNo,
                                                                                     keyword)) ||
                                                                                 (EF.Functions.Like(p.TransactionId,
                                                                                     keyword)) ||
                                                                                 (EF.Functions.Like(p.AccountNo,
                                                                                     keyword)))))
            .CountAsync();


        return (totalCount, list);
    }


    public async Task Save(PaymentDetails paymentDetails)
    {
        onlinePaymentDbContext.Add(paymentDetails);
        await onlinePaymentDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<PaymentDetails>> paymentDetailSheduler()
    {
        DateTime oneHourAgo = DateTime.Now.AddHours(-1);

        // return await onlinePaymentDbContext.PaymentDetails.Where(p => p.Status == 0 && p.Error ==0 && p.CreatedAt >= oneHourAgo).ToListAsync();
        return await onlinePaymentDbContext.PaymentDetails.Where(p => p.Status == 0 && p.Error ==0 ).ToListAsync();
        
    }


    private OnlinePaymentDbContext onlinePaymentDbContext
    {
        get { return Context as OnlinePaymentDbContext; }
    }
}