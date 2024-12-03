using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Models.OnlinePayment;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.AssessmentTax;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.Mixin;
using CAT20.Core.Services.OnlinePayment;
using CAT20.Services.Mixin;
using CAT20.Services.WaterBilling;
using CAT20.WebApi.Resources.OnlinePayment;
using DocumentFormat.OpenXml.Office2010.Excel;
using Irony.Parsing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Task = DocumentFormat.OpenXml.Office2021.DocumentTasks.Task;


namespace CAT20.Services.OnlinePayment;

public class OnlinePaymentService : IOnlinePaymentService
{
    private readonly IOnlinePaymentUnitOfWork _unitOfWork;
    private readonly IAssessmentBalanceService _assessmentBalanceService;
    private readonly ILogger<MixinOrderService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IVoteAssignmentService _voteAssignmentService;
    private readonly WaterConnectionBalanceService _waterConnectionBalanceService;

    public OnlinePaymentService(IOnlinePaymentUnitOfWork unitOfWork, IAssessmentBalanceService assessmentBalanceService,IConfiguration configuration,
        ILogger<MixinOrderService> logger, IVoteAssignmentService voteAssignment)
    {
        _unitOfWork = unitOfWork;
        _assessmentBalanceService = assessmentBalanceService;
        _logger = logger;
        _configuration = configuration;
        _voteAssignmentService = voteAssignment;

    }

    // public OnlinePaymentService(OnlinePaymentService unitOfWork)
    // {
    //     
    // }

    public async Task<IEnumerable<Assessment>> GetAllForCustomerIdAndSabhaId(int customerid, int sabhaId)
    {
        var allForCustomerIdAndSabhaId =
            await _unitOfWork.Assessments.GetAllForCustomerIdAndSabhaId(customerid, sabhaId);

        return allForCustomerIdAndSabhaId;
    }

    public async Task<IEnumerable<Assessment>> GetAllForIds(List<int> assessmentIds)
    {
        var allForIds =
            await _unitOfWork.Assessments.GetAllForIds(assessmentIds);

        return allForIds;
    }

    public async Task<IEnumerable<Assessment>> GetAllForIdsAndSabha(List<int> assessmentIds, int sabhaid)
    {
        var allForIds =
            await _unitOfWork.Assessments.GetAllForIdsAndSabha(assessmentIds,sabhaid);

        return allForIds;
    }
    
    public async Task<IEnumerable<WaterConnection>> getWaterBill(int partnerId, int sabhaId)
    {
        var WaterConnectionList = await _unitOfWork.Balances.getWaterBill(partnerId, sabhaId);
        return WaterConnectionList;
        //return new List<WaterConnection>();
    }

    public async Task<IEnumerable<Shop>> getShopRental(int partnerId, int sabhaId)
    {
        var shopsList = await _unitOfWork.Shops.getAllShopsForSabhaAndPartnerId(partnerId, sabhaId);
        return shopsList;
    }

