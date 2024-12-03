using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Enums;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;
using CAT20.WebApi.Resources.Final;
using CAT20.Core.Models.Interfaces;
using Newtonsoft.Json.Linq;
using CAT20.Core.DTO.Final;

namespace CAT20.Data.Repositories.FinalAccount;

public class VoucherRepository : Repository<Voucher>, IVoucherRepository
{
    public VoucherRepository(DbContext context) : base(context)
    {
    }

    public async Task<(int totalCount,IEnumerable<Voucher> list)> getVoucherForApproval(int sabhaId, List<int?> excludedIds, int? category, int stage, int pageNo,
        int pageSize, string? filterKeyword)
    {
        if (filterKeyword != "undefined")
        {
            filterKeyword = "%" + filterKeyword + "%";
        }
        else if (filterKeyword == "undefined")
        {
            filterKeyword = null;
        }

        var keyword = filterKeyword ?? "";
        FinalAccountActionStates status = (FinalAccountActionStates)Enum.Parse(typeof(FinalAccountActionStates), stage.ToString());
        VoucherCategory? voucherCategory = category.HasValue?  (VoucherCategory)Enum.Parse(typeof(VoucherCategory), category.ToString()!) :null;



        var result = voteAccDbContext.Voucher
            .Include(v => v.VoucherLine)
            .Include(v=>v.SubVoucherItems)
            .Where(a => !excludedIds.Contains(a.Id))
            .Where(v => (v.ActionState == status && v.SabhaId == sabhaId))
            .Where(v => voucherCategory.HasValue ? v.VoucherCategory == voucherCategory : true)
            .Where(v => EF.Functions.Like(v.VoucherSequenceNumber!, "%" + filterKeyword + "%") || EF.Functions.Like(v.VoucherAmount!, "%" + filterKeyword + "%") || EF.Functions.Like(v.TotalChequeAmount, "%" + filterKeyword + "%"))
            .OrderByDescending(v => v.Id);
            


        int totalCount = await result.CountAsync();


        //var pageSize = 10;
        int skipAmount = (pageNo - 1) * pageSize;

        var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


        return (totalCount, list);



    }


    public async Task<(int totalCount, IEnumerable<Voucher> list)> searchVoucherByKeywordForSurcharge(int sabhaId,  int pageNo,  int pageSize, string? filterKeyword)
    {
        if (filterKeyword != "undefined")
        {
            filterKeyword = "%" + filterKeyword + "%";
        }
        else if (filterKeyword == "undefined")
        {
            filterKeyword = null;
        }

        var keyword = filterKeyword ?? "";



        var result = voteAccDbContext.Voucher
            .Include(v => v.VoucherLine)
            .Include(v => v.SubVoucherItems)
            .Where(v => (v.ActionState == FinalAccountActionStates.HasCheque && v.SabhaId == sabhaId))
            .Where(v =>  v.VoucherCategory == VoucherCategory.VoteLedger )
            .Where(v => EF.Functions.Like(v.VoucherSequenceNumber!, "%" + filterKeyword + "%") || EF.Functions.Like(v.VoucherAmount!, "%" + filterKeyword + "%") || EF.Functions.Like(v.TotalChequeAmount, "%" + filterKeyword + "%"))
            .OrderByDescending(v => v.Id);



        int totalCount = await result.CountAsync();


        //var pageSize = 10;
        int skipAmount = (pageNo - 1) * pageSize;

        var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


        return (totalCount, list);



    }

    public async Task<(int totalCount,IEnumerable<Voucher> list)> getVoucherProgressRejected(int sabhaId, List<int?> stages, int pageNo,
        int pageSize, string? filterKeyword)
    {
        if (filterKeyword != "undefined")
        {
            filterKeyword = "%" + filterKeyword + "%";
        }
        else if (filterKeyword == "undefined")
        {
            filterKeyword = null;
        }

        var keyword = filterKeyword ?? "";


        

        var result = voteAccDbContext.Voucher
            .Include(v => v.VoucherLine)
            .Include(v => v.SubVoucherItems)
            .Where(a => stages.Contains((int)a.ActionState!.Value))
            .Where(v => (v.SabhaId == sabhaId))
            //.Where(v => voucherCategory.HasValue ? v.VoucherCategory == voucherCategory : true)
            .Where(v => EF.Functions.Like(v.VoucherSequenceNumber!, "%" + filterKeyword + "%") || EF.Functions.Like(v.VoucherAmount!, "%" + filterKeyword + "%") || EF.Functions.Like(v.TotalChequeAmount, "%" + filterKeyword + "%"))
            .OrderByDescending(v => v.Id);



        int totalCount = await result.CountAsync();


        //var pageSize = 10;
        int skipAmount = (pageNo - 1) * pageSize;

        var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


        return (totalCount, list);
    }
    
    public async Task<Voucher> getVoucherById(int id)
    {
        return await voteAccDbContext.Voucher
            .Include(m => m.DepositsForVoucher)
            .Include(m => m.SubVoucherItems)
            .ThenInclude(c => c.VoucherCrossOrders)
            //.Include(m => m.VoucherLog)
            .Include(m=>m.VoucherDocuments)
            .Include(m => m.VoucherInvoices)
            .Include(m => m.VoucherLine)
            .Where(m => m.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Voucher>> getVoucherForPsReport(int sabhaId, int year, int month)
    {
        return await voteAccDbContext.Voucher
           //.Include(m => m.CrossSettlement)
           //.Include(m => m.VoucherLog)
           //.Include(m => m.VoucherLine)
           .Where(m => m.SabhaId == sabhaId && m.Year == year && m.Month==month && m.ActionState == FinalAccountActionStates.HasCheque)
           .ToListAsync();
    }

    public async Task<IEnumerable<Voucher>> GetVoucherBySubVouchers(List<int> subVoucherIds)
    {
        return await voteAccDbContext.Voucher
            .AsNoTracking()
            .Include(m => m.VoucherLine)
            .Include(m => m.SubVoucherItems)
            .Where(m => m.SubVoucherItems!.Any(c => subVoucherIds.Contains(c.Id!.Value!)))
            .Distinct()
            .ToListAsync();
    }

    static List<FinalAccountActionStates[]> ConvertIntArrayToEnumArrays(List<int?> values)
    {
        var stageArrays = new List<FinalAccountActionStates[]>();
        foreach (int value in values)
        {
            stageArrays.Add(ConvertIntToEnumArray(value));
        }
        return stageArrays;
    }

    static FinalAccountActionStates[] ConvertIntToEnumArray(int value)
    {
        var stages = (FinalAccountActionStates[])Enum.GetValues(typeof(FinalAccountActionStates));
        return Array.FindAll(stages, stage => stage != FinalAccountActionStates.Deleted && (value & (int)stage) != 0);
    }

  

    private VoteAccDbContext voteAccDbContext
    {
        get { return Context as VoteAccDbContext; }
    }
}