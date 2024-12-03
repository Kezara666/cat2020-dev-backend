using CAT20.Core;
using CAT20.Core.Services.AssessmentTax;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.FinalAccount;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Services.OnlinePayment;
using CAT20.Core.Services.ShopRental;
using CAT20.Core.Services.TradeTax;
using CAT20.Core.Services.User;
using CAT20.Core.Services.Vote;
using CAT20.Core.Services.WaterBilling;
using CAT20.Core.Services.AuditTrails;
using CAT20.Data;
using CAT20.Services.AssessmentTax;
using CAT20.Services.Control;
using CAT20.Services.FinalAccount;
using CAT20.Services.Mixin;
using CAT20.Services.OnlinePayment;
using CAT20.Services.ShopRental;
using CAT20.Services.TradeTax;
using CAT20.Services.User;
using CAT20.Services.Vote;
using CAT20.Services.WaterBilling;
using CAT20.WebApi.Configuration;
using CAT20.WebApi.CustomMiddlewares;
using CAT20.WebApi.Resources.WaterBilling;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using App.Metrics.Formatters.Prometheus;
using DinkToPdf.Contracts;
using DinkToPdf;
using System.Text.Json;
using CAT20.Core.Services.AuditTrails;
using CAT20.Services.AuditTrails;
using CAT20.Core.Services.AssessmentAuditActivity;
using CAT20.Services.AssessmentAuditActivity;
using CAT20.Core.Services.HRM.PersonalFile;
using CAT20.Services.HRM.PersonalFile;
using CAT20.Core.Repositories.FinalAccount;
using CAT20.Core.Services.HRM.LoanManagement;
using CAT20.Services.HRM.LoanManagement;

