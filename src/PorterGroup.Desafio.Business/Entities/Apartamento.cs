using System;
using System.Collections.Generic;

namespace PorterGroup.Desafio.Business.Entities
{
    public record Apartamento
    {
        public string Id { get; init; }
        public string Numero { get; init; }
        public string Andar { get; init; }
        public string BlocoId { get; init; }
    }
}
