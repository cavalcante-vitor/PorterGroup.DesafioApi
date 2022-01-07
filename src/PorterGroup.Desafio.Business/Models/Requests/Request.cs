using System;

namespace PorterGroup.Desafio.Business.Models.Requests
{
    public record Request
    {
        public Guid Id { get; init; }

        public string Type { get; init; }

        public string Sort { get; init; }

        public int Skip { get; init; }

        public int Take { get; init; }

        public DateTimeOffset DateTimeNow { get; } = DateTimeOffset.Now;
    }
}
