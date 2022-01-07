﻿namespace PorterGroup.Desafio.Infra.Data.Models
{
    public record FilterPaging
    {
        public string Sort { get; init; }

        public int? Skip { get; init; }

        public int? Take { get; init; }
    }
}
