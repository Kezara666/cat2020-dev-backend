using CAT20.Core.Services.Control;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CAT20.WebApi.Email
{
    public class EmailSenderService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly IEmailConfigurationService _emailConfigurationService;
        private readonly IEmailOutBoxService _emailOutBoxService;

        public EmailSenderService(IServiceProvider serviceProvider, IEmailConfigurationService emailConfigurationService, IEmailOutBoxService emailOutBoxService)
        {
            _serviceProvider = serviceProvider;
            _emailConfigurationService = emailConfigurationService;
            _emailOutBoxService = emailOutBoxService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();
                        await emailService.sendMail();
                    }

                    // Wait for 5 minutes before the next execution
                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                }
                catch (Exception ex)
                {
                    // Log or handle exceptions as needed
                }
            }
        }
    }
}
