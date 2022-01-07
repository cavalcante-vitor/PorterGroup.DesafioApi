using System.Data;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Data.SqlClient;
using PorterGroup.Desafio.Infra.Data.Configurations;

namespace PorterGroup.Desafio.Infra.Data.Context
{
    [ExcludeFromCodeCoverage]
    internal class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;

        public ConnectionFactory(DatabaseConfig database) =>
            _connectionString = database.ConnectionString;

        public IDbConnection GetNewConnection() =>
            new SqlConnection(_connectionString);
    }
}
