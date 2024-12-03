using CAT20.Core;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Repositories.AssessmentTax;
using CAT20.Core.Repositories.Common;
using CAT20.Core.Repositories.Control;
using CAT20.Core.Repositories.FinalAccount;
using CAT20.Core.Repositories.ShopRental;
using CAT20.Data.Repositories;
using CAT20.Data.Repositories.AssessmentTax;
using CAT20.Data.Repositories.Common;
using CAT20.Data.Repositories.Control;
using CAT20.Data.Repositories.FinalAccount;
using CAT20.Data.Repositories.ShopRental;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace CAT20.Data
{
    public class ControlUnitOfWork : IControlUnitOfWork
    {
        private readonly ControlDbContext _ccontext;
        private readonly AssessmentTaxDbContext _assmtcontext;
        private readonly ShopRentalDbContext _shopcontext;
        private readonly VoteAccDbContext _votecontext;

        private AppCategoryRepository _appCategoryRepository;
        private BankDetailRepository _bankDetailRepository;
        private DistrictRepository _districtRepository;
        private GenderRepository _genderRepository;
        private LanguageRepository _languageRepository;
        private MonthRepository _monthRepository;
        private OfficeRepository _officeRepository;
        private OfficeTypeRepository _officeTypeRepository;
        private ProvinceRepository _provinceRepository;
        private SabhaRepository _sabhaRepository;
        private SelectedLanguageRepository _selectedLanguageRepository;
        private YearRepository _yearRepository;
        private AuditLogRepository _auditLogRepository;
        private EmailOutBoxRepository _emailOutBoxRepository;
        private EmailConfigurationRepository _configurationRepository;
        private GnDivisionsRepository _gnDivisionsRepository;
        private PaymentVatRepository _paymentVatRepository;
        private PaymentNbtRepository _paymentNbtRepository;
        private PartnerRepository _partnerRepository;
        private DocumentSequenceNumberRepository _documentSequenceNumberRepository;
        private SessionRepository _sessionRepository;
        private TaxTypeRepository _taxTypeRepository;
        private BusinessRepository _businessRepository;
        private BusinessTaxRepository _businessTaxRepository;
        private BusinessPlaceRepository _businessPlaceRepository;
        private SMSOutBoxRepository _smsOutBoxRepository;
        private SMSConfigurationRepository _smsconfigurationRepository;
        private PartnerMobileRepository _partnerMobileRepository;
        private PartnerDocumentRepository _partnerDocumentRepository;

        private AgentsRepository _agentsRepository;

        //linking repository

        private AssessmentRepository _AssessmentRepository;
        private AssessmentProcessRepository _AssessmentProcessLogRepository;

        //linking shop rental repository
        private ShopRepository _shopRepository;
        private ShopRentalBalanceRepository _shopRentalBalanceRepository;
        private ShopRentalProcessRepository _shopRentalProcessRepository;
        private ShopRentalProcessConfigarationRepository _shopRentalProcessConfigarationRepository;

        private FinalAccountSequenceNumberRepository _finalAccountSequenceNumberRepository;
        private BankBranchRepository _bankBranchRepository;

        public ControlUnitOfWork(ControlDbContext context, AssessmentTaxDbContext assmtcontext, ShopRentalDbContext shopcontext, VoteAccDbContext votecontext)
        {
            _ccontext = context;
            _assmtcontext = assmtcontext;
            _shopcontext = shopcontext;
            _votecontext = votecontext;
        }

        public IAppCategoryRepository AppCategories => _appCategoryRepository = _appCategoryRepository ?? new AppCategoryRepository(_ccontext);
        public IBankDetailRepository BankDetails => _bankDetailRepository = _bankDetailRepository ?? new BankDetailRepository(_ccontext);
        public IDistrictRepository Districts => _districtRepository = _districtRepository ?? new DistrictRepository(_ccontext);
        public IGenderRepository Genders => _genderRepository = _genderRepository ?? new GenderRepository(_ccontext);
        public ILanguageRepository Languages => _languageRepository = _languageRepository ?? new LanguageRepository(_ccontext);
        public IMonthRepository Months => _monthRepository = _monthRepository ?? new MonthRepository(_ccontext);
        public IOfficeRepository Offices => _officeRepository = _officeRepository ?? new OfficeRepository(_ccontext);
        public IOfficeTypeRepository OfficeTypes => _officeTypeRepository = _officeTypeRepository ?? new OfficeTypeRepository(_ccontext);
        public IProvinceRepository Provinces => _provinceRepository = _provinceRepository ?? new ProvinceRepository(_ccontext);
        public ISabhaRepository Sabhas => _sabhaRepository = _sabhaRepository ?? new SabhaRepository(_ccontext);
        public ISelectedLanguageRepository SelectedLanguages => _selectedLanguageRepository = _selectedLanguageRepository ?? new SelectedLanguageRepository(_ccontext);
        public IYearRepository Years => _yearRepository = _yearRepository ?? new YearRepository(_ccontext);
        public IAuditLogRepository AuditLogs => _auditLogRepository = _auditLogRepository ?? new AuditLogRepository(_ccontext);
        public IEmailOutBoxRepository EmailOutBoxes => _emailOutBoxRepository = _emailOutBoxRepository ?? new EmailOutBoxRepository(_ccontext);
        public IEmailConfigurationRepository EmailConfigurations => _configurationRepository = _configurationRepository ?? new EmailConfigurationRepository(_ccontext);
        public IGnDivisionsRepository GnDivisions => _gnDivisionsRepository = _gnDivisionsRepository ?? new GnDivisionsRepository(_ccontext);
        public IPaymentNbtRepository PaymentNbts => _paymentNbtRepository = _paymentNbtRepository ?? new PaymentNbtRepository(_ccontext);
        public IPaymentVatRepository PaymentVats => _paymentVatRepository = _paymentVatRepository ?? new PaymentVatRepository(_ccontext);
        public IPartnerRepository Partners => _partnerRepository = _partnerRepository ?? new PartnerRepository(_ccontext);
        public IDocumentSequenceNumberRepository DocumentSequenceNumbers => _documentSequenceNumberRepository = _documentSequenceNumberRepository ?? new DocumentSequenceNumberRepository(_ccontext);
        public ISessionRepository Sessions => _sessionRepository = _sessionRepository ?? new SessionRepository(_ccontext);
        public ITaxTypeRepository TaxTypes => _taxTypeRepository = _taxTypeRepository ?? new TaxTypeRepository(_ccontext);
        public IBusinessRepository Businesses => _businessRepository = _businessRepository ?? new BusinessRepository(_ccontext);
        public IBusinessTaxRepository BusinessTaxes => _businessTaxRepository = _businessTaxRepository ?? new BusinessTaxRepository(_ccontext);
        public IBusinessPlaceRepository BusinessPlaces => _businessPlaceRepository = _businessPlaceRepository ?? new BusinessPlaceRepository(_ccontext);
        public ISMSOutBoxRepository SMSOutBoxes => _smsOutBoxRepository = _smsOutBoxRepository ?? new SMSOutBoxRepository(_ccontext);
        public ISMSConfigurationRepository SMSConfigurations => _smsconfigurationRepository = _smsconfigurationRepository ?? new SMSConfigurationRepository(_ccontext);

        public IPartnerMobileRepository PartnerMobiles => _partnerMobileRepository = _partnerMobileRepository ?? new PartnerMobileRepository(_ccontext);
        public IPartnerDocumentRepository PartnerDocuments => _partnerDocumentRepository = _partnerDocumentRepository ?? new PartnerDocumentRepository(_ccontext);

        public IAgentsRepository Agents => _agentsRepository = _agentsRepository ?? new AgentsRepository(_ccontext);

        public IBankBranchRepository BankBranches => _bankBranchRepository = _bankBranchRepository ?? new BankBranchRepository(_ccontext);

        //linking repository 

        public IAssessmentRepository Assessments => _AssessmentRepository = _AssessmentRepository ?? new AssessmentRepository(_assmtcontext);
        public IAssessmentProcessRepository AssessmentProcesses => _AssessmentProcessLogRepository = _AssessmentProcessLogRepository ?? new AssessmentProcessRepository(_assmtcontext);

        //linking shop repository
        public IShopRepository Shop => _shopRepository = _shopRepository ?? new ShopRepository(_shopcontext);
        public IShopRentalBalanceRepository ShopRentalBalance => _shopRentalBalanceRepository = _shopRentalBalanceRepository ?? new ShopRentalBalanceRepository(_shopcontext);
        public IShopRentalProcessRepository ShopRentalProcess => _shopRentalProcessRepository = _shopRentalProcessRepository ?? new ShopRentalProcessRepository(_shopcontext);
        public IShopRentalProcessConfigarationRepository ShopRentalProcessConfigaration => _shopRentalProcessConfigarationRepository = _shopRentalProcessConfigarationRepository ?? new ShopRentalProcessConfigarationRepository(_shopcontext);


        public IFinalAccountSequenceNumberRepository FinalAccountSequenceNumber => _finalAccountSequenceNumberRepository = _finalAccountSequenceNumberRepository ?? new FinalAccountSequenceNumberRepository(_votecontext);
 

        //public async Task<int> CommitAsync()
        //{
        //    return await _context.SaveChangesAsync();
        //}

        ////public void Dispose()
        ////{
        ////    _context.Dispose();
        ////}
        //private bool _disposed = false;
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!_disposed)
        //    {
        //        if (disposing)
        //        {
        //            _context.Dispose();
        //        }
        //        _disposed = true;
        //    }
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //public async ValueTask DisposeAsync()
        //{
        //    Dispose();
        //    await Task.CompletedTask;
        //}

        public IDbTransaction BeginTransaction()
        {
            var transaction = _ccontext.Database.BeginTransaction();
            return transaction.GetDbTransaction();
        }

        public async Task<IDbTransaction> BeginTransactionAsync()
        {
            var transaction = await _ccontext.Database.BeginTransactionAsync();
            return transaction.GetDbTransaction();
        }

        public async Task<int> CommitAsync()
        {
            // Use the execution strategy returned by DbContext.Database.CreateExecutionStrategy()
            var strategy = _assmtcontext.Database.CreateExecutionStrategy();

            // Execute the SaveChangesAsync calls within the execution strategy
            return await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _assmtcontext.Database.BeginTransaction())
                {
                    try
                    {
                        int result = await _assmtcontext.SaveChangesAsync();

                        result += await _shopcontext.SaveChangesAsync();

                        result += await _ccontext.SaveChangesAsync();
                        result += await _votecontext.SaveChangesAsync();
                        //result += await _wbcontext.SaveChangesAsync();
                        //result += await _mixincontext.SaveChangesAsync();

                        // If all SaveChangesAsync calls were successful, commit the transaction
                        transaction.Commit();

                        return result;
                    }
                    catch (Exception ex)
                    {
                        // If an exception occurs, roll back the transaction
                        transaction.Rollback();
                        throw;
                    }
                }
            });
        }

        public void Dispose()
        {
            _ccontext.Dispose();
            _assmtcontext.Dispose();
            _shopcontext.Dispose();
            _votecontext.Dispose();
        }

    }
}