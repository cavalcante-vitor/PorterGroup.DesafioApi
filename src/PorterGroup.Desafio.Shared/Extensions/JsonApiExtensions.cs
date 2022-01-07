using System.Collections.Generic;
using JsonApiSerializer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PorterGroup.Desafio.Shared.Extensions
{
    public static class JsonApiExtensions
    {
        public static IEnumerable<JObject> ToJObjects<T>(this IEnumerable<T> objects)
            where T : class
        {
            var settings = new JsonApiSerializerSettings();

            foreach (var item in objects)
            {
                var serialized = JsonConvert.SerializeObject(item, settings);
                yield return JObject.Parse(serialized).Value<JObject>("data");
            }
        }
    }
}
