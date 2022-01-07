using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PorterGroup.Desafio.Infra.Data.HealthChecks;
using PorterGroup.Desafio.WarmUp.HealthChecks;

namespace PorterGroup.Desafio.Infra.IoC.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class HealthChecksExtensions
    {
        private const string HealthCheckTimeout = "HealthCheckTimeoutMilliseconds";
        private const string HealthCheckLiveness = "live";
        private const string HealthCheckReadiness = "ready";

        public static IServiceCollection AddChecks(
            this IHealthChecksBuilder healthChecksBuilder,
            IConfiguration configuration)
        {
            var timeoutMs = configuration.GetValue<int?>(HealthCheckTimeout, null);

            return healthChecksBuilder
                .AddCheck<WarmUpHealthCheck>(
                    name: "WarmUp",
                    failureStatus: HealthStatus.Degraded,
                    tags: new[] { HealthCheckLiveness, HealthCheckReadiness })
                .AddCheck<SqlServerHealthCheck>(
                    name: "SqlServer",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new[] { HealthCheckReadiness },
                    timeout: timeoutMs is null ? null : TimeSpan.FromMilliseconds(timeoutMs.Value))

                .Services;
        }
    }
}
