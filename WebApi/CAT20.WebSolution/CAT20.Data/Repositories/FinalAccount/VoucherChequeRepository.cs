using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data.Repositories.FinalAccount;

public class VoucherChequeRepository: Repository<VoucherCheque>, IVoucherChequeRepository
{
    public Task<IVoucherChequeRepository> getVoucherById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IVoucherChequeRepository> getVoucherForSabha(int sabhaId)
    {
        throw new NotImplementedException();
    }

    public VoucherChequeRepository(DbContext context) : base(context)
    {
    }
    
    public async Task<(int totalCount,IEnumerable<VoucherCheque> list)> getChequeForSabha(int sabhaId, bool stage, int pageNo,
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
        var result = voteAccDbContext.VoucherCheque
            .Include(v => v.VoucherItemsForCheque)
            .Where(m => (m.SabhaId == sabhaId)
                        && (string.IsNullOrEmpty(keyword) ||
                            (EF.Functions.Like(m.Id, keyword)) ||
                            (EF.Functions.Like(m.ChequeNo, keyword))
                        ))
            .OrderByDescending(m => m.Id);
           

        int totalCount = await result.CountAsync();


        //var pageSize = 10;
        int skipAmount = (pageNo - 1) * pageSize;

        var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


        return (totalCount, list);

    }

    Task<VoucherCheque> IVoucherChequeRepository.getVoucherById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasChequeeNumberExist(string chequeNumber, HTokenClaim  token)
    {
        return voteAccDbContext.VoucherCheque.AnyAsync(x => x.ChequeNo == chequeNumber && x.SabhaId == token.sabhaId );
    }

    private VoteAccDbContext voteAccDbContext
    {
        get { return Context as VoteAccDbContext; }
    }
    
}