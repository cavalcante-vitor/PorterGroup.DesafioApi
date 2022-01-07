using System;
using PorterGroup.Desafio.Business.Entities;
using PorterGroup.Desafio.Shared.Contexts;

namespace PorterGroup.Desafio.Business.Factories
{
    internal class MoradorFactory : IMoradorFactory
    {
        private readonly IRequestContextHolder _requestContext;

        public MoradorFactory(IRequestContextHolder requestContext) =>
            _requestContext = requestContext;

        public Morador FromCreate(Morador morador) =>
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Nome = morador.Nome,
                Cpf = morador.Cpf,
                DataNascimento = morador.DataNascimento,
                Email = morador.Email,
                Telefone = morador.Telefone,
                ApartamentoId = morador.ApartamentoId,
            };
    }
}
