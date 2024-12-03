using CAT20.Core.Models.Vote;
using CAT20.Core.Repositories.Vote;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.Vote
{
    public class CustomVoteEntryRepository : Repository<CustomVoteEntry>, ICustomVoteEntryRepository
    {
        public CustomVoteEntryRepository(DbContext context) : base(context)
        {
        }
    }
}
