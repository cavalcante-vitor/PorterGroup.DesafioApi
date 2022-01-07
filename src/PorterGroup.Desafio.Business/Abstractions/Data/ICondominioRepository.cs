using System;
using System.Threading.Tasks;
using PorterGroup.Desafio.Business.Entities;

namespace PorterGroup.Desafio.Business.Abstractions.Data
{
    public interface ICondominioRepository
    {
        Task<string> RegisterAsync(Condominio condominio);

        Task UpdateAsync(Condominio condominio);

        Task<Condominio> GetByIdAsync(string id);
    }
}
