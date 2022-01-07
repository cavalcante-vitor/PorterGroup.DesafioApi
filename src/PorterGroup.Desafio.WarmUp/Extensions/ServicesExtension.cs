using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using PorterGroup.Desafio.WarmUp.HealthChecks;
using PorterGroup.Desafio.WarmUp.HostedServices;
using PorterGroup.Desafio.WarmUp.Services;

namespace PorterGroup.Desafio.WarmUp.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServicesExtension
    {
        public static IServiceCollection AddWarmUp(this IServiceCollection services) =>
            services
                .AddHostedService<WarmUpHostedService>()
                .AddSingleton<WarmUpHealthCheck>()
                .AddWarmUpper();

        private static IServiceCollection AddWarmUpper(this IServiceCollection services) =>
            services
                .AddTransient(provider => new ServicesPreloader(
                    logMessage => Console.WriteLine(logMessage),
                    services,
                    provider))
                .AddTransient(provider => new WarmerExecutor(
                    logMessage => Console.WriteLine(logMessage),
                    services,
                    provider))
                .AddTransient(provider => new ExternalHttpCallWarmer(
                    logMessage => Console.WriteLine(logMessage),
                    services,
                    provider));
    }
}
