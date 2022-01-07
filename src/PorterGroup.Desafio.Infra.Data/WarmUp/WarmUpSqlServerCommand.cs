using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using PorterGroup.Desafio.Business.Abstractions.Data;
using PorterGroup.Desafio.WarmUp.Abtraction;

namespace PorterGroup.Desafio.Infra.Data.WarmUp
{
    [ExcludeFromCodeCoverage]
    internal class WarmUpSqlServerCommand : IWarmerCommand
    {
        private const string WarmUpQuery = "SELECT 1";
        private const string WarmupMessage = "Warming up database";
        private const string WarmupExceptionErrorMessage = "Warming up database failed";
        private const int TotalExecutionCount = 3;

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<WarmUpSqlServerCommand> _logger;

        public WarmUpSqlServerCommand(
            IUnitOfWork unitOfWork,
            ILogger<WarmUpSqlServerCommand> logger) =>
            (_unitOfWork, _logger) = (unitOfWork, logger);

        public async Task ExecuteAsync()
        {
            try
            {
                for (var currentExecutionCount = 1; currentExecutionCount < TotalExecutionCount; currentExecutionCount++)
                {
                    _logger.LogInformation(WarmupMessage);
                    _ = await _unitOfWork.Connection
                        .QuerySingleAsync<int>(WarmUpQuery);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, WarmupExceptionErrorMessage);
            }
        }
    }
}
