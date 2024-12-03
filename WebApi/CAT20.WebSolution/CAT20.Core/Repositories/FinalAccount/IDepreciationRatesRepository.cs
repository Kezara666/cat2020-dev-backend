﻿using CAT20.Core.Models.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Repositories.FinalAccount
{
    public interface IDepreciationRatesRepository: IRepository<DepreciationRates>
    {
        public Task<DepreciationRates> GetDepreciationRate(string subtitle,int? sabhaId);
    }
}
