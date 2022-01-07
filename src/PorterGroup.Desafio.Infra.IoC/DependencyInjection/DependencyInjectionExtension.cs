using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PorterGroup.Desafio.Business.Extensions;
using PorterGroup.Desafio.Infra.Data.Extensions;
using PorterGroup.Desafio.Infra.IoC.Extensions;
using PorterGroup.Desafio.Infra.Logger.Extensions;

namespace PorterGroup.Desafio.Infra.IoC.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddIoc(
           this IServiceCollection services,
           IConfiguration configuration) => services
            .AddBusiness()
            .AddInfraData(configuration)
            .AddLogger(configuration)
            .AddHealthChecks()
            .AddChecks(configuration);
    }
}
