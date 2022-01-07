using System;

namespace PorterGroup.Desafio.Shared.Contexts
{
    public interface IRequestContextHolder
    {
        public Guid CorrelationId { get; set; }

        public object RequestBody { get; set; }

        public string RequestUser { get; set; }
    }
}
