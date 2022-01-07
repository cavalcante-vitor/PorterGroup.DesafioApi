using System.Threading.Tasks;
using PorterGroup.Desafio.Business.Entities;

namespace PorterGroup.Desafio.Business.Services
{
    public interface ICondominioService
    {
        Task<Condominio> GetByIdAsync(
            string id);

        Task<string> CreateAsync(
            Condominio condominio);

        Task PutAsync(
            Condominio condominio);
    }
}
