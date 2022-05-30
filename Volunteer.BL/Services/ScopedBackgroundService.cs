using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Volunteer.BL.Services
{
    public sealed class ScopedBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ScopedBackgroundService> _logger;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;

        private string Schedule => "0 0 * * *";

        public ScopedBackgroundService(IServiceProvider serviceProvider,
            ILogger<ScopedBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = false });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(ScopedBackgroundService)} is running.");
            do
            {
                var now = DateTime.Now;
                var nextrun = _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    await DoWorkAsync(stoppingToken);
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                    _logger.LogInformation(
                $"{nextrun} next run");
                }
                await Task.Delay(5000, stoppingToken); //5 seconds delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(ScopedBackgroundService)} is working.");

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                /*IScopedProcessingService scopedProcessingService =
                    scope.ServiceProvider.GetRequiredService<IScopedProcessingService>();

                await scopedProcessingService.DoWorkAsync(stoppingToken);*/
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(ScopedBackgroundService)} is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
