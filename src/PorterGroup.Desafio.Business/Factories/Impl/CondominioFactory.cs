using System;
using PorterGroup.Desafio.Business.Entities;
using PorterGroup.Desafio.Shared.Contexts;

namespace PorterGroup.Desafio.Business.Factories
{
    internal class CondominioFactory : ICondominioFactory
    {
        private readonly IRequestContextHolder _requestContext;

        public CondominioFactory(IRequestContextHolder requestContext) =>
            _requestContext = requestContext;

        public Condominio FromCreate(Condominio condominio) =>
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Nome = condominio.Nome,
                EmailSindico = condominio.EmailSindico,
                Telefone = condominio.Telefone,
            };
    }
}
