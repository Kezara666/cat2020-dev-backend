using CAT20.Core.Models.Control;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Services.OnlinePayment;
using CAT20.WebApi.Resources.OnlinePayment;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static CAT20.WebApi.Controllers.OnlinePayment.CYBSPEBBasic;

namespace CAT20.WebApi.Controllers.OnlinePayment
{
    [Route("api/onlinePayments/[controller]")]
    [ApiController]
    public class PeoplesBankPaymentController : ControllerBase
    {
        private readonly IOnlinePaymentService _onlinePaymentService;
        private readonly IConfiguration _configuration;

        public PeoplesBankPaymentController(IConfiguration configuration, IOnlinePaymentService onlinePaymentService)
        {
            _onlinePaymentService = onlinePaymentService;
            _configuration = configuration;
        }

        [HttpPost("getPaymentForm")]
        public IActionResult GetPaymentForm([FromBody] PaymentFormData formData)
        {
            try
            {
                CYBSPEBBasic ipg = new CYBSPEBBasic();
                PaymentGateway gatewayInfo = new PaymentGateway
                {
                    SecretKey = "d6ef666014be4cda9421ac41e51048d36527af1800914fd4ae371d43907ec101082cd843f0c34450a0b9299883dbf055af7bf4112ced4e3497024368698e7ba3394a8c3761a4455a9d35b61231b810e90c10bc5a7e1042c597b80c0ea02bdbe0291e061a2d7448648aedfb326a305a9b391469a697ce4b7aae5c56431eda3350",
                    AccessKey = "47e0d13063583e179ee8ecf126cdb878",
                    ProfileID = "E6719FA3-6E97-41FA-9935-EBFE3DC5B466"
                };
                string formHtml = ipg.GetDefaultForm(formData, gatewayInfo);

                // Wrap the HTML content in a JSON object
                var response = new { formHtml = formHtml };
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an appropriate error response
                return StatusCode(500, new { error = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpGet("callback")]
        public IActionResult PaymentCallback([FromQuery] string status, [FromQuery] string transactionId)
        {
            Console.WriteLine($"Payment status: {status}, Transaction ID: {transactionId}");
            return Ok();
        }


        //new
        private const string SecretKey = "d6ef666014be4cda9421ac41e51048d36527af1800914fd4ae371d43907ec101082cd843f0c34450a0b9299883dbf055af7bf4112ced4e3497024368698e7ba3394a8c3761a4455a9d35b61231b810e90c10bc5a7e1042c597b80c0ea02bdbe0291e061a2d7448648aedfb326a305a9b391469a697ce4b7aae5c56431eda3350";
        private const string AccessKey = "47e0d13063583e179ee8ecf126cdb878";
        private const string ProfileId = "E6719FA3-6E97-41FA-9935-EBFE3DC5B466";
        private const string GatewayUrl = "https://testsecureacceptance.cybersource.com/pay";

        [HttpPost("initiate")]
        public IActionResult InitiatePayment([FromBody] PaymentRequest request)
        {
            try
            {
                var signature = GenerateSignature(request);
                return Ok(new { signature });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private string GenerateSignature(PaymentRequest request)
        {
            var signedFieldNames = "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,currency";
            var dataToSign = $"{signedFieldNames},{request.signed_date_time},{request.transaction_type},{request.reference_number},{request.amount},{request.currency},{request.auth_trans_ref_no},{request.bill_to_email},{request.bill_to_forename},{request.bill_to_surname},{request.bill_to_address_line1},{request.bill_to_address_city},{request.bill_to_address_country},{request.bill_to_address_postal_code}";
            return SignData(dataToSign);
        }

        private string SignData(string data)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(SecretKey)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return Convert.ToBase64String(hash);
            }
        }

        public class PaymentRequest
        {
            public string transaction_uuid { get; set; }
            public string signed_date_time { get; set; }
            public string transaction_type { get; set; }
            public string reference_number { get; set; }
            public double amount { get; set; }
            public string currency { get; set; }
            public string auth_trans_ref_no { get; set; }
            public string bill_to_email { get; set; }
            public string bill_to_forename { get; set; }
            public string bill_to_surname { get; set; }
            public string bill_to_address_line1 { get; set; }
            public string bill_to_address_city { get; set; }
            public string bill_to_address_country { get; set; }
            public string bill_to_address_postal_code { get; set; }
            public string signed_field_names { get; set; }
            public string unsigned_field_names { get; set; }
            public string locale { get; set; }

            // Constructor
            public PaymentRequest()
            {
                // You can initialize properties or implement constructor logic here if needed
            }
        }

    }

    public class PaymentFormData
    {
        public string OrderId { get; set; }
        public string PurchaseAmt { get; set; }
    }

    public class CYBSPEBBasic
    {
        public string GetDefaultForm(PaymentFormData FormData, PaymentGateway GatewayInfo)
        {
            //string SECRET_KEY = "d6ef666014be4cda9421ac41e51048d36527af1800914fd4ae371d43907ec101082cd843f0c34450a0b9299883dbf055af7bf4112ced4e3497024368698e7ba3394a8c3761a4455a9d35b61231b810e90c10bc5a7e1042c597b80c0ea02bdbe0291e061a2d7448648aedfb326a305a9b391469a697ce4b7aae5c56431eda3350";
            //string access_key = "47e0d13063583e179ee8ecf126cdb878";
            //string profile_id = "E6719FA3-6E97-41FA-9935-EBFE3DC5B466";
            string url = "https://testsecureacceptance.cybersource.com/pay";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["transaction_uuid"] = Guid.NewGuid().ToString();
            parameters["access_key"] = GatewayInfo.AccessKey;
            parameters["profile_id"] = GatewayInfo.ProfileID;
            parameters["signed_field_names"] = "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,currency";
            parameters["unsigned_field_names"] = "auth_trans_ref_no,bill_to_forename,bill_to_surname,bill_to_address_line1,bill_to_address_city,bill_to_address_country,bill_to_email";
            parameters["signed_date_time"] = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            parameters["locale"] = "en";
            parameters["transaction_type"] = "sale";
            parameters["reference_number"] = FormData.OrderId;
            parameters["auth_trans_ref_no"] = FormData.OrderId;
            parameters["amount"] = FormData.PurchaseAmt;
            parameters["currency"] = "LKR";
            parameters["bill_to_email"] = "null@cybersource.com";
            parameters["bill_to_forename"] = "noreal";
            parameters["bill_to_surname"] = "name";
            parameters["bill_to_address_line1"] = "1295 Charleston Rd";
            parameters["bill_to_address_city"] = "Mountain View";
            parameters["bill_to_address_country"] = "US";
            parameters["bill_to_address_postal_code"] = "94043";
            parameters["signature"] = Sign(parameters, GatewayInfo.SecretKey);


            StringBuilder formText = new StringBuilder();
            formText.Append("<script src=\"https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js\"></script>");
            formText.Append("<script>$(\"#payment_confirmation\").ready(function(){$(\"#payment_confirmation\").submit();});</script>");
            formText.Append("<form type=\"hidden\" id=\"payment_confirmation\" action=\"" + WebUtility.HtmlEncode(url) + "\" method=\"post\">");
            foreach (var kvp in parameters)
            {
                formText.Append("<input type='hidden' id='" + WebUtility.HtmlEncode(kvp.Key) + "' name='" + WebUtility.HtmlEncode(kvp.Key) + "' value='" + WebUtility.HtmlEncode(kvp.Value) + "'/><br/>");
            }
            formText.Append("</form>");

            return formText.ToString();
        }

        public string GetPeoplesBankPaymentForm(PaymentDetails orderDetails, PaymentGateway GatewayInfo, string apiurl, Partner partner)
        {
            if (orderDetails == null || GatewayInfo == null)
            {
                // Handle null parameters
                return string.Empty;
            }
            //string SECRET_KEY = "d6ef666014be4cda9421ac41e51048d36527af1800914fd4ae371d43907ec101082cd843f0c34450a0b9299883dbf055af7bf4112ced4e3497024368698e7ba3394a8c3761a4455a9d35b61231b810e90c10bc5a7e1042c597b80c0ea02bdbe0291e061a2d7448648aedfb326a305a9b391469a697ce4b7aae5c56431eda3350";
            //string access_key = "47e0d13063583e179ee8ecf126cdb878";
            //string profile_id = "E6719FA3-6E97-41FA-9935-EBFE3DC5B466";
            string url = apiurl;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["transaction_uuid"] = orderDetails.PaymentDetailId.ToString();
            parameters["access_key"] = GatewayInfo.AccessKey;
            parameters["profile_id"] = GatewayInfo.ProfileID;
            parameters["signed_field_names"] = "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,currency";
            parameters["unsigned_field_names"] = "auth_trans_ref_no,bill_to_forename,bill_to_surname,bill_to_address_line1,bill_to_address_city,bill_to_address_country,bill_to_email";
            parameters["signed_date_time"] = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            parameters["locale"] = "en";
            parameters["transaction_type"] = "sale";//orderDetails.Description.ToString();
            parameters["reference_number"] = orderDetails.PaymentDetailId.ToString();
            parameters["auth_trans_ref_no"] = orderDetails.PaymentDetailId.ToString();
            parameters["amount"] = orderDetails.TotalAmount.ToString();
            parameters["currency"] = "LKR";
            if (partner.Email != null &&  IsValidEmail(partner.Email.ToString()))
            {
                parameters["bill_to_email"] = partner.Email.ToString();
            }
            else
            {
                parameters["bill_to_email"] = "nomail@cat2020.lk";
            }
            //parameters["bill_to_email"] = orderDetails.PartnerEmail.ToString();
            parameters["bill_to_forename"] = partner.Name.ToString();
            //parameters["bill_to_forename"] = orderDetails.PartnerName.ToString();
            parameters["bill_to_surname"] = partner.Name.ToString();
            //parameters["bill_to_surname"] = orderDetails.PartnerName.ToString();
            parameters["bill_to_address_line1"] = partner.Street1.ToString() + ", " + partner.Street2.ToString();
            if (string.IsNullOrEmpty(partner.City))
            {
                parameters["bill_to_address_city"] = "CAT20 Automation System";
            }
            else
            {
                parameters["bill_to_address_city"] = partner.City;
            }
            parameters["bill_to_address_country"] = "LK";
            if (string.IsNullOrEmpty(partner.Zip))
            {
                parameters["bill_to_address_postal_code"] = "00000";
            }
            else
            {
                parameters["bill_to_address_postal_code"] = partner.Zip.ToString();
            }
            
            parameters["signature"] = Sign(parameters, GatewayInfo.SecretKey);

            //getPaymentDetailId.TransactionId = transactionIdtocreate;
            //var updatedPaymentDetail = await _onlinePaymentService.UpdatePaymentDetail(getPaymentDetailId);

            StringBuilder formText = new StringBuilder();
            formText.Append("<script src=\"https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js\"></script>");
            formText.Append("<script>$(\"#payment_confirmation\").ready(function(){$(\"#payment_confirmation\").submit();});</script>");
            formText.Append("<form type=\"hidden\" id=\"payment_confirmation\" action=\"" + WebUtility.HtmlEncode(url) + "\" method=\"post\">");
            foreach (var kvp in parameters)
            {
                formText.Append("<input type='hidden' id='" + WebUtility.HtmlEncode(kvp.Key) + "' name='" + WebUtility.HtmlEncode(kvp.Key) + "' value='" + WebUtility.HtmlEncode(kvp.Value) + "'/><br/>");
            }
            formText.Append("</form>");

            return formText.ToString();

            //var jsonObject = new
            //{
            //    signature = parameters["signature"],
            //    formtext = formText.ToString()
            //};

            //string jsonResult = JsonConvert.SerializeObject(jsonObject);

            //return jsonResult;
        }

        static bool IsValidEmail(string email)
        {
            // Define a regular expression for email validation
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Create a Regex object
            Regex regex = new Regex(pattern);

            // Use the IsMatch method to test whether the string matches the regular expression
            return regex.IsMatch(email);
        }

        private string Sign(Dictionary<string, string> parameters, string SECRET_KEY)
        {
            return SignData(BuildDataToSign(parameters), SECRET_KEY);
        }

        private string SignData(string data, string secretKey)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            using (HMACSHA256 hmac = new HMACSHA256(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(dataBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        private string BuildDataToSign(Dictionary<string, string> parameters)
        {
            string signedFieldNames = parameters["signed_field_names"];
            string[] signedFields = signedFieldNames.Split(',');
            List<string> dataToSign = new List<string>();
            foreach (string field in signedFields)
            {
                dataToSign.Add(field + "=" + parameters[field]);
            }
            return CommaSeparate(dataToSign);
        }

        private string CommaSeparate(List<string> dataToSign)
        {
            return string.Join(",", dataToSign);
        }
}
}
