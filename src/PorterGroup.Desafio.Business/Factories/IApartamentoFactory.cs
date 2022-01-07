using System.Threading.Tasks;
using PorterGroup.Desafio.Business.Entities;
using PorterGroup.Desafio.Business.Models.Requests;

namespace PorterGroup.Desafio.Business.Factories
{
    public interface IApartamentoFactory
    {
        Apartamento FromCreate(Apartamento apartamento);
    }
}
