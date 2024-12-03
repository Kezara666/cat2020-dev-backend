using AutoMapper;
using CAT20.Core;
using CAT20.Core.DTO.Assessment.Save;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.AssessmentTax.Quarter;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.AssessmentTax;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.Vote;
using DocumentFormat.OpenXml.Spreadsheet;
using Irony.Parsing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Linq.Expressions;
using System.Transactions;
using AppCategory = CAT20.Core.Models.Enums.AppCategory;

namespace CAT20.Services.AssessmentTax
{

    public class AssessmentService : IAssessmentService
    {
        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        private readonly ILogger<AssessmentService> _logger;
        private readonly IMapper _mapper;
        private readonly IAssessmentBalanceService _assessmentBalanceService;
        private readonly IPartnerService _partnerService;
        private readonly IVoteBalanceService _voteBalanceService;

        public AssessmentService(ILogger<AssessmentService> logger,IMapper mapper, IAssessmentTaxUnitOfWork unitOfWork, IAssessmentBalanceService assessmentBalanceService, IPartnerService partnerService,IVoteBalanceService voteBalanceService)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _assessmentBalanceService = assessmentBalanceService;
            _partnerService = partnerService;
            _voteBalanceService = voteBalanceService;
        }

        public async Task<bool> ISAssessmentNoExist(string assessmentNo, int streetId, HTokenClaim token)
        {
            return await _unitOfWork.Assessments.ISAssessmentNoExist(assessmentNo,streetId, token);
        }
        public async Task<Assessment> GetById(int? id)
        {
            return await _unitOfWork.Assessments.GetById(id);
        }

        public async Task<(int totalCount, IEnumerable<Assessment> list)> GetAll(Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, Nullable<int> propertyDescription, string assessmentNo, Nullable<int> assessmentId, Nullable<int> partnerId, int pageNo, int pageSize)
        {
            try
            {
                return await _unitOfWork.Assessments.GetAll(sabhaId, officeId, wardId, streetId, propertyType, propertyDescription, assessmentNo, assessmentId, partnerId, pageNo, pageSize);
            }
            catch (Exception ex)
            {
                List<Assessment> list1 = new List<Assessment>();
                return (0, list1);
            }
        }

        //public async Task<IEnumerable<Assessment>> GetAll()
        //{
        //    return await _unitOfWork.Assessments.GetAll();
        //}

        public async Task<IEnumerable<Assessment>> GetAllForOffice(int officeid)
        {
            return await _unitOfWork.Assessments.GetAllForOffice(officeid);
        }

        public async Task<IEnumerable<Assessment>> GetAllForSabha(int sabhhaid)
        {
            return await _unitOfWork.Assessments.GetAllForSabha(sabhhaid);

        }

        public async Task<IEnumerable<Assessment>> GetAllForWard(int wardid)
        {
            return await _unitOfWork.Assessments.GetAllForWard(wardid);
        }
        public async Task<IEnumerable<Assessment>> GetAllForStreet(int streetid)
        {
            return await _unitOfWork.Assessments.GetAllForStreet(streetid);
        }
        public async Task<IEnumerable<Assessment>> GetAllForStreetWithOrdering(int streetId,HTokenClaim token)
        {
            return await _unitOfWork.Assessments.GetAllForStreetWithOrdering(streetId,token);
        }
        public async Task<IEnumerable<Assessment>> GetAllForCustomerId(int customerid)
        {
            return await _unitOfWork.Assessments.GetAllForCustomerId(customerid);
        }

