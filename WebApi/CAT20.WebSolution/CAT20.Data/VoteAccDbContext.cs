using CAT20.Core.Models.Vote;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using Microsoft.Extensions.Logging;
using CAT20.Core.Models.TradeTax;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.FinalAccount.logs;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.WaterBilling;
using CAT20.Data.Repositories.FinalAccount;
using CAT20.Core.Models.Control;

namespace CAT20.Data
{
    public partial class VoteAccDbContext : DbContext
    {
        public VoteAccDbContext()
        {
        }

        public VoteAccDbContext(DbContextOptions<VoteAccDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SpecialLedgerAccountTypes> SpecialLedgerAccountTypes { get; set; }
        public virtual DbSet<SpecialLedgerAccounts> SpecialLedgerAccounts { get; set; }

        public virtual DbSet<AccountBalanceDetail> AccountBalanceDetails { get; set; }
        public virtual DbSet<AccountDetail> AccountDetails { get; set; }
        public virtual DbSet<BalancesheetBalance> BalancesheetBalances { get; set; }
        public virtual DbSet<BalancesheetSubtitle> BalancesheetSubtitles { get; set; }
        public virtual DbSet<BalancesheetTitle> BalancesheetTitles { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<SubProject> SubProjects { get; set; }
        public virtual DbSet<IncomeSubtitle> IncomeSubtitles { get; set; }
        public virtual DbSet<IncomeTitle> IncomeTitles { get; set; }
        public virtual DbSet<VoteBalance> VoteBalances { get; set; }
        public virtual DbSet<VoteBalanceLog> VoteBalancesLogs { get; set; }
        public virtual DbSet<CustomVoteBalance> CustomVoteBalances { get; set; }
        public virtual DbSet<CustomVoteBalanceLog> CustomVoteBalancesLogs { get; set; }

        public virtual DbSet<Programme> Programmes { get; set; }
        public virtual DbSet<VoteDetail> VoteDetails { get; set; }
        public virtual DbSet<BusinessNature> BusinessNatures { get; set; }
        public virtual DbSet<BusinessSubNature> BusinessSubNatures { get; set; }
        public virtual DbSet<TaxValue> TaxValues { get; set; }
        public virtual DbSet<TradeTaxVoteAssignment> TradeTaxVoteAssignments { get; set; }
        public virtual DbSet<Commitment> Commitment { get; set; }
        public virtual DbSet<CommitmentLine> CommitmentLine { get; set; }
        public virtual DbSet<CommitmentActionsLog> CommitmentApprovedLog { get; set; }
        public virtual DbSet<CommitmentLineVotes> CommitmentLineVotes { get; set; }
        public virtual DbSet<CommitmentLog> CommitmentLogs { get; set; }
        public virtual DbSet<CommitmentLineLog> CommitmentLineLogs { get; set; }
        public virtual DbSet<CommitmentLineVotesLog> CommitmentLineVotesLogs { get; set; }
        public virtual DbSet<Voucher> Voucher { get; set; }
        public virtual DbSet<SubVoucherItem> DepositVoucherItems { get; set; }
        public virtual DbSet<DepositForVoucher> DepositForVoucher { get; set; }
        public virtual DbSet<EmployeeLoansForVoucher> LoansForVoucher { get; set; }


        public virtual DbSet<VoucherLine> VoucherLines { get; set; }
        public virtual DbSet<VoucherSubLine> VoucherSubLines { get; set; }
        public virtual DbSet<VoucherLog> VoucherLog { get; set; }

        public virtual DbSet<VoucherDocument> VoucherDocuments { get; set; }
        public virtual DbSet<VoucherInvoice> VoucherInvoices { get; set; }

        public virtual DbSet<VoucherActionLog> VoucherApprovedLogs { get; set; }
        public virtual DbSet<VoucherCrossOrder> VoucherCrossOrders { get; set; }
        public virtual DbSet<VoucherCheque> VoucherCheque { get; set; }
        public virtual DbSet<DepositSubInfo> DepositSubInfo { get; set; }
        public virtual DbSet<FinalAccountSequenceNumber> FinalAccountSequenceNumber { get; set; }
        public virtual DbSet<VoteJournalAdjustment> VoteJournalAdjustments { get; set; }
        public virtual DbSet<VoteJournalItemFrom> VoteJournalItems { get; set; }


        /*vote transfer*/
        public virtual DbSet<Supplementary> Supplementary { get; set; }
        public virtual DbSet<CutProvision> CutProvision { get; set; }
        public virtual DbSet<FR66Transfer> FR66Transfer { get; set; }
        public virtual DbSet<FR66FromItem> FR66FromItem { get; set; }
        public virtual DbSet<FR66ToItem> FR66ToItem { get; set; }
        public virtual DbSet<AccountTransfer> AccountTransfer { get; set; }
        public virtual DbSet<AccountTransferRefunding> AccountTransferRefunding { get; set; }

        public virtual DbSet<SubImprest> SubImprests { get; set; }
        public virtual DbSet<SubImprestSettlement> SubImprestSettlements { get; set; }
        public virtual DbSet<SubImprestDocument> SubImprestDocument { get; set; }


        public virtual DbSet<VoteLedgerBook> VoteLedgerBook { get; set; }
        public virtual DbSet<CashBook> CashBook { get; set; }
        public virtual DbSet<CashBookDailyBalance> CashBookDailyBalance { get; set; }
        public virtual DbSet<Deposit> Deposit { get; set; }
        public virtual DbSet<VoteLedgerBookDailyBalance> VoteLedgerBookDailyBalance { get; set; }

        public virtual DbSet<InternalJournalTransfers> InternalJournalTransfers { get; set; }

        public virtual DbSet<SabhaFundSource> SabhaFundSource { get; set; }
        public virtual DbSet<IndustrialCreditorsDebtorsTypes> IndustrialCreditorsDebtorsTypes { get; set; }

        /*for open balances*/
        public virtual DbSet<CreditorBilling> CreditorBilling { get; set; }
        public virtual DbSet<FixedDeposit> FixedDeposit { get; set; }
        public virtual DbSet<IndustrialCreditors> IndustrialCreditors { get; set; }
        public virtual DbSet<IndustrialDebtors> IndustrialDebtors { get; set; }
        public virtual DbSet<LALoan> LALoan { get; set; }
        public virtual DbSet<PrepaidPayment> PrePaidAccounts { get; set; }
        public virtual DbSet<ReceivableExchangeNonExchange> ReceivableExchangeNonExchange { get; set; }
        public virtual DbSet<StoresCreditor> StoresCreditors { get; set; }
        public virtual DbSet<FixedAssets> FixedAssets { get; set; }
        public virtual DbSet<SingleOpenBalance> SingleOpenBalances { get; set; }
        public virtual DbSet<DepreciationRates> DepreciationRates { get; set; }
        public virtual DbSet<Classification> Classifications { get; set; }
        public virtual DbSet<MainLedgerAccount> MainLedgerAccounts { get; set; }
        public virtual DbSet<IncomeExpenditureSubledgerAccount> IncomeExpenditureSubledgerAccounts { get; set; }
        public virtual DbSet<BalsheetSubledgerAccount> BalsheetSubledgerAccounts { get; set; }
        //public virtual DbSet<CommitmentLineCustomVotes> CommitmentLineCustomVotes { get; set; }
        public virtual DbSet<VoteBalanceActionLog> VoteBalanceActionLogs { get; set; }
        public virtual DbSet<CustomVoteBalanceActionLog> CustomVoteBalanceActionLogs { get; set; }
        //public virtual DbSet<CustomVoteEntry> CustomVoteEntry { get; set; }
        public virtual DbSet<Budget> Budget { get; set; }
        public virtual DbSet<LedgerAccountGroupAssignment> LedgerAccountGroupAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_sinhala_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<SpecialLedgerAccountTypes>(entity =>
            {

                entity.ToTable("special_ledger_account_type");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                //entity.HasIndex(e => e.AccountDetailID, "fk_acc_bd_acc_d_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NameInEnglish).HasColumnName("name_in_english");
                entity.Property(e => e.NameInSinhala).HasColumnName("name_in_sinhala");
                entity.Property(e => e.NameInTamil).HasColumnName("name_in_tamil");

                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("status");


                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");



            });

            
            modelBuilder.Entity<SpecialLedgerAccounts>(entity =>
            {
                entity.ToTable("special_ledger_accounts");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");


                entity.HasIndex(e => new { e.SabhaId, e.TypeId }).IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.VoteId).HasColumnName("vote_id");

                entity.Property(e => e.VoteCode).HasColumnName("vote_code");
                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("status");

                entity.Property(e => e.SabhaId).HasColumnName("acc_sabha_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");



            });


            modelBuilder.Entity<AccountBalanceDetail>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("PRIMARY");

                entity.ToTable("account_bal_details");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                //entity.HasIndex(e => e.AccountDetailID, "fk_acc_bd_acc_d_id");

                entity.Property(e => e.ID).HasColumnName("acc_bd_id");

                entity.Property(e => e.AccountDetailID).HasColumnName("acc_bd_acc_d_id");

                entity.Property(e => e.BalanceAmount).HasColumnName("acc_bd_bal_amount");

                entity.Property(e => e.EnteredDate).HasColumnName("acc_bd_enter_date");

                entity.Property(e => e.Status).HasColumnName("acc_bd_status");

                entity.Property(e => e.Year).HasColumnName("acc_bd_year");

                entity.Property(e => e.SabhaID).HasColumnName("acc_sabha_id");

                entity.HasOne(d => d.accountDetail)
                    .WithMany(p => p.accountBalDetail)
                    .HasForeignKey(d => d.AccountDetailID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_acc_bd_acc_d_id");

                entity.Property(e => e.RowVersion).IsConcurrencyToken();

            });

            modelBuilder.Entity<AccountDetail>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("PRIMARY");

                entity.ToTable("account_details");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID).HasColumnName("acc_d_id");

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(255)
                    .HasColumnName("acc_d_acc_no");

                entity.Property(e => e.BankID)
                    .HasColumnName("acc_d_bank_id")
                    .HasComment("control db fk");

                entity.Property(e => e.VoteId)
                .HasColumnName("acc_d_vote_id");

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("acc_d_name_english");

                entity.Property(e => e.NameSinhala)
                    .HasMaxLength(255)
                    .HasColumnName("acc_d_name_sinhala");

                entity.Property(e => e.NameTamil)
                    .HasMaxLength(255)
                    .HasColumnName("acc_d_name_tamil");

                entity.Property(e => e.OfficeID).HasColumnName("acc_d_office_id");
                entity.Property(e => e.ExpenseHold).HasDefaultValue(0m).HasColumnName("expense_hold");
                entity.Property(e => e.RunningBalance).HasDefaultValue(0m).HasColumnName("running_balance");

                entity.Property(e => e.Status).HasColumnName("acc_d_status");
                entity.Property(e => e.BankCode).HasMaxLength(10).HasColumnName("acc_d_bank_code");
                entity.Property(e => e.BranchCode).HasMaxLength(10).HasColumnName("acc_d_branch_code");

                entity.Property(e => e.BranchCode).HasMaxLength(10).HasColumnName("acc_d_branch_code");

                // entity.HasOne(va => va.BankDetail)
                //                  .WithMany(vd => vd.AccountDetail)
                //                  .OnDelete(DeleteBehavior.Restrict)
                //                  .HasForeignKey(va => va.BankID);


