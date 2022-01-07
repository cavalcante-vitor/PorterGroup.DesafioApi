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
    internal class ExternalHttpCallWarmer : BaseWarmer
    {
        private const int MaxExecutionTimes = 15;
        private const string ExecuteWarming = "Calling external HTTP to warmup: {0}";
        private const string WarmingUpError = "Error while calling external http to warmup service {0}: [{1}]";
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _provider;

        public ExternalHttpCallWarmer(
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
                var service = (IHttpCallWarmerCommand)scope.ServiceProvider.GetService(warmer);
                Log(string.Format(ExecuteWarming, service.GetType().Name));

                await ExecuteWarmingUp(service);
            }
        }

        public Task ExecuteWarmingUp(IHttpCallWarmerCommand warmer) => Task.Run(async () =>
        {
            try
            {
                if (warmer is null)
                {
                    await Task.CompletedTask;
                    return;
                }

                Task.WaitAll(Enumerable.Range(1, MaxExecutionTimes)
                                .Aggregate(new List<Task>(MaxExecutionTimes), (calls, _) =>
                                {
                                    calls.Add(warmer.ExecuteAsync());
                                    return calls;
                                }).ToArray());

                await Task.CompletedTask;
            }
            catch (Exception exception)
            {
                Log(string.Format(
                    WarmingUpError,
                    warmer.GetType().Name,
                    exception.GetType().Name));
                await Task.CompletedTask;
            }
        });

        private static IEnumerable<Type> GetServices(IServiceCollection services) =>
            services
                .Where(descriptor => descriptor
                    .ImplementationType?
                    .GetInterfaces()
                    .Contains(typeof(IHttpCallWarmerCommand)) == true)
                .Select(descriptor => descriptor.ServiceType)
                .Distinct();
    }
}
