using PorterGroup.Desafio.Business.Entities;

namespace PorterGroup.Desafio.Business.Factories
{
    public interface IBlocoFactory
    {
        Bloco FromCreate(Bloco bloco);
    }
}