    // public async Task<IEnumerable<Sabha>> getSabhaProviceDistricForPartner(int partnerId)
    public async Task<List<IEnumerable<Sabha>>> getSabhaProviceDistricForPartner(int partnerId)
    {
        try
        {
            var allassessmentForPartner = await _unitOfWork.Assessments.GetAllForCustomerId(partnerId);
            var waterConnections = await _unitOfWork.Balances.getWaterConnection(partnerId);
            var partner = await _unitOfWork.Partners.GetByIdWithDetails(partnerId);

            HashSet<int> sabhaIdSet = new HashSet<int>();

            if (partner.SabhaId != null)
            {
                sabhaIdSet.Add(partner.SabhaId.GetValueOrDefault());
            }

            //Asessment
            if (allassessmentForPartner != null)
            {
                foreach (var assessment in allassessmentForPartner)
                {
                    var assessmentSabhaId = assessment.SabhaId.GetValueOrDefault(); // Convert nullable int to int
                    sabhaIdSet.Add(assessmentSabhaId);
                }
            }

            if (partner != null && partner.PermittedThirdPartyAssessments.Any())
            {
                var thirdPartyAssmtIds = partner.PermittedThirdPartyAssessments.Select(a => a.AssessmentId).ToList();
                var thirdPartyAssessmentsList = await _unitOfWork.Assessments.GetAllForIds(thirdPartyAssmtIds);

                foreach (var thirdpartyassessment in thirdPartyAssessmentsList)
                {
                    var thirdpartyassessmentSabhaId = thirdpartyassessment.SabhaId.Value;
                    sabhaIdSet.Add(thirdpartyassessmentSabhaId);
                }
            }

            //Water
            if (waterConnections != null)
            {
                foreach (var connection in waterConnections)
                {
                    var connectionSabhaId = connection.SubRoad?.MainRoad?.SabhaId; // Nullable int?
                    if (connectionSabhaId.HasValue && connectionSabhaId >= 0)
                    {
                        sabhaIdSet.Add(connectionSabhaId.Value); // Extract the value from nullable int?
                    }
                }
            }


            List<IEnumerable<Sabha>> sabhaWithDistrictProvice = new List<IEnumerable<Sabha>>();

            if (sabhaIdSet.Count() == 0)
            {
                var partner1 = await _unitOfWork.Partners.GetByIdAsync(partnerId);
                sabhaIdSet.Add(partner1.SabhaId.Value);
            }

            foreach (var sabhaID in sabhaIdSet)
            {
                try
                {
                    var allWithSabhaBySabhaIdAsync = await _unitOfWork.Sabhas.GetDistrictProvice(sabhaID);
                    sabhaWithDistrictProvice.Add(allWithSabhaBySabhaIdAsync);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return sabhaWithDistrictProvice;

        }
        catch (Exception ex)
        {

            throw ex;
        }
       
        // return sabhaWithDistrictProvice[0];

        // sabhaIdSet.Add(sabhaWithDistrictProvice.Count);
    }

    public async Task<Verified> isAvailable(string NIC, string mobileNo)
    {
        var partner = await _unitOfWork.Partners.GetByNICAsync(NIC);
        var message = "";
        
        if (partner != null && (partner.MobileNumber).Equals(mobileNo))
        {
            var random = new Random();
            var randomNumber = (random.Next(100000, 999999));
            message =
                $"Your One-time password (OTP) for CAT20 Local Government Payment System is \n {randomNumber}.\n Only valid for 5 minutes.";

            return new Verified
            {
                MobileNo = mobileNo,
                Email = "",
                Text = message,
                SabhaId = partner.SabhaId,
                OTP = randomNumber,
                Module = "OnlineUser",
                Subject = "Payment-OTP",
                isIntheSysterm = true
            };
        }
        else
        {
            foreach (var partnerPartnerMobile in partner.PartnerMobiles)
            {
                if (partnerPartnerMobile.MobileNo == mobileNo)
                {
                    var random = new Random();
                    var randomNumber = (random.Next(100000, 999999));
                    message =
                        $"Your One-time password (OTP) for CAT20 Local Government Payment System is \n {randomNumber}.\n Only valid for 5 minutes.";

                    return new Verified
                    {
                        MobileNo = mobileNo,
                        Text = message,
                        SabhaId = 1,
                        OTP = randomNumber,
                        Module = "OnlineUser",
                        Subject = "Payment-OTP",
                        isIntheSysterm = true
                    };
                }
            }
        }

        return new Verified
        {
            isIntheSysterm = false
        };
    }

    public async Task<Verified> isMobileAvailable(string mobileNo)
    {
        var partner = await _unitOfWork.Partners.GetByPhoneNoAsync(mobileNo);
        var message = "";

        if (partner != null)
        {
            var random = new Random();
            var randomNumber = (random.Next(100000, 999999));
            message =
                $"Your One-time password (OTP) for CAT20 Local Government Payment System is \n {randomNumber}.\n Only valid for 5 minutes.";

            return new Verified
            {
                MobileNo = mobileNo,
                Email = "",
                Text = message,
                SabhaId = partner.SabhaId,
                OTP = randomNumber,
                Module = "OnlineUser",
                Subject = "Payment-OTP",
                isIntheSysterm = true
            };
        }
        else
        {
            foreach (var partnerPartnerMobile in partner.PartnerMobiles)
            {
                if (partnerPartnerMobile.MobileNo == mobileNo)
                {
                    var random = new Random();
                    var randomNumber = (random.Next(100000, 999999));
                    message =
                        $"Your One-time password (OTP) for CAT20 Local Government Payment System is \n {randomNumber}.\n Only valid for 5 minutes.";

                    return new Verified
                    {
                        MobileNo = mobileNo,
                        Email = "",
                        Text = message,
                        SabhaId = 1,
                        OTP = randomNumber,
                        Module = "OnlineUser",
                        Subject = "Payment-OTP",
                        isIntheSysterm = true
                    };
                }
            }
        }

        return new Verified
        {
            isIntheSysterm = false
        };
    }


    public async Task<Verified> isEmailAvailable(string email)
    {
        var partner = await _unitOfWork.Partners.GetByEmailAsync(email);
        var message = "";

        if (partner != null)
        {
            var random = new Random();
            var randomNumber = (random.Next(100000, 999999));
            message =
                $"Your One-time password (OTP) for CAT20 Local Government Payment System is \n {randomNumber}.\n Only valid for 5 minutes.";

            return new Verified
            {
                Email = email,
                MobileNo = "",
                Text = message,
                SabhaId = partner.SabhaId,
                OTP = randomNumber,
                Module = "OnlineUser",
                Subject = "Payment-OTP",
                isIntheSysterm = true
            };
        }

        return new Verified
        {
            isIntheSysterm = false
        };
    }

    public async Task<Verified> isPartnerAvailable(string NIC, string mobileNo, int? sabhaId)
    {
        var partner = await _unitOfWork.Partners.GetByNICAsync(NIC);
        var byPhoneNoAsync = await _unitOfWork.Partners.GetByPhoneNoAsync(mobileNo);
        var message = "";
        if (partner != null && byPhoneNoAsync != null)
        {
            return new Verified
            {
                id = 1,
                pNumber = 1,
                isIntheSysterm = true
            };
        }
        else if (partner != null && byPhoneNoAsync == null)
        {
            return new Verified
            {
                id = 1,
                pNumber = 0,
                isIntheSysterm = true
            };
        }
        else if (partner == null && byPhoneNoAsync != null)
        {
            return new Verified
            {
                id = 0,
                pNumber = 1,
                isIntheSysterm = true
            };
        }

        var random = new Random();
        var randomNumber = (random.Next(100000, 999999));
        message = $"Your Local Government Payment System code: \n {randomNumber}.\n Only valid for 5 minutes.";

        return new Verified
        {
            MobileNo = mobileNo,
            Text = message,
            SabhaId = sabhaId,
            OTP = randomNumber,
            Module = "OnlineUser",
            Subject = "Mobile-OTP",
            isIntheSysterm = false,
            id = 0,
            pNumber = 0
        };
    }


    public string GenerateKey()
    {
        string keyBase64 = "";
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.GenerateKey();
            keyBase64 = Convert.ToBase64String(aes.Key);
        }

        return keyBase64;
    }


    public string Encrypt(string data, string key)
    {
        try
        {
            byte[] initializationVector = Encoding.ASCII.GetBytes("abcede0123456789");
            using (Aes aes = Aes.Create())
            {
                // aes.Padding = PaddingMode.Zeros;
                aes.Key = Convert.FromBase64String(key);
                aes.IV = initializationVector;
                var symmetricEncryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream =
                           new CryptoStream(memoryStream, symmetricEncryptor, CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(data);
                        }
                    } // Close the CryptoStream here

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public string Decrypt(string cipherText, string key)
    {
        byte[] initializationVector = Encoding.ASCII.GetBytes("abcede0123456789");
        byte[] buffer = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create())
        {
            // aes.Padding = PaddingMode.Zeros;
            aes.Padding = PaddingMode.PKCS7;

            aes.Key = Convert.FromBase64String(key);
            aes.IV = initializationVector;

            using (var memoryStream = new MemoryStream(buffer))
            {
                using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (var streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }


    public async Task<int?> SaveGateway(PaymentGateway paymentGateway)
    {
        // string key = GenerateKey();
        string key = "Bi83bNUnVcEYaSgg2qIjYPfNyu2kOGzJ3tJBAMM2ddc=";
        paymentGateway.MerchantId = Encrypt(paymentGateway.MerchantId, key);
        paymentGateway.APIKey = Encrypt(paymentGateway.APIKey, key);

        try
        {
            await _unitOfWork.PaymentGateways.AddAsync(paymentGateway);
            await _unitOfWork.CommitAsync1();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        var paymentGatewayId = paymentGateway.Id;

        return paymentGatewayId;
    }

    public async Task<PaymentGateway> GetGateway(int? sabhsId)
    {
        try
        {
            string key = "Bi83bNUnVcEYaSgg2qIjYPfNyu2kOGzJ3tJBAMM2ddc=";
            var paymentGateway = await _unitOfWork.PaymentGateways.GetBySabhaId(sabhsId);
            if (paymentGateway != null) { 
            var gateway = new PaymentGateway
            {
                MerchantId = Decrypt(paymentGateway.MerchantId, key),
                APIKey = Decrypt(paymentGateway.APIKey, key),
                Id = paymentGateway.Id,
                BankName = paymentGateway.BankName,
                SabhaId = paymentGateway.SabhaId,
                ProvinceId = paymentGateway.ProvinceId,
                ServicePercentage = paymentGateway.ServicePercentage,
                ReportAPIKey = paymentGateway.ReportAPIKey,
                AccessKey = paymentGateway.AccessKey,
                ProfileID = paymentGateway.ProfileID,
                SecretKey = paymentGateway.SecretKey
            };

            return gateway;
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public async Task<bool> PlaceOrderBackUp(int status, int paymentDetailId)
    {
        var newOrder = new PaymentDetailBackUp
        {
            Id = null,
            PaymentDetailId = paymentDetailId,
            Status = status
        };

        try
        {
            await _unitOfWork.PaymentsBackUps.AddAsync(newOrder);
            await _unitOfWork.CommitAsync1();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<PaymentDetails> PlaceOtherPaymentOrder(int status, int id)
    {
        var paymentDetailById = await _unitOfWork.Payments.GetById(id);
        if (paymentDetailById != null)
        {
            try
            {
                paymentDetailById.Status = status;
                await _unitOfWork.CommitAsync1();
                return await _unitOfWork.Payments.GetById(id);
            }
            catch (Exception e)
            {
                return await _unitOfWork.Payments.GetById(id);

            }
        }

        return await _unitOfWork.Payments.GetById(id);

    }

    public async Task<PaymentDetails> PlaceAssessmentOrder(List<MixinOrder> newOrders, int status, int id ,int? cId)
    {
        
        
            var sessionDate = await _unitOfWork.Sessions.IsRescueSessionThenDateO(newOrders.First().SessionId);
            List<int?> asseessmentIds = newOrders.Select(mx => mx.AssessmentId).ToList();
            var asmtBals = await _unitOfWork.AssessmentBalances.GetForOrderTransaction(asseessmentIds);

            var paymentDetailById = await _unitOfWork.Payments.GetById(id);


            foreach (var item in asmtBals)
            {
                item.HasTransaction = true;
            }

            if (sessionDate.HasValue)
            {
                foreach (var newMixinOrder in newOrders)
                {
                    newMixinOrder.CreatedAt = (DateTime)sessionDate;
                    newMixinOrder.PaymentDetailId = paymentDetailById.PaymentDetailId;
                    var officeId = newMixinOrder.OfficeId;
                    var office = await _unitOfWork.Offices.GetByIdAsync(officeId);


                    var docSeqNums = await _unitOfWork.DocumentSequenceNumbers
                        .GetNextSequenceNumberForYearOfficePrefixAsync(DateTime.Now.Year, officeId, "MIX");

                    newMixinOrder.State = OrderStatus.Posted;
                    newMixinOrder.CashierId = -1;
                    newMixinOrder.UpdatedAt = DateTime.Now;
                    // newMixinOrder.Code = $"ONLINE/{office.Code}/{DateTime.Now.Year}/MIX/{++docSeqNums.LastIndex}";
                    newMixinOrder.Code = $"{office.Code}/{DateTime.Now.Year}/ONLINE/ASM/{++docSeqNums.LastIndex}";


                    if (newMixinOrder.BusinessId.HasValue && newMixinOrder.BusinessId != 0)
                    {
                        var businessTaxes =
                            await _unitOfWork.BusinessTaxes.GetByIdAsync(newMixinOrder.BusinessId.Value);

                        if (businessTaxes != null)
                        {
                            businessTaxes.TaxState = TaxStatus.Paid;
                            businessTaxes.LicenseNo = newMixinOrder.Code;
                            businessTaxes.UpdatedAt = System.DateTime.Now;
                            businessTaxes.UpdatedBy = -1;
                        }
                        else
                        {
                            throw new Exception("Business taxes not found.");
                        }
                    }

                    if (newMixinOrder.AssessmentId.HasValue && newMixinOrder.AssessmentId != 0)
                    {
                        var assessmentBalance =
                            await _unitOfWork.AssessmentBalances.GetByIdToProcessPayment(newMixinOrder.AssessmentId
                                .Value);
                        int? month =
                            await _unitOfWork.Sessions.GetCurrentSessionMonthForProcess(newMixinOrder.SessionId);
                        var rates = await _unitOfWork.AssessmentRates.GetByIdAsync(1);

                        if (assessmentBalance != null && assessmentBalance.Q1 != null &&
                            assessmentBalance.Q2 != null &&
                            assessmentBalance.Q3 != null && assessmentBalance.Q4 != null && month.HasValue &&
                            rates != null && assessmentBalance.HasTransaction == true)
                        {
                            (var op, var payable, var deduction, var paying, var nextbal, var discount,
                                    var dctRate) =
                                _assessmentBalanceService.CalculatePaymentBalance(assessmentBalance, rates,
                                    newMixinOrder.TotalAmount, month.Value, true);

                            assessmentBalance.ByExcessDeduction += deduction.Total;
                            assessmentBalance.Paid += newMixinOrder.TotalAmount;
                            assessmentBalance.ExcessPayment = 0;
                            assessmentBalance.DiscountRate =
                                discount.Total > 0 ? dctRate : assessmentBalance.DiscountRate;
                            assessmentBalance.Discount += discount.Total;
                            assessmentBalance.OverPayment += paying.OverPayment +=
                                deduction.OverPayment != 0 ? deduction.OverPayment : 0;

                            assessmentBalance.LYWarrant = nextbal.LYWarrant;
                            assessmentBalance.LYArrears = nextbal.LYArrears;

                            assessmentBalance.TYWarrant = nextbal.TYWarrant;
                            assessmentBalance.TYArrears = nextbal.TYArrears;
                            assessmentBalance.NumberOfPayments += 1;


                            if (!assessmentBalance.Q1.IsOver && !assessmentBalance.Q1.IsCompleted &&
                                (paying.Q1 != 0 || (paying.Q1 == 0 && discount.Q1 != 0)))

                            {
                                assessmentBalance.Q1.ByExcessDeduction += deduction.Q1;
                                assessmentBalance.Q1.Paid += paying.Q1;
                                assessmentBalance.Q1.Discount += discount.Q1;
                                assessmentBalance.Q1.IsCompleted =
                                    assessmentBalance.Q1.Amount - assessmentBalance.Q1.ByExcessDeduction -
                                    assessmentBalance.Q1.Paid - assessmentBalance.Q1.Discount == 0
                                        ? true
                                        : false;
                            }

                            if (!assessmentBalance.Q2.IsOver && !assessmentBalance.Q2.IsCompleted &&
                                (paying.Q2 != 0 || (paying.Q2 == 0 && discount.Q2 != 0)))
                            {
                                assessmentBalance.Q2.ByExcessDeduction += deduction.Q2;
                                assessmentBalance.Q2.Paid += paying.Q2;
                                assessmentBalance.Q2.Discount += discount.Q2;
                                assessmentBalance.Q2.IsCompleted =
                                    assessmentBalance.Q2.Amount - assessmentBalance.Q2.ByExcessDeduction -
                                    assessmentBalance.Q2.Paid - assessmentBalance.Q2.Discount == 0
                                        ? true
                                        : false;
                            }

                            if (!assessmentBalance.Q3.IsOver && !assessmentBalance.Q3.IsCompleted &&
                                (paying.Q3 != 0 || (paying.Q3 == 0 && discount.Q3 != 0)))
                            {
                                assessmentBalance.Q3.ByExcessDeduction += deduction.Q3;
                                assessmentBalance.Q3.Paid += paying.Q3;
                                assessmentBalance.Q3.Discount += discount.Q3;
                                assessmentBalance.Q3.IsCompleted =
                                    assessmentBalance.Q3.Amount - assessmentBalance.Q3.ByExcessDeduction -
                                    assessmentBalance.Q3.Paid - assessmentBalance.Q3.Discount == 0
                                        ? true
                                        : false;
                            }

                            if (!assessmentBalance.Q4.IsOver && !assessmentBalance.Q4.IsCompleted &&
                                (paying.Q4 != 0 || (paying.Q4 == 0 && discount.Q4 != 0)))
                            {
                                assessmentBalance.Q4.ByExcessDeduction += deduction.Q4;
                                assessmentBalance.Q4.Paid += paying.Q4;
                                assessmentBalance.Q4.Discount += discount.Q4;
                                assessmentBalance.Q4.IsCompleted =
                                    assessmentBalance.Q4.Amount - assessmentBalance.Q4.ByExcessDeduction -
                                    assessmentBalance.Q4.Paid - assessmentBalance.Q4.Discount == 0
                                        ? true
                                        : false;
                            }

                            if (assessmentBalance.Q1.IsCompleted && assessmentBalance.Q2.IsCompleted &&
                                assessmentBalance.Q3.IsCompleted && assessmentBalance.Q4.IsCompleted)
                            {
                                assessmentBalance.IsCompleted = true;
                            }


                            assessmentBalance.UpdatedBy = -1;
                            assessmentBalance.UpdatedAt = DateTime.Now;
                            assessmentBalance.HasTransaction = false;

                            var q1 = (!assessmentBalance.Q1.IsCompleted && !assessmentBalance.Q1.IsOver)
                                ? assessmentBalance.Q1.Amount - assessmentBalance.Q1.ByExcessDeduction -
                                  assessmentBalance.Q1.Paid - assessmentBalance.Q1.Discount
                                : 0;
                            var q2 = (!assessmentBalance.Q2.IsCompleted && !assessmentBalance.Q2.IsOver)
                                ? assessmentBalance.Q2.Amount - assessmentBalance.Q2.ByExcessDeduction -
                                  assessmentBalance.Q2.Paid - assessmentBalance.Q2.Discount
                                : 0;
                            var q3 = (!assessmentBalance.Q3.IsCompleted && !assessmentBalance.Q3.IsOver)
                                ? assessmentBalance.Q3.Amount - assessmentBalance.Q3.ByExcessDeduction -
                                  assessmentBalance.Q3.Paid - assessmentBalance.Q3.Discount
                                : 0;
                            var q4 = (!assessmentBalance.Q4.IsCompleted && !assessmentBalance.Q4.IsOver)
                                ? assessmentBalance.Q4.Amount - assessmentBalance.Q4.ByExcessDeduction -
                                  assessmentBalance.Q4.Paid - assessmentBalance.Q4.Discount
                                : 0;


                            var transaction = new AssessmentTransaction
                            {
                                AssessmentId = assessmentBalance.AssessmentId,
                                //DateTime = DateTime.Now,
                                Type = AssessmentTransactionsType.Payment,
                                LYArrears = assessmentBalance.LYArrears,
                                LYWarrant = assessmentBalance.LYWarrant,

                                TYArrears = assessmentBalance.TYArrears,
                                TYWarrant = assessmentBalance.TYWarrant,
                                RunningOverPay = assessmentBalance.OverPayment,

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
                                    - assessmentBalance.OverPayment,
                                DiscountRate = assessmentBalance.DiscountRate
                            };

                            await _unitOfWork.AssessmentTransactions.AddAsync(transaction);
                        }
                    }

                    foreach (var item in newMixinOrder!.MixinOrderLine)
                    {
                        item.CreatedAt = (DateTime)sessionDate;
                    }
                }

                
                // newOrders.ForEach(order => order.Code = DateTime.Now.ToString("HHmmssfff") + newOrders.IndexOf(order));
                await _unitOfWork.MixinOrders.AddRangeAsync(newOrders);
                // await _unitOfWork.CommitAsync();

                if (paymentDetailById != null)
                {
                    paymentDetailById.UpdatedAt = DateTime.Now;
                    paymentDetailById.Status = status;

                    if (paymentDetailById.Error == 1 && cId != null)
                    {
                        paymentDetailById.Error = 0;
                        paymentDetailById.Check = 1;
                        paymentDetailById.CashierId = cId;
                        paymentDetailById.CashierUpdatedAt = DateTime.Now;
                    }
                    

                    await _unitOfWork.CommitAsync();
                }

                return await _unitOfWork.Payments.GetById(id);
            }

            return await _unitOfWork.Payments.GetById(id);
        
       
    
    }



    public async Task<PaymentDetails> PlaceWaterBillOrder(List<MixinOrder> newOrders, int status, int id, int? cId )
    {
        var sessionDate = await _unitOfWork.Sessions.IsRescueSessionThenDateO(newOrders.First().SessionId);
        List<int?> waterConnectionIDs= newOrders.Select(mx => mx.WaterConnectionId).ToList();
       /* var asmtBals = await _unitOfWork.AssessmentBalances.GetForOrderTransaction(waterConnectionIDs);
       */
        var paymentDetailById = await _unitOfWork.Payments.GetById(id);


        //foreach (var item in asmtBals)
        //{
        //    item.HasTransaction = true;
        //}

        if (sessionDate.HasValue)
        {
            foreach (var newMixinOrder in newOrders)
            {

                var session = await _unitOfWork.Sessions.GetActiveSessionByOfficeAsync(newOrders[0].OfficeId);
                if (session != null)
                {

                    var SessionDate = await _unitOfWork.Sessions.IsRescueSessionThenDate(newMixinOrder.SessionId);

                    if (SessionDate.HasValue)
                    {

                        newMixinOrder.CreatedAt = (DateTime)sessionDate;

                        foreach (var item in newMixinOrder!.MixinOrderLine)
                        {
                            item.CreatedAt = (DateTime)sessionDate;
                        }
                    }
                    else
                    {

                        newMixinOrder.CreatedAt = DateTime.Now;
                        sessionDate = DateTime.Now;
                        foreach (var item in newMixinOrder!.MixinOrderLine)
                        {
                            item.CreatedAt = DateTime.Now;
                        }

                    }





                    var office = await _unitOfWork.Offices.GetByIdAsync(newMixinOrder.OfficeId.Value);


                    if (!string.IsNullOrEmpty(office?.Code))
                    {
                        int bankaccountid = newMixinOrder.AccountDetailId.Value;
                        string prefix = "MIX";
                        if (newMixinOrder.AppCategoryId == 5)
                        {
                            prefix = "ASM";
                            if (office.OfficeTypeID == 2)
                            {
                                bankaccountid = await _voteAssignmentService.GetAssignedBankAccountForSubOffice(office.ID.Value);
                                if (bankaccountid != 0 && bankaccountid != null)
                                {
                                    newMixinOrder.AccountDetailId = bankaccountid;
                                }
                            }
                        }
                        else if (newMixinOrder.AppCategoryId == 3)
                        {
                            prefix = "SHP";
                        }
                        else if (newMixinOrder.AppCategoryId == 4)
                        {
                            prefix = "WTR";
                        }
                        else
                        {
                            prefix = "MIX";
                        }
                        var docSeqNums = await _unitOfWork.DocumentSequenceNumbers
                            .GetNextSequenceNumberForYearOfficePrefixAsync(newMixinOrder.CreatedAt.Year, newMixinOrder.OfficeId.Value, "MIX");

                        if (docSeqNums != null)
                        {

                            // Update mxOrder properties
                            newMixinOrder.State = OrderStatus.Paid;
                            newMixinOrder.CashierId = -1;
                            newMixinOrder.UpdatedAt = DateTime.Now;
                            newMixinOrder.Code = $"{office.Code}/{DateTime.Now.Year}/{prefix}/{++docSeqNums.LastIndex}";
                        }
                    }



                    await _unitOfWork.MixinOrders.AddAsync(newMixinOrder);



                  //  var x = await _waterConnectionBalanceService.CalculatePayments(newMixinOrder.WaterConnectionId!.Value, sessionDate.Value, newMixinOrder.TotalAmount, true, false, newMixinOrder.CreatedBy);


                   /* if (token.IsFinalAccountsEnabled == 1)
                    {
                        if (await UpdateVoteBalance(newMixinOrder, session, CashBookTransactionType.DEBIT, CashBookIncomeCategory.Mix, token))
                        {
                            //await _unitOfWork.CommitAsync();
                        }
                        else
                        {
                            throw new FinalAccountException("Vote Balance Entry Not updated.");
                        }



                        if (await CreateCashBookEntry(wbOrder, CashBookTransactionType.DEBIT, CashBookIncomeCategory.Mix, session, token))
                        {
                            //await _unitOfWork.CommitAsync();
                        }
                        else
                        {
                            throw new FinalAccountException("Cashbook Entry Not Created.");
                        }
                    }
                   */
                    await _unitOfWork.CommitAsync();
                    //return newMixinOrder;
                }

                foreach (var item in newMixinOrder!.MixinOrderLine)
                {
                    item.CreatedAt = (DateTime)sessionDate;
                }
            }


            // newOrders.ForEach(order => order.Code = DateTime.Now.ToString("HHmmssfff") + newOrders.IndexOf(order));
            await _unitOfWork.MixinOrders.AddRangeAsync(newOrders);
            // await _unitOfWork.CommitAsync();

            if (paymentDetailById != null)
            {
                paymentDetailById.UpdatedAt = DateTime.Now;
                paymentDetailById.Status = status;

                if (paymentDetailById.Error == 1 && cId != null)
                {
                    paymentDetailById.Error = 0;
                    paymentDetailById.Check = 1;
                    paymentDetailById.CashierId = cId;
                    paymentDetailById.CashierUpdatedAt = DateTime.Now;
                }


                await _unitOfWork.CommitAsync();
            }

            return await _unitOfWork.Payments.GetById(id);
        }

        return await _unitOfWork.Payments.GetById(id);



    }

    public async Task<Dispute> CreateDispute(Dispute dispute)
    {
        dispute.CreatedAt = DateTime.Now;
        await _unitOfWork.Dispute.AddAsync(dispute);
        var paymentDetails = await _unitOfWork.Payments.GetById(dispute.PaymentDetailId);
        paymentDetails.Error = 1;
        await _unitOfWork.CommitAsync1();
        return dispute;
    }


    public async Task<int?> SavePaymentDetail(PaymentDetails paymentDetails)
    {
        
            paymentDetails.CreatedAt = DateTime.Now;
            await _unitOfWork.Payments.AddAsync(paymentDetails);
            await _unitOfWork.CommitAsync1();
        
            
        var paymentDetailsId = paymentDetails.PaymentDetailId;

        return paymentDetailsId;
    }


    public async Task<PaymentDetails> GetPaymentDetailById(int? id)
    {
        return await _unitOfWork.Payments.GetById(id);
    }

    public async Task<PaymentDetails> CheckByCashier(int id, int cId, int flag)
    {
        
        
            if (flag ==1 || flag ==2)
            {
                var paymentDetails = await _unitOfWork.Payments.GetById(id);
                paymentDetails.Check = 1;
                paymentDetails.CashierId = cId;
                paymentDetails.CashierUpdatedAt = DateTime.Now;
                await _unitOfWork.CommitAsync1();
            } else if (flag ==3)
            {
                var paymentDetails = await _unitOfWork.Payments.GetById(id);
                paymentDetails.Error = 0;
                paymentDetails.Status = 1;
                // paymentDetails.CashierId = cId;
                paymentDetails.Dispute.UpdatedAt = DateTime.Now;
                paymentDetails.Dispute.UserId = cId;
                paymentDetails.CashierUpdatedAt = DateTime.Now;
                await _unitOfWork.CommitAsync1();

            }

            return await _unitOfWork.Payments.GetById(id);
        
       
    }

    public async Task<(int totalCount, IEnumerable<PaymentDetails> list)> GetAllPaymentDetails(int officeId,
        int? pageNumber, int? pageSize,
        string? filterKeyword)
    {
        return await _unitOfWork.Payments.GetAllPaymentDetails(officeId, pageNumber, pageSize, filterKeyword);
    }

    public async Task<(int totalCount, IEnumerable<PaymentDetails> list)> GetOtherPaymentDetails(int officeId,
        int? pageNumber, int? pageSize,
        string? filterKeyword)
    {
        return await _unitOfWork.Payments.GetOtherPaymentDetails(officeId, pageNumber, pageSize, filterKeyword);
    }
    public async Task<(int totalCount, IEnumerable<PaymentDetails> list)> getDisputes(int officeId,
        int? pageNumber, int? pageSize,
        string? filterKeyword)
    {
        return await _unitOfWork.Payments.getDisputes(officeId, pageNumber, pageSize, filterKeyword);
    }

    public async Task<(int totalCount, IEnumerable<PaymentDetails> list)> getPaymentHistory(int officeId,
        int? pageNumber, int? pageSize, string? filterKeyword)
    {
        return await _unitOfWork.Payments.GetOtherPaymentDetails(officeId, pageNumber, pageSize, filterKeyword);
    }

    public async Task<(int totalCount, IEnumerable<PaymentDetails> list)> getPaymentHistoryForOffice(int officeId,
        int? pageNumber, int? pageSize, string? filterKeyword)
    {
        return await _unitOfWork.Payments.getPaymentHistoryForOffice(officeId, pageNumber, pageSize, filterKeyword);
    }

    public async Task<IEnumerable<PaymentDetails>> getPartnerPaymentHistory(int partnerId, int? pageNumber,
        int? pageSize)
    {
        return await _unitOfWork.Payments.getPartnerPaymentHistory(partnerId, pageNumber, pageSize);
    }
    public async Task<IEnumerable<PaymentDetails>> getPartnerDisputes(int partnerId, int? pageNumber,
        int? pageSize)
    {
        return await _unitOfWork.Payments.getPartnerDisputes(partnerId, pageNumber, pageSize);
    }

    public async Task<bool> SaveError(int PaymentDetailsId)
    {
        try
        {
            var paymentDetails = await _unitOfWork.Payments.GetById(PaymentDetailsId);
            paymentDetails.Error = 1;
            await _unitOfWork.CommitAsync1();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<int?> SaveLogInInfo(LogInDetails logInDetails)
    {
        try
        {
            await _unitOfWork.OnlineLogIns.AddAsync(logInDetails);
            await _unitOfWork.CommitAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        var paymentDetailsLogInId = logInDetails.logInID;

        return paymentDetailsLogInId;
    }

    public async Task<int?> UpdatePaymentDetail(PaymentDetails paymentDetails)
    {
        return await _unitOfWork.CommitAsync();
    }


    public async Task<PaymentDetails> PlaceBookingPaymentOrder(int status, int id , int orderId)
    {
        var paymentDetailById = await _unitOfWork.Payments.GetById(id);

        if (paymentDetailById != null)
        {
            try
            {
                //Booking and change the transaction Id Set 

                var order = await _unitOfWork.OnlineBooking.GetByIdAsync(orderId);
                order.PaymentStatus = BookingPaymentStatus.Paid;

                order.TransactionId = id;


                paymentDetailById.Status = status;
                await _unitOfWork.CommitAsync1();
                return await _unitOfWork.Payments.GetById(id);
            }
            catch (Exception e)
            {
                return await _unitOfWork.Payments.GetById(id);

            }
        }

        return await _unitOfWork.Payments.GetById(id);

    }

    public async Task<Object> Create(MixinOrder newMixinOrder , int? cId, bool? dispute)
    {
        
        
            var sessionDate = await _unitOfWork.Sessions.IsRescueSessionThenDate(newMixinOrder.SessionId);
            
            if (sessionDate.HasValue)
            {
                newMixinOrder.CreatedAt = (DateTime)sessionDate;
            
                foreach (var item in newMixinOrder!.MixinOrderLine)
                {
                    item.CreatedAt = (DateTime)sessionDate;
                }
            }
            else
            {
                newMixinOrder.CreatedAt = DateTime.Now;
            
                foreach (var item in newMixinOrder!.MixinOrderLine)
                {
                    item.CreatedAt = DateTime.Now;
                }
            }
            
            var office = await _unitOfWork.Offices.GetByIdAsync(newMixinOrder.OfficeId.Value);

            
            if (!string.IsNullOrEmpty(office?.Code))
            {
                int bankaccountid = newMixinOrder.AccountDetailId.Value;
                string prefix = "MIX";
                if (newMixinOrder.AppCategoryId == 5)
                {
                    prefix = "ONLINE/ASM";
                    if (office.OfficeTypeID == 2)
                    {
                        bankaccountid = await _voteAssignmentService.GetAssignedBankAccountForSubOffice(office.ID.Value);
                        if(bankaccountid!=0 && bankaccountid!=null)
                        {
                            newMixinOrder.AccountDetailId = bankaccountid;
                        }
                    }
                }
                else if(newMixinOrder.AppCategoryId == 3)
                {
                    prefix = "ONLINE/SHP";
                }
                else if (newMixinOrder.AppCategoryId == 4)
                {
                    prefix = "ONLINE/WTR";
                } else if (newMixinOrder.AppCategoryId == 8)
                {
                    prefix = "ONLINE/TRT";
                } else if (newMixinOrder.AppCategoryId == 6)
                {
                    prefix =  "ONLINE/GUL";
                } else if (newMixinOrder.AppCategoryId == 7)
                {
                    prefix =  "ONLINE/HAB";
                }
                else
                {
                    prefix = "ONLINE/MIX";
                }
                var docSeqNums = await _unitOfWork.DocumentSequenceNumbers
                    .GetNextSequenceNumberForYearOfficePrefixAsync(newMixinOrder.CreatedAt.Year, newMixinOrder.OfficeId.Value, "MIX");

                newMixinOrder.State = OrderStatus.Posted;
                newMixinOrder.CashierId = cId;
                newMixinOrder.UpdatedAt = DateTime.Now;

                if (docSeqNums != null)
                {

                    // Update mxOrder properties
                    newMixinOrder.State = OrderStatus.Posted;
                    newMixinOrder.CashierId = cId;
                    newMixinOrder.UpdatedAt = DateTime.Now;
                    newMixinOrder.Code = $"{office.Code}/{DateTime.Now.Year}/{prefix}/{++docSeqNums.LastIndex}";

                }
            }
            
            if (newMixinOrder.BusinessId.HasValue && newMixinOrder.BusinessId != 0)
            {

                var businessTaxes = await _unitOfWork.BusinessTaxes.GetByIdAsync(newMixinOrder.BusinessId.Value);

                if (businessTaxes != null)
                {
                    businessTaxes.TaxState = TaxStatus.Paid;
                    businessTaxes.LicenseNo = newMixinOrder.Code;
                    businessTaxes.UpdatedAt = System.DateTime.Now;
                    businessTaxes.UpdatedBy = cId;
                }
                else
                {
                    throw new Exception("Business taxes not found.");
                }


            }
            
            
            // newMixinOrder.Code = DateTime.Now.ToString("HHmmssfff") + new Random().Next(10);
            await _unitOfWork.MixinOrders.AddAsync(newMixinOrder);
            
            var paymentDetailById = await GetPaymentDetailById(newMixinOrder.PaymentDetailId);
            paymentDetailById.Check = 1;
            paymentDetailById.CashierId = cId;
            if (dispute ?? false)
            {
                paymentDetailById.Status = 1;
                paymentDetailById.Error = 0;
                paymentDetailById.Dispute.UpdatedAt = DateTime.Now;
            }
            paymentDetailById.CashierUpdatedAt = DateTime.Now;
            
            #region Audit Log
            
            //var note = new StringBuilder();
            //if (newMixinOrder.ID == 0)
            //    note.Append("Created on ");
            //else
            //    note.Append("Edited on ");
            //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            //note.Append(" by ");
            //note.Append("System");
            
            
            //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
            //{
            //    dateTime = DateTime.Now,
            //    TransactionID = newMixinOrder.ID,
            //    TransactionName = "MixinOrder",
            //    User = 1,
            //    Note = note.ToString()
            //});
            
            #endregion
            
            await _unitOfWork.CommitAsync2();
            var paymentDetail =  await _unitOfWork.Payments.GetById(newMixinOrder.PaymentDetailId);
            
            return paymentDetail;
            
            
            

    }

    public async Task<IEnumerable<Province>> GetAllProvince()
    {
        var gateways = await _unitOfWork.PaymentGateways.getAll();
        HashSet<int> provinceIdSet = new HashSet<int>();

        if (gateways != null)
        {
            foreach (var gateway in gateways)
            {
                var provinceId = gateway.ProvinceId.GetValueOrDefault(); // Convert nullable int to int
                if (provinceId != null)
                {
                    provinceIdSet.Add(provinceId);
                }
            }
        }

        List<Province> ProvinceList = new List<Province>();

        foreach (var provinceId in provinceIdSet)
        {
            try
            {
                var province = await _unitOfWork.Provinces.GetWithProvinceByIdAsync(provinceId);
                if (province != null)
                {
                    ProvinceList.Add(province);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        return ProvinceList;
    }

    public async Task<IEnumerable<Sabha>> getAllSabha(int provinceId)
    {
        try
        {
            var gateways = await _unitOfWork.PaymentGateways.getAllByProvinceId(provinceId);
            HashSet<int> sabhaIdSet = new HashSet<int>();

            if (gateways != null)
            {
                foreach (var gateway in gateways)
                {
                    var sabhaId = gateway.SabhaId.GetValueOrDefault();
                    if (sabhaId != null)
                    {
                        sabhaIdSet.Add(sabhaId);
                    }
                }
            }

            List<Sabha> sabhaList = new List<Sabha>();

            foreach (var sabhaId in sabhaIdSet)
            {
                var sabha = await _unitOfWork.Sabhas.GetWithSabhaByIdAsync(sabhaId);
                if (sabha != null)
                {
                    sabhaList.Add(sabha);
                }
            }


            return sabhaList;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> paymentDetailSheduler()
    {
        
            var paymentDetailsEnumerable = await _unitOfWork.Payments.paymentDetailSheduler();
            foreach (var paymentDetails in paymentDetailsEnumerable)
            {
                var paymentGateway = await GetGateway(paymentDetails.SabhaId);

                var apiUrlTemplate = _configuration["BOCPaymentGateway:ApiReportUrl"];
                var merchantId = paymentGateway.MerchantId;
                var ApiUrl = apiUrlTemplate.Replace("{merchantId}", merchantId)
                    .Replace("{orderid}", paymentDetails.TransactionId).Replace("{transactionid}", "1");
                var apiUrl = ApiUrl;
                var apiKey = $"merchant.{paymentGateway.MerchantId}:{paymentGateway.APIKey}";

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization",
                        "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey)));

                    var response = await httpClient.GetAsync(apiUrl);

                    var result = "";
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<ReportResponse>(result);
                        if (responseObject.Result == "SUCCESS")
                        {
                            var byPaymentDetailId = await _unitOfWork.MixinOrders.getByPaymentDetailId(paymentDetails.PaymentDetailId);

                            if (byPaymentDetailId.Id > 0 && byPaymentDetailId != null && byPaymentDetailId.PaymentDetailId == paymentDetails.PaymentDetailId && byPaymentDetailId.TotalAmount == paymentDetails.InputAmount)
                            {
                                paymentDetails.Status = 1;
                            }
                            else
                            {
                                paymentDetails.Error = 1;
                                paymentDetails.Dispute = new Dispute()
                                {
                                    Id = null,
                                    Reason = DisputeReason.PaymentFailed,
                                    CreatedAt = DateTime.Now,
                                    UpdatedAt = null,
                                    UserId = -1,
                                    Message = "Payment Failed"
                                };
                            }
                            
                        }
                    }
                }
            }
            await _unitOfWork.CommitAsync1();
            return true;
        
        
    }
}