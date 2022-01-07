using System.Data;
using System.Threading.Tasks;
using Dapper;
using PorterGroup.Desafio.Business.Abstractions.Data;
using PorterGroup.Desafio.Business.Entities;
using PorterGroup.Desafio.Infra.Data.Repositories.Statements;

namespace PorterGroup.Desafio.Infra.Data.Repositories
{
    internal class BlocoRepository : IBlocoRepository
    {
        private const int MaxTimeOut = 60;
        private readonly IUnitOfWork _unitOfWork;

        public BlocoRepository(IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        public async Task<Bloco> GetByIdAsync(string id)
        {
            return await _unitOfWork.Connection.QuerySingleAsync<Bloco>(
                sql: BlocoStmt.SelectById,
                param: new
                {
                    Id = id,
                },
                _unitOfWork.Transaction,
                commandTimeout: MaxTimeOut,
                commandType: CommandType.Text);
        }

        public async Task<string> RegisterAsync(Bloco bloco)
        {
            await _unitOfWork.Connection.ExecuteAsync(
                sql: BlocoStmt.Insert,
                param: new
                {
                    Id = bloco.Id,
                    Nome = bloco.Nome,
                    CondominioId = bloco.CondominioId,
                },
                _unitOfWork.Transaction,
                commandTimeout: MaxTimeOut,
                commandType: CommandType.Text);

            return bloco.Id;
        }

        public async Task UpdateAsync(Bloco bloco)
        {
            await _unitOfWork.Connection.ExecuteAsync(
              sql: BlocoStmt.Update,
              param: new
              {
                  Nome = bloco.Nome,
                  CondominioId = bloco.CondominioId,
                  Id = bloco.Id,
              },
              transaction: _unitOfWork.Transaction,
              commandTimeout: MaxTimeOut,
              commandType: CommandType.Text);
        }
    }
}
