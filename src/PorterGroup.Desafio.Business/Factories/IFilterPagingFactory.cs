using PorterGroup.Desafio.Business.Models.Requests;

namespace PorterGroup.Desafio.Business.Factories
{
    public interface IFilterPagingFactory
    {
        FilterPaging From(Request request);
    }
}
