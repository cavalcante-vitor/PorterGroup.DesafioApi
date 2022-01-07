using System.Data;

namespace PorterGroup.Desafio.Infra.Data.Context
{
    public interface IConnectionFactory
    {
        IDbConnection GetNewConnection();
    }
}
