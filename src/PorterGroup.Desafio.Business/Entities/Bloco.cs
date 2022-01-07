using System;

namespace PorterGroup.Desafio.Business.Entities
{
    public record Bloco
    {
        public string Id { get; init; }
        public string Nome { get; init; }
        public string CondominioId { get; init; }
    }
}
