using System.Threading.Tasks;
using PorterGroup.Desafio.Business.Entities;
using PorterGroup.Desafio.Business.Models.Requests;

namespace PorterGroup.Desafio.Business.Factories
{
    public interface ICondominioFactory
    {
        Condominio FromCreate(Condominio condominio);
    }
}
