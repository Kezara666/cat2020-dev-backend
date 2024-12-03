using System.Net;
using CAT20.Core.Services.Control;
using CAT20.Core.Repositories.Control;
using CAT20.Services.Control;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using System.Text.Json;
using System;
using System.Net.Http;
using System.Text;
using CAT20.WebApi.Resources.Control;
using CAT20.WebApi.Resources.OnlinePayment;
using ServiceReference1;

namespace CAT20.WebApi.SMS
{
    public class SMSService 
    {
        private ISMSConfigurationService _configurationService;
        private ISMSOutBoxService _smsOutBoxService;
        public SMSService(ISMSConfigurationService configurationService, ISMSOutBoxService smsOutBoxService)
        {
            _configurationService = configurationService;
            _smsOutBoxService = smsOutBoxService;
        }
        public async Task sendSMS()
        {
            foreach (var sms in await _smsOutBoxService.GetAllPendingAsync())
            {
                SMSConfiguration smsSettings = null;
                try
                {
                    if (sms.Module == "User")
                    {
                        smsSettings = await _configurationService.GetSMSConfigurationBySabhaId(sms.SabhaID);
                        if(smsSettings==null)
                        { 
                        smsSettings = await _configurationService.GetSMSConfigurationBySabhaId(1);
                        }
                    } else if (sms.Module == "OnlineUser")
                    {
                        smsSettings = await _configurationService.GetSMSConfigurationBySabhaId(sms.SabhaID);
                    }
                    else
                    {
                        smsSettings = await _configurationService.GetSMSConfigurationBySabhaId(sms.SabhaID);
                    }
                    //smsSettings = await _configurationService.GetSMSConfigurationBySabhaId(sms.SabhaID);

                    if (smsSettings != null)
                    {

                        if(smsSettings.Provider=="Sendlk")
                        { 
                        using (var httpClient = new HttpClient())
                        {
                            // Disable SSL certificate validation (similar to CURLOPT_SSL_VERIFYHOST and CURLOPT_SSL_VERIFYPEER set to 0)
                            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                            var msgdata = new
                            {
                                recipient = sms.Recipient,
                                sender_id = sms.SenderId,
                                message = sms.SMSContent
                            };

                            var content = new StringContent(JsonSerializer.Serialize(msgdata), Encoding.UTF8, "application/json");

                            // Set the authorization header and accept header
                            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {smsSettings.ApiToken.Trim()}");
                            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                            // URL-encode the API endpoint
                            var encodedApiEndpoint = Uri.EscapeUriString(smsSettings.ApiEndPoint.Trim());

                            // Send the POST request to the send.lk API
                            var response = await httpClient.PostAsync(encodedApiEndpoint, content);

                            if (response.IsSuccessStatusCode)
                            {
                                sms.SMSStatus = SMSStatus.Sent;
                                await _smsOutBoxService.CreateSMSOutBox(sms);
                                //return await response.Content.ReadAsStringAsync();
                            }
                            else
                            {
                                //var error = ex.Message;
                                if (sms.SMSSendAttempts <= SMSSendAttempts.Attempt_3)
                                    sms.SMSSendAttempts = sms.SMSSendAttempts + 1;
                                else
                                    sms.SMSStatus = SMSStatus.Failed;
                                await _smsOutBoxService.CreateSMSOutBox(sms);
                                // Handle the error response here
                                //throw new Exception($"Failed to send SMS. Status code: {response.StatusCode}");
                            }
                        }
                        }
                        else if(smsSettings.Provider == "Mobitel")
                        {
                            var response=await SendMobitelSMSEnterprise(smsSettings, sms);

                            if (response == 200)
                            {
                                sms.SMSStatus = SMSStatus.Sent;
                                await _smsOutBoxService.CreateSMSOutBox(sms);
                            }
                            else
                            {
                                if (sms.SMSSendAttempts <= SMSSendAttempts.Attempt_3)
                                    sms.SMSSendAttempts = sms.SMSSendAttempts + 1;
                                else
                                    sms.SMSStatus = SMSStatus.Failed;
                                await _smsOutBoxService.CreateSMSOutBox(sms);
                            }
                        }
                        else //if not assigned to any provider
                        {
                            sms.Note = "Missing Configurations For SabhaId=" + sms.SabhaID;
                            await _smsOutBoxService.CreateSMSOutBox(sms);
                        }
                    }
                    else
                    {
                        sms.Note = "Missing Configurations For SabhaId=" + sms.SabhaID;
                        await _smsOutBoxService.CreateSMSOutBox(sms);
                    }

                }
                catch (Exception ex)
                {
                    //var error = ex.Message;
                    //if (sms.SMSSendAttempts <= SMSSendAttempts.Attempt_3)
                    //    sms.SMSSendAttempts = sms.SMSSendAttempts + 1;
                    //else
                    //    sms.SMSStatus = SMSStatus.Failed;
                    //await _smsOutBoxService.CreateSMSOutBox(sms);
                }

            }
        }

        public async Task<int> SendMobitelSMSEnterprise(SMSConfiguration smsconfig, SMSOutBox sms)
        {
            try
            {
                // Create user
                user user1 = new user
                {
                    username = smsconfig.Username,
                    password = smsconfig.ApiToken
                };

                // Create EnterpriseSMSWSClient
                EnterpriseSMSWSClient client = new EnterpriseSMSWSClient();

                // Test service
                Console.WriteLine(client.serviceTest(user1));

                // Create session
                session session1 = client.createSession(user1);

                // Check session
                bool isSession = client.isSession(session1, out string status);
                Console.WriteLine(isSession);

                Console.WriteLine(session1.isActive);

                // Create list of recipients
                var myList = new List<string>();

                // Add items to the list
                myList.Add(sms.Recipient);

                // Create and send SMS
                smsMessage messages = new smsMessage
                {
                    message = sms.SMSContent,
                    sender = smsconfig.SenderId,
                    recipients = myList.ToArray(),
                    messageType = 1
                };
                var response = client.sendMessages(session1, messages);
                Console.WriteLine(response);

                client.closeSession(session1);

                return await Task.FromResult(response); // Return response status code
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return await Task.FromResult(-1); // Return -1 for error
            }
        }


