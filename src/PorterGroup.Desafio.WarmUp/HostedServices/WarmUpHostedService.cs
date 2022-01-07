using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PorterGroup.Desafio.WarmUp.HealthChecks;
using PorterGroup.Desafio.WarmUp.Services;

namespace PorterGroup.Desafio.WarmUp.HostedServices
{
    [ExcludeFromCodeCoverage]
    internal class WarmUpHostedService : BackgroundService
    {
        private readonly IImmutableList<BaseWarmer> _commands;
        private readonly ILogger<WarmUpHostedService> _logger;
        private readonly WarmUpHealthCheck _warmUpHealthCheck;

        public WarmUpHostedService(
            ILogger<WarmUpHostedService> logger,
            ServicesPreloader serviceWarmer,
            WarmerExecutor warmerExecutor,
            WarmUpHealthCheck warmUpHealthCheck,
            ExternalHttpCallWarmer warmerLoop)
        {
            _commands = ImmutableList.Create<BaseWarmer>(
                serviceWarmer,
                warmerExecutor,
                warmerLoop);

            _logger = logger;
            _warmUpHealthCheck = warmUpHealthCheck;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var executionChronometer = new Stopwatch();
            executionChronometer.Start();
            _logger.LogInformation("Starting Warm-up.");
            Task.Run(
                async () =>
                {
                    foreach (var command in _commands)
                    {
                        await command.ExecuteAsync();
                    }

                    executionChronometer.Stop();
                    _logger.LogInformation("Warmup finished.");
                    _logger.LogInformation("Warmup took {0} seconds.", executionChronometer.Elapsed.Seconds);
                    _warmUpHealthCheck.WarmUpCompleted = true;
                }, stoppingToken);

            return Task.CompletedTask;
        }
    }
}
