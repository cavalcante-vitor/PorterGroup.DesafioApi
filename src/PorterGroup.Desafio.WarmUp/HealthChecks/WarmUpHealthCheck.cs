using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PorterGroup.Desafio.WarmUp.HealthChecks
{
    [ExcludeFromCodeCoverage]
    public class WarmUpHealthCheck : IHealthCheck
    {
        private const string Running = "Warm-up in process.";
        private const string Finished = "Warm-up finished.";

        private volatile bool _warmUpCompleted = false;

        public bool WarmUpCompleted
        {
            get => _warmUpCompleted;
            set => _warmUpCompleted = value;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default) => WarmUpCompleted
            ? Task.FromResult(
                HealthCheckResult.Healthy(Finished))
            : Task.FromResult(
                HealthCheckResult.Unhealthy(Running));
    }
}
