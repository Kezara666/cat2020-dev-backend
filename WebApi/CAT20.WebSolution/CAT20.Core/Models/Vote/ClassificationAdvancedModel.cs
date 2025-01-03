﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Vote
{
    public  class ClassificationAdvancedModel
    {
        public ClassificationAdvancedModel()
        {
        }

        public int? Id { get; set; }
        public string Description { get; set; }
        public string? Code { get; set; }

        public virtual ICollection<VoteDetailBasicModel>? VoteDetails { get; set; }
    }
}
