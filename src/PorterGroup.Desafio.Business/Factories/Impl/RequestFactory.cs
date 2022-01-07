using System.Text;
using Newtonsoft.Json;
using PorterGroup.Desafio.Business.Models.Requests;

namespace PorterGroup.Desafio.Business.Factories
{
    internal class RequestFactory : IRequestFactory
    {
        public Request From(string request)
        {
            var data = System.Convert.FromBase64String(request);
            var decodeString = ASCIIEncoding.ASCII.GetString(data);
            return JsonConvert.DeserializeObject<Request>(decodeString);
        }
    }
}
