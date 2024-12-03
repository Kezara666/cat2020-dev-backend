using AutoMapper;
using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.AssessmentTax.Quarter;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.AssessmentTax;
using CAT20.Core.Services.Vote;
using DocumentFormat.OpenXml.ExtendedProperties;
using Irony.Parsing;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities.Collections;

namespace CAT20.Services.AssessmentTax
{
    public class AssessmentJournalService : IAssessmentJournalService
    {
        private readonly IMapper _mapper;
        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        private readonly IAssessmentBalanceService _assessmentBalanceService;
        private readonly IVoteBalanceService _voteBalanceService;

        public AssessmentJournalService(IMapper mapper, IAssessmentTaxUnitOfWork unitOfWork, IAssessmentBalanceService assessmentBalanceService,IVoteBalanceService voteBalanceService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _assessmentBalanceService = assessmentBalanceService;
            _voteBalanceService = voteBalanceService;
        }

        public async Task<IEnumerable<Assessment>> GetForPendingJournalRequest(int? sabhaId)
        {
            return await _unitOfWork.Assessments.GetForPendingJournalRequest(sabhaId);
        }

        public async Task<Assessment> GetAssessmentForJournal(int? sabhaId, int? kFormId)
        {
            return await _unitOfWork.Assessments.GetAssessmentForJournal(sabhaId, kFormId);
        }

        public Task<AssessmentPropertyType> GetRequestedPropterType(int id)
        {
            return _unitOfWork.AssessmentPropertyTypes.GetById(id);
        }

