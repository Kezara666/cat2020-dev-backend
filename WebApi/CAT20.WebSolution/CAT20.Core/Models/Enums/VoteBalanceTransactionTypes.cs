using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Enums
{
    public enum VoteBalanceTransactionTypes
    {
        Inti,
        BFCredit, //Balance Forward Credit
        BFDebit, //Balance Forward Debit

        BFRBCredit, //Balance Forward Roll Back Credit
        BFRBDebit, //Balance Forward Roll Back Debit

        BFJACredit, //Balance Forward Journal Adjustment
        BFJADebit, //Balance Forward Journal Adjustment

        Credit,
        Debit,

        Billing, // billing transaction for other module like assessment,water, shop rental 
        ReverseBilling, // opposite of billing transaction

        Income, //Income from mix receipt
        ReverseIncome, //Cancel receipt from mix receipt

        JournalDebit,  // general journal debit
        JournalCredit, // general journal credit

        FRDebit,  // vote transfer from FR 
        FRCredit, // vote transfer to FR
        Supplementary,
        CutProvision,
        TakeHold,
        ReleaseTakeHold,


        YearEndJournalDebit, // year end journal debit
        YearEndJournalCredit, // year end journal credit

        AfterFAJournalDebit, // after final account journal debit
        AfterFAJournalCredit, // after final account journal credit

        AutoTransferJNLCredit, // auto transfer from other module
        AutoTransferJNLDebit, // auto transfer to other module

        AutoTransferJNLRBCredit, // auto transfer roll back from other module
        AutoTransferJNLRBDebit, // auto transfer roll back to other module


        CreateCommitment,
        CreateVoucher,
        SubjectAction,
        CCAction,
        SecretaryAction,
        ChairmanAction,
        CreateCheque,
        PrintCheque,
        WithdrawCommitment,
        ReleaseCommitmentHold,
        Disapproval,
        WithdrawVoucher
    }
}
