using System.Threading.Tasks;
using PorterGroup.Desafio.Business.Entities;

namespace PorterGroup.Desafio.Business.Services
{
    public interface IMoradorService
    {
        Task<Morador> GetByIdAsync(
            string id);

        Task<string> CreateAsync(
            Morador morador);

        Task PutAsync(
            Morador morador);
    }
}