namespace CAT20.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration
        {
            get;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options => {
                options.AllowSynchronousIO = true;
            });
            services.AddMetrics();
            services.AddLogging();

            // DataBase Context Registering
            AddDbContext<ControlDbContext>("control");
            AddDbContext<VoteAccDbContext>("vote");
            AddDbContext<UserActivityDBContext>("user");
            AddDbContext<MixinDbContext>("mixin");
            AddDbContext<ShopRentalDbContext>("shoprental");
            AddDbContext<WaterBillingDbContext>("waterbilling");
            AddDbContext<AssessmentTaxDbContext>("assessment");
            AddDbContext<OnlinePaymentDbContext>("online");
            AddDbContext<AuditTrailDbContext>("audittrail");
            AddDbContext<HRMDbContext>("hrm");

            // Define a method to add DbContext with common configurations
            void AddDbContext<TContext>(string connectionStringName) where TContext : DbContext
            {
                services.AddDbContext<TContext>(options => options
                  .UseMySql(Configuration.GetConnectionString(connectionStringName), ServerVersion.AutoDetect(Configuration.GetConnectionString(connectionStringName)))
                  .LogTo(Console.WriteLine, LogLevel.Information)
                  .LogTo(Console.WriteLine, LogLevel.Error)
                  .LogTo(Console.WriteLine, LogLevel.Critical))
                  .AddScoped<TContext>();
            }

            // Scheduler Service
            // services.AddHostedService<RepeatingService>();

            // Registering Unit of works
            services.AddTransient<IControlUnitOfWork, ControlUnitOfWork>();
            services.AddTransient<IVoteUnitOfWork, VoteUnitOfWork>();
            services.AddTransient<IUserUnitOfWork, UserUnitOfWork>();
            services.AddTransient<IMixinUnitOfWork, MixinUnitOfWork>();
            services.AddTransient<IShopRentalUnitOfWork, ShopRentalUnitOfWork>();
            services.AddTransient<IWaterBillingUnitOfWork, WaterBillingUnitOfWork>();
            services.AddTransient<IAssessmentTaxUnitOfWork, AssessmentTaxUnitOfWork>();
            services.AddTransient<IOnlinePaymentUnitOfWork, OnlinePaymentUnitOfWork>();
            services.AddTransient<IAuditTrailUnitOfWork, AuditTrailUnitOfWork>();
            services.AddTransient<IHRMUnitOfWork, HRMUnitOfWork>();

            //services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));  //dinktopdf

            services.AddControllers().AddNewtonsoftJson();
            services.AddControllers();

            //updated for prometheus
            services.AddMetrics();
            services.AddMetricsEndpoints(options => {
                options.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                options.EnvironmentInfoEndpointEnabled = false;
            });

            services.AddSwaggerGen(options => {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            services.AddAuthentication(option => {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option => {
                option.RequireHttpsMetadata = false;
                option.SaveToken = true;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Jwt:Key").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.Configure<FormOptions>(options => {
                //options.MultipartBodyLengthLimit = 104857600; // Set your desired maximum size in bytes
                //options.MultipartBodyLengthLimit = 130 * 1024 * 1024; // Set your desired maximum size in bytes
                options.MultipartBodyLengthLimit = 134217728; // Set your desired maximum size in bytes
            });

            services.Configure<IISServerOptions>(options => {
                options.MaxRequestBodySize = null;
                options.AllowSynchronousIO = true;
                options.MaxRequestBodyBufferSize = 100;
            });

            services.Configure<KestrelServerOptions>(options => {
                options.Limits.MaxRequestBodySize = null; // Unlimited request body size
                options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(10); // Set the request headers timeout
                                                                                 //options.Listen(IPAddress.Any, 5100);
            });

            services.AddEndpointsApiExplorer();

            //Control Services
            services.AddTransient<IAppCategoryService, AppCategoryService>();
            services.AddTransient<IBankDetailService, BankDetailService>();
            services.AddTransient<IDistrictService, DistrictService>();
            services.AddTransient<IGenderService, GenderService>();
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<IMonthService, MonthService>();
            services.AddTransient<IOfficeService, OfficeService>();
            services.AddTransient<IOfficeTypeService, OfficeTypeService>();
            services.AddTransient<IProvinceService, ProvinceService>();
            services.AddTransient<ISabhaService, SabhaService>();
            services.AddTransient<ISelectedLanguageService, SelectedLanguageService>();
            services.AddTransient<IYearService, YearService>();
            services.AddTransient<IEmailOutBoxService, EmailOutBoxService>();
            services.AddTransient<IEmailConfigurationService, EmailConfigurationService>();
            services.AddTransient<IGnDivisionService, GnDivisionService>();
            services.AddTransient<IPaymentNbtService, PaymentNbtService>();
            services.AddTransient<IPaymentVatService, PaymentVatService>();
            services.AddTransient<IPartnerService, PartnerService>();
            services.AddTransient<IDocumentSequenceNumberService, DocumentSequenceNumberService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<ITaxTypeService, TaxTypeService>();
            services.AddTransient<ISMSOutBoxService, SMSOutBoxService>();
            services.AddTransient<ISMSConfigurationService, SMSConfigurationService>();
            services.AddTransient<IAgentsService, AgentsService>();
            services.AddTransient<IBankBranchService, BankBranchService>();

            //Vote Services
            services.AddTransient<IAccountBalanceDetailService, AccountBalanceDetailService>();
            services.AddTransient<IAccountDetailService, AccountDetailService>();
            services.AddTransient<IBalancesheetBalanceService, BalancesheetBalanceService>();
            services.AddTransient<IBalancesheetSubtitleService, BalancesheetSubtitleService>();
            services.AddTransient<IBalancesheetTitleService, BalancesheetTitleService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<ISubProjectService, SubProjectService>();
            services.AddTransient<IIncomeSubtitleService, IncomeSubtitleService>();
            services.AddTransient<IIncomeTitleService, IncomeTitleService>();
            services.AddTransient<IVoteBalanceService, VoteBalanceService>();
            services.AddTransient<IProgrammeService, ProgrammeService>();
            services.AddTransient<IVoteDetailService, VoteDetailService>();
            services.AddTransient<ICustomVoteEntryService, CustomVoteEntryService>();
            services.AddTransient<ICustomVoteBalanceService, CustomVoteBalanceService>();

            //Trade Tax
            services.AddTransient<IBusinessNatureService, BusinessNatureService>();
            services.AddTransient<IBusinessSubNatureService, BusinessSubNatureService>();
            services.AddTransient<ITaxValueService, TaxValueService>();
            services.AddTransient<ITradeTaxVoteAssignmentService, TradeTaxVoteAssignmentService>();
            services.AddTransient<IBusinessService, BusinessService>();
            services.AddTransient<IBusinessTaxService, BusinessTaxService>();
            services.AddTransient<IBusinessPlaceService, BusinessPlaceService>();

            //User Services
            services.AddTransient<IUserDetailService, UserDetailService>();
            services.AddTransient<IRuleService, RuleService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IGroupUserService, GroupUserService>();
            services.AddTransient<IGroupRuleService, GroupRuleService>();
            services.AddTransient<IUserRecoverQuestionService, UserRecoverQuestionService>();
            services.AddTransient<IUserLoginActivityService, UserLoginActivityService>();

            //Mixin Services
            services.AddTransient<IVoteAssignmentService, VoteAssignmentService>();
            services.AddTransient<IVoteAssignmentDetailsService, VoteAssignmentDetailsService>();
            services.AddTransient<ICustomVoteSubLevel1Service, CustomVoteSubLevel1Service>();
            services.AddTransient<ICustomVoteSubLevel2Service, CustomVoteSubLevel2Service>();
            services.AddTransient<IMixinOrderService, MixinOrderService>();
            services.AddTransient<IMixinOrderLineService, MixinOrderLineService>();
            services.AddTransient<IMixinCancelOrderService, MixinCancelOrderService>();
            services.AddTransient<IBankingService, BankingService>();
            services.AddTransient<IMixinVoteBalanceService, MixinVoteBalanceService>();

            services.AddTransient<IAssessmentCancelOrderService, AssessmentCancelOrderService>();
            services.AddTransient<IWaterBillCancelOrderService, WaterBillCancelOrderService>();
            // services.AddTransient<IWaterBillCancelOrderService, WaterBillCancelOrderService>();

            //--------------[cancelShopRentalOrder]----------------------------------------------
            //Note : modified : 2024/04/09
            services.AddTransient<IShopRentalCancelOrderService, ShopRentalCancelOrderService>();
            services.AddTransient<IShopRentalProcessPaymentService, ShopRentalProcessPaymentService>();
            //--------------[cancelShopRentalOrder]----------------------------------------------

            //ShopRental
            services.AddTransient<IRentalPlaceService, RentalPlaceService>();
            services.AddTransient<IFloorService, FloorService>();
            services.AddTransient<IPropertyTypeService, PropertyTypeService>();
            services.AddTransient<IPropertyNatureService, PropertyNatureService>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<IShopService, ShopService>();
            services.AddTransient<IShopRentalOpeningBalanceService, ShopRentalOpeningBalanceService>();
            services.AddTransient<IShopRentalBalanceService, ShopRentalBalanceService>();
            services.AddTransient<IShopRentalVoteAssignService, ShopRentalVoteAssignService>();
            services.AddTransient<IShopRentalVotePaymentTypeService, ShopRentalVotePaymentTypeService>();
            services.AddTransient<IShopRentalProcessConfigarationService, ShopRentalProcessConfigarationService>();
            services.AddTransient<IFineRateTypeService, FineRateTypeService>();
            services.AddTransient<IFineCalTypeService, FineCalTypeService>();
            services.AddTransient<IRentalPaymentDateTypeService, RentalPaymentDateTypeService>();
            services.AddTransient<IFineChargingMethodService, FineChargingMethodService>();
            services.AddTransient<IProcessConfigurationSettingAssignService, ProcessConfigurationSettingAssignService>();
            services.AddTransient<IShopAgreementChangeRequestService, ShopAgreementChangeRequestService>();
            services.AddTransient<IShopRentalProcessService, ShopRentalProcessService>();
            services.AddTransient<IShopRentalReceivableIncomeVoteAssignService, ShopRentalRecievableIncomeVoteAssignService>();
            services.AddTransient<IDailyFineProcessLogService, DailyFineProcessLogService>();
            services.AddTransient<IShopAgreementActivityLogService, ShopAgreementActivityLogService>();

            //WaterBilling

            services.AddTransient<IWaterProjectMainRoadService, WaterProjectMainRoadService>();
            services.AddTransient<IWaterProjectSubRoadService, WaterProjectSubRoadService>();
            services.AddTransient<IWaterProjectNatureService, WaterProjectNatureService>();
            services.AddTransient<IWaterProjectService, WaterProjectService>();
            services.AddTransient<IWaterTariffService, WaterTariffService>();

            services.AddTransient<INonMeterFixChargeService, NonMeterFixChargeService>();
            services.AddTransient<IWaterProjectGnDivisionService, WaterProjectGnDivisionService>();
            services.AddTransient<IMeterReaderAssignService, MeterReaderAssignService>();
            services.AddTransient<IMeterConnectInfoService, MeterConnectInfoService>();

            services.AddTransient<IVoteAssignService, VoteAssignService>();
            services.AddTransient<IWaterBillPaymentCategoryService, WaterBillPaymentCategoryService>();

            services.AddTransient<IApplicationForConnectionService, ApplicationForConnectionService>();
            services.AddTransient<IApplicationForConnectionDocumentsService, ApplicationForConnectionDocumentsService>();

            services.AddTransient<IWaterBillDocumentService, WaterBillDocumentService>();
            services.AddTransient<IWaterConnectionService, WaterConnectionService>();
            services.AddTransient<IWaterConnectionNatureLogService, WaterConnectionNatureLogService>();
            services.AddTransient<IWaterConnectionStatusLogService, WaterConnectionStatusLogService>();

            services.AddTransient<IOpeningBalanceInformationService, OpeningBalanceInformationService>();
            services.AddTransient<IWaterConnectionAuditLogService, WaterConnectionAuditLogService>();

            services.AddTransient<IWaterConnectionBalanceService, WaterConnectionBalanceService>();
            services.AddTransient<IWaterConnectionBalanceHistoryService, WaterConnectionBalanceHistoryService>();
            services.AddTransient<IWaterMonthEndReportService, WaterMonthEndReportService>();

            //OnlinePaymentService Service
            services.AddTransient<IOnlinePaymentService, OnlinePaymentService>();
            services.AddTransient<IBookingPropertyService, BookingPropertyService>();
            services.AddTransient<IBookingSubPropertyService, BookingSubPropertyService>();
            services.AddTransient<IBookingTimeSlotService, BookingTimeSlotsService>();
            services.AddTransient<IChargingSchemeService, BookingChargingSchemeService>();
            services.AddTransient<IBookingDateService, BookingDateService>();

            services.AddTransient<IOnlineBookingService, OnlineBookingService>();
            //Assessment Tax
            services.AddTransient<IWardService, WardService>();
            services.AddTransient<IStreetService, StreetService>();
            services.AddTransient<IAssessmentPropertyTypeService, AssessmentPropertyTypeService>();
            services.AddTransient<IDescriptionService, DescriptionService>();
            services.AddTransient<IAssessmentService, AssessmentService>();
            services.AddTransient<IAssessmentBalanceService, AssessmentBalanceService>();
            services.AddTransient<IAssessmentTempSubPartnerService, SubOwnerService>();
            services.AddTransient<IAllocationService, AllocationService>();
            services.AddTransient<IAssessmetTempPartnerService, AssessmetTempPartnerService>();
            services.AddTransient<IAssmtVoteAssignService, AssmtVoteAssignService>();
            services.AddTransient<IAssmtVotePaymentTypeService, AssmVoteTypeService>();

            services.AddTransient<IAssessmentRatesService, AssessmentRatesService>();

            services.AddTransient<IAssessmentTransactionService, AssessmentTransactionService>();
            services.AddTransient<IAssessmentDescriptionLogService, AssessmentDescriptionLogService>();
            services.AddTransient<IAssessmentPropertyTypeLogService, AssessmentPropertyTypeLogService>();

            services.AddTransient<IAssessmentJournalService, AssessmentJournalService>();
            services.AddTransient<IAssessmentEndSessionService, AssessmentEndSessionService>();
            services.AddTransient<IAssessmentAssetsChangeService, AssessmentAssetsChangeService>();
            services.AddTransient<IAssessmentAuditLogService, AssessmentAuditLogService>();
            services.AddTransient<IAssessmentQuarterReportService, AssessmentQuarterReportService>();
            services.AddTransient<IAssessmentBillAdjustmentService, AssessmentBillAdjustmentService>();
            services.AddTransient<IAssessmentRenewalService, AssessmentRenewalService>();
            services.AddTransient<IAmalgamationSubDivisionService, AmalgamationSubDivisionService>();
            services.AddTransient<IAssessmentATDService, AssessmentATDService>();


            /*assessment user activity*/
            services.AddTransient<IAssessmentUserActivityService, AssessmentUserActivityService>();

            /* additional services ignoring monolithic  */

            services.AddTransient<IAssessmentProcessService, AssessmentProcessService>();
            services.AddTransient<IAssessmentWarrantService, AssessmentWarrantService>();
            services.AddTransient<IAssessmentPartnerSearchService, AssessmentPartnerSearchService>();

            services.AddSingleton<IConverter>(new SynchronizedConverter(new PdfTools()));
            services.AddMvc().AddControllersAsServices();
            services.AddTransient<IDocumentService, DocumentService>();

            // Register GlobalExceptionHandlingMiddleware

            //Final Account
            services.AddTransient<ICommitmentService, CommitmentService>();
            services.AddTransient<ICommitmentLineService, CommitmentLineService>();
            services.AddTransient<IVoucherService, VoucherService>();
            services.AddTransient<IVoucherInvoiceService, VoucherInvoiceService>();
            services.AddTransient<IVoucherDocumentService, VoucherDocumentService>();



            services.AddTransient<IVoucherChequeService, VoucherChequeService>();
            services.AddTransient<IDepositSubInfoService, DepositSubInfoService>();
            services.AddTransient<IFinalAccountSequenceNumberService, FinalAccountSequenceNumberService>();
            
            services.AddTransient<IVoteLedgerBookDailyBalanceService, VoteLedgerBookDailyBalanceService>();
            services.AddTransient<IVoteLedgerBookService, VoteLedgerBookService>();
            services.AddTransient<ICashBookDailyBalanceService, CashBookDailyBalanceService>();
            services.AddTransient<ICashBookService, CashBookService>();
            services.AddTransient<IDepositService, DepositService>();
           
            
            
            services.AddTransient<IVoteJournalAdjustmentService, VoteJournalAdjustmentService>();
            services.AddTransient<IFR66TransferService, FR66TransferService>();
            services.AddTransient<ICutProvisionService, CutProvisionService>();
            services.AddTransient<ISupplementaryService, SupplementaryService>();
            services.AddTransient<IAccountTransferService, AccountTransferService>();
            services.AddTransient<IAccountTransferRefundingService, AccountTransferRefundingService>();



            services.AddTransient<IDepositService, DepositService>();
            services.AddTransient<ISubImprestService, SubImprestService>();
            services.AddTransient<ISubImprestSettlementService, SubImprestSettlementService>();


            services.AddTransient<ISpecialLedgerAccountsService, SpecialLedgerAccountsService>();



            /*final account linking with other module  */
            services.AddTransient<IAssessmentFinalAccountService, AssessmentFinalAccountService>();
            services.AddTransient<IShopRentalFinalAccountService, ShopRentalFinalAccountService>();
            services.AddTransient<IWaterFinalAccountService, WaterFinalAccountService>();

            /*final account control service*/
            services.AddTransient<ISabhaFundSourceService, SabhaFundSourceService>();
            services.AddTransient<IIndustrialCreditorsDebtorsTypesService, IndustrialCreditorsDebtorsTypesService>();
            //.......................................//
            //final account opening balances

            services.AddTransient<ICreditorsBillingService, CreditorsBillingService>();
            services.AddTransient<IFixedDepositService, FixedDepositService>();
            services.AddTransient<IIndustrialCreditorsService, IndustrialCreditorsService>();
            services.AddTransient<IIndustrialDebtorsService, IndustrialDebtorsService>();
            services.AddTransient<ILABankLoanService, LABankLoanService>();
            services.AddTransient<IPrepaidPaymentService, PrepaidPaymentService>();
            services.AddTransient<IReceivableExchangeNonExchangeService, ReceivableExchangeNonExchangeService>();
            services.AddTransient<IStoreCreditorsService, StoreCreditorsService>();
            services.AddTransient<IFixedAssetsService, FixedAssetsService>();
            services.AddTransient<ISingleOpenBalanceService, SingleOpenBalanceService>();
            services.AddTransient<IFinalAccountAdjustmentService, FinalAccountAdjustmentService>();

            services.AddTransient<IClassificationService, ClassificationService>();
            services.AddTransient<IBudgetService, BudgetService>();


            //.................................//


            // Audit Trails
            services.AddTransient<IAuditTrailService, AuditTrailService>();
            services.AddTransient<IAuditTrailDetailService, AuditTrailDetailService>();


            // HRM
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IEmployeeTypeDataService, EmployeeTypeDataService>();
            services.AddTransient<ICarderStatusDataService, CarderStatusDataService>();
            services.AddTransient<ISalaryStructureDataService, SalaryStructureDataService>();
            services.AddTransient<IServiceTypeDataService, ServiceTypeDataService>();
            services.AddTransient<IClassLevelDataService, ClassLevelDataService>();
            services.AddTransient<IGradeLevelDataService, GradeLevelDataService>();
            services.AddTransient<IJobTitleDataService, JobTitleDataService>();
            services.AddTransient<IAgraharaCategoryDataService, AgraharaCategoryDataService>();
            services.AddTransient<IAppointmentTypeDataService, AppointmentTypeDataService>();
            services.AddTransient<IFundingSourceDataService, FundingSourceDataService>();
            services.AddTransient<ISupportingDocTypeDataService, SupportingDocTypeDataService>();

            services.AddTransient<IAdvanceBService, AdvanceBService>();
            services.AddTransient<IAdvanceBTypeDataService, AdvanceBTypeDataService>();
            services.AddTransient<IMixFinalAccountCorrectionService, MixFinalAccountCorrectionService>();


            services.AddSingleton<HtmlToPdfService>();  //added only PDF service

            //Email Service - Background Service to run in each 5 minutes
            //services.AddSingleton<EmailService>();
            //services.AddSingleton<EmailSenderService>();
            //services.AddHostedService(provider => provider.GetRequiredService<EmailSenderService>());

            // Retrieve allowed origins from appsettings.json
            var allowedOrigins = Configuration.GetSection("AllowedOrigins").Get<string[]>();

            // Configure CORS with allowed origins
            services.AddCors(options => {
                options.AddPolicy("AllowSpecificOrigins",
                  builder => builder.WithOrigins(allowedOrigins)
                  .AllowAnyHeader()
                  .AllowAnyMethod());
            });

            services.AddControllers().AddNewtonsoftJson(options => {

                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddControllers()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            services.AddSwaggerGen(options =>
              options.MapType<DateOnly>(() => new OpenApiSchema
              {
                  Type = "string",
                  Format = "date",
                  Example = new OpenApiString("2022-01-01")
              })
            );

            services.AddControllers();

            // Configure the upload directory path
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "APIS",
                    Version = "v1"
                });
            });
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //app.UseHttpsRedirection();

            app.UseSwaggerUI();
            //app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("AllowSpecificOrigins");

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIs");
            });

            //updated for prometheus
            app.UseMetricsAllMiddleware();
            app.UseMetricsAllEndpoints();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                  name: "FileRoute",
                  pattern: "Files/retrieve/{fileName}");
            });

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                  name: "FileRoute",
                  pattern: "Files/cust_photo/{fileName}");
            });

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                  name: "FileRoute",
                  pattern: "Files/cust_docs/{fileName}");
            });
        }
    }
}