using AutoMapper;
using CAT20.Core.Models.Vote;
using CAT20.WebApi.Resources.Vote;

namespace CAT20.WebApi.Mapping.MappingReslover
{
    public class AccountBalanceResolver :   IValueResolver<AccountDetail, AccountDetailResource, decimal>
        {
            public decimal Resolve(AccountDetail source, AccountDetailResource destination, decimal destMember, ResolutionContext context)
            {
                    return source.RunningBalance - source.ExpenseHold;
            }
        }
    
}
