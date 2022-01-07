using System.Collections.Generic;
using System.Linq;

namespace PorterGroup.Desafio.Business.Extensions
{
    public static class EnumerableExtensions
    {
        public static T Second<T>(
            this IEnumerable<T> enumerable)
        {
            return enumerable.Skip(1).Take(1).First();
        }
    }
}
