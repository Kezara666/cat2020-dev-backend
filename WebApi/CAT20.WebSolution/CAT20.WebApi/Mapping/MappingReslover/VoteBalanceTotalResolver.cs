using AutoMapper;
using CAT20.Core.Models.Vote;
using CAT20.WebApi.Resources.Vote;

namespace CAT20.WebApi.Mapping.MappingReslover
{



    public class VoteBalanceTotalResolver : IValueResolver<VoteBalance, VoteBalanceResource, decimal>
    {
        public decimal Resolve(VoteBalance source, VoteBalanceResource destination, decimal destMember, ResolutionContext context)
        {

            /*
         classification = 1 => income
        classification = 2 => expense
        classification = 3 => asset
        classification = 4 => liability
        classification = 5 => equity
*/


            if (source.ClassificationId == 1)
            {
                return source.Credit - source.Debit; // Reverse calculation when classification is 1
            }
            else if (source.ClassificationId == 2)
            {
                return  (decimal)source.AllocationAmount! + source.Credit - source.Debit; // Reverse calculation when classification is 2
            }
            else if (source.ClassificationId == 3)
            {
                return source.Credit - source.Debit; // Reverse calculation when classification is 3
            }
            else if (source.ClassificationId == 4)
            {
                return source.Debit - source.Credit; // Reverse calculation when classification is 4
            }
            else
            {

                return source.Debit - source.Credit; // Default calculation
            }
        }
    }


}
