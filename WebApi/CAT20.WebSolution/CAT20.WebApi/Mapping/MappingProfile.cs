using AutoMapper;
using CAT20.Core.DTO;
using CAT20.Core.DTO.Assessment.Save;
using CAT20.Core.DTO.Final;
using CAT20.Core.DTO.Final.Save;
using CAT20.Core.DTO.HRM;
using CAT20.Core.DTO.Mix;
using CAT20.Core.DTO.OtherModule;
using CAT20.Core.DTO.Vote;
using CAT20.Core.DTO.Vote.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.AssessmentTax.AssessmentBalanceHistory.AssessmentBalanceHistory;
using CAT20.Core.Models.AssessmentTax.Quarter;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.FinalAccount.logs;
using CAT20.Core.Models.HRM.LoanManagement;
using CAT20.Core.Models.HRM.PersonalFile;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Models.OnlinePayment;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Models.TradeTax;
using CAT20.Core.Models.User;
using CAT20.Core.Models.Vote;
using CAT20.Core.Models.WaterBilling;
using CAT20.WebApi.Mapping.MappingReslover;
using CAT20.WebApi.Resources.AssessmentTax;
using CAT20.WebApi.Resources.Control;
using CAT20.WebApi.Resources.Final;
using CAT20.WebApi.Resources.FInalAccount;
using CAT20.WebApi.Resources.FInalAccount.Save;
using CAT20.WebApi.Resources.HRM.LoanManagement;
using CAT20.WebApi.Resources.HRM.PersonalFile;
using CAT20.WebApi.Resources.Mixin;
using CAT20.WebApi.Resources.Mixin.Save;
using CAT20.WebApi.Resources.OnlinePayment;
using CAT20.WebApi.Resources.ShopRental;
using CAT20.WebApi.Resources.TradeTax;
using CAT20.WebApi.Resources.User;
using CAT20.WebApi.Resources.User.Save;
using CAT20.WebApi.Resources.Vote;
using CAT20.WebApi.Resources.Vote.Save;
using CAT20.WebApi.Resources.WaterBilling;

