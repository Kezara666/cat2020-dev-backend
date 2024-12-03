using CAT20.Core.Models.Control;
using CAT20.Core.Repositories.AssessmentTax;
using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.FinalAccount;
using CAT20.Core.Repositories.ShopRental;
using System;
using System.Data;
using System.Threading.Tasks;

namespace CAT20.Core
{
    public interface IControlUnitOfWork : IDisposable
    {
        IAppCategoryRepository AppCategories { get; }
        IBankDetailRepository BankDetails { get; }
        IDistrictRepository Districts { get; }
        IGenderRepository Genders { get; }
        ILanguageRepository Languages { get; }
        IMonthRepository Months { get; }
        IOfficeRepository Offices { get; }
        IOfficeTypeRepository OfficeTypes { get; }
        IProvinceRepository Provinces { get; }
        ISabhaRepository Sabhas { get; }
        ISelectedLanguageRepository SelectedLanguages { get; }
        IYearRepository Years { get; }
        IEmailOutBoxRepository  EmailOutBoxes { get; }
        IEmailConfigurationRepository EmailConfigurations { get; }
        IGnDivisionsRepository GnDivisions { get; }
        IPaymentVatRepository PaymentVats { get; }
        IPaymentNbtRepository PaymentNbts { get; }
        IPartnerRepository Partners { get; }
        IDocumentSequenceNumberRepository DocumentSequenceNumbers { get; }
        ISessionRepository Sessions { get; }
        ITaxTypeRepository TaxTypes { get; }
        IBusinessRepository Businesses { get; }
        IBusinessTaxRepository BusinessTaxes { get; }
        IBusinessPlaceRepository BusinessPlaces { get; }
        ISMSOutBoxRepository SMSOutBoxes { get; }
        ISMSConfigurationRepository SMSConfigurations { get; }
        IPartnerMobileRepository PartnerMobiles { get; }
        IPartnerDocumentRepository PartnerDocuments { get; }

        IAgentsRepository Agents { get; }

        //linking repository 
        IAssessmentRepository Assessments { get; }
        IAssessmentProcessRepository AssessmentProcesses { get; }

        //linking shop rental repository
        IShopRepository Shop { get; }
        IShopRentalBalanceRepository ShopRentalBalance { get; }
        IShopRentalProcessRepository ShopRentalProcess { get; }
        IShopRentalProcessConfigarationRepository ShopRentalProcessConfigaration {  get; }

        IFinalAccountSequenceNumberRepository FinalAccountSequenceNumber { get; }
        IBankBranchRepository BankBranches { get; }

        Task<int> CommitAsync();
        IDbTransaction BeginTransaction();
        Task<IDbTransaction> BeginTransactionAsync();

    }
}
