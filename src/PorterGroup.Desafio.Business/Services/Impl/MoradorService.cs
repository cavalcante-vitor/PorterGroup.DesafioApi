using System.Threading.Tasks;
using PorterGroup.Desafio.Business.Abstractions.Data;
using PorterGroup.Desafio.Business.Entities;
using PorterGroup.Desafio.Business.Factories;

namespace PorterGroup.Desafio.Business.Services
{
    internal class MoradorService : IMoradorService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMoradorRepository _moradorRepository;
        private readonly IMoradorFactory _moradorFactory;

        public MoradorService(
            IUnitOfWork uow,
            IMoradorRepository moradorRepository,
            IMoradorFactory moradorFactory)
        {
            _uow = uow;
            _moradorRepository = moradorRepository;
            _moradorFactory = moradorFactory;
        }

        public async Task<Morador> GetByIdAsync(string id)
        {
            _uow.BeginTransaction();
            var morador = await _moradorRepository.GetByIdAsync(id);
            _uow.Commit();

            return morador;
        }

        public async Task<string> CreateAsync(Morador morador)
        {
            _uow.BeginTransaction();
            var id = await _moradorRepository.RegisterAsync(_moradorFactory.FromCreate(morador));
            _uow.Commit();

            return id;
        }

        public async Task PutAsync(Morador morador)
        {
            _uow.BeginTransaction();
            await _moradorRepository.UpdateAsync(morador);
            _uow.Commit();
        }
    }
}
