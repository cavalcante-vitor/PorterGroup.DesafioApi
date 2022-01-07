using Newtonsoft.Json;
using PorterGroup.Desafio.Business.Models.Requests;

namespace PorterGroup.Desafio.Business.Factories
{
    internal class FilterPagingFactory : IFilterPagingFactory
    {
        public FilterPaging From(Request request)
        {
            return
                new()
                {
                    Sort = request.Sort,
                    Skip = request.Skip,
                    Take = request.Take,
                };
        }
    }
}
