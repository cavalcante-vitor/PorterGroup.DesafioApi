using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PorterGroup.Desafio.Infra.Data.Configurations
{
    [ExcludeFromCodeCoverage]
    internal record DatabaseConfig
    {
        [Required(AllowEmptyStrings = false)]
        public string ConnectionString { get; init; }
    }
}
