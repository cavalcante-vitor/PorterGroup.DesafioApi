using System;

namespace PorterGroup.Desafio.Business.Entities
{
    public record Condominio
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string EmailSindico { get; set; }
    }
}
