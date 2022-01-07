using System.Threading.Tasks;
using PorterGroup.Desafio.Business.Abstractions.Data;
using PorterGroup.Desafio.Business.Entities;
using PorterGroup.Desafio.Business.Factories;

namespace PorterGroup.Desafio.Business.Services
{
    internal class ApartamentoService : IApartamentoService
    {
        private readonly IUnitOfWork _uow;
        private readonly IApartamentoRepository _apartamentoRepository;
        private readonly IApartamentoFactory _apartamentoFactory;

        public ApartamentoService(
            IUnitOfWork uow,
            IApartamentoRepository apartamentoRepository,
            IApartamentoFactory apartamentoFactory)
        {
            _uow = uow;
            _apartamentoRepository = apartamentoRepository;
            _apartamentoFactory = apartamentoFactory;
        }

        public async Task<Apartamento> GetByIdAsync(string id)
        {
            _uow.BeginTransaction();
            var apartamento = await _apartamentoRepository.GetByIdAsync(id);
            _uow.Commit();

            return apartamento;
        }

        public async Task<string> CreateAsync(Apartamento apartamento)
        {
            _uow.BeginTransaction();
            var id = await _apartamentoRepository.RegisterAsync(_apartamentoFactory.FromCreate(apartamento));
            _uow.Commit();

            return id;
        }

        public async Task PutAsync(Apartamento apartamento)
        {
            _uow.BeginTransaction();
            await _apartamentoRepository.UpdateAsync(apartamento);
            _uow.Commit();
        }
    }
}
