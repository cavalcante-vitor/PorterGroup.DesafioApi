using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using PorterGroup.Desafio.Business.Abstractions.Data;
using PorterGroup.Desafio.Business.Entities;
using PorterGroup.Desafio.Infra.Data.Repositories.Statements;

namespace PorterGroup.Desafio.Infra.Data.Repositories
{
    internal class ApartamentoRepository : IApartamentoRepository
    {
        private const int MaxTimeOut = 60;
        private readonly IUnitOfWork _unitOfWork;

        public ApartamentoRepository(IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        public async Task<Apartamento> GetByIdAsync(string id)
        {
            return await _unitOfWork.Connection.QuerySingleAsync<Apartamento>(
                sql: ApartamentoStmt.SelectById,
                param: new
                {
                    Id = id,
                },
                _unitOfWork.Transaction,
                commandTimeout: MaxTimeOut,
                commandType: CommandType.Text);
        }

        public async Task<string> RegisterAsync(Apartamento apartamento)
        {
            await _unitOfWork.Connection.ExecuteAsync(
                sql: ApartamentoStmt.Insert,
                param: new
                {
                    Id = apartamento.Id,
                    Numero = apartamento.Numero,
                    Andar = apartamento.Andar,
                    BlocoId = apartamento.BlocoId,
                },
                _unitOfWork.Transaction,
                commandTimeout: MaxTimeOut,
                commandType: CommandType.Text);

            return apartamento.Id;
        }

        public async Task UpdateAsync(Apartamento apartamento)
        {
            await _unitOfWork.Connection.ExecuteAsync(
              sql: ApartamentoStmt.Update,
              param: new
              {
                  Numero = apartamento.Numero,
                  Andar = apartamento.Andar,
                  BlocoId = apartamento.BlocoId,
                  Id = apartamento.Id,
              },
              transaction: _unitOfWork.Transaction,
              commandTimeout: MaxTimeOut,
              commandType: CommandType.Text);
        }
    }
}
