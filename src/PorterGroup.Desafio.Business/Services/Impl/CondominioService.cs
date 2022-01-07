using System.Threading.Tasks;
using PorterGroup.Desafio.Business.Abstractions.Data;
using PorterGroup.Desafio.Business.Entities;
using PorterGroup.Desafio.Business.Factories;

namespace PorterGroup.Desafio.Business.Services
{
    internal class CondominioService : ICondominioService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICondominioRepository _condominioRepository;
        private readonly ICondominioFactory _condominioFactory;

        public CondominioService(
            IUnitOfWork uow,
            ICondominioRepository condominioRepository,
            ICondominioFactory condominioFactory)
        {
            _uow = uow;
            _condominioRepository = condominioRepository;
            _condominioFactory = condominioFactory;
        }

        public async Task<Condominio> GetByIdAsync(string id)
        {
            _uow.BeginTransaction();
            var condominio = await _condominioRepository.GetByIdAsync(id);
            _uow.Commit();

            return condominio;
        }

        public async Task<string> CreateAsync(Condominio condominio)
        {
            _uow.BeginTransaction();
            var id = await _condominioRepository.RegisterAsync(_condominioFactory.FromCreate(condominio));
            _uow.Commit();

            return id;
        }

        public async Task PutAsync(Condominio condominio)
        {
            _uow.BeginTransaction();
            await _condominioRepository.UpdateAsync(condominio);
            _uow.Commit();
        }
    }
}
