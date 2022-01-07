using System;
using System.Threading.Tasks;
using PorterGroup.Desafio.Business.Entities;

namespace PorterGroup.Desafio.Business.Abstractions.Data
{
    public interface IApartamentoRepository
    {
        Task<string> RegisterAsync(Apartamento apartamento);

        Task UpdateAsync(Apartamento apartamento);

        Task<Apartamento> GetByIdAsync(string id);
    }
}
