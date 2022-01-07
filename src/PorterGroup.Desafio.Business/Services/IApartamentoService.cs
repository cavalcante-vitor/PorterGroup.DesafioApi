using System.Threading.Tasks;
using PorterGroup.Desafio.Business.Entities;

namespace PorterGroup.Desafio.Business.Services
{
    public interface IApartamentoService
    {
        Task<Apartamento> GetByIdAsync(
            string id);

        Task<string> CreateAsync(
            Apartamento apartamento);

        Task PutAsync(
            Apartamento apartamento);
    }
}
