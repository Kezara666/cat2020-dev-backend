using CAT20.Core.Models.Control;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Repositories.Mixin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Mixin
{
    public class CustomVoteSubLevel2Repository : Repository<CustomVoteSubLevel2>, ICustomVoteSubLevel2Repository
    {
        public CustomVoteSubLevel2Repository(DbContext context) : base(context)
        {
        }
        private MixinDbContext mixinDbContext
        {
            get { return Context as MixinDbContext; }
        }


        public async Task<CustomVoteSubLevel2> GetCustomVoteSubLevel2ByIdAsync(int id)
        {
            //return await mixinDbContext.CustomVoteSubLevel2s.FindAsync(id);

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CustomVoteSubLevel2>> GetCustomVoteSubLevel2sBySubLevel1Async(int subLevel1Id)
        {
            //return await mixinDbContext.CustomVoteSubLevel2s
            //    .Where(subLevel2 => subLevel2.CustomVoteSubLevel1Id == subLevel1Id)
            //    .ToListAsync();

            throw new NotImplementedException();
        }
    }
}