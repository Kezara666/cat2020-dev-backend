using CAT20.Core.Services.OnlinePayment;
using Org.BouncyCastle.Crypto.Prng.Drbg;

namespace CAT20.WebApi.Controllers.OnlinePayment;

public class RepeatingService: IHostedService, IDisposable
{

    // private IOnlinePaymentService _onlinePaymentService;
    
    // public RepeatingService(IOnlinePaymentService onlinePaymentService)
    // {
    //     _onlinePaymentService = onlinePaymentService;
    // }
    //
    // protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    // {
    //     while (!stoppingToken.IsCancellationRequested)
    //     {
    //
    //        _onlinePaymentService.paymentDetailSheduler();
    //         await Task.Delay(600000, stoppingToken);
    //     }
    // }
    
    private int executionCount = 0;

    private System.Threading.Timer _timerNotification;
    public IConfiguration _iconfiguration;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
    

    public RepeatingService(IServiceScopeFactory serviceScopeFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment env, IConfiguration iconfiguration)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _env = env;
        _iconfiguration = iconfiguration;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timerNotification = new Timer(RunJob, null, TimeSpan.Zero,
            TimeSpan.FromMinutes(5)); /*Set Interval time here*/


        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timerNotification?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timerNotification?.Dispose();
    }
    
    private async void RunJob(object state)
    {

        using (var scrope = _serviceScopeFactory.CreateScope())
        {
            try
            {
                //  var store = scrope.ServiceProvider.GetService<IStoreRepo>(); /* You can access any interface or service like this here*/
                //store.GetAll(); /* You can access any interface or service method like this here*/

                /*
                 Place your code here which you want to schedule on regular intervals
                 */
                var store = scrope.ServiceProvider.GetService<IOnlinePaymentService>();
                await store.paymentDetailSheduler();

            }


            catch (Exception ex)
            {

            }

            Interlocked.Increment(ref executionCount);
               
        }

    }
}