using AutoMapper;
using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.Vote;
using CAT20.Core.DTO.Vote.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.Vote
{
    public class SpecialLedgerAccountsService : ISpecialLedgerAccountsService
    {

        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SpecialLedgerAccountsService(IVoteUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<(bool, string?)> AssignSpecialLedgerAccount(AssignSpecialLedgerAccount assignSpecialLedgerAccountResource, HTokenClaim token)
        {
            try
            {

                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                if (session != null)
                {

                    var vt = await _unitOfWork.VoteDetails.GetByIdAsync(assignSpecialLedgerAccountResource.SpecialLedgerAccountId);


                    if (vt == null)
                    {
                        throw new Exception("Ledger Account Not Found");
                    }

                    var newSpecialLedgerAccount = new SpecialLedgerAccounts
                    {
                        VoteId = vt.ID,
                        VoteCode = vt.Code,
                        TypeId = assignSpecialLedgerAccountResource.SpecialLedgerAccountTypeId,
                        SabhaId = token.sabhaId,
                        CreatedBy = token.userId,
                        CreatedAt = session.StartAt,
                        SystemActionAt = DateTime.Now,

                    };

                    await _unitOfWork.SpecialLedgerAccounts.AddAsync(newSpecialLedgerAccount);
                    await _unitOfWork.CommitAsync();
                    return (true, "Special Ledger Account Assigned Successfully");

                }
                else
                {
                    throw new FieldAccessException("Active Session Not Found");
                }

            }
            catch (Exception ex)
            {
                if (ex is FinalAccountException or GeneralException)
                {
                    return (false, ex.Message);
                }
                else
                {
                    return (false, null);
                }



            }
        }

        public async Task<IEnumerable<SpecialLedgerAccountsResource>> GetSpecialLedgerAccountsForSabaha(int sabahId)
        {
             var specialLedgerAccounts = await _unitOfWork.SpecialLedgerAccounts.GetSpecialLedgerAccountsForSabaha(sabahId);


            var specialLedgerAccountsResource = _mapper.Map<IEnumerable<SpecialLedgerAccounts>, IEnumerable<SpecialLedgerAccountsResource>>(specialLedgerAccounts);

            foreach (var item in specialLedgerAccountsResource)
            {
                item.SpecialLegerAccount = _mapper.Map<VoteDetailLimitedresource>(await _unitOfWork.VoteDetails.GetByIdAsync(item.VoteId!.Value));
            }


            return specialLedgerAccountsResource;
        }

        public Task<IEnumerable<SpecialLedgerAccountTypes>> GetSpecialLedgerAccountTypes()
        {
            return _unitOfWork.SpecialLedgerAccounts.GetSpecialLedgerAccountTypes();
        }
    }
}
