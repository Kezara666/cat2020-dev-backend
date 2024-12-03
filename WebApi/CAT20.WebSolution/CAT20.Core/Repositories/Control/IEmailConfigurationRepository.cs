using CAT20.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.Control
{
    public interface IEmailConfigurationRepository : IRepository<EmailConfiguration>
    {
        Task<EmailConfiguration> GetByIdAsync(int id);
    }
}
