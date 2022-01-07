using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PorterGroup.Desafio.WarmUp.Abtraction;

namespace PorterGroup.Desafio.WarmUp.Services
{
    [ExcludeFromCodeCoverage]
    internal class WarmerExecutor : BaseWarmer
    {
        private const string ExecuteWarming = "Execute warming up: {0}";
        private const string WarmingUpError = "Error while trying to warmup service {0}: [{1}] - {2}";
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _provider;

        public WarmerExecutor(
            Action<string> logClass,
            IServiceCollection services,
            IServiceProvider provider)
            : base(logClass) =>
            (_services, _provider) = (services, provider);

        public override async Task ExecuteAsync()
        {
            using var scope = _provider.CreateScope();

            foreach (var warmer in GetServices(_services))
            {
                var service = (IWarmerCommand)scope.ServiceProvider.GetService(warmer);
                Log(string.Format(ExecuteWarming, service.GetType().Name));

                await ExecuteWarmingUpAsync(service);
            }
        }

        public async Task ExecuteWarmingUpAsync(IWarmerCommand warmer)
        {
            try
            {
                if (warmer is null)
                {
                    return;
                }

                await warmer.ExecuteAsync();
            }
            catch (Exception exception)
            {
                Log(string.Format(
                    WarmingUpError,
                    warmer.GetType().FullName,
                    exception.GetType().FullName,
                    exception.Message));
            }
        }

        private static IEnumerable<Type> GetServices(IServiceCollection services) =>
            services
                .Where(descriptor => descriptor
                    .ImplementationType?
                    .GetInterfaces()
                    .Contains(typeof(IWarmerCommand)) == true)
                .Select(descriptor => descriptor.ServiceType)
                .Distinct();
    }
}
