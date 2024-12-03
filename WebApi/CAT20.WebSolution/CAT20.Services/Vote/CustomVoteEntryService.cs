using AutoMapper;
using CAT20.Core;
using CAT20.Core.Services.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.Vote
{
    public class CustomVoteEntryService : ICustomVoteEntryService
    {

        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomVoteEntryService(IVoteUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


    }
}
