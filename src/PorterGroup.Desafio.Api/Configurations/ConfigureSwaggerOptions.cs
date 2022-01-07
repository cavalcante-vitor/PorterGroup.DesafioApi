using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PorterGroup.Desafio.Api.Configurations
{
    [ExcludeFromCodeCoverage]
    internal class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            _provider.ApiVersionDescriptions
                .ToList()
                .ForEach(d =>
                {
                    var info = new OpenApiInfo
                    {
                        Title = "PorterGroup.Desafio.Api",
                        Description = "PorterGroup.Desafio (ASP.NET net6.0)",
                        Version = d.ApiVersion.ToString(),
                        Contact = new OpenApiContact()
                        {
                            Name = "Desafio Técnico PorterGroup.DesafioApi",
                            Email = "vitor.cavalcante@gmail.com",
                        },
                    };

                    options.SwaggerDoc(d.GroupName, info);
                });

            options.ExampleFilters();
        }
    }
}