        public async Task createSMS(SMSResource SMSResource)
        {
            #region Create SMS Record
            if (SMSResource != null)
            {
                if (SMSResource.MobileNo.Length == 10)
                {
                    if (SMSResource.Text.Length > 1)
                    {
                        try
                        {
                            SMSConfiguration smsSettings = new SMSConfiguration();

                            if (SMSResource.Module == "User")
                            {
                                smsSettings = await _configurationService.GetSMSConfigurationBySabhaId(SMSResource.SabhaId);
                                if(smsSettings==null)
                                { 
                                smsSettings = await _configurationService.GetSMSConfigurationBySabhaId(1);
                                }
                            }
                            else
                            {
                                smsSettings = await _configurationService.GetSMSConfigurationBySabhaId(SMSResource.SabhaId);
                            }
                                if (smsSettings != null)
                                {
                                    await _smsOutBoxService.CreateSMSOutBox(new Core.Models.Control.SMSOutBox
                                    {
                                        Module = SMSResource.Module,
                                        SabhaID = SMSResource.SabhaId,
                                        Recipient = SMSResource.MobileNo,
                                        SMSContent = SMSResource.Text,
                                        Subject = SMSResource.Subject,
                                        //CreatedBy = SMSResource.CreatedBy,
                                        SMSSendAttempts = 0,
                                        SMSStatus = Core.Models.Enums.SMSStatus.Pending,
                                        Note = string.Empty,
                                        SenderId = smsSettings.SenderId,
                                    });
                                }
                                else
                                {
                                    await _smsOutBoxService.CreateSMSOutBox(new Core.Models.Control.SMSOutBox
                                    {
                                        Module = SMSResource.Module,
                                        SabhaID = SMSResource.SabhaId,
                                        Recipient = SMSResource.MobileNo,
                                        SMSContent = SMSResource.Text,
                                        Subject = SMSResource.Subject,
                                        //CreatedBy = SMSResource.CreatedBy,
                                        SMSSendAttempts = 0,
                                        SMSStatus = Core.Models.Enums.SMSStatus.Failed,
                                        Note = "Missing Config For SabhaId=" + SMSResource.SabhaId.ToString(),
                                        SenderId = "-",
                                    });
                                }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("SMS Text Should Be at Least 2 letters");
                    }
                }
                else
                {
                    Console.WriteLine("Mobile Number Should Contain 10 digits");
                }
            }
            #endregion
            await sendSMS();
        }

        public async Task createSMS(PaymentSMSResource paymentSmsResource)
        {
            #region Create SMS Record
            if (paymentSmsResource != null)
            {
                if (paymentSmsResource.MobileNo.Length >= 10)
                {
                    if (paymentSmsResource.Text.Length > 1)
                    {
                        try
                        {
                            SMSConfiguration smsSettings = new SMSConfiguration();

                            if (paymentSmsResource.Module == "User")
                            {
                                smsSettings = await _configurationService.GetSMSConfigurationBySabhaId(paymentSmsResource.SabhaId);
                                if(smsSettings==null)
                                { 
                                smsSettings = await _configurationService.GetSMSConfigurationBySabhaId(1);
                                }
                            }
                            else
                            {
                                smsSettings = await _configurationService.GetSMSConfigurationBySabhaId(paymentSmsResource.SabhaId);
                            }
                                if (smsSettings != null)
                                {
                                    await _smsOutBoxService.CreateSMSOutBox(new Core.Models.Control.SMSOutBox
                                    {
                                        Module = paymentSmsResource.Module,
                                        SabhaID = paymentSmsResource.SabhaId,
                                        Recipient = paymentSmsResource.MobileNo,
                                        SMSContent = paymentSmsResource.Text,
                                        Subject = paymentSmsResource.Subject,
                                        //CreatedBy = SMSResource.CreatedBy,
                                        SMSSendAttempts = 0,
                                        SMSStatus = Core.Models.Enums.SMSStatus.Pending,
                                        Note = string.Empty,
                                        SenderId = smsSettings.SenderId,
                                    });
                                }
                                else
                                {
                                    await _smsOutBoxService.CreateSMSOutBox(new Core.Models.Control.SMSOutBox
                                    {
                                        Module = paymentSmsResource.Module,
                                        SabhaID = paymentSmsResource.SabhaId,
                                        Recipient = paymentSmsResource.MobileNo,
                                        SMSContent = paymentSmsResource.Text,
                                        Subject = paymentSmsResource.Subject,
                                        //CreatedBy = SMSResource.CreatedBy,
                                        SMSSendAttempts = 0,
                                        SMSStatus = Core.Models.Enums.SMSStatus.Failed,
                                        Note = "Missing Config For SabhaId=" + paymentSmsResource.SabhaId.ToString(),
                                        SenderId = "-",
                                    });
                                }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("SMS Text Should Be at Least 2 letters");
                    }
                }
                else
                {
                    Console.WriteLine("Mobile Number Should Contain 10 digits");
                }
            }
            #endregion
            await sendSMS();
        }
        
        
    }
}
