using System.Net.Mail;
using System.Net;
using CAT20.Core.Services.Control;
using CAT20.Core.Repositories.Control;
using CAT20.Services.Control;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;

namespace CAT20.WebApi.Email
{
    public class EmailService
    {
        private IEmailConfigurationService _configurationService;
        private IEmailOutBoxService _emailOutBoxService;
        public EmailService(IEmailConfigurationService configurationService, IEmailOutBoxService emailOutBoxService)
        {
            _configurationService = configurationService;
            _emailOutBoxService = emailOutBoxService;
        }
        public async Task sendMail()
        {
            MailAddress sender = null;
            EmailConfiguration emailSettings = null;
            try
            {
                emailSettings = await _configurationService.GetEmailConfigurationById(1);
            }
            catch (Exception e)
            {
                //return;
            }
            foreach (var email in await _emailOutBoxService.GetAllPendingAsync())
            {
                sender = new MailAddress(emailSettings.SystemMailAddress, emailSettings.SystemMailAddress);
                using (var mailServer = new SmtpClient(emailSettings.SystemMailAddress, (emailSettings.SystemMailPORT != null ? emailSettings.SystemMailPORT.Value : 0)))
                {
                    try
                    {
                        var mailMessaage = new MailMessage();
                        mailMessaage.To.Add(email.Recipient);
                        mailMessaage.Subject = email.Subject;
                        mailMessaage.Body = email.MailContent;
                        mailMessaage.IsBodyHtml = email.IsBodyHtml;
                        mailMessaage.Sender = sender;
                        mailMessaage.From = sender;

                        mailServer.Host = emailSettings.SystemMailSMTP;
                        mailServer.Port = (emailSettings.SystemMailPORT != null ? emailSettings.SystemMailPORT.Value : 0);
                        mailServer.EnableSsl = true;
                        mailServer.UseDefaultCredentials = false;
                        mailServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                        mailServer.Credentials = new NetworkCredential(emailSettings.SystemMailAddress, emailSettings.SystemMailPassword);
                        mailServer.Send(mailMessaage);

                        email.EmailStatus = EmailStatus.Sent;
                       await _emailOutBoxService.CreateEmailOutBox(email);
                    }
                    catch (Exception ex)
                    {
                        var error = ex.Message;
                        if (email.EmailSendAttempts <= EmailSendAttempts.Attempt_3)
                            email.EmailSendAttempts = email.EmailSendAttempts + 1;
                        else
                            email.EmailStatus = EmailStatus.Failed;
                      await  _emailOutBoxService.CreateEmailOutBox(email);
                    }
                }
            }
        }
    }
}
