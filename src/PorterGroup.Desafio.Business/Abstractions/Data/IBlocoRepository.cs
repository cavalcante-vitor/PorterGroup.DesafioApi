using System;
using System.Threading.Tasks;
using PorterGroup.Desafio.Business.Entities;

namespace PorterGroup.Desafio.Business.Abstractions.Data
{
    public interface IBlocoRepository
    {
        Task<string> RegisterAsync(Bloco bloco);

        Task UpdateAsync(Bloco bloco);

        Task<Bloco> GetByIdAsync(string id);
    }
}
