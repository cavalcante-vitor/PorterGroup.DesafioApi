using PorterGroup.Desafio.Business.Entities;

namespace PorterGroup.Desafio.Business.Factories
{
    public interface IMoradorFactory
    {
        Morador FromCreate(Morador morador);
    }
}
