using System;
using PorterGroup.Desafio.Business.Entities;
using PorterGroup.Desafio.Shared.Contexts;

namespace PorterGroup.Desafio.Business.Factories
{
    internal class BlocoFactory : IBlocoFactory
    {
        private readonly IRequestContextHolder _requestContext;

        public BlocoFactory(IRequestContextHolder requestContext) =>
            _requestContext = requestContext;

        public Bloco FromCreate(Bloco bloco) =>
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Nome = bloco.Nome,
                CondominioId = bloco.CondominioId,
            };
    }
}
