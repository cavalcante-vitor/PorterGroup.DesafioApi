using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using PorterGroup.Desafio.Business.Factories;
using PorterGroup.Desafio.Business.Services;

namespace PorterGroup.Desafio.Business.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServicesExtension
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services) => services
            .AddScoped<ICondominioFactory, CondominioFactory>()
            .AddScoped<IBlocoFactory, BlocoFactory>()
            .AddScoped<IApartamentoFactory, ApartamentoFactory>()
            .AddScoped<IMoradorFactory, MoradorFactory>()
            .AddScoped<IFilterPagingFactory, FilterPagingFactory>()
            .AddScoped<IRequestFactory, RequestFactory>()
            .AddScoped<ICondominioService, CondominioService>()
            .AddScoped<IBlocoService, BlocoService>()
            .AddScoped<IApartamentoService, ApartamentoService>()
            .AddScoped<IMoradorService, MoradorService>();
    }
}
