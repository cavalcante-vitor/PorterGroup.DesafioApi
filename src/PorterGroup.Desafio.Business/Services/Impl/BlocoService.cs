using System.Threading.Tasks;
using PorterGroup.Desafio.Business.Abstractions.Data;
using PorterGroup.Desafio.Business.Entities;
using PorterGroup.Desafio.Business.Factories;

namespace PorterGroup.Desafio.Business.Services
{
    internal class BlocoService : IBlocoService
    {
        private readonly IUnitOfWork _uow;
        private readonly IBlocoRepository _blocoRepository;
        private readonly IBlocoFactory _blocoFactory;

        public BlocoService(
            IUnitOfWork uow,
            IBlocoRepository blocoRepository,
            IBlocoFactory blocoFactory)
        {
            _uow = uow;
            _blocoRepository = blocoRepository;
            _blocoFactory = blocoFactory;
        }

        public async Task<Bloco> GetByIdAsync(string id)
        {
            _uow.BeginTransaction();
            var bloco = await _blocoRepository.GetByIdAsync(id);
            _uow.Commit();

            return bloco;
        }

        public async Task<string> CreateAsync(Bloco bloco)
        {
            _uow.BeginTransaction();
            var id = await _blocoRepository.RegisterAsync(_blocoFactory.FromCreate(bloco));
            _uow.Commit();

            return id;
        }

        public async Task PutAsync(Bloco bloco)
        {
            _uow.BeginTransaction();
            await _blocoRepository.UpdateAsync(bloco);
            _uow.Commit();
        }
    }
}
