using System.Data;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PorterGroup.Desafio.Business.Abstractions.Data;
using PorterGroup.Desafio.Infra.Data.Configurations;
using PorterGroup.Desafio.Infra.Data.Context;
using PorterGroup.Desafio.Infra.Data.Repositories;

namespace PorterGroup.Desafio.Infra.Data.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServicesExtension
    {
        public static IServiceCollection AddInfraData(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services
                .AddDatabaseConfig(configuration)
                .AddTransient<IDbConnection>(provider =>
                {
                    var connectionString = provider.GetRequiredService<DatabaseConfig>().ConnectionString;
                    return new SqlConnection(connectionString);
                })
                .AddScoped<IConnectionFactory, ConnectionFactory>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<ICondominioRepository, CondominioRepository>()
                .AddScoped<IBlocoRepository, BlocoRepository>()
                .AddScoped<IApartamentoRepository, ApartamentoRepository>()
                .AddScoped<IMoradorRepository, MoradorRepository>();

        private static IServiceCollection AddDatabaseConfig(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddSingleton(_ =>
            {
                var connectionString = configuration.GetConnectionString("Default");
                var sqlConnection = new SqlConnectionStringBuilder(connectionString)
                {
                    MultipleActiveResultSets = true,
                    PoolBlockingPeriod = PoolBlockingPeriod.NeverBlock,
                    CommandTimeout = 1,
                    ConnectRetryCount = 0,
                };

                return new DatabaseConfig()
                {
                    ConnectionString = sqlConnection.ConnectionString,
                };
            });
    }
}
