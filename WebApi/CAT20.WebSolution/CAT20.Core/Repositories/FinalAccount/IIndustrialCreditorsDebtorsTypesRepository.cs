using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.FinalAccount
{
    public interface IIndustrialCreditorsDebtorsTypesRepository: IRepository<IndustrialCreditorsDebtorsTypes>
    {
        Task<IEnumerable<IndustrialCreditorsDebtorsTypes>> GetAllCreditorsTypesActive();
        Task<IEnumerable<IndustrialCreditorsDebtorsTypes>> GetAllDebtorsTypesActive();
    }
}
