using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PorterGroup.Desafio.Infra.Logger.Logging;
using Serilog;

namespace PorterGroup.Desafio.Infra.Logger.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServicesExtension
    {
        public static IServiceCollection AddLogger(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services
                .AddScoped<ILogWriter, LogWriter>()
                .ConfigLogger(configuration);

        private static IServiceCollection ConfigLogger(
            this IServiceCollection services,
            IConfiguration configuration) => services.AddSingleton(_ =>
            {
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .WriteTo.Console(outputTemplate: "{Message:lj}{NewLine}")
                    .CreateLogger();

                return Log.Logger;
            });
    }
}