namespace CAT20.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            //CreateMap<Product, ProductResource>();
            //CreateMap<Invoice, InvoiceResource>().ForMember(x => x.InvoiceItemResource, x => x.MapFrom(x => x.InvoiceItems)) ;
            //CreateMap<InvoiceItems, InvoiceItemResource>();
            CreateMap<Province, ProvinceResource>();
            CreateMap<BankDetail, BankDetailResource>();
            CreateMap<BankDetail, FinalBankDetailResource>();
            CreateMap<BankBranch, BankBranchResource>();
            CreateMap<BankBranch, FinalBankBranchResource>();
            CreateMap<Agents, AgentsResource>();
            CreateMap<District, DistrictResource>();
            CreateMap<Gender, GenderResource>();
            CreateMap<Language, LanguageResource>();
            CreateMap<Month, MonthResource>();
            CreateMap<Office, OfficeResource>();
            CreateMap<OfficeType, OfficeTypeResource>();
            CreateMap<Province, ProvinceResource>();
            CreateMap<Sabha, SabhaResource>();
            CreateMap<SelectedLanguage, SelectedLanguageResource>();
            CreateMap<Year, YearResource>();
            CreateMap<GnDivisions, GnDivisionsResource>().ReverseMap();
            CreateMap<PaymentNbt, PaymentNbtResource>().ReverseMap();
            CreateMap<PaymentVat, PaymentVatResource>().ReverseMap();
            CreateMap<Partner, PartnerResource>().ReverseMap();
            CreateMap<Partner, PartnerResourceOn>().ReverseMap();
            CreateMap<Partner, PartnerResourceOn>();
            CreateMap<PermittedThirdPartyAssessments, PermittedThirdPartyAssessmentsResource>().ReverseMap();

            CreateMap<Session, SessionResource>().ReverseMap();
            CreateMap<TaxType, TaxTypeResource>().ReverseMap();
            CreateMap<Business, BusinessResource>().ReverseMap();
            CreateMap<Business, SaveBusinessResource>().ReverseMap();
            CreateMap<BusinessTaxes, BusinessTaxesResource>().ReverseMap();
            CreateMap<BusinessPlace, BusinessPlaceResource>().ReverseMap();

            CreateMap<Programme, ProgrammeResource>();
            CreateMap<AccountBalanceDetail, AccountBalanceDetailResource>();
            CreateMap<AccountDetail, AccountDetailResource>()
                 .ForMember(dest => dest.AccountBalance, opt => opt.MapFrom<AccountBalanceResolver>())
                 .ReverseMap();


            CreateMap<BalancesheetBalance, BalancesheetBalanceResource>();
            CreateMap<BalancesheetSubtitle, BalancesheetSubtitleResource>();
            CreateMap<BalancesheetTitle, BalancesheetTitleResource>();
            CreateMap<Project, ProjectResource>();
            CreateMap<SubProject, SubProjectResource>();
            CreateMap<IncomeSubtitle, IncomeSubtitleResource>();
            CreateMap<IncomeTitle, IncomeTitleResource>();
            CreateMap<IncomeExpenditureSubledgerAccount, IncomeExpenditureSubledgerAccountResource>();
            CreateMap<BalsheetSubledgerAccount, BalsheetSubledgerAccountResource>();
            
            CreateMap<VoteBalance, VoteBalanceResource>()
                 //.ForMember(dest => dest.TotalPaid, opt => opt.MapFrom(src => src.Debit-src.Credit))
                 .ForMember(dest => dest.CreditDebitBalance, opt => opt.MapFrom<VoteBalanceTotalCreditDebitResolver>())
                 .ForMember(dest => dest.VoteBalance, opt => opt.MapFrom<VoteBalanceTotalResolver>())
                 .ForMember(dest => dest.PreBalance, opt => opt.MapFrom<VotePreBalanceResolver>())
                 .ReverseMap();


            CreateMap<VoteDetail, VoteDetailResource>();
            CreateMap<VoteDetail, VoteDetailFullResource>();
            CreateMap<SpecialLedgerAccounts, SpecialLedgerAccountsResource>().ReverseMap();
            CreateMap<SpecialLedgerAccountTypes, SpecialLedgerAccountTypesResource>().ReverseMap();

            CreateMap<UserDetail, UserDetailResource>();
            CreateMap<UserDetail, UserDetailBasicResource>().ReverseMap();
            CreateMap<LoginDetail, LoginResource>();
            CreateMap<Rule, RuleResource>();
            CreateMap<Group, GroupResource>();
            CreateMap<GroupRule, GroupRuleResource>();
            CreateMap<GroupUser, GroupUserResource>();
            CreateMap<UserRecoverQuestion, UserRecoverQuestionResource>();
            CreateMap<UserLoginActivity, UserLoginActivityResource>().ReverseMap();

            //Online Payment
            CreateMap<Verified, PaymentSMSResource>();
            CreateMap<Verified, PaymentEmailResource>();
            CreateMap<OrderDetails, PaymentDetails>();
            CreateMap<LoginRequest, LogInDetails>();
            CreateMap<PaymentGatewayResource, PaymentGateway>();
            CreateMap<PaymentGateway, PaymentGatewayResource>();
            CreateMap<SuccessMessageResource, PaymentDetails>();
            CreateMap<SuccessMessageResource, PaymentDetails>().ReverseMap();

            CreateMap<Dispute, SystemErrorMessage>();
            CreateMap<Dispute, SystemErrorMessage>().ReverseMap();

            CreateMap<PaymentGateway, PaymentGatewayBasicResource>().ReverseMap();
            CreateMap<PaymentGateway, PaymentGatewayResource>().ReverseMap();

            //................//

            CreateMap<BookingProperty, BookingPropertyResource>().ReverseMap();
            CreateMap<BookingSubProperty, BookingSubPropertyResource>().ReverseMap();
            CreateMap<SaveBookingPropertyResource, BookingProperty>().ReverseMap();
            CreateMap<SaveBookingSubPropertyResource, BookingSubProperty>().ReverseMap();
            CreateMap<BookingTimeSlot, BookingTimeSlotResource>().ReverseMap();
            CreateMap<ChargingScheme, ChargingSchemeResource>().ReverseMap();
            CreateMap<ChargingScheme, SaveChargingSchemeResource>().ReverseMap();
            CreateMap<SaveBookingTimeResource, BookingTimeSlot>().ReverseMap();
            CreateMap<OnlineBooking, OnlineBookingResource>().ReverseMap();
            CreateMap<SaveOnlineBookingResource, OnlineBooking>().ReverseMap();
            //.................//

            //mixin
            CreateMap<VoteAssignment, VoteAssignmentResource>();
            CreateMap<VoteAssignment, VoteAssignmentBasicResource>();
            CreateMap<VoteAssignmentDetails, VoteAssignmentDetailsResource>();
            CreateMap<VoteAssignmentDetails, HVoteAssignmentDetails>().ReverseMap();
            CreateMap<VoteAssignmentDetails, SaveVoteAssignmentDetails>().ReverseMap();
            CreateMap<VoteAssignmentDetails, CustomVoteResource>();
            CreateMap<MixinOrder, MixinOrderResource>();
            CreateMap<MixinOrderLine, MixinOrderLineResource>();
            CreateMap<MixinCancelOrder, MixinCancelOrderResource>().ReverseMap();
            CreateMap<Banking, BankingResource>().ReverseMap();

            //TradeTax
            CreateMap<BusinessNature, BusinessNatureResource>().ReverseMap();
            CreateMap<BusinessSubNature, BusinessSubNatureResource>().ReverseMap();
            CreateMap<TaxValue, TaxValueResource>().ReverseMap();
            CreateMap<TradeTaxVoteAssignment, TradeTaxVoteAssignmentResource>().ReverseMap();

            //ShopRental
            CreateMap<RentalPlace, RentalPlaceResource>().ReverseMap();
            CreateMap<RentalPlace, BasicRentalPlaceResource>().ReverseMap();

            CreateMap<Floor, FloorResource>().ReverseMap();
            CreateMap<Floor, BasicFloorResource>().ReverseMap();

            CreateMap<PropertyType, PropertyTypeResource>().ReverseMap();
            CreateMap<PropertyNature, PropertyNatureResource>().ReverseMap();

            CreateMap<Property, PropertyResource>().ReverseMap();
            CreateMap<Property, BasicPropertyResource>().ReverseMap();
            CreateMap<Property, PropertyFullResource>().ReverseMap();

            CreateMap<Shop, ShopResource>().ReverseMap();
            CreateMap<Shop, BasicShopResource>().ReverseMap();
            CreateMap<Shop, ShopFullResource>().ReverseMap();

            CreateMap<OpeningBalance, OpeningBalanceResource>().ReverseMap();
            CreateMap<OpeningBalance, BasicOpeningBalanceResource>().ReverseMap();

            CreateMap<ShopRentalVoteAssign, ShopRentalVoteAssignResource>().ReverseMap();
            CreateMap<ShopRentalVoteAssign, BasicShopRentalVoteAssignResource>().ReverseMap();

            CreateMap<ShopRentalBalance, ShopRentalBalanceResource>().ReverseMap();
            CreateMap<ShopRentalBalance, BasicShopRentalBalanceResource>().ReverseMap();

            CreateMap<ShopRentalBalance, ShopRentalBalanceSMSNotificationResource>().ReverseMap();

            CreateMap<ShopRentalProcessConfigaration, ShopRentalProcessConfigarationResource>().ReverseMap();
            CreateMap<FineCalType, FineCalTypeResource>().ReverseMap();
            CreateMap<FineChargingMethod, FineChargingMethodResource>().ReverseMap();
            CreateMap<FineRateType, FineRateTypeResource>().ReverseMap();
            CreateMap<RentalPaymentDateType, RentalPaymentDateTypeResource>().ReverseMap();
            CreateMap<ProcessConfigurationSettingAssign, ProcessConfigurationSettingAssignResource>().ReverseMap();

            CreateMap<ShopAgreementChangeRequest, ShopAgreementChangeRequestResource>().ReverseMap();
            CreateMap<ShopAgreementChangeRequest, BasicShopAgreementChangeRequestResource>().ReverseMap();
            CreateMap<ShopRentalProcess, ShopRentalProcessResource>().ReverseMap();
            CreateMap<ShopRentalProcess, SaveShopRentalProcessResource>().ReverseMap();

            CreateMap<ShopRentalRecievableIncomeVoteAssign, ShopRentalRecievableIncomeVoteAssignResource>().ReverseMap();


            //AssessmentTax
            CreateMap<Ward, WardResource>().ReverseMap();
            CreateMap<Street, StreetResource>().ReverseMap();
            CreateMap<Description, DescriptionResource>().ReverseMap();
            CreateMap<AssessmentPropertyType, AssessmentPropertyTypeResource>().ReverseMap();
            CreateMap<Assessment, AssessmentResource>().ReverseMap();
            CreateMap<Assessment, AssessmentBulkSaveRequest>().ReverseMap();
            CreateMap<AssessmentBalance, AssessmentBalanceResource>().ReverseMap();
            CreateMap<AssessmentTempSubPartner, AssessmentTempSubPartnerResource>().ReverseMap();
            CreateMap<AssessmentTempSubPartner, SaveAssessmentTempSubPartnerResource>().ReverseMap();
            CreateMap<Allocation, AllocationResource>().ReverseMap();
            CreateMap<Allocation, SaveAllocationResource>().ReverseMap();
            CreateMap<AssessmentTempPartner, AssessmentTempPartnerResource>().ReverseMap();
            CreateMap<AssessmentTempPartner, SaveAssessmentTempPartnerResource>().ReverseMap();
            CreateMap<VotePaymentType, VotePaymentTypeResource>().ReverseMap();

            CreateMap<AssessmentRates, AssessmentRatesResource>().ReverseMap();
            CreateMap<AssessmentVoteAssign, AssessmentVoteAssignResource>().ReverseMap();


            //Assessment Quarters Mapping
            CreateMap<Q1, QResource>().ReverseMap();
            CreateMap<Q2, QResource>().ReverseMap();
            CreateMap<Q3, QResource>().ReverseMap();
            CreateMap<Q4, QResource>().ReverseMap();

            //Assessment Quarters History  Mapping


            CreateMap<AssessmentBalancesHistory, AssessmentBalancesHistoryResources>().ReverseMap();

            CreateMap<QH1, QResource>().ReverseMap();
            CreateMap<QH2, QResource>().ReverseMap();
            CreateMap<QH3, QResource>().ReverseMap();
            CreateMap<QH4, QResource>().ReverseMap();


            CreateMap<AssessmentTransaction, AssessmentTransactionResource>()
                .ForMember(tr => tr.Type, opt => opt.MapFrom(src => Enum.GetName(typeof(AssessmentTransactionsType), src.Type)));

            CreateMap<AssessmentDescriptionLog, AssessmentDescriptionLogResource>().ReverseMap();
            CreateMap<AssessmentPropertyTypeLog, AssessmentPropertyTypeLogResource>().ReverseMap();

            CreateMap<AssessmentProcess, AssessmentProcessResource>()
                .ForMember(p => p.ProcessType, opt => opt.MapFrom(src => Enum.GetName(typeof(AssessmentProcessType), src.ProcessType)));

            CreateMap<AssessmentProcess, SaveAssessmentProcessResource>().ReverseMap();
            CreateMap<AssessmentJournal, AssessmentJournalResource>().ReverseMap();
            CreateMap<AssessmentAssetsChange, AssessmentAssetsChangeResource>().ReverseMap();
            CreateMap<NewAllocationRequest, NewAllocationRequestResource>().ReverseMap();
            CreateMap<SaveAssessmentBillAdjustmentResource, AssessmentBillAdjustment>().ReverseMap();
            CreateMap<AssessmentBillAdjustmentResource, AssessmentBillAdjustment>().ReverseMap();
            CreateMap<SaveSubDivisionAssessmentResource, Assessment>().ReverseMap();
            CreateMap<SaveAmalgamationAssessmentResource, Assessment>().ReverseMap();

            CreateMap<SaveAmalgamationAssessmentResource, Amalgamation>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int?)null))
                     .ForMember(dest => dest.KFormId, opt => opt.MapFrom(src => src.Id));

            //helper class mapping
            CreateMap<HAssessmentWarrantResources, HAssessmentWarrant>().ReverseMap();

            CreateMap<AmalgamationSubDivision, AmalgamationSubDivisionResource>().ReverseMap();
            CreateMap<SubDivision, SubDivisionResource>().ReverseMap();
            CreateMap<Amalgamation, AmalgamationResource>().ReverseMap();
            CreateMap<AmalgamationSubDivisionActions, AmalgamationSubDivisionActionsResource>().ReverseMap();
            CreateMap<AmalgamationSubDivisionActions, SaveAmalgamationSubDivisionActionsResource>().ReverseMap();

            CreateMap<AssessmentATDResource, AssessmentATD>()
            .ForMember(dest => dest.AssessmentATDOwnerslogs, opt => opt.MapFrom(src => src.AssessmentATDOwnerslogs))
            .ReverseMap();

            //WaterBilling

            CreateMap<WaterProjectMainRoad, WaterProjectMainRoadResource>().ReverseMap();
            CreateMap<WaterProjectSubRoad, WaterProjectSubRoadResource>().ReverseMap();
            CreateMap<WaterProjectGnDivision, WaterProjectGnDivisionResource>().ReverseMap();

            CreateMap<WaterProjectNature, WaterProjectNatureResource>().ReverseMap();
            CreateMap<WaterProject, WaterProjectResource>().ReverseMap();
            CreateMap<WaterTariff, WaterTariffResource>().ReverseMap();
            CreateMap<NonMeterFixCharge, NonMeterFixChargeResource>().ReverseMap();

            CreateMap<MeterConnectInfo, MeterConnectInfoResource>().ReverseMap();
            CreateMap<MeterReaderAssign, MeterReaderAssignResource>().ReverseMap();
            CreateMap<VoteAssign, VoteAssignResource>().ReverseMap();
            CreateMap<PaymentCategory, PaymentCategoryResource>().ReverseMap();

            CreateMap<ApplicationForConnection, ApplicationForConnectionResource>().ReverseMap();
            CreateMap<ApplicationForConnectionDocument, ApplicationForConnectionDocumentResource>().ReverseMap();

            CreateMap<WaterConnection, WaterConnectionResource>().ReverseMap();
            CreateMap<WaterBillDocument, WaterBillDocumentResource>().ReverseMap();
            CreateMap<WaterConnectionNatureLog, WaterConnectionNatureLogResource>().ReverseMap();
            CreateMap<WaterConnectionStatusLog, WaterConnectionStatusLogResource>().ReverseMap();


            CreateMap<OpeningBalanceInformation, OpeningBalanceInformationResource>().ReverseMap();
            CreateMap<ConnectionAuditLog, ConnectionAuditLogResource>().ReverseMap();
            CreateMap<ConnectionAuditLog, WaterConnection>().ReverseMap();

            CreateMap<ConnectionAuditLog, ConnectionAuditLogResource>()
                .ForMember(dest => dest.Action, opt => opt.MapFrom(src => Enum.GetName(typeof(WbAuditLogAction), src.Action)))
                .ForMember(dest => dest.EntityType, opt => opt.MapFrom(src => Enum.GetName(typeof(WbEntityType), src.EntityType)));


            CreateMap<WaterConnectionBalance, WaterConnectionBalanceResource>().ReverseMap();
            CreateMap<WaterConnectionBalance, WaterConnectionBalanceAddReadingResource>().ReverseMap();

            //FInal Accounts

            /*save resource map*/
            CreateMap<Commitment, SaveCommitmentResource>().ReverseMap();
            CreateMap<CommitmentLine, SaveCommitmentLineResource>().ReverseMap();
            CreateMap<CommitmentLineVotes, SaveCommitmentLineVotesResource>().ReverseMap();

            CreateMap<SaveVoucherChequeResources, VoucherCheque>()
             .ForMember(dest => dest.VoucherItemsForCheque, opt => opt.MapFrom(src => src.SubVoucherItems));

  
            CreateMap<SelectedVoucherItemsForCheque, VoucherItemsForCheque>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int?)null))
            .ForMember(dest => dest.SubVoucherItemId, opt => opt.MapFrom(src => src.Id));

            CreateMap<AccountDetail, AccountDetailOnlyBankId>().ReverseMap();
            CreateMap<MixinOrder, SaveCrossOrderResource>().ReverseMap();
            CreateMap<MixinOrderLine, SaveCrossOrderLineResource>().ReverseMap();
            
            
            //CreateMap< Voucher,SaveDepositVoucherResource>().ReverseMap();
            CreateMap< SaveDepositVoucherResource, Voucher>()
           .ForMember(dest => dest.BankId, opt => opt.MapFrom(src => src.BankId == 0 ? (int?)null : src.BankId));

            CreateMap<SaveRepaymentVoucher, Voucher>()
                .ForMember(dest => dest.BankId, opt => opt.MapFrom(src => src.BankId == 0 ? (int?)null : src.BankId));


            CreateMap<SubVoucherItem, SaveSubVoucherItemResource>().ReverseMap();

            CreateMap<SelectedDepositResource, DepositForVoucher>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int?)null))
            .ForMember(dest => dest.DepositId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.PayingAmount));

            CreateMap<Voucher, SaveSalaryVoucher>().ReverseMap();
            CreateMap< SaveEmployeeLoansForVoucher, EmployeeLoansForVoucher>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int?)null))
            .ForMember(dest => dest.LoanId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.InstallmentAmount, opt => opt.MapFrom(src => src.SettleInstallmentAmount))
            .ForMember(dest => dest.InterestAmount, opt => opt.MapFrom(src => src.SettleInterestAmount));

            /*custom vote*/

            CreateMap<SaveCustomVoteEntry, CustomVoteEntry>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int?)null))
             .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => (int?)null))
            .ForMember(dest => dest.CustomVoteDetailIdParentId, opt => opt.MapFrom(src => src.ParentId));

            CreateMap<CustomVoteBalance, CustomVoteBalanceLog>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int?)null))
             .ForMember(dest => dest.CustomVoteBalanceId, opt => opt.MapFrom(src => src.Id))
             .ReverseMap();

            //Budget
             CreateMap<Budget, BudgetResource>().ReverseMap();
             CreateMap<Budget, SaveBudgetResource>().ReverseMap();


            /*for response*/
            CreateMap<Commitment, CommitmentResource>().ReverseMap();
            CreateMap<CommitmentLine, CommitmentLineResource>().ReverseMap();
            CreateMap<CommitmentLineVotes, CommitmentLineVotesResource>().ReverseMap();
            CreateMap<CommitmentActionsLog, CommitmentActionsLogResources>().ReverseMap();  

            /*linking model*/
            CreateMap<Partner, VendorResource>().ReverseMap();
            CreateMap<Employee, FinalEmployeeResource>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => $"{src.Initials} {src.FirstName} {src.LastName}")).ReverseMap();


            
     







            //CreateMap<Commitment, CommitmentServiceModel>();
            //CreateMap<Commitment, CommitmentViewModel>();
            //CreateMap<VoucherResource, Voucher>();
            CreateMap< Voucher, VoucherResource>()
                .ForMember(dest => dest.VoucherCategoryString, opt => opt.MapFrom(src => Enum.GetName(typeof(VoucherCategory), src.VoucherCategory))).ReverseMap();
            
            CreateMap<SaveVoucherResource, Voucher>().ReverseMap();
            CreateMap<VoucherPostSettlement, Voucher>().ReverseMap();
            CreateMap<VoucherLine, VoucherLineResources>().ReverseMap();
            CreateMap<SaveVoucherLineResource, VoucherLine>().ReverseMap();
            CreateMap<SaveVoucherSubLineResources, VoucherSubLine>().ReverseMap();
            CreateMap<SaveVoucherCrossOrderResource, VoucherCrossOrder>().ReverseMap();
            CreateMap< VoucherCrossOrder, VoucherCrossOrderResource>().ReverseMap();

            CreateMap<SaveVoucherDocument, VoucherDocument>().ReverseMap();
            CreateMap<SaveVoucherInvoice, VoucherInvoice>().ReverseMap();

            CreateMap< VoucherDocument, VoucherDocumentResource>().ReverseMap();
            CreateMap< VoucherInvoice, VoucherInvoiceResource>().ReverseMap();

            CreateMap<Voucher, VoucherApprovalResource>();
            CreateMap<Voucher, VoucherApprovalResource>().ReverseMap();


            CreateMap<VoucherCheque, VoucherChequeResource>()
                .ForMember(dest => dest.ChequeCategoryString, opt => opt.MapFrom(src => Enum.GetName(typeof(VoucherCategory), src.ChequeCategory))).ReverseMap(); ;

            CreateMap<VoucherItemsForCheque, VoucherItemsForChequeResource>().ReverseMap();
            CreateMap<SubVoucherItem, SubVoucherItemResource>().ReverseMap();

            CreateMap< DepositForVoucher,DepositsForVoucherResource>().ReverseMap();

            //CreateMap<CommitmentApprovedLog, CommitmentApprovedLogResource>();
            //CreateMap<CommitmentApprovedLog, CommitmentApprovedLogResource>().ReverseMap();
            //CreateMap<CommitmentLog, CommitmentLogResource>();
            //CreateMap<CommitmentLog, CommitmentLogResource>().ReverseMap();
            //CreateMap<CommitmentLine, CommitmentLineResource1>();
            //CreateMap<CommitmentLine, CommitmentLineResource1>().ReverseMap();
            CreateMap<CommitmentLineVotes, CommitmentLineVotesResource>();
            CreateMap<CommitmentLineVotes, CommitmentLineVotesResource>().ReverseMap();
            
            
            
            CreateMap<VoteJournalAdjustment, SaveVoteJournalAdjustmentResource>().ReverseMap();
            CreateMap<VoteJournalItemFrom, SaveVoteJournalItemFromResource>().ReverseMap();
            CreateMap<VoteJournalItemTo, SaveVoteJournalItemToResource>().ReverseMap();


               /*deposits */
            CreateMap<Deposit, DepositResource>().ReverseMap();




            CreateMap<VoteJournalAdjustment, VoteJournalAdjustmentResource>().ReverseMap();
            CreateMap<VoteJournalItemFrom, VoteJournalItemFromResource>().ReverseMap();
            CreateMap<VoteJournalItemTo, VoteJournalItemToResource>().ReverseMap();

            CreateMap<FR66Transfer, SaveFR66TransferResource>().ReverseMap();
            CreateMap<FR66ToItem, SaveFR66ToItemResource>().ReverseMap();
            CreateMap<FR66FromItem, SaveFR66FromItemResource>().ReverseMap();
            CreateMap<CutProvision, SaveCutProvisionResource>().ReverseMap();
            CreateMap<Supplementary, SaveSupplementaryResource>().ReverseMap();

            CreateMap<AccountTransfer, SaveAccountTransferResource>().ReverseMap();
            CreateMap<AccountTransferRefunding, SaveAccountTransferRefundingResource>().ReverseMap();

            CreateMap<FR66Transfer, FR66TransferResource>().ReverseMap();
            CreateMap<FR66ToItem, FR66ToItemResource>().ReverseMap();
            CreateMap<FR66FromItem, FR66FromItemResource>().ReverseMap();
            CreateMap<CutProvision, CutProvisionResource>().ReverseMap();
            CreateMap<Supplementary,SupplementaryResource>().ReverseMap();
            CreateMap<AccountTransfer, AccountTransferResource>().ReverseMap();
            CreateMap<AccountTransferRefunding, AccountTransferRefundingResource>().ReverseMap();

            CreateMap<VoteDetail, VoteDetailLimitedresource>().ReverseMap();

            

            /*deposits */
            CreateMap<Deposit, DepositResource>().ReverseMap();
            CreateMap<Deposit, SaveDepositResource>().ReverseMap();
            CreateMap<DepositSubInfo, DepositSubInfoResource>().ReverseMap();
            CreateMap<SubImprest, SaveSubImprestResource>().ReverseMap();
            CreateMap<SubImprestSettlement, SaveSubImprestSettlementResource>().ReverseMap();

            CreateMap<SubImprest, SettleSubImprestResource>().ReverseMap();
            CreateMap<SubImprest, SubImprestResource>().ReverseMap();
            CreateMap<SubImprestSettlement, SubImprestSettlementResource>().ReverseMap();
            CreateMap<SettlementCrossOrder, SettlementCrossOrderResource>().ReverseMap();

            /*final account helper model mapping*/


            CreateMap<Partner, PayeeResources>()
                .ForMember(dest => dest.PayeeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PayeeName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PayeeCategory, opt => opt.MapFrom(src => VoucherPayeeCategory.Partner));

            CreateMap<Employee, PayeeResources>()
                .ForMember(dest => dest.PayeeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PayeeName, opt => opt.MapFrom(src => $"{src.Initials} {src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.PayeeCategory, opt => opt.MapFrom(src => VoucherPayeeCategory.Employee));


            CreateMap<Agents, PayeeResources>()
               .ForMember(dest => dest.PayeeId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.PayeeName, opt => opt.MapFrom(src => $"{src.Reference} {src.BankBranch!.BranchName} - {src.BankBranch!.Bank!.Description}"))
               .ForMember(dest => dest.PayeeCategory, opt => opt.MapFrom(src => VoucherPayeeCategory.Agent));

            CreateMap<Partner, CreditorDebtorResource>()
             .ForMember(dest => dest.CreditorDebtorId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest. CreditorDebtorName, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.PayeeCategory, opt => opt.MapFrom(src => VoucherPayeeCategory.Partner));



            CreateMap<Employee, CreditorDebtorResource>()
             .ForMember(dest => dest.CreditorDebtorId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreditorDebtorName, opt => opt.MapFrom(src => $"{src.Initials} {src.FirstName} {src.LastName}"))
             .ForMember(dest => dest.PayeeCategory, opt => opt.MapFrom(src => VoucherPayeeCategory.Partner));

            CreateMap<Agents, CreditorDebtorResource>()
             .ForMember(dest => dest.CreditorDebtorId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreditorDebtorName, opt => opt.MapFrom(src => src.Reference))
            .ForMember(dest => dest.PayeeCategory, opt => opt.MapFrom(src => VoucherPayeeCategory.Partner));

            /* FinalAccountActionStates Account Log Mapping */

            CreateMap<Commitment, CommitmentLog>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int?)null))
                    .ForMember(dest => dest.CommitmentId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<CommitmentLine, CommitmentLineLog>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int?)null))
                    .ForMember(dest => dest.CommitmentLineId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<CommitmentLineVotes, CommitmentLineVotesLog>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int?)null))
                   .ForMember(dest => dest.CommitmentLineVoteId, opt => opt.MapFrom(src => src.Id))
               .ReverseMap();



            CreateMap<Voucher, VoucherLog>().ReverseMap();
            CreateMap<VoucherLine, VoucherLineLog>().ReverseMap();
            CreateMap<VoteBalance, VoteBalanceLog>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int?)null))
                    .ForMember(dest => dest.VoteBalanceId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();


            /*cashbook*/

            CreateMap<CashBook, CashBookResource>().ReverseMap();


            //HRM Model Mappings

            CreateMap<Employee, EmployeeResource>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => $"{src.Initials} {src.FirstName} {src.MiddleName} {src.LastName}")).ReverseMap();
            CreateMap<Address, AddressResource>().ReverseMap();
            CreateMap<SpouserInfo, SpouserInfoResource>().ReverseMap();
            CreateMap<ChildInfo, ChildInfoResource>().ReverseMap();
            CreateMap<ServiceInfo, ServiceInfoResource>().ReverseMap();
            CreateMap<SalaryInfo, SalaryInfoResource>().ReverseMap();
            CreateMap<NetSalaryAgent, NetSalaryAgentResource>().ReverseMap();
            CreateMap<OtherRemittanceAgent, OtherRemittanceAgentResource>().ReverseMap();
            CreateMap<SupportingDocument, SupportingDocumentResource>().ReverseMap();

            CreateMap<EmployeeTypeData, EmployeeTypeDataResource>().ReverseMap();
            CreateMap<CarderStatusData, CarderStatusDataResource>().ReverseMap();
            CreateMap<SalaryStructureData, SalaryStructureDataResource>().ReverseMap();
            CreateMap<ServiceTypeData, ServiceTypeDataResource>().ReverseMap();
            CreateMap<JobTitleData, JobTitleDataResource>().ReverseMap();
            CreateMap<ClassLevelData, ClassLevelDataResource>().ReverseMap();
            CreateMap<GradeLevelData, GradeLevelDataResource>().ReverseMap();
            CreateMap<AgraharaCategoryData, AgraharaCategoryDataResource>().ReverseMap();
            CreateMap<AppointmentTypeData, AppointmentTypeDataResource>().ReverseMap();
            CreateMap<FundingSourceData, FundingSourceDataResource>().ReverseMap();
            CreateMap<SupportingDocTypeData, SupportingDocTypeDataResource>().ReverseMap();

            CreateMap<AdvanceB, AdvanceBResource>().ReverseMap();
            CreateMap<SaveAdvanceBResource, AdvanceB>()
                .ForMember(dest => dest.TransferInOrOutDate, opt => opt.MapFrom(src => src.TransferInOrOutDate.HasValue ? DateOnly.FromDateTime(src.TransferInOrOutDate.Value) : (DateOnly?)null))
                .ForMember(dest => dest.DeceasedDate, opt => opt.MapFrom(src => src.DeceasedDate.HasValue ? DateOnly.FromDateTime(src.DeceasedDate.Value) : (DateOnly?)null));

            CreateMap<AdvanceBSettlement, SaveAdvanceBSettlementResource>().ReverseMap();
            CreateMap<AdvanceBAttachment, AdvanceBAttachmentResource>().ReverseMap();
            CreateMap<AdvanceBSettlement, AdvanceBSettlementResource>().ReverseMap();
            CreateMap<AdvanceBTypeData, AdvanceBTypeDataResource>().ReverseMap();
            CreateMap<AdvanceBTypeLedgerMapping, AdvanceBTypeLedgerMappingResource>().ReverseMap();


            //
            // CreateMap<Commitment, SaveCommitmentResource>().ReverseMap()
            //     .ForMember(dest => dest.CommitmentLine, opt => opt.MapFrom(src => src.SaveCommitmentLineResource));
            //
            // CreateMap<CommitmentLine, SaveCommitmentLineResource>().ReverseMap();
            //
            // CreateMap<CommitmentLine, CommitmentLineResource>().ReverseMap();
            // CreateMap<CommitmentLine, SaveCommitmentLineResource>().ReverseMap();
            // CreateMap<CommitmentLine, CommitmentLineResource>().ReverseMap();



            // Resource to Domain

            //CreateMap<ProductResource, Product>();
            //CreateMap<SaveProductResource, Product>();

            //CreateMap<InvoiceResource, Invoice>().ForMember(x => x.InvoiceItems, x => x.MapFrom(x => x.InvoiceItemResource));
            //CreateMap<SaveInvoiceResource, Invoice>().ForMember(x => x.InvoiceItems, x => x.MapFrom(x => x.SaveInvoiceItemResource));

            //CreateMap<InvoiceItemResource, InvoiceItems>();
            //CreateMap<SaveInvoiceItemResource, InvoiceItems>();

            CreateMap<AccountBalanceDetailResource, AccountBalanceDetail>();
            CreateMap<SaveAccountBalanceDetailResource, AccountBalanceDetail>();

            CreateMap<AccountDetailResource, AccountDetail>();
            CreateMap<SaveAccountDetailResource, AccountDetail>();

            CreateMap<BalancesheetBalanceResource, BalancesheetBalance>();
            CreateMap<SaveBalancesheetBalanceResource, BalancesheetBalance>();

            CreateMap<BalancesheetSubtitleResource, BalancesheetSubtitle>();
            CreateMap<SaveBalancesheetSubtitleResource, BalancesheetSubtitle>();

            CreateMap<BalancesheetTitleResource, BalancesheetTitle>();
            CreateMap<SaveBalancesheetTitleResource, BalancesheetTitle>();

            CreateMap<ProjectResource, Project>();
            CreateMap<SaveProjectResource, Project>();

            CreateMap<SubProjectResource, SubProject>();
            CreateMap<SaveSubProjectResource, SubProject>();

            CreateMap<IncomeSubtitleResource, IncomeSubtitle>();
            CreateMap<SaveIncomeSubtitleResource, IncomeSubtitle>();

            CreateMap<IncomeTitleResource, IncomeTitle>();
            CreateMap<SaveIncomeTitleResource, IncomeTitle>();

            CreateMap<VoteBalanceResource, VoteBalance>();
            CreateMap<SaveVoteAllocationResource, VoteBalance>();

            //CreateMap<ExpenditureVoteAllocationResource, ExpenditureVoteAllocation>().ReverseMap();

            CreateMap<ProgrammeResource, Programme>();
            CreateMap<SaveProgrammeResource, Programme>();

            CreateMap<VoteDetailResource, VoteDetail>();
            CreateMap<SaveVoteDetailResource, VoteDetail>();

            CreateMap<UserDetailResource, UserDetail>();
            CreateMap<SaveUserDetailResource, UserDetail>();

            CreateMap<UserDetail, UserActionByResources>();
            CreateMap<UserDetail, FinalUserActionByResources>();


            CreateMap<LoginResource, UserDetail>();

            CreateMap<UserRecoverQuestionResource, UserRecoverQuestion>();
            CreateMap<SaveUserRecoverQuestionResource, UserRecoverQuestion>();

            //user
            CreateMap<RuleResource, Rule>();
            CreateMap<GroupResource, Group>();
            CreateMap<GroupRuleResource, GroupRule>();
            CreateMap<GroupUserResource, GroupUser>();

            //mixin
            CreateMap<VoteAssignmentResource, VoteAssignment>();
            CreateMap<HVoteAssignment, VoteAssignment>().ReverseMap();
            CreateMap<VoteAssignmentDetailsResource, VoteAssignmentDetails>();

            CreateMap<SaveMixinOrderResource, MixinOrder>();
            CreateMap<SaveMixinOrderLineResource, MixinOrderLine>();
            CreateMap<NewSaveMixinOrderLineResource, MixinOrderLine>();


            CreateMap<SaveAsssessmentOrderResource, MixinOrder>();
            CreateMap<OrderResponseResource, MixinOrder>().ReverseMap();

            CreateMap<SaveWaterBillOrderResource, MixinOrder>();
            CreateMap<SaveSurchargeOrderResource, MixinOrder>();


            //--------------[placeShopRentalOrder]----------------------------------------------
            //Note : modified : 2024/04/03
            CreateMap<SaveShoprentalOrderResource, MixinOrder>();
            //--------------[placeShopRentalOrder]----------------------------------------------


            CreateMap<CreditorBilling,CreditorBillingResource>().ReverseMap();
            CreateMap<SaveCreditorBillingResource, CreditorBilling>().ReverseMap();

            CreateMap<SaveSingleOpenBalanceResource, SingleOpenBalance>().ReverseMap();
            CreateMap<SingleOpenBalance, SingleOpenBalanceResource>().ReverseMap();


            CreateMap<ReceivableExchangeNonExchange, ReceivableExchangeNonExchgangeResource>().ReverseMap();
            CreateMap<ReceivableExchangeNonExchange, SaveReceivableExchangeNonExchangeResource>().ReverseMap();
            CreateMap<StoresCreditor, StoresCreditorResource>().ReverseMap();
            CreateMap<SaveStoresCreditor, StoresCreditor>().ReverseMap();

            CreateMap<IndustrialCreditors, IndustrialCreditorsResource>().ReverseMap();
            CreateMap<SaveIndustrialCreditorsResource,IndustrialCreditors>().ReverseMap();

            CreateMap<IndustrialDebtors, IndustrialDebtorsResource>().ReverseMap();
            CreateMap<SaveIndustrialDebtorsResource, IndustrialDebtors >().ReverseMap();

            CreateMap<PrepaidPayment, PrePaidPaymentsResource>().ReverseMap();
            CreateMap<SavePrepaidPaymentResource, PrepaidPayment>().ReverseMap();

            CreateMap<LALoan, LALoanResource>().ReverseMap();
            CreateMap<SaveLALoanResource, LALoan>().ReverseMap();
            CreateMap<SaveAgentsResource, Agents>().ReverseMap();


            CreateMap<FixedDeposit, FixedDepositResource>().ReverseMap();
            CreateMap<SaveFixedDepositResource, FixedDeposit>().ReverseMap();
            CreateMap<FixedAssets, FixedAssetsResource>().ForMember(dest => dest.BalanceTypeString, opt => opt.MapFrom(src => src.BalanceType == FixedAssetsBalanceTypes.Original ? "Original" : src.BalanceType == FixedAssetsBalanceTypes.Revalue  ? "Revalue" : "Unknown" ))
     .ReverseMap();

            CreateMap<SaveFixedAssetsResource, FixedAssets>().ReverseMap();
            CreateMap<Classification, ClassificatioResource>().ReverseMap();
            CreateMap<MainLedgerAccount, MainLedgerAccountResource>().ReverseMap();
        }
    }
}

