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
    public class CustomVoteSubLevel1Repository : Repository<CustomVoteSubLevel1>, ICustomVoteSubLevel1Repository
    {
        public CustomVoteSubLevel1Repository(DbContext context) : base(context)
        {
        }
        private MixinDbContext mixinDbContext
        {
            get { return Context as MixinDbContext; }
        }


        public async Task<CustomVoteSubLevel1> GetByCustomVoteSubLevel1IdAsysc(int id)
        {
            //return await mixinDbContext.CustomVoteSubLevel1s.FindAsync(id);
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CustomVoteSubLevel1>> GetCustomVoteSubLevel1sByCustomVoteAsync(int subLevel1Id)
        {
            //return await mixinDbContext.CustomVoteSubLevel1s
            //    .Where(subLevel1 => subLevel1.CustomVoteId == subLevel1Id)
            //    .ToListAsync();

           throw new NotImplementedException();
        }
    }
}