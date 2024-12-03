using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Repositories.FinalAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Vote;

namespace CAT20.Data.Repositories.FinalAccount
{
    public class CommitmentRepository : Repository<Commitment>, ICommitmentRepository
    {
        public CommitmentRepository(DbContext context) : base(context)
        {
        }
        private VoteAccDbContext voteAccDbContext
        {
            get { return Context as VoteAccDbContext; }
        }
        public async Task<IEnumerable<Commitment>> GetAll()
        {
            return await voteAccDbContext.Commitment
                .Where(m => m.ActionState != FinalAccountActionStates.Deleted)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<Commitment> GetForCreateVoucherById(int id)
        {
            return await voteAccDbContext.Commitment
                           .Where(m=>m.Id == id)
                           .Include(m=>m.CommitmentLine).ThenInclude(cl=>cl.CommitmentLineVotes)
                           .FirstOrDefaultAsync();
        }



        public async Task<(int totalCount,IEnumerable<Commitment> list)> getCommitmentForApproval(int sabhaId, int stage, int pageNo, int  pageSize, string? filterKeyword)
        {
            if (filterKeyword != "undefined")
            {
                filterKeyword = "%" + filterKeyword + "%";
            }
            else if (filterKeyword == "undefined")
            {
                filterKeyword = null;
            }

            var keyword = filterKeyword ?? "";
            
            FinalAccountActionStates status = (FinalAccountActionStates)Enum.Parse(typeof(FinalAccountActionStates), stage.ToString());
            // AuditStatus auditStatus = (AuditStatus)Enum.Parse(typeof(AuditStatus), stage.ToString());


            var result = voteAccDbContext.Commitment
                .Include(m => m.ActionLog!.Where(al => al.ActionState == FinalAccountActionStates.CCRejected))
                .Include(m => m.CommitmentLine!).ThenInclude(m => m.CommitmentLineVotes)
                .Where(m => (m.ActionState == status && m.SabhaId == sabhaId && m.ActionState != FinalAccountActionStates.Edited)
                && (string.IsNullOrEmpty(keyword) ||
                    (EF.Functions.Like(m.Id, keyword)) ||
                    (EF.Functions.Like(m.PayeeName, keyword))
                ))
                .OrderByDescending(m => m.Id);



             int totalCount = await result.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);


            
        }
        public async Task<(int totalCount,IEnumerable<Commitment> list)> getCommitmentForVoucher(int sabhaId, int stage, int pageNo, int  pageSize, string? filterKeyword)
        {
            if (filterKeyword != "undefined")
            {
                filterKeyword = "%" + filterKeyword + "%";
            }
            else if (filterKeyword == "undefined")
            {
                filterKeyword = null;
            }

            var keyword = filterKeyword ?? "";
            
            FinalAccountActionStates status = (FinalAccountActionStates)Enum.Parse(typeof(FinalAccountActionStates), stage.ToString());
            var result =  voteAccDbContext.Commitment
                .Include(m => m.ActionLog!.Where(al => al.ActionState == FinalAccountActionStates.CCRejected))
                //.Include(m => m.CommitmentLog.Where(cl => cl.Action == VoucherStatus.Created))
                .Include(m => m.CommitmentLine!).ThenInclude(m => m.CommitmentLineVotes)
                .Where(m => (m.ActionState == status && m.SabhaId == sabhaId && (m.HasVoucher == false||m.PaymentStatus == PaymentStatus.PartPayment))
                && (string.IsNullOrEmpty(keyword) ||
                    (EF.Functions.Like(m.Id, keyword)) ||
                    (EF.Functions.Like(m.PayeeName, keyword))
                ))
                .OrderByDescending(m => m.Id);


            int totalCount = await result.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);


        }
       public async Task<(int totalCount,IEnumerable<Commitment> list)> getCreatedCommitment(int sabhaId, int stage, int userId, int pageNo, int  pageSize, string? filterKeyword)
        {
            if (filterKeyword != "undefined")
            {
                filterKeyword = "%" + filterKeyword + "%";
            }
            else if (filterKeyword == "undefined")
            {
                filterKeyword = null;
            }

            var keyword = filterKeyword ?? "";
            
            FinalAccountActionStates status = (FinalAccountActionStates)Enum.Parse(typeof(FinalAccountActionStates), stage.ToString());
            // AuditStatus auditStatus = (AuditStatus)Enum.Parse(typeof(AuditStatus), stage.ToString());
            var result = voteAccDbContext.Commitment
                .Include(m => m.ActionLog.Where(al => al.ActionState == FinalAccountActionStates.CCRejected))
                .Include(m => m.CommitmentLine).ThenInclude(m => m.CommitmentLineVotes)
                .Where(m => (m.ActionState == status && m.SabhaId == sabhaId && m.CreatedBy == userId && m.ActionState != FinalAccountActionStates.Edited) &&
                            (string.IsNullOrEmpty(keyword) ||
                             (EF.Functions.Like(m.Id, keyword)) ||
                             (EF.Functions.Like(m.PayeeName, keyword))
                            ))
                .OrderByDescending(m => m.Id);
               

            int totalCount = await result.CountAsync();


            //var pageSize = 10;
            int skipAmount = (pageNo - 1) * pageSize;

            var list = await result.Skip(skipAmount).Take(pageSize).ToListAsync();


            return (totalCount, list);

        }

     

       public async Task<Commitment> GetCommitmentById(string id)
        {
            return await voteAccDbContext.Commitment
                .Include(m => m.CommitmentLog)
                .Include(m => m.ActionLog)
                .Include(m => m.CommitmentLine).ThenInclude( m => m.CommitmentLineVotes)
                .Where(m => m.CommitmentSequenceNumber == id && m.ActionState != FinalAccountActionStates.Edited)
                .FirstOrDefaultAsync();
        }  
        public async Task<Commitment> GetCommitmentById(int? id)
        {
            return await voteAccDbContext.Commitment
                .Include(m => m.ActionLog)
                .Include(m => m.CommitmentLine).ThenInclude(m => m.CommitmentLineVotes)
                .Where(m => m.Id == id && m.ActionState != FinalAccountActionStates.Edited)
                .FirstOrDefaultAsync();
        }
        
        
    }
    
   
}

