using System;

namespace PorterGroup.Desafio.Business.Models.Requests
{
    public record CondominioRequest
    {
        public Guid Id { get; } = Guid.NewGuid();

        public string Type { get; } = "condominio";

    }
}
