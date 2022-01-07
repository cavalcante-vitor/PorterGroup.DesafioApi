using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace PorterGroup.Desafio.WarmUp.Services
{
    [ExcludeFromCodeCoverage]
    internal class ServicesPreloader : BaseWarmer
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _provider;

        public ServicesPreloader(
            Action<string> logClass,
            IServiceCollection services,
            IServiceProvider provider)
            : base(logClass) =>
            (_services, _provider) = (services, provider);

        public override Task ExecuteAsync() => Task.Run(() =>
        {
            using var scope = _provider.CreateScope();

            foreach (var singleton in GetServices(_services))
            {
                var service = scope.ServiceProvider.GetServices(singleton);

                Log(string.Format("Pre-loading: {0}", service
                    .Select(objectFound => objectFound.GetType().Name)
                    .Aggregate(
                        new StringBuilder(),
                        (sb, name) => sb
                            .AppendLine(name))
                    .ToString()));
            }
        });

        private static IEnumerable<Type> GetServices(IServiceCollection services) =>
            services
                .Where(descriptor =>
                    descriptor.ImplementationType != typeof(ServicesPreloader) &&
                    !descriptor.ServiceType.ContainsGenericParameters)
                .Select(descriptor => descriptor.ServiceType)
                .Distinct();
    }
}
