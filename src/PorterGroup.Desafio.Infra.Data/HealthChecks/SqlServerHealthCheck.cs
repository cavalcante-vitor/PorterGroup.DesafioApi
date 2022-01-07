using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PorterGroup.Desafio.Business.Abstractions.Data;

namespace PorterGroup.Desafio.Infra.Data.HealthChecks
{
    public class SqlServerHealthCheck : IHealthCheck
    {
        private const string HealthCheckQuery = "SELECT 1";
        private readonly IUnitOfWork _unitOfWork;

        public SqlServerHealthCheck(IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _ = await _unitOfWork.Connection
                    .QuerySingleAsync<int>(HealthCheckQuery);
                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(
                     status: context.Registration.FailureStatus,
                     description: ex.Message);
            }
        }
    }
}
