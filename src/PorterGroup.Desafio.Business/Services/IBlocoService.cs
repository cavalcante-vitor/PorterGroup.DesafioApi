using System.Threading.Tasks;
using PorterGroup.Desafio.Business.Entities;

namespace PorterGroup.Desafio.Business.Services
{
    public interface IBlocoService
    {
        Task<Bloco> GetByIdAsync(
            string id);

        Task<string> CreateAsync(
            Bloco bloco);

        Task PutAsync(
            Bloco bloco);
    }
}
