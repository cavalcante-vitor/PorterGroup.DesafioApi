using System.Threading.Tasks;

namespace PorterGroup.Desafio.WarmUp.Abtraction
{
    public interface IWarmerCommand
    {
        Task ExecuteAsync();
    }
}
