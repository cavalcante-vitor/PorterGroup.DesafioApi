using System.Threading.Tasks;

namespace PorterGroup.Desafio.WarmUp.Abtraction
{
    public interface IHttpCallWarmerCommand
    {
        Task ExecuteAsync();
    }
}
