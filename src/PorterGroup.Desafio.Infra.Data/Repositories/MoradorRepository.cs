using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using PorterGroup.Desafio.Business.Abstractions.Data;
using PorterGroup.Desafio.Business.Entities;
using PorterGroup.Desafio.Infra.Data.Repositories.Statements;

namespace PorterGroup.Desafio.Infra.Data.Repositories
{
    internal class MoradorRepository : IMoradorRepository
    {
        private const int MaxTimeOut = 60;
        private readonly IUnitOfWork _unitOfWork;

        public MoradorRepository(IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        public async Task<Morador> GetByIdAsync(string id)
        {
            return await _unitOfWork.Connection.QuerySingleAsync<Morador>(
                sql: MoradorStmt.SelectById,
                param: new
                {
                    Id = id,
                },
                _unitOfWork.Transaction,
                commandTimeout: MaxTimeOut,
                commandType: CommandType.Text);
        }

        public async Task<string> RegisterAsync(Morador morador)
        {
            await _unitOfWork.Connection.ExecuteAsync(
                sql: MoradorStmt.Insert,
                param: new
                {
                    Id = morador.Id,
                    Nome = morador.Nome,
                    DataNascimento = morador.DataNascimento,
                    Telefone = morador.Telefone,
                    Cpf = morador.Cpf,
                    Email = morador.Email,
                    ApartamentoId = morador.ApartamentoId,
                },
                _unitOfWork.Transaction,
                commandTimeout: MaxTimeOut,
                commandType: CommandType.Text);

            return morador.Id;
        }

        public async Task UpdateAsync(Morador morador)
        {
            await _unitOfWork.Connection.ExecuteAsync(
              sql: MoradorStmt.Update,
              param: new
              {
                  Nome = morador.Nome,
                  DataNascimento = morador.DataNascimento,
                  Telefone = morador.Telefone,
                  Cpf = morador.Cpf,
                  Email = morador.Email,
                  ApartamentoId = morador.ApartamentoId,
                  Id = morador.Id,
              },
              transaction: _unitOfWork.Transaction,
              commandTimeout: MaxTimeOut,
              commandType: CommandType.Text);
        }
    }
}
