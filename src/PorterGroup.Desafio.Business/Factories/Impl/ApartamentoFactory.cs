using System;
using PorterGroup.Desafio.Business.Entities;
using PorterGroup.Desafio.Shared.Contexts;

namespace PorterGroup.Desafio.Business.Factories
{
    internal class ApartamentoFactory : IApartamentoFactory
    {
        private readonly IRequestContextHolder _requestContext;

        public ApartamentoFactory(IRequestContextHolder requestContext) =>
            _requestContext = requestContext;

        public Apartamento FromCreate(Apartamento apartamento) =>
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Andar = apartamento.Andar,
                BlocoId = apartamento.BlocoId,
                Numero = apartamento.Numero,
            };
    }
}
