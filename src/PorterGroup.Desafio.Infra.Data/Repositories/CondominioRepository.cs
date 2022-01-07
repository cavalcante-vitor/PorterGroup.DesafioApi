using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using PorterGroup.Desafio.Business.Abstractions.Data;
using PorterGroup.Desafio.Business.Entities;
using PorterGroup.Desafio.Infra.Data.Repositories.Statements;

namespace PorterGroup.Desafio.Infra.Data.Repositories
{
    internal class CondominioRepository : ICondominioRepository
    {
        private const int MaxTimeOut = 60;
        private readonly IUnitOfWork _unitOfWork;

        public CondominioRepository(IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        public async Task<Condominio> GetByIdAsync(string id) =>
            await _unitOfWork.Connection.QuerySingleAsync<Condominio>(
                sql: CondominioStmt.SelectById,
                param: new
                {
                    Id = id,
                },
                transaction: _unitOfWork.Transaction,
                commandTimeout: MaxTimeOut,
                commandType: CommandType.Text);

        public async Task<string> RegisterAsync(Condominio condominio)
        {
            await _unitOfWork.Connection.ExecuteAsync(
                sql: CondominioStmt.Insert,
                param: new
                {
                    Id = condominio.Id,
                    Nome = condominio.Nome,
                    Telefone = condominio.Telefone,
                    EmailSindico = condominio.EmailSindico,
                },
                transaction: _unitOfWork.Transaction,
                commandTimeout: MaxTimeOut,
                commandType: CommandType.Text);

            return condominio.Id;
        }

        public async Task UpdateAsync(Condominio condominio)
        {
            await _unitOfWork.Connection.ExecuteAsync(
              sql: CondominioStmt.Update,
              param: new
              {
                  Nome = condominio.Nome,
                  Telefone = condominio.Telefone,
                  EmailSindico = condominio.EmailSindico,
                  Id = condominio.Id,
              },
              transaction: _unitOfWork.Transaction,
              commandTimeout: MaxTimeOut);
        }
    }
}