        public async Task<Assessment> Create(Assessment newAssessment)
        {
            try
            {
                if(newAssessment.AssessmentTempPartner!=null && (newAssessment.AssessmentTempPartner.MobileNumber!=null || newAssessment.AssessmentTempPartner.NICNumber != null || newAssessment.AssessmentTempPartner.Email!=null))
                {
                    //if(newAssessment.AssessmentTempPartner.MobileNumber != null && newAssessment.AssessmentTempPartner.MobileNumber.Length>9)
                    //{ 
                    //        var partner =  _partnerService.GetByPhoneNo(newAssessment.AssessmentTempPartner.MobileNumber);
                    //        if(partner!=null)
                    //        {
                    //            newAssessment.IsPartnerUpdated = true;
                    //            newAssessment.PartnerId = partner.Id;
                    //        }
                    //}
                    //else if (newAssessment.AssessmentTempPartner.NICNumber != null && newAssessment.AssessmentTempPartner.NICNumber.Length > 9)
                    //{
                    //    var partner = _partnerService.GetByNIC(newAssessment.AssessmentTempPartner.NICNumber);
                    //    if (partner != null)
                    //    {
                    //        newAssessment.IsPartnerUpdated = true;
                    //        newAssessment.PartnerId = partner.Id;
                    //    }
                    //}
                    //else if(newAssessment.AssessmentTempPartner.Email != null && newAssessment.AssessmentTempPartner.Email.Length > 5)
                    //{
                    //    var partner = _partnerService.GetByEmail(newAssessment.AssessmentTempPartner.Email);
                    //    if (partner != null)
                    //    {
                    //        newAssessment.IsPartnerUpdated = true;
                    //        newAssessment.PartnerId = partner.Id;
                    //    }
                    //}
                    //else
                    //{
                    //    Partner partner = new Partner();
                    //    partner.Name = newAssessment.AssessmentTempPartner.Name ?? partner.Name;
                    //    partner.Street1 = newAssessment.AssessmentTempPartner.Street1 ?? partner.Street1;
                    //    partner.Street2 = newAssessment.AssessmentTempPartner.Street2 ?? partner.Street2;
                    //    partner.MobileNumber = newAssessment.AssessmentTempPartner.MobileNumber ?? partner.MobileNumber;
                    //    partner.NicNumber = newAssessment.AssessmentTempPartner.NICNumber ?? partner.NicNumber;
                    //    partner.Email = newAssessment.AssessmentTempPartner.Email ?? partner.Email;
                    //    partner.IsTempory = 0;
                    //    partner.IsEditable = 0;
                    //    partner.SabhaId = newAssessment.SabhaId;
                    //    partner.CreatedBy = newAssessment.CreatedBy.Value;
                    //    partner.GnDivisionId = -1;
                    //    partner.CreatedAt = DateTime.Now;

                    //    var createdpartner = _partnerService.Create(partner);

                    //    if (createdpartner != null)
                    //    {
                    //        newAssessment.IsPartnerUpdated = true;
                    //        newAssessment.PartnerId = partner.Id;
                    //    }
                    //}





                }
                await _unitOfWork.Assessments
                .AddAsync(newAssessment);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newAssessment.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newAssessment.ID,
                //    TransactionName = "Assessment",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return newAssessment;
        }

        public async Task<IEnumerable<Assessment>> BulkCreate(IEnumerable<Assessment> newObjsList)
        {
            try
            {
                foreach(var x in newObjsList)
                {
                    x.Allocation.NextYearAllocationAmount = x.Allocation.AllocationAmount;
                }

                int batchSize = 10000;
                _logger.LogInformation("Bulk Uploaded Started for Sabaha " + newObjsList.ToList()[0].SabhaId + " with " + newObjsList.ToList().Count + " records. {EventType}", "Assessment");

                if (newObjsList.Count() == 1)  //if a single asseement save
                {
                    Assessment newAssessment = new Assessment();
                    newAssessment = newObjsList.First();

                    //if (newAssessment.AssessmentTempPartner != null && (newAssessment.AssessmentTempPartner.MobileNumber != null || newAssessment.AssessmentTempPartner.NICNumber != null || newAssessment.AssessmentTempPartner.Email != null))
                    //{
                    //    if (newAssessment.AssessmentTempPartner.MobileNumber != null && newAssessment.AssessmentTempPartner.MobileNumber.Length > 9)
                    //    {
                    //        var partner = await _partnerService.GetByPhoneNo(newAssessment.AssessmentTempPartner.MobileNumber);
                    //        if (partner != null && partner.MobileNumber== newAssessment.AssessmentTempPartner.MobileNumber)
                    //        {
                    //            newAssessment.IsPartnerUpdated = true;
                    //            newAssessment.PartnerId = partner.Id;
                    //        }
                    //    }
                    //    else if (newAssessment.AssessmentTempPartner.NICNumber != null && newAssessment.AssessmentTempPartner.NICNumber.Length > 9)
                    //    {
                    //        var partner = await _partnerService.GetByNIC(newAssessment.AssessmentTempPartner.NICNumber);
                    //        if (partner != null && partner.NicNumber == newAssessment.AssessmentTempPartner.NICNumber)
                    //        {
                    //            newAssessment.IsPartnerUpdated = true;
                    //            newAssessment.PartnerId = partner.Id;
                    //        }
                    //    }
                    //    else if (newAssessment.AssessmentTempPartner.Email != null && newAssessment.AssessmentTempPartner.Email.Length > 5)
                    //    {
                    //        var partner = await _partnerService.GetByEmail(newAssessment.AssessmentTempPartner.Email);
                    //        if (partner != null && partner.Email == newAssessment.AssessmentTempPartner.Email)
                    //        {
                    //            newAssessment.IsPartnerUpdated = true;
                    //            newAssessment.PartnerId = partner.Id;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        newAssessment.IsPartnerUpdated = false;
                    //        newAssessment.PartnerId = 0;
                    //    }

                    //    if(newAssessment.IsPartnerUpdated == false)
                    //    {
                    //        Partner partner = new Partner();
                    //        partner.Name = newAssessment.AssessmentTempPartner.Name ?? partner.Name;
                    //        partner.Street1 = newAssessment.AssessmentTempPartner.Street1 ?? partner.Street1;
                    //        partner.Street2 = newAssessment.AssessmentTempPartner.Street2 ?? "-";
                    //        partner.MobileNumber = newAssessment.AssessmentTempPartner.MobileNumber ?? "-";
                    //        partner.NicNumber = newAssessment.AssessmentTempPartner.NICNumber ?? "-";
                    //        partner.Email = newAssessment.AssessmentTempPartner.Email ?? "-";
                    //        partner.Id = 0;
                    //        partner.IsTempory = 0;
                    //        partner.IsEditable = 0;
                    //        partner.SabhaId = newAssessment.SabhaId;
                    //        partner.CreatedBy = newAssessment.CreatedBy.Value;
                    //        partner.GnDivisionId = -1;
                    //        partner.CreatedAt = DateTime.Now;
                    //        try
                    //        {
                    //            var createdpartner = await _partnerService.Create(partner);

                    //            if (createdpartner != null && partner.Name == newAssessment.AssessmentTempPartner.Name)
                    //            {
                    //                newAssessment.IsPartnerUpdated = true;
                    //                newAssessment.PartnerId = partner.Id;
                    //            }
                    //            await _unitOfWork.Assessments.AddAsync(newAssessment);
                    //            await _unitOfWork.CommitAsync();
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            return null;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        await _unitOfWork.Assessments.AddAsync(newAssessment);
                    //        await _unitOfWork.CommitAsync();
                    //    }

                    //}

                    await _unitOfWork.Assessments.AddAsync(newAssessment);
                    await _unitOfWork.CommitAsync();
                }
                else  //if a bulk
                {
                    int runningtotal = 0;
                    for (int i = 0; i < newObjsList.Count(); i += batchSize)
                    {
                        var batch = newObjsList.Skip(i).Take(batchSize).ToList();
                        await _unitOfWork.Assessments.AddRangeAsync(batch);
                        runningtotal += batchSize;
                        _logger.LogInformation("Added " + runningtotal + " / " + newObjsList.ToList().Count + " of Sabha " + newObjsList.ToList()[0].SabhaId + ". {EventType}", "Assessment");
                    }
                    await _unitOfWork.CommitAsync();
                    _logger.LogInformation("Committed All " + newObjsList.ToList().Count + " records of Sabha " + newObjsList.ToList()[0].SabhaId + " and next Starting the init process. {EventType}", "Assessment");
                }

                //await _unitOfWork.Assessments
                //.AddRangeAsync(newObjsList);

                //await _unitOfWork.Assessments.AddRangeAsync(newObjsList);
                //await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Commit Failed " + newObjsList.ToList().Count + " records of Sabha " + newObjsList.ToList()[0].SabhaId + ". {EventType}", "Assessment");

                _logger.LogError(ex.InnerException, "First Init.{EventType}", "Assessment");

                Console.WriteLine(ex.Message.ToString());
                return new List<Assessment>();
            }
            return newObjsList;
        }

        public async Task<(bool, string)> CreateAssessmentForNextYear(List<Assessment> newObjsList, HTokenClaim token)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach(var newObject in newObjsList)
                    {
                        newObject.AssessmentStatus = AssessmentStatus.NextYearActive;
                    }

                    int batchSize = 10000;

                    int runningtotal = 0;
                    for (int i = 0; i < newObjsList.Count(); i += batchSize)
                    {
                        var batch = newObjsList.Skip(i).Take(batchSize).ToList();
                        await _unitOfWork.Assessments.AddRangeAsync(batch);
                        runningtotal += batchSize;
                        _logger.LogInformation("Added " + runningtotal + " / " + newObjsList.ToList().Count + " of Sabha " + newObjsList.ToList()[0].SabhaId + ". {EventType}", "Assessment");
                    }
                    await _unitOfWork.StepAsync();

                    await InitNextYearBalance(null, null, null, token);

                    await _unitOfWork.StepAsync();
                    transactionScope.Complete();

                    return (true, "Successfully Uploaded");

                }
                catch (Exception ex)
                {
                   
                    return (false, ex.Message.ToString());
                }
            }
        }

        public async Task Update(Assessment assessmentToBeUpdated, Assessment assessment)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (rentalPlaceToBeUpdated.NameEnglish !=assessment.NameEnglish)
                //    note.Append(" English Name :" +assessmentToBeUpdated.NameEnglish + " Change to " +assessment.NameEnglish);
                //if (rentalPlaceToBeUpdated.NameSinhala !=assessment.NameSinhala)
                //    note.Append(" Sinhala Name :" +assessmentToBeUpdated.NameSinhala + " Change to " +assessment.NameSinhala);
                //if (rentalPlaceToBeUpdated.NameTamil !=assessment.NameTamil)
                //    note.Append(" Tamil Name :" +assessmentToBeUpdated.NameTamil + " Change to " +assessment.NameTamil);
                //if (rentalPlaceToBeUpdated.Code !=assessment.Code)
                //    note.Append(" Code :" +assessmentToBeUpdated.Code + " Change to " +assessment.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID =assessmentToBeUpdated.ID,
                //    TransactionName = "Assessment",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                assessmentToBeUpdated.PartnerId = assessment.PartnerId;
                assessmentToBeUpdated.StreetId = assessment.StreetId;
                //assessmentToBeUpdated.WardId = assessment.WardId;
                assessmentToBeUpdated.PropertyTypeId = assessment.PropertyTypeId;
                assessmentToBeUpdated.DescriptionId = assessment.DescriptionId;
                assessmentToBeUpdated.OrderNo = assessment.OrderNo;
                assessmentToBeUpdated.AssessmentNo = assessment.AssessmentNo;
                assessmentToBeUpdated.AssessmentStatus = assessment.AssessmentStatus;
                assessmentToBeUpdated.Syn = assessment.Syn;
                assessmentToBeUpdated.Comment = assessment.Comment;
                assessmentToBeUpdated.Obsolete = assessment.Obsolete;
                assessmentToBeUpdated.OfficeId = assessment.OfficeId;
                assessmentToBeUpdated.SabhaId = assessment.SabhaId;
                assessmentToBeUpdated.IsWarrant = assessment.IsWarrant;
                assessmentToBeUpdated.UpdatedBy = assessment.UpdatedBy;


                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }

        public async Task Delete(Assessment obj)
        {
            _unitOfWork.Assessments.Remove(obj);
            await _unitOfWork.CommitAsync();
        }

        public async Task<(bool, string?)> UpdateAssessmentOrder(List<SaveAssessmentOrderNo> newOrderList, HTokenClaim token)
        {
            try
            {

                foreach(var order in newOrderList)
                {
                    var assessment = await _unitOfWork.Assessments.GetByIdAsync(order.Id);
                    if (assessment != null)
                    {
                        assessment.OrderNo = order.OrderNo;
                    }
                }

                await _unitOfWork.CommitAsync();

               return (true, "Updated Successfully");


            }
            catch(Exception ex)
            {
                return (false, ex.Message);
            }   
        }
        public async Task<(bool, string?)> UpdateAssessmentOrderByPattern(int sabhaid)
        {
            try
            {

              var  streets = await _unitOfWork.Streets.GetAllForSabha(sabhaid);


                foreach (var s in streets)
                {

                    var assessment = await _unitOfWork.Assessments.GetAssessmentOrderByPattern(sabhaid, s.Id!.Value);
                    //var assessment = await _unitOfWork.Assessments.GetAssessmentOrderByPattern(139, 2296);

                    foreach (var (item, index) in assessment.Select((item, index) => (item, index)))
                    {
                        item.OrderNo = index;
                    }


                }
                await _unitOfWork.CommitAsync();

                return (true, "Updated Successfully");


            }
            catch(Exception ex)
            {
                return (false, ex.Message);
            }   
        }

        public async Task<bool> InitBulkCreate(int sabhaId, HTokenClaim token)
        {

           

                _logger.LogInformation("Bulk Uploaded and Init Process Started for Sabha Id " + sabhaId + ". {EventType}", "Assessment");

            var notinitList = new List<Assessment>();

            List<Q1> Q1s = new List<Q1>();
            List<Q2> Q2s = new List<Q2>();
            List<Q3> Q3s = new List<Q3>();
            List<Q4> Q4s = new List<Q4>();
            List<AssessmentBalancesHistory> balance_histories = new List<AssessmentBalancesHistory>();
            List<AssessmentTransaction> transactions = new List<AssessmentTransaction>();


            try
            {
                var asmts = await _unitOfWork.Assessments.GetInitProcessForSabha(sabhaId);

                var firstAsmt = await _unitOfWork.Assessments.GetForFirstForInit(sabhaId);

                int? currentQuater = 1;
                if (firstAsmt.AssessmentBalance.CurrentQuarter != null)
                {
                    currentQuater = firstAsmt.AssessmentBalance.CurrentQuarter;
                }

                var today = DateOnly.FromDateTime(DateTime.Now);
                var todayTime = DateTime.Now;




                foreach (var asmt in asmts)
                {
                    if (asmt.Allocation != null && asmt.AssessmentPropertyType != null && asmt.AssessmentBalance != null)
                    {
                        //decimal precision handling
                        var annualAmmount = (asmt.Allocation.AllocationAmount * (asmt.AssessmentPropertyType.QuarterRate / 100));
                        var qAmount = annualAmmount / 4;

                        var roundedValue = Math.Round((decimal)qAmount, 2, MidpointRounding.AwayFromZero);

                        var remainder = annualAmmount - (roundedValue * 4);

                        asmt.AssessmentBalance.AnnualAmount = annualAmmount;
                        asmt.AssessmentBalance.CurrentQuarter = currentQuater;

                        /*
                         * var bal_histoty = new AssessmentBalancesHistory
                        {

                            Id = null,
                            AssessmentId = asmt.Id,
                            //Year
                            Year = asmt.AssessmentBalance.Year - 1,
                            StartDate = today,
                            EndData = today,

                            ExcessPayment = asmt.AssessmentBalance.ExcessPayment,

                            TYWarrant = asmt.AssessmentBalance.LYWarrant,
                            LYArrears = asmt.AssessmentBalance.LYArrears,

                            LYWarrant = asmt.AssessmentBalance.LYWarrant,
                            TYArrears = asmt.AssessmentBalance.TYArrears,
                            ByExcessDeduction = 0,

                            Paid = asmt.AssessmentBalance.Paid,
                            OverPayment = asmt.AssessmentBalance.OverPayment,
                            DiscountRate = 0,
                            Discount = asmt.AssessmentBalance.Discount,
                            IsCompleted = true,
                            Status = 0,
                            UpdatedAt = todayTime,
                            CreatedBy = -1,
                            UpdatedBy = -1,
                        };

                        //balance_histories.Add(bal_histoty);
                        */

                        var Q1 = new Q1
                        {
                            Id = null,
                            Amount = roundedValue,
                            ByExcessDeduction = 0,
                            Paid = 0,
                            Discount = 0,
                            Warrant = 0,
                            StartDate = todayTime,
                            EndDate = null,
                            BalanceId = asmt.AssessmentBalance.Id,
                            IsCompleted = false,
                            IsOver = false,
                        };

                        var Q2 = new Q2
                        {
                            Id = null,
                            Amount = roundedValue,
                            ByExcessDeduction = 0,
                            Paid = 0,
                            Discount = 0,
                            Warrant = 0,
                            StartDate = null,
                            EndDate = null,
                            BalanceId = asmt.AssessmentBalance.Id,
                            IsCompleted = false,
                            IsOver = false,
                        };

                        var Q3 = new Q3
                        {
                            Id = null,
                            Amount = roundedValue,
                            ByExcessDeduction = 0,
                            Paid = 0,
                            Discount = 0,
                            Warrant = 0,
                            StartDate = null,
                            EndDate = null,
                            BalanceId = asmt.AssessmentBalance.Id,
                            IsCompleted = false,
                            IsOver = false,
                        };

                        var Q4 = new Q4
                        {
                            Id = null,
                            Amount = roundedValue + remainder,
                            ByExcessDeduction = 0,
                            Paid = 0,
                            Discount = 0,
                            Warrant = 0,
                            StartDate = null,
                            EndDate = null,
                            BalanceId = asmt.AssessmentBalance.Id,
                            IsCompleted = false,
                            IsOver = false,
                        };


                        if (1 < asmt.AssessmentBalance.CurrentQuarter)
                        {
                            asmt.AssessmentBalance.LYArrears = 0;
                            asmt.AssessmentBalance.LYWarrant = 0;
                            asmt.AssessmentBalance.ExcessPayment = 0;

                            Q1.IsOver = true;
                            Q1.WarrantMethod = 0;
                            Q1.EndDate = todayTime;
                            //asmt.AssessmentBalance.TYArrears += Q1.Amount - Q1.ByExcessDeduction - Q1.Paid - Q1.Discount;
                        }

                        if (2 < asmt.AssessmentBalance.CurrentQuarter)
                        {
                            Q2.IsOver = true;
                            Q2.WarrantMethod = 0;
                            Q1.EndDate = todayTime;
                            //asmt.AssessmentBalance.TYArrears += Q2.Amount - Q2.ByExcessDeduction - Q2.Paid - Q2.Discount;
                        }

                        if (3 < asmt.AssessmentBalance.CurrentQuarter)
                        {
                            Q3.IsOver = true;
                            Q3.WarrantMethod = 0;
                            Q1.EndDate = todayTime;
                            //asmt.AssessmentBalance.TYArrears += Q3.Amount - Q3.ByExcessDeduction - Q3.Paid - Q3.Discount;
                        }

                        if (4 < asmt.AssessmentBalance.CurrentQuarter)
                        {
                            Q4.IsOver = true;
                            Q4.WarrantMethod = 0;
                            //asmt.AssessmentBalance.TYArrears += Q4.Amount - Q4.ByExcessDeduction - Q4.Paid - Q4.Discount;
                        }

                        if (asmt.AssessmentBalance.CurrentQuarter == 1) {
                            var oldReport = new AssessmentQuarterReport
                            {
                                DateTime = todayTime,
                                Year = asmt.AssessmentBalance.Year - 1,
                                QuarterNo = 4,
                                QAmount = 0,
                                M1Paid = 0,
                                M2Paid = 0,
                                M3Paid = asmt.AssessmentBalance.ExcessPayment,
                                QWarrant = 0,
                                QDiscount = 0,

                                LYArrears = asmt.AssessmentBalance.LYArrears,
                                LYWarrant = asmt.AssessmentBalance.LYWarrant,
                                TYArrears = asmt.AssessmentBalance.TYArrears,
                                TYWarrant = asmt.AssessmentBalance.TYWarrant,
                                AssessmentId = asmt.Id,
                                CreatedBy = token.userId,
                                CreatedAt = DateTime.Now,
                                UseTransactionsType = AssessmentTransactionsType.Init,

                            };
                            oldReport.RunningBalance =
                                                +oldReport.LYArrears
                                                + oldReport.LYWarrant
                                                + oldReport.TYArrears
                                                + oldReport.TYWarrant
                                                + oldReport.QAmount
                                                + oldReport.QWarrant

                                                - oldReport.QDiscount
                                                - oldReport.M1Paid
                                                - oldReport.M2Paid
                                                - oldReport.M3Paid;



                            await _unitOfWork.AssessmentQuarterReports.AddAsync(oldReport);
                        }
                        else if (asmt.AssessmentBalance.CurrentQuarter == 2)
                        {
                            var oldReport = new AssessmentQuarterReport
                            {
                                DateTime = todayTime,
                                Year = asmt.AssessmentBalance.Year,
                                QuarterNo = 1,
                                QAmount = 0,
                                M1Paid = 0,
                                M2Paid = 0,
                                M3Paid = asmt.AssessmentBalance.ExcessPayment,
                                QWarrant = 0,
                                QDiscount = 0,

                                LYArrears = asmt.AssessmentBalance.LYArrears,
                                LYWarrant = asmt.AssessmentBalance.LYWarrant,
                                TYArrears = asmt.AssessmentBalance.TYArrears,
                                TYWarrant = asmt.AssessmentBalance.TYWarrant,
                                AssessmentId = asmt.Id,
                                CreatedBy = token.userId,
                                CreatedAt = DateTime.Now,
                                UseTransactionsType = AssessmentTransactionsType.Init,

                            };
                            oldReport.RunningBalance =
                                                +oldReport.LYArrears
                                                + oldReport.LYWarrant
                                                + oldReport.TYArrears
                                                + oldReport.TYWarrant
                                                + oldReport.QAmount
                                                + oldReport.QWarrant

                                                - oldReport.QDiscount
                                                - oldReport.M1Paid
                                                - oldReport.M2Paid
                                                - oldReport.M3Paid;



                            await _unitOfWork.AssessmentQuarterReports.AddAsync(oldReport);
                        }
                        else if (asmt.AssessmentBalance.CurrentQuarter == 3)
                        {
                            var oldReport = new AssessmentQuarterReport
                            {
                                DateTime = todayTime,
                                Year = asmt.AssessmentBalance.Year,
                                QuarterNo = 2,
                                QAmount = 0,
                                M1Paid = 0,
                                M2Paid = 0,
                                M3Paid = asmt.AssessmentBalance.ExcessPayment,
                                QWarrant = 0,
                                QDiscount = 0,

                                LYArrears = asmt.AssessmentBalance.LYArrears,
                                LYWarrant = asmt.AssessmentBalance.LYWarrant,
                                TYArrears = asmt.AssessmentBalance.TYArrears,
                                TYWarrant = asmt.AssessmentBalance.TYWarrant,
                                AssessmentId = asmt.Id,
                                CreatedBy = token.userId,
                                CreatedAt = DateTime.Now,
                                UseTransactionsType = AssessmentTransactionsType.Init,

                            };
                            oldReport.RunningBalance =
                                                +oldReport.LYArrears
                                                + oldReport.LYWarrant
                                                + oldReport.TYArrears
                                                + oldReport.TYWarrant
                                                + oldReport.QAmount
                                                + oldReport.QWarrant

                                                - oldReport.QDiscount
                                                - oldReport.M1Paid
                                                - oldReport.M2Paid
                                                - oldReport.M3Paid;



                            await _unitOfWork.AssessmentQuarterReports.AddAsync(oldReport);
                        }
                        else if (asmt.AssessmentBalance.CurrentQuarter == 4)
                        {
                            var oldReport = new AssessmentQuarterReport
                            {
                                DateTime = todayTime,
                                Year = asmt.AssessmentBalance.Year,
                                QuarterNo = 3,
                                QAmount = 0,
                                M1Paid = 0,
                                M2Paid = 0,
                                M3Paid = asmt.AssessmentBalance.ExcessPayment,
                                QWarrant = 0,
                                QDiscount = 0,

                                LYArrears = asmt.AssessmentBalance.LYArrears,
                                LYWarrant = asmt.AssessmentBalance.LYWarrant,
                                TYArrears = asmt.AssessmentBalance.TYArrears,
                                TYWarrant = asmt.AssessmentBalance.TYWarrant,
                                AssessmentId = asmt.Id,
                                CreatedBy =token.userId,
                                CreatedAt = DateTime.Now,
                                UseTransactionsType = AssessmentTransactionsType.Init,

                            };
                            oldReport.RunningBalance =
                                                +oldReport.LYArrears
                                                + oldReport.LYWarrant
                                                + oldReport.TYArrears
                                                + oldReport.TYWarrant
                                                + oldReport.QAmount
                                                + oldReport.QWarrant

                                                - oldReport.QDiscount
                                                - oldReport.M1Paid
                                                - oldReport.M2Paid
                                                - oldReport.M3Paid;



                            await _unitOfWork.AssessmentQuarterReports.AddAsync(oldReport);
                        }


                        Q1s.Add(Q1);
                        Q2s.Add(Q2);
                        Q3s.Add(Q3);
                        Q4s.Add(Q4);

                        transactions.Add(createTransaction(asmt.AssessmentBalance, Q1, Q2, Q3, Q4, AssessmentTransactionsType.Init));


                    }



                }


                await _unitOfWork.Q1s.AddRangeAsync(Q1s);
                await _unitOfWork.Q2s.AddRangeAsync(Q2s);
                await _unitOfWork.Q3s.AddRangeAsync(Q3s);
                await _unitOfWork.Q4s.AddRangeAsync(Q4s);

                await _unitOfWork.AssessmentTransactions.AddRangeAsync(transactions);

                //await _unitOfWork.AssessmentsBalancesHistories.AddRangeAsync(balance_histories);


                await _unitOfWork.CommitAsync();

                if (token.IsFinalAccountsEnabled == 1)
                {
                    if (await this.UpdateLedgerAccounts(asmts.Select(a => a.Id).ToList(), token))
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "An error occurred during data init processing.{EventType}", "Assessment");

                    return false;
            }


            _logger.LogInformation("Init Process Successfully Completed. {EventType}", "Assessment");


            return true;
        


        }

        public async Task<bool> InitNextYearBalance(int?streetId,int? propertyId,int? assessmentId, HTokenClaim token)
        {



            _logger.LogInformation("Bulk Uploaded and Init Process Started for Sabha Id " + token.sabhaId + ". {EventType}", "Assessment");


            try
            {
                var asmts = await _unitOfWork.Assessments.GetInitNextYearProcessForSabha(streetId,propertyId,assessmentId,token);

                var firstAsmt = await _unitOfWork.Assessments.GetForFirstForInit(token.sabhaId);

                if (firstAsmt.AssessmentBalance == null) { 
                
                    throw new Exception("First Assessment Balance is not found");
                }

                //int? currentQuater = 1;
                //if (firstAsmt.AssessmentBalance!.CurrentQuarter != null)
                //{
                //    currentQuater = firstAsmt.AssessmentBalance.CurrentQuarter;
                //}

                var today = DateOnly.FromDateTime(DateTime.Now);
                var todayTime = DateTime.Now;


                foreach (var asmt in asmts)
                {
                    if (asmt.Allocation != null && asmt.AssessmentPropertyType != null)
                    {
                        ////decimal precision handling
                        //var annualAmmount = (asmt.Allocation.AllocationAmount * (asmt.AssessmentPropertyType.QuarterRate / 100));
                        //var qAmount = annualAmmount / 4;

                        //var roundedValue = Math.Round((decimal)qAmount, 2, MidpointRounding.AwayFromZero);

                        //var remainder = annualAmmount - (roundedValue * 4);

                        if (asmt.AssessmentBalance != null)
                        {


                            asmt.AssessmentBalance.AnnualAmount = 0;
                            asmt.AssessmentBalance.CurrentQuarter = 0;
                            asmt.AssessmentBalance.Year = firstAsmt.AssessmentBalance!.Year;
                        }
                        else
                        {
                            asmt.AssessmentBalance = new AssessmentBalance
                            {
                                Id = null,
                                AssessmentId = asmt.Id!.Value,
                                AnnualAmount = 0,
                                CurrentQuarter = 0,
                                Year= firstAsmt.AssessmentBalance!.Year,
                                TYArrears = 0,
                                TYWarrant = 0,
                                LYArrears = 0,
                                LYWarrant = 0,
                                ExcessPayment = 0,
                                Paid = 0,
                                OverPayment = 0,
                                ByExcessDeduction=0,
                                Discount = 0,
                                DiscountRate=0,
                                IsCompleted = false,
                                Status = 1,
                                CreatedBy = token.userId,
                                CreatedAt = todayTime,


                            };


                          await   _unitOfWork.StepAsync();


                        }

                        if (asmt.Allocation != null && asmt.AssessmentBalance != null)
                        {

                            //var item = assessmentRenewals.Where(x => x.Id == asmt.Id).FirstOrDefault();




                            if (!asmt.Allocation!.NextYearAllocationAmount.HasValue)
                            {
                                asmt.Allocation!.NextYearAllocationAmount = asmt.Allocation!.AllocationAmount;

                            }

                            if (!asmt.NextYearPropertyTypeId.HasValue)
                            {

                                asmt.NextYearPropertyTypeId = asmt.PropertyTypeId;

                            }

                            if (!asmt.NextYearDescriptionId.HasValue)
                            {

                                asmt.NextYearDescriptionId = asmt.DescriptionId;

                            }

                            AssessmentPropertyType assessmentPropertyType = await _unitOfWork.AssessmentPropertyTypes.GetByIdAsync(asmt.NextYearPropertyTypeId!.Value);


                            //decimal precision handling
                            var annualAmmount = (asmt.Allocation.NextYearAllocationAmount * (assessmentPropertyType.NextYearQuarterRate / 100));
                            var qAmount = annualAmmount / 4;

                            var roundedValue = Math.Round((decimal)qAmount, 2, MidpointRounding.AwayFromZero);

                            var remainder = annualAmmount - (roundedValue * 4);


                            if (asmt.AssessmentBalance.NQ1 != null)
                            {

                                asmt.AssessmentBalance.NQ1.Amount = roundedValue;
                            }
                            else
                            {

                                var nQ1 = new NQ1
                                {
                                    Id = null,
                                    Amount = roundedValue,
                                    //ByExcessDeduction = 0,
                                    //Paid = 0,
                                    //Discount = 0,
                                    //Warrant = 0,
                                    //StartDate = todayTime,
                                    //EndDate = null,
                                    BalanceId = asmt.AssessmentBalance.Id,
                                    //IsCompleted = false,
                                    //IsOver = false,
                                };

                                await _unitOfWork.NQ1s.AddAsync(nQ1);

                            }
                            if (asmt.AssessmentBalance.NQ2 != null)
                            {
                                asmt.AssessmentBalance.NQ2.Amount = roundedValue;
                            }
                            else
                            {
                                var nQ2 = new NQ2
                                {
                                    Id = null,
                                    Amount = roundedValue,
                                    //ByExcessDeduction = 0,
                                    //Paid = 0,
                                    //Discount = 0,
                                    //Warrant = 0,
                                    //StartDate = null,
                                    //EndDate = null,
                                    BalanceId = asmt.AssessmentBalance.Id,
                                    //IsCompleted = false,
                                    //IsOver = false,
                                };
                                await _unitOfWork.NQ2s.AddAsync(nQ2);

                            }

                            if (asmt.AssessmentBalance.NQ3 != null)
                            {
                                asmt.AssessmentBalance.NQ3.Amount = roundedValue;
                            }
                            else
                            {
                                var nQ3 = new NQ3
                                {
                                    Id = null,
                                    Amount = roundedValue,
                                    //ByExcessDeduction = 0,
                                    //Paid = 0,
                                    //Discount = 0,
                                    //Warrant = 0,
                                    //StartDate = null,
                                    //EndDate = null,
                                    BalanceId = asmt.AssessmentBalance.Id,
                                    //IsCompleted = false,
                                    //IsOver = false,
                                };

                                await _unitOfWork.NQ3s.AddAsync(nQ3);
                            }

                            if (asmt.AssessmentBalance.NQ4 != null)
                            {
                                asmt.AssessmentBalance.NQ4.Amount = roundedValue + remainder;
                            }
                            else
                            {


                                var nQ4 = new NQ4
                                {
                                    Id = null,
                                    Amount = roundedValue + remainder,
                                    //ByExcessDeduction = 0,
                                    //Paid = 0,
                                    //Discount = 0,
                                    //Warrant = 0,
                                    //StartDate = null,
                                    //EndDate = null,
                                    BalanceId = asmt.AssessmentBalance.Id,
                                    //IsCompleted = false,
                                    //IsOver = false,
                                };
                                await _unitOfWork.NQ4s.AddAsync(nQ4);
                            }


                        }

                    }
                }

                //await _unitOfWork.StepAsync();
                return true;
             
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "An error occurred during data init processing.{EventType}", "Assessment");

                return false;
            }


        }
        private AssessmentTransaction createTransaction(AssessmentBalance assessmentBalance, Q1 Q1, Q2 Q2, Q3 Q3, Q4 Q4, AssessmentTransactionsType trp)
        {
            var q1 = (!Q1!.IsCompleted && !Q1.IsOver) ? Q1!.Amount - Q1.ByExcessDeduction - Q1.Paid - Q1.Discount : 0;
            var q2 = (!Q2!.IsCompleted && !Q2.IsOver) ? Q2!.Amount - Q2.ByExcessDeduction - Q2.Paid - Q2.Discount : 0;
            var q3 = (!Q3!.IsCompleted && !Q3.IsOver) ? Q3!.Amount - Q3.ByExcessDeduction - Q3.Paid - Q3.Discount : 0;
            var q4 = (!Q4!.IsCompleted && !Q4.IsOver) ? Q4!.Amount - Q4.ByExcessDeduction - Q4.Paid - Q4.Discount : 0;
            var transaction = new AssessmentTransaction
            {
                AssessmentId = assessmentBalance.AssessmentId,
                //DateTime = DateTime.Now,
                Type = trp,
                LYArrears = assessmentBalance.LYArrears,
                LYWarrant = assessmentBalance.LYWarrant,
                TYArrears = assessmentBalance.TYArrears,
                TYWarrant = assessmentBalance.TYWarrant,
                RunningOverPay = assessmentBalance.ExcessPayment,
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

            return transaction;

            //await _unitOfWork.AssessmentTransactions.AddAsync(transaction);
        }

        public async Task<IEnumerable<Assessment>> PendingCustomizationRequests(int sabhaid)
        {
            return await _unitOfWork.Assessments.PendingCustomizationRequests(sabhaid);
        }

        public async Task<IEnumerable<Assessment>> PendingUpdateDescriptionForSabha(int sabhaid)
        {
            return await _unitOfWork.Assessments.PendingUpdateDescriptionForSabha(sabhaid);
        }

        public async Task<IEnumerable<Assessment>> PendingUpdatePropertyTypeForSabha(int sabhaid)
        {
            return await _unitOfWork.Assessments.PendingUpdatePropertyTypeForSabha(sabhaid);
        }



        public async Task<bool> updatePartner(int sabhaId)
        {
            try
            {

                var asmts = await _unitOfWork.Assessments.GetPartnerUpdateForSabha(sabhaId);

                foreach (var asmt in asmts)
                {
                    var nic = asmt.AssessmentTempPartner!.NICNumber;

                    if (System.Text.RegularExpressions.Regex.IsMatch(nic, @"^\d{12}$|^\d{9}[Vv]$"))
                    {

                        var partner = await _unitOfWork.Partners.GetByNICAsync(nic);

                        if (partner != null)
                        {
                            asmt.PartnerId = partner.Id;
                            asmt.IsPartnerUpdated = true;

                        }
                        else
                        {
                            var newPartner = new Partner
                            {
                                Name = asmt.AssessmentTempPartner!.Name,
                                NicNumber = nic,
                                MobileNumber = asmt.AssessmentTempPartner!.MobileNumber,
                                Street1 = asmt.AssessmentTempPartner!.Street1,

                                IsEditable = 0,

                                CreatedBy = (int)asmt.AssessmentTempPartner!.CreatedBy,
                                SabhaId = sabhaId,
                            };

                            
                            await _unitOfWork.Partners.AddAsync(newPartner);
                            await _unitOfWork.CommitAsync();

                            asmt.PartnerId = newPartner.Id;
                            asmt.IsPartnerUpdated = true;

                        }

                    }

                }
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<(int, bool)> SaveUpdateAssessmentPartner(Partner newpartner, int assessmentId)
        {
            try
            {
                var asmt = await _unitOfWork.Assessments.GetByIdAsync(assessmentId);
                var partner = await _unitOfWork.Partners.GetByIdAsync(newpartner.Id);

                if (partner != null)
                {


                    partner.MobileNumber ??= newpartner.MobileNumber;
                    partner.GnDivisionId = partner.GnDivisionId == 0 ? newpartner.GnDivisionId : partner.GnDivisionId;
                    partner.Street1 = partner.Street1 == null || partner.Street1 == "-" ? newpartner.Street1 : partner.Street1;
                    partner.City = partner.City == null || partner.City == "-" ? newpartner.City : partner.City;
                    partner.Zip = partner.Zip == null || partner.Zip == "-" ? newpartner.Zip : partner.Zip;
                    partner.Email = partner.Email == null || partner.Email == "-" ? newpartner.Email : partner.Email;


                    partner.Name = newpartner.Name;

                    partner.IsEditable = 0;
                    partner.UpdatedBy = newpartner.CreatedBy;



                    asmt.PartnerId = partner.Id;
                }
                else
                {
                    await _unitOfWork.Partners.AddAsync(newpartner);
                    await _unitOfWork.CommitAsync();
                    asmt.PartnerId = newpartner.Id;
                }


                asmt.IsPartnerUpdated = true;
                asmt.UpdatedBy = newpartner.UpdatedBy;
                asmt.UpdatedAt = DateTime.Now;

                
                /*assessment_log*/
                await _unitOfWork.AssessmentAuditLogs.AddAsync(new AssessmentAuditLog
                {
                    AssessmentId = asmt.Id,
                    Action = AssessmentAuditLogAction.Update,
                    Timestamp = DateTime.Now,
                    ActionBy = newpartner.CreatedBy,
                    EntityType = AssessmentRelatedEntityType.Partner
                });


                await _unitOfWork.CommitAsync();
                return (newpartner.Id, true);
            }
            catch (Exception ex)
            {
                return (-1, false);

            }
        }

        public async Task<(int totalCount, IEnumerable<Assessment> list)> GetAllAndExclude(List<int?> excludedIds, int? sabhaId, int? officeId, int? wardId, int? streetId, int? propertyType, int? propertyDescription, string assessmentNo, int? assessmentId, int? partnerId, string? nic, string? name, int pageNo, int pageSize)
        {
            try
            {
                return await _unitOfWork.Assessments.GetAllAndExclude(excludedIds, sabhaId, officeId, wardId, streetId, propertyType, propertyDescription, assessmentNo, assessmentId, partnerId, nic, name, pageNo, pageSize);
            }
            catch (Exception ex)
            {
                List<Assessment> list1 = new List<Assessment>();
                return (0, list1);
            }
        }

        public async Task<(int totalCount, IEnumerable<Assessment> list)> GetAllAndExcludeForWarrant(List<int?> excludedIds, int? sabhaId, int? officeId, int? wardId, int? streetId, int? propertyType, int? propertyDescription, string assessmentNo, int? assessmentId, int? partnerId, int? quarter, int pageNo, int pageSize)
        {
            try
            {
                if (1 <= quarter && quarter <= 4)
                {

                    return await _unitOfWork.Assessments.GetAllAndExcludeForWarrant(excludedIds, sabhaId, officeId, wardId, streetId, propertyType, propertyDescription, assessmentNo, assessmentId, partnerId, quarter, pageNo, pageSize);
                }
                else
                {
                    List<Assessment> list1 = new List<Assessment>();
                    return (0, list1);
                }
            }
            catch (Exception ex)
            {
                List<Assessment> list1 = new List<Assessment>();
                return (0, list1);
            }
        }


        public async Task<(int totalCount, IEnumerable<Assessment> list)> GetAllWarrantedList(List<int?> excludedIds, int? sabhaId, int? officeId, int? wardId, int? streetId, int? propertyType, int? propertyDescription, string assessmentNo, int? assessmentId, int? partnerId, int? quarter, int pageNo, int pageSize)
        {
            try
            {
                if (1 <= quarter && quarter <= 4)
                {

                    return await _unitOfWork.Assessments.GetAllWarrantedList(excludedIds, sabhaId, officeId, wardId, streetId, propertyType, propertyDescription, assessmentNo, assessmentId, partnerId, quarter, pageNo, pageSize);
                }
                else
                {
                    List<Assessment> list1 = new List<Assessment>();
                    return (0, list1);
                }
            }
            catch (Exception ex)
            {
                List<Assessment> list1 = new List<Assessment>();
                return (0, list1);
            }
        }

        public async Task<List<int?>> GetAllKFormIdsFor(Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, string query)
        {
            try
            {
                return await _unitOfWork.Assessments.GetAllKFormIdsFor(sabhaId, officeId, wardId, streetId, propertyType, query);
            }
            catch (Exception ex)
            {
                return new List<int?>();
            }
        }

        public async Task<List<string?>> GetAllAssessmentNoFor(Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, string query)
        {
            try
            {
                return await _unitOfWork.Assessments.GetAllAssessmentNoFor(sabhaId, officeId, wardId, streetId, propertyType, query);
            }
            catch (Exception ex)
            {
                return new List<string?>();
            }
        }

        public async Task<Assessment> getAssessmentForUpdate(string assessmentNo, Nullable<int> assessmentId ,HTokenClaim token)
        {
                return await _unitOfWork.Assessments.getAssessmentForUpdate(assessmentNo,assessmentId,token);
           
        }

        public async Task<Assessment> GetAssessmentForCustomize( int sabhaId, int assessmentId)
        {
            return await _unitOfWork.Assessments.GetAssessmentForCustomize( sabhaId,assessmentId);
        }



        public async Task<bool> CustomizeAssessmentRequest(int assessmentId,  decimal newAllocation, int newPropertyId, int newDescriptionId, string requestNote,HTokenClaim token)
        {
            try
            {
                var asmt = await _unitOfWork.Assessments.GetForJournal(assessmentId);

                var flag = false;
                if (asmt != null)
                {


                    if (newAllocation != -1 && !asmt.HasAssetsChangeRequest )
                    {

                        if (await _unitOfWork.NewAllocationRequests.HasEntity(asmt.Id!.Value))
                        {
                                throw new Exception("Allocation already requested");
                            
                        }

                        var newAllocationRequest = new NewAllocationRequest
                        {
                            Id = null,
                            AssessmentId = asmt.Id,
                            AllocationAmount = newAllocation,
                            AllocationDescription= requestNote,
                            CreatedBy = token.userId,
                            CreatedAt = DateTime.Now,
                            ActivationYear = asmt.AssessmentBalance!.Year,
                            ActivationQuarter = (int)asmt.AssessmentBalance!.CurrentQuarter!,
                        };


                        await _unitOfWork.NewAllocationRequests.AddAsync(newAllocationRequest);
                        asmt.AllocationChangeRequest = true;
                        flag = true;
                    }
                   

                    if (newPropertyId != -1 && !asmt.PropertyTypeChangeRequest)
                    {

                        if (!await _unitOfWork.AssessmentPropertyTypeLogs.HasAlreadyLog(asmt.Id!.Value)) { 
                        
                            var oldPropertyRequest = new AssessmentPropertyTypeLog
                            {
                            Id = null,
                            AssessmentId = asmt.Id,
                            PropertyTypeId = (int)asmt.PropertyTypeId!,
                            CreatedAt = asmt.CreatedAt,
                            CreatedBy = asmt.CreatedBy!,
                            Comment = asmt.Allocation!.AllocationDescription!,
                            ActivatedDate = asmt.CreatedAt,
                            ActionBy = asmt.CreatedBy,
                            ActivationYear = 2024,
                            ActivationQuarter = 1,
                            ActionNote="Initial Property Type",
                            Status = 1,
                            UpdatedBy = asmt.UpdatedBy,
                            UpdatedAt = asmt.UpdatedAt,

                                };

                         await   _unitOfWork.AssessmentPropertyTypeLogs.AddAsync(oldPropertyRequest);
                        
                        }

                        var newPropertyRequest = new AssessmentPropertyTypeLog
                        {
                            Id = null,
                            AssessmentId = asmt.Id,

                            PropertyTypeId = newPropertyId,
                            CreatedBy = token.userId,
                            CreatedAt = DateTime.Now,
                            Comment = requestNote,
                            Status = 1,
                            ActivationYear = asmt.AssessmentBalance!.Year,
                            ActivationQuarter = (int)asmt.AssessmentBalance!.CurrentQuarter!,

                        };

                        asmt.PropertyTypeChangeRequest = true;

                       await  _unitOfWork.AssessmentPropertyTypeLogs.AddAsync(newPropertyRequest);

                        flag = true;
                    }

                    if (newDescriptionId != -1 &&  !asmt.DescriptionChangeRequest )
                    {
                        if (!await _unitOfWork.AssessmentDescriptionLogs.HasAlreadyLog(asmt.Id!.Value))
                        {
                            var oldDescriptionRequest = new AssessmentDescriptionLog
                            {
                                Id = null,
                                AssessmentId = asmt.Id,
                                DescriptionId = (int)asmt.DescriptionId!,
                                Comment = "Initial Description",
                                ActionBy = asmt.CreatedBy,
                                ActivatedDate = asmt.CreatedAt,
                                ActionNote = "System Initial Description",
                                CreatedAt = asmt.CreatedAt,
                                CreatedBy = asmt.CreatedBy!,
                                ActivationYear = 2024,
                                ActivationQuarter = 1,
                                Status = 1,
                                UpdatedBy = asmt.UpdatedBy,
                                UpdatedAt = asmt.UpdatedAt,

                            };

                        await   _unitOfWork.AssessmentDescriptionLogs.AddAsync(oldDescriptionRequest);

                        }

                        var newDescriptionRequest = new AssessmentDescriptionLog
                        {
                            Id = null,
                            AssessmentId = asmt.Id,
                            DescriptionId = newDescriptionId,
                            CreatedBy = token.userId,
                            CreatedAt = DateTime.Now,
                            Comment = requestNote,
                            Status = 1,

                            ActivationYear = asmt.AssessmentBalance!.Year,
                            ActivationQuarter = (int)asmt.AssessmentBalance!.CurrentQuarter!,


                        };

                        asmt.DescriptionChangeRequest = true;   
                      await  _unitOfWork.AssessmentDescriptionLogs.AddAsync(newDescriptionRequest);
                        flag = true;
                    }

                   
                }
                else
                {
                    throw new Exception("Assessment not found");    
                }
                if (flag)
                {
                        await _unitOfWork.CommitAsync(); 
                }

                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ApproveDisapproveCustomization(int assessmentId,string actionNote,int state,HTokenClaim token)
        {
            try
            {
                var hasactiveSession = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(token.officeId);
                bool commitFlag = false;
                bool hasBlanceUpdate = false;
                if (hasactiveSession != null)
                {



                    var asmt = await _unitOfWork.Assessments.GetForCustomize(assessmentId);

                    //decimal allocationAmount = (decimal)asmt.Allocation.AllocationAmount;
                    decimal quarterRate = (decimal)asmt.AssessmentPropertyType.QuarterRate;

                    int? month = await _unitOfWork.Sessions.GetCurrentSessionMonthForProcess(hasactiveSession.Id);

                    var rates = await _unitOfWork.AssessmentRates.GetByIdAsync(1);


                    if (asmt != null && asmt.Allocation != null && month.HasValue && rates != null)
                    {

                        if (asmt.DescriptionChangeRequest)
                        {
                            if (asmt.AssessmentDescriptionLogs != null && asmt.AssessmentDescriptionLogs.First() != null && asmt.AssessmentDescriptionLogs.First().ActionBy == null)
                            {
                                if (state == 1)
                                {
                                    asmt.DescriptionId = asmt.AssessmentDescriptionLogs.First().DescriptionId;

                                    asmt.AssessmentDescriptionLogs.First().Status = 1;
                                    asmt.AssessmentDescriptionLogs.First().ActionBy = token.userId;
                                    asmt.AssessmentDescriptionLogs.First().ActionNote = actionNote;
                                    asmt.AssessmentDescriptionLogs.First().ActivatedDate = DateTime.Now;
                                    asmt.AssessmentDescriptionLogs.First().UpdatedAt = DateTime.Now;
                                    asmt.AssessmentDescriptionLogs.First().UpdatedBy = token.userId;

                                   
                                }
                                else if (state == 0)
                                {
                                    asmt.AssessmentDescriptionLogs.First().Status = -1;
                                    asmt.AssessmentDescriptionLogs.First().ActionBy = token.userId;
                                    asmt.AssessmentDescriptionLogs.First().ActionNote = actionNote;
                                    asmt.AssessmentDescriptionLogs.First().UpdatedAt = DateTime.Now;
                                    asmt.AssessmentDescriptionLogs.First().UpdatedBy = token.userId;

                                }

                                asmt.DescriptionChangeRequest = false;
                               
                                commitFlag = true;

                            }
                            else
                            {
                                throw new Exception("Unable to update description");
                            }
                        }


                        if (asmt.PropertyTypeChangeRequest)
                        {
                            if (asmt.AssessmentPropertyTypeLogs != null && asmt.AssessmentPropertyTypeLogs.First() != null)
                            {
                                if (state == 1)
                                {
                                    var propType = await _unitOfWork.AssessmentPropertyTypes.GetByIdAsync(asmt.AssessmentPropertyTypeLogs.First().PropertyTypeId);

                                    if (propType != null)
                                    {
                                        asmt.PropertyTypeId = asmt.AssessmentPropertyTypeLogs.First().PropertyTypeId;
                                        quarterRate = (decimal)propType.QuarterRate;
                                        asmt.AssessmentPropertyTypeLogs.First().ActionBy = token.userId;
                                        asmt.AssessmentPropertyTypeLogs.First().ActionNote = actionNote;
                                        asmt.AssessmentPropertyTypeLogs.First().ActivatedDate = DateTime.Now;
                                        asmt.AssessmentPropertyTypeLogs.First().Status = 1;

                                        asmt.AssessmentPropertyTypeLogs.First().ActivationQuarter = (int)asmt.AssessmentBalance!.CurrentQuarter!;
                                        hasBlanceUpdate = true;
                                    }
                                    else
                                    {
                                        throw new Exception("Property Type not found");
                                    }



                                }else if (state ==0)
                                {

                                    asmt.PropertyTypeId = asmt.AssessmentPropertyTypeLogs.First().PropertyTypeId;
                                    asmt.AssessmentPropertyTypeLogs.First().ActionBy =token.userId;
                                    asmt.AssessmentPropertyTypeLogs.First().ActionNote = actionNote;
                                    asmt.AssessmentPropertyTypeLogs.First().ActivatedDate = DateTime.Now;
                                    asmt.AssessmentPropertyTypeLogs.First().Status = -1;

                                    asmt.AssessmentPropertyTypeLogs.First().ActivationQuarter = (int)asmt.AssessmentBalance!.CurrentQuarter!;

                                }
                                asmt.PropertyTypeChangeRequest = false;
                                commitFlag = true;
                               
                            }
                        }


                        if (asmt.AllocationChangeRequest)
                        {
                            if (asmt.NewAllocationRequest != null)
                            {

                                if (state == 1)
                                {


                                    var newAllocationLog = new AllocationLog
                                    {
                                        Id = null,
                                        Amount = asmt.Allocation.AllocationAmount,
                                        FromDate = asmt.Allocation.ChangedDate,
                                        ToDate = DateOnly.FromDateTime(DateTime.Now),
                                        Description = asmt.Allocation.AllocationDescription,
                                        AllocationId = asmt.Allocation.Id,

                                        CreatedAt = asmt.Allocation.CreatedAt,
                                        CreatedBy = asmt.Allocation.CreatedBy,
                                        Status = 1,
                                        UpdatedAt = DateTime.Now,
                                        UpdatedBy = token.userId,
                                    };

                                    await _unitOfWork.AllocationLogs.AddAsync(newAllocationLog);




                                    asmt.Allocation.AllocationAmount = asmt.NewAllocationRequest.AllocationAmount;
                                    asmt.Allocation.ChangedDate = DateOnly.FromDateTime(DateTime.Now);
                                    asmt.Allocation.AllocationDescription = asmt.NewAllocationRequest.AllocationDescription;
                                    asmt.Allocation.UpdatedBy = token.userId;
                                    asmt.Allocation.UpdatedAt = DateTime.Now;

                                     asmt.NewAllocationRequest.ActivationQuarter = (int)asmt.AssessmentBalance!.CurrentQuarter!;
                                    hasBlanceUpdate = true;


                                }
                                else if (state == 0)
                                {
                                    var newAllocationLog = new AllocationLog
                                    {
                                        Id = null,
                                        Amount = asmt.NewAllocationRequest.AllocationAmount,
                                        FromDate = DateOnly.FromDateTime(DateTime.Now),
                                        ToDate = DateOnly.FromDateTime(DateTime.Now),
                                        Description = asmt.NewAllocationRequest.AllocationDescription,
                                        AllocationId = asmt.Allocation.Id,

                                        CreatedAt = asmt.NewAllocationRequest.CreatedAt,
                                        CreatedBy = asmt.NewAllocationRequest.CreatedBy,
                                        Status = -1,
                                        UpdatedAt = DateTime.Now,
                                        UpdatedBy = token.userId,
                                    };

                                    await _unitOfWork.AllocationLogs.AddAsync(newAllocationLog);
                                }

                                var newAllocationRequest = await _unitOfWork.NewAllocationRequests.GetByIdAsync(asmt.NewAllocationRequest.Id);

                                 _unitOfWork.NewAllocationRequests.Remove(newAllocationRequest);

                                asmt.AllocationChangeRequest = false;
                                commitFlag = true;
                                

                            }
                            else
                            {
                                throw new Exception("Allocation Request not found");
                            }
                        }





                        if (hasBlanceUpdate)
                        {
                            if (!updateQuaters(ref asmt, quarterRate, rates, token.userId,(int)month))
                            {
                                throw new Exception("Quarter Update Failed");
                            }
                        }

                        if (commitFlag)
                        {
                            createTransaction(asmt.AssessmentBalance!, AssessmentTransactionsType.AllocationNatureChange);
                            await _unitOfWork.CommitAsync();

                        }

                    }
                    else
                    {
                        throw new Exception("Unable to fetch required data");
                    }


                }
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }


        }

        private bool updateQuaters(ref Assessment asmt,decimal quarterRate, AssessmentRates rates,int actionBy, int month)
        {
            try
            {
                bool isQ1Completed= asmt.AssessmentBalance!.Q1!.IsCompleted;
                bool isQ2Completed = asmt.AssessmentBalance!.Q2!.IsCompleted;
                bool isQ3Completed = asmt.AssessmentBalance!.Q3!.IsCompleted;
                bool isQ4Completed = asmt.AssessmentBalance!.Q4!.IsCompleted;

              

                var annualAmmount = (asmt.Allocation.AllocationAmount * (quarterRate / 100));
                var qAmount = annualAmmount / 4;

                var roundedValue = Math.Round((decimal)qAmount, 2, MidpointRounding.AwayFromZero);

                var remainder = annualAmmount - (roundedValue * 4);

                asmt.AssessmentBalance.AnnualAmount = annualAmmount;

                decimal? totalDeduction=0;
                decimal? totalPaid=0;


                if ( !asmt.AssessmentBalance!.Q4!.IsOver)
                {
                    if (asmt.AssessmentBalance!.Q4!.IsCompleted || asmt.AssessmentBalance!.Q4!.Amount ==0)
                    {
                        totalDeduction += asmt.AssessmentBalance!.Q4!.ByExcessDeduction;
                        totalPaid += asmt.AssessmentBalance!.Q4!.Paid;

                    }
                    asmt.AssessmentBalance!.Q4!.Amount = roundedValue + remainder;

                }

                if (!asmt.AssessmentBalance!.Q3!.IsOver)
                {
                    if (asmt.AssessmentBalance!.Q3!.IsCompleted || asmt.AssessmentBalance!.Q3!.Amount == 0 )
                    {
                        totalDeduction += asmt.AssessmentBalance!.Q3!.ByExcessDeduction;
                        totalPaid += asmt.AssessmentBalance!.Q3!.Paid;

                    }
                    asmt.AssessmentBalance!.Q3!.Amount = roundedValue;

                }

                if (!asmt.AssessmentBalance!.Q2!.IsOver)
                {
                    if (asmt.AssessmentBalance!.Q2!.IsCompleted || asmt.AssessmentBalance!.Q2!.Amount == 0)
                    {
                        totalDeduction += asmt.AssessmentBalance!.Q2!.ByExcessDeduction;
                        totalPaid += asmt.AssessmentBalance!.Q2!.Paid;

                    }
                    asmt.AssessmentBalance!.Q2!.Amount = roundedValue;

                }


                if (!asmt.AssessmentBalance!.Q1!.IsOver)
                {
                    if (asmt.AssessmentBalance!.Q1!.IsCompleted || asmt.AssessmentBalance!.Q1!.Amount == 0)
                    {
                        totalDeduction += asmt.AssessmentBalance!.Q1!.ByExcessDeduction;
                        totalPaid += asmt.AssessmentBalance!.Q1!.Paid;

                    }
                    asmt.AssessmentBalance!.Q1!.Amount = roundedValue + remainder;

                }

                if (totalDeduction > 0 || totalPaid > 0 || asmt.AssessmentBalance!.OverPayment>0 )
               {
                    if (totalPaid > 0)
                    {
                        totalPaid+= asmt.AssessmentBalance!.OverPayment;
                        asmt.AssessmentBalance.OverPayment = 0;
                    }

                    if(totalPaid == 0 )
                    {
                        asmt.AssessmentBalance.ExcessPayment = totalDeduction+ asmt.AssessmentBalance!.OverPayment;
                        asmt.AssessmentBalance.OverPayment = 0;
                    }
                    

                    if (!asmt.AssessmentBalance!.Q1!.IsOver && asmt.AssessmentBalance.Q1.IsCompleted)
                    {
                        asmt.AssessmentBalance!.Q1!.IsCompleted = false;
                        asmt.AssessmentBalance!.Q1!.ByExcessDeduction = 0;
                        asmt.AssessmentBalance!.Q1!.Paid = 0;
                        asmt.AssessmentBalance!.Q1!.Discount = 0;

                    }

                    if (!asmt.AssessmentBalance!.Q2!.IsOver && asmt.AssessmentBalance.Q2.IsCompleted)
                    {
                        asmt.AssessmentBalance!.Q2!.IsCompleted = false;
                        asmt.AssessmentBalance!.Q2!.ByExcessDeduction = 0;
                        asmt.AssessmentBalance!.Q2!.Paid = 0;
                        asmt.AssessmentBalance!.Q2!.Discount = 0;

                    }

                    if (!asmt.AssessmentBalance!.Q3!.IsOver && asmt.AssessmentBalance.Q3.IsCompleted)
                    {
                        asmt.AssessmentBalance!.Q3!.IsCompleted = false;
                        asmt.AssessmentBalance!.Q3!.ByExcessDeduction = 0;
                        asmt.AssessmentBalance!.Q3!.Paid = 0;
                        asmt.AssessmentBalance!.Q3!.Discount = 0;

                    }

                    if (!asmt.AssessmentBalance!.Q4!.IsOver && asmt.AssessmentBalance.Q4.IsCompleted)
                    {
                        asmt.AssessmentBalance!.Q4!.IsCompleted = false;
                        asmt.AssessmentBalance!.Q4!.ByExcessDeduction = 0;
                        asmt.AssessmentBalance!.Q4!.Paid = 0;
                        asmt.AssessmentBalance!.Q4!.Discount = 0;

                    }

                    /*
                     update Balance

                     */





                   

                    if (asmt.AssessmentBalance != null && asmt.AssessmentBalance.Q1 != null && asmt.AssessmentBalance.Q2 != null && asmt.AssessmentBalance.Q3 != null && asmt.AssessmentBalance.Q4 != null && month!=-1 && rates != null && asmt.AssessmentBalance.HasTransaction == false)
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






                        (var op, var payable, var deduction, var paying, var nextbal, var discount, var dctRate) = _assessmentBalanceService.CalculatePaymentBalance(asmt.AssessmentBalance, rates, (decimal)totalPaid, month, true);

                        //asmt.AssessmentBalance.ByExcessDeduction += deduction.Total;
                        //asmt.AssessmentBalance.Paid += mxOrder.TotalAmount;
                        asmt.AssessmentBalance.ExcessPayment = 0;
                        asmt.AssessmentBalance.DiscountRate = discount.Total > 0 ? dctRate : asmt.AssessmentBalance.DiscountRate;
                        asmt.AssessmentBalance.Discount += discount.Total;
                        asmt.AssessmentBalance.OverPayment += paying.OverPayment += deduction.OverPayment != 0 ? deduction.OverPayment : 0;

                        asmt.AssessmentBalance.LYWarrant = nextbal.LYWarrant;
                        asmt.AssessmentBalance.LYArrears = nextbal.LYArrears;

                        asmt.AssessmentBalance.TYWarrant = nextbal.TYWarrant;
                        asmt.AssessmentBalance.TYArrears = nextbal.TYArrears;




                        if (!asmt.AssessmentBalance.Q1.IsOver && !asmt.AssessmentBalance.Q1.IsCompleted && (paying.Q1 != 0 || (paying.Q1 == 0 && discount.Q1 != 0)))

                        {
                            asmt.AssessmentBalance.Q1.ByExcessDeduction += deduction.Q1;
                            asmt.AssessmentBalance.Q1.Paid += paying.Q1;
                            asmt.AssessmentBalance.Q1.Discount += discount.Q1;
                            asmt.AssessmentBalance.Q1.IsCompleted = asmt.AssessmentBalance.Q1.Amount - asmt.AssessmentBalance.Q1.ByExcessDeduction - asmt.AssessmentBalance.Q1.Paid - asmt.AssessmentBalance.Q1.Discount == 0 ? true : false;
                        }

                        if (!asmt.AssessmentBalance.Q2.IsOver && !asmt.AssessmentBalance.Q2.IsCompleted && (paying.Q2 != 0 || (paying.Q2 == 0 && discount.Q2 != 0)))
                        {
                            asmt.AssessmentBalance.Q2.ByExcessDeduction += deduction.Q2;
                            asmt.AssessmentBalance.Q2.Paid += paying.Q2;
                            asmt.AssessmentBalance.Q2.Discount += discount.Q2;
                            asmt.AssessmentBalance.Q2.IsCompleted = asmt.AssessmentBalance.Q2.Amount - asmt.AssessmentBalance.Q2.ByExcessDeduction - asmt.AssessmentBalance.Q2.Paid - asmt.AssessmentBalance.Q2.Discount == 0 ? true : false;
                        }

                        if (!asmt.AssessmentBalance.Q3.IsOver && !asmt.AssessmentBalance.Q3.IsCompleted && (paying.Q3 != 0 || (paying.Q3 == 0 && discount.Q3 != 0)))
                        {
                            asmt.AssessmentBalance.Q3.ByExcessDeduction += deduction.Q3;
                            asmt.AssessmentBalance.Q3.Paid += paying.Q3;
                            asmt.AssessmentBalance.Q3.Discount += discount.Q3;
                            asmt.AssessmentBalance.Q3.IsCompleted = asmt.AssessmentBalance.Q3.Amount - asmt.AssessmentBalance.Q3.ByExcessDeduction - asmt.AssessmentBalance.Q3.Paid - asmt.AssessmentBalance.Q3.Discount == 0 ? true : false;
                        }

                        if (!asmt.AssessmentBalance.Q4.IsOver && !asmt.AssessmentBalance.Q4.IsCompleted && (paying.Q4 != 0 || (paying.Q4 == 0 && discount.Q4 != 0)))
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
                        else
                        {
                            asmt.AssessmentBalance.IsCompleted = false;
                        }

                        asmt.AssessmentBalance.NumberOfPayments += 1;
                        asmt.AssessmentBalance.UpdatedBy = actionBy;
                        asmt.AssessmentBalance.UpdatedAt = DateTime.Now;
                        asmt.AssessmentBalance.HasTransaction = false;




                        //var q1 = (!assessmentBalance.Q1.IsCompleted && !assessmentBalance.Q1.IsOver) ? assessmentBalance.Q1.Amount - assessmentBalance.Q1.ByExcessDeduction - assessmentBalance.Q1.Paid - assessmentBalance.Q1.Discount : 0;
                        //var q2 = (!assessmentBalance.Q2.IsCompleted && !assessmentBalance.Q2.IsOver) ? assessmentBalance.Q2.Amount - assessmentBalance.Q2.ByExcessDeduction - assessmentBalance.Q2.Paid - assessmentBalance.Q2.Discount : 0;
                        //var q3 = (!assessmentBalance.Q3.IsCompleted && !assessmentBalance.Q3.IsOver) ? assessmentBalance.Q3.Amount - assessmentBalance.Q3.ByExcessDeduction - assessmentBalance.Q3.Paid - assessmentBalance.Q3.Discount : 0;
                        //var q4 = (!assessmentBalance.Q4.IsCompleted && !assessmentBalance.Q4.IsOver) ? assessmentBalance.Q4.Amount - assessmentBalance.Q4.ByExcessDeduction - assessmentBalance.Q4.Paid - assessmentBalance.Q4.Discount : 0;


                    }
                    else
                    {
                        throw new Exception("Unable to calculate new balance");
                    }
                }

                return true;
            }catch(Exception ex)
            {
                return false;
            }

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
                DiscountRate = assessmentBalance.DiscountRate,
            };

            await _unitOfWork.AssessmentTransactions.AddAsync(transaction);
        }

        private  async Task<bool> UpdateLedgerAccounts(List<int?> assessmentIds, HTokenClaim token)
        {
            try { 
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
                        vtbLog.Code = "ASM LYW O/B";
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
                        vtbLog.Code = "ASM LYW O/B";
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
                        vtbLog.Code = "ASM LYA O/B";
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
                        vtbLog.Code = "ASM LYA O/B";
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
                        vtbLog.Code = "ASM TYW O/B";
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
                        vtbLog.Code = "ASM TYW O/B";
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
                        vtbLog.Code = "ASM TYA O/B";
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
                        vtbLog.Code = "ASM TYA O/B";
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
                        vtbLog.Code = "ASM Taxing O/B";
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
                        vtbLog.Code = "ASM OverPay O/B";
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
                        vtbLog.Code = "ASM OverPay O/B";
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
                        vtbLog.Code = "ASM Billing O/B";
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
                        vtbLog.Code = "ASM Warranting O/B";
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
        }catch (Exception ex)
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


    }
}