                entity.Property(e => e.RowStatus).HasColumnName("row_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                entity.Property(e => e.RowVersion).HasColumnName("row_version");


            });

            modelBuilder.Entity<BalancesheetBalance>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("PRIMARY");

                entity.ToTable("vt_balancesheet_balance");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID).HasColumnName("vt_balancesheet_bal_id");

                entity.Property(e => e.Balance).HasColumnName("vt_balancesheet_bal_balance");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .HasColumnName("vt_balancesheet_bal_comment");

                entity.Property(e => e.EnteredDate).HasColumnName("vt_balancesheet_bal_enter_date");

                entity.Property(e => e.SabhaID).HasColumnName("vt_balancesheet_bal_sabha_id");

                entity.Property(e => e.Status).HasColumnName("vt_balancesheet_bal_status");

                entity.Property(e => e.VoteDetailID).HasColumnName("vt_balancesheet_bal_vote_id");

                entity.Property(e => e.Year).HasColumnName("vt_balancesheet_bal_year");
            });

            modelBuilder.Entity<BalancesheetSubtitle>(entity =>
            {
                entity.ToTable("vt_balsheet_subtitle");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                //entity.HasIndex(e => e.BalsheetTitleID, "fk_vt_balsheet_subtitle_title_id");

                entity.Property(e => e.ID).HasColumnName("vt_balsheet_subtitle_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("vt_balsheet_subtitle_code");

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("vt_balsheet_subtitle_name_english");

                entity.Property(e => e.NameSinhala)
                    .HasMaxLength(255)
                    .HasColumnName("vt_balsheet_subtitle_name_sinhala");

                entity.Property(e => e.NameTamil)
                    .HasMaxLength(255)
                    .HasColumnName("vt_balsheet_subtitle_name_tamil");

                entity.Property(e => e.SabhaID).HasColumnName("vt_balsheet_subtitle_sabha_id");

                entity.Property(e => e.Status).HasColumnName("vt_balsheet_subtitle_status");
                //entity.Property(e => e.Status)
                //    .HasMaxLength(255)
                //    .HasColumnName("vt_balsheet_subtitle_status");

                entity.Property(e => e.BalsheetTitleID).HasColumnName("vt_balsheet_subtitle_title_id");
                entity.Property(e => e.BankAccountID).HasColumnName("vt_balsheet_subtitle_bank_account_id");

                entity.HasOne(d => d.balancesheetTitle)
                    .WithMany(p => p.balancesheetSubtitle)
                    .HasForeignKey(d => d.BalsheetTitleID)
                    .HasConstraintName("fk_vt_balsheet_subtitle_title_id");

                entity.Ignore(e => e.accountDetail);
            });

            modelBuilder.Entity<BalancesheetTitle>(entity =>
            {
                entity.ToTable("vt_balsheet_title");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.Property(e => e.ID).HasColumnName("vt_balsheet_title_id");

                entity.Property(e => e.Balpath).HasColumnName("vt_balsheet_title_balpath");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("vt_balsheet_title_code");

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("vt_balsheet_title_name_english");

                entity.Property(e => e.NameSinhala)
                    .HasMaxLength(255)
                    .HasColumnName("vt_balsheet_title_name_sinhala");

                entity.Property(e => e.NameTamil)
                    .HasMaxLength(255)
                    .HasColumnName("vt_balsheet_title_name_tamil");

                entity.Property(e => e.SabhaID).HasColumnName("vt_balsheet_title_sabha_id");

                entity.Property(e => e.Status).HasColumnName("vt_balsheet_title_status");
                entity.Property(e => e.ClassificationID).HasColumnName("vt_balsheet_title_classification_id");
                entity.Property(e => e.MainLedgerAccountID).HasColumnName("vt_balsheet_title_mainlegr_acct_id");
            });


            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("vt_inc_project");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                //entity.HasIndex(e => e.ProgrammeID, "fk_vt_inc_project_programme_id");

                entity.Property(e => e.ID).HasColumnName("vt_inc_project_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("vt_inc_project_code");

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("vt_inc_project_english");

                entity.Property(e => e.ProgrammeID).HasColumnName("vt_inc_project_programme_id");

                entity.Property(e => e.SabhaID).HasColumnName("vt_inc_project_sabha_id");

                entity.Property(e => e.NameSinhala)
                    .HasMaxLength(255)
                    .HasColumnName("vt_inc_project_sinhala");

                entity.Property(e => e.Status).HasColumnName("vt_inc_project_status");

                entity.Property(e => e.NameTamil)
                    .HasMaxLength(255)
                    .HasColumnName("vt_inc_project_tamil");

                entity.HasOne(d => d.programme)
                    .WithMany(p => p.project)
                    .HasForeignKey(d => d.ProgrammeID)
                    .HasConstraintName("fk_vt_inc_project_programme_id");
            });

            modelBuilder.Entity<SubProject>(entity =>
            {
                entity.ToTable("vt_inc_sub_project");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                //entity.HasIndex(e => e.ProjectID, "fk_vt_inc_sub_project_project_id");

                entity.Property(e => e.ID).HasColumnName("vt_inc_sub_project_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("vt_inc_sub_project_name_code");

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("vt_inc_sub_project_name_english");

                entity.Property(e => e.NameSinhala)
                    .HasMaxLength(255)
                    .HasColumnName("vt_inc_sub_project_name_sinhala");

                entity.Property(e => e.NameTamil)
                    .HasMaxLength(255)
                    .HasColumnName("vt_inc_sub_project_name_tamil");

                entity.Property(e => e.ProjectID).HasColumnName("vt_inc_sub_project_project_id");

                entity.Property(e => e.SabhaID).HasColumnName("vt_inc_sub_project_sabha_id");
                entity.Property(e => e.ProgrammeID).HasColumnName("vt_inc_sub_project_programme_id");

                entity.Property(e => e.Status).HasColumnName("vt_inc_sub_project_status");

                entity.HasOne(d => d.project)
                    .WithMany(p => p.subProject)
                    .HasForeignKey(d => d.ProjectID)
                    .HasConstraintName("fk_vt_inc_sub_project_project_id");
            });

            modelBuilder.Entity<IncomeSubtitle>(entity =>
            {
                entity.ToTable("vt_inc_subtitle");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                //entity.HasIndex(e => e.IncomeTitleID, "fk_vt_inc_subtitle_title_id");

                entity.Property(e => e.ID).HasColumnName("vt_inc_subtitle_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("vt_inc_subtitle_name_code");

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("vt_inc_subtitle_name_english");

                entity.Property(e => e.NameSinhala)
                    .HasMaxLength(255)
                    .HasColumnName("vt_inc_subtitle_name_sinhala");

                entity.Property(e => e.NameTamil)
                    .HasMaxLength(255)
                    .HasColumnName("vt_inc_subtitle_name_tamil");

                entity.Property(e => e.SabhaID).HasColumnName("vt_inc_subtitle_sabha_id");
                entity.Property(e => e.ProgrammeID).HasColumnName("vt_inc_subtitle_programme_id");

                entity.Property(e => e.Status).HasColumnName("vt_inc_subtitle_status");


                entity.Property(e => e.IncomeTitleID).HasColumnName("vt_inc_subtitle_title_id");

                entity.HasOne(d => d.incomeTitle)
                    .WithMany(p => p.incomeSubtitle)
                    .HasForeignKey(d => d.IncomeTitleID)
                    .HasConstraintName("fk_vt_inc_subtitle_title_id");
            });

            modelBuilder.Entity<IncomeTitle>(entity =>
            {
                entity.ToTable("vt_inc_title");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                //entity.HasIndex(e => e.ProgrammeID, "fk_vt_inc_title_name_programme_id");

                entity.Property(e => e.ID).HasColumnName("vt_inc_title_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("vt_inc_title_name_code");

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("vt_inc_title_name_english");

                entity.Property(e => e.NameSinhala)
                    .HasMaxLength(255)
                    .HasColumnName("vt_inc_title_name_sinhala");

                entity.Property(e => e.NameTamil)
                    .HasMaxLength(255)
                    .HasColumnName("vt_inc_title_name_tamil");

                entity.Property(e => e.ProgrammeID).HasColumnName("vt_inc_title_programme_id");

                entity.Property(e => e.SabhaID).HasColumnName("vt_inc_title_sabha_id");

                entity.Property(e => e.Status).HasColumnName("vt_inc_title_status");

                entity.Property(e => e.ClassificationID).HasColumnName("vt_inc_title_classification_id");
                entity.Property(e => e.MainLedgerAccountID).HasColumnName("vt_inc_title_mainlegr_acct_id");

                //entity.HasOne(d => d.programme)
                //    .WithMany(p => p.incomeTitle)
                //    .HasForeignKey(d => d.ProgrammeID)
                //    .HasConstraintName("fk_vt_inc_title_name_programme_id");
            });

            modelBuilder.Entity<VoteBalance>(entity =>
            {
                entity.ToTable("vt_vote_balance");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("vt_vote_bal_id");
                entity.Property(e => e.SabhaId).HasColumnName("vt_vote_bal_sabha_id");
                entity.Property(e => e.VoteDetailId).HasColumnName("vt_vote_bal_vote_id");
                entity.Property(e => e.ClassificationId).HasColumnName("vt_vote_classification_id");
                entity.Property(e => e.Year).HasColumnName("vt_vote_bal_year");
                entity.Property(e => e.IsCarryForward).HasDefaultValue(false).HasColumnName("vt_vote_bal_is_carry_forward");
                entity.Property(e => e.Month).HasColumnName("vt_vote_bal_month");
                entity.Property(e => e.Status).HasColumnName("vt_vote_bal_status");


                entity.Property(e => e.EstimatedIncome).HasColumnName("vt_vote_bal_estimated_income_amount").HasDefaultValue(0m);
    
                entity.Property(e => e.AllocationAmount).HasColumnName("vt_vote_bal_allocation_amount");
                entity.Property(e => e.TakeHoldRate).HasColumnName("vt_vote_bal_take_hold_rate");
                entity.Property(e => e.TakeHoldAmount).HasColumnName("vt_vote_bal_take_hold_amount");
                //entity.Property(e => e.AllocationExchangeAmount).HasColumnName("vt_vote_bal_allocation_exchange_amount");
                entity.Property(e => e.TotalHold).HasColumnName("vt_vote_bal_total_hold").HasDefaultValue(0m);
                entity.Property(e => e.Debit).HasColumnName("vt_vote_bal_debit").HasDefaultValue(0m);
                entity.Property(e => e.Credit).HasColumnName("vt_vote_bal_credit").HasDefaultValue(0m);
                entity.Property(e => e.CustomVoteDebit).HasColumnName("vt_vote_bal_cstm_vt_debit").HasDefaultValue(0m);
                entity.Property(e => e.CustomVoteCredit).HasColumnName("vt_vote_bal_cstm_vt_credit").HasDefaultValue(0m);
                entity.Property(e => e.TotalCommitted).HasColumnName("vt_vote_bal_total_commited").HasDefaultValue(0m);
                entity.Property(e => e.TotalPending).HasColumnName("vt_vote_bal_total_pending").HasDefaultValue(0m);

                //entity.Property(e => e.ExchangedAmount).HasColumnName("vt_vote_bal_exchange_amount").HasDefaultValue(0m);
                entity.Property(e => e.CreditDebitRunningBalance).HasColumnName("vt_vote_bal_credit_debit_balance").HasDefaultValue(0m);
                entity.Property(e => e.RunningBalance).HasColumnName("vt_vote_bal_running_balance").HasDefaultValue(0m);
                entity.Property(e => e.TransactionType).HasColumnName("vt_vote_bal_transaction_type");
                //entity.Property(e => e.Code).HasColumnName("vt_vote_bal_code");
                //entity.Property(e => e.SubCode).HasColumnName("vt_vote_bal_sub_code");

                entity.Property(e => e.CarryForwardCredit).HasColumnName("vt_vote_bal_carry_forward_total_credit").HasDefaultValue(0m);
                entity.Property(e => e.CarryForwardDebit).HasColumnName("vt_vote_bal_carry_forward_total_debit").HasDefaultValue(0m);

                entity.Property(e => e.CreditDebitCarryForwardRunningBalance).HasColumnName("vt_vote_bal_credit_debit_carry_forward_balance").HasDefaultValue(0m);

                entity.Property(e => e.SurchargeCredit).HasColumnName("vt_vote_bal_surcharge_total_credit").HasDefaultValue(0m);
                entity.Property(e => e.SurchargeDebit).HasColumnName("vt_vote_bal_surcharge_total_debit").HasDefaultValue(0m);

                entity.Property(e => e.CreditDebitSurchargeRunningBalance).HasColumnName("vt_vote_bal_credit_debit_surcharge_balance").HasDefaultValue(0m);

                entity.Property(e => e.TransferFlag).HasColumnName("vt_vote_transfer_flag").HasDefaultValue(VoteTransferFlag.Nothing);


                entity.HasIndex(e => new { e.VoteDetailId, e.Year, e.Status }).IsUnique();


                entity.HasOne(va => va.VoteDetail)
                    .WithMany(vd => vd.VoteBalances)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(va => va.VoteDetailId);


                entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("updated_at");

                entity.Property(e => e.OfficeId).HasColumnName("office_id");
                entity.Property(e => e.SessionIdByOffice).HasColumnName("session_id_by_office");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();


            });


            modelBuilder.Entity<VoteBalanceLog>(entity =>
            {
                entity.ToTable("vt_vote_balance_log");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("vt_vote_bal_lg_id");
                entity.Property(e => e.VoteBalanceId).HasColumnName("vt_vote_bal_lg_vote_bal_id");
                entity.Property(e => e.SabhaId).HasColumnName("vt_vote_bal_lg_sabha_id");
                entity.Property(e => e.VoteDetailId).HasColumnName("vt_vote_bal_lg_vote_id");
                entity.Property(e => e.Year).HasColumnName("vt_vote_bal_lg_year");
                entity.Property(e => e.IsCarryForward).HasDefaultValue(false).HasColumnName("vt_vote_bal_lg_is_carry_forward");
                entity.Property(e => e.Month).HasColumnName("vt_vote_bal_lg_month");
                entity.Property(e => e.Status).HasColumnName("vt_vote_bal_lg_status");


                entity.Property(e => e.EstimatedIncome).HasColumnName("vt_vote_bal_lg_estimated_income_amount").HasDefaultValue(0m);

                entity.Property(e => e.AllocationAmount).HasColumnName("vt_vote_bal_lg_allocation_amount").HasDefaultValue(0m);
                entity.Property(e => e.TakeHoldRate).HasColumnName("vt_vote_bal_lg_take_hold_rate");
                entity.Property(e => e.TakeHoldAmount).HasColumnName("vt_vote_bal_lg_take_hold_amount");
                entity.Property(e => e.AllocationExchangeAmount).HasColumnName("vt_vote_bal_lg_allocation_exchange_amount").HasDefaultValue(0m);
                entity.Property(e => e.TotalHold).HasColumnName("vt_vote_bal_lg_total_hold").HasDefaultValue(0m);
                entity.Property(e => e.TotalCommitted).HasColumnName("vt_vote_bal_lg_total_commited").HasDefaultValue(0m);
                entity.Property(e => e.TotalPending).HasColumnName("vt_vote_bal_total_lg_pending").HasDefaultValue(0m);

                entity.Property(e => e.Debit).HasColumnName("vt_vote_bal_lg_total_debit").HasDefaultValue(0m);
                entity.Property(e => e.Credit).HasColumnName("vt_vote_bal_lg_total_credit").HasDefaultValue(0m);

                entity.Property(e => e.CustomVoteDebit).HasColumnName("vt_vote_bal_lg_cstm_vt_total_debit").HasDefaultValue(0m);
                entity.Property(e => e.CustomVoteCredit).HasColumnName("vt_vote_bal_lg_cstm_vt_total_credit").HasDefaultValue(0m);

                entity.Property(e => e.AppCategory).HasColumnName("vt_vote_bal_lg_app_category");
                entity.Property(e => e.ModulePrimaryKey).HasColumnName("vt_vote_bal_lg_module_primary_key");

                entity.Property(e => e.TransactionType).HasColumnName("vt_vote_bal_lg_transaction_type");
                entity.Property(e => e.Code).HasColumnName("vt_vote_bal_lg_code");
                entity.Property(e => e.SubCode).HasColumnName("vt_vote_bal_lg_sub_code");
                entity.Property(e => e.Description).HasColumnName("vt_vote_bal_lg_description");
                entity.Property(e => e.CreditDebitRunningBalance).HasColumnName("vt_vote_bal_lg_credit_debit_balance").HasDefaultValue(0m);
                entity.Property(e => e.RunningBalance).HasColumnName("vt_vote_bal_lg_running_balance");
                entity.Property(e => e.ExchangedAmount).HasColumnName("vt_vote_bal_lg_exchange_amount").HasDefaultValue(0m);

                entity.Property(e => e.CarryForwardCredit).HasColumnName("vt_vote_bal_lg_carry_forward_total_credit").HasDefaultValue(0m);
                entity.Property(e => e.CarryForwardDebit).HasColumnName("vt_vote_bal_lg_carry_forward_total_debit").HasDefaultValue(0m);

                entity.Property(e => e.CreditDebitCarryForwardRunningBalance).HasColumnName("vt_vote_bal_lg_credit_debit_carry_forward_balance").HasDefaultValue(0m);

                entity.Property(e => e.SurchargeCredit).HasColumnName("vt_vote_bal_lg_surcharge_total_credit").HasDefaultValue(0m);
                entity.Property(e => e.SurchargeDebit).HasColumnName("vt_vote_bal_lg_surcharge_total_debit").HasDefaultValue(0m);

                entity.Property(e => e.CreditDebitSurchargeRunningBalance).HasColumnName("vt_vote_bal_lg_credit_debit_surcharge_balance").HasDefaultValue(0m);

                entity.HasOne(va => va.VoteBalance)
                    .WithMany(vd => vd.VoteBalanceLogs)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(va => va.VoteBalanceId);

                entity.Property(e => e.UpdatedAt)
                  .HasColumnType("datetime")
                  .HasColumnName("updated_at");
                entity.Property(e => e.OfficeId).HasColumnName("office_id");
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");
                entity.Property(e => e.SessionIdByOffice).HasColumnName("session_id_by_office");
                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();
            });


            modelBuilder.Entity<VoteBalanceActionLog>(entity =>
            {
                entity.ToTable("vt_vote_balance_action_log");
                entity.HasCharSet("utf8mb4")
                   .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.VoteBalanceId).HasColumnName("vote_balance_id");
                entity.Property(e => e.ActionState).HasColumnName("action_state");
                entity.Property(e => e.ActionBy).HasColumnName("action_by");
                entity.Property(e => e.Comment).HasColumnName("comment");
                entity.Property(e => e.ActionDateTime).HasColumnName("action_date");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");

                entity.HasOne(st => st.VoteBalance)
                    .WithMany(wd => wd.ActionLogs)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(st => st.VoteBalanceId);



            });

            
            modelBuilder.Entity<CustomVoteBalance>(static entity =>
            {
                entity.ToTable("vt_custom_vote_balance");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("vt_cstm_vote_bal_id");
                entity.Property(e => e.SabhaId).HasColumnName("vt_cstm_vote_bal_sabha_id");
                entity.Property(e => e.CustomVoteDetailId).HasColumnName("vt_cstm_vote_bal__csmtvote_id");
                entity.Property(e => e.CustomVoteDetailIdParentId).HasColumnName("vt_cstm_vote_bal_csmt_vote_detail_parent_id");
                entity.Property(e => e.ClassificationId).HasColumnName("vt_cstm_vote_bal_classification_id");
                entity.Property(e => e.ParentId).HasColumnName("vt_cstm_vote_bal_parent_id");
                entity.Property(e => e.Depth).HasColumnName("vt_cstm_vote_bal_depth");
                entity.Property(e => e.IsSubLevel).HasColumnName("vt_cstm_vote_bal_is_sublevel");
                entity.Property(e => e.Year).HasColumnName("vt_cstm_vote_bal_year");
                entity.Property(e => e.IsCarryForward).HasDefaultValue(false).HasColumnName("vt_cstm_vote_bal_is_carry_forward");
                entity.Property(e => e.Month).HasColumnName("vt_cstm_vote_bal_month");
                entity.Property(e => e.Status).HasColumnName("vt_cstm_vote_bal_status");


                entity.Property(e => e.EstimatedIncome).HasColumnName("vt_cstm_vote_bal_estimated_income_amount").HasDefaultValue(0m);

                entity.Property(e => e.AllocationAmount).HasColumnName("vt_cstm_vote_bal_allocation_amount");
                entity.Property(e => e.TakeHoldRate).HasColumnName("vt_cstm_vote_bal_take_hold_rate");
                entity.Property(e => e.TakeHoldAmount).HasColumnName("vt_cstm_vote_bal_take_hold_amount");
                //entity.Property(e => e.AllocationExchangeAmount).HasColumnName("vt_vote_bal_allocation_exchange_amount");
                entity.Property(e => e.TotalHold).HasColumnName("vt_cstm_vote_bal_total_hold").HasDefaultValue(0m);
                entity.Property(e => e.Debit).HasColumnName("vt_cstm_vote_bal_debit").HasDefaultValue(0m);
                entity.Property(e => e.Credit).HasColumnName("vt_cstm_vote_bal_credit").HasDefaultValue(0m);
                entity.Property(e => e.ChildrenDebit).HasColumnName("vt_cstm_vote_bal_children_debit").HasDefaultValue(0m);
                entity.Property(e => e.ChildrenCredit).HasColumnName("vt_cstm_vote_bal_children_credit").HasDefaultValue(0m);
                entity.Property(e => e.TotalCommitted).HasColumnName("vt_cstm_vote_bal_total_commited").HasDefaultValue(0m);
                entity.Property(e => e.TotalPending).HasColumnName("vt_cstm_vote_bal_total_pending").HasDefaultValue(0m);

                //entity.Property(e => e.ExchangedAmount).HasColumnName("vt_vote_bal_exchange_amount").HasDefaultValue(0m);
                entity.Property(e => e.CreditDebitRunningBalance).HasColumnName("vt_cstm_vote_bal_credit_debit_balance").HasDefaultValue(0m);
                entity.Property(e => e.RunningBalance).HasColumnName("vt_cstm_vote_bal_running_balance").HasDefaultValue(0m);
                entity.Property(e => e.TransactionType).HasColumnName("vt_cstm_vote_bal_transaction_type");
                //entity.Property(e => e.Code).HasColumnName("vt_vote_bal_code");
                //entity.Property(e => e.SubCode).HasColumnName("vt_vote_bal_sub_code");

                entity.Property(e => e.CarryForwardCredit).HasColumnName("vt_cstm_vote_bal_carry_forward_total_credit").HasDefaultValue(0m);
                entity.Property(e => e.CarryForwardDebit).HasColumnName("vt_cstm_vote_bal_carry_forward_total_debit").HasDefaultValue(0m);

                entity.Property(e => e.CreditDebitCarryForwardRunningBalance).HasColumnName("vt_cstm_vote_bal_credit_debit_carry_forward_balance").HasDefaultValue(0m);

                entity.Property(e => e.SurchargeCredit).HasColumnName("vt_cstm_vote_bal_surcharge_total_credit").HasDefaultValue(0m);
                entity.Property(e => e.SurchargeDebit).HasColumnName("vt_cstm_vote_bal_surcharge_total_debit").HasDefaultValue(0m);

                entity.Property(e => e.CreditDebitSurchargeRunningBalance).HasColumnName("vt_cstm_vote_bal_credit_debit_surcharge_balance").HasDefaultValue(0m);

                entity.Property(e => e.TransferFlag).HasColumnName("vt_cstm_vote_transfer_flag").HasDefaultValue(VoteTransferFlag.Nothing);


                entity.HasIndex(e => new { e.CustomVoteDetailId, e.Year, e.Status }).IsUnique();

                entity.Ignore(e => e.VoteAssignmentDetails);

                //entity.HasOne(va => va.VoteAssignmentDetails)
                //    .WithMany(vd => vd.CustomVoteBalance)
                //    .OnDelete(DeleteBehavior.Restrict)
                //    .HasForeignKey(va => va.CustomVoteId);


                entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("updated_at");

                entity.Property(e => e.OfficeId).HasColumnName("office_id");
                entity.Property(e => e.SessionIdByOffice).HasColumnName("session_id_by_office");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();


            });

            //Custom_vote_balance_log
            modelBuilder.Entity<CustomVoteBalanceLog>(entity =>
            {
                entity.ToTable("vt_custom_vote_balance_log");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("vt_cstm_vote_bal_lg_id");
                entity.Property(e => e.CustomVoteBalanceId).HasColumnName("vt_cstm_vote_bal_lg_csmt_vote_bal_id");
                entity.Property(e => e.SabhaId).HasColumnName("vt_cstm_vote_bal_lg_sabha_id");
                entity.Property(e => e.CustomVoteDetailId).HasColumnName("vt_cstm_cus_vote_bal_lg_csmt_vote_id");
                entity.Property(e => e.CustomVoteDetailIdParentId).HasColumnName("vt_cstm_vote_bal_lg_csmt_vote_detail_parent_id");
                entity.Property(e => e.ParentId).HasColumnName("vt_cstm_vote_bal_lg_parent_id");
                entity.Property(e => e.Depth).HasColumnName("vt_cstm_vote_bal_lg_depth");
                entity.Property(e => e.IsSubLevel).HasColumnName("vt_cstm_vote_bal_lg_is_sublevel");
                entity.Property(e => e.Year).HasColumnName("vt_cstm_vote_bal_lg_year");
                entity.Property(e => e.IsCarryForward).HasDefaultValue(false).HasColumnName("vt_cstm_vote_bal_lg_is_carry_forward");
                entity.Property(e => e.Month).HasColumnName("vt_cstm_vote_bal_lg_month");
                entity.Property(e => e.Status).HasColumnName("vt_cstm_vote_bal_lg_status");


                entity.Property(e => e.EstimatedIncome).HasColumnName("vt_cstm_vote_bal_lg_estimated_income_amount").HasDefaultValue(0m);

                entity.Property(e => e.AllocationAmount).HasColumnName("vt_cstm_vote_bal_lg_allocation_amount").HasDefaultValue(0m);
                entity.Property(e => e.TakeHoldRate).HasColumnName("vt_cstm_vote_bal_lg_take_hold_rate");
                entity.Property(e => e.TakeHoldAmount).HasColumnName("vt_cstm_vote_bal_lg_take_hold_amount");
                entity.Property(e => e.AllocationExchangeAmount).HasColumnName("vt_cstm_vote_bal_lg_allocation_exchange_amount").HasDefaultValue(0m);
                entity.Property(e => e.TotalHold).HasColumnName("vt_cstm_vote_bal_lg_total_hold").HasDefaultValue(0m);
                entity.Property(e => e.TotalCommitted).HasColumnName("vt_cstm_vote_bal_lg_total_commited").HasDefaultValue(0m);
                entity.Property(e => e.TotalPending).HasColumnName("vt_cstm_vote_bal_total_lg_pending").HasDefaultValue(0m);

                entity.Property(e => e.Debit).HasColumnName("vt_cstm_vote_bal_lg_total_debit").HasDefaultValue(0m);
                entity.Property(e => e.Credit).HasColumnName("vt_cstm_vote_bal_lg_total_credit").HasDefaultValue(0m);
                entity.Property(e => e.ChildrenDebit).HasColumnName("vt_cstm_vote_bal_lg_children_total_debit").HasDefaultValue(0m);
                entity.Property(e => e.ChildrenCredit).HasColumnName("vt_cstm_vote_bal_lg_children_total_credit").HasDefaultValue(0m);

                entity.Property(e => e.AppCategory).HasColumnName("vt_cstm_vote_bal_lg_app_category");
                entity.Property(e => e.ModulePrimaryKey).HasColumnName("vt_cstm_vote_bal_lg_module_primary_key");

                entity.Property(e => e.TransactionType).HasColumnName("vt_cstm_vote_bal_lg_transaction_type");
                entity.Property(e => e.Code).HasColumnName("vt_cstm_vote_bal_lg_code");
                entity.Property(e => e.SubCode).HasColumnName("vt_cstm_vote_bal_lg_sub_code");
                entity.Property(e => e.Description).HasColumnName("vt_cstm_vote_bal_lg_description");
                entity.Property(e => e.CreditDebitRunningBalance).HasColumnName("vt_cstm_vote_bal_lg_credit_debit_balance").HasDefaultValue(0m);
                entity.Property(e => e.RunningBalance).HasColumnName("vt_cstm_vote_bal_lg_running_balance");
                entity.Property(e => e.ExchangedAmount).HasColumnName("vt_cstm_vote_bal_lg_exchange_amount").HasDefaultValue(0m);

                entity.Property(e => e.CarryForwardCredit).HasColumnName("vt_cstm_vote_bal_lg_carry_forward_total_credit").HasDefaultValue(0m);
                entity.Property(e => e.CarryForwardDebit).HasColumnName("vt_cstm_vote_bal_lg_carry_forward_total_debit").HasDefaultValue(0m);

                entity.Property(e => e.CreditDebitCarryForwardRunningBalance).HasColumnName("vt_cstm_vote_bal_lg_credit_debit_carry_forward_balance").HasDefaultValue(0m);

                entity.Property(e => e.SurchargeCredit).HasColumnName("vt_cstm_vote_bal_lg_surcharge_total_credit").HasDefaultValue(0m);
                entity.Property(e => e.SurchargeDebit).HasColumnName("vt_cstm_vote_bal_lg_surcharge_total_debit").HasDefaultValue(0m);

                entity.Property(e => e.CreditDebitSurchargeRunningBalance).HasColumnName("vt_cstm_vote_bal_lg_credit_debit_surcharge_balance").HasDefaultValue(0m);

                entity.HasOne(va => va.CustomVoteBalance)
                    .WithMany(vd => vd.CustomVoteBalanceLog)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(va => va.CustomVoteBalanceId);

                entity.Property(e => e.UpdatedAt)
                  .HasColumnType("datetime")
                  .HasColumnName("updated_at");
                entity.Property(e => e.OfficeId).HasColumnName("office_id");
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");
                entity.Property(e => e.SessionIdByOffice).HasColumnName("session_id_by_office");
                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();
            });


            modelBuilder.Entity<CustomVoteBalanceActionLog>(entity =>
            {
                entity.ToTable("vt_custom_vote_balance_action_log");
                entity.HasCharSet("utf8mb4")
                   .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CustomVoteBalanceId).HasColumnName("cstm_vote_balance_id");
                entity.Property(e => e.ActionState).HasColumnName("action_state");
                entity.Property(e => e.ActionBy).HasColumnName("action_by");
                entity.Property(e => e.Comment).HasColumnName("comment");
                entity.Property(e => e.ActionDateTime).HasColumnName("action_date");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");

                entity.HasOne(st => st.CustomVoteBalance)
                    .WithMany(wd => wd.CustomVoteBalanceActionLog)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(st => st.CustomVoteBalanceId);

            });



            
            //modelBuilder.Entity<CustomVoteEntry>(entity =>
            //{
            //    entity.ToTable("vt_custom_vote_entries");

            //    entity.HasCharSet("utf8mb4")
            //        .UseCollation("utf8mb4_sinhala_ci");

            //    entity.Property(e => e.Id).HasColumnName("vt_cstm_vote_entry_id");
            //    entity.Property(e => e.CustomVoteDetailId).HasColumnName("vt_cstm_vote_entry_csmt_vote_id");
            //    entity.Property(e => e.CustomVoteDetailIdParentId).HasColumnName("vt_cstm_vote_entry_csmt_vote_detail_parent_id");
            //    entity.Property(e => e.ParentId).HasColumnName("vt_cstm_vote_entry_parent_id");
            //    entity.Property(e => e.Depth).HasColumnName("vt_cstm_vote_entry_depth");
            //    entity.Property(e => e.IsSubLevel).HasColumnName("vt_cstm_vote_entry_is_sublevel");
            //    entity.Property(e => e.Status).HasColumnName("vt_cstm_vote_entry_status");

            //    entity.Property(e => e.Amount).HasColumnName("vt_cstm_vote_entry_total_credit").HasDefaultValue(0m);

            //    entity.Property(e => e.EntityType).HasColumnName("vt_cstm_vote_entry_entity_type");
            //    entity.Property(e => e.EntityPrimaryId).HasColumnName("vt_cstm_vote_entry_entity_primary_id");


            //    entity.Property(e => e.CreatedBy).HasColumnName("create_by");
            //    entity.Property(e => e.CreatedAt).HasColumnName("create_at");
            //    entity.Property(e => e.UpdatedBy).HasColumnName("update_by");
            //    entity.Property(e => e.UpdatedAt).HasColumnName("update_at");
            //    entity.Property(e => e.SystemCreateAt).HasColumnName("system_create_at");
            //    entity.Property(e => e.SystemUpdateAt).HasColumnName("system_update_at");

            //    entity.Property(e => e.RowVersion).HasColumnName("row_version");
            //    entity.Property(e => e.RowVersion).IsConcurrencyToken();
            //});



            modelBuilder.Entity<Programme>(entity =>
            {
                entity.ToTable("vt_programme");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID).HasColumnName("vt_programme_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("vt_programme_code");

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("vt_programme_name_english");

                entity.Property(e => e.NameSinhala)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("vt_programme_name_sinhala");

                entity.Property(e => e.NameTamil)
                    .HasMaxLength(255)
                    .HasColumnName("vt_programme_name_tamil");

                entity.Property(e => e.SabhaID).HasColumnName("vt_programme_sabha_id");

                entity.Property(e => e.Status).HasColumnName("vt_programme_status");
            });

            modelBuilder.Entity<VoteDetail>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("PRIMARY");

                entity.ToTable("vt_vote_details");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID).HasColumnName("vt_d_id");

                entity.Property(e => e.IncomeOrExpense).HasColumnName("vt_d_income_or_expense");
                entity.Property(e => e.SubLedgerId).HasColumnName("vt_d_sub_ledger_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_programme_code");

                entity.Property(e => e.ProgrammeID).HasColumnName("vt_d_programme_id");

                entity.Property(e => e.ProgrammeNameEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_programme_name_english");

                entity.Property(e => e.ProgrammeNameSinhala)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_programme_name_sinhala");

                entity.Property(e => e.ProgrammeNameTamil)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_programme_name_tamil");

                entity.Property(e => e.ProjectCode)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_project_code");

                entity.Property(e => e.ProjectID).HasColumnName("vt_d_project_id");

                entity.Property(e => e.ProjectNameEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_project_name_english");

                entity.Property(e => e.ProjectNameSinhala)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_project_name_sinhala");

                entity.Property(e => e.ProjectNameTamil)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_project_name_tamil");

                entity.Property(e => e.SabhaCode)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_sabha_code");

                entity.Property(e => e.SabhaID).HasColumnName("vt_d_sabha_id");

                entity.Property(e => e.Status).HasColumnName("vt_d_status");

                entity.Property(e => e.SubprojectCode)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_subproject_code");

                entity.Property(e => e.SubprojectID).HasColumnName("vt_d_subproject_id");

                entity.Property(e => e.SubprojectNameEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_subproject_name_english");

                entity.Property(e => e.SubprojectNameSinhala)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_subproject_name_sinhala");

                entity.Property(e => e.SubprojectNameTamil)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_subproject_name_tamil");

                entity.Property(e => e.ClassificationID).HasColumnName("vt_d_classsification_id");

                entity.Property(e => e.ClasssificationDescription)
                        .HasMaxLength(255)
                        .HasColumnName("vt_d_classsification_desc");

                entity.Property(e => e.MainLedgerAccountID).HasColumnName("vt_d_main_ledger_acct_id");

                entity.Property(e => e.MainLedgerAccountDescription)
                        .HasMaxLength(255)
                        .HasColumnName("vt_d_main_ledger_acct_desc");

                entity.Property(e => e.IncomeTitleCode)
                   .HasMaxLength(255)
                   .HasColumnName("vt_d_title_code");

                entity.Property(e => e.IncomeTitleID).HasColumnName("vt_d_title_id");

                entity.Property(e => e.IncomeTitleNameEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_title_name_english");

                entity.Property(e => e.IncomeTitleNameSinhala)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_title_name_sinhala");

                entity.Property(e => e.IncomeTitleNameTamil)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_title_name_tamil");

                entity.Property(e => e.IncomeSubtitleCode)
                            .HasMaxLength(255)
                            .HasColumnName("vt_d_subtitle_code");

                entity.Property(e => e.IncomeSubtitleID).HasColumnName("vt_d_subtitle_id");

                entity.Property(e => e.IncomeSubtitleNameEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_subtitle_name_english");

                entity.Property(e => e.IncomeSubtitleNameSinhala)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_subtitle_name_sinhala");

                entity.Property(e => e.IncomeSubtitleNameTamil)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_subtitle_name_tamil");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_vote_code");

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_vote_name_english");

                entity.Property(e => e.NameSinhala)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_vote_name_sinhala");

                entity.Property(e => e.NameTamil)
                    .HasMaxLength(255)
                    .HasColumnName("vt_d_vote_name_tamil");

                entity.Property(e => e.VoteOrBal).HasColumnName("vt_d_vote_or_bal");

                entity.Property(e => e.VoteOrder).HasColumnName("vt_d_vote_order");

                entity.Property(e => e.CreatedBy).HasColumnName("vt_d_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("vt_d_updated_by");
                entity.Property(e => e.CreatedAt).HasColumnName("vt_d_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").ValueGeneratedOnAddOrUpdate().HasColumnName("vt_d_updated_at");

                // entity.HasMany(va => va.VoteAllocations)
                //     .WithOne(vd => vd.VoteDetail)
                //     .HasForeignKey(vd => vd.ID);

            });

            modelBuilder.Entity<BusinessNature>(entity =>
            {
                entity.ToTable("trade_tax_business_natures");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID).HasColumnName("business_nature_id");

                entity.Property(e => e.BusinessNatureName)
                    .HasColumnName("business_nature_name");

                entity.Property(e => e.ActiveStatus).HasColumnName("active_status");

                entity.Property(e => e.SabhaID).HasColumnName("sabha_id");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.CreatedAt).HasColumnName("business_nature_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").ValueGeneratedOnAddOrUpdate().HasColumnName("business_nature_updated_at");
            });


            modelBuilder.Entity<BusinessSubNature>(entity =>
            {
                entity.ToTable("trade_tax_business_subnatures");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID).HasColumnName("business_subnature_id");

                //entity.HasIndex(e => e.BusinessNatureID, "fk_business_subnature_business_nature_id");

                entity.Property(e => e.BusinessSubNatureName)
                    .HasColumnName("business_subnature_name");

                entity.Property(e => e.BusinessSubNatureStatus)
                    .HasColumnName("business_subnature_status");

                entity.Property(e => e.SabhaID).HasColumnName("sabha_id");
                entity.Property(e => e.BusinessNatureID).HasColumnName("business_nature_id");

                entity.Property(e => e.ActiveStatus).HasColumnName("active_status");

                entity.Property(e => e.OtherCharges).HasColumnName("other_charges");

                entity.Property(e => e.TaxAmount).HasColumnName("tax_amount");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();

                entity.HasOne(d => d.businessNature)
                    .WithMany(p => p.businessSubNatures)
                    .HasForeignKey(d => d.BusinessNatureID)
                    .HasConstraintName("fk_business_subnature_business_nature_id");
            });


            modelBuilder.Entity<TaxValue>(entity =>
            {
                entity.ToTable("trade_tax_tax_values");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID).HasColumnName("tax_value_id");

                entity.Property(e => e.SabhaID).HasColumnName("sabha_id");

                entity.Property(e => e.TaxTypeID).HasColumnName("tax_type_id");

                entity.Property(e => e.ActiveStatus).HasColumnName("active_status");

                entity.Property(e => e.AnnualValueMinimum).HasColumnName("annual_value_minimum");

                entity.Property(e => e.AnnualValueMaximum).HasColumnName("annual_value_maximum");

                entity.Property(e => e.TaxAmount).HasColumnName("tax_value");

                entity.Property(e => e.TaxValueStatus).HasColumnName("tax_value_status");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            });

            modelBuilder.Entity<TradeTaxVoteAssignment>(entity =>
            {
                entity.ToTable("trade_tax_vote_assignments");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID).HasColumnName("id");

                entity.Property(e => e.ActiveStatus).HasColumnName("active_status");

                entity.Property(e => e.SabhaID).HasColumnName("sabha_id");

                entity.Property(e => e.TaxTypeID).HasColumnName("tax_type_id");

                entity.Property(e => e.VoteAssignmentDetailID).HasColumnName("vote_assignement_detail_id");
                //entity.Property(e => e.BankAccountID).HasColumnName("bank_account_id");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                entity.HasIndex(e => new { e.SabhaID, e.TaxTypeID }).IsUnique(true);
            });

            modelBuilder.Entity<Commitment>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_commitment");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.BankId).HasColumnName("bank_id");
                entity.Property(e => e.PayeeCategory).HasColumnName("payee_category");
                entity.Property(e => e.PayeeName).HasColumnName("payee_name");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.CommitmentSequenceNumber).HasColumnName("commitment_sequence_number");
                entity.Property(e => e.ActionState).HasColumnName("action_state");
                entity.Property(e => e.HasVoucher).HasDefaultValue(false).HasColumnName("has_voucher");
                entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");
                entity.Property(e => e.SabhaId).HasColumnName("sabha_id");
                entity.Property(e => e.SessionId).HasColumnName("session_id");
                entity.Property(e => e.Year).HasColumnName("year");
                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.TotalAmount).HasColumnName("total_amount");

                entity.HasMany(e => e.CommitmentLine)
                    .WithOne(e => e.Commitment)
                    .HasForeignKey(e => e.Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.CommitmentLog)
                     .WithOne(e => e.Commitment)
                     .HasForeignKey(e => e.Id)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.ActionLog)
                     .WithOne(e => e.Commitment)
                     .HasForeignKey(e => e.Id)
                     .OnDelete(DeleteBehavior.Restrict);



                // mandatory fields for entity

                entity.Property(e => e.RowStatus).HasDefaultValue(1).HasColumnName("row_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.SystemCreateAt).HasColumnName("system_create_at");
                entity.Property(e => e.SystemUpdateAt).HasColumnName("system_update_at");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();

            });


            modelBuilder.Entity<CommitmentLine>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_commitment_line");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CommitmentId).HasColumnName("commitment_id");
                entity.Property(e => e.Comment).HasColumnName("comment");
                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.RowStatus).HasDefaultValue(1).HasColumnName("row_status");


                entity.HasOne(st => st.Commitment)
                    .WithMany(wd => wd.CommitmentLine)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasForeignKey(st => st.CommitmentId);


                entity.HasMany(e => e.CommitmentLineVotes)
                    .WithOne(e => e.CommitmentLine)
                    .HasForeignKey(e => e.Id)
                    .OnDelete(DeleteBehavior.Restrict); 

            });


            modelBuilder.Entity<CommitmentActionsLog>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_commitment_action_log");
                entity.HasCharSet("utf8mb4")
                   .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CommitmentId).HasColumnName("commitment_id");
                entity.Property(e => e.ActionState).HasColumnName("action_state");
                entity.Property(e => e.ActionBy).HasColumnName("action_by");
                entity.Property(e => e.Comment).HasColumnName("comment");
                entity.Property(e => e.ActionDateTime).HasColumnName("action_date");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");


                entity.HasOne(st => st.Commitment)
                    .WithMany(wd => wd.ActionLog)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(st => st.CommitmentId);



            });

            modelBuilder.Entity<CommitmentLineVotes>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_commitmentline_votes");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.VoteId).HasColumnName("vote_id");
                entity.Property(e => e.Comment).HasColumnName("comment");
                entity.Property(e => e.VoteAllocationId).HasColumnName("vote_allacation_id");
                entity.Property(e => e.VoteCode).HasColumnName("vote_code");
                entity.Property(e => e.CommitmentLineId).HasColumnName("commitment_line_id");
                entity.Property(e => e.Amount).HasColumnName("amount");
                entity.Property(e => e.RemainingAmount).HasColumnName("remaining").HasDefaultValue(0.00);
                entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");


                entity.Property(e => e.RowStatus).HasDefaultValue(1).HasColumnName("row_status");

                entity.HasOne(cl => cl.CommitmentLine)
                    .WithMany(clv => clv.CommitmentLineVotes)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(cl => cl.CommitmentLineId);
            });

            //modelBuilder.Entity<CommitmentLineCustomVotes>(entity =>
            //{
            //    entity.ToTable("vt_lgr_pmnt_commitmentline_custom_votes");

            //    entity.HasKey(e => e.Id);
            //    entity.Property(e => e.Id).HasColumnName("id");
            //    entity.Property(e => e.CommitmentLineVotesLineId).HasColumnName("commitment_vote_line_id");
            //    entity.Property(e => e.CustomVoteDetailId).HasColumnName("custom_vote_detail_id");
            //    entity.Property(e => e.CustomVoteDetailIdParentId).HasColumnName("custom_vote_detail_parent_id");
            //    entity.Property(e => e.ParentId).HasColumnName("parent_id");
            //    entity.Property(e => e.Depth).HasColumnName("depth");
            //    entity.Property(e => e.IsSubLevel).HasColumnName("is_sublevel");

            //    entity.Property(e => e.Amount).HasColumnName("amount");
            //    entity.Property(e => e.RemainingAmount).HasColumnName("remaining").HasDefaultValue(0.00);
            //    entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");


            //    entity.Property(e => e.RowStatus).HasDefaultValue(1).HasColumnName("row_status");

            //    entity.HasOne(cl => cl.CommitmentLineVotesLine)
            //        .WithMany(clv => clv.CommitmentLineCustomVotes)
            //        .OnDelete(DeleteBehavior.Restrict)
            //        .HasForeignKey(cl => cl.CommitmentLineVotesLineId);
            //});


            modelBuilder.Entity<CommitmentLog>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_commitment_log");
                entity.HasCharSet("utf8mb4")
                   .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CommitmentId).HasColumnName("commitment_id");
                entity.Property(e => e.PayeeId).HasColumnName("payee_id");
                entity.Property(e => e.PayeeName).HasColumnName("payee_name");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.CommitmentSequenceNumber).HasColumnName("commitment_sequence_number");
                entity.Property(e => e.ActionState).HasColumnName("action_state");
                entity.Property(e => e.HasVoucher).HasDefaultValue(false).HasColumnName("has_voucher");
                entity.Property(e => e.sabhaId).HasColumnName("sabha_id");
                entity.Property(e => e.SessionId).HasColumnName("session_id");
                entity.Property(e => e.Year).HasColumnName("year");
                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.TotalAmount).HasColumnName("total_amount");
                entity.Property(e => e.SystemUpdateAt).HasColumnName("system_update_at");

                entity.HasOne(st => st.Commitment)
                   .WithMany(wd => wd.CommitmentLog)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasForeignKey(st => st.CommitmentId);


                entity.HasMany(e => e.CommitmentLineLog)
                    .WithOne(e => e.CommitmentLog)
                    .HasForeignKey(e => e.CommitmentLogId)
                    .OnDelete(DeleteBehavior.Restrict);

        
                // mandatory fields for entity

                entity.Property(e => e.RowStatus).HasDefaultValue(1).HasColumnName("row_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                //entity.Property(e => e.SystemCreateAt).HasColumnName("system_create_at");
                entity.Property(e => e.SystemUpdateAt).HasColumnName("system_update_at");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();



            });


            modelBuilder.Entity<CommitmentLineLog>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_commitment_line_log");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CommitmentLineId).HasColumnName("commitment_line_id");

                entity.Property(e => e.CommitmentLogId).HasColumnName("commitment_log_id");
                entity.Property(e => e.Comment).HasColumnName("comment");
                entity.Property(e => e.Amount).HasColumnName("amount");


                entity.HasOne(st => st.CommitmentLog)
                  .WithMany(wd => wd.CommitmentLineLog)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasForeignKey(st => st.CommitmentLogId);


                //entity.HasOne(st => st.CommitmentLog)
                //    .WithMany(wd => wd.CommitmentLineLog)
                //    .OnDelete(DeleteBehavior.Restrict)

                //    .HasForeignKey(st => st.CommitmentId);
                //// .HasConstraintName("fk_commitmentId_Id");


                entity.HasMany(e => e.CommitmentLineVotesLog)
                    .WithOne(e => e.CommitmentLineLog)
                    .HasForeignKey(e => e.Id);

            });



            modelBuilder.Entity<CommitmentLineVotesLog>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_commitmentline_votes_log");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");    

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CommitmentLineVoteId).HasColumnName("commitment_line_vote_id");
                entity.Property(e => e.VoteId).HasColumnName("vote_id");
                entity.Property(e => e.VoteAllocationId).HasColumnName("vote_allacation_id");
                entity.Property(e => e.VoteCode).HasColumnName("vote_code");
                entity.Property(e => e.CommitmentLineLogId).HasColumnName("commitment_line_log_id");
                entity.Property(e => e.Amount).HasColumnName("amount");
                entity.Property(e => e.RemainingAmount).HasColumnName("remaining").HasDefaultValue(0.00);
                entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");


                entity.HasOne(cl => cl.CommitmentLineLog)
                    .WithMany(clv => clv.CommitmentLineVotesLog)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(cl => cl.CommitmentLineLogId);
                // .HasConstraintName("fk_commitmentLineId_Id");



            });


            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_voucher");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.PayeeCategory).HasColumnName("payee_category");
                entity.Property(e => e.CommentOrDescription).HasColumnName("comment_or_description");
                entity.Property(e => e.CommitmentId).HasColumnName("commitment_id");
                entity.Property(e => e.SubImprestId).HasColumnName("sub_imprest_id");
                entity.Property(e => e.SettlementId).HasColumnName("settlement_id");
                entity.Property(e => e.AccountTransferId).HasColumnName("account_transfer_id");
                entity.Property(e => e.RefundId).HasColumnName("refund_id");
                entity.Property(e => e.RePaymentOrderId).HasColumnName("repayment_order_id");
                entity.Property(e => e.AdvancedBId).HasColumnName("advanced_b_id");
                entity.Property(e => e.PayeeCategory).HasColumnName("payee_category");

                entity.Property(e => e.SabhaId).HasColumnName("sabha_id");
                entity.Property(e => e.VATTotal).HasDefaultValue(0m).HasColumnName("vat_total");
                entity.Property(e => e.NBTTotal).HasDefaultValue(0m).HasColumnName("nbt_total");
                entity.Property(e => e.TotalChequeAmount).HasDefaultValue(0m).HasColumnName("total_cheque_amount");
                entity.Property(e => e.VoucherAmount).HasDefaultValue(0m).HasColumnName("voucher_amount");
                entity.Property(e => e.StampTotal).HasDefaultValue(0m).HasColumnName("stamp_total");
                entity.Property(e => e.ActionState).HasColumnName("action_state");
                entity.Property(e => e.VoucherCategory).HasColumnName("voucher_category");
                entity.Property(e => e.VoucherSequenceNumber).HasColumnName("voucher_sequence_number");


                //entity.Property(e => e.PartnerId).HasColumnName("partner_id");
                entity.Property(e => e.BankId).HasColumnName("bank_id");
                entity.Property(e => e.CrossAmount).HasDefaultValue(0m).HasColumnName("cross_amount");

                entity.Property(e => e.Year).HasColumnName("year");
                entity.Property(e => e.Month).HasColumnName("month");
                entity.Property(e => e.SessionId).HasColumnName("session_id");

                entity.Property(e => e.FileNo).HasColumnName("file_no");
                entity.Property(e => e.PreChairmanApprove).HasColumnName("pre_chairman_approve");
                entity.Property(e => e.PreCommitteeNo).HasColumnName("pre_committee_no");
                entity.Property(e => e.PreCommitteeApprove).HasColumnName("pre_committee_approve");

                entity.Property(e => e.PaymentStatus).HasDefaultValue(PaymentStatus.FinalPayment).HasColumnName("payment_status");


                entity.Property(e => e.PreCouncilNo).HasColumnName("per_council_no");
                entity.Property(e => e.PreCouncilApprove).HasColumnName("per_council_approve_date");
                entity.Property(e => e.SubjectToApprove).HasColumnName("subject_to_approve");



                entity.HasMany(v => v.VoucherLine)
                    .WithOne(vl => vl.Voucher)
                    .HasForeignKey(vl => vl.Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(v => v.VoucherLog)
                    .WithOne(vlo => vlo.Voucher)
                    .HasForeignKey(vlo => vlo.VoucherId)
                    .OnDelete(DeleteBehavior.Restrict); 

                entity.HasMany(v => v.ActionLog)
                    .WithOne(vpl => vpl.Voucher)
                    .HasForeignKey(vlo => vlo.Id)
                    .OnDelete(DeleteBehavior.Restrict); 

                //entity.HasOne(e => e.CrossSettlement)
                //    .WithOne(e => e.Voucher)
                //    .HasForeignKey<CrossSettlement>(e => e.Id)
                //    .OnDelete(DeleteBehavior.Restrict);

                // mandatory fields for entity

                entity.Property(e => e.RowStatus).HasColumnName("row_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.SystemCreateAt).HasColumnName("system_create_at");
                entity.Property(e => e.SystemUpdateAt).HasColumnName("system_update_at");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();


            });




            modelBuilder.Entity<DepositForVoucher>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_deposit_for_voucher");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id).HasName("id");
                entity.Property(e => e.VoucherId).HasColumnName("voucher_id");
                entity.Property(e => e.Amount).HasDefaultValue(0).HasColumnName("amount");
                entity.Property(e => e.DepositId).HasColumnName("deposit_id");

                  entity.HasOne(vl => vl.Voucher)
                 .WithMany(v => v.DepositsForVoucher)
                 .OnDelete(DeleteBehavior.Restrict)
                 .HasForeignKey(vl => vl.VoucherId);
            });

            modelBuilder.Entity<EmployeeLoansForVoucher>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_emp_loan_for_voucher");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id).HasName("id");
                entity.Property(e => e.VoucherId).HasColumnName("voucher_id");
                entity.Property(e => e.InstallmentAmount).HasDefaultValue(0M).HasColumnName("installment_amount");
                entity.Property(e => e.InterestAmount).HasColumnName("interest_amount");

                entity.HasOne(vl => vl.Voucher)
               .WithMany(v => v.EmployeeLoansForVoucher)
               .OnDelete(DeleteBehavior.Restrict)
               .HasForeignKey(vl => vl.VoucherId);
            });

            modelBuilder.Entity<SubVoucherItem>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_sub_voucher_item");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id).HasName("id");
                entity.Property(e => e.VoucherId).HasColumnName("voucher_id");
                entity.Property(e => e.VoucherItemAmount).HasDefaultValue(0m).HasColumnName("voucher_item_amount");
                entity.Property(e => e.PayeeId).HasColumnName("payee_id");
                entity.Property(e => e.SubVoucherNo).HasColumnName("sub_voucher_no");
                entity.Property(e => e.CommentOrDescription).HasColumnName("comment_or_description");
                entity.Property(e => e.VATTotal).HasDefaultValue(0m).HasColumnName("vat_total");
                entity.Property(e => e.NBTTotal).HasDefaultValue(0m).HasColumnName("nbt_total");
                entity.Property(e => e.ChequeAmount).HasDefaultValue(0m).HasColumnName("total_cheque_amount");
                entity.Property(e => e.VoucherItemAmount).HasDefaultValue(0m).HasColumnName("amount");
                entity.Property(e => e.Stamp).HasDefaultValue(0m).HasColumnName("stamp");

                entity.Property(e => e.CrossAmount).HasDefaultValue(0m).HasColumnName("cross_amount");

                entity.HasIndex(sv => new { sv.VoucherId, sv.SubVoucherNo })
               .IsUnique();

                entity.HasOne(vl => vl.Voucher)
               .WithMany(v => v.SubVoucherItems)
               .OnDelete(DeleteBehavior.Restrict)
               .HasForeignKey(vl => vl.VoucherId);

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();
            });



            modelBuilder.Entity<VoucherLine>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_voucher_line");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.VoucherId).HasColumnName("voucher_id");
                entity.Property(e => e.CommentOrDescription).HasColumnName("comment_or_description");
                entity.Property(e => e.CommitmentLineId).HasColumnName("commitment_line_id");
                entity.Property(e => e.VoteId).HasColumnName("vote_id");
                entity.Property(e => e.VoteCode).HasColumnName("vote_code");
                entity.Property(e => e.NetAmount).HasColumnName("amount");
                entity.Property(e => e.VAT).HasColumnName("vat");
                entity.Property(e => e.NBT).HasColumnName("nbt");
                entity.Property(e => e.TotalAmount).HasColumnName("total_amount");
                entity.Property(e => e.SurchargeAmount).HasColumnName("surcharge_amount");
                entity.Property(e => e.VoteBalanceId).HasColumnName("vote_balance_id");
               
                
                entity.Property(e => e.RptBudgetAllocation).HasColumnName("rpt_budget_allocation");
                entity.Property(e => e.RptExpenditure).HasColumnName("rpt_expenditure");
                entity.Property(e => e.RptBalance).HasColumnName("rpt_balance");


                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();

                entity.HasOne(vl => vl.Voucher)
                    .WithMany(v => v.VoucherLine)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(vl => vl.VoucherId);
                 //.HasConstraintName("fk_voucherId_id");

            });



            modelBuilder.Entity<VoucherSubLine>(entity =>
            {
                entity.ToTable("vt_lgr_voucher_sub_line");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.VoucherLineId).HasColumnName("voucher_line_id");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.NetAmount).HasColumnName("amount");
                entity.Property(e => e.VAT).HasColumnName("vat");
                entity.Property(e => e.NBT).HasColumnName("nbt");
                entity.Property(e => e.TotalAmount).HasColumnName("total_amount");


                entity.HasOne(vl => vl.VoucherLine)
                    .WithMany(v => v.VoucherSubLines)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(vl => vl.VoucherLineId)
                .HasConstraintName("fk_voucher_line_id_voucher_subline_voucher_line_id");

            });

            modelBuilder.Entity<VoucherCrossOrder>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_voucher_cross_orders");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id).HasName("id");
                entity.Property(e => e.SubVoucherItemId).HasColumnName("voucher_item_id");
                entity.Property(e => e.Amount).HasDefaultValue(0).HasColumnName("amount");
                entity.Property(e => e.CrossOrderId).HasDefaultValue(0).HasColumnName("cross_order_id");
                entity.Property(e => e.OrderType).HasColumnName("order_type");

                entity.HasOne(vl => vl.SubVoucherItem)
               .WithMany(v => v.VoucherCrossOrders)
               .OnDelete(DeleteBehavior.Restrict)
               .HasForeignKey(vl => vl.SubVoucherItemId);
            });

            modelBuilder.Entity<VoucherActionLog>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_voucher_action_log");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.VoucherId).HasColumnName("voucher_id");
                entity.Property(e => e.ActionState).HasColumnName("action_state");
                entity.Property(e => e.ActionBy).HasColumnName("action_by");
                entity.Property(e => e.Comment).HasColumnName("comment");
                entity.Property(e => e.ActionDateTime).HasColumnName("action_date");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");

                entity.HasOne(val => val.Voucher)
                    .WithMany(v => v.ActionLog)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(val => val.VoucherId);

            });

            modelBuilder.Entity<VoucherLog>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_voucher_log");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.VoucherId).HasColumnName("voucher_id");
                entity.Property(e => e.CommitmentId).HasColumnName("commitment_id");
                entity.Property(e => e.SabhaID).HasColumnName("sabha_id");
                entity.Property(e => e.VATTotal).HasColumnName("vat_total");
                entity.Property(e => e.NBTTotal).HasColumnName("nbt_total");
                entity.Property(e => e.TotalAmount).HasColumnName("total_amount");
                entity.Property(e => e.Amount).HasColumnName("amount");
                entity.Property(e => e.Stamp).HasColumnName("stamp");
                entity.Property(e => e.ActionState).HasColumnName("action_state");
                entity.Property(e => e.VoucherCategory).HasColumnName("voucher_category");
                entity.Property(e => e.VoucherSequenceNumber).HasColumnName("voucher_sequence_number");
                entity.Property(e => e.SessionId).HasColumnName("session_id");
                entity.Property(e => e.PayeeId).HasColumnName("payee_id");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");


                entity.HasMany(v => v.voucherLineLog)
                    .WithOne(vl => vl.VoucherLog)
                    .HasForeignKey(vl => vl.VoucherLogId)
                    .OnDelete(DeleteBehavior.Restrict);

                // mandatory fields for entity

                entity.Property(e => e.RowStatus).HasColumnName("row_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.SystemCreateAt).HasColumnName("system_create_at");
                entity.Property(e => e.SystemUpdateAt).HasColumnName("system_update_at");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();



            });


            modelBuilder.Entity<VoucherLineLog>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_voucher_line_log");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");

                entity.HasOne(vl => vl.VoucherLog)
                    .WithMany(v => v.voucherLineLog)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(vl => vl.VoucherLogId);
                //.HasConstraintName("fk_voucherId_id");


                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.VoucherLogId).HasColumnName("voucher_log_id");
                entity.Property(e => e.VoucherLineId).HasColumnName("voucher_line_id");
                entity.Property(e => e.CommentOrDescription).HasColumnName("comment_or_description");
                entity.Property(e => e.CommitmentLineId).HasColumnName("commitment_line_id");
                entity.Property(e => e.VoteId).HasColumnName("vote_id");
                entity.Property(e => e.VoteCode).HasColumnName("vote_code");
                entity.Property(e => e.NetAmount).HasColumnName("amount");
                entity.Property(e => e.VAT).HasColumnName("vat");
                entity.Property(e => e.NBT).HasColumnName("nbt");
                entity.Property(e => e.TotalAmount).HasColumnName("total_amount");
                entity.Property(e => e.SurchargeAmount).HasColumnName("surcharge_amount");
                entity.Property(e => e.VoteBalanceId).HasColumnName("vote_balance_id");


                entity.Property(e => e.RptBudgetAllocation).HasColumnName("rpt_budget_allocation");
                entity.Property(e => e.RptExpenditure).HasColumnName("rpt_expenditure");
                entity.Property(e => e.RptBalance).HasColumnName("rpt_balance");

            });

            modelBuilder.Entity<VoucherDocument>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_voucher_doucument");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.VoucherId).HasColumnName("voucher_id");
                entity.Property(e => e.DocType).HasColumnName("doc_type");

                entity.Property(e => e.Uri).HasColumnName("uri");


                entity.HasOne(vl => vl.Voucher)
                    .WithMany(v => v.VoucherDocuments)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(vl => vl.VoucherId);
                //.HasConstraintName("fk_voucherId_id");


                entity.Property(e => e.Status).HasColumnName("row_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });


            modelBuilder.Entity<VoucherInvoice>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_voucher_invoice");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.VoucherId).HasColumnName("voucher_id");

                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Uri).HasColumnName("uri");


                entity.HasOne(vl => vl.Voucher)
                    .WithMany(v => v.VoucherInvoices)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(vl => vl.VoucherId);
                //.HasConstraintName("fk_voucherId_id");


                entity.Property(e => e.Status).HasColumnName("row_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            });

            modelBuilder.Entity<CutProvision>(entity =>
            {
                entity.ToTable("vt_lgr_vote_cut_province");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CPNo).HasColumnName("cut_province_no");
                entity.Property(e => e.SabahId).HasColumnName("sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("office_id");
                entity.Property(e => e.VoteDetailId).HasColumnName("vote_detail_id");
                entity.Property(e => e.VoteBalanceId).HasColumnName("vote_balance_id");
                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.RequestSnapshotAllocation).HasColumnName("request_snapshot_allocation");
                entity.Property(e => e.ActionSnapshotAllocation).HasColumnName("action_snapshot_allocation");

                entity.Property(e => e.RequestSnapshotBalance).HasColumnName("request_snapshot_balance");
                entity.Property(e => e.ActionSnapshotBalance).HasColumnName("action_snapshot_balance");


                entity.Property(e => e.RequestDate).HasColumnName("requet_date");
                entity.Property(e => e.RequestBy).HasColumnName("requet_by");
                entity.Property(e => e.RequestNote).HasColumnName("requet_note");
                entity.Property(e => e.ActionDate).HasColumnName("action_date");
                entity.Property(e => e.ActionBy).HasColumnName("action_by");
                entity.Property(e => e.ActionNote).HasColumnName("action_note");

                entity.Property(e => e.SystemRequestDate).HasColumnName("requet_system_date");
                entity.Property(e => e.ActionNote).HasColumnName("action_system_date");

                // mandatory fields for entity

                entity.Property(e => e.RowStatus).HasDefaultValue(1).HasColumnName("row_status");



            });
            modelBuilder.Entity<Supplementary>(entity =>
            {
                entity.ToTable("vt_lgr_vote_supplementary");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.SPLNo).HasColumnName("cut_province_no");
                entity.Property(e => e.SabahId).HasColumnName("sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("office_id");
                entity.Property(e => e.VoteDetailId).HasColumnName("vote_detail_id");
                entity.Property(e => e.VoteBalanceId).HasColumnName("vote_balance_id");
                entity.Property(e => e.Amount).HasColumnName("from_amount");

                entity.Property(e => e.RequestSnapshotAllocation).HasColumnName("request_snapshot_allocation");
                entity.Property(e => e.ActionSnapshotAllocation).HasColumnName("action_snapshot_allocation");

                entity.Property(e => e.RequestSnapshotBalance).HasColumnName("request_snapshot_balance");
                entity.Property(e => e.ActionSnapshotBalance).HasColumnName("action_snapshot_balance");

                entity.Property(e => e.RequestDate).HasColumnName("requet_date");
                entity.Property(e => e.RequestBy).HasColumnName("requet_by");
                entity.Property(e => e.RequestNote).HasColumnName("requet_note");
                entity.Property(e => e.ActionDate).HasColumnName("action_date");
                entity.Property(e => e.ActionBy).HasColumnName("action_by");
                entity.Property(e => e.ActionNote).HasColumnName("action_note");

                entity.Property(e => e.SystemRequestDate).HasColumnName("requet_system_date");
                entity.Property(e => e.ActionNote).HasColumnName("action_system_date");

                // mandatory fields for entity

                entity.Property(e => e.RowStatus).HasDefaultValue(1).HasColumnName("row_status");

            });

            modelBuilder.Entity<VoteJournalAdjustment>(entity =>
            {
                entity.ToTable("vt_lgr_vote_journal_adjustments");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.JournalNo).HasColumnName("journal_no");
                entity.Property(e => e.SabahId).HasColumnName("sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("office_id");
                entity.Property(e => e.FromAmount).HasColumnName("from_amount");
                entity.Property(e => e.ToAmount).HasColumnName("to_amount");
                entity.Property(e => e.RequestDate).HasColumnName("requet_date");
                entity.Property(e => e.RequestBy).HasColumnName("requet_by");
                entity.Property(e => e.RequestNote).HasColumnName("requet_note");
                entity.Property(e => e.ActionDate).HasColumnName("action_date");
                entity.Property(e => e.ActionBy).HasColumnName("action_by");
                entity.Property(e => e.ActionNote).HasColumnName("action_note");
               
                entity.Property(e => e.SystemRequestDate).HasColumnName("requet_system_date");
                entity.Property(e => e.ActionNote).HasColumnName("action_system_date");

                // mandatory fields for entity

                entity.Property(e => e.RowStatus).HasDefaultValue(1).HasColumnName("row_status");



            });

            modelBuilder.Entity<VoteJournalItemFrom>(entity =>
            {
                entity.ToTable("vt_lgr_vote_journal_items_from");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.VoteJournalAdjustmentId).HasColumnName("vote_journal_adjustment_id");
                entity.Property(e => e.FromVoteBalanceId).HasColumnName("vote_journal_from_vote_balance_id");
                entity.Property(e => e.FromVoteDetailId).HasColumnName("from_vote_detail_id");
                entity.Property(e => e.FromAmount).HasColumnName("from_amount");

                entity.HasOne(val => val.VoteJournalAdjustment)
                    .WithMany(v => v.VoteJournalItemsFrom)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(val => val.VoteJournalAdjustmentId);

                // mandatory fields for entity

                entity.Property(e => e.RowStatus).HasDefaultValue(1).HasColumnName("row_status");

            });

            modelBuilder.Entity<VoteJournalItemTo>(entity =>
            {
                entity.ToTable("vt_lgr_vote_journal_items_to");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.VoteJournalAdjustmentId).HasColumnName("vote_journal_adjustment_id");
                entity.Property(e => e.ToVoteBalanceId).HasColumnName("vote_journal_to_vote_balance_id");
                entity.Property(e => e.ToVoteDetailId).HasColumnName("to_vote_detail_id");
                entity.Property(e => e.ToAmount).HasColumnName("to_amount");

                entity.HasOne(val => val.VoteJournalAdjustment)
                    .WithMany(v => v.VoteJournalItemsTo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(val => val.VoteJournalAdjustmentId);

                // mandatory fields for entity

                entity.Property(e => e.RowStatus).HasDefaultValue(1).HasColumnName("row_status");

            });

            modelBuilder.Entity<FR66Transfer>(entity =>
            {
                entity.ToTable("vt_lgr_vote_fr66_transfer");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.FR66No).HasColumnName("journal_no");
                entity.Property(e => e.SabahId).HasColumnName("sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("office_id");
                entity.Property(e => e.FromAmount).HasColumnName("from_amount");
                entity.Property(e => e.ToAmount).HasColumnName("to_amount");
                entity.Property(e => e.RequestDate).HasColumnName("requet_date");
                entity.Property(e => e.RequestBy).HasColumnName("requet_by");
                entity.Property(e => e.RequestNote).HasColumnName("requet_note");
                entity.Property(e => e.ActionDate).HasColumnName("action_date");
                entity.Property(e => e.ActionBy).HasColumnName("action_by");
                entity.Property(e => e.ActionNote).HasColumnName("action_note");

                entity.Property(e => e.SystemRequestDate).HasColumnName("requet_system_date");
                entity.Property(e => e.ActionNote).HasColumnName("action_system_date");

                // mandatory fields for entity

                entity.Property(e => e.RowStatus).HasDefaultValue(1).HasColumnName("row_status");



            });

            modelBuilder.Entity<FR66FromItem>(entity =>
            {
                entity.ToTable("vt_lgr_vote_fr66_items_from");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.FR66Id).HasColumnName("vote_fr66_id");
                entity.Property(e => e.FromVoteBalanceId).HasColumnName("vote_fr66_from_vote_balance_id");
                entity.Property(e => e.FromVoteDetailId).HasColumnName("from_vote_detail_id");
                entity.Property(e => e.FromAmount).HasColumnName("from_amount");

                entity.Property(e => e.RequestSnapshotAllocation).HasColumnName("request_snapshot_allocation");
                entity.Property(e => e.ActionSnapshotAllocation).HasColumnName("action_snapshot_allocation");

                entity.Property(e => e.RequestSnapshotBalance).HasColumnName("request_snapshot_balance");
                entity.Property(e => e.ActionSnapshotBalance).HasColumnName("action_snapshot_balance");

                entity.HasOne(val => val.FR66Transfer)
                    .WithMany(v => v.FR66FromItems)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(val => val.FR66Id);

                // mandatory fields for entity

                entity.Property(e => e.RowStatus).HasDefaultValue(1).HasColumnName("row_status");

            });

            modelBuilder.Entity<FR66ToItem>(entity =>
            {
                entity.ToTable("vt_lgr_vote_fr66_items_to");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.FR66Id).HasColumnName("vote_fr66_id");
                entity.Property(e => e.ToVoteBalanceId).HasColumnName("vote_fr66_to_vote_balance_id");
                entity.Property(e => e.ToVoteDetailId).HasColumnName("to_vote_detail_id");
                entity.Property(e => e.ToAmount).HasColumnName("to_amount");

                entity.Property(e => e.RequestSnapshotAllocation).HasColumnName("request_snapshot_allocation");
                entity.Property(e => e.ActionSnapshotAllocation).HasColumnName("action_snapshot_allocation");

                entity.Property(e => e.RequestSnapshotBalance).HasColumnName("request_snapshot_balance");
                entity.Property(e => e.ActionSnapshotBalance).HasColumnName("action_snapshot_balance");

                entity.HasOne(val => val.FR66Transfer)
                    .WithMany(v => v.FR66ToItems)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(val => val.FR66Id);

                // mandatory fields for entity

                entity.Property(e => e.RowStatus).HasDefaultValue(1).HasColumnName("row_status");

            });

            modelBuilder.Entity<VoucherCheque>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_voucher_cheque");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ChequeNo).HasColumnName("cheque_no");
                entity.Property(e => e.GrpId).HasColumnName("grp_id");
                entity.Property(e => e.BankId).HasColumnName("bank_id");
                entity.Property(e => e.PayeeCategory).HasColumnName("payee_category");
                entity.Property(e => e.PayeeName).HasColumnName("payee_name");
                entity.Property(e => e.PayeeId).HasColumnName("payee_id");
                entity.Property(e => e.Amount).HasColumnName("amount").HasColumnType("decimal(18, 2)");
                ;
                entity.Property(e => e.VoucherIdsAsString).HasColumnName("voucher_id_as_string");
                entity.Property(e => e.SabhaId).HasColumnName("sabha_id");
                entity.Property(e => e.ChequeCategory).HasColumnName("cheque_category");
                entity.Property(e => e.VoucherItemIds).HasColumnName("voucher_item_id");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.IsPrinted).HasColumnName("is_printed");

                entity.Property(e => e.RowStatus).HasDefaultValue(1).HasColumnName("row_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.SystemCreateAt).HasColumnName("system_create_at");
                entity.Property(e => e.SystemUpdateAt).HasColumnName("system_update_at");


            });

            modelBuilder.Entity<VoucherChequeLog>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_voucher_cheque_log");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.VoucherChequeId).HasColumnName("voucher_cheque_id");
                entity.Property(e => e.ChequeNo).HasColumnName("cheque_no");
                entity.Property(e => e.IsPrinted).HasColumnName("is_printed");

                entity.Property(e => e.RowStatus).HasDefaultValue(1).HasColumnName("row_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                entity.Property(e => e.SystemCreateAt).HasColumnName("system_create_at");
                entity.Property(e => e.SystemUpdateAt).HasColumnName("system_update_at");

                     entity.HasOne(val => val.VoucherCheque)
                    .WithMany(v => v.VoucherChequeLogs)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(val => val.VoucherChequeId);



            });
            modelBuilder.Entity<VoucherItemsForCheque>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_voucher_cheque_voucher_items");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.VoucherChequeId).HasColumnName("voucher_cheque_id");
                entity.Property(e => e.SubVoucherItemId).HasColumnName("sub_voucher_item_id");

                entity.HasOne(val => val.VoucherCheque)
                   .WithMany(v => v.VoucherItemsForCheque)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasForeignKey(val => val.VoucherChequeId);

            });

            modelBuilder.Entity<VoucherChequeActionLog>(entity =>
            {
                entity.ToTable("vt_lgr_pmnt_voucher_cheque_action_log");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.VoucherChequeId).HasColumnName("voucher_cheque_id");
                entity.Property(e => e.ActionState).HasColumnName("action_state");
                entity.Property(e => e.ActionBy).HasColumnName("action_by");
                entity.Property(e => e.Comment).HasColumnName("comment");
                entity.Property(e => e.ActionDateTime).HasColumnName("action_date");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");

                entity.HasOne(val => val.VoucherCheque)
                   .WithMany(v => v.ActionLogs)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasForeignKey(val => val.VoucherChequeId);

            });

            modelBuilder.Entity<DepositSubInfo>(entity =>
            {
                entity.ToTable("deposit_sub_info");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.SabhaId).HasColumnName("sabha_id");


                entity.Property(e => e.CreatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("created_at")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("status");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                entity.Property(e => e.SystemCreateAt).HasColumnName("system_craete_at");
                entity.Property(e => e.SystemUpdateAt).HasColumnName("system_update_at");

                entity.HasIndex(e => new { e.Name, e.SabhaId }).IsUnique(true);
            });

            modelBuilder.Entity<FinalAccountSequenceNumber>(entity =>
            {
                entity.ToTable("final_account_sequence_numbers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.Property(e => e.SabhaId).HasColumnName("sabha_id");

                entity.Property(e => e.ModuleType).HasColumnName("module_type");
                entity.Property(e => e.LastIndex).HasColumnName("last_index");

                entity.Property(e => e.Prefix)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("prefix");

                entity.HasIndex(e => new { e.Year, e.SabhaId,e.ModuleType }).IsUnique(true);
            });

            modelBuilder.Entity<VoteLedgerBook>(entity =>
            {
                entity.ToTable("vt_ledger_book");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");


                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.SabhaId).HasColumnName("vt_lb_sbha_id");
                entity.Property(e => e.OfiiceId).HasColumnName("vt_lb_office_id");
                entity.Property(e => e.ItemCode).HasDefaultValue('-').HasColumnName("vt_lb_item_code");
                entity.Property(e => e.Description).HasColumnName("vt_lb_description");
                entity.Property(e => e.Date).HasColumnName("vt_lb_date");
                entity.Property(e => e.Code).HasColumnName("vt_lb_code");

                entity.Property(e => e.VoteBalanceId).HasColumnName("vt_lb_vote_balance_id");
                entity.Property(e => e.VoteDetailId).HasColumnName("vt_lb_vote_detail_id");
                entity.Property(e => e.TransactionType).HasColumnName("vt_lb_tr_type");
                entity.Property(e => e.VoteBalanceTransactionTypes).HasColumnName("vt_lb_vote_bal_tr_type");
                entity.Property(e => e.Credit).HasColumnName("vt_lb_credit");
                entity.Property(e => e.Debit).HasColumnName("vt_lb_debit");


                entity.Property(e => e.RunningTotal).HasColumnName("vt_lb_rn_total");
                entity.Property(e => e.IncomeItemId).HasColumnName("vt_lb_income_item_id");
                entity.Property(e => e.ExpenseItemId).HasColumnName("vt_lb_expense_item_id");
                entity.Property(e => e.SessionId).HasColumnName("vt_lb_session_id");

                entity.Property(e => e.RowStatus).HasColumnName("row_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.SystemActionDate).HasColumnName("system_action_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                entity.Property(e => e.RowVersion).HasColumnName("row_version");



            });

            modelBuilder.Entity<CashBook>(entity =>
            {
                entity.ToTable("vt_cash_book");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");


                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.SabhaId).HasColumnName("vt_cb_sbha_id");
                entity.Property(e => e.OfiiceId).HasColumnName("vt_cb__office_id");
                entity.Property(e => e.BankAccountId).HasColumnName("vt_cb_bank_acc_id");
                entity.Property(e => e.Description).HasColumnName("vt_cb_description");
                entity.Property(e => e.Date).HasColumnName("vt_cb_date");
                entity.Property(e => e.Code).HasColumnName("vt_cb_code");
                entity.Property(e => e.SubCode).HasColumnName("vt_cb_sub_code");

                entity.Property(e => e.TransactionType).HasColumnName("vt_cb_tr_type");
                entity.Property(e => e.IncomeCategory).HasColumnName("vt_cb_income_category");
                entity.Property(e => e.ExpenseCategory).HasColumnName("vt_cb_expense_category");
               
                entity.Property(e => e.ChequeNo).HasColumnName("vt_cb_cheque_no");

                entity.Property(e => e.IncomeHeadsAsString).HasColumnName("vt_cb_income_head_string");
                entity.Property(e => e.ExpenseHeadsAsString).HasColumnName("vt_cb_expense_head_string");
                
                entity.Property(e => e.CashAmount).HasColumnName("vt_cb_cash_amount");
                entity.Property(e => e.ChequeAmount).HasColumnName("vt_cb_cheque_amount");
                entity.Property(e => e.DirectAmount).HasColumnName("vt_cb_direct_amount");
                entity.Property(e => e.CrossAmount).HasColumnName("vt_cb_cross_amount");
                entity.Property(e => e.RunningTotal).HasColumnName("vt_cb_rn_total");
                entity.Property(e => e.IncomeItemId).HasColumnName("vt_cb_income_item_id");
                entity.Property(e => e.ExpenseItemId).HasColumnName("vt_cb_expense_item_id");
                entity.Property(e => e.IncomeExpenseMethod).HasColumnName("vt_cb_income_expense_method");
                entity.Property(e => e.SessionId).HasColumnName("vt_cb_session_id");

                entity.Property(e => e.RowStatus).HasDefaultValue(1).HasColumnName("row_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                entity.Property(e => e.RowVersion).HasColumnName("row_version");

                entity.Property(e => e.SystemAt).HasColumnName("system_at");

            });

            modelBuilder.Entity<CashBookDailyBalance>(entity =>
            {
                entity.ToTable("vt_cash_book_daily_balance");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");


                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.SabhaId).HasColumnName("sabha_id");
                entity.Property(e => e.SessionId).HasColumnName("session_id");
                entity.Property(e => e.Date).HasColumnName("date");
                entity.Property(e => e.OfficeId).HasColumnName("office_id");
                entity.Property(e => e.BankAccountId).HasColumnName("bank_acc_id");


                entity.Property(e => e.IncomeTotalCashAmount).HasColumnName("inc_cheque_amount");
                entity.Property(e => e.IncomeTotalChequeAmount).HasColumnName("inc_cheque_amount");
                entity.Property(e => e.IncomeTotalDirectAmount).HasColumnName("inc_direct_amount");
                entity.Property(e => e.IncomeTotalCrossAmount).HasColumnName("inc_cross_amount");
                entity.Property(e => e.IncomeTotalAmount).HasColumnName("inc_total_amount");

                entity.Property(e => e.ExpenseTotalCashAmount).HasColumnName("exp_cash_amount");
                entity.Property(e => e.ExpenseTotalChequeAmount).HasColumnName("exp_cheque_amount");
                entity.Property(e => e.ExpenseTotalDirectAmount).HasColumnName("exp_direct_amount");
                entity.Property(e => e.ExpenseTotalCrossAmount).HasColumnName("exp_cross_amount");
                entity.Property(e => e.ExpenseTotalAmount).HasColumnName("exp_total_amount");

                entity.Property(e => e.ExpenseTotalAmount).HasColumnName("current_balance");



                entity.Property(e => e.RowStatus).HasColumnName("row_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                entity.Property(e => e.RowVersion).HasColumnName("row_version");



            });

            modelBuilder.Entity<Deposit>(entity =>
            {
                entity.ToTable("deposits");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");


                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("dep_id");
                entity.Property(e => e.SabhaId).HasColumnName("dep_sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("dep_office_id");
                entity.Property(e => e.DepositSubCategoryId).HasColumnName("dep_deposit_sub_category_id");
                entity.Property(e => e.LedgerAccountId).HasColumnName("dep_ledger_account_id");
                entity.Property(e => e.DepositDate).HasColumnName("dep_deposit_date");
                entity.Property(e => e.SubInfoId).HasColumnName("dep_sub_info");
                entity.Property(e => e.MixOrderId).HasColumnName("dep_mix_order_id");
                entity.Property(e => e.MixOrderLineId).HasColumnName("dep_mix_order_line_id");
                entity.Property(e => e.ReceiptNo).HasColumnName("dep_receipt_no");
                entity.Property(e => e.Description).HasColumnName("dep_description");
                entity.Property(e => e.PartnerId).HasColumnName("dep_partner_id");
                entity.Property(e => e.InitialDepositAmount).HasColumnName("dep_initial_deposit_amount");
                entity.Property(e => e.ReleasedAmount).HasColumnName("dep_relesed_amount");
                entity.Property(e => e.HoldAmount).HasColumnName("hold_amount");
                entity.Property(e => e.IsCompleted).HasDefaultValue(false).HasColumnName("is_completed");
                entity.Property(e => e.IsEditable).HasDefaultValue(false).HasColumnName("is_editable");
             

                
                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("dep_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.SystemCreateAt).HasColumnName("system_create_at");
                entity.Property(e => e.SystemUpdateAt).HasColumnName("system_update_at");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();

            });

            modelBuilder.Entity<SubImprest>(entity =>
            {
                entity.ToTable("sub_imprest");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");


                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.SubImprestVoteId).HasColumnName("sub_imprest_vote_id");
                entity.Property(e => e.VoucherId).HasColumnName("voucher_id");
                entity.Property(e => e.ReceiptNo).HasColumnName("receipt_no");
                entity.Property(e => e.Amount).HasColumnName("amount");
                entity.Property(e => e.SettleByCash).HasDefaultValue(0m).HasColumnName("settle_by_cash");
                entity.Property(e => e.SettleByBills).HasDefaultValue(0m).HasColumnName("settle_by_bills");
                entity.Property(e => e.Date).HasColumnName("date");
                entity.Property(e => e.MixOrderId).HasColumnName("dep_mix_order_id");
                entity.Property(e => e.IsOpenBalance).HasColumnName("is_open_balance");
                entity.Property(e => e.ActionStates).HasDefaultValue(FinalAccountActionStates.Init).HasColumnName("action_states");
                entity.Property(e => e.SettlementVoucherId).HasColumnName("settlement_voucher_id");
                entity.Property(e => e.ExceedSettlementVoteId).HasColumnName("exceed_settlement_vote_id");
                entity.Property(e => e.ExceedAmount).HasColumnName("exceed_amount");
                entity.Property(e => e.SabhaId).HasColumnName("sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("office_id");
                entity.Property(e => e.IsIllegal).HasDefaultValue(false).HasColumnName("is_illegal");
                



                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("dep_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.SystemCreateAt).HasColumnName("system_create_at");
                entity.Property(e => e.SystemUpdateAt).HasColumnName("system_update_at");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();

            });


            modelBuilder.Entity<SettlementCrossOrder>(entity =>
            {
                entity.ToTable("sub_imprest_settlement_corss_orders");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");


                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.SettlementCrossId).HasColumnName("settlement_cross_id");
                entity.Property(e => e.Amount).HasColumnName("amount");


                entity.HasOne(val => val.SubImprest)
               .WithMany(v => v.SettlementCrossOrders)
               .OnDelete(DeleteBehavior.Restrict)
               .HasForeignKey(val => val.SubImprestId);


                //entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("dep_status");

                //entity.Property(e => e.CreatedAt)
                //    .HasColumnType("datetime")
                //    .HasColumnName("created_at");

                //entity.Property(e => e.UpdatedAt)
                //   .HasColumnType("datetime")
                //   .HasColumnName("updated_at");

                //entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                //entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                //entity.Property(e => e.SystemCreateAt).HasColumnName("system_create_at");
                //entity.Property(e => e.SystemUpdateAt).HasColumnName("system_update_at");

                //entity.Property(e => e.RowVersion).HasColumnName("row_version");
                //entity.Property(e => e.RowVersion).IsConcurrencyToken();

            });
            modelBuilder.Entity<SubImprestSettlement>(entity =>
            {
                entity.ToTable("sub_imprest_settlement");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");


                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.SubImprestId).HasColumnName("sub_ipmrest_id");

                entity.Property(e => e.VoteDetailId).HasColumnName("vote_detail_id");
                entity.Property(e => e.Amount).HasColumnName("amount");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Date).HasColumnName("date");


                entity.HasOne(val => val.SubImprest)
                  .WithMany(v => v.SubImprestSettlements)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasForeignKey(val => val.SubImprestId);

                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("dep_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.SystemCreateAt).HasColumnName("system_create_at");
                entity.Property(e => e.SystemUpdateAt).HasColumnName("system_update_at");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();

            });

            modelBuilder.Entity<SubImprestDocument>(entity =>
            {
                entity.ToTable("sub_imprest_documents");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");


                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.SubImprestSettlementId).HasColumnName("sub_ipmrest_settlement_id");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.URI).HasColumnName("uri");


                //entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("dep_status");

                //entity.Property(e => e.CreatedAt)
                //    .HasColumnType("datetime")
                //    .HasColumnName("created_at");

                //entity.Property(e => e.UpdatedAt)
                //   .HasColumnType("datetime")
                //   .HasColumnName("updated_at");

                //entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                //entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                //entity.Property(e => e.SystemCreateAt).HasColumnName("system_create_at");
                //entity.Property(e => e.SystemUpdateAt).HasColumnName("system_update_at");

                //entity.Property(e => e.RowVersion).HasColumnName("row_version");
                //entity.Property(e => e.RowVersion).IsConcurrencyToken();

                entity.HasOne(val => val.SubImprestSettlement)
                   .WithMany(v => v.Documents)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasForeignKey(val => val.SubImprestSettlementId);

            });

            modelBuilder.Entity<AccountTransfer>(entity =>
            {
                entity.ToTable("account_transfer");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");


                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FromAccountId).HasColumnName("from_account_Id");
                entity.Property(e => e.RequestNote).HasColumnName("request_note");
                entity.Property(e => e.FromVoteBalanceId).HasColumnName("from_vote_balanceId");
                entity.Property(e => e.FromVoteDetailId).HasColumnName("from_vote_detail_id");

                entity.Property(e => e.ToVoteBalanceId).HasColumnName("to_vote_balanceId");
                entity.Property(e => e.ToAccountId).HasColumnName("to_account_id");
                entity.Property(e => e.ToVoteDetailId).HasColumnName("to_vote_detail_id");
                entity.Property(e => e.VoucherId).HasColumnName("voucher_Id");
                entity.Property(e => e.Amount).HasColumnName("amount");
                entity.Property(e => e.RefundedAmount).HasDefaultValue(0m).HasColumnName("refunded_amount");
                entity.Property(e => e.SabhaId).HasColumnName("sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("office_id");
                entity.Property(e => e.IsRefund).HasColumnName("is_refund");
                entity.Property(e => e.IsFullyRefunded).HasDefaultValue(false).HasColumnName("is_fully_refunded");



                entity.Property(e => e.RowStatus).HasDefaultValue(1).HasColumnName("row_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.SystemCreateAt).HasColumnName("system_create_at");
                entity.Property(e => e.SystemUpdateAt).HasColumnName("system_update_at");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();

            });

            modelBuilder.Entity<AccountTransferRefunding>(entity =>
            {
                entity.ToTable("account_transfer_refunding");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");


                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.AccountTransferId).HasColumnName("account_transfer_id");
                entity.Property(e => e.RefundNote).HasColumnName("refund_note");

                entity.Property(e => e.Amount).HasColumnName("amount");


                entity.HasOne(x => x.AccountTransfer)
                  .WithMany(y => y.AccountTransferRefunding)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasForeignKey(x => x.AccountTransferId);

                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("dep_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.SystemCreateAt).HasColumnName("system_create_at");
                entity.Property(e => e.SystemUpdateAt).HasColumnName("system_update_at");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();

            });

            modelBuilder.Entity<VoteLedgerBookDailyBalance>(entity =>
            {
                entity.ToTable("vt_ledger_book_daily_balance");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");


                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.SabhaId).HasColumnName("sabha_id");
                entity.Property(e => e.SessionId).HasColumnName("session_id");
                entity.Property(e => e.Date).HasColumnName("date");
                entity.Property(e => e.VoteBalanceId).HasColumnName("vote_balance_id");
                entity.Property(e => e.VoteDetailId).HasColumnName("vote_detail_id");


                entity.Property(e => e.Debit).HasColumnName("total_debit");
                entity.Property(e => e.Credit).HasColumnName("total_credit");

                entity.Property(e => e.TotalDailyCredit).HasColumnName("total_daily_credit");
                entity.Property(e => e.TotalDailyCredit).HasColumnName("total_daily_credit");

                entity.Property(e => e.RunningTotal).HasColumnName("total_running_balance");



                entity.Property(e => e.RowStatus).HasColumnName("row_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.SystemActionDate).HasColumnName("system_action_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                entity.Property(e => e.RowVersion).HasColumnName("row_version");



            });

            modelBuilder.Entity<InternalJournalTransfers>(entity =>
            {
                entity.ToTable("vt_lgr_internal_journal_transfer");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("ijt_id");
                entity.Property(e => e.VoteBalanceId).HasColumnName("ijt_vote_bal_id");
                entity.Property(e => e.SabhaId).HasColumnName("ijt_sabha_id");
                entity.Property(e => e.VoteDetailId).HasColumnName("ijt_vote_id");
                entity.Property(e => e.Year).HasColumnName("ijt_year");
                entity.Property(e => e.Month).HasColumnName("ijt_month");
                entity.Property(e => e.Status).HasColumnName("ijt_status");
                entity.Property(e => e.Credit).HasColumnName("ijt_credit").HasDefaultValue(0m);
                entity.Property(e => e.Debit).HasColumnName("ijt_debit").HasDefaultValue(0m);
                entity.Property(e => e.AppCategory).HasColumnName("ijt_app_category");
                entity.Property(e => e.ModulePrimaryKey).HasColumnName("ijt_module_primary_key");
                entity.Property(e => e.TransactionType).HasColumnName("ijt_transaction_type");
                entity.Property(e => e.Code).HasColumnName("ijt_code");
                entity.Property(e => e.SubCode).HasColumnName("ijt_sub_code");
                entity.Property(e => e.Description).HasColumnName("ijt_description");
                entity.Property(e => e.CreateBy).HasColumnName("system_create_by");
                entity.Property(e => e.CreateAt).HasColumnName("system_create_at");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");


            });


            /*for controlling purpose*/
            
        modelBuilder.Entity<IndustrialCreditorsDebtorsTypes>(entity =>
        {
            entity.ToTable("vt_obl_industrial_creditors_debtors_types");
            entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.CreatedBy).HasColumnName("system_create_by");
            entity.Property(e => e.CreatedAt).HasColumnName("system_create_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("system_update_by");
            entity.Property(e => e.UpdatedAt).HasColumnName("system_update_at");
            entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");

        });

        modelBuilder.Entity<SabhaFundSource>(entity =>
        {
            entity.ToTable("vt_obl_sabha_fund_source");
            entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountSystem).HasColumnName("account_system");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.CreatedBy).HasColumnName("system_create_by");
            entity.Property(e => e.CreatedAt).HasColumnName("system_create_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("system_update_by");
            entity.Property(e => e.UpdatedAt).HasColumnName("system_update_at");
            entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");


        });
            
        /*for open balance*/
        
            modelBuilder.Entity<CreditorBilling>(entity =>
            {
                entity.ToTable("vt_obl_creditor_billing");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("cr_bl_id");
                entity.Property(e => e.LedgerAccountId).HasColumnName("cr_bl_ledger_account_id");
                entity.Property(e => e.CustomVoteId).HasColumnName("cr_bl_custom_vote_id");
                entity.Property(e => e.Description).HasColumnName("cr_bl_description");
                entity.Property(e => e.CreditorId).HasColumnName("cr_bl_creditor_id");
                entity.Property(e => e.CreditorCategory).HasColumnName("cr_bl_creditor_cetegory");
                entity.Property(e => e.Year).HasColumnName("cr_bl_year");
                entity.Property(e => e.Month).HasColumnName("cr_bl_month");
                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("cr_bl_status");

                entity.Property(e => e.SabhaId).HasColumnName("cr_bl_sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("cr_bl_office_id");



                entity.Property(e => e.CreatedBy).HasColumnName("system_create_by");
                entity.Property(e => e.CreatedAt).HasColumnName("system_create_at");
                entity.Property(e => e.UpdatedBy).HasColumnName("system_update_by");
                entity.Property(e => e.UpdatedAt).HasColumnName("system_update_at");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");

            });

            modelBuilder.Entity<SingleOpenBalance>(entity =>
            {
                entity.ToTable("vt_obl_single_open");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("single_bal_id");
                entity.Property(e => e.LedgerAccountId).HasColumnName("single_bal_ledger_account_id");
                entity.Property(e => e.CustomVoteId).HasColumnName("single_bal_custom_vote_id");
                entity.Property(e => e.Description).HasColumnName("single_bal_description");
                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("single_bal_status");

                entity.Property(e => e.SabhaId).HasColumnName("single_bal_sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("single_bal_office_id");

                entity.Property(e => e.CreatedBy).HasColumnName("single_bal_create_by");
                entity.Property(e => e.CreatedAt).HasColumnName("single_bal_create_at");
                entity.Property(e => e.UpdatedBy).HasColumnName("single_bal_update_by");
                entity.Property(e => e.UpdatedAt).HasColumnName("single_bal_update_at");
                entity.Property(e => e.SystemActionAt).HasColumnName("single_bal_action_at");

            });

            modelBuilder.Entity<FixedDeposit>(entity =>
            {
                entity.ToTable("vt_obl_fixed_deposits");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("fd_id");
                entity.Property(e => e.DepositTypeVote).HasColumnName("fd_type_vote_id");
                entity.Property(e => e.CustomVoteId).HasColumnName("fd_custom_vote_id");
                entity.Property(e => e.BankBranchId).HasColumnName("fd_bank_branch_id");
                entity.Property(e => e.Reference).HasColumnName("fd_reference");
                entity.Property(e => e.InterestRate).HasColumnName("fd_interest_rate");
                entity.Property(e => e.DepositDate).HasColumnName("fd_deposit_date");
                entity.Property(e => e.FDAmount).HasColumnName("fd_fd_amount");
                entity.Property(e => e.Duration).HasColumnName("fd_duration");
                entity.Property(e => e.RenewableDate).HasColumnName("fd_renewable_date");
                
                
                
                
                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("fd_status");
                entity.Property(e => e.SabhaId).HasColumnName("fd_sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("fd_office_id");


                entity.Property(e => e.CreatedBy).HasColumnName("system_create_by");
                entity.Property(e => e.CreatedAt).HasColumnName("system_create_at");
                entity.Property(e => e.UpdatedBy).HasColumnName("system_update_by");
                entity.Property(e => e.UpdatedAt).HasColumnName("system_update_at");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");


            });  
            modelBuilder.Entity<IndustrialCreditors>(entity =>
            {
                entity.ToTable("vt_obl_industrial_creditors");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("idc_id");
                entity.Property(e => e.CreditorTypeId).HasColumnName("idc_creditor_type_id");
                entity.Property(e => e.CustomVoteId).HasColumnName("idc_creditor_custom_vote_id");
                entity.Property(e => e.FundSourceId).HasColumnName("idc_fund_source_id");
                entity.Property(e => e.CategoryVote).HasColumnName("idc_category_vote");
                entity.Property(e => e.CreditorCategory).HasColumnName("idc_creditor_category");
                entity.Property(e => e.CreditorId).HasColumnName("idc_creditor_id");
                entity.Property(e => e.ProjectName).HasColumnName("idc_project_name");
                entity.Property(e => e.Amount).HasColumnName("idc_amount");
                
                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("idc_status");
                entity.Property(e => e.SabhaId).HasColumnName("idc_sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("idc_office_id");


                entity.Property(e => e.CreatedBy).HasColumnName("system_create_by");
                entity.Property(e => e.CreatedAt).HasColumnName("system_create_at");
                entity.Property(e => e.UpdatedBy).HasColumnName("system_update_by");
                entity.Property(e => e.UpdatedAt).HasColumnName("system_update_at");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");

                //entity.HasOne(ic => ic.FundSource)
                //       .WithMany(fs => fs.IndustrialCreditors)
                //       .HasForeignKey(ic => ic.FundSourceId)
                //      .HasConstraintName("fk_industrial_creditors_sabha_fund_dource");

                entity.HasOne(ic => ic.CreditorType)
                  .WithMany(fs => fs.IndustrialCreditors)
                  .HasForeignKey(ic => ic.CreditorTypeId)
                 .HasConstraintName("fk_creditor_type_sabha_fund_dource");

            });

            modelBuilder.Entity<IndustrialDebtors>(entity =>
            {
                entity.ToTable("vt_obl_industrial_debtors");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("idb_id");
                entity.Property(e => e.DebtorTypeId).HasColumnName("idb_debtor_type_id");
                entity.Property(e => e.FundSourceId).HasColumnName("idb_fund_source_id");
                entity.Property(e => e.CategoryVote).HasColumnName("idb_category_vote");
                entity.Property(e => e.CustomVoteId).HasColumnName("idb_custom_vote_id");
                entity.Property(e => e.DebtorCategory).HasColumnName("idb_debtor_category");
                entity.Property(e => e.DebtorId).HasColumnName("idb_debtor_id");
                entity.Property(e => e.ProjectName).HasColumnName("idb_project_name");
                entity.Property(e => e.Amount).HasColumnName("idb_amount");

                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("idc_status");
                entity.Property(e => e.SabhaId).HasColumnName("idc_sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("idc_office_id");


                entity.Property(e => e.CreatedBy).HasColumnName("system_create_by");
                entity.Property(e => e.CreatedAt).HasColumnName("system_create_at");
                entity.Property(e => e.UpdatedBy).HasColumnName("system_update_by");
                entity.Property(e => e.UpdatedAt).HasColumnName("system_update_at");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");

                //entity.HasOne(id => id.FundSource)
                //       .WithMany(fs => fs.IndustrialDebtors)
                //       .HasForeignKey(id => id.FundSourceId)
                //      .HasConstraintName("fk_industrial_debtors_sabha_fund_dource");

                entity.HasOne(id => id.DebtorType)
                   .WithMany(fs => fs.IndustrialDebtors)
                   .HasForeignKey(id => id.DebtorTypeId)
                  .HasConstraintName("fk_debtor_type_sabha_fund_dource");

            }); 
            modelBuilder.Entity<LALoan>(entity =>
            {
                entity.ToTable("vt_obl_la_loan");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("lal_id");
                entity.Property(e => e.LoanTypeVote).HasColumnName("lal_loan_type_vote");
                entity.Property(e => e.CustomVoteId).HasColumnName("lal_loan_type_custom_vote");
                entity.Property(e => e.BankBranchId).HasColumnName("lal_bank_branch_id");
                entity.Property(e => e.LoanAmount).HasColumnName("lal_loan_amount");
                entity.Property(e => e.InterestRate).HasColumnName("lal_interest_rate");
                entity.Property(e => e.BorrowingDate).HasColumnName("lal_borrowing_date");
                entity.Property(e => e.Duration).HasColumnName("lal_duration");
                entity.Property(e => e.Installment).HasColumnName("lal_installment");
                entity.Property(e => e.Balance).HasColumnName("lal_balance");

                entity.Property(e => e.LoanPurpose).HasColumnName("lal_loan_purpose");
                entity.Property(e => e.Mortgage).HasColumnName("lal_mortgage");

                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("lal_status");
                entity.Property(e => e.SabhaId).HasColumnName("lal_sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("lal_office_id");



                entity.Property(e => e.CreatedBy).HasColumnName("system_create_by");
                entity.Property(e => e.CreatedAt).HasColumnName("system_create_at");
                entity.Property(e => e.UpdatedBy).HasColumnName("system_update_by");
                entity.Property(e => e.UpdatedAt).HasColumnName("system_update_at");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");


            });
            modelBuilder.Entity<PrepaidPayment>(entity =>
            {
                entity.ToTable("vt_obl_pre_paid_payments");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("prepay_id");
                entity.Property(e => e.CategoryVote).HasColumnName("prepay_category_vote");
                entity.Property(e => e.CustomVoteId).HasColumnName("prepay_category_custom_vote_id");
                entity.Property(e => e.PrePaidPaidToId).HasColumnName("prepay_paid_to_id");
                entity.Property(e => e.PrePaidToCategory).HasColumnName("prepay_to_category");
                entity.Property(e => e.Amount).HasColumnName("prepay_amount");
                entity.Property(e => e.Description).HasColumnName("prepay_description");

                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("prepay_status");
                entity.Property(e => e.SabhaId).HasColumnName("prepay_sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("prepay_office_id");



                entity.Property(e => e.CreatedBy).HasColumnName("system_create_by");
                entity.Property(e => e.CreatedAt).HasColumnName("system_create_at");
                entity.Property(e => e.UpdatedBy).HasColumnName("system_update_by");
                entity.Property(e => e.UpdatedAt).HasColumnName("system_update_at");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");


            });
            modelBuilder.Entity<StoresCreditor>(entity =>
            {
                entity.ToTable("vt_obl_stores_creditors");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("stc_id");
                entity.Property(e => e.LedgerAccountId).HasColumnName("stc_ledger_account_id");
                entity.Property(e => e.CustomVoteId).HasColumnName("stc_custom_vote_id");
                entity.Property(e => e.SupplierCategory).HasColumnName("stc_supplier_category");
                entity.Property(e => e.SupplierId).HasColumnName("stc_supplier_id");
                entity.Property(e => e.PurchasingDate).HasColumnName("stc_purchasing_date");
                entity.Property(e => e.OrderNo).HasColumnName("stc_order_no");
                entity.Property(e => e.ReceivedNumber).HasColumnName("stc_received_number");
                entity.Property(e => e.GRN).HasColumnName("stc_grn");
                entity.Property(e => e.InvoiceAmount).HasColumnName("stc_invoice_amount");

                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("stc_status");
                entity.Property(e => e.SabhaId).HasColumnName("stc_sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("stc_office_id");



                entity.Property(e => e.CreatedBy).HasColumnName("system_create_by");
                entity.Property(e => e.CreatedAt).HasColumnName("system_create_at");
                entity.Property(e => e.UpdatedBy).HasColumnName("system_update_by");
                entity.Property(e => e.UpdatedAt).HasColumnName("system_update_at");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");


            });

            modelBuilder.Entity<ReceivableExchangeNonExchange>(entity =>
            {
                entity.ToTable("vt_obl_receivable_exchange_non_exchange");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("r_ex_nex_id");
                entity.Property(e => e.LedgerAccountId).HasColumnName("r_ex_nex_ledger_account_id");
                entity.Property(e => e.CustomVoteId).HasColumnName("r_ex_nex_custom_vote_id");
                entity.Property(e => e.ExchangeType).HasColumnName("r_ex_nex_exchnage_type");
                entity.Property(e => e.ReceivableFromId).HasColumnName("r_ex_nex_receivable_from_id");
                entity.Property(e => e.FinancialYear).HasColumnName("r_ex_nex_financial_year");
                entity.Property(e => e.ReceivableCategory).HasColumnName("r_ex_nex_receivable_category");
                entity.Property(e => e.Amount).HasColumnName("r_ex_nex_amount");

                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("r_ex_nex_status");
                entity.Property(e => e.SabhaId).HasColumnName("r_ex_nex_sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("r_ex_nex_office_id");


                entity.Property(e => e.CreatedBy).HasColumnName("system_create_by");
                entity.Property(e => e.CreatedAt).HasColumnName("system_create_at");
                entity.Property(e => e.UpdatedBy).HasColumnName("system_update_by");
                entity.Property(e => e.UpdatedAt).HasColumnName("system_update_at");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");


            });



            modelBuilder.Entity<FixedAssets>(entity =>
            {
                entity.ToTable("vt_obl_fixed_assets");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("fxa_id");
                entity.Property(e => e.AssetsLedgerAccountId).HasColumnName("fxa_ledger_account_id");
                entity.Property(e => e.CustomVoteId).HasColumnName("fxa_custom_vote_id");
                entity.Property(e => e.AssetsTitle).HasColumnName("fxa_assets_title");
                entity.Property(e => e.BalanceType).HasColumnName("fxa_balance_type");
                entity.Property(e => e.AssetsRegNo).HasColumnName("fxa_assets_reg_no");
                entity.Property(e => e.AcquiredDate).HasColumnName("fxa_acquired_date");
                entity.Property(e => e.RevalueDate).HasColumnName("fxa_revalue_date");
                entity.Property(e => e.RemainingLifetime).HasColumnName("fxa_remaining_life_time");
                entity.Property(e => e.GrantLedgerAccountId).HasColumnName("fxa_grant_ledger_account_id");
                entity.Property(e => e.GrantCustomVoteId).HasColumnName("fxa_grant_custom_vote_id");
                entity.Property(e => e.OriginalORRevaluedAmount).HasColumnName("fxa_original_or_revalued_amount");
                entity.Property(e => e.AccumulatedDepreciation).HasColumnName("fxa_accumulated_depreciation");
                entity.Property(e => e.GrantAmount).HasColumnName("fxa_grant_amount");
                entity.Property(e => e.AccumulatedRevenueRecognition).HasColumnName("fxa_accumulated_revenue_recognition");

                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("fxa_status");
                entity.Property(e => e.SabhaId).HasColumnName("fxa_sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("fxa_office_id");



                entity.Property(e => e.CreatedBy).HasColumnName("system_create_by");
                entity.Property(e => e.CreatedAt).HasColumnName("system_create_at");
                entity.Property(e => e.UpdatedBy).HasColumnName("system_update_by");
                entity.Property(e => e.UpdatedAt).HasColumnName("system_update_at");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");


            });


            modelBuilder.Entity<Classification>(entity =>
            {
                entity.HasKey(e => e.Id)
                   .HasName("PRIMARY");

                entity.ToTable("vt_classification");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("vt_clsf_id");
                entity.Property(e => e.Description).HasMaxLength(255).HasColumnName("vt_clsf_description");
                entity.Property(e => e.Code).HasMaxLength(10).HasColumnName("vt_clsf_code");
                entity.Property(e => e.Status).HasColumnName("vt_clsf_status").HasDefaultValueSql("'1'");
                entity.Property(e => e.CreatedAt).HasColumnName("vt_clsf_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("vt_clsf_updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<MainLedgerAccount>(entity =>
            {
                entity.HasKey(e => e.Id)
                   .HasName("PRIMARY");

                entity.ToTable("vt_main_ledger_account");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");
                entity.HasIndex(e => e.ClassificationId);
                entity.Property(e => e.Id).HasColumnName("vt_mla_id");
                entity.Property(e => e.ClassificationId).HasColumnName("vt_mla_classification_id");
                entity.Property(e => e.Description).HasMaxLength(255).HasColumnName("vt_mla_description");
                entity.Property(e => e.Code).HasMaxLength(10).HasColumnName("vt_mla_code");
                entity.Property(e => e.Status).HasColumnName("vt_mla_status").HasDefaultValueSql("'1'"); 

                entity.Property(e => e.CreatedAt).HasColumnName("vt_mla_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("vt_mla_updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();

                entity.HasOne(d => d.Classifications)
                   .WithMany(p => p.MainLedgerAccount)
                   .HasForeignKey(d => d.ClassificationId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
              
            });

            modelBuilder.Entity<IncomeExpenditureSubledgerAccount>(entity =>
            {
                entity.ToTable("vt_inc_exp_subledger_account");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.NameSinhala).HasColumnName("vt_inc_exp_subledger_name_sinhala").HasMaxLength(255);
                entity.Property(e => e.NameEnglish).HasColumnName("vt_inc_exp_subledger_name_english").HasMaxLength(255);
                entity.Property(e => e.NameTamil).HasColumnName("vt_inc_exp_subledger_name_tamil").HasMaxLength(255);
                entity.Property(e => e.Code).HasColumnName("vt_inc_exp_subledger_code").HasMaxLength(50);
                entity.Property(e => e.IncomeExpenditureLedgerAccountId).HasColumnName("vt_inc_exp_subledger_ledger_account_id");
                entity.Property(e => e.COAVersionId).HasColumnName("vt_inc_exp_subledger_coa_version_id");
                entity.Property(e => e.StatusID).HasDefaultValue(1).HasColumnName("vt_inc_exp_subledger_status_id");

                entity.HasOne(e => e.IncomeSubtitles)
                    .WithMany(e => e.SubLedgerAccounts)
                    .HasForeignKey(e => e.IncomeExpenditureLedgerAccountId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BalsheetSubledgerAccount>(entity =>
            {
                entity.ToTable("vt_balsheet_subledger_account");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.NameSinhala).HasColumnName("vt_balsheet_subledger_name_sinhala").HasMaxLength(255);
                entity.Property(e => e.NameEnglish).HasColumnName("vt_balsheet_subledger_name_english").HasMaxLength(255);
                entity.Property(e => e.NameTamil).HasColumnName("vt_balsheet_subledger_name_tamil").HasMaxLength(255);
                entity.Property(e => e.Code).HasColumnName("vt_balsheet_subledger_code").HasMaxLength(50);
                entity.Property(e => e.BalsheetLedgerAccountId).HasColumnName("vt_balsheet_subledger_balsheet_ledger_account_id");
                entity.Property(e => e.COAVersionId).HasColumnName("vt_balsheet_subledger_coa_version_id");
                entity.Property(e => e.StatusID).HasColumnName("vt_balsheet_subledger_status_id");

                entity.HasOne(e => e.BalancesheetSubtitles)
                    .WithMany(e => e.SubLedgerAccounts)
                    .HasForeignKey(e => e.BalsheetLedgerAccountId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Budget>(entity =>
            {
                entity.ToTable("vt_budget");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("bdgt_id");

                entity.Property(e => e.VoteDetailId).HasColumnName("bdgt_votedetail_id");
                //entity.Property(e => e.BudgetType).HasColumnName("bdgt_budget_type");
                entity.Property(e => e.Year).HasColumnName("bdgt_year");

                entity.Property(e => e.BudgetTotal).HasColumnName("bdgt_total");
                entity.Property(e => e.Q1Amount).HasColumnName("bdgt_q1_amount");
                entity.Property(e => e.Q2Amount).HasColumnName("bdgt_q2_amount");
                entity.Property(e => e.Q3Amount).HasColumnName("bdgt_q3_amount");
                entity.Property(e => e.Q4Amount).HasColumnName("bdgt_q4_amount");

                entity.Property(e => e.AnnualAmount).HasColumnName("bdgt_annual_amount");

                entity.Property(e => e.January).HasColumnName("bdgt_january");
                entity.Property(e => e.February).HasColumnName("bdgt_february");
                entity.Property(e => e.March).HasColumnName("bdgt_march");
                entity.Property(e => e.April).HasColumnName("bdgt_april");
                entity.Property(e => e.May).HasColumnName("bdgt_may");
                entity.Property(e => e.June).HasColumnName("bdgt_june");
                entity.Property(e => e.July).HasColumnName("bdgt_july");
                entity.Property(e => e.August).HasColumnName("bdgt_august");
                entity.Property(e => e.September).HasColumnName("bdgt_september");
                entity.Property(e => e.October).HasColumnName("bdgt_october");
                entity.Property(e => e.November).HasColumnName("bdgt_november");
                entity.Property(e => e.December).HasColumnName("bdgt_december");

                entity.Property(e => e.Status).HasColumnName("bdgt_status").HasDefaultValue(1); ;
                entity.Property(e => e.SabhaID).HasColumnName("bdgt_sabha_id");
                entity.Property(e => e.CreatedBy).HasColumnName("bdgt_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("bdgt_updated_by");
                entity.Property(e => e.CreatedAt).HasColumnName("bdgt_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").ValueGeneratedOnAddOrUpdate().HasColumnName("bdgt_updated_at");
                entity.HasIndex(e => new { e.Year, e.VoteDetailId }).IsUnique();
            });


            modelBuilder.Entity<LedgerAccountGroupAssignment>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("PRIMARY");

                entity.ToTable("vt_ldger_acc_grp_assign");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID).HasColumnName("ldger_acc_grp_asgn_id");

                entity.Property(e => e.SubtitleCode)
                    .HasMaxLength(255)
                    .HasColumnName("ldger_acc_grp_asgn_subtitle_code");

                entity.Property(e => e.SubtitleDescription)
                    .HasMaxLength(255)
                    .HasColumnName("ldger_acc_grp_asgn_subtitle_desc");

                entity.Property(e => e.LedgerAccountGroupCategory)
                    .HasColumnName("ldger_acc_grp_asgn_category")
                    .HasConversion<int>(); 

                entity.Property(e => e.LedgerAccountGroupCategoryDescription)
                    .HasMaxLength(255)
                    .HasColumnName("ldger_acc_grp_asgn_catgry_desc");

                entity.Property(e => e.AccountSystemVersion)
                    .HasColumnName("ldger_acc_grp_asgn_account_system_version");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("ldger_acc_grp_asgn_created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("ldger_acc_grp_asgn_updated_at")
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate();
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}