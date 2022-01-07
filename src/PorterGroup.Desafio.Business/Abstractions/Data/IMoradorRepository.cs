using System;
using System.Threading.Tasks;
using PorterGroup.Desafio.Business.Entities;

namespace PorterGroup.Desafio.Business.Abstractions.Data
{
    public interface IMoradorRepository
    {
        Task<string> RegisterAsync(Morador morador);

        Task UpdateAsync(Morador morador);

        Task<Morador> GetByIdAsync(string id);
    }
}