        public async Task<bool> Create(AssessmentJournal obj,HTokenClaim token)
        {
            try
            {
                var assmt = await _unitOfWork.Assessments.GetForJournal(obj.AssessmentId!.Value);

                if(obj.NewPropertyTypeId != null)
                {
                    var propType = await _unitOfWork.AssessmentPropertyTypes.GetById(obj.NewPropertyTypeId.Value);

                    if(propType==null)
                    {
                        throw new Exception("Property Type Not Found");
                    }

                    obj.OldPropertyTypeId = assmt.PropertyTypeId;
                }

                if (assmt != null)
                {

                    if(assmt.AssessmentBalance!.NumberOfPayments!= assmt.AssessmentBalance.NumberOfCancels && token.userId !=1518)
                    {
                        //throw new GeneralException("Unauthorized Journal Entry Transaction");
                    }

                    assmt.HasJournalRequest = true;
                    await _unitOfWork.AssessmentJournals.AddAsync(obj);

                }
                else
                {
                    throw new Exception("Assessment not found");
                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> ApproveRejectJournal(HApproveRejectJournal obj, HTokenClaim token)
        {
            try
            {
                var isForce = false;
                var jnl = await _unitOfWork.AssessmentJournals.GetByIdAsync(obj.Id);
                var rates = await _unitOfWork.AssessmentRates.GetByIdAsync(1);

                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);

                if(session == null)
                {
                    throw new GeneralException("No Active session Found");
                }

                if (jnl != null)
                {
                    jnl.ActionBy = obj.ActionBy;
                    jnl.ActionDate = DateTime.Now;
                    //jnl.ActionNote = "Approve";
                    jnl.DraftApproveReject = obj.DraftApproveReject;

                    var asmt = await _unitOfWork.Assessments.GetForJournal(jnl.AssessmentId!.Value);

                    if (asmt != null && asmt.Allocation != null && asmt.AssessmentPropertyType != null && asmt.AssessmentBalance != null && asmt.AssessmentBalance.Q1 != null && asmt.AssessmentBalance.Q2 != null && asmt.AssessmentBalance.Q3 != null && asmt.AssessmentBalance.Q4 != null)
                    {
                        

                        if (obj.DraftApproveReject == 1 )
                        {

                            if (token.IsFinalAccountsEnabled == 1)
                            {
                                await UpdateLedgerAccountsRollBack(new List<int?> { asmt.Id }, token);
                            }


                            if (jnl.OldLYArrears.HasValue && jnl.NewLYArrears.HasValue && asmt.AssessmentBalance.LYArrears != jnl.NewLYArrears)
                            {
                                asmt.AssessmentBalance.LYArrears = jnl.NewLYArrears;
                            }

                            if (jnl.OldLYWarrant.HasValue && jnl.NewLYWarrant.HasValue && asmt.AssessmentBalance.LYWarrant != jnl.NewLYWarrant)
                            {
                                asmt.AssessmentBalance.LYWarrant = jnl.NewLYWarrant;
                            }


                            //if (jnl.OldTYArrears.HasValue && jnl.NewTYArrears.HasValue && asmt.AssessmentBalance.TYArrears != jnl.NewTYArrears)
                            //{
                            //    asmt.AssessmentBalance.TYArrears = jnl.NewTYArrears;
                            //}


                            //if (jnl.OldTYWarrant.HasValue && jnl.NewTYWarrant.HasValue && asmt.AssessmentBalance.TYWarrant != jnl.NewTYWarrant)
                            //{
                            //    asmt.AssessmentBalance.TYWarrant = jnl.NewTYWarrant;
                            //}



                            //if (jnl.OldExcessPayment.HasValue && jnl.NewExcessPayment.HasValue && asmt.AssessmentBalance.ExcessPayment != jnl.NewExcessPayment)
                            //{
                            //    if (asmt.AssessmentBalance.LYWarrant == 0 && asmt.AssessmentBalance.LYArrears >= 0 && 0 <= jnl.NewExcessPayment)
                            //    {
                            //        asmt.AssessmentBalance.ExcessPayment = jnl.NewExcessPayment;

                            //    }
                            //    else
                            //    {
                            //        throw new Exception("Excess Payment Unable To Update");
                            //    }
                            //}



                            //if ((jnl.OldAllocation.HasValue && jnl.NewAllocation.HasValue && asmt.Allocation.AllocationAmount != jnl.NewAllocation) || (jnl.OldPropertyTypeId.HasValue && jnl.NewPropertyTypeId.HasValue && asmt.PropertyTypeId != jnl.NewPropertyTypeId))
                            //{

                                if (jnl.OldAllocation.HasValue && jnl.NewAllocation.HasValue && asmt.Allocation.AllocationAmount != jnl.NewAllocation)
                                {
                                    asmt.Allocation.AllocationAmount = jnl.NewAllocation;
                                }

                                if(jnl.OldPropertyTypeId.HasValue && jnl.NewPropertyTypeId.HasValue && asmt.PropertyTypeId != jnl.NewPropertyTypeId){

                                    asmt.PropertyTypeId = jnl.NewPropertyTypeId;

                                    var propType = await _unitOfWork.AssessmentPropertyTypes.GetById(asmt.PropertyTypeId.Value);
                                    if(propType != null)
                                    {
                                        asmt.AssessmentPropertyType = propType;
                                    }
                                    else
                                    {
                                        throw new Exception("New Property Type Not Found");
                                    }

                                }

                                /*old q values*/
                                var oldQ1 = asmt.AssessmentBalance.Q1.Amount;
                                var oldQ2 = asmt.AssessmentBalance.Q2.Amount;
                                var oldQ3 = asmt.AssessmentBalance.Q3.Amount;
                                var oldQ4 = asmt.AssessmentBalance.Q4.Amount;

                                /*old q arrears*/

                                var oldQ1Arrears = asmt.AssessmentBalance.Q1.Amount - asmt.AssessmentBalance.Q1.ByExcessDeduction - asmt.AssessmentBalance.Q1.Paid - asmt.AssessmentBalance.Q1.Discount;
                                var oldQ2Arrears = asmt.AssessmentBalance.Q2.Amount - asmt.AssessmentBalance.Q2.ByExcessDeduction - asmt.AssessmentBalance.Q2.Paid - asmt.AssessmentBalance.Q2.Discount;
                                var oldQ3Arrears = asmt.AssessmentBalance.Q3.Amount - asmt.AssessmentBalance.Q3.ByExcessDeduction - asmt.AssessmentBalance.Q3.Paid - asmt.AssessmentBalance.Q3.Discount;
                                var oldQ4Arrears = asmt.AssessmentBalance.Q4.Amount - asmt.AssessmentBalance.Q4.ByExcessDeduction - asmt.AssessmentBalance.Q4.Paid - asmt.AssessmentBalance.Q4.Discount;


                                var annualAmmount = (asmt.Allocation.AllocationAmount * (asmt.AssessmentPropertyType.QuarterRate / 100));
                                var qAmount = annualAmmount / 4;

                                var roundedValue = Math.Round((decimal)qAmount, 2, MidpointRounding.AwayFromZero);

                                var remainder = annualAmmount - (roundedValue * 4);

                                asmt.AssessmentBalance.AnnualAmount = annualAmmount;
                                //asmt.AssessmentBalance.CurrentQuarter = 1;




                                asmt.AssessmentBalance.Q1.Amount = roundedValue;
                                asmt.AssessmentBalance.Q2.Amount = roundedValue;
                                asmt.AssessmentBalance.Q3.Amount = roundedValue;
                                asmt.AssessmentBalance.Q4.Amount = roundedValue + remainder;

                                //asmt.AssessmentBalance.Q1.ByExcessDeduction = 0;
                                //asmt.AssessmentBalance.Q2.ByExcessDeduction = 0;
                                //asmt.AssessmentBalance.Q3.ByExcessDeduction = 0;
                                //asmt.AssessmentBalance.Q4.ByExcessDeduction = 0;

                                //asmt.AssessmentBalance.Q1.Paid = 0;
                                //asmt.AssessmentBalance.Q2.Paid = 0;
                                //asmt.AssessmentBalance.Q3.Paid = 0;
                                //asmt.AssessmentBalance.Q4.Paid = 0;

                                //asmt.AssessmentBalance.Q1.Discount = 0;
                                //asmt.AssessmentBalance.Q2.Discount = 0;
                                //asmt.AssessmentBalance.Q3.Discount = 0;
                                //asmt.AssessmentBalance.Q4.Discount = 0;

                                //asmt.AssessmentBalance.Q1.Warrant = 0;
                                //asmt.AssessmentBalance.Q2.Warrant = 0;
                                //asmt.AssessmentBalance.Q3.Warrant = 0;
                                //asmt.AssessmentBalance.Q4.Warrant = 0;


                                //asmt.AssessmentBalance.Q1.StartDate = todayTime;
                                //asmt.AssessmentBalance.Q2.StartDate = null;
                                //asmt.AssessmentBalance.Q3.StartDate = null;
                                //asmt.AssessmentBalance.Q4.StartDate = null;


                                //asmt.AssessmentBalance.Q1.WarrantMethod = null;
                                //asmt.AssessmentBalance.Q2.WarrantMethod = null;
                                //asmt.AssessmentBalance.Q3.WarrantMethod = null;
                                //asmt.AssessmentBalance.Q4.WarrantMethod = null;


                                //asmt.AssessmentBalance.Q1.EndDate = null;
                                //asmt.AssessmentBalance.Q2.EndDate = null;
                                //asmt.AssessmentBalance.Q3.EndDate = null;
                                //asmt.AssessmentBalance.Q4.EndDate = null;

                                //asmt.AssessmentBalance.Q1.IsCompleted = false;
                                //asmt.AssessmentBalance.Q2.IsCompleted = false;
                                //asmt.AssessmentBalance.Q3.IsCompleted = false;
                                //asmt.AssessmentBalance.Q4.IsCompleted = false;
                                if (!await _unitOfWork.MixinOrders.HasPaidPostedOrdersForAssessment(asmt.Id!.Value) && asmt.AssessmentBalance.NumberOfPayments == asmt.AssessmentBalance.NumberOfCancels && asmt.AssessmentBalance.ByExcessDeduction == 0)
                                {
                                    if (jnl.OldExcessPayment.HasValue && jnl.NewExcessPayment.HasValue && asmt.AssessmentBalance.ExcessPayment != jnl.NewExcessPayment)
                                    {
                                        if (asmt.AssessmentBalance.LYWarrant == 0 && asmt.AssessmentBalance.LYArrears >= 0 && 0 <= jnl.NewExcessPayment)
                                        {
                                            asmt.AssessmentBalance.ExcessPayment = jnl.NewExcessPayment;

                                        }
                                        else
                                        {
                                            throw new Exception("Excess Payment Unable To Update");
                                        }
                                    }


                                if (0 < asmt.AssessmentBalance.ExcessPayment)
                                    {

                                        bool isQ1Over = asmt.AssessmentBalance.Q1.IsOver;
                                        bool isQ2Over = asmt.AssessmentBalance.Q2.IsOver;
                                        bool isQ3Over = asmt.AssessmentBalance.Q3.IsOver;
                                        bool isQ4Over = asmt.AssessmentBalance.Q4.IsOver;


                                        asmt.AssessmentBalance.Q1.IsOver = false;
                                        asmt.AssessmentBalance.Q2.IsOver = false;
                                        asmt.AssessmentBalance.Q3.IsOver = false;
                                        asmt.AssessmentBalance.Q4.IsOver = false;

                                      

                                        asmt.AssessmentBalance.TYArrears = 0;
                                        asmt.AssessmentBalance.TYWarrant = 0;

                                        var Qreports = await _unitOfWork.AssessmentQuarterReports.GetAllByAssessmentId(asmt.Id!.Value);


                                        if (token.IsFinalAccountsEnabled == 1)
                                        {
                                            await UpdateLedgerAccounts(new List<int?> { asmt.Id }, token);
                                        }


                                        if (!discountProcess(ref asmt, rates, -1,1))
                                        {
                                            throw new Exception("Discount Process Failed");
                                        }

                                        if (asmt.AssessmentBalance != null && asmt.AssessmentBalance.Q1 != null && asmt.AssessmentBalance.Q2 != null && asmt.AssessmentBalance.Q3 != null && asmt.AssessmentBalance.Q4 != null)
                                        {


                                            if (isQ1Over)
                                            {

                                                if (!asmt.AssessmentBalance!.Q1!.IsCompleted)
                                                {
                                                    asmt.AssessmentBalance.TYArrears += asmt.AssessmentBalance.Q1.Amount - asmt.AssessmentBalance.Q1.ByExcessDeduction - asmt.AssessmentBalance.Q1.Paid - asmt.AssessmentBalance.Q1.Discount;


                                                    if (asmt.AssessmentBalance.Q1.WarrantMethod != null)
                                                    {

                                                        if (asmt.AssessmentBalance.Q1.WarrantMethod == 1)
                                                        {

                                                            var warrantAmount = (asmt.AssessmentBalance.Q1.Amount * (asmt.AssessmentPropertyType.WarrantRate / 100));

                                                            asmt.AssessmentBalance.Q1.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);


                                                            asmt.AssessmentBalance.TYWarrant += asmt.AssessmentBalance.Q1.Warrant;

                                                            asmt.AssessmentBalance.Q1.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;


                                                        }
                                                        else if (asmt.AssessmentBalance.Q1.WarrantMethod == 2)
                                                        {
                                                            var warrantAmount = ((asmt.AssessmentBalance.Q1.Amount - asmt.AssessmentBalance.Q1.ByExcessDeduction - asmt.AssessmentBalance.Q1.Paid - asmt.AssessmentBalance.Q1.Discount) * (asmt.AssessmentPropertyType.WarrantRate / 100));

                                                            asmt.AssessmentBalance.Q1.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);

                                                            asmt.AssessmentBalance.TYWarrant += asmt.AssessmentBalance.Q1.Warrant;

                                                            asmt.AssessmentBalance.Q1.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;

                                                        }
                                                        else if (asmt.AssessmentBalance.Q1.WarrantMethod == 0)
                                                        {

                                                        }
                                                        else
                                                        {
                                                            throw new Exception("Q1 Warrant Method Not Found");
                                                        }

                                                    }


                                                }
                                                else
                                                {
                                                    asmt.AssessmentBalance.Q1.IsOver = true;
                                                }
                                            }

                                            if (isQ2Over)
                                            {

                                                if (!asmt.AssessmentBalance!.Q2!.IsCompleted)
                                                {

                                                    asmt.AssessmentBalance.TYArrears += asmt.AssessmentBalance.Q2.Amount - asmt.AssessmentBalance.Q2.ByExcessDeduction - asmt.AssessmentBalance.Q2.Paid - asmt.AssessmentBalance.Q2.Discount;

                                                    if (asmt.AssessmentBalance.Q2.WarrantMethod != null)
                                                    {

                                                        if (asmt.AssessmentBalance.Q2.WarrantMethod == 1)
                                                        {

                                                            var warrantAmount = (asmt.AssessmentBalance.Q2.Amount * (asmt.AssessmentPropertyType.WarrantRate / 100));

                                                            asmt.AssessmentBalance.Q2.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);


                                                            asmt.AssessmentBalance.TYWarrant += asmt.AssessmentBalance.Q2.Warrant;

                                                            asmt.AssessmentBalance.Q2.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;


                                                        }
                                                        else if (asmt.AssessmentBalance.Q2.WarrantMethod == 2)
                                                        {
                                                            var warrantAmount = ((asmt.AssessmentBalance.Q2.Amount - asmt.AssessmentBalance.Q2.ByExcessDeduction - asmt.AssessmentBalance.Q2.Paid - asmt.AssessmentBalance.Q2.Discount) * (asmt.AssessmentPropertyType.WarrantRate / 100));

                                                            asmt.AssessmentBalance.Q2.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);

                                                            asmt.AssessmentBalance.TYWarrant += asmt.AssessmentBalance.Q2.Warrant;

                                                            asmt.AssessmentBalance.Q2.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;

                                                        }
                                                        else if (asmt.AssessmentBalance.Q2.WarrantMethod == 0)
                                                        {

                                                        }
                                                        else
                                                        {
                                                            throw new Exception("Q2 Warrant Method Not Found");
                                                        }


                                                    }
                                                }
                                                else
                                                {
                                                    asmt.AssessmentBalance.Q2.IsOver = true;
                                                }

                                            }


                                            if (isQ3Over)
                                            {

                                                if (!asmt.AssessmentBalance!.Q3!.IsCompleted)
                                                {

                                                    asmt.AssessmentBalance.TYArrears += asmt.AssessmentBalance.Q3.Amount - asmt.AssessmentBalance.Q3.ByExcessDeduction - asmt.AssessmentBalance.Q3.Paid - asmt.AssessmentBalance.Q3.Discount;

                                                    if (asmt.AssessmentBalance.Q3.WarrantMethod != null)
                                                    {

                                                        if (asmt.AssessmentBalance.Q3.WarrantMethod == 1)
                                                        {

                                                            var warrantAmount = (asmt.AssessmentBalance.Q3.Amount * (asmt.AssessmentPropertyType.WarrantRate / 100));

                                                            asmt.AssessmentBalance.Q3.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);


                                                            asmt.AssessmentBalance.TYWarrant += asmt.AssessmentBalance.Q3.Warrant;

                                                            asmt.AssessmentBalance.Q3.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;


                                                        }
                                                        else if (asmt.AssessmentBalance.Q3.WarrantMethod == 2)
                                                        {
                                                            var warrantAmount = ((asmt.AssessmentBalance.Q2.Amount - asmt.AssessmentBalance.Q2.ByExcessDeduction - asmt.AssessmentBalance.Q2.Paid - asmt.AssessmentBalance.Q3.Discount) * (asmt.AssessmentPropertyType.WarrantRate / 100));

                                                            asmt.AssessmentBalance.Q3.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);

                                                            asmt.AssessmentBalance.TYWarrant += asmt.AssessmentBalance.Q3.Warrant;

                                                            asmt.AssessmentBalance.Q3.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;

                                                        }
                                                        else if (asmt.AssessmentBalance.Q3.WarrantMethod == 0)
                                                        {

                                                        }
                                                        else
                                                        {
                                                            throw new Exception("Q3 Warrant Method Not Found");
                                                        }

                                                    }
                                                }
                                                else
                                                {
                                                    asmt.AssessmentBalance.Q3.IsOver = true;
                                                }
                                            }

                                            if (isQ4Over)
                                            {
                                                throw new Exception("Q4 Unable to Update");
                                            }

                                        }
                                        else
                                        {
                                            throw new Exception("Assessment Balance Not Found");
                                        }

                                        if (asmt.AssessmentBalance.CurrentQuarter > 2 && Qreports.Count() > 0)
                                        {


                                            var initQReport = Qreports.FirstOrDefault();

                                            initQReport.DateTime = DateTime.Now;
                                            //initQReport.Year = asmt.AssessmentBalance.Year,
                                            //initQReport.QuarterNo = 3
                                            initQReport.QAmount = 0;
                                            initQReport.M1Paid = 0;
                                            initQReport.M2Paid = 0;
                                            initQReport.M3Paid = asmt.AssessmentBalance.ExcessPayment;
                                            initQReport.QWarrant = 0;
                                            initQReport.QDiscount = 0;

                                            initQReport.LYArrears = asmt.AssessmentBalance.LYArrears;
                                            initQReport.LYWarrant = asmt.AssessmentBalance.LYWarrant;
                                            initQReport.TYArrears = asmt.AssessmentBalance.TYArrears;
                                            initQReport.TYWarrant = asmt.AssessmentBalance.TYWarrant;
                                            initQReport.AssessmentId = asmt.Id;
                                            initQReport.CreatedBy = -1;
                                            initQReport.CreatedAt = DateTime.Now;
                                            initQReport.UseTransactionsType = AssessmentTransactionsType.Init;
                                            initQReport.UpdatedAt = DateTime.Now;
                                            initQReport.UpdatedBy = obj.ActionBy;



                                            initQReport.RunningBalance =
                                                        +initQReport.LYArrears
                                                        + initQReport.LYWarrant
                                                        + initQReport.TYArrears
                                                        + initQReport.TYWarrant
                                                        + initQReport.QAmount
                                                        + initQReport.QWarrant

                                                        - initQReport.QDiscount
                                                        - initQReport.M1Paid
                                                        - initQReport.M2Paid
                                                        - initQReport.M3Paid;



                                            //await _unitOfWork.AssessmentQuarterReports.AddAsync(oldReport);

                                            var Q1Report = Qreports.FirstOrDefault(r => r.QuarterNo == 1 && r.Year == asmt.AssessmentBalance.Year);
                                            //var Q2Report = Qreports.FirstOrDefault(r => r.QuarterNo == 2 && r.Year == asmt.AssessmentBalance.Year);
                                            //var Q3Report = Qreports.FirstOrDefault(r => r.QuarterNo == 3 && r.Year == asmt.AssessmentBalance.Year);
                                            //var Q4Report = Qreports.FirstOrDefault(r => r.QuarterNo == 4 && r.Year == asmt.AssessmentBalance.Year);


                                            if (isQ1Over && Q1Report != null)
                                            {
                                                Q1Report.DateTime = DateTime.Now;
                                                //initQReport.Year = asmt.AssessmentBalance.Year,
                                                //initQReport.QuarterNo = 3
                                                Q1Report.QAmount = asmt.AssessmentBalance.Q1.Amount;
                                                Q1Report.M1Paid = 0;
                                                Q1Report.M2Paid = 0;
                                                Q1Report.M3Paid = 0;
                                                Q1Report.QWarrant = asmt.AssessmentBalance.Q1.Warrant;
                                                Q1Report.QDiscount = asmt.AssessmentBalance.Q1.Discount;

                                                //Q1Report.LYArrears = asmt.AssessmentBalance.LYArrears;
                                                //Q1Report.LYWarrant = asmt.AssessmentBalance.LYWarrant;
                                                //Q1Report.TYArrears = asmt.AssessmentBalance.TYArrears;
                                                //Q1Report.TYWarrant = asmt.AssessmentBalance.TYWarrant;
                                                Q1Report.AssessmentId = asmt.Id;
                                                Q1Report.CreatedBy = -1;
                                                Q1Report.CreatedAt = DateTime.Now;
                                                Q1Report.UseTransactionsType = AssessmentTransactionsType.Init;
                                                Q1Report.UpdatedAt = DateTime.Now;
                                                Q1Report.UpdatedBy = obj.ActionBy;



                                                Q1Report.RunningBalance = initQReport.RunningBalance + Q1Report.QAmount + Q1Report.QWarrant - (Q1Report.M1Paid + Q1Report.M2Paid + Q1Report.M3Paid + Q1Report.QDiscount);
                                            }

                                        }

                                    }
                                    else
                                    {

                                        if (asmt.AssessmentBalance.Q1.IsOver)
                                        {
                                            var newQ1Arrears = (asmt.AssessmentBalance.Q1.Amount - asmt.AssessmentBalance.Q1.ByExcessDeduction - asmt.AssessmentBalance.Q1.Paid - asmt.AssessmentBalance.Q1.Discount);

                                            asmt.AssessmentBalance.TYArrears -= oldQ1Arrears;
                                            asmt.AssessmentBalance.TYArrears += newQ1Arrears;

                                            if (asmt.AssessmentBalance.Q1.WarrantMethod != null)
                                            {
                                                asmt.AssessmentBalance.TYWarrant -= asmt.AssessmentBalance.Q1.Warrant;

                                                if (asmt.AssessmentBalance.Q1.WarrantMethod == 1)
                                                {

                                                    var warrantAmount = (asmt.AssessmentBalance.Q1.Amount * (asmt.AssessmentPropertyType.WarrantRate / 100));

                                                    asmt.AssessmentBalance.Q1.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);

                                                    asmt.AssessmentBalance.TYWarrant += asmt.AssessmentBalance.Q1.Warrant;

                                                    asmt.AssessmentBalance.Q1.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;

                                                }
                                                else if (asmt.AssessmentBalance.Q1.WarrantMethod == 2)
                                                {

                                                    var warrantAmount = ((asmt.AssessmentBalance.Q1.Amount - asmt.AssessmentBalance.Q1.ByExcessDeduction - asmt.AssessmentBalance.Q1.Paid - asmt.AssessmentBalance.Q1.Discount) * (asmt.AssessmentPropertyType.WarrantRate / 100));
                                                    asmt.AssessmentBalance.Q1.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);
                                                    asmt.AssessmentBalance.TYWarrant += asmt.AssessmentBalance.Q1.Warrant;
                                                    asmt.AssessmentBalance.Q1.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;

                                                }
                                                else if (asmt.AssessmentBalance.Q1.WarrantMethod == 0)
                                                {

                                                }
                                                else
                                                {
                                                    throw new Exception("Q1 Warrant Method Not Found");
                                                }

                                            }
                                        }
                                        if (asmt.AssessmentBalance.Q2.IsOver)
                                        {
                                            var newQ2Arrears = (asmt.AssessmentBalance.Q2.Amount - asmt.AssessmentBalance.Q2.ByExcessDeduction - asmt.AssessmentBalance.Q2.Paid - asmt.AssessmentBalance.Q1.Discount);

                                            asmt.AssessmentBalance.TYArrears -= oldQ2Arrears;
                                            asmt.AssessmentBalance.TYArrears += newQ2Arrears;

                                            if (asmt.AssessmentBalance.Q2.WarrantMethod != null)
                                            {
                                                asmt.AssessmentBalance.TYWarrant -= asmt.AssessmentBalance.Q2.Warrant;

                                                if (asmt.AssessmentBalance.Q2.WarrantMethod == 1)
                                                {

                                                    var warrantAmount = (asmt.AssessmentBalance.Q2.Amount * (asmt.AssessmentPropertyType.WarrantRate / 100));

                                                    asmt.AssessmentBalance.Q2.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);

                                                    asmt.AssessmentBalance.TYWarrant += asmt.AssessmentBalance.Q2.Warrant;

                                                    asmt.AssessmentBalance.Q2.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;

                                                }
                                                else if (asmt.AssessmentBalance.Q2.WarrantMethod == 2)
                                                {

                                                    var warrantAmount = ((asmt.AssessmentBalance.Q2.Amount - asmt.AssessmentBalance.Q2.ByExcessDeduction - asmt.AssessmentBalance.Q2.Paid - asmt.AssessmentBalance.Q2.Discount) * (asmt.AssessmentPropertyType.WarrantRate / 100));
                                                    asmt.AssessmentBalance.Q2.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);
                                                    asmt.AssessmentBalance.TYWarrant += asmt.AssessmentBalance.Q2.Warrant;
                                                    asmt.AssessmentBalance.Q2.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;

                                                }
                                                else if (asmt.AssessmentBalance.Q2.WarrantMethod == 0)
                                                {

                                                }
                                                else
                                                {
                                                    throw new Exception("Q2 Warrant Method Not Found");
                                                }

                                            }
                                        }
                                        if (asmt.AssessmentBalance.Q3.IsOver)
                                        {

                                            var newQ3Arrears = (asmt.AssessmentBalance.Q3.Amount - asmt.AssessmentBalance.Q3.ByExcessDeduction - asmt.AssessmentBalance.Q3.Paid - asmt.AssessmentBalance.Q3.Discount);

                                            asmt.AssessmentBalance.TYArrears -= oldQ3Arrears;
                                            asmt.AssessmentBalance.TYArrears += newQ3Arrears;

                                            if (asmt.AssessmentBalance.Q3.WarrantMethod != null)
                                            {
                                                asmt.AssessmentBalance.TYWarrant -= asmt.AssessmentBalance.Q3.Warrant;

                                                if (asmt.AssessmentBalance.Q3.WarrantMethod == 1)
                                                {

                                                    var warrantAmount = (asmt.AssessmentBalance.Q3.Amount * (asmt.AssessmentPropertyType.WarrantRate / 100));

                                                    asmt.AssessmentBalance.Q3.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);

                                                    asmt.AssessmentBalance.TYWarrant += asmt.AssessmentBalance.Q3.Warrant;

                                                    asmt.AssessmentBalance.Q3.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;

                                                }
                                                else if (asmt.AssessmentBalance.Q3.WarrantMethod == 2)
                                                {

                                                    var warrantAmount = ((asmt.AssessmentBalance.Q3.Amount - asmt.AssessmentBalance.Q3.ByExcessDeduction - asmt.AssessmentBalance.Q3.Paid - asmt.AssessmentBalance.Q3.Discount) * (asmt.AssessmentPropertyType.WarrantRate / 100));
                                                    asmt.AssessmentBalance.Q3.Warrant = Math.Round((decimal)warrantAmount, 2, MidpointRounding.AwayFromZero);
                                                    asmt.AssessmentBalance.TYWarrant += asmt.AssessmentBalance.Q3.Warrant;

                                                    asmt.AssessmentBalance.Q3.WarrantRate = asmt.AssessmentPropertyType.WarrantRate / 100;

                                                }
                                                else if (asmt.AssessmentBalance.Q3.WarrantMethod == 0)
                                                {

                                                }
                                                else
                                                {
                                                    throw new Exception("Q3 Warrant Method Not Found");
                                                }

                                            }


                                        }
                                        if (asmt.AssessmentBalance.Q4.IsOver)
                                        {

                                            throw new Exception("Q4 Unable to Update");


                                        }
                                    }
                                }else
                                {
                                    if (token.IsFinalAccountsEnabled == 1)
                                    {
                                        throw new Exception("Unable to Update");
                                    }

                                    isForce = true;

                                    //asmt.AssessmentBalance.Q1.Adjustment = jnl.Q1Adjustment;
                                    //asmt.AssessmentBalance.Q2.Adjustment = jnl.Q2Adjustment;
                                    //asmt.AssessmentBalance.Q3.Adjustment = jnl.Q3Adjustment;
                                    //asmt.AssessmentBalance.Q4.Adjustment = jnl.Q4Adjustment;


                                    //asmt.AssessmentBalance.LYArrearsAdjustment = jnl.NewLYArrears;
                                    //asmt.AssessmentBalance.LYWarrantAdjustment = jnl.NewLYWarrant;
                                    //asmt.AssessmentBalance.TYArrearsAdjustment = jnl.NewTYArrears;
                                    //asmt.AssessmentBalance.TYWarrantAdjustment = jnl.NewTYWarrant;
                                    //asmt.AssessmentBalance.OverPayAdjustment = jnl.NewExcessPayment;


                                if ((jnl.NewExcessPayment?? 0) != 0 && ((jnl.NewLYArrears ?? 0) + (jnl.NewLYWarrant ?? 0) + (jnl.NewTYArrears ?? 0) + (jnl.NewTYWarrant ?? 0) != 0))
                                {
                                    throw new Exception("This Transaction Not Allowed");
                                }

                                else
                                {
                                    //asmt.AssessmentBalance.LYArrearsAdjustment = asmt.AssessmentBalance.LYArrearsAdjustment ?? 0;
                                    //asmt.AssessmentBalance.LYWarrantAdjustment = asmt.AssessmentBalance.LYWarrantAdjustment ?? 0;
                                    //asmt.AssessmentBalance.TYArrearsAdjustment = asmt.AssessmentBalance.TYArrearsAdjustment ?? 0;
                                    //asmt.AssessmentBalance.TYWarrantAdjustment = asmt.AssessmentBalance.TYWarrantAdjustment ?? 0;
                                    //asmt.AssessmentBalance.OverPayAdjustment = jnl.NewExcessPayment ?? 0;

                                    //jnl.NewLYArrears = jnl.NewLYArrears ?? 0;
                                    //jnl.NewLYWarrant = jnl.NewLYWarrant ?? 0;
                                    //jnl.NewTYArrears = jnl.NewTYArrears ?? 0;
                                    //jnl.NewTYWarrant = jnl.NewTYWarrant ?? 0;
                                    //jnl.NewExcessPayment = jnl.NewExcessPayment ?? 0;


                                    //    asmt.AssessmentBalance.LYArrears += jnl.NewLYArrears??0;
                                    //    asmt.AssessmentBalance.LYWarrant += jnl.NewLYWarrant??0;
                                        asmt.AssessmentBalance.TYArrears += jnl.NewTYArrears??0;
                                        asmt.AssessmentBalance.TYWarrant += jnl.NewTYWarrant?? 0;
                                    //    asmt.AssessmentBalance.OverPayAdjustment += jnl.NewExcessPayment ?? 0;

                                    //asmt.AssessmentBalance.LYArrearsAdjustment += jnl.NewLYArrears ?? 0;
                                    //asmt.AssessmentBalance.LYWarrantAdjustment += jnl.NewLYWarrant ?? 0;
                                    //asmt.AssessmentBalance.TYArrearsAdjustment += jnl.NewTYArrears ?? 0;
                                    //asmt.AssessmentBalance.TYWarrantAdjustment += jnl.NewTYWarrant ?? 0;
                                    //asmt.AssessmentBalance.OverPayAdjustment += jnl.NewExcessPayment ?? 0;
                                    asmt.AssessmentBalance.LYArrearsAdjustment ??= 0;
                                    asmt.AssessmentBalance.LYWarrantAdjustment ??= 0;
                                    asmt.AssessmentBalance.TYArrearsAdjustment ??= 0;
                                    asmt.AssessmentBalance.TYWarrantAdjustment ??= 0;
                                    asmt.AssessmentBalance.OverPayAdjustment ??= 0;

                                    jnl.NewLYArrears ??= 0;
                                    jnl.NewLYWarrant ??= 0;
                                    jnl.NewTYArrears ??= 0;
                                    jnl.NewTYWarrant ??= 0;
                                    jnl.NewExcessPayment ??= 0;

                                    //asmt.AssessmentBalance.LYArrears += jnl.NewLYArrears ?? 0;
                                    //asmt.AssessmentBalance.LYWarrant += jnl.NewLYWarrant ?? 0;
                                    //asmt.AssessmentBalance.TYArrears += jnl.NewTYArrears ?? 0;
                                    //asmt.AssessmentBalance.TYWarrant += jnl.NewTYWarrant ?? 0;
                                    //asmt.AssessmentBalance.OverPayAdjustment += jnl.NewExcessPayment ?? 0;

                                    asmt.AssessmentBalance.LYArrearsAdjustment += jnl.NewLYArrears ?? 0;
                                    asmt.AssessmentBalance.LYWarrantAdjustment += jnl.NewLYWarrant ?? 0;
                                    asmt.AssessmentBalance.TYArrearsAdjustment += jnl.NewTYArrears ?? 0;
                                    asmt.AssessmentBalance.TYWarrantAdjustment += jnl.NewTYWarrant ?? 0;
                                    asmt.AssessmentBalance.OverPayAdjustment += jnl.NewExcessPayment ?? 0;


                                    if (jnl.NewExcessPayment + asmt.AssessmentBalance.OverPayment > 0)
                                        {
                                            var remainderOverpay = jnl.NewExcessPayment+asmt.AssessmentBalance.OverPayment; // Use remainder instead of modifying jnl.NewExcessPayment directly



                                        // Apply excess payment to LYWarrant
                                        if (remainderOverpay > 0 && asmt.AssessmentBalance.LYWarrant > 0)
                                        {
                                            decimal amountToApply = Math.Min(remainderOverpay ?? 0, asmt.AssessmentBalance.LYWarrant ?? 0);
                                            asmt.AssessmentBalance.LYWarrant -= amountToApply;
                                            remainderOverpay -= amountToApply; // Reduce remainder by the amount applied
                                        }
                                        // Apply excess payment to LYArrears
                                        if (asmt.AssessmentBalance.LYArrears > 0)
                                        {
                                            decimal amountToApply = Math.Min(remainderOverpay ?? 0, asmt.AssessmentBalance.LYArrears ?? 0);
                                            asmt.AssessmentBalance.LYArrears -= amountToApply;
                                            remainderOverpay -= amountToApply; // Reduce remainder by the amount applied
                                        }
                                        // Apply excess payment to TYWarrant
                                        if (remainderOverpay > 0 && asmt.AssessmentBalance.TYWarrant > 0)
                                        {
                                            decimal amountToApply = Math.Min(remainderOverpay ?? 0, asmt.AssessmentBalance.TYWarrant ?? 0);
                                            asmt.AssessmentBalance.TYWarrant -= amountToApply;
                                            remainderOverpay -= amountToApply; // Reduce remainder by the amount applied
                                        }
                                        // Apply excess payment to TYArrears
                                        if (remainderOverpay > 0 && asmt.AssessmentBalance.TYArrears > 0)
                                        {
                                            decimal amountToApply = Math.Min(remainderOverpay ?? 0, asmt.AssessmentBalance.TYArrears ?? 0);
                                            asmt.AssessmentBalance.TYArrears -= amountToApply;
                                            remainderOverpay -= amountToApply; // Reduce remainder by the amount applied
                                        }



                                        // Update OverPayAdjustment with whatever is left in the remainder
                                        asmt.AssessmentBalance.OverPayment = remainderOverpay;
                                            asmt.AssessmentBalance.IsCompleted = asmt.AssessmentBalance.OverPayment>0?true: (asmt.AssessmentBalance.TYArrears + asmt.AssessmentBalance.TYWarrant+asmt.AssessmentBalance.LYArrears+asmt.AssessmentBalance.LYWarrant)>0?false:true;
                                        }



                                        asmt.AssessmentBalance.Q1.Adjustment = jnl.Q1Adjustment;
                                        asmt.AssessmentBalance.Q2.Adjustment = jnl.Q2Adjustment;
                                        asmt.AssessmentBalance.Q3.Adjustment = jnl.Q3Adjustment;
                                        asmt.AssessmentBalance.Q4.Adjustment = jnl.Q4Adjustment;

                                        //asmt.AssessmentBalance.Q1.QReportAdjustment = 0;
                                        //asmt.AssessmentBalance.Q2.QReportAdjustment = 0;
                                        //asmt.AssessmentBalance.Q3.QReportAdjustment = 0;
                                        //asmt.AssessmentBalance.Q4.QReportAdjustment = 0;



                                        if (asmt.AssessmentBalance.CurrentQuarter == 1)
                                        {
                                            asmt.AssessmentBalance.Q1.QReportAdjustment += jnl.NewLYArrears + jnl.NewLYWarrant + jnl.NewTYArrears + jnl.NewTYWarrant - jnl.NewExcessPayment;

                                        }

                                        if (asmt.AssessmentBalance.CurrentQuarter == 2)
                                        {
                                            asmt.AssessmentBalance.Q2.QReportAdjustment += jnl.NewLYArrears + jnl.NewLYWarrant + jnl.NewTYArrears + jnl.NewTYWarrant - jnl.NewExcessPayment;

                                        }

                                        if (asmt.AssessmentBalance.CurrentQuarter == 3)
                                        {
                                            asmt.AssessmentBalance.Q3.QReportAdjustment += jnl.NewLYArrears + jnl.NewLYWarrant + jnl.NewTYArrears + jnl.NewTYWarrant - jnl.NewExcessPayment;

                                        }

                                        if (asmt.AssessmentBalance.CurrentQuarter == 4)
                                        {
                                            asmt.AssessmentBalance.Q4.QReportAdjustment += jnl.NewLYArrears + jnl.NewLYWarrant + jnl.NewTYArrears + jnl.NewTYWarrant - jnl.NewExcessPayment;

                                        }

                                    if (!asmt.AssessmentBalance.Q1.IsOver || !asmt.AssessmentBalance.Q2.IsOver || !asmt.AssessmentBalance.Q3.IsOver || !asmt.AssessmentBalance.Q4.IsOver)
                                    {

                                        if (asmt.AssessmentBalance.OverPayment > 0)
                                        {
                                            asmt.AssessmentBalance.ExcessPayment = asmt.AssessmentBalance.OverPayment;
                                            asmt.AssessmentBalance.OverPayment = 0;
                                            discountProcess(ref asmt, rates, token.userId,session.StartAt.Month);

                                        }

                                    }

                                }

                                }
                            //}


                            createTransaction(asmt.AssessmentBalance, isForce? AssessmentTransactionsType.JournalAdjustmentForce: AssessmentTransactionsType.JournalAdjustment);
                            asmt.HasJournalRequest = false;
                        }
                        else if (obj.DraftApproveReject == 0)
                        {
                            asmt.HasJournalRequest = false;
                        }
                        else
                        {
                            asmt.HasJournalRequest = false;
                        }


                    }
                    else
                    {
                        throw new Exception("Assessment Not found Or Record Updated  By Other Operations");
                    }


                    await _unitOfWork.CommitAsync();


                }
                else
                {
                    throw new Exception("Not found");
                }




                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<(int totalCount, IEnumerable<AssessmentJournal> list)> GetAllJournalForAssessment(int assessmentId, int pageNo)
        {
            try
            {
                return await _unitOfWork.AssessmentJournals.GetAllJournalForAssessment(assessmentId, pageNo);
            }
            catch (Exception ex)
            {
                List<AssessmentJournal> list1 = new List<AssessmentJournal>();
                return (0, list1);
            }
        }

        public async Task<(int totalCount, IEnumerable<Assessment> list)> GetForPendingJournalRequest(int? sabhaId, int pageNo)
        {
            return await _unitOfWork.AssessmentJournals.GetForPendingJournalRequest(sabhaId, pageNo);
        }

        public async Task<(int totalCount, IEnumerable<AssessmentJournal> list)> GetAllJournalRequestForOffice(int? officeId, int pageNo)
        {
            return await _unitOfWork.AssessmentJournals.GetAllJournalRequestForOffice(officeId, pageNo);
        }


        private async void createTransaction(AssessmentBalance assessmentBalance, AssessmentTransactionsType trp)
        {
            //if (assessmentBalance == null || assessmentBalance.Q1 == null || assessmentBalance.Q2 == null || assessmentBalance.Q3 == null || assessmentBalance.Q4 == null) { }

            var q1 = (!assessmentBalance!.Q1!.IsCompleted && !assessmentBalance.Q1.IsOver) ? assessmentBalance.Q1!.Amount - assessmentBalance.Q1.ByExcessDeduction - assessmentBalance.Q1.Paid - assessmentBalance.Q1.Discount : 0;
            var q2 = (!assessmentBalance!.Q2!.IsCompleted && !assessmentBalance.Q2.IsOver) ? assessmentBalance.Q2!.Amount - assessmentBalance.Q2.ByExcessDeduction - assessmentBalance.Q2.Paid - assessmentBalance.Q2.Discount : 0;
            var q3 = (!assessmentBalance!.Q3!.IsCompleted && !assessmentBalance.Q3.IsOver) ? assessmentBalance.Q3!.Amount - assessmentBalance.Q3.ByExcessDeduction - assessmentBalance.Q3.Paid - assessmentBalance.Q3.Discount : 0;
            var q4 = (!assessmentBalance!.Q4!.IsCompleted && !assessmentBalance.Q4.IsOver) ? assessmentBalance.Q4!.Amount - assessmentBalance.Q4.ByExcessDeduction - assessmentBalance.Q4.Paid - assessmentBalance.Q4.Discount : 0;
            var transaction = new AssessmentTransaction
            {
                AssessmentId = assessmentBalance.AssessmentId,
                //DateTime = DateTime.Now,
                Type = trp,
                LYArrears = assessmentBalance.LYArrears,
                LYWarrant = assessmentBalance.LYWarrant,
                TYArrears = assessmentBalance.TYArrears,
                TYWarrant = assessmentBalance.TYWarrant,
                RunningOverPay = assessmentBalance.OverPayment + assessmentBalance.ExcessPayment,
                Q1 = q1,
                Q2 = q2,
                Q3 = q3,
                Q4 = q4,
                RunningDiscount = assessmentBalance.Discount,
                RunningTotal =
                assessmentBalance.LYArrears
                + assessmentBalance.LYWarrant
                + assessmentBalance.TYArrears
                + assessmentBalance.TYWarrant
                + q1
                + q2
                + q3
                + q4
                - assessmentBalance.OverPayment - assessmentBalance.ExcessPayment,
            };

            await _unitOfWork.AssessmentTransactions.AddAsync(transaction);
        }

        private  bool discountProcess(ref Assessment  asmt, AssessmentRates rates ,int actionby,int month)
        {
            try
            {
                //var assessmentBalance = await _unitOfWork.AssessmentBalances.GetForJournal(assessmentId);

                if (rates != null && asmt.AssessmentBalance != null && asmt.AssessmentBalance.Q1 != null && asmt.AssessmentBalance.Q2 != null && asmt.AssessmentBalance.Q3 != null && asmt.AssessmentBalance.Q4 != null && rates != null)
                {
                    /*****
                         op => outstandingPayable
                         payable => payable amount for year
                         deduction => deduction amount from last year overpayment
                         paying => paying quantities    
                         nextbal => next time balance after paying
                         discount => applied discount
                         dctRate => discount Rates   
                         */


                    (var op, var payable, var deduction, var paying, var nextbal, var discount, var dctRate) = _assessmentBalanceService.CalculatePaymentBalance(asmt.AssessmentBalance, rates, 0, month, false);

                    asmt.AssessmentBalance.ByExcessDeduction += deduction.Total;
                    //assessmentBalance.Paid += mxOrder.TotalAmount;
                    asmt.AssessmentBalance.ExcessPayment = 0;
                    asmt.AssessmentBalance.DiscountRate = discount.Total > 0 ? dctRate : 0;
                    asmt.AssessmentBalance.Discount += discount.Total;
                    asmt.AssessmentBalance.OverPayment += paying.OverPayment += deduction.OverPayment != 0 ? deduction.OverPayment : 0;

                    asmt.AssessmentBalance.LYWarrant = nextbal.LYWarrant;
                    asmt.AssessmentBalance.LYArrears = nextbal.LYArrears;

                    asmt.AssessmentBalance.TYWarrant = nextbal.TYWarrant;
                    asmt.AssessmentBalance.TYArrears = nextbal.TYArrears;




                    if (!asmt.AssessmentBalance.Q1.IsOver && !asmt.AssessmentBalance.Q1.IsCompleted && deduction.Q1 != 0 && paying.Q1 == 0)

                    {
                        asmt.AssessmentBalance.Q1.ByExcessDeduction += deduction.Q1;
                        asmt.AssessmentBalance.Q1.Paid += paying.Q1;
                        asmt.AssessmentBalance.Q1.Discount += discount.Q1;
                        asmt.AssessmentBalance.Q1.IsCompleted = asmt.AssessmentBalance.Q1.Amount - asmt.AssessmentBalance.Q1.ByExcessDeduction - asmt.AssessmentBalance.Q1.Paid - asmt.AssessmentBalance.Q1.Discount == 0 ? true : false;
                    }

                    if (!asmt.AssessmentBalance.Q2.IsOver && !asmt.AssessmentBalance.Q2.IsCompleted && deduction.Q2 != 0 && paying.Q2 == 0)
                    {
                        asmt.AssessmentBalance.Q2.ByExcessDeduction += deduction.Q2;
                        asmt.AssessmentBalance.Q2.Paid += paying.Q2;
                        asmt.AssessmentBalance.Q2.Discount += discount.Q2;
                         asmt.AssessmentBalance.Q2.IsCompleted = asmt.AssessmentBalance.Q2.Amount - asmt.AssessmentBalance.Q2.ByExcessDeduction - asmt.AssessmentBalance.Q2.Paid - asmt.AssessmentBalance.Q2.Discount == 0 ? true : false;
                    }

                    if (!asmt.AssessmentBalance.Q3.IsOver && !asmt.AssessmentBalance.Q3.IsCompleted && deduction.Q3 != 0 && paying.Q3 == 0)
                    {
                        asmt.AssessmentBalance.Q3.ByExcessDeduction += deduction.Q3;
                        asmt.AssessmentBalance.Q3.Paid += paying.Q3;
                        asmt.AssessmentBalance.Q3.Discount += discount.Q3;
                        asmt.AssessmentBalance.Q3.IsCompleted = asmt.AssessmentBalance.Q3.Amount - asmt.AssessmentBalance.Q3.ByExcessDeduction - asmt.AssessmentBalance.Q3.Paid -asmt.AssessmentBalance.Q3.Discount == 0 ? true : false;
                    }

                    if (!asmt.AssessmentBalance.Q4.IsOver && !asmt.AssessmentBalance.Q4.IsCompleted && deduction.Q4 != 0 && paying.Q4 == 0)
                    {
                        asmt.AssessmentBalance.Q4.ByExcessDeduction += deduction.Q4;
                        asmt.AssessmentBalance.Q4.Paid += paying.Q4;
                        asmt.AssessmentBalance.Q4.Discount += discount.Q4;
                        asmt.AssessmentBalance.Q4.IsCompleted = asmt.AssessmentBalance.Q4.Amount - asmt.AssessmentBalance.Q4.ByExcessDeduction - asmt.AssessmentBalance.Q4.Paid - asmt.AssessmentBalance.Q4.Discount == 0 ? true : false;
                    }

                    if (asmt.AssessmentBalance.Q1.IsCompleted && asmt.AssessmentBalance.Q2.IsCompleted && asmt.AssessmentBalance.Q3.IsCompleted && asmt.AssessmentBalance.Q4.IsCompleted)
                    {
                        asmt.AssessmentBalance.IsCompleted = true;
                    }

                    asmt.AssessmentBalance.UpdatedBy = actionby;
                    asmt.AssessmentBalance.UpdatedAt = DateTime.Now;
                    asmt.AssessmentBalance.HasTransaction = false;

                    //createTransaction(assessmentBalance, AssessmentTransactionsType.DiscountProcess);
                }
                else
                {

                    throw new Exception("Unable To Run Process.");
                }



                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
        private bool discountProcess(ref AssessmentBalance assessmentBalance, AssessmentRates rates, int actionby)
        {
            try
            {
                //var assessmentBalance = await _unitOfWork.AssessmentBalances.GetForJournal(assessmentId);

                if (rates != null && assessmentBalance != null && assessmentBalance.Q1 != null && assessmentBalance.Q2 != null && assessmentBalance.Q3 != null && assessmentBalance.Q4 != null && rates != null)
                {
                    /*****
                         op => outstandingPayable
                         payable => payable amount for year
                         deduction => deduction amount from last year overpayment
                         paying => paying quantities    
                         nextbal => next time balance after paying
                         discount => applied discount
                         dctRate => discount Rates   
                         */


                    (var op, var payable, var deduction, var paying, var nextbal, var discount, var dctRate) = _assessmentBalanceService.CalculatePaymentBalance(assessmentBalance, rates, 0, 1, false);

                    assessmentBalance.ByExcessDeduction += deduction.Total;
                    //assessmentBalance.Paid += mxOrder.TotalAmount;
                    assessmentBalance.ExcessPayment = 0;
                    assessmentBalance.DiscountRate = discount.Total > 0 ? dctRate : 0;
                    assessmentBalance.Discount += discount.Total;
                    assessmentBalance.OverPayment += paying.OverPayment += deduction.OverPayment != 0 ? deduction.OverPayment : 0;

                    assessmentBalance.LYWarrant = nextbal.LYWarrant;
                    assessmentBalance.LYArrears = nextbal.LYArrears;

                    assessmentBalance.TYWarrant = nextbal.TYWarrant;
                    assessmentBalance.TYArrears = nextbal.TYArrears;




                    if (!assessmentBalance.Q1.IsOver && !assessmentBalance.Q1.IsCompleted && deduction.Q1 != 0 && paying.Q1 == 0)

                    {
                        assessmentBalance.Q1.ByExcessDeduction += deduction.Q1;
                        assessmentBalance.Q1.Paid += paying.Q1;
                        assessmentBalance.Q1.Discount += discount.Q1;
                        assessmentBalance.Q1.IsCompleted = assessmentBalance.Q1.Amount - assessmentBalance.Q1.ByExcessDeduction - assessmentBalance.Q1.Paid - assessmentBalance.Q1.Discount == 0 ? true : false;
                    }

                    if (!assessmentBalance.Q2.IsOver && !assessmentBalance.Q2.IsCompleted && deduction.Q2 != 0 && paying.Q2 == 0)
                    {
                        assessmentBalance.Q2.ByExcessDeduction += deduction.Q2;
                        assessmentBalance.Q2.Paid += paying.Q2;
                        assessmentBalance.Q2.Discount += discount.Q2;
                        assessmentBalance.Q2.IsCompleted = assessmentBalance.Q2.Amount - assessmentBalance.Q2.ByExcessDeduction - assessmentBalance.Q2.Paid - assessmentBalance.Q2.Discount == 0 ? true : false;
                    }

                    if (!assessmentBalance.Q3.IsOver && !assessmentBalance.Q3.IsCompleted && deduction.Q3 != 0 && paying.Q3 == 0)
                    {
                        assessmentBalance.Q3.ByExcessDeduction += deduction.Q3;
                        assessmentBalance.Q3.Paid += paying.Q3;
                        assessmentBalance.Q3.Discount += discount.Q3;
                        assessmentBalance.Q3.IsCompleted = assessmentBalance.Q3.Amount - assessmentBalance.Q3.ByExcessDeduction - assessmentBalance.Q3.Paid - assessmentBalance.Q3.Discount == 0 ? true : false;
                    }

                    if (!assessmentBalance.Q4.IsOver && !assessmentBalance.Q4.IsCompleted && deduction.Q4 != 0 && paying.Q4 == 0)
                    {
                        assessmentBalance.Q4.ByExcessDeduction += deduction.Q4;
                        assessmentBalance.Q4.Paid += paying.Q4;
                        assessmentBalance.Q4.Discount += discount.Q4;
                        assessmentBalance.Q4.IsCompleted = assessmentBalance.Q4.Amount - assessmentBalance.Q4.ByExcessDeduction - assessmentBalance.Q4.Paid - assessmentBalance.Q4.Discount == 0 ? true : false;
                    }

                    if (assessmentBalance.Q1.IsCompleted && assessmentBalance.Q2.IsCompleted && assessmentBalance.Q3.IsCompleted && assessmentBalance.Q4.IsCompleted)
                    {
                        assessmentBalance.IsCompleted = true;
                    }

                    assessmentBalance.UpdatedBy = actionby;
                    assessmentBalance.UpdatedAt = DateTime.Now;
                    assessmentBalance.HasTransaction = false;

                    //createTransaction(assessmentBalance, AssessmentTransactionsType.DiscountProcess);
                }
                else
                {

                    throw new Exception("Unable To Run Process.");
                }



                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
        private async Task<bool> UpdateLedgerAccountsRollBack(List<int?> assessmentIds, HTokenClaim token)
        {
            try
            {
                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                if (session != null)
                {

                    var assmts = await _unitOfWork.Assessments.GetForInitProcessForFinalAccount(assessmentIds);

                    var votes = await _unitOfWork.AssmtVoteAssigns.GetAllForSabha(token.sabhaId);


                    var accumulatedFund = await _unitOfWork.SpecialLedgerAccounts.GetAccumulatedFundLedgerAccount(token.sabhaId);

                    var accumulatedFundBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(accumulatedFund.VoteId!.Value, token.sabhaId, session.StartAt.Year);

                    if (accumulatedFundBalance == null)
                    {
                        accumulatedFundBalance = await _voteBalanceService.CreateNewVoteBalance(accumulatedFund.VoteId!.Value, session, token);


                    }




                    foreach (var asmt in assmts)
                    {

                        decimal? LYArrears = 0m;
                        decimal? LYWarrant = 0m;
                        decimal? TYArrears = 0m;
                        decimal? TYWarrant = 0m;
                        decimal? OverPay = 0m;
                        decimal? IncomeAmount = asmt.AssessmentBalance!.AnnualAmount;
                        decimal? WarrantAmount = asmt.AssessmentBalance!.Q1!.Warrant + asmt.AssessmentBalance.Q2!.Warrant + asmt.AssessmentBalance.Q3!.Warrant + asmt.AssessmentBalance.Q4!.Warrant;

                        var sysAdj = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.SystemAdjustment).LastOrDefault();
                        var jnlAdj = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.JournalAdjustment).LastOrDefault();
                        var init = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.Init).FirstOrDefault();


                        //var voteBalance = await unitOfWork.VoteBalances.GetActiveVoteBalance(item.VoteId, sabhaId, item.Year);

                        if (sysAdj != null)
                        {

                            LYArrears = sysAdj.LYArrears;
                            LYWarrant = sysAdj.LYWarrant;
                            TYArrears = sysAdj.TYArrears;
                            TYWarrant = sysAdj.TYWarrant;
                            OverPay = sysAdj.RunningOverPay;

                        }
                        else if (jnlAdj != null)
                        {
                            LYArrears = jnlAdj.LYArrears;
                            LYWarrant = jnlAdj.LYWarrant;
                            TYArrears = jnlAdj.TYArrears;
                            TYWarrant = jnlAdj.TYWarrant;
                            OverPay = jnlAdj.RunningOverPay;

                        }
                        else if (init != null)
                        {
                            LYArrears = init.LYArrears;
                            LYWarrant = init.LYWarrant;
                            TYArrears = init.TYArrears;
                            TYWarrant = init.TYWarrant;
                            OverPay = init.RunningOverPay;






                        }

                        if (asmt.AssessmentBalance!.Q1!.StartDate.HasValue && asmt.AssessmentBalance!.Q1!.StartDate == asmt.AssessmentBalance.Q1.EndDate)
                        {
                            IncomeAmount -= asmt.AssessmentBalance.Q1.Amount;

                        }

                        if (asmt.AssessmentBalance!.Q2!.StartDate.HasValue && asmt.AssessmentBalance!.Q2!.StartDate == asmt.AssessmentBalance.Q2.EndDate)
                        {
                            IncomeAmount -= asmt.AssessmentBalance.Q2.Amount;
                        }

                        if (asmt.AssessmentBalance!.Q3!.StartDate.HasValue && asmt.AssessmentBalance!.Q3!.StartDate == asmt.AssessmentBalance.Q3.EndDate)
                        {
                            IncomeAmount -= asmt.AssessmentBalance.Q3.Amount;
                        }

                        if (asmt.AssessmentBalance!.Q4!.StartDate.HasValue && asmt.AssessmentBalance!.Q4!.StartDate == asmt.AssessmentBalance.Q4.EndDate)
                        {
                            IncomeAmount -= asmt.AssessmentBalance.Q4.Amount;
                        }


                        /*
                         * ii =Last Year Warrant
                         * i = Last Year Arrears
                         * iv = This Year Warrant
                         * iii = This Year Arrears
                         * 

                        1	Last year Warent
                        2	Last year Arrears
                        3	This year Warent
                        4	This Year Arrears
                        5	Tax payment
                        6	Over Payment

                         */


                        var voteBalanceLYWarrant = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 1).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, token.sabhaId, session.StartAt.Year);

                        if (voteBalanceLYWarrant == null)
                        {
                            voteBalanceLYWarrant = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 1).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                        }

                        if (voteBalanceLYWarrant != null)
                        {

                            voteBalanceLYWarrant.Credit += (decimal)LYWarrant!;
                            voteBalanceLYWarrant.UpdatedBy = token.userId;
                            voteBalanceLYWarrant.UpdatedAt = session.StartAt;
                            voteBalanceLYWarrant.SystemActionAt = DateTime.Now;
                            voteBalanceLYWarrant.ExchangedAmount = (decimal)LYWarrant!;


                            voteBalanceLYWarrant.TransactionType = VoteBalanceTransactionTypes.Credit;
                            //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                            voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant.Debit - voteBalanceLYWarrant.Credit;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceLYWarrant);
                            voteBalanceLYWarrant.ExchangedAmount = 0;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM LYW JNL RB";
                            vtbLog.SubCode = asmt.AssessmentNo;

                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;
                            vtbLog.OfficeId = asmt.OfficeId;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                            CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);


                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Last year Warrant");
                        }


                        /******************/

                        if (accumulatedFund != null)
                        {

                            accumulatedFundBalance.Debit += (decimal)LYWarrant!;
                            accumulatedFundBalance.UpdatedBy = token.userId;
                            accumulatedFundBalance.UpdatedAt = session.StartAt;
                            accumulatedFundBalance.SystemActionAt = DateTime.Now;
                            accumulatedFundBalance.ExchangedAmount = (decimal)LYWarrant!;


                            accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.Debit;
                            //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                            accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                            accumulatedFundBalance.ExchangedAmount = 0;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM LYW JNL RB";
                            vtbLog.SubCode = asmt.AssessmentNo;

                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;
                            vtbLog.OfficeId = asmt.OfficeId;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);



                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Accumulated Fund");
                        }

                        /******************/



                        var voteBalanceLYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 2).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, token.sabhaId, session.StartAt.Year);


                        if (voteBalanceLYArrears == null)
                        {
                            voteBalanceLYArrears = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 2).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                        }

                        if (voteBalanceLYArrears != null)
                        {
                            voteBalanceLYArrears.Credit += (decimal)LYArrears!;
                            voteBalanceLYArrears.UpdatedBy = token.userId;
                            voteBalanceLYArrears.UpdatedAt = session.StartAt;
                            voteBalanceLYArrears.SystemActionAt = DateTime.Now;
                            voteBalanceLYArrears.ExchangedAmount = (decimal)LYArrears!;

                            voteBalanceLYArrears.TransactionType = VoteBalanceTransactionTypes.Credit;
                            voteBalanceLYArrears.CreditDebitRunningBalance = voteBalanceLYArrears.Debit - voteBalanceLYArrears.Credit;

                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceLYArrears);
                            voteBalanceLYArrears.ExchangedAmount = 0;


                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM LYA JNL RB";
                            vtbLog.SubCode = asmt.AssessmentNo;
                            vtbLog.OfficeId = asmt.OfficeId;
                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;

                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Last year Arrears");
                        }


                        /******************/

                        if (accumulatedFund != null)
                        {

                            accumulatedFundBalance.Debit += (decimal)LYArrears!;
                            accumulatedFundBalance.UpdatedBy = token.userId;
                            accumulatedFundBalance.UpdatedAt = session.StartAt;
                            accumulatedFundBalance.SystemActionAt = DateTime.Now;
                            accumulatedFundBalance.ExchangedAmount = (decimal)LYWarrant!;


                            accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.Debit;
                            //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                            accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                            accumulatedFundBalance.ExchangedAmount = 0;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM LYA JNL RB";
                            vtbLog.SubCode = asmt.AssessmentNo;

                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;
                            vtbLog.OfficeId = asmt.OfficeId;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);




                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Accumulated Fund");
                        }

                        /******************/

                        var voteBalanceTYWarrant = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 3).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, token.sabhaId, session.StartAt.Year);


                        if (voteBalanceTYWarrant == null)
                        {
                            voteBalanceTYWarrant = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 3).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                        }

                        if (voteBalanceTYWarrant != null)
                        {

                            voteBalanceTYWarrant.Credit += (decimal)TYWarrant! + (decimal)WarrantAmount!;
                            voteBalanceTYWarrant.UpdatedBy = token.userId;
                            voteBalanceTYWarrant.UpdatedAt = session.StartAt;
                            voteBalanceTYWarrant.SystemActionAt = DateTime.Now;
                            voteBalanceTYWarrant.ExchangedAmount = (decimal)TYWarrant! + (decimal)WarrantAmount!;

                            voteBalanceTYWarrant.TransactionType = VoteBalanceTransactionTypes.Credit;
                            voteBalanceTYWarrant.CreditDebitRunningBalance = voteBalanceTYWarrant.Debit - voteBalanceTYWarrant.Credit;

                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceTYWarrant);
                            voteBalanceTYWarrant.ExchangedAmount = 0m;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM TYW JNL RB";
                            vtbLog.SubCode = asmt.AssessmentNo;

                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;
                            vtbLog.OfficeId = asmt.OfficeId;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                            /**********/
                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found This year Warrant");
                        }


                        /******************/

                        if (accumulatedFund != null)
                        {

                            accumulatedFundBalance.Debit += (decimal)TYWarrant!;
                            accumulatedFundBalance.UpdatedBy = token.userId;
                            accumulatedFundBalance.UpdatedAt = session.StartAt;
                            accumulatedFundBalance.SystemActionAt = DateTime.Now;
                            accumulatedFundBalance.ExchangedAmount = (decimal)LYWarrant!;


                            accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.Debit;
                            //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                            accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                            accumulatedFundBalance.ExchangedAmount = 0;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM TYW JNL RB";
                            vtbLog.SubCode = asmt.AssessmentNo;

                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;
                            vtbLog.OfficeId = asmt.OfficeId;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);




                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Accumulated Fund");
                        }

                        /******************/

                        var voteBalanceTYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 4).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, token.sabhaId, session.StartAt.Year);


                        if (voteBalanceTYArrears == null)
                        {
                            voteBalanceTYArrears = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 4).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                        }

                        if (voteBalanceTYArrears != null)
                        {

                            voteBalanceTYArrears.Credit += (decimal)TYArrears!;
                            voteBalanceTYArrears.UpdatedBy = token.userId;
                            voteBalanceTYArrears.UpdatedAt = session.StartAt;
                            voteBalanceTYArrears.SystemActionAt = DateTime.Now;

                            voteBalanceTYArrears.TransactionType = VoteBalanceTransactionTypes.Credit;
                            voteBalanceTYArrears.CreditDebitRunningBalance = voteBalanceTYArrears.Debit - voteBalanceTYArrears.Credit;
                            voteBalanceTYArrears.ExchangedAmount = (decimal)TYArrears!;

                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceTYArrears);
                            voteBalanceTYArrears.ExchangedAmount = 0m;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM TYA JNL RB";
                            vtbLog.SubCode = asmt.AssessmentNo;
                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;
                            vtbLog.OfficeId = asmt.OfficeId;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                            /**********/
                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For This Year Arrears");
                        }


                        /******************/

                        if (accumulatedFund != null)
                        {

                            accumulatedFundBalance.Debit += (decimal)TYArrears!;
                            accumulatedFundBalance.UpdatedBy = token.userId;
                            accumulatedFundBalance.UpdatedAt = session.StartAt;
                            accumulatedFundBalance.SystemActionAt = DateTime.Now;
                            accumulatedFundBalance.ExchangedAmount = (decimal)LYWarrant!;


                            accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.Debit;
                            //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                            accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                            accumulatedFundBalance.ExchangedAmount = 0;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM TYA JNL RB";
                            vtbLog.SubCode = asmt.AssessmentNo;

                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;
                            vtbLog.OfficeId = asmt.OfficeId;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);


                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Accumulated Fund");
                        }

                        /******************/

                        var voteBalanceIncome = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 5).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, token.sabhaId, session.StartAt.Year);


                        if (voteBalanceIncome == null)
                        {
                            voteBalanceIncome = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 5).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                        }


                        if (voteBalanceIncome != null)
                        {
                            voteBalanceIncome.Credit += (decimal)IncomeAmount!;
                            voteBalanceIncome.UpdatedBy = token.userId;
                            voteBalanceIncome.UpdatedAt = session.StartAt;
                            voteBalanceIncome.SystemActionAt = DateTime.Now;

                            voteBalanceIncome.TransactionType = VoteBalanceTransactionTypes.Credit;
                            voteBalanceIncome.CreditDebitRunningBalance = voteBalanceIncome.Debit - voteBalanceIncome.Credit;
                            voteBalanceIncome.ExchangedAmount = (decimal)IncomeAmount!;

                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceIncome);
                            voteBalanceIncome.ExchangedAmount = 0m;




                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM Taxing JNL RB";
                            vtbLog.SubCode = asmt.AssessmentNo;
                            vtbLog.OfficeId = asmt.OfficeId;
                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;

                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                            /**********/


                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Tax Payment");
                        }

                        var voteBalanceOverPay = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 6).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, token.sabhaId, session.StartAt.Year);


                        if (voteBalanceOverPay == null)
                        {
                            voteBalanceOverPay = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 6).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                        }

                        if (voteBalanceOverPay != null)
                        {
                            voteBalanceOverPay.Debit += (decimal)OverPay!;
                            voteBalanceOverPay.UpdatedBy = token.userId;
                            voteBalanceOverPay.UpdatedAt = session.StartAt;
                            voteBalanceOverPay.SystemActionAt = DateTime.Now;

                            voteBalanceOverPay.TransactionType = VoteBalanceTransactionTypes.Debit;
                            voteBalanceOverPay.CreditDebitRunningBalance = voteBalanceOverPay.Credit - voteBalanceOverPay.Debit;
                            voteBalanceOverPay.ExchangedAmount = (decimal)OverPay!;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceOverPay);
                            voteBalanceOverPay.ExchangedAmount = 0m;


                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM OverPay JNL RB";
                            vtbLog.SubCode = asmt.AssessmentNo;
                            vtbLog.OfficeId = asmt.OfficeId;
                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;

                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.CREDIT);

                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Over Payment");
                        }

                        /******************/

                        if (accumulatedFund != null)
                        {

                            accumulatedFundBalance.Credit += (decimal)OverPay!;
                            accumulatedFundBalance.UpdatedBy = token.userId;
                            accumulatedFundBalance.UpdatedAt = session.StartAt;
                            accumulatedFundBalance.SystemActionAt = DateTime.Now;
                            accumulatedFundBalance.ExchangedAmount = (decimal)OverPay!;


                            accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.Credit;
                            //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                            accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                            accumulatedFundBalance.ExchangedAmount = 0;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM OverPay JNL RB";
                            vtbLog.SubCode = asmt.AssessmentNo;

                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;
                            vtbLog.OfficeId = asmt.OfficeId;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);




                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Accumulated Fund");
                        }

                        /******************/

                        var voteBalanceTaxBilling = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 7).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, token.sabhaId, session.StartAt.Year);


                        if (voteBalanceTaxBilling == null)
                        {
                            voteBalanceTaxBilling = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 7).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                        }


                        if (voteBalanceTaxBilling != null)
                        {

                            //var billingAmount  = asmt.AssessmentBalance!.Q1!.Amount + asmt.AssessmentBalance.Q2.Amount + asmt.AssessmentBalance.Q3.Amount + asmt.AssessmentBalance.Q4.Amount;

                            voteBalanceTaxBilling.Debit += (decimal)IncomeAmount!;
                            voteBalanceTaxBilling.UpdatedBy = token.userId;
                            voteBalanceTaxBilling.UpdatedAt = session.StartAt;
                            voteBalanceTaxBilling.SystemActionAt = DateTime.Now;

                            voteBalanceTaxBilling.TransactionType = VoteBalanceTransactionTypes.Debit;
                            voteBalanceTaxBilling.ExchangedAmount = (decimal)IncomeAmount!;




                            voteBalanceTaxBilling.CreditDebitRunningBalance = voteBalanceTaxBilling.Credit - voteBalanceTaxBilling.Debit;

                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceTaxBilling);
                            voteBalanceTaxBilling.ExchangedAmount = 0m;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM Billing JNL RB";
                            vtbLog.SubCode = asmt.AssessmentNo;
                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;
                            vtbLog.OfficeId = asmt.OfficeId;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.CREDIT);

                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found Tax Billing");
                        }

                        var voteBalanceWarrantBilling = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 8).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, token.sabhaId, session.StartAt.Year);

                        if (voteBalanceWarrantBilling == null)
                        {
                            voteBalanceWarrantBilling = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 8).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                        }


                        if (voteBalanceWarrantBilling != null)
                        {


                            voteBalanceWarrantBilling.Debit += (decimal)WarrantAmount!;
                            voteBalanceWarrantBilling.UpdatedBy = token.userId;
                            voteBalanceWarrantBilling.UpdatedAt = session.StartAt;
                            voteBalanceWarrantBilling.SystemActionAt = DateTime.Now;

                            voteBalanceWarrantBilling.TransactionType = VoteBalanceTransactionTypes.Debit;

                            voteBalanceWarrantBilling.CreditDebitRunningBalance = voteBalanceWarrantBilling.Credit - voteBalanceWarrantBilling.Debit;
                            voteBalanceWarrantBilling.ExchangedAmount = (decimal)WarrantAmount!;

                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceWarrantBilling);
                            voteBalanceWarrantBilling.ExchangedAmount = 0m;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM Warranting JNL RB";
                            vtbLog.SubCode = asmt.AssessmentNo;
                            vtbLog.OfficeId = asmt.OfficeId;
                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;

                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.CREDIT);

                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Warrant Billing");
                        }
                    }

                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        private async Task<bool> UpdateLedgerAccounts(List<int?> assessmentIds, HTokenClaim token)
        {
            try
            {
                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                if (session != null)
                {

                    var assmts = await _unitOfWork.Assessments.GetForInitProcessForFinalAccount(assessmentIds);

                    var votes = await _unitOfWork.AssmtVoteAssigns.GetAllForSabha(token.sabhaId);


                    var accumulatedFund = await _unitOfWork.SpecialLedgerAccounts.GetAccumulatedFundLedgerAccount(token.sabhaId);

                    var accumulatedFundBalance = await _unitOfWork.VoteBalances.GetActiveVoteBalance(accumulatedFund.VoteId!.Value, token.sabhaId, session.StartAt.Year);

                    if (accumulatedFundBalance == null)
                    {
                        accumulatedFundBalance = await _voteBalanceService.CreateNewVoteBalance(accumulatedFund.VoteId!.Value, session, token);


                    }




                    foreach (var asmt in assmts)
                    {

                        decimal? LYArrears = 0m;
                        decimal? LYWarrant = 0m;
                        decimal? TYArrears = 0m;
                        decimal? TYWarrant = 0m;
                        decimal? OverPay = 0m;
                        decimal? IncomeAmount = asmt.AssessmentBalance!.AnnualAmount;
                        decimal? WarrantAmount = asmt.AssessmentBalance!.Q1!.Warrant + asmt.AssessmentBalance.Q2!.Warrant + asmt.AssessmentBalance.Q3!.Warrant + asmt.AssessmentBalance.Q4!.Warrant;

                        var sysAdj = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.SystemAdjustment).LastOrDefault();
                        var jnlAdj = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.JournalAdjustment).LastOrDefault();
                        var init = asmt.Transactions!.Where(asmt => asmt.Type == AssessmentTransactionsType.Init).FirstOrDefault();


                        //var voteBalance = await unitOfWork.VoteBalances.GetActiveVoteBalance(item.VoteId, sabhaId, item.Year);

                        if (sysAdj != null)
                        {

                            LYArrears = sysAdj.LYArrears;
                            LYWarrant = sysAdj.LYWarrant;
                            TYArrears = sysAdj.TYArrears;
                            TYWarrant = sysAdj.TYWarrant;
                            OverPay = sysAdj.RunningOverPay;

                        }
                        else if (jnlAdj != null)
                        {
                            LYArrears = jnlAdj.LYArrears;
                            LYWarrant = jnlAdj.LYWarrant;
                            TYArrears = jnlAdj.TYArrears;
                            TYWarrant = jnlAdj.TYWarrant;
                            OverPay = jnlAdj.RunningOverPay;

                        }
                        else if (init != null)
                        {
                            LYArrears = init.LYArrears;
                            LYWarrant = init.LYWarrant;
                            TYArrears = init.TYArrears;
                            TYWarrant = init.TYWarrant;
                            OverPay = init.RunningOverPay;






                        }

                        if (asmt.AssessmentBalance!.Q1!.StartDate.HasValue && asmt.AssessmentBalance!.Q1!.StartDate == asmt.AssessmentBalance.Q1.EndDate)
                        {
                            IncomeAmount -= asmt.AssessmentBalance.Q1.Amount;

                        }

                        if (asmt.AssessmentBalance!.Q2!.StartDate.HasValue && asmt.AssessmentBalance!.Q2!.StartDate == asmt.AssessmentBalance.Q2.EndDate)
                        {
                            IncomeAmount -= asmt.AssessmentBalance.Q2.Amount;
                        }

                        if (asmt.AssessmentBalance!.Q3!.StartDate.HasValue && asmt.AssessmentBalance!.Q3!.StartDate == asmt.AssessmentBalance.Q3.EndDate)
                        {
                            IncomeAmount -= asmt.AssessmentBalance.Q3.Amount;
                        }

                        if (asmt.AssessmentBalance!.Q4!.StartDate.HasValue && asmt.AssessmentBalance!.Q4!.StartDate == asmt.AssessmentBalance.Q4.EndDate)
                        {
                            IncomeAmount -= asmt.AssessmentBalance.Q4.Amount;
                        }


                        /*
                         * ii =Last Year Warrant
                         * i = Last Year Arrears
                         * iv = This Year Warrant
                         * iii = This Year Arrears
                         * 

                        1	Last year Warent
                        2	Last year Arrears
                        3	This year Warent
                        4	This Year Arrears
                        5	Tax payment
                        6	Over Payment

                         */


                        var voteBalanceLYWarrant = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 1).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, token.sabhaId, session.StartAt.Year);

                        if (voteBalanceLYWarrant == null)
                        {
                            voteBalanceLYWarrant = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 1).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                        }

                        if (voteBalanceLYWarrant != null)
                        {

                            voteBalanceLYWarrant.Debit += (decimal)LYWarrant!;
                            voteBalanceLYWarrant.UpdatedBy = token.userId;
                            voteBalanceLYWarrant.UpdatedAt = session.StartAt;
                            voteBalanceLYWarrant.SystemActionAt = DateTime.Now;
                            voteBalanceLYWarrant.ExchangedAmount = (decimal)LYWarrant!;


                            voteBalanceLYWarrant.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                            //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                            voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant.Debit - voteBalanceLYWarrant.Credit;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceLYWarrant);
                            voteBalanceLYWarrant.ExchangedAmount = 0;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM LYW JNL";
                            vtbLog.SubCode = asmt.AssessmentNo;

                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;
                            vtbLog.OfficeId = asmt.OfficeId;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);

                            CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);


                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Last year Warrant");
                        }


                        /******************/

                        if (accumulatedFund != null)
                        {

                            accumulatedFundBalance.Credit += (decimal)LYWarrant!;
                            accumulatedFundBalance.UpdatedBy = token.userId;
                            accumulatedFundBalance.UpdatedAt = session.StartAt;
                            accumulatedFundBalance.SystemActionAt = DateTime.Now;
                            accumulatedFundBalance.ExchangedAmount = (decimal)LYWarrant!;


                            accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                            //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                            accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                            accumulatedFundBalance.ExchangedAmount = 0;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM LYW JNL";
                            vtbLog.SubCode = asmt.AssessmentNo;

                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;
                            vtbLog.OfficeId = asmt.OfficeId;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);



                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Accumulated Fund");
                        }

                        /******************/



                        var voteBalanceLYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 2).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, token.sabhaId, session.StartAt.Year);


                        if (voteBalanceLYArrears == null)
                        {
                            voteBalanceLYArrears = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 2).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                        }

                        if (voteBalanceLYArrears != null)
                        {
                            voteBalanceLYArrears.Debit += (decimal)LYArrears!;
                            voteBalanceLYArrears.UpdatedBy = token.userId;
                            voteBalanceLYArrears.UpdatedAt = session.StartAt;
                            voteBalanceLYArrears.SystemActionAt = DateTime.Now;
                            voteBalanceLYArrears.ExchangedAmount = (decimal)LYArrears!;

                            voteBalanceLYArrears.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                            voteBalanceLYArrears.CreditDebitRunningBalance = voteBalanceLYArrears.Debit - voteBalanceLYArrears.Credit;

                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceLYArrears);
                            voteBalanceLYArrears.ExchangedAmount = 0;


                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM LYA JNL";
                            vtbLog.SubCode = asmt.AssessmentNo;
                            vtbLog.OfficeId = asmt.OfficeId;
                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;

                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Last year Arrears");
                        }


                        /******************/

                        if (accumulatedFund != null)
                        {

                            accumulatedFundBalance.Credit += (decimal)LYArrears!;
                            accumulatedFundBalance.UpdatedBy = token.userId;
                            accumulatedFundBalance.UpdatedAt = session.StartAt;
                            accumulatedFundBalance.SystemActionAt = DateTime.Now;
                            accumulatedFundBalance.ExchangedAmount = (decimal)LYWarrant!;


                            accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                            //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                            accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                            accumulatedFundBalance.ExchangedAmount = 0;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM LYA JNL";
                            vtbLog.SubCode = asmt.AssessmentNo;

                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;
                            vtbLog.OfficeId = asmt.OfficeId;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);




                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Accumulated Fund");
                        }

                        /******************/

                        var voteBalanceTYWarrant = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 3).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, token.sabhaId, session.StartAt.Year);


                        if (voteBalanceTYWarrant == null)
                        {
                            voteBalanceTYWarrant = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 3).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                        }

                        if (voteBalanceTYWarrant != null)
                        {

                            voteBalanceTYWarrant.Debit += (decimal)TYWarrant! + (decimal)WarrantAmount!;
                            voteBalanceTYWarrant.UpdatedBy = token.userId;
                            voteBalanceTYWarrant.UpdatedAt = session.StartAt;
                            voteBalanceTYWarrant.SystemActionAt = DateTime.Now;
                            voteBalanceTYWarrant.ExchangedAmount = (decimal)TYWarrant! + (decimal)WarrantAmount!;

                            voteBalanceTYWarrant.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                            voteBalanceTYWarrant.CreditDebitRunningBalance = voteBalanceTYWarrant.Debit - voteBalanceTYWarrant.Credit;

                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceTYWarrant);
                            voteBalanceTYWarrant.ExchangedAmount = 0m;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM TYW JNL";
                            vtbLog.SubCode = asmt.AssessmentNo;

                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;
                            vtbLog.OfficeId = asmt.OfficeId;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                            /**********/
                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found This year Warrant");
                        }


                        /******************/

                        if (accumulatedFund != null)
                        {

                            accumulatedFundBalance.Credit += (decimal)TYWarrant!;
                            accumulatedFundBalance.UpdatedBy = token.userId;
                            accumulatedFundBalance.UpdatedAt = session.StartAt;
                            accumulatedFundBalance.SystemActionAt = DateTime.Now;
                            accumulatedFundBalance.ExchangedAmount = (decimal)LYWarrant!;


                            accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                            //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                            accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                            accumulatedFundBalance.ExchangedAmount = 0;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM TYW JNL";
                            vtbLog.SubCode = asmt.AssessmentNo;

                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;
                            vtbLog.OfficeId = asmt.OfficeId;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);




                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Accumulated Fund");
                        }

                        /******************/

                        var voteBalanceTYArrears = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 4).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, token.sabhaId, session.StartAt.Year);


                        if (voteBalanceTYArrears == null)
                        {
                            voteBalanceTYArrears = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 4).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                        }

                        if (voteBalanceTYArrears != null)
                        {

                            voteBalanceTYArrears.Debit += (decimal)TYArrears!;
                            voteBalanceTYArrears.UpdatedBy = token.userId;
                            voteBalanceTYArrears.UpdatedAt = session.StartAt;
                            voteBalanceTYArrears.SystemActionAt = DateTime.Now;

                            voteBalanceTYArrears.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                            voteBalanceTYArrears.CreditDebitRunningBalance = voteBalanceTYArrears.Debit - voteBalanceTYArrears.Credit;
                            voteBalanceTYArrears.ExchangedAmount = (decimal)TYArrears!;

                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceTYArrears);
                            voteBalanceTYArrears.ExchangedAmount = 0m;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM TYA JNL";
                            vtbLog.SubCode = asmt.AssessmentNo;
                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;
                            vtbLog.OfficeId = asmt.OfficeId;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                            /**********/
                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For This Year Arrears");
                        }


                        /******************/

                        if (accumulatedFund != null)
                        {

                            accumulatedFundBalance.Credit += (decimal)TYArrears!;
                            accumulatedFundBalance.UpdatedBy = token.userId;
                            accumulatedFundBalance.UpdatedAt = session.StartAt;
                            accumulatedFundBalance.SystemActionAt = DateTime.Now;
                            accumulatedFundBalance.ExchangedAmount = (decimal)LYWarrant!;


                            accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                            //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                            accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                            accumulatedFundBalance.ExchangedAmount = 0;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM TYA JNL";
                            vtbLog.SubCode = asmt.AssessmentNo;

                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;
                            vtbLog.OfficeId = asmt.OfficeId;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);


                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Accumulated Fund");
                        }

                        /******************/

                        var voteBalanceIncome = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 5).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, token.sabhaId, session.StartAt.Year);


                        if (voteBalanceIncome == null)
                        {
                            voteBalanceIncome = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 5).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                        }


                        if (voteBalanceIncome != null)
                        {
                            voteBalanceIncome.Debit += (decimal)IncomeAmount!;
                            voteBalanceIncome.UpdatedBy = token.userId;
                            voteBalanceIncome.UpdatedAt = session.StartAt;
                            voteBalanceIncome.SystemActionAt = DateTime.Now;

                            voteBalanceIncome.TransactionType = VoteBalanceTransactionTypes.Billing;
                            voteBalanceIncome.CreditDebitRunningBalance = voteBalanceIncome.Debit - voteBalanceIncome.Credit;
                            voteBalanceIncome.ExchangedAmount = (decimal)IncomeAmount!;

                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceIncome);
                            voteBalanceIncome.ExchangedAmount = 0m;




                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM Taxing JNL";
                            vtbLog.SubCode = asmt.AssessmentNo;
                            vtbLog.OfficeId = asmt.OfficeId;
                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;

                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.DEBIT);

                            /**********/


                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Tax Payment");
                        }

                        var voteBalanceOverPay = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 6).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, token.sabhaId, session.StartAt.Year);


                        if (voteBalanceOverPay == null)
                        {
                            voteBalanceOverPay = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 6).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                        }

                        if (voteBalanceOverPay != null)
                        {
                            voteBalanceOverPay.Credit += (decimal)OverPay!;
                            voteBalanceOverPay.UpdatedBy = token.userId;
                            voteBalanceOverPay.UpdatedAt = session.StartAt;
                            voteBalanceOverPay.SystemActionAt = DateTime.Now;

                            voteBalanceOverPay.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                            voteBalanceOverPay.CreditDebitRunningBalance = voteBalanceOverPay.Credit - voteBalanceOverPay.Debit;
                            voteBalanceOverPay.ExchangedAmount = (decimal)OverPay!;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceOverPay);
                            voteBalanceOverPay.ExchangedAmount = 0m;


                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM OverPay JNL";
                            vtbLog.SubCode = asmt.AssessmentNo;
                            vtbLog.OfficeId = asmt.OfficeId;
                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;

                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.CREDIT);

                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Over Payment");
                        }

                        /******************/

                        if (accumulatedFund != null)
                        {

                            accumulatedFundBalance.Debit += (decimal)OverPay!;
                            accumulatedFundBalance.UpdatedBy = token.userId;
                            accumulatedFundBalance.UpdatedAt = session.StartAt;
                            accumulatedFundBalance.SystemActionAt = DateTime.Now;
                            accumulatedFundBalance.ExchangedAmount = (decimal)OverPay!;


                            accumulatedFundBalance.TransactionType = VoteBalanceTransactionTypes.BFDebit;
                            //voteBalanceLYWarrant.CreditDebitRunningBalance = voteBalanceLYWarrant
                            accumulatedFundBalance.CreditDebitRunningBalance = accumulatedFundBalance.Credit - accumulatedFundBalance.Debit;


                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(accumulatedFundBalance);
                            accumulatedFundBalance.ExchangedAmount = 0;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM OverPay JNL";
                            vtbLog.SubCode = asmt.AssessmentNo;

                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;
                            vtbLog.OfficeId = asmt.OfficeId;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);




                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Accumulated Fund");
                        }

                        /******************/

                        var voteBalanceTaxBilling = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 7).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, token.sabhaId, session.StartAt.Year);


                        if (voteBalanceTaxBilling == null)
                        {
                            voteBalanceTaxBilling = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 7).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                        }


                        if (voteBalanceTaxBilling != null)
                        {

                            //var billingAmount  = asmt.AssessmentBalance!.Q1!.Amount + asmt.AssessmentBalance.Q2.Amount + asmt.AssessmentBalance.Q3.Amount + asmt.AssessmentBalance.Q4.Amount;

                            voteBalanceTaxBilling.Credit += (decimal)IncomeAmount!;
                            voteBalanceTaxBilling.UpdatedBy = token.userId;
                            voteBalanceTaxBilling.UpdatedAt = session.StartAt;
                            voteBalanceTaxBilling.SystemActionAt = DateTime.Now;

                            voteBalanceTaxBilling.TransactionType = VoteBalanceTransactionTypes.BFCredit;
                            voteBalanceTaxBilling.ExchangedAmount = (decimal)IncomeAmount!;




                            voteBalanceTaxBilling.CreditDebitRunningBalance = voteBalanceTaxBilling.Credit - voteBalanceTaxBilling.Debit;

                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceTaxBilling);
                            voteBalanceTaxBilling.ExchangedAmount = 0m;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM Billing JNL";
                            vtbLog.SubCode = asmt.AssessmentNo;
                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;
                            vtbLog.OfficeId = asmt.OfficeId;
                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.CREDIT);

                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found Tax Billing");
                        }

                        var voteBalanceWarrantBilling = await _unitOfWork.VoteBalances.GetActiveVoteBalance(votes.Where(v => v.PaymentTypeId == 8).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, token.sabhaId, session.StartAt.Year);

                        if (voteBalanceWarrantBilling == null)
                        {
                            voteBalanceWarrantBilling = await _voteBalanceService.CreateNewVoteBalance(votes.Where(v => v.PaymentTypeId == 8).Select(v => v.VoteDetailId).FirstOrDefault()!.Value, session, token);


                        }


                        if (voteBalanceWarrantBilling != null)
                        {


                            voteBalanceWarrantBilling.Credit += (decimal)WarrantAmount!;
                            voteBalanceWarrantBilling.UpdatedBy = token.userId;
                            voteBalanceWarrantBilling.UpdatedAt = session.StartAt;
                            voteBalanceWarrantBilling.SystemActionAt = DateTime.Now;

                            voteBalanceWarrantBilling.TransactionType = VoteBalanceTransactionTypes.BFCredit;

                            voteBalanceWarrantBilling.CreditDebitRunningBalance = voteBalanceWarrantBilling.Credit - voteBalanceWarrantBilling.Debit;
                            voteBalanceWarrantBilling.ExchangedAmount = (decimal)WarrantAmount!;

                            /*vote balance log */
                            var vtbLog = _mapper.Map<VoteBalance, VoteBalanceLog>(voteBalanceWarrantBilling);
                            voteBalanceWarrantBilling.ExchangedAmount = 0m;

                            vtbLog.Year = session.StartAt.Year;
                            vtbLog.Code = "ASM Warranting JNL";
                            vtbLog.SubCode = asmt.AssessmentNo;
                            vtbLog.OfficeId = asmt.OfficeId;
                            vtbLog.AppCategory = AppCategory.Assessment_Tax;
                            vtbLog.ModulePrimaryKey = asmt.Id;

                            await _unitOfWork.VoteBalanceLogs.AddAsync(vtbLog);
                            CreateJournalTransfer(vtbLog, CashBookTransactionType.CREDIT);

                            /**********/

                        }
                        else
                        {
                            throw new Exception("Vote Balance Not Found For Warrant Billing");
                        }
                    }

                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void CreateJournalTransfer(VoteBalanceLog vbl, CashBookTransactionType transactionType)
        {
            var journalTransfer = new InternalJournalTransfers
            {
                VoteBalanceId = vbl.VoteBalanceId,
                VoteDetailId = vbl.VoteDetailId,
                SabhaId = vbl.SabhaId,
                Year = vbl.Year,
                Month = vbl.Month,
                Code = vbl.Code,
                SubCode = vbl.SubCode,
                Description = vbl.Description,
                Status = vbl.Status,
                TransactionType = vbl.TransactionType,
                ModulePrimaryKey = vbl.ModulePrimaryKey,
                AppCategory = vbl.AppCategory,
                CreateBy = vbl.UpdatedBy,
                CreateAt = vbl.UpdatedAt,
                SystemActionAt = vbl.SystemActionAt
            };

            if (transactionType == CashBookTransactionType.CREDIT)
            {
                journalTransfer.Credit = vbl.ExchangedAmount;
            }
            else
            {
                journalTransfer.Debit = vbl.ExchangedAmount;
            }

            _unitOfWork.InternalJournalTransfers.AddAsync(journalTransfer);

        }


        private void UpdateAssessmentBalance(ref AssessmentBalance assessmentBalance,MixinOrder mxOrder,AssessmentRates rates)
        {
            /*****
                                  op => outstandingPayable
                                  payable => payable amount for year
                                  deduction => deduction amount from last year overpayment
                                  paying => paying quantities    
                                  nextbal => next time balance after paying
                                  discount => applied discount
                                  dctRate => discount Rates   
                                  */


            (var op, var payable, var deduction, var paying, var nextbal, var discount, var dctRate) = _assessmentBalanceService.CalculatePaymentBalance(assessmentBalance, rates, mxOrder.TotalAmount, mxOrder.CreatedAt.Month, true);

            assessmentBalance.ByExcessDeduction += deduction.Total;
            assessmentBalance.Paid += mxOrder.TotalAmount;
            assessmentBalance.ExcessPayment = 0;
            assessmentBalance.DiscountRate = discount.Total > 0 ? dctRate : assessmentBalance.DiscountRate;
            assessmentBalance.Discount += discount.Total;
            assessmentBalance.OverPayment += paying.OverPayment += deduction.OverPayment != 0 ? deduction.OverPayment : 0;

            assessmentBalance.LYWarrant = nextbal.LYWarrant;
            assessmentBalance.LYArrears = nextbal.LYArrears;

            assessmentBalance.TYWarrant = nextbal.TYWarrant;
            assessmentBalance.TYArrears = nextbal.TYArrears;




            if (!assessmentBalance.Q1.IsOver && !assessmentBalance.Q1.IsCompleted && (paying.Q1 != 0 || (paying.Q1 == 0 && discount.Q1 != 0)))

            {
                assessmentBalance.Q1.ByExcessDeduction += deduction.Q1;
                assessmentBalance.Q1.Paid += paying.Q1;
                assessmentBalance.Q1.Discount += discount.Q1;
                assessmentBalance.Q1.IsCompleted = assessmentBalance.Q1.Amount - assessmentBalance.Q1.ByExcessDeduction - assessmentBalance.Q1.Paid - assessmentBalance.Q1.Discount == 0 ? true : false;
            }

            if (!assessmentBalance.Q2.IsOver && !assessmentBalance.Q2.IsCompleted && (paying.Q2 != 0 || (paying.Q2 == 0 && discount.Q2 != 0)))
            {
                assessmentBalance.Q2.ByExcessDeduction += deduction.Q2;
                assessmentBalance.Q2.Paid += paying.Q2;
                assessmentBalance.Q2.Discount += discount.Q2;
                assessmentBalance.Q2.IsCompleted = assessmentBalance.Q2.Amount - assessmentBalance.Q2.ByExcessDeduction - assessmentBalance.Q2.Paid - assessmentBalance.Q2.Discount == 0 ? true : false;
            }

            if (!assessmentBalance.Q3.IsOver && !assessmentBalance.Q3.IsCompleted && (paying.Q3 != 0 || (paying.Q3 == 0 && discount.Q3 != 0)))
            {
                assessmentBalance.Q3.ByExcessDeduction += deduction.Q3;
                assessmentBalance.Q3.Paid += paying.Q3;
                assessmentBalance.Q3.Discount += discount.Q3;
                assessmentBalance.Q3.IsCompleted = assessmentBalance.Q3.Amount - assessmentBalance.Q3.ByExcessDeduction - assessmentBalance.Q3.Paid - assessmentBalance.Q3.Discount == 0 ? true : false;
            }

            if (!assessmentBalance.Q4.IsOver && !assessmentBalance.Q4.IsCompleted && (paying.Q4 != 0 || (paying.Q4 == 0 && discount.Q4 != 0)))
            {
                assessmentBalance.Q4.ByExcessDeduction += deduction.Q4;
                assessmentBalance.Q4.Paid += paying.Q4;
                assessmentBalance.Q4.Discount += discount.Q4;
                assessmentBalance.Q4.IsCompleted = assessmentBalance.Q4.Amount - assessmentBalance.Q4.ByExcessDeduction - assessmentBalance.Q4.Paid - assessmentBalance.Q4.Discount == 0 ? true : false;
            }

            if (assessmentBalance.Q1.IsCompleted && assessmentBalance.Q2.IsCompleted && assessmentBalance.Q3.IsCompleted && assessmentBalance.Q4.IsCompleted)
            {
                assessmentBalance.IsCompleted = true;
            }
        }


        private async Task<bool> ApproveJournalWithPayemnst(int jnlId, HTokenClaim token)
        {
            try
            {
                var jnl = await _unitOfWork.AssessmentJournals.GetByIdAsync(jnlId);





                return true;

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        //private async Task<MixinOrder> getNewMixOrder()
        //{

        //}

        private void Q1WarrentAdjustment(ref AssessmentBalance assessmentBalance, Assessment assmt,int warrantMethod)
        {
           
            

           
        }

    }


}