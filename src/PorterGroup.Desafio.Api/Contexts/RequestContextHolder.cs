using System;
using System.Diagnostics.CodeAnalysis;
using PorterGroup.Desafio.Shared.Contexts;

namespace PorterGroup.Desafio.Api.Contexts
{
    [ExcludeFromCodeCoverage]
    internal class RequestContextHolder : IRequestContextHolder
    {
        public Guid CorrelationId { get; set; }

        public object RequestBody { get; set; }

        public string RequestUser { get; set; }
    }
}
