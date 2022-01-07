using System;

namespace PorterGroup.Desafio.Business.Entities
{
    public record Morador
    {
        public string Id { get; init; }
        public string Nome { get; init; }
        public DateTimeOffset DataNascimento { get; init; }
        public string Telefone { get; init; }
        public string Cpf { get; init; }
        public string Email { get; init; }
        public string ApartamentoId { get; init; }
    }
}